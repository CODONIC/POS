using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;

namespace POS.Admin
{
    public class BusinessStatsService
    {
        private readonly string _connectionString;
        private readonly Guid _companyId;

        public BusinessStatsService(string connectionString, Guid companyId)
        {
            _connectionString = connectionString;
            _companyId = companyId;
        }

        public KpiData FetchKPIs(DateTime from, DateTime to)
        {
            var data = new KpiData();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            // Transaction KPIs
            using (var cmd = new NpgsqlCommand(@"
                SELECT COALESCE(SUM(CASE WHEN LOWER(status)='completed' THEN subtotal END),0),
                       COALESCE(SUM(CASE WHEN LOWER(status)='completed' THEN total_amount END),0),
                       COUNT(*), COALESCE(AVG(CASE WHEN LOWER(status)='completed' THEN total_amount END),0),
                       COALESCE(SUM(CASE WHEN LOWER(status)='completed' THEN discount_amount END),0),
                       COALESCE(SUM(CASE WHEN LOWER(status)='completed' THEN vat_amount END),0),
                       COUNT(CASE WHEN LOWER(status)='completed' THEN 1 END),
                       COUNT(CASE WHEN LOWER(status)='voided' THEN 1 END)
                FROM public.transactions WHERE company_id=@companyId AND transaction_date>=@from AND transaction_date<=@to", conn))
            {
                cmd.Parameters.AddWithValue("companyId", _companyId);
                cmd.Parameters.AddWithValue("from", from);
                cmd.Parameters.AddWithValue("to", to);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    data.GrossSales = Convert.ToDecimal(r[0]);
                    data.TotalRevenue = Convert.ToDecimal(r[1]);
                    data.TotalTransactions = Convert.ToInt64(r[2]);
                    data.AvgTransactionValue = Convert.ToDecimal(r[3]);
                    data.TotalDiscount = Convert.ToDecimal(r[4]);
                    data.TotalVAT = Convert.ToDecimal(r[5]);
                    data.CompletedTransactions = Convert.ToInt64(r[6]);
                    data.VoidedTransactions = Convert.ToInt64(r[7]);
                }
            }

            // Items Sold
            using (var cmd = new NpgsqlCommand(@"
                SELECT COALESCE(SUM(ti.quantity),0) FROM public.transaction_items ti 
                JOIN public.transactions t ON t.id = ti.transaction_id 
                WHERE t.company_id=@companyId AND LOWER(t.status)='completed' AND t.transaction_date>=@from AND t.transaction_date<=@to", conn))
            {
                cmd.Parameters.AddWithValue("companyId", _companyId);
                cmd.Parameters.AddWithValue("from", from);
                cmd.Parameters.AddWithValue("to", to);
                data.TotalItemsSold = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
            }

            // Inventory KPIs
            using (var cmd = new NpgsqlCommand(@"
                SELECT COALESCE(SUM(quantity),0), COUNT(CASE WHEN quantity>0 AND quantity<=reorder_level THEN 1 END), COUNT(CASE WHEN quantity=0 THEN 1 END)
                FROM public.products WHERE company_id=@companyId", conn))
            {
                cmd.Parameters.AddWithValue("companyId", _companyId);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    data.TotalProducts = Convert.ToInt64(r[0]);
                    data.LowStockCount = Convert.ToInt64(r[1]);
                    data.OutOfStockCount = Convert.ToInt64(r[2]);
                }
            }

            // Daily Revenue
            using (var cmd = new NpgsqlCommand(@"
                SELECT DATE(transaction_date), COALESCE(SUM(total_amount),0) FROM public.transactions 
                WHERE company_id=@companyId AND LOWER(status)='completed' AND transaction_date>=@from AND transaction_date<=@to 
                GROUP BY DATE(transaction_date) ORDER BY DATE(transaction_date) ASC", conn))
            {
                cmd.Parameters.AddWithValue("companyId", _companyId);
                cmd.Parameters.AddWithValue("from", from);
                cmd.Parameters.AddWithValue("to", to);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    data.DailyLabels.Add(r.GetFieldValue<DateOnly>(0).ToString("MMM dd"));
                    data.DailyRevenue.Add(Convert.ToDecimal(r[1]));
                }
            }

            // Top 10 Best Sellers - FIXED: removed ORDER BY total_sold and added alias properly
            using (var cmd = new NpgsqlCommand(@"
                SELECT ti.product_name, COALESCE(SUM(ti.quantity),0) as total_sold
                FROM public.transaction_items ti 
                JOIN public.transactions t ON t.id = ti.transaction_id 
                WHERE t.company_id=@companyId AND LOWER(t.status)='completed' AND t.transaction_date>=@from AND t.transaction_date<=@to 
                GROUP BY ti.product_name 
                ORDER BY total_sold DESC 
                LIMIT 10", conn))
            {
                cmd.Parameters.AddWithValue("companyId", _companyId);
                cmd.Parameters.AddWithValue("from", from);
                cmd.Parameters.AddWithValue("to", to);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    data.TopProductNames.Add(r.GetString(0));
                    data.TopProductSales.Add(r.IsDBNull(1) ? 0m : Convert.ToDecimal(r[1]));
                }
            }

            return data;
        }
    }

    public class KpiData
    {
        public long TotalProducts { get; set; }
        public decimal GrossSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public long TotalTransactions { get; set; }
        public decimal AvgTransactionValue { get; set; }
        public long TotalItemsSold { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalVAT { get; set; }
        public long CompletedTransactions { get; set; }
        public long VoidedTransactions { get; set; }
        public long LowStockCount { get; set; }
        public long OutOfStockCount { get; set; }
        public List<string> DailyLabels { get; set; } = new();
        public List<decimal> DailyRevenue { get; set; } = new();
        public List<string> TopProductNames { get; set; } = new();
        public List<decimal> TopProductSales { get; set; } = new();
    }
}