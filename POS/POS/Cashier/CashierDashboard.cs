using Npgsql;
using POS.Cashier;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class CashierDashboard : BaseForm
    {
        private readonly string _username, _companyName, _companyId;
        private readonly CashierProductService _productService;
        private readonly CartManager _cartManager;
        private readonly TransactionCalculator _calculator;

        private DataTable _productsTable;
        private bool _isCartView = false;
        private bool _suppressSelectionChanged = false;

        public CashierDashboard(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);

            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _productService = new CashierProductService(_companyId);
            _cartManager = new CartManager();
            _calculator = new TransactionCalculator();

            lblCashierName.Text = $"{_username} | Cashier";
            titleLabel.Text = $"{_companyName}";
            SetUserContext(_username, _companyId);

            SetupDataGridView();
            WireUpEvents();
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

        private void SetupDataGridView()
        {
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            dgvProducts.AllowUserToAddRows = false;
        }

        private void WireUpEvents()
        {
            txtProductCode.TextChanged += (s, e) => SearchTable(txtProductCode.Text.Trim(), "product_code");
            txtProductName.TextChanged += (s, e) => SearchTable(txtProductName.Text.Trim(), "product_name");
            txtQuan.TextChanged += (s, e) => SearchTable(txtQuan.Text.Trim(), "quantity");
            txtPrice.TextChanged += (s, e) => SearchTable(txtPrice.Text.Trim(), "price");
        }

        private async Task LoadProductsAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;
            try
            {
                _productsTable = await _productService.LoadProductsAsync();
                ShowProductsView();
            }
            catch (Exception ex) { MessageBox.Show($"Failed to load products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void UpdateFinancialDisplay()
        {
            lblVATable.Text = $"₱ {_calculator.VatableAmount:N2}";
            lblVAT.Text = $"₱ {_calculator.VatAmount:N2}";
            lblTotalPrice.Text = _calculator.TotalAmount.ToString("F2");
        }

        private void ShowProductsView()
        {
            _suppressSelectionChanged = true;
            _isCartView = false;
            dgvProducts.DataSource = _productsTable;
            RenameProductColumns();
            _suppressSelectionChanged = false;
            lblProducts.Text = "Products";
            btnCart.Text = "CART TABLE >";
            dgvProducts.ClearSelection();
        }

        private void ShowCartView()
        {
            _suppressSelectionChanged = true;
            _isCartView = true;
            dgvProducts.DataSource = _cartManager.CartTable;
            RenameCartColumns();
            _suppressSelectionChanged = false;
            lblProducts.Text = "Cart";
            btnCart.Text = "< PRODUCTS TABLE";
            dgvProducts.ClearSelection();
        }

        private void RenameProductColumns()
        {
            if (dgvProducts.Columns.Contains("product_code")) dgvProducts.Columns["product_code"].HeaderText = "Product Code";
            if (dgvProducts.Columns.Contains("product_name")) dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            if (dgvProducts.Columns.Contains("price")) dgvProducts.Columns["price"].HeaderText = "Price";
            if (dgvProducts.Columns.Contains("quantity")) dgvProducts.Columns["quantity"].HeaderText = "Quantity";
        }

        private void RenameCartColumns()
        {
            if (dgvProducts.Columns.Contains("product_code")) dgvProducts.Columns["product_code"].HeaderText = "Product Code";
            if (dgvProducts.Columns.Contains("product_name")) dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            if (dgvProducts.Columns.Contains("price")) dgvProducts.Columns["price"].HeaderText = "Price";
            if (dgvProducts.Columns.Contains("quantity")) dgvProducts.Columns["quantity"].HeaderText = "Qty";
            if (dgvProducts.Columns.Contains("subtotal")) dgvProducts.Columns["subtotal"].HeaderText = "Subtotal";
        }

        private void SearchTable(string keyword, string column)
        {
            if (_suppressSelectionChanged) return;

            DataTable source = _isCartView ? _cartManager.CartTable : _productsTable;
            if (string.IsNullOrEmpty(keyword))
            {
                dgvProducts.DataSource = source;
                if (_isCartView) RenameCartColumns(); else RenameProductColumns();
                return;
            }

            var filtered = source.AsEnumerable()
                .Where(r => r[column]?.ToString().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            var result = filtered.Count > 0 ? filtered.CopyToDataTable() : source.Clone();
            dgvProducts.DataSource = result;
            if (_isCartView) RenameCartColumns(); else RenameProductColumns();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            string typedCode = txtProductCode.Text.Trim();
            if (string.IsNullOrEmpty(typedCode))
            { MessageBox.Show("Please select or enter a product code.", "No Product", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var product = _productService.FindProductByCode(_productsTable, typedCode);
            if (product == null)
            { MessageBox.Show("Product code not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            int alreadyInCart = _cartManager.CartTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString() == typedCode)?["quantity"] as int? ?? 0;
            int remainingStock = _productService.GetAvailableStock(_productsTable, typedCode, alreadyInCart);

            if (remainingStock < 1)
            { MessageBox.Show($"No more stock available.", "Stock Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            using var qtyDialog = new QuantityDialog($"{product["product_name"]}\nAvailable: {remainingStock}", remainingStock);
            if (qtyDialog.ShowDialog() != DialogResult.OK) return;

            if (!_cartManager.IsTransactionStarted)
                txtTransNo.Text = TransactionNumberGenerator.Generate();

            _cartManager.AddItem(typedCode, product["product_name"].ToString(), Convert.ToDecimal(product["price"]), qtyDialog.Quantity);
            _calculator.CalculateAmounts(_cartManager.CartTable);
            UpdateFinancialDisplay();
            ShowCartView();
            ClearSearchFields();
        }

        private void btnRemoveItems_Click(object sender, EventArgs e)
        {
            if (!_isCartView)
            { MessageBox.Show("Please switch to cart view to remove items.", "Not in Cart", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvProducts.SelectedRows.Count == 0)
            { MessageBox.Show("Please select an item to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var row = dgvProducts.SelectedRows[0];
            string productCode = row.Cells["product_code"].Value?.ToString();
            string productName = row.Cells["product_name"].Value?.ToString();
            int currentQty = Convert.ToInt32(row.Cells["quantity"].Value);
            decimal price = Convert.ToDecimal(row.Cells["price"].Value);

            using var qtyDialog = new QuantityDialog($"Remove: {productName}\nIn cart: {currentQty}", currentQty, "Remove Item");
            if (qtyDialog.ShowDialog() != DialogResult.OK) return;

            _cartManager.RemoveItem(productCode, qtyDialog.Quantity, price);
            _calculator.CalculateAmounts(_cartManager.CartTable);
            UpdateFinancialDisplay();
            ShowCartView();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (_cartManager.IsEmpty)
            { MessageBox.Show("Cart is empty.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (string.IsNullOrEmpty(txtTransNo.Text))
                txtTransNo.Text = TransactionNumberGenerator.Generate();

            var payment = new PaymentFrm(_username, _companyName, txtTransNo.Text, _cartManager.CartTable.Copy(),
                _calculator.Subtotal, _calculator.DiscountPercentage, _calculator.DiscountAmount,
                _calculator.VatableAmount, _calculator.VatAmount, _calculator.TotalAmount);

            if (payment.ShowDialog() == DialogResult.OK)
            {
                _cartManager.ResetTransaction();
                _calculator.Reset();
                UpdateFinancialDisplay();
                txtTransNo.Text = "";
                _ = LoadProductsAsync();
                MessageBox.Show("Transaction completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cartManager.IsEmpty) return;
            if (MessageBox.Show("Clear all items from cart?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _cartManager.ClearCart();
                _calculator.Reset();
                UpdateFinancialDisplay();
                txtTransNo.Text = "";
                if (_isCartView) ShowCartView();
            }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            _suppressSelectionChanged = true;
            ClearSearchFields();
            ShowProductsView();
            _suppressSelectionChanged = false;
        }

        private void ClearSearchFields()
        {
            txtProductCode.Clear();
            txtProductName.Clear();
            txtQuan.Clear();
            txtPrice.Clear();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged || _isCartView || dgvProducts.SelectedRows.Count == 0) return;
            var row = dgvProducts.SelectedRows[0];
            if (row.IsNewRow) return;

            _suppressSelectionChanged = true;
            txtProductCode.Text = row.Cells["product_code"]?.Value?.ToString() ?? "";
            txtProductName.Text = row.Cells["product_name"]?.Value?.ToString() ?? "";
            txtQuan.Text = row.Cells["quantity"]?.Value?.ToString() ?? "";
            txtPrice.Text = row.Cells["price"]?.Value?.ToString() ?? "";
            _suppressSelectionChanged = false;
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            if (_isCartView) ShowProductsView(); else ShowCartView();
        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            await AuditService.LogLogoutAsync(_username, _companyId, Environment.MachineName);
            new LogInForm().Show();
            Close();
        }

        private void InitializeShortcuts()
        {
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) btnAddToCart_Click(s, e);
                else if (e.KeyCode == Keys.Delete) btnRemoveItems_Click(s, e);
                else if (e.KeyCode == Keys.C && e.Control && e.Shift) btnClearCart_Click(s, e);
                else if (e.KeyCode == Keys.Escape)
                {
                    if (dgvProducts.SelectedRows.Count > 0) btnClearSelection_Click(s, e);
                    else btnLogOut_Click(s, e);
                }
                else if (e.KeyCode == Keys.F2) btnPayment_Click(s, e);
                else if (e.KeyCode == Keys.F1) btnCart_Click(s, e);
                e.Handled = true;
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(btnAddToCart, "Enter");
            toolTip.SetToolTip(btnRemoveItems, "Delete");
            toolTip.SetToolTip(btnClearCart, "Ctrl+Shift+C");
            toolTip.SetToolTip(btnClearSelection, "Esc");
            toolTip.SetToolTip(btnPayment, "F2");
            toolTip.SetToolTip(btnLogOut, "Esc");
            toolTip.SetToolTip(btnCart, "F1");
        }
    }
}