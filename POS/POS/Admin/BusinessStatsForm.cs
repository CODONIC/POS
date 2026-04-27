using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using Npgsql;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class BusinessStatsForm : BaseForm
    {
        private string _username;
        private string _companyName;
        private Guid _companyId;
        private KpiData _lastKpi;

        private bool _isLineChart = false;
        private bool _isBestSellersView = false;

        public BusinessStatsForm(string username, string companyName)
        {
            InitializeComponent();
            cartesianChart1.AnimationsSpeed = TimeSpan.Zero;
            cartesianChart1.EasingFunction = null;
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = FetchCompanyId(companyName);
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";

            SetDateDefaults();
            LoadKPIs();

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
            cmbQuickFilter.Items.AddRange(new[] { "Today", "This Week", "This Month", "This Year", "All Time", "Custom" });
            cmbQuickFilter.SelectedIndex = 2;
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
                DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);

                var kpi = await Task.Run(() => FetchKPIs(from, to));
                _lastKpi = kpi;

                // ── KPI Labels ────────────────────────────────────────────────
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

                // ── Chart ─────────────────────────────────────────────────────
                if (_isBestSellersView)
                    LoadBestSellersChart(kpi);
                else
                    LoadRevenueChart(kpi);
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
            lblLoadingIndicator.Visible = loading;
            btnRefresh.Enabled = !loading;
        }

        // ─── Revenue Chart ────────────────────────────────────────────────────

        private void LoadRevenueChart(KpiData kpi)
        {
            if (kpi.DailyLabels == null || kpi.DailyLabels.Count == 0)
            {
                cartesianChart1.Series = Array.Empty<ISeries>();
                cartesianChart1.XAxes = new Axis[] { new Axis { Labels = new[] { "No data" } } };
                cartesianChart1.YAxes = new Axis[] { new Axis() };
                return;
            }

            var xAxis = new Axis
            {
                Labels = kpi.DailyLabels,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 11,
                LabelsRotation = -45,
            };

            var yAxis = new Axis
            {
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 11,
                Labeler = val => $"₱{val:N0}",
            };

            ISeries series = _isLineChart
                ? new LineSeries<decimal>
                {
                    Name = "Daily Revenue",
                    Values = kpi.DailyRevenue,
                    Fill = new SolidColorPaint(new SKColor(59, 130, 246, 40)),
                    Stroke = new SolidColorPaint(new SKColor(59, 130, 246)) { StrokeThickness = 2 },
                    GeometrySize = 6,
                    GeometryFill = new SolidColorPaint(SKColors.White),
                    GeometryStroke = new SolidColorPaint(new SKColor(59, 130, 246)) { StrokeThickness = 2 },
                }
                : new ColumnSeries<decimal>
                {
                    Name = "Daily Revenue",
                    Values = kpi.DailyRevenue,
                    Fill = new SolidColorPaint(new SKColor(59, 130, 246)),
                    Stroke = null,
                    Rx = 4,
                    Ry = 4,
                };

            cartesianChart1.Series = new ISeries[] { series };
            cartesianChart1.XAxes = new Axis[] { xAxis };
            cartesianChart1.YAxes = new Axis[] { yAxis };
            //cartesianChart1.AnimationsSpeed = TimeSpan.Zero;
            cartesianChart1.LegendPosition = LiveChartsCore.Measure.LegendPosition.Top;
            cartesianChart1.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
        }

        // ─── Best Sellers Chart ───────────────────────────────────────────────

        private void LoadBestSellersChart(KpiData kpi)
        {
            if (kpi.TopProductNames == null || kpi.TopProductNames.Count == 0)
            {
                cartesianChart1.Series = Array.Empty<ISeries>();
                cartesianChart1.XAxes = new Axis[] { new Axis { Labels = new[] { "No data" } } };
                cartesianChart1.YAxes = new Axis[] { new Axis() };
                return;
            }

            // Create a list and sort by sales descending (highest first)
            var products = new List<(string Name, decimal Sales)>();
            for (int i = 0; i < kpi.TopProductNames.Count; i++)
            {
                products.Add((kpi.TopProductNames[i], kpi.TopProductSales[i]));
            }

            // Sort by sales descending (highest first)
            products = products.OrderByDescending(p => p.Sales).ToList();

            // Debug: Check the values
            foreach (var p in products)
            {
                System.Diagnostics.Debug.WriteLine($"Product: {p.Name}, Sales: {p.Sales}");
            }

            // For RowSeries, reverse so highest is at top
            var displayNames = products.Select(p => p.Name).Reverse().ToList();
            var displaySales = products.Select(p => p.Sales).Reverse().ToList();

            // Create labels with correct numbering
            var totalCount = displayNames.Count;
            var labelsWithNumbers = displayNames.Select((name, index) => $"{totalCount - index}. {name}").ToList();

            var series = new RowSeries<decimal>
            {
                Name = "Top 10 Products Sold",
                
                Values = displaySales,  // Now should have correct values (100, 50, etc.)
                Fill = new SolidColorPaint(new SKColor(16, 185, 129)),
                Stroke = null,
                Rx = 4,
                Ry = 4,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 11,
                //DataLabelsFormatter = (point) => $"{point.Model:N0}",
                DataLabelsFormatter = point => $"{point.Coordinate.PrimaryValue:N0} units",
                MaxBarWidth = 30,
                Padding = 6,

            };

            var xAxis = new Axis
            {
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 11,
                Labeler = val => $"{val:N0} units",
                MinStep = 1,
                //ForceStepToMin = true,
                MinLimit = 0,
            };

            var yAxis = new Axis
            {
                NameTextSize = 9,
                Labels = labelsWithNumbers,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 11,
                MinStep = 1,
                ForceStepToMin = true,
                //MinLimit = 0,
                LabelsAlignment = LiveChartsCore.Drawing.Align.Start,
                Padding = new LiveChartsCore.Drawing.Padding(5, 10, 0, 10)
            };

            cartesianChart1.Series = new ISeries[] { series };
            cartesianChart1.XAxes = new Axis[] { xAxis };
            cartesianChart1.YAxes = new Axis[] { yAxis };
            cartesianChart1.AnimationsSpeed = TimeSpan.Zero;
            cartesianChart1.LegendPosition = LiveChartsCore.Measure.LegendPosition.Top;
            cartesianChart1.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
            cartesianChart1.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Hidden;
            
            //cartesianChart1.DrawMarginFrame = null;

        }
        // ─── Chart Toggle Buttons ─────────────────────────────────────────────

        private void btnToggleChart_Click(object sender, EventArgs e)
        {
            _isLineChart = !_isLineChart;
            btnToggleChart.Text = _isLineChart ? "📊 Bar" : "📈 Line";
            if (_lastKpi != null)
                LoadRevenueChart(_lastKpi);
        }

        private void btnBestSellers_Click(object sender, EventArgs e)
        {
            _isBestSellersView = !_isBestSellersView;
            btnBestSellers.Text = _isBestSellersView ? "📈 Revenue" : "🏆 Best Sellers";
            btnToggleChart.Enabled = !_isBestSellersView;

            if (_lastKpi != null)
            {
                if (_isBestSellersView)
                    LoadBestSellersChart(_lastKpi);
                else
                    LoadRevenueChart(_lastKpi);
            }
        }

        // ─── Database Query ───────────────────────────────────────────────────

        private KpiData FetchKPIs(DateTime from, DateTime to)
        {
            var data = new KpiData();

            using (var conn = new NpgsqlConnection(DatabaseService.ConnectionString))
            {
                conn.Open();

                // ── Transaction KPIs ──────────────────────────────────────────
                string transSql = @"
                    SELECT
                        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN subtotal        ELSE 0 END), 0) AS gross_sales,
                        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN total_amount    ELSE 0 END), 0) AS total_revenue,
                        COUNT(*)                                                                                  AS total_transactions,
                        COALESCE(AVG(CASE WHEN LOWER(status) = 'completed' THEN total_amount    END),    0)     AS avg_value,
                        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN discount_amount ELSE 0 END), 0) AS total_discount,
                        COALESCE(SUM(CASE WHEN LOWER(status) = 'completed' THEN vat_amount      ELSE 0 END), 0) AS total_vat,
                        COUNT(CASE WHEN LOWER(status) = 'completed' THEN 1 END)                                 AS completed_count,
                        COUNT(CASE WHEN LOWER(status) = 'voided'    THEN 1 END)                                 AS voided_count
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
                            data.GrossSales = Convert.ToDecimal(reader[0]);
                            data.TotalRevenue = Convert.ToDecimal(reader[1]);
                            data.TotalTransactions = Convert.ToInt64(reader[2]);
                            data.AvgTransactionValue = Convert.ToDecimal(reader[3]);
                            data.TotalDiscount = Convert.ToDecimal(reader[4]);
                            data.TotalVAT = Convert.ToDecimal(reader[5]);
                            data.CompletedTransactions = Convert.ToInt64(reader[6]);
                            data.VoidedTransactions = Convert.ToInt64(reader[7]);
                        }
                    }
                }

                // ── Items Sold ────────────────────────────────────────────────
                string itemsSql = @"
                    SELECT COALESCE(SUM(ti.quantity), 0)
                    FROM   public.transaction_items ti
                    JOIN   public.transactions      t ON t.id = ti.transaction_id
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

                // ── Inventory KPIs ────────────────────────────────────────────
                string stockSql = @"
                    SELECT
                        COALESCE(SUM(quantity), 0)                                              AS total_stock,
                        COUNT(CASE WHEN quantity > 0 AND quantity <= reorder_level THEN 1 END)  AS low_stock,
                        COUNT(CASE WHEN quantity = 0                               THEN 1 END)  AS out_of_stock
                    FROM public.products
                    WHERE company_id = @companyId";

                using (var cmd = new NpgsqlCommand(stockSql, conn))
                {
                    cmd.Parameters.AddWithValue("companyId", _companyId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.TotalProducts = Convert.ToInt64(reader[0]);
                            data.LowStockCount = Convert.ToInt64(reader[1]);
                            data.OutOfStockCount = Convert.ToInt64(reader[2]);
                        }
                    }
                }

                // ── Daily Revenue ─────────────────────────────────────────────
                string dailySql = @"
                    SELECT
                        DATE(transaction_date)         AS day,
                        COALESCE(SUM(total_amount), 0) AS daily_revenue
                    FROM public.transactions
                    WHERE company_id        = @companyId
                      AND LOWER(status)     = 'completed'
                      AND transaction_date >= @from
                      AND transaction_date <= @to
                    GROUP BY DATE(transaction_date)
                    ORDER BY DATE(transaction_date) ASC";

                using (var cmd = new NpgsqlCommand(dailySql, conn))
                {
                    cmd.Parameters.AddWithValue("companyId", _companyId);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dateOnly = reader.GetFieldValue<DateOnly>(0);
                            data.DailyLabels.Add(dateOnly.ToString("MMM dd"));
                            data.DailyRevenue.Add(Convert.ToDecimal(reader[1]));
                        }
                    }
                }

                // ── Top 10 Best Sellers ───────────────────────────────────────
                string bestSellersSql = @"
    SELECT ti.product_name, COALESCE(SUM(ti.quantity), 0) AS total_sold
    FROM public.transaction_items ti
    JOIN public.transactions t ON t.id = ti.transaction_id
    WHERE t.company_id = @companyId
        AND LOWER(t.status) = 'completed'
        AND t.transaction_date >= @from
        AND t.transaction_date <= @to
    GROUP BY ti.product_name
    ORDER BY total_sold DESC  -- This ensures highest sold comes first
    LIMIT 10";

                using (var cmd = new NpgsqlCommand(bestSellersSql, conn))
                {
                    cmd.Parameters.AddWithValue("companyId", _companyId);
                    cmd.Parameters.AddWithValue("from", from);
                    cmd.Parameters.AddWithValue("to", to);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.TopProductNames.Add(reader.GetString(0));
                            data.TopProductSales.Add(reader.IsDBNull(1) ? 0m : Convert.ToDecimal(reader[1]));
                        }
                    }
                }
            }

            return data;
        }

        // ─── Buttons ──────────────────────────────────────────────────────────

        private void btnRefresh_Click(object sender, EventArgs e) => LoadKPIs();

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

        // Revenue chart data
        public List<string> DailyLabels { get; set; } = new();
        public List<decimal> DailyRevenue { get; set; } = new();

        // Best sellers chart data
        public List<string> TopProductNames { get; set; } = new();
        public List<decimal> TopProductSales { get; set; } = new();
    }
}