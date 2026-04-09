using Npgsql;
using POS.Cashier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace POS
{
    public partial class CashierDashboard : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;

        private DataTable _productsTable = new DataTable();
        private DataTable _cartTable = new DataTable();
        private bool _isCartView = false;
        private bool _suppressSelectionChanged = false;
        private bool _transactionStarted = false;


        public CashierDashboard(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);

            // ─── Setup dgv ────────────────────────────────────────────────────
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;

            _username = username;
            _companyName = companyName;
            lblCashierName.Text = $"{_username} | Cashier";
            titleLabel.Text = $"{_companyName}";
            _companyId = GetCompanyId(_companyName);

            // ─── Setup cart table columns ─────────────────────────────────────
            _cartTable.Columns.Add("product_code", typeof(string));
            _cartTable.Columns.Add("product_name", typeof(string));
            _cartTable.Columns.Add("price", typeof(decimal));
            _cartTable.Columns.Add("quantity", typeof(int));
            _cartTable.Columns.Add("subtotal", typeof(decimal));

            // ─── Wire up search events ────────────────────────────────────────
            txtProductCode.TextChanged += txtProductCode_TextChanged;
            txtProductName.TextChanged += txtProductName_TextChanged;
            txtQuan.TextChanged += txtQuan_TextChanged;
            txtPrice.TextChanged += txtPrice_TextChanged;

            dgvProducts.AllowUserToAddRows = false;

            this.KeyPreview = true;
            this.KeyDown += CashierDashboard_KeyDown;
            ShortcutKeyHints();
        }

        // ─── Resolve company name to ID ───────────────────────────────────────────

        private string GetCompanyId(string companyName)
        {
            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", companyName);
                        var result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resolving company:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // ─── Generate Transaction Number ──────────────────────────────────────────

        private void GenerateTransactionNumber()
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string timePart = DateTime.Now.ToString("HHmmss");
            string randomPart = new Random().Next(100, 999).ToString();
            txtTransNo.Text = $"TXN-{datePart}-{timePart}-{randomPart}";
        }

        // ─── Load Products ────────────────────────────────────────────────────────

        private async void CashierDashboard_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
            UpdateTotalPrice();
        }

        private async Task LoadProductsAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                string sql = @"
                    SELECT p.product_code, p.product_name, p.price, p.quantity
                    FROM products p
                    WHERE p.company_id = @companyId
                    ORDER BY p.product_name";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                await using var reader = await cmd.ExecuteReaderAsync();

                _productsTable = new DataTable();
                _productsTable.Load(reader);

                ShowProductsView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Update Total Price ───────────────────────────────────────────────────

        private void UpdateTotalPrice()
        {
            decimal total = _cartTable.AsEnumerable()
                .Sum(r => Convert.ToDecimal(r["subtotal"]));

            lblTotalPrice.Text = $"{total:N2}";
        }

        // ─── View Switching ───────────────────────────────────────────────────────

        private void ShowProductsView()
        {
            _suppressSelectionChanged = true;
            _isCartView = false;
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = _productsTable;

            if (dgvProducts.Columns.Contains("product_code")) dgvProducts.Columns["product_code"].HeaderText = "Product Code";
            if (dgvProducts.Columns.Contains("product_name")) dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            if (dgvProducts.Columns.Contains("price")) dgvProducts.Columns["price"].HeaderText = "Price";
            if (dgvProducts.Columns.Contains("quantity")) dgvProducts.Columns["quantity"].HeaderText = "Quantity";

            _suppressSelectionChanged = false;
            lblProducts.Text = "Products";
            btnCart.Text = "Cart Table >";
            dgvProducts.ClearSelection();
        }

        private void ShowCartView()
        {
            _suppressSelectionChanged = true;
            _isCartView = true;
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = _cartTable;

            if (dgvProducts.Columns.Contains("product_code")) dgvProducts.Columns["product_code"].HeaderText = "Product Code";
            if (dgvProducts.Columns.Contains("product_name")) dgvProducts.Columns["product_name"].HeaderText = "Product Name";
            if (dgvProducts.Columns.Contains("price")) dgvProducts.Columns["price"].HeaderText = "Price";
            if (dgvProducts.Columns.Contains("quantity")) dgvProducts.Columns["quantity"].HeaderText = "Qty";
            if (dgvProducts.Columns.Contains("subtotal")) dgvProducts.Columns["subtotal"].HeaderText = "Subtotal";

            _suppressSelectionChanged = false;
            lblProducts.Text = "Cart";
            btnCart.Text = "< Products Table";
            dgvProducts.ClearSelection();
        }

        // ─── Toggle Cart / Products ───────────────────────────────────────────────

        private void btnCart_Click(object sender, EventArgs e)
        {
            if (_isCartView)
                ShowProductsView();
            else
                ShowCartView();
        }

        // ─── Add to Cart ──────────────────────────────────────────────────────────

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            string typedCode = txtProductCode.Text.Trim();

            if (string.IsNullOrEmpty(typedCode))
            {
                MessageBox.Show("Please select a product or enter a product code.", "No Product Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var match = _productsTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString()
                    .Equals(typedCode, StringComparison.OrdinalIgnoreCase));

            if (match == null)
            {
                MessageBox.Show("Product code not found.", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productCode = match["product_code"].ToString();
            string productName = match["product_name"].ToString();
            decimal price = Convert.ToDecimal(match["price"]);
            int availableQty = Convert.ToInt32(match["quantity"]);

            // ─── Check existing cart quantity ─────────────────────────────────
            var existingRow = _cartTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString() == productCode);
            int alreadyInCart = existingRow != null ? Convert.ToInt32(existingRow["quantity"]) : 0;
            int remainingStock = availableQty - alreadyInCart;

            if (remainingStock < 1)
            {
                MessageBox.Show($"No more stock available. Already {alreadyInCart} in cart.", "Stock Limit",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ─── Prompt for quantity ──────────────────────────────────────────
            using (var qtyDialog = new QuantityDialog($"{productName}\nAvailable: {remainingStock}", remainingStock))
            {
                if (qtyDialog.ShowDialog() != DialogResult.OK) return;
                int requestedQty = qtyDialog.Quantity;

                // ─── Generate transaction number on first item added ───────────
                if (!_transactionStarted)
                {
                    GenerateTransactionNumber();
                    _transactionStarted = true;
                }

                if (existingRow != null)
                {
                    int newQty = alreadyInCart + requestedQty;
                    existingRow["quantity"] = newQty;
                    existingRow["subtotal"] = newQty * price;
                }
                else
                {
                    _cartTable.Rows.Add(productCode, productName, price, requestedQty, requestedQty * price);
                }
            }

            txtProductCode.Clear();
            txtProductName.Clear();
            txtQuan.Clear();
            txtPrice.Clear();

            ShowCartView();
            UpdateTotalPrice();
        }

        // ─── Populate fields on row selection ────────────────────────────────────

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged) return;
            if (_isCartView) return;
            if (dgvProducts.SelectedRows.Count == 0) return;

            var row = dgvProducts.SelectedRows[0];
            if (row.Index < 0 || row.Index >= dgvProducts.Rows.Count) return;
            if (row.IsNewRow) return;

            _suppressSelectionChanged = true;
            txtProductCode.Text = (dgvProducts.Columns.Contains("product_code") && row.Cells["product_code"].Value != null)
                                   ? row.Cells["product_code"].Value.ToString() : "";
            txtProductName.Text = (dgvProducts.Columns.Contains("product_name") && row.Cells["product_name"].Value != null)
                                   ? row.Cells["product_name"].Value.ToString() : "";
            txtQuan.Text = (dgvProducts.Columns.Contains("quantity") && row.Cells["quantity"].Value != null)
                           ? row.Cells["quantity"].Value.ToString() : "";
            txtPrice.Text = (dgvProducts.Columns.Contains("price") && row.Cells["price"].Value != null)
                            ? row.Cells["price"].Value.ToString() : "";
            _suppressSelectionChanged = false;
        }

        // ─── Search ───────────────────────────────────────────────────────────────

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged) return;
            SearchTable(txtProductCode.Text.Trim(), "product_code");
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged) return;
            SearchTable(txtProductName.Text.Trim(), "product_name");
        }

        private void txtQuan_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged) return;
            SearchTable(txtQuan.Text.Trim(), "quantity");
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged) return;
            SearchTable(txtPrice.Text.Trim(), "price");
        }

        private void SearchTable(string keyword, string column)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                if (_isCartView)
                    ShowCartView();
                else
                    ShowProductsView();
                return;
            }

            DataTable source = _isCartView ? _cartTable : _productsTable;

            var filtered = source.AsEnumerable()
                .Where(r => r[column]?.ToString()
                    .IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            var result = filtered.Count > 0
                ? filtered.CopyToDataTable()
                : source.Clone();

            _suppressSelectionChanged = true;
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = result;
            _suppressSelectionChanged = false;

            if (_isCartView)
            {
                if (dgvProducts.Columns.Contains("product_code")) dgvProducts.Columns["product_code"].HeaderText = "Product Code";
                if (dgvProducts.Columns.Contains("product_name")) dgvProducts.Columns["product_name"].HeaderText = "Product Name";
                if (dgvProducts.Columns.Contains("price")) dgvProducts.Columns["price"].HeaderText = "Price";
                if (dgvProducts.Columns.Contains("quantity")) dgvProducts.Columns["quantity"].HeaderText = "Qty";
                if (dgvProducts.Columns.Contains("subtotal")) dgvProducts.Columns["subtotal"].HeaderText = "Subtotal";
            }
            else
            {
                if (dgvProducts.Columns.Contains("product_code")) dgvProducts.Columns["product_code"].HeaderText = "Product Code";
                if (dgvProducts.Columns.Contains("product_name")) dgvProducts.Columns["product_name"].HeaderText = "Product Name";
                if (dgvProducts.Columns.Contains("price")) dgvProducts.Columns["price"].HeaderText = "Price";
                if (dgvProducts.Columns.Contains("quantity")) dgvProducts.Columns["quantity"].HeaderText = "Quantity";
            }
        }

        // ─── Buttons ──────────────────────────────────────────────────────────────

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                LogInForm login = new LogInForm();
                login.Show();
                this.Close();
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            DiscountFrm discount = new DiscountFrm(_username, _companyName);
            discount.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentFrm payment = new PaymentFrm(_username, _companyName);
            payment.Show();
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            _suppressSelectionChanged = true;

            txtProductCode.Clear();
            txtProductName.Clear();
            txtQuan.Clear();
            txtPrice.Clear();
            ShowProductsView();
            dgvProducts.ClearSelection();

            _suppressSelectionChanged = false;
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is already empty.", "Clear Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to clear all items from the cart?",
                "Confirm Clear Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _cartTable.Rows.Clear();
                _transactionStarted = false;
                txtTransNo.Text = "";

                if (_isCartView)
                    ShowCartView();

                UpdateTotalPrice();
            }
        }

        private void btnRemoveItems_Click(object sender, EventArgs e)
        {
            // ─── Must be in cart view ─────────────────────────────────────────
            if (!_isCartView)
            {
                MessageBox.Show("Please switch to the cart view to remove items.",
                    "Not in Cart View", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ─── Must have a row selected ─────────────────────────────────────
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item from the cart to remove.",
                    "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ─── Get selected row data ────────────────────────────────────────
            var selectedRow = dgvProducts.SelectedRows[0];
            string productCode = selectedRow.Cells["product_code"].Value?.ToString();
            string productName = selectedRow.Cells["product_name"].Value?.ToString();
            int currentQty = Convert.ToInt32(selectedRow.Cells["quantity"].Value);
            decimal price = Convert.ToDecimal(selectedRow.Cells["price"].Value);

            // ─── Find matching row in _cartTable ──────────────────────────────
            var cartRow = _cartTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString() == productCode);

            if (cartRow == null) return;

            // ─── Prompt how many to remove ────────────────────────────────────
            using (var qtyDialog = new QuantityDialog($"Remove: {productName}\nIn cart: {currentQty}", currentQty, "Remove Item"))
            {
                if (qtyDialog.ShowDialog() != DialogResult.OK) return;
                int removeQty = qtyDialog.Quantity;

                int newQty = currentQty - removeQty;

                if (newQty <= 0)
                {
                    _cartTable.Rows.Remove(cartRow);
                }
                else
                {
                    cartRow["quantity"] = newQty;
                    cartRow["subtotal"] = newQty * price;
                }
            }

            // ─── If cart is now empty, reset transaction ──────────────────────
            if (_cartTable.Rows.Count == 0)
            {
                _transactionStarted = false;
                txtTransNo.Text = "";
            }

            ShowCartView();
            UpdateTotalPrice();
        }

        // ─── Reset Transaction (called externally after successful payment) ────────
        public void ResetTransaction()
        {
            _cartTable.Rows.Clear();
            _transactionStarted = false;
            txtTransNo.Text = "";
            UpdateTotalPrice();

            if (_isCartView)
                ShowCartView();
        }


        // ─── Shortcut Keys ────────────────────────────────────────────────────────────

        private void CashierDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)                            // Add to Cart
            {
                btnAddToCart_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Delete)                      // Remove from Cart
            {
                btnRemoveItems_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.C && e.Control && e.Shift)  // Clear Cart
            {
                btnClearCart_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    // ─── Only clear selection if a row is actually selected
                    btnClearSelection_Click(sender, e);
                }
                else
                {
                    // ─── No row selected, go straight to logout
                    btnLogOut_Click(sender, e);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)                          // Proceed to Payment
            {
                btnPayment_Click(sender, e);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnCart_Click(sender, e);
                e.Handled = true;
            }
        }
        


        private void ShortcutKeyHints()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 200;
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(btnAddToCart, "Enter");
            toolTip.SetToolTip(btnRemoveItems, "Delete");
            toolTip.SetToolTip(btnClearCart, "Ctrl + Shift + C");
            toolTip.SetToolTip(btnClearSelection, "Esc");
            toolTip.SetToolTip(btnPayment, "F2");
            toolTip.SetToolTip(btnLogOut, "Esc");
            toolTip.SetToolTip(btnCart, "F1");

            AttachHoverEffect(btnAddToCart);
            AttachHoverEffect(btnRemoveItems);
            AttachHoverEffect(btnClearCart);
            AttachHoverEffect(btnClearSelection);
            AttachHoverEffect(btnPayment);
            AttachHoverEffect(btnLogOut);
            AttachHoverEffect(btnCart);
        }
        private void AttachHoverEffect(Button btn)
        {
            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
                btn.Padding = new Padding(0, 0, 0, 6); // push text up
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Location = originalLocation;
                btn.Padding = new Padding(0); // reset
            };
        }
    }
}