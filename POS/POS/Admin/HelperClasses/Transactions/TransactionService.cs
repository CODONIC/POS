using System;
using System.Data;
using Npgsql;

namespace POS.Admin
{
    public class TransactionService
    {
        private readonly string _companyId;
        private readonly string _connectionString;

        public TransactionService(string companyId)
        {
            _companyId = companyId;
            _connectionString = DatabaseService.ConnectionString;
        }

        public DataTable GetAllTransactions()
        {
            var dt = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string sql = @"
                SELECT
                    transaction_number,
                    transaction_date,
                    cashier_name,
                    subtotal,
                    discount_percentage,
                    discount_amount,
                    vatable_amount,
                    vat_amount,
                    total_amount,
                    payment_method,
                    customer_payment,
                    change_amount,
                    status
                FROM public.transactions
                WHERE company_id = @companyId
                ORDER BY transaction_date DESC";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public DataView SearchTransactions(DataTable transactions, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return transactions.DefaultView;
            }

            string sanitizedKeyword = keyword.Replace("'", "''");
            transactions.DefaultView.RowFilter =
                $"transaction_number LIKE '%{sanitizedKeyword}%' OR " +
                $"cashier_name LIKE '%{sanitizedKeyword}%' OR " +
                $"payment_method LIKE '%{sanitizedKeyword}%' OR " +
                $"status LIKE '%{sanitizedKeyword}%'";

            return transactions.DefaultView;
        }
    }
}