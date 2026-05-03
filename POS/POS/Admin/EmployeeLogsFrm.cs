using Google.Protobuf;
using Npgsql;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class EmployeeLogsFrm : BaseForm
    {
        private readonly string _username, _companyName;
        private string _companyId;
        private AuditLogService _auditService;
        private List<AuditLogRow> _auditRows = new();
        private int _currentPage = 1, _pageSize = 50, _totalRecords = 0;

        public EmployeeLogsFrm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";
            InitializeGrid();
            InitializeFilters();
            txtSearch.TextChanged += async (s, e) => { _currentPage = 1; await LoadAuditLogsAsync(); };
        }

        private async Task<bool> ResolveCompanyIdAsync()
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();
                using var cmd = new NpgsqlCommand("SELECT id FROM public.companies WHERE name = @name LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@name", _companyName);
                var result = await cmd.ExecuteScalarAsync();
                if (result == null) throw new Exception($"Company '{_companyName}' not found.");
                _companyId = result.ToString();
                _auditService = new AuditLogService(_companyId);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return false; }
        }

        private void InitializeGrid()
        {
            dgvAuditLogs.ReadOnly = true;
            dgvAuditLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLogs.AutoGenerateColumns = false;
            dgvAuditLogs.CellClick += dgvAuditLogs_CellClick;
            dgvAuditLogs.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "colTimestamp", HeaderText = "Timestamp", FillWeight = 15 },
                new DataGridViewTextBoxColumn { Name = "colUsername", HeaderText = "Employee", FillWeight = 12 },
                new DataGridViewTextBoxColumn { Name = "colCategory", HeaderText = "Category", FillWeight = 10 },
                new DataGridViewTextBoxColumn { Name = "colAction", HeaderText = "Action", FillWeight = 10 },
                new DataGridViewTextBoxColumn { Name = "colTable", HeaderText = "Table Affected", FillWeight = 12 },
                new DataGridViewTextBoxColumn { Name = "colRecordId", HeaderText = "Record ID", FillWeight = 13 },
                new DataGridViewTextBoxColumn { Name = "colRemarks", HeaderText = "Remarks", FillWeight = 18 }
            });
        }

        private void InitializeFilters()
        {
            dtpFrom.MaxDate = dtpTo.MaxDate = DateTime.Today;
            dtpFrom.MinDate = dtpTo.MinDate = new DateTime(2000, 1, 1);
            dtpFrom.Value = dtpTo.Value = DateTime.Today;
            dtpFrom.ValueChanged += Dtp_ValueChanged;
            dtpTo.ValueChanged += Dtp_ValueChanged;
            cmbCategory.Items.AddRange(new[] { "All", "AUTH", "DATA_CHANGE" });
            cmbAction.Items.AddRange(new[] { "All", "LOGIN", "LOGOUT", "INSERT", "UPDATE", "DELETE" });
            cmbCategory.SelectedIndex = cmbAction.SelectedIndex = 0;
        }

        private void Dtp_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date) dtpTo.Value = dtpFrom.Value;
            else if (dtpTo.Value.Date < dtpFrom.Value.Date) dtpFrom.Value = dtpTo.Value;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (await ResolveCompanyIdAsync())
            {
                SetUserContext(_username, _companyId);
                await LoadAuditLogsAsync();
            }
        }

        private async Task LoadAuditLogsAsync()
        {
            try
            {
                SetLoading(true);
                ClearDetail();
                int offset = (_currentPage - 1) * _pageSize;
                var (total, rows) = await _auditService.GetLogsAsync(
                    dtpFrom.Value.Date, dtpTo.Value.Date.AddDays(1).AddSeconds(-1),
                    cmbCategory.SelectedItem?.ToString(), cmbAction.SelectedItem?.ToString(),
                    txtSearch.Text.Trim(), _pageSize, offset);

                _totalRecords = total;
                _auditRows = rows;
                dgvAuditLogs.Rows.Clear();

                foreach (var log in rows)
                {
                    int i = dgvAuditLogs.Rows.Add();
                    var row = dgvAuditLogs.Rows[i];
                    row.Cells["colTimestamp"].Value = log.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss tt");
                    row.Cells["colUsername"].Value = log.Username ?? "—";
                    row.Cells["colCategory"].Value = log.Category ?? "—";
                    row.Cells["colAction"].Value = log.Action ?? "—";
                    row.Cells["colTable"].Value = log.TableName ?? "—";
                    row.Cells["colRecordId"].Value = log.RecordId ?? "—";
                    row.Cells["colRemarks"].Value = log.Remarks ?? "—";
                }

                FormatGrid();
                UpdatePagination();
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { SetLoading(false); }
        }

        private void FormatGrid()
        {
            foreach (DataGridViewRow row in dgvAuditLogs.Rows)
            {
                row.DefaultCellStyle.BackColor = row.Cells["colAction"].Value?.ToString() switch
                {
                    "LOGIN" => Color.FromArgb(220, 255, 220),
                    "LOGOUT" => Color.FromArgb(255, 245, 200),
                    "INSERT" => Color.FromArgb(210, 235, 255),
                    "UPDATE" => Color.FromArgb(255, 228, 196),
                    "DELETE" => Color.FromArgb(255, 210, 210),
                    _ => Color.White
                };
            }
        }

        private void dgvAuditLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _auditRows.Count) return;
            var log = _auditRows[e.RowIndex];
            rtbOldValues.Text = JsonFormatter.Format(log.OldValues);
            rtbNewValues.Text = JsonFormatter.Format(log.NewValues);
            lblDetailAction.Text = $"Action: {log.Action ?? "—"}";
            lblDetailTable.Text = $"Table: {log.TableName ?? "—"}";
            lblDetailEmployee.Text = $"Employee: {log.Username ?? "—"}";
            lblDetailTime.Text = $"Time: {log.CreatedAt.ToLocalTime():yyyy-MM-dd hh:mm:ss tt}";
        }

        private void ClearDetail() => rtbOldValues.Text = rtbNewValues.Text = string.Empty;

        private void UpdatePagination()
        {
            int totalPages = (int)Math.Ceiling(_totalRecords / (double)_pageSize);
            lblPagination.Text = $"Page {_currentPage} of {Math.Max(totalPages, 1)} ({_totalRecords} records)";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private async void btnPrev_Click(object sender, EventArgs e) { if (_currentPage > 1) { _currentPage--; await LoadAuditLogsAsync(); } }
        private async void btnNext_Click(object sender, EventArgs e) { int total = (int)Math.Ceiling(_totalRecords / (double)_pageSize); if (_currentPage < total) { _currentPage++; await LoadAuditLogsAsync(); } }
        private async void btnApplyFilter_Click(object sender, EventArgs e) { _currentPage = 1; await LoadAuditLogsAsync(); }
        private void btnResetFilter_Click(object sender, EventArgs e) { dtpFrom.Value = dtpTo.Value = DateTime.Today; cmbCategory.SelectedIndex = cmbAction.SelectedIndex = 0; txtSearch.Text = ""; _currentPage = 1; _ = LoadAuditLogsAsync(); }
        private void btnExport_Click(object sender, EventArgs e) => AuditLogExporter.ExportToCsv(_auditRows, _companyName);
        private void SetLoading(bool loading) { btnApplyFilter.Enabled = btnResetFilter.Enabled = btnExport.Enabled = btnPrev.Enabled = btnNext.Enabled = !loading; Cursor = loading ? Cursors.WaitCursor : Cursors.Default; }
        private void btnBack_Click(object sender, EventArgs e) { new AdminDashboard(_username, _companyName).Show(); Hide(); }
    }
}