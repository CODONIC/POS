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
        private bool _suppressSelectionChanged = false; // ← NEW


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

        // ─── Load Products ────────────────────────────────────────────────────────

        private async void CashierDashboard_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
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

            // ─── Find product from _productsTable using txtProductCode ────────
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

            // ─── Check if already in cart ─────────────────────────────────────
            var existingRow = _cartTable.AsEnumerable()
                .FirstOrDefault(r => r["product_code"].ToString() == productCode);

            if (existingRow != null)
            {
                int newQty = Convert.ToInt32(existingRow["quantity"]) + 1;

                if (newQty > availableQty)
                {
                    MessageBox.Show($"Not enough stock. Available: {availableQty}", "Stock Limit",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                existingRow["quantity"] = newQty;
                existingRow["subtotal"] = newQty * price;
            }
            else
            {
                if (availableQty < 1)
                {
                    MessageBox.Show("This product is out of stock.", "Out of Stock",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _cartTable.Rows.Add(productCode, productName, price, 1, price);
            }

            txtProductCode.Clear();
            txtProductName.Clear();
            txtQuan.Clear();
            txtPrice.Clear();

            ShowCartView();
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

            _suppressSelectionChanged = true; // ← NEW: prevent TextChanged from firing a search
            txtProductCode.Text = (dgvProducts.Columns.Contains("product_code") && row.Cells["product_code"].Value != null)
                                   ? row.Cells["product_code"].Value.ToString() : "";
            txtProductName.Text = (dgvProducts.Columns.Contains("product_name") && row.Cells["product_name"].Value != null)
                                   ? row.Cells["product_name"].Value.ToString() : "";
            txtQuan.Text = (dgvProducts.Columns.Contains("quantity") && row.Cells["quantity"].Value != null)
                           ? row.Cells["quantity"].Value.ToString() : "";
            txtPrice.Text = (dgvProducts.Columns.Contains("price") && row.Cells["price"].Value != null)
                            ? row.Cells["price"].Value.ToString() : "";
            _suppressSelectionChanged = false; // ← NEW
        }

        // ─── Search ───────────────────────────────────────────────────────────────

        private void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionChanged) return; // ← prevents loop when row selection fills the textbox
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

            _suppressSelectionChanged = true; // ← NEW
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = result;
            _suppressSelectionChanged = false; // ← NEW

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

            dgvProducts.ClearSelection();

            _suppressSelectionChanged = false;

            // ← Restore full product list in case a search had filtered it
            ShowProductsView();
        }
    }
}