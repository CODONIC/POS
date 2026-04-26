using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace POS.Admin
{
    public partial class BusinessStatsForm : BaseForm
    {
        private string _username;
        private string _companyName;
        private Guid _companyId;

        public BusinessStatsForm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = FetchCompanyId(companyName);
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";

            SetDateDefaults();
            LoadKPIs();

            // Wire up date filter controls
            dtpFrom.ValueChanged += (s, e) => LoadKPIs();
            dtpTo.ValueChanged += (s, e) => LoadKPIs();
            cmbQuickFilter.SelectedIndexChanged += CmbQuickFilter_SelectedIndexChanged;
        }

        private Guid FetchCompanyId(string companyName)
        {
            using (var conn = new NpgsqlConnection(DatabaseService.ConnectionString))
            {
                conn.Open();
                string sql = "SELECT id FROM public.companies WHERE name = @name LIMIT 1";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("name", companyName);
                    var result = cmd.ExecuteScalar();

                    if (result == null)
                        throw new Exception($"Company '{companyName}' not found in database.");

                    return (Guid)result;
                }
            }
        }

        // ─── Date Defaults ────────────────────────────────────────────────────

        private void SetDateDefaults()
        {
            // Populate quick filter
            cmbQuickFilter.Items.AddRange(new[] { "Today", "This Week", "This Month", "This Year", "All Time", "Custom" });
            cmbQuickFilter.SelectedIndex = 2; // default: This Month
        }

        private void CmbQuickFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;
            bool isCustom = cmbQuickFilter.SelectedItem?.ToString() == "Custom";

            dtpFrom.Enabled = isCustom;
            dtpTo.Enabled = isCustom;

            switch (cmbQuickFilter.SelectedItem?.ToString())
            {
                case "Today":
                    dtpFrom.Value = now;
                    dtpTo.Value = now;
                    break;
                case "This Week":
                    int diff = (int)now.DayOfWeek;
                    dtpFrom.Value = now.AddDays(-diff);
                    dtpTo.Value = now;
                    break;
                case "This Month":
                    dtpFrom.Value = new DateTime(now.Year, now.Month, 1);
                    dtpTo.Value = now;
                    break;
                case "This Year":
                    dtpFrom.Value = new DateTime(now.Year, 1, 1);
                    dtpTo.Value = now;
                    break;
                case "All Time":
                    dtpFrom.Value = new DateTime(2000, 1, 1);
                    dtpTo.Value = now;
                    break;
            }

            if (!isCustom) LoadKPIs();
        }

        // ─── KPI Loading ──────────────────────────────────────────────────────

        private async void LoadKPIs()
        {
            try
            {
                SetLoadingState(true);

                DateTime from = dtpFrom.Value.Date;
                DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1); // end of day

                var kpi = await Task.Run(() => FetchKPIs(from, to));

                // Update KPI labels
                lblTotalProducts.Text = kpi.TotalProducts.ToString("N0");
                lblGrossSales.Text = kpi.GrossSales.ToString("C2");
                lblTotalRevenue.Text = kpi.TotalRevenue.ToString("C2");
                lblTotalTransactions.Text = kpi.TotalTransactions.ToString("N0");
                lblAvgTransValue.Text = kpi.AvgTransactionValue.ToString("C2");
                lblTotalItemsSold.Text = kpi.TotalItemsSold.ToString("N0");
                lblTotalDiscount.Text = kpi.TotalDiscount.ToString("C2");
                lblTotalVAT.Text = kpi.TotalVAT.ToString("C2");
                lblCompletedTrans.Text = kpi.CompletedTransactions.ToString("N0");
                lblVoidedTrans.Text = kpi.VoidedTransactions.ToString("N0");
                lblLowStockCount.Text = kpi.LowStockCount.ToString("N0");
                lblOutOfStockCount.Text = kpi.OutOfStockCount.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading KPIs:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool loading)
        {
            if (InvokeRequired) { Invoke(new Action<bool>(SetLoadingState), loading); return; }

            // Show a subtle loading indicator — adjust control name as needed
            lblLoadingIndicator.Visible = loading;
            btnRefresh.Enabled = !loading;
        }

        // ─── Database Query ───────────────────────────────────────────────────

        private KpiData FetchKPIs(DateTime from, DateTime to)
        {
            var data = new KpiData();

            using (var conn = new NpgsqlConnection(DatabaseService.ConnectionString))
            {
                conn.Open();

                // ── Transaction KPIs ──────────────────────────────────────────────
                string transSql = @"
    SELECT
        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN subtotal       ELSE 0 END), 0) AS gross_sales,
        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN total_amount   ELSE 0 END), 0) AS total_revenue,
        COUNT(*)                                                                                 AS total_transactions,
        COALESCE(AVG(CASE WHEN LOWER(status) = 'completed' THEN total_amount   END),    0)     AS avg_value,
        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN discount_amount ELSE 0 END), 0) AS total_discount,
        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN vat_amount      ELSE 0 END), 0) AS total_vat,
        COUNT(CASE WHEN LOWER(status) = 'completed' THEN 1 END)                                AS completed_count,
        COUNT(CASE WHEN LOWER(status) = 'voided'    THEN 1 END)                                AS voided_count
    FROM public.transactions
    WHERE company_id        = @companyId
      AND transaction_date >= @from
      AND transaction_date <= @to";

                using (var cmd = new NpgsqlCommand(transSql, conn))
                {
                    cmd.Parameters.AddWithValue("companyId", _companyId);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.GrossSales = Convert.ToDecimal(reader[0]); // ADD
                            data.TotalRevenue = Convert.ToDecimal(reader[1]); // was [0]
                            data.TotalTransactions = Convert.ToInt64(reader[2]);   // was [1]
                            data.AvgTransactionValue = Convert.ToDecimal(reader[3]); // was [2]
                            data.TotalDiscount = Convert.ToDecimal(reader[4]); // was [3]
                            data.TotalVAT = Convert.ToDecimal(reader[5]); // was [4]
                            data.CompletedTransactions = Convert.ToInt64(reader[6]);   // was [5]
                            data.VoidedTransactions = Convert.ToInt64(reader[7]);   // was [6]
                        }
                    }
                }

                // ── Items Sold ────────────────────────────────────────────────────
                string itemsSql = @"
            SELECT COALESCE(SUM(ti.quantity), 0)
            FROM   public.transaction_items ti
            JOIN   public.transactions      t  ON t.id = ti.transaction_id
            WHERE  t.company_id        = @companyId
              AND  LOWER(t.status)     = 'completed'
              AND  t.transaction_date >= @from
              AND  t.transaction_date <= @to";

                using (var cmd = new NpgsqlCommand(itemsSql, conn))
                {
                    cmd.Parameters.AddWithValue("companyId", _companyId);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);

                    data.TotalItemsSold = Convert.ToInt64(cmd.ExecuteScalar() ?? 0L);
                }

                // ── Inventory KPIs ────────────────────────────────────────────────
                string stockSql = @"
            SELECT
        COALESCE(SUM(quantity), 0)                                      AS total_products,
        COUNT(CASE WHEN quantity > 0 AND quantity <= reorder_level THEN 1 END) AS low_stock,
        COUNT(CASE WHEN quantity = 0                               THEN 1 END) AS out_of_stock
    FROM public.products
    WHERE company_id = @companyId";

                using (var cmd = new NpgsqlCommand(stockSql, conn))
                {
                    cmd.Parameters.AddWithValue("companyId", _companyId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.TotalProducts = Convert.ToInt64(reader[0]); // ADD — shift others +1
                            data.LowStockCount = Convert.ToInt64(reader[1]); // was [0]
                            data.OutOfStockCount = Convert.ToInt64(reader[2]); // was [1]
                        }
                    }
                }
            }

            return data;
        }

        // ─── Refresh Button ───────────────────────────────────────────────────

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadKPIs();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard(_username, _companyName);
            admin.Show();
            this.Hide();
        }
    }

    // ─── KPI Data Model ───────────────────────────────────────────────────────

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
    }
}