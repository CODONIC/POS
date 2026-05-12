using Npgsql;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class BusinessStatsForm : BaseForm
    {
        private readonly string _username, _companyName;
        private readonly Guid _companyId;
        private readonly BusinessStatsService _statsService;
        private KpiData _lastKpi;
        private bool _isLineChart = false, _isBestSellersView = false;
        private readonly string _userId;
        private readonly string _sessionToken;

        public BusinessStatsForm(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            cartesianChart1.AnimationsSpeed = TimeSpan.Zero;
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = FetchCompanyId(companyName);
            _statsService = new BusinessStatsService(DatabaseService.ConnectionString, _companyId);
            _userId = userId;
            _sessionToken = sessionToken;
            SetUserContext(_username, _userId, _sessionToken);
            SetUserContext(_username, _companyId.ToString());
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";
            SetupDateDefaults();
            dtpFrom.ValueChanged += (s, e) => LoadKPIs();
            dtpTo.ValueChanged += (s, e) => LoadKPIs();
            cmbQuickFilter.SelectedIndexChanged += CmbQuickFilter_SelectedIndexChanged;
            LoadKPIs();

            this.KeyPreview = true;
            this.KeyDown += businessForm_KeyDown;
            InitializeShortcutHints();
        }

        private Guid FetchCompanyId(string companyName)
        {
            using var conn = new NpgsqlConnection(DatabaseService.ConnectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT id FROM public.companies WHERE name = @name", conn);
            cmd.Parameters.AddWithValue("name", companyName);
            var result = cmd.ExecuteScalar();
            if (result == null)
                throw new Exception($"Company '{companyName}' not found.");
            return (Guid)result;
        }

        private void SetupDateDefaults()
        {
            dtpFrom.MaxDate = dtpTo.MaxDate = DateTime.Today;
            dtpFrom.MinDate = dtpTo.MinDate = new DateTime(2000, 1, 1);
            cmbQuickFilter.Items.AddRange(new[] { "Today", "This Week", "This Month", "This Year", "All Time", "Custom" });
            dtpFrom.Enabled = dtpTo.Enabled = false;
            dtpFrom.Value = new DateTime(2000, 1, 1);
            dtpTo.Value = DateTime.Today;
            cmbQuickFilter.SelectedIndex = 4;
        }

        private void CmbQuickFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = cmbQuickFilter.SelectedItem?.ToString();
            bool isCustom = filter == "Custom";
            dtpFrom.Enabled = dtpTo.Enabled = isCustom;

            if (!isCustom)
            {
                var (from, to) = GetFilterDates(filter, DateTime.Today);
                dtpFrom.ValueChanged -= Dtp_ValueChanged;
                dtpTo.ValueChanged -= Dtp_ValueChanged;
                dtpFrom.Value = from;
                dtpTo.Value = to;
                dtpFrom.ValueChanged += Dtp_ValueChanged;
                dtpTo.ValueChanged += Dtp_ValueChanged;
                LoadKPIs();
            }
        }

        private (DateTime from, DateTime to) GetFilterDates(string filter, DateTime now)
        {
            switch (filter)
            {
                case "Today": return (now, now);
                case "This Week": return (now.AddDays(-(int)now.DayOfWeek), now);
                case "This Month": return (new DateTime(now.Year, now.Month, 1), now);
                case "This Year": return (new DateTime(now.Year, 1, 1), now);
                case "All Time": return (new DateTime(2000, 1, 1), now);
                default: return (now, now);
            }
        }

        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
                dtpTo.Value = dtpFrom.Value;
            else if (dtpTo.Value.Date < dtpFrom.Value.Date)
                dtpFrom.Value = dtpTo.Value;
        }

        private async void LoadKPIs()
        {
            try
            {
                SetLoadingState(true);
                var kpi = await Task.Run(() => _statsService.FetchKPIs(dtpFrom.Value.Date, dtpTo.Value.Date.AddDays(1).AddTicks(-1)));
                _lastKpi = kpi;
                UpdateKpiLabels(kpi);

                if (_isBestSellersView)
                    ChartHelper.LoadBestSellersChart(cartesianChart1, kpi);
                else
                    ChartHelper.LoadRevenueChart(cartesianChart1, kpi, _isLineChart);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void UpdateKpiLabels(KpiData kpi)
        {
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

        private void SetLoadingState(bool loading)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetLoadingState), loading);
                return;
            }
            lblLoadingIndicator.Visible = loading;
            btnRefresh.Enabled = !loading;
        }

        private void btnToggleChart_Click(object sender, EventArgs e)
        {
            _isLineChart = !_isLineChart;
            btnToggleChart.Text = _isLineChart ? "📊 Bar" : "📈 Line";
            if (_lastKpi != null)
                ChartHelper.LoadRevenueChart(cartesianChart1, _lastKpi, _isLineChart);
        }

        private void btnBestSellers_Click(object sender, EventArgs e)
        {
            _isBestSellersView = !_isBestSellersView;
            btnBestSellers.Text = _isBestSellersView ? "📈 Revenue" : "🏆 Best Sellers";
            btnToggleChart.Enabled = !_isBestSellersView;

            if (_lastKpi != null)
            {
                if (_isBestSellersView)
                    ChartHelper.LoadBestSellersChart(cartesianChart1, _lastKpi);
                else
                    ChartHelper.LoadRevenueChart(cartesianChart1, _lastKpi, _isLineChart);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadKPIs();

        private void btnBack_Click(object sender, EventArgs e)
        {
            SetNavigating(true);
            new AdminDashboard(_username, _companyName, _userId, _sessionToken).Show();
            Hide();
        }


        private void businessForm_KeyDown(object sender, KeyEventArgs e)
        {
            var shortcuts = new Dictionary<Keys, EventHandler>
            {
                { Keys.Escape, btnBack_Click },
                { Keys.F1, btnBestSellers_Click },
                { Keys.F2, btnToggleChart_Click },
                { Keys.F3, btnRefresh_Click },

            };

            if (shortcuts.TryGetValue(e.KeyCode, out var handler))
            {
                handler?.Invoke(sender, e);
                e.Handled = true;
            }
        }

        private void InitializeShortcutHints()
        {
            var shortcuts = new Dictionary<Button, string>
            {
                { btnBack, "ESC" },
                { btnBestSellers, "F1" },
                { btnToggleChart, "F2" },
                { btnRefresh, "F3" }
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };

            foreach (var (button, shortcut) in shortcuts)
            {
                toolTip.SetToolTip(button, shortcut);
                AttachHoverEffect(button);
            }
        }

        private void AttachHoverEffect(Button btn)
        {
            var originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
                btn.Padding = new Padding(0, 0, 0, 6);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Location = originalLocation;
                btn.Padding = new Padding(0);
            };
        }
    }
}