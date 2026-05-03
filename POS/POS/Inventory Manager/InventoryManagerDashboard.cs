using Npgsql;
using POS.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Inventory_Manager
{
    public partial class InventoryManagerDashboard : BaseForm
    {
        private readonly string _username;
        private readonly string _companyName;
        private readonly string _companyId;
        private DataTable _inventoryStatusTable;

        public InventoryManagerDashboard(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);

            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(_companyName);

            SetUserContext(_username, _companyId);

            lblInventoryName.Text = $"{_username} | Inventory Manager";
            titleLabel.Text = $"{_companyName} ";

            SetupInventoryStatusGrid();
            this.Load += async (s, e) => await LoadInventoryStatusAsync();

            this.KeyPreview = true;
            this.KeyDown += Dashboard_KeyDown;
            InitializeShortcutHints();
        }

        // ── Setup Inventory Status Grid ────────────────────────────────────────

        private void SetupInventoryStatusGrid()
        {
            dgvInventStatus.AutoGenerateColumns = false;
            dgvInventStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventStatus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventStatus.ReadOnly = true;
            dgvInventStatus.RowHeadersVisible = false;
            dgvInventStatus.AllowUserToAddRows = false;
            dgvInventStatus.BackgroundColor = Color.White;
            dgvInventStatus.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // Add columns
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "product_code", HeaderText = "Product Code", FillWeight = 12 });
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "product_name", HeaderText = "Product Name", FillWeight = 25 });
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "current_stock", HeaderText = "Current Stock", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "reorder_level", HeaderText = "Reorder Level", FillWeight = 10, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "status", HeaderText = "Stock Status", FillWeight = 12 });
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "last_updated", HeaderText = "Last Stock Update", FillWeight = 15 });
            dgvInventStatus.Columns.Add(new DataGridViewTextBoxColumn { Name = "category", HeaderText = "Category", FillWeight = 10 });

            // Color the status column
            dgvInventStatus.CellFormatting += DgvInventStatus_CellFormatting;
        }

        private void DgvInventStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvInventStatus.Columns[e.ColumnIndex].Name != "status" || e.Value == null)
                return;

            string status = e.Value.ToString();
            e.CellStyle.Font = new Font(dgvInventStatus.Font, FontStyle.Bold);

            switch (status)
            {
                case "Critical - Low Stock":
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    break;
                case "Warning - Approaching Reorder":
                    e.CellStyle.ForeColor = Color.Orange;
                    e.CellStyle.BackColor = Color.FromArgb(255, 245, 200);
                    break;
                case "Normal - Adequate Stock":
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.BackColor = Color.FromArgb(220, 255, 220);
                    break;
                case "Out of Stock":
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    e.CellStyle.Font = new Font(dgvInventStatus.Font, FontStyle.Bold | FontStyle.Italic);
                    break;
            }
        }

        // ── Load Inventory Status ─────────────────────────────────────────────

        private async Task LoadInventoryStatusAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                SetLoadingState(true);

                var dt = await Task.Run(() => FetchInventoryStatus());
                _inventoryStatusTable = dt;

                dgvInventStatus.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dgvInventStatus.Rows.Add();
                    var gridRow = dgvInventStatus.Rows[rowIndex];

                    gridRow.Cells["product_code"].Value = row["product_code"];
                    gridRow.Cells["product_name"].Value = row["product_name"];
                    gridRow.Cells["current_stock"].Value = row["current_stock"];
                    gridRow.Cells["reorder_level"].Value = row["reorder_level"];
                    gridRow.Cells["status"].Value = row["status"];
                    gridRow.Cells["last_updated"].Value = row["last_stock_update"];
                    gridRow.Cells["category"].Value = row["category"];
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory status:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private DataTable FetchInventoryStatus()
        {
            var dt = new DataTable();

            using var conn = DatabaseService.GetConnection();
            conn.Open();

            string sql = @"
                SELECT 
                    p.product_code,
                    p.product_name,
                    p.quantity AS current_stock,
                    COALESCE(p.reorder_level, 0) AS reorder_level,
                    CASE 
                        WHEN p.quantity <= 0 THEN 'Out of Stock'
                        WHEN p.quantity <= p.reorder_level THEN 'Critical - Low Stock'
                        WHEN p.quantity <= p.reorder_level * 2 THEN 'Warning - Approaching Reorder'
                        ELSE 'Normal - Adequate Stock'
                    END AS status,
                    p.stocked_in_date AS last_stock_update,
                    c.name AS category,
                    p.price
                FROM public.products p
                LEFT JOIN public.categories c ON p.category_id = c.id
                WHERE p.company_id = @companyId
                ORDER BY 
                    CASE 
                        WHEN p.quantity <= 0 THEN 1
                        WHEN p.quantity <= p.reorder_level THEN 2
                        WHEN p.quantity <= p.reorder_level * 2 THEN 3
                        ELSE 4
                    END,
                    p.product_name";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }

        

        private void SetLoadingState(bool loading)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetLoadingState), loading);
                return;
            }

            if (loading)
            {
                Cursor = Cursors.WaitCursor;
                dgvInventStatus.Enabled = false;
                btnRefresh.Enabled = false;
            }
            else
            {
                Cursor = Cursors.Default;
                dgvInventStatus.Enabled = true;
                btnRefresh.Enabled = true;
            }
        }

        

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadInventoryStatusAsync();
        }

        // ── Navigation ────────────────────────────────────────────────────────

        private void ShowSubScreen(Form form)
        {
            form.Show();
            this.Hide();
        }

        private void btnManageProducts_Click(object sender, EventArgs e) =>
            ShowSubScreen(new ManageProdFrm(_username, _companyName));

        private void btnManageStocks_Click(object sender, EventArgs e) =>
            ShowSubScreen(new ManageStocks(_username, _companyName));

        // ── Logout ────────────────────────────────────────────────────────────

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirm Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            await LogLogoutAsync();

            new LogInForm().Show();
            this.Hide();
        }

        private async Task LogLogoutAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(_companyId))
                    await AuditService.LogLogoutAsync(_username, _companyId, Environment.MachineName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Logout audit failed: {ex.Message}");
            }
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private string GetCompanyId(string companyName)
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                conn.Open();

                const string query = "SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", companyName);

                return cmd.ExecuteScalar()?.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error resolving company: {ex.Message}");
                return null;
            }
        }

        // ── Keyboard Shortcuts ────────────────────────────────────────────────

        private void Dashboard_KeyDown(object sender, KeyEventArgs e)
        {
            var shortcuts = new Dictionary<Keys, EventHandler>
            {
                { Keys.Escape, btnLogOut_Click },
                { Keys.F1,     btnManageProducts_Click },
                { Keys.F2,     btnManageStocks_Click },
                { Keys.F5,     (s, ev) => btnRefresh_Click(s, ev) }
               
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
                { btnLogOut,        "ESC — Logout" },
                { btnManageProducts,"F1 — Manage Products" },
                { btnManageStocks,  "F2 — Manage Stocks" },
                { btnRefresh,       "F5 — Refresh Status" }
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };

            foreach (var (button, hint) in shortcuts)
            {
                toolTip.SetToolTip(button, hint);
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