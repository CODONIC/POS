using Npgsql;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ManageProdFrm : BaseForm
    {
        private readonly string _username, _companyName, _companyId;
        private readonly ProductService _productService;
        private string _selectedProductId;

        public ManageProdFrm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _productService = new ProductService(_companyId);

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            SetUserContext(_username, _companyId);

            SetupDataGridView();
            InitializeShortcuts();
            this.Load += async (s, e) => { await LoadProductsAsync(); await LoadCategoriesAsync(); };
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

        private void SetupDataGridView()
        {
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionChanged += (s, e) => LoadSelectedProduct();
        }

        private async Task LoadProductsAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;
            try
            {
                var dt = await _productService.GetProductsAsync(txtSearch.Text.Trim());
                dt.Columns["product_code"].ColumnName = "Code";
                dt.Columns["product_name"].ColumnName = "Product Name";
                dt.Columns["price"].ColumnName = "Price";
                dt.Columns["quantity"].ColumnName = "Quantity";
                dt.Columns["reorder_level"].ColumnName = "Reorder Level";
                dt.Columns["category"].ColumnName = "Category";
                dgvProducts.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show($"Failed to load products:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async Task LoadCategoriesAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;
            try
            {
                var categories = await _productService.GetCategoriesAsync();
                cmbCategory.Items.Clear();
                foreach (var cat in categories) cmbCategory.Items.Add(cat);
            }
            catch (Exception ex) { MessageBox.Show($"Error loading categories:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadSelectedProduct()
        {
            if (dgvProducts.SelectedRows.Count == 0) return;
            var row = dgvProducts.SelectedRows[0];
            _selectedProductId = row.Cells["Code"].Value?.ToString();
            txtProductCode.Text = row.Cells["Code"].Value?.ToString();
            txtProductName.Text = row.Cells["Product Name"].Value?.ToString();
            txtPrice.Text = row.Cells["Price"].Value?.ToString();
            txtReorderLevel.Text = row.Cells["Reorder Level"].Value?.ToString();

            string categoryName = row.Cells["Category"].Value?.ToString();
            foreach (var item in cmbCategory.Items)
                if (item is CategoryItem cat && cat.Name == categoryName) { cmbCategory.SelectedItem = item; break; }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var result = ProductValidator.ValidateInputs(
                txtProductCode.Text.Trim(),
                txtProductName.Text.Trim(),
                txtPrice.Text.Trim(),
                txtReorderLevel.Text.Trim(),
                cmbCategory.SelectedItem);

            if (!result.IsValid)  // ← Fix: use .IsValid property
            {
                MessageBox.Show(result.ErrorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string code = txtProductCode.Text.Trim();
            if (await _productService.ProductExistsAsync(code))
            {
                MessageBox.Show($"Product code '{code}' already exists.", "Duplicate",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductCode.FocusInner();
                return;
            }

            var category = cmbCategory.SelectedItem as CategoryItem;
            await _productService.AddProductAsync(code, txtProductName.Text.Trim(),
                decimal.Parse(txtPrice.Text), int.Parse(txtReorderLevel.Text), category.Id);

            await AuditService.LogInsertAsync(_username, _companyId, "products", code,
                AuditService.ToJson(("product_code", code), ("product_name", txtProductName.Text),
                ("price", decimal.Parse(txtPrice.Text)), ("reorder_level", int.Parse(txtReorderLevel.Text)),
                ("category", category.Name)));

            MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadProductsAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            {
                MessageBox.Show("Please select a product to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = ProductValidator.ValidateInputs(
                txtProductCode.Text.Trim(),
                txtProductName.Text.Trim(),
                txtPrice.Text.Trim(),
                txtReorderLevel.Text.Trim(),
                cmbCategory.SelectedItem);

            if (!result.IsValid)  // ← Fix: use .IsValid property
            {
                MessageBox.Show(result.ErrorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (oldName, oldPrice, oldReorder, oldCategory) = await _productService.GetProductOldValuesAsync(_selectedProductId);
            var category = cmbCategory.SelectedItem as CategoryItem;

            await _productService.UpdateProductAsync(_selectedProductId, txtProductName.Text.Trim(),
                decimal.Parse(txtPrice.Text), int.Parse(txtReorderLevel.Text), category.Id);

            await AuditService.LogUpdateAsync(_username, _companyId, "products", _selectedProductId,
                AuditService.ToJson(("product_name", oldName), ("price", oldPrice), ("reorder_level", oldReorder), ("category", oldCategory)),
                AuditService.ToJson(("product_name", txtProductName.Text), ("price", decimal.Parse(txtPrice.Text)),
                ("reorder_level", int.Parse(txtReorderLevel.Text)), ("category", category.Name)));

            MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadProductsAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            { MessageBox.Show("Please select a product to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show($"Delete '{txtProductName.Text}'? This cannot be undone.", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            await _productService.DeleteProductAsync(_selectedProductId);
            await AuditService.LogDeleteAsync(_username, _companyId, "products", _selectedProductId,
                AuditService.ToJson(("product_code", _selectedProductId), ("product_name", txtProductName.Text)));

            MessageBox.Show("Product deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadProductsAsync();
        }

        private void ClearFields()
        {
            _selectedProductId = null;
            txtProductCode.Text = txtProductName.Text = txtPrice.Text = txtReorderLevel.Text = "";
            cmbCategory.SelectedIndex = -1;
            dgvProducts.ClearSelection();
            txtProductCode.Focus();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e) => await LoadProductsAsync();
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();
        private void btnBack_Click(object sender, EventArgs e) { new AdminDashboard(_username, _companyName).Show(); Hide(); }

        private void InitializeShortcuts()
        {
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) btnBack_Click(s, e);
                else if (e.KeyCode == Keys.F1) btnAdd_Click(s, e);
                else if (e.KeyCode == Keys.F2) btnEdit_Click(s, e);
                else if (e.KeyCode == Keys.F3) btnDelete_Click(s, e);
                else if (e.KeyCode == Keys.F4) btnClear_Click(s, e);
                e.Handled = true;
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(btnBack, "ESC"); toolTip.SetToolTip(btnAdd, "F1");
            toolTip.SetToolTip(btnEdit, "F2"); toolTip.SetToolTip(btnDelete, "F3");
            toolTip.SetToolTip(btnClear, "F4");
        }
    }
}