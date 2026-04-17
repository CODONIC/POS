
using Npgsql;
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

namespace POS.Admin
{
    public partial class ManageProdFrm : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;
        private string _selectedProductId;

        public ManageProdFrm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            this.KeyPreview = true;
            this.KeyDown += ManageProdFrm_KeyDown;
            ShortcutKeyHints();
            _companyId = GetCompanyId(_companyName);
            SetupDataGridView();
            LoadCategories();
        }
        // ─── Load Form ────────────────────────────────────────────────────────────

        private async void ManageProdFrm_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
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

        // ─── Setup DataGridView ───────────────────────────────────────────────────

        private void SetupDataGridView()
        {
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        }

        // ─── Load Products ────────────────────────────────────────────────────────

        private async Task LoadProductsAsync(string search = "")
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                string sql = @"
    SELECT p.product_code, p.product_name, p.price, 
           p.quantity, p.reorder_level, c.name AS category
    FROM products p
    LEFT JOIN categories c ON p.category_id = c.id
    WHERE p.company_id = @companyId
      AND (
           p.product_code ILIKE @search
        OR p.product_name ILIKE @search
        OR c.name         ILIKE @search
      )
    ORDER BY p.product_name";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
                cmd.Parameters.AddWithValue("search", $"%{search}%");

                var adapter = new NpgsqlDataAdapter(cmd);
                var dt = new DataTable();
                adapter.Fill(dt);

                dt.Columns["product_code"].ColumnName = "Code";
                dt.Columns["product_name"].ColumnName = "Product Name";
                dt.Columns["price"].ColumnName = "Price";
                dt.Columns["quantity"].ColumnName = "Quantity";
                dt.Columns["reorder_level"].ColumnName = "Reorder Level";
                dt.Columns["category"].ColumnName = "Category";

                dgvProducts.DataSource = dt;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Load Categories into ComboBox ────────────────────────────────────────

        private void LoadCategories()
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT id, name FROM categories 
                                   WHERE company_id = @companyId 
                                   ORDER BY name";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("companyId",
                            NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                        using (var reader = cmd.ExecuteReader())
                        {
                            cmbCategory.Items.Clear();
                            while (reader.Read())
                            {
                                cmbCategory.Items.Add(new CategoryItem
                                {
                                    Id = reader["id"].ToString(),
                                    Name = reader["name"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Selection ────────────────────────────────────────────────────────────

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
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
            {
                if (item is CategoryItem cat && cat.Name == categoryName)
                {
                    cmbCategory.SelectedItem = item;
                    break;
                }
            }
        }

        // ─── Search ───────────────────────────────────────────────────────────────

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadProductsAsync(txtSearch.Text.Trim());
        }
        // ─── Buttons ───────────────────────────────────────────────────────────────
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string productCode = txtProductCode.Text.Trim();
            string productName = txtProductName.Text.Trim();
            decimal price = decimal.Parse(txtPrice.Text.Trim());
            int reorderLevel = int.Parse(txtReorderLevel.Text.Trim());
            var selectedCategory = cmbCategory.SelectedItem as CategoryItem;

            try
            {
                btnAdd.Enabled = false;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Check duplicate product code
                string checkSql = "SELECT COUNT(*) FROM products WHERE product_code = @code AND company_id = @companyId";
                await using var checkCmd = new NpgsqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("code", productCode);
                checkCmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                long count = (long)await checkCmd.ExecuteScalarAsync();
                if (count > 0)
                {
                    MessageBox.Show($"Product code '{productCode}' already exists.", "Duplicate Code",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProductCode.Focus();
                    return;
                }

                string sql = @"
            INSERT INTO products (product_code, product_name, price, quantity, reorder_level, category_id, company_id)
            VALUES (@code, @name, @price, 0, @reorderLevel, @categoryId, @companyId)";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("code", productCode);
                cmd.Parameters.AddWithValue("name", productName);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("reorderLevel", reorderLevel);
                cmd.Parameters.AddWithValue("categoryId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(selectedCategory.Id));
                cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                await cmd.ExecuteNonQueryAsync();

                MessageBox.Show($"Product '{productName}' added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAdd.Enabled = true;
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            {
                MessageBox.Show("Please select a product to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            string productName = txtProductName.Text.Trim();
            decimal price = decimal.Parse(txtPrice.Text.Trim());
            int reorderLevel = int.Parse(txtReorderLevel.Text.Trim());
            var selectedCategory = cmbCategory.SelectedItem as CategoryItem;

            var confirm = MessageBox.Show($"Update product '{_selectedProductId}'?", "Confirm Edit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                btnEdit.Enabled = false;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                string sql = @"
            UPDATE products 
            SET product_name  = @name,
                price         = @price,
                reorder_level = @reorderLevel,
                category_id   = @categoryId
            WHERE product_code = @code
              AND company_id   = @companyId";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("name", productName);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("reorderLevel", reorderLevel);
                cmd.Parameters.AddWithValue("categoryId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(selectedCategory.Id));
                cmd.Parameters.AddWithValue("code", _selectedProductId);
                cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                await cmd.ExecuteNonQueryAsync();

                MessageBox.Show($"Product '{productName}' updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEdit.Enabled = true;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            {
                MessageBox.Show("Please select a product to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string productName = txtProductName.Text.Trim();

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete '{productName}' ({_selectedProductId})?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                btnDelete.Enabled = false;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                string sql = "DELETE FROM products WHERE product_code = @code AND company_id = @companyId";
                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("code", _selectedProductId);
                cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                await cmd.ExecuteNonQueryAsync();

                MessageBox.Show($"Product '{productName}' deleted successfully.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // ─── Helpers ──────────────────────────────────────────────────────────────────

        private void ClearFields()
        {
            _selectedProductId = null;
            txtProductCode.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtReorderLevel.Text = "";
            cmbCategory.SelectedIndex = -1;
            dgvProducts.ClearSelection();
            txtProductCode.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtProductCode.Text))
            {
                MessageBox.Show("Product code is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Please enter a valid price (0 or greater).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtReorderLevel.Text.Trim(), out int reorder) || reorder < 0)
            {
                MessageBox.Show("Please enter a valid reorder level (0 or greater).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReorderLevel.Focus();
                return false;
            }

            if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            return true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard(_username, _companyName);
            admin.Show();
            this.Hide();
        }

        // ─── Shortcut Keys ────────────────────────────────────────────────────────────

        private void ManageProdFrm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btnBack_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F1:
                    btnAdd_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F2:
                    btnEdit_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F3:
                    btnDelete_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F4:
                    btnClear_Click(sender, e);
                    e.Handled = true;
                    break;
            }
        }

        private void ShortcutKeyHints()
        {
            //Shortcut keys:

            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 200; // ms before tooltip appears
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(btnBack, "ESC");
            toolTip.SetToolTip(btnAdd, "F1");
            toolTip.SetToolTip(btnEdit, "F2");
            toolTip.SetToolTip(btnDelete, "F3");
            toolTip.SetToolTip(btnClear, "F4");
            AttachHoverEffect(btnBack, "BACK", "ESC");
            AttachHoverEffect(btnAdd, "ADD", "F1");
            AttachHoverEffect(btnEdit, "EDIT", "F2");
            AttachHoverEffect(btnDelete, "DELETE", "F3");
            AttachHoverEffect(btnClear, "CLEAR", "F4");
        }
        private void AttachHoverEffect(Button btn, string defaultText, string shortcut)
        {
            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.Text = $"{defaultText}\n({shortcut})";
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Text = defaultText;
                btn.Location = originalLocation;
            };
        }
        // ─── Helper Class ─────────────────────────────────────────────────────────────
        public class CategoryItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }
    }
}
