using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace POS.Admin
{
    public partial class EmployeeLogsFrm : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;

        // -- Pagination --
        private int _currentPage = 1;
        private int _pageSize = 50;
        private int _totalRecords = 0;

        // -- Row store for detail panel --
        private List<AuditLogRow> _auditRows = new List<AuditLogRow>();

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

            txtSearch.TextChanged += async (s, e) =>
            {
                _currentPage = 1;
                await LoadAuditLogsAsync();
            };
        }

        // ─────────────────────────────────────────────
        //  RESOLVE COMPANY ID FROM NAME
        // ─────────────────────────────────────────────
        private async Task<bool> ResolveCompanyIdAsync()
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = "SELECT id FROM public.companies WHERE name = @name LIMIT 1";
                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", _companyName);

                var result = await cmd.ExecuteScalarAsync();
                if (result == null)
                {
                    MessageBox.Show($"Company '{_companyName}' not found in the database.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _companyId = result.ToString();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resolving company: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ─────────────────────────────────────────────
        //  GRID SETUP
        // ─────────────────────────────────────────────
        private void InitializeGrid()
        {
            dgvAuditLogs.ReadOnly = true;
            dgvAuditLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLogs.MultiSelect = false;
            dgvAuditLogs.AllowUserToAddRows = false;
            dgvAuditLogs.RowHeadersVisible = false;
            dgvAuditLogs.AutoGenerateColumns = false; // ← prevents DB column names from overriding
            dgvAuditLogs.BackgroundColor = Color.White;
            dgvAuditLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);
            dgvAuditLogs.CellClick += dgvAuditLogs_CellClick;

            dgvAuditLogs.Columns.Clear();
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTimestamp", HeaderText = "Timestamp", FillWeight = 15 });
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUsername", HeaderText = "Employee", FillWeight = 12 });
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCategory", HeaderText = "Category", FillWeight = 10 });
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAction", HeaderText = "Action", FillWeight = 10 });
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTable", HeaderText = "Table Affected", FillWeight = 12 });
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRecordId", HeaderText = "Record ID", FillWeight = 13 });
            //dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colIpAddress", HeaderText = "IP Address", FillWeight = 10 });
            dgvAuditLogs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRemarks", HeaderText = "Remarks", FillWeight = 18 });
        }

        private void InitializeFilters()
        {
            // Set maximum date to today (disable future dates)
            dtpFrom.MaxDate = DateTime.Today;
            dtpTo.MaxDate = DateTime.Today;

            // Set minimum dates to a reasonable past date (optional)
            dtpFrom.MinDate = new DateTime(2000, 1, 1);
            dtpTo.MinDate = new DateTime(2000, 1, 1);

            // Set both dates to today (not tomorrow)
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;  // Changed from DateTime.Today.AddDays(1).AddSeconds(-1)

            // Add event handlers to validate date ranges
            dtpFrom.ValueChanged += DtpFrom_ValueChanged;
            dtpTo.ValueChanged += DtpTo_ValueChanged;

            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(new[] { "All", "AUTH", "DATA_CHANGE" });
            cmbCategory.SelectedIndex = 0;

            cmbAction.Items.Clear();
            cmbAction.Items.AddRange(new[] { "All", "LOGIN", "LOGOUT", "INSERT", "UPDATE", "DELETE" });
            cmbAction.SelectedIndex = 0;
        }

        private void DtpFrom_ValueChanged(object sender, EventArgs e)
        {
            // Ensure 'From' date is not after 'To' date
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                dtpTo.Value = dtpFrom.Value;
            }
        }

        private void DtpTo_ValueChanged(object sender, EventArgs e)
        {
            // Ensure 'To' date is not before 'From' date
            if (dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                dtpFrom.Value = dtpTo.Value;
            }
        }

        // ─────────────────────────────────────────────
        //  LOAD ON SHOW
        // ─────────────────────────────────────────────
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bool resolved = await ResolveCompanyIdAsync();
            if (resolved)
            {
                // Set user context for audit logging after company ID is resolved
                SetUserContext(_username, _companyId);
                await LoadAuditLogsAsync();
            }
        }

        // ─────────────────────────────────────────────
        //  CORE DATA LOAD
        // ─────────────────────────────────────────────
        private async Task LoadAuditLogsAsync()
        {
            try
            {
                SetLoading(true);
                ClearDetail();

                string categoryFilter = cmbCategory.SelectedItem?.ToString();
                string actionFilter = cmbAction.SelectedItem?.ToString();
                string searchText = txtSearch.Text.Trim();
                DateTime fromDate = dtpFrom.Value.Date;
                DateTime toDate = dtpTo.Value.Date.AddDays(1).AddSeconds(-1);

                int offset = (_currentPage - 1) * _pageSize;

                // Build dynamic WHERE clauses
                var conditions = new List<string>
                {
                    "a.company_id = @companyId",
                    "a.created_at BETWEEN @fromDate AND @toDate"
                };

                if (categoryFilter != "All")
                    conditions.Add("a.action_category = @category");

                if (actionFilter != "All")
                    conditions.Add("a.action_type = @action");

                if (!string.IsNullOrEmpty(searchText))
                    conditions.Add("(a.username ILIKE @search OR a.table_name ILIKE @search OR a.record_id ILIKE @search OR a.remarks ILIKE @search)");

                string where = string.Join(" AND ", conditions);

                string countSql = $"SELECT COUNT(*) FROM public.audit_logs a WHERE {where}";

                string dataSql = $@"
                    SELECT
                        a.id,
                        a.created_at,
                        a.username,
                        a.action_category,
                        a.action_type,
                        a.table_name,
                        a.record_id,
                        a.old_values,
                        a.new_values,
                        a.ip_address,
                        a.session_id,
                        a.device_info,
                        a.remarks
                    FROM public.audit_logs a
                    WHERE {where}
                    ORDER BY a.created_at DESC
                    LIMIT @limit OFFSET @offset";

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Get total count for pagination
                await using (var countCmd = new NpgsqlCommand(countSql, conn))
                {
                    AddParameters(countCmd, categoryFilter, actionFilter, searchText, fromDate, toDate);
                    _totalRecords = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                // Fetch page rows
                var rows = new List<AuditLogRow>();
                await using (var cmd = new NpgsqlCommand(dataSql, conn))
                {
                    AddParameters(cmd, categoryFilter, actionFilter, searchText, fromDate, toDate);
                    cmd.Parameters.AddWithValue("@limit", _pageSize);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    await using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        rows.Add(new AuditLogRow
                        {
                            Id = reader["id"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            Username = reader["username"]?.ToString(),
                            Category = reader["action_category"]?.ToString(),
                            Action = reader["action_type"]?.ToString(),
                            TableName = reader["table_name"]?.ToString(),
                            RecordId = reader["record_id"]?.ToString(),
                            OldValues = reader["old_values"]?.ToString(),
                            NewValues = reader["new_values"]?.ToString(),
                            IpAddress = reader["ip_address"]?.ToString(),
                            Remarks = reader["remarks"]?.ToString(),
                        });
                    }
                }

                // Populate grid manually — avoids DataSource overriding column headers
                _auditRows = rows;
                dgvAuditLogs.Rows.Clear();

                foreach (var log in rows)
                {
                    int i = dgvAuditLogs.Rows.Add();
                    var row = dgvAuditLogs.Rows[i];
                    row.Cells["colTimestamp"].Value = log.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                    row.Cells["colUsername"].Value = log.Username ?? "—";
                    row.Cells["colCategory"].Value = log.Category ?? "—";
                    row.Cells["colAction"].Value = log.Action ?? "—";
                    row.Cells["colTable"].Value = log.TableName ?? "—";
                    row.Cells["colRecordId"].Value = log.RecordId ?? "—";
                    // row.Cells["colIpAddress"].Value = log.IpAddress ?? "—";
                    row.Cells["colRemarks"].Value = log.Remarks ?? "—";
                }

                FormatGrid();
                UpdatePaginationLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading audit logs: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void AddParameters(NpgsqlCommand cmd, string categoryFilter,
                                   string actionFilter, string searchText,
                                   DateTime fromDate, DateTime toDate)
        {
            cmd.Parameters.AddWithValue("@companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);

            if (categoryFilter != "All")
                cmd.Parameters.AddWithValue("@category", categoryFilter);

            if (actionFilter != "All")
                cmd.Parameters.AddWithValue("@action", actionFilter);

            if (!string.IsNullOrEmpty(searchText))
                cmd.Parameters.AddWithValue("@search", $"%{searchText}%");
        }

        // ─────────────────────────────────────────────
        //  GRID FORMATTING
        // ─────────────────────────────────────────────
        private void FormatGrid()
        {
            foreach (DataGridViewRow row in dgvAuditLogs.Rows)
            {
                string action = row.Cells["colAction"].Value?.ToString();

                Color rowColor = action switch
                {
                    "LOGIN" => Color.FromArgb(220, 255, 220),  // light green
                    "LOGOUT" => Color.FromArgb(255, 245, 200),  // light yellow
                    "INSERT" => Color.FromArgb(210, 235, 255),  // light blue
                    "UPDATE" => Color.FromArgb(255, 228, 196),  // light orange
                    "DELETE" => Color.FromArgb(255, 210, 210),  // light red
                    _ => Color.White
                };

                row.DefaultCellStyle.BackColor = rowColor;
            }
        }

        // ─────────────────────────────────────────────
        //  DETAIL PANEL — shows old/new values on click
        // ─────────────────────────────────────────────
        private void dgvAuditLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _auditRows.Count) return;

            var log = _auditRows[e.RowIndex];

            rtbOldValues.Text = string.IsNullOrEmpty(log.OldValues) ? "(none)" : FormatJson(log.OldValues);
            rtbNewValues.Text = string.IsNullOrEmpty(log.NewValues) ? "(none)" : FormatJson(log.NewValues);
            lblDetailAction.Text = $"Action: {log.Action ?? "—"}";
            lblDetailTable.Text = $"Table: {log.TableName ?? "—"}";
            lblDetailEmployee.Text = $"Employee: {log.Username ?? "—"}";
            lblDetailTime.Text = $"Time: {log.CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }

        private void ClearDetail()
        {
            rtbOldValues.Text = string.Empty;
            rtbNewValues.Text = string.Empty;
            lblDetailAction.Text = "Action: —";
            lblDetailTable.Text = "Table: —";
            lblDetailEmployee.Text = "Employee: —";
            lblDetailTime.Text = "Time: —";
        }

        private string FormatJson(string json)
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                int indent = 0;
                bool inString = false;

                foreach (char c in json)
                {
                    if (c == '"') inString = !inString;

                    if (!inString)
                    {
                        if (c == '{' || c == '[')
                        {
                            sb.Append(c);
                            sb.AppendLine();
                            sb.Append(new string(' ', ++indent * 2));
                            continue;
                        }
                        if (c == '}' || c == ']')
                        {
                            sb.AppendLine();
                            sb.Append(new string(' ', --indent * 2));
                            sb.Append(c);
                            continue;
                        }
                        if (c == ',')
                        {
                            sb.Append(c);
                            sb.AppendLine();
                            sb.Append(new string(' ', indent * 2));
                            continue;
                        }
                        if (c == ':') { sb.Append(": "); continue; }
                    }
                    sb.Append(c);
                }
                return sb.ToString();
            }
            catch
            {
                return json;
            }
        }

        // ─────────────────────────────────────────────
        //  PAGINATION
        // ─────────────────────────────────────────────
        private void UpdatePaginationLabel()
        {
            int totalPages = (int)Math.Ceiling(_totalRecords / (double)_pageSize);
            lblPagination.Text = $"Page {_currentPage} of {Math.Max(totalPages, 1)}  ({_totalRecords} records)";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1) { _currentPage--; await LoadAuditLogsAsync(); }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling(_totalRecords / (double)_pageSize);
            if (_currentPage < totalPages) { _currentPage++; await LoadAuditLogsAsync(); }
        }

        // ─────────────────────────────────────────────
        //  FILTER / SEARCH EVENTS
        // ─────────────────────────────────────────────
        private async void btnApplyFilter_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadAuditLogsAsync();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;  // Changed from DateTime.Today.AddDays(1).AddSeconds(-1)
            cmbCategory.SelectedIndex = 0;
            cmbAction.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            _currentPage = 1;
            _ = LoadAuditLogsAsync();
        }

        // ─────────────────────────────────────────────
        //  EXPORT TO CSV
        // ─────────────────────────────────────────────
        private void btnExport_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"AuditLog_{_companyName}_{DateTime.Today:yyyyMMdd}.csv"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                if (_auditRows == null || _auditRows.Count == 0)
                {
                    MessageBox.Show("No records to export.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var sb = new System.Text.StringBuilder();

                sb.AppendLine("\"Timestamp\",\"Employee\",\"Category\",\"Action\",\"Table Affected\",\"Record ID\",\"Remarks\",\"Old Values\",\"New Values\"");

                foreach (var log in _auditRows)
                {
                    string Esc(string s) => $"\"{s?.Replace("\"", "\"\"") ?? ""}\"";
                    sb.AppendLine(string.Join(",",
                        Esc(log.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")),
                        Esc(log.Username),
                        Esc(log.Category),
                        Esc(log.Action),
                        Esc(log.TableName),
                        Esc(log.RecordId),
                        Esc(log.Remarks),
                        Esc(log.OldValues),
                        Esc(log.NewValues)
                    ));
                }

                System.IO.File.WriteAllText(dialog.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                MessageBox.Show("Exported successfully!", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─────────────────────────────────────────────
        //  LOADING STATE
        // ─────────────────────────────────────────────
        private void SetLoading(bool loading)
        {
            btnApplyFilter.Enabled = !loading;
            btnResetFilter.Enabled = !loading;
            btnExport.Enabled = !loading;
            btnPrev.Enabled = !loading;
            btnNext.Enabled = !loading;
            Cursor = loading ? Cursors.WaitCursor : Cursors.Default;
        }

        // ─────────────────────────────────────────────
        //  BACK BUTTON
        // ─────────────────────────────────────────────
        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard(_username, _companyName);
            admin.Show();
            this.Hide();
        }

        // ─────────────────────────────────────────────
        //  AUDIT ROW MODEL
        // ─────────────────────────────────────────────
        private class AuditLogRow
        {
            public string Id { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Username { get; set; }
            public string Category { get; set; }
            public string Action { get; set; }
            public string TableName { get; set; }
            public string RecordId { get; set; }
            public string OldValues { get; set; }
            public string NewValues { get; set; }
            public string IpAddress { get; set; }
            public string Remarks { get; set; }
        }
    }
}