using Npgsql;
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

        public ManageStocks(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _stockService = new StockService(_companyId);

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            SetUserContext(_username, _companyId);

            SetupDataGridViews();
            _pendingChanges = StockChangeHelper.CreatePendingTable();
            dgvPending.DataSource = _pendingChanges;

            InitializeShortcuts();
            this.Load += async (s, e) => await LoadProductsAsync();
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
            dgvPending.AutoGenerateColumns = true; // Let it auto-generate columns from the DataTable
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

                // Format the dgvAllProducts columns
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
            // Validate stock selection
            var selectionResult = StockValidator.ValidateStockSelection(dgvAllProducts);
            if (!selectionResult.IsValid)
            {
                MessageBox.Show(selectionResult.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate quantity
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
            // Validate stock selection
            var selectionResult = StockValidator.ValidateStockSelection(dgvAllProducts);
            if (!selectionResult.IsValid)
            {
                MessageBox.Show(selectionResult.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate quantity
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
            // Validate pending changes
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

            new AdminDashboard(_username, _companyName).Show();
            Close();
        }

        private void UpdatePendingGrid()
        {
            dgvPending.Refresh();
            // Optional: Add a label to show count
            // lblPendingCount.Text = $"Pending: {_pendingChanges.Rows.Count}";
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e) => await LoadProductsAsync();

        private void InitializeShortcuts()
        {
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) btnBack_Click(s, e);
                else if (e.KeyCode == Keys.F1) btnAddStock_Click(s, e);
                else if (e.KeyCode == Keys.F2) btnRemoveStock_Click(s, e);
                else if (e.KeyCode == Keys.F3) btnSave_Click(s, e);
                else if (e.KeyCode == Keys.F4) btnCancel_Click(s, e);
                e.Handled = true;
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(btnBack, "ESC");
            toolTip.SetToolTip(btnAddStock, "F1");
            toolTip.SetToolTip(btnRemoveStock, "F2");
            toolTip.SetToolTip(btnSave, "F3");
            toolTip.SetToolTip(btnCancel, "F4");
        }
    }
}