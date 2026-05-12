using Npgsql;
using POS.Inventory_Manager;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ManageStocks : BaseForm
    {
        private readonly string _username, _companyName, _companyId;
        private readonly StockService _stockService;
        private DataTable _pendingChanges;
        private readonly string _role;
        private readonly string _userId;
        private readonly string _sessionToken;

        public ManageStocks(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _stockService = new StockService(_companyId);
            _role = GetUserRole(username);
            _userId = userId;
            _sessionToken = sessionToken;

            // Set title based on role
            if (_role == "INVENTORY MANAGER")
            {
                lblAdminName.Text = $"{_username} | Inventory Manager";
                titleLabel.Text = $"{_companyName} (Stock Management)";
            }
            else
            {
                lblAdminName.Text = $"{_username} | Admin";
                titleLabel.Text = $"{_companyName} ";
            }
            SetUserContext(_username, _userId, _sessionToken);
            SetUserContext(_username, _companyId);

            SetupDataGridViews();
            _pendingChanges = StockChangeHelper.CreatePendingTable();
            dgvPending.DataSource = _pendingChanges;

            
            this.KeyPreview = true;
            this.KeyDown += manageStocks_KeyDown;
            InitializeShortcutHints();
            this.Load += async (s, e) => await LoadProductsAsync();
        }

        private string GetUserRole(string username)
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                conn.Open();
                const string query = @"
                    SELECT r.name 
                    FROM public.users u
                    JOIN public.roles r ON u.role_id = r.id
                    WHERE LOWER(u.username) = LOWER(@username) LIMIT 1";
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                return cmd.ExecuteScalar()?.ToString()?.ToUpper();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error resolving role: {ex.Message}");
                return null;
            }
        }

        private string GetCompanyId(string companyName)
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@name", companyName);
                return cmd.ExecuteScalar()?.ToString();
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
        }

        private void SetupDataGridViews()
        {
            dgvAllProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAllProducts.MultiSelect = false;
            dgvAllProducts.ReadOnly = true;
            dgvAllProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllProducts.SelectionChanged += (s, e) => LoadSelectedProduct();

            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.ReadOnly = true;
            dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPending.AutoGenerateColumns = true;
        }

        private async Task LoadProductsAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;
            try
            {
                var dt = await _stockService.GetProductsAsync(txtSearch.Text.Trim());
                dt.Columns["product_code"].ColumnName = "Code";
                dt.Columns["product_name"].ColumnName = "Description";
                dt.Columns["category"].ColumnName = "Category";
                dt.Columns["price"].ColumnName = "Price";
                dt.Columns["quantity"].ColumnName = "Quantity";
                dt.Columns["reorder_level"].ColumnName = "Reorder Level";
                dt.Columns["stocked_in_date"].ColumnName = "Stocked In Date";
                dgvAllProducts.DataSource = dt;

                if (dgvAllProducts.Columns["Price"] != null)
                    dgvAllProducts.Columns["Price"].DefaultCellStyle.Format = "C2";
            }
            catch (Exception ex) { MessageBox.Show($"Failed to load products:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadSelectedProduct()
        {
            if (dgvAllProducts.SelectedRows.Count == 0) return;
            var row = dgvAllProducts.SelectedRows[0];
            txtProductCode.Text = row.Cells["Code"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
            txtCategory.Text = row.Cells["Category"].Value?.ToString();
            txtUnitPrice.Text = row.Cells["Price"].Value?.ToString();
            txtStockInDate.Text = row.Cells["Stocked In Date"].Value?.ToString();

            
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            // Inventory Manager can add stock
            var selectionResult = StockValidator.ValidateStockSelection(dgvAllProducts);
            if (!selectionResult.IsValid)
            {
                MessageBox.Show(selectionResult.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var quantityResult = StockValidator.ValidateQuantity(txtAdd, "add");
            if (!quantityResult.IsValid)
            {
                MessageBox.Show(quantityResult.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdd.FocusInner();
                return;
            }

            var row = dgvAllProducts.SelectedRows[0];
            StockChangeHelper.AddChange(_pendingChanges,
                row.Cells["Code"].Value?.ToString(),
                row.Cells["Description"].Value?.ToString(),
                row.Cells["Category"].Value?.ToString(),
                "ADD", int.Parse(txtAdd.Text.Trim()));

            txtAdd.Text = "";
            UpdatePendingGrid();
        }

        private void btnRemoveStock_Click(object sender, EventArgs e)
        {
            // Inventory Manager can remove stock
            var selectionResult = StockValidator.ValidateStockSelection(dgvAllProducts);
            if (!selectionResult.IsValid)
            {
                MessageBox.Show(selectionResult.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvAllProducts.SelectedRows[0];
            int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);

            var quantityResult = StockValidator.ValidateQuantity(txtRemove, "remove", currentQty);
            if (!quantityResult.IsValid)
            {
                MessageBox.Show(quantityResult.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRemove.FocusInner();
                return;
            }

            int qty = int.Parse(txtRemove.Text.Trim());

            StockChangeHelper.AddChange(_pendingChanges,
                row.Cells["Code"].Value?.ToString(),
                row.Cells["Description"].Value?.ToString(),
                row.Cells["Category"].Value?.ToString(),
                "REMOVE", qty);

            txtRemove.Text = "";
            UpdatePendingGrid();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var changesResult = StockValidator.ValidatePendingChanges(_pendingChanges);
            if (!changesResult.IsValid)
            {
                MessageBox.Show(changesResult.ErrorMessage, "Nothing to Save",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSave.Enabled = false;

                foreach (DataRow row in _pendingChanges.Rows)
                {
                    string code = row["product_code"].ToString();
                    string name = row["product_name"].ToString();
                    string type = row["change_type"].ToString();
                    int qty = Convert.ToInt32(row["quantity"]);

                    int oldQty = await _stockService.GetCurrentQuantityAsync(code);
                    int newQty = type == "ADD" ? oldQty + qty : oldQty - qty;

                    await _stockService.UpdateStockAsync(code, qty, type);

                    await AuditService.LogUpdateAsync(_username, _companyId, "products", code,
                        AuditService.ToJson(("quantity", oldQty)),
                        AuditService.ToJson(("quantity", newQty)),
                        $"Stock {type}: {qty} unit(s) for '{name}'.");
                }

                MessageBox.Show("Stock updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _pendingChanges.Clear();
                UpdatePendingGrid();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving stock changes:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!StockChangeHelper.HasChanges(_pendingChanges)) return;

            var confirmResult = MessageBox.Show(
                "Cancel all pending changes?", "Confirm Cancel",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                _pendingChanges.Clear();
                UpdatePendingGrid();
                txtAdd.Text = "";
                txtRemove.Text = "";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (StockChangeHelper.HasChanges(_pendingChanges))
            {
                var confirmResult = MessageBox.Show(
                    "You have unsaved changes. Go back?", "Unsaved Changes",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes) return;
            }

            // Return to appropriate dashboard based on role
            if (_role == "INVENTORY MANAGER")
            {
                SetNavigating(true);
                var inventoryDashboard = new InventoryManagerDashboard(_username, _companyName, _userId, _sessionToken);
                inventoryDashboard.Show();
            }
            else
            {
                SetNavigating(true);
                var adminDashboard = new AdminDashboard(_username, _companyName, _userId, _sessionToken);
                adminDashboard.Show();
            }
            Close();
        }

        private void UpdatePendingGrid()
        {
            dgvPending.Refresh();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e) => await LoadProductsAsync();

        

        private void manageStocks_KeyDown(object sender, KeyEventArgs e)
        {
            var shortcuts = new Dictionary<Keys, EventHandler>
            {
                { Keys.Escape, btnBack_Click },
                { Keys.F1, btnAddStock_Click },
                { Keys.F2, btnRemoveStock_Click },
                { Keys.F3,  btnSave_Click },
                { Keys.F4, btnCancel_Click },

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
                { btnBack, "ESC" }, { btnAddStock, "F1" }, { btnRemoveStock, "F2" },
                { btnSave, "F3" }, { btnCancel, "F4" }
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