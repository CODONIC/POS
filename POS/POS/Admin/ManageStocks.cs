using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ManageStocks : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;
        private DataTable _pendingChanges = new DataTable(); // staging table

        public ManageStocks(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            this.KeyPreview = true;
            this.KeyDown += ManageStocksFrm_KeyDown;
            ShortcutKeyHints();
            _companyId = GetCompanyId(_companyName);
            SetupDataGridViews();
            SetupPendingTable();
        }

        // ─── Load Form ────────────────────────────────────────────────────────────

        private async void ManageStocks_Load(object sender, EventArgs e)
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

        // ─── Setup ────────────────────────────────────────────────────────────────

        private void SetupDataGridViews()
        {
            // all products
            dgvAllProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAllProducts.MultiSelect = false;
            dgvAllProducts.ReadOnly = true;
            dgvAllProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAllProducts.SelectionChanged += dgvAllProducts_SelectionChanged;
            // pending changes
            dgvPending.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPending.MultiSelect = false;
            dgvPending.ReadOnly = true;
            dgvPending.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        
        }
        // ─── Selection ────────────────────────────────────────────────────────────────

        private void dgvAllProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAllProducts.SelectedRows.Count == 0) return;

            var row = dgvAllProducts.SelectedRows[0];

            txtProductCode.Text = row.Cells["Code"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();
            txtCategory.Text = row.Cells["Category"].Value?.ToString();
            txtUnitPrice.Text = row.Cells["Price"].Value?.ToString();
            txtStockInDate.Text = row.Cells["Stocked In Date"].Value?.ToString();
        }

        private void SetupPendingTable()
        {
            _pendingChanges.Columns.Add("product_code", typeof(string));
            _pendingChanges.Columns.Add("product_name", typeof(string));
            _pendingChanges.Columns.Add("category", typeof(string));
            _pendingChanges.Columns.Add("change_type", typeof(string)); // ADD or REMOVE
            _pendingChanges.Columns.Add("quantity", typeof(int));

            dgvPending.DataSource = _pendingChanges;
            dgvPending.Columns["product_code"].HeaderText = "Product Code";
            dgvPending.Columns["product_name"].HeaderText = "Description";
            dgvPending.Columns["category"].HeaderText = "Category";
            dgvPending.Columns["change_type"].HeaderText = "Type";
            dgvPending.Columns["quantity"].HeaderText = "Quantity";
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
    SELECT p.product_code, p.product_name, c.name AS category, 
           p.price, p.quantity, p.reorder_level, p.stocked_in_date
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
                dt.Columns["product_name"].ColumnName = "Description";
                dt.Columns["category"].ColumnName = "Category";
                dt.Columns["price"].ColumnName = "Price";
                dt.Columns["quantity"].ColumnName = "Quantity";
                dt.Columns["reorder_level"].ColumnName = "Reorder Level";
                dt.Columns["stocked_in_date"].ColumnName = "Stocked In Date";

                dgvAllProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Search ───────────────────────────────────────────────────────────────

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadProductsAsync(txtSearch.Text.Trim());
        }

        // ─── ADD STOCK ────────────────────────────────────────────────────────────────

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            if (dgvAllProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAdd.Text.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity to add.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvAllProducts.SelectedRows[0];
            string code = row.Cells["Code"].Value?.ToString();
            string name = row.Cells["Description"].Value?.ToString();
            string cat = row.Cells["Category"].Value?.ToString();


            _pendingChanges.Rows.Add(code, name, cat, "ADD", qty);
            txtAdd.Text = "";
        }

        // ─── REMOVE STOCK ─────────────────────────────────────────────────────────────

        private void btnRemoveStock_Click(object sender, EventArgs e)
        {
            if (dgvAllProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtRemove.Text.Trim(), out int qty) || qty <= 0)
            {
                MessageBox.Show("Please enter a valid quantity to remove.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvAllProducts.SelectedRows[0];
            string code = row.Cells["Code"].Value?.ToString();
            string name = row.Cells["Description"].Value?.ToString();
            string cat = row.Cells["Category"].Value?.ToString();
            int currentQty = Convert.ToInt32(row.Cells["Quantity"].Value);

            if (qty > currentQty)
            {
                MessageBox.Show($"Cannot remove more than current stock ({currentQty}).", "Invalid Quantity",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _pendingChanges.Rows.Add(code, name, cat, "REMOVE", qty);
            txtRemove.Text = "";
        }

        // ─── SAVE ─────────────────────────────────────────────────────────────────

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_pendingChanges.Rows.Count == 0)
            {
                MessageBox.Show("No pending changes to save.", "Nothing to Save",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                foreach (DataRow row in _pendingChanges.Rows)
                {
                    string code = row["product_code"].ToString();
                    string type = row["change_type"].ToString();
                    int qty = Convert.ToInt32(row["quantity"]);

                    
                    string sql = type == "ADD"
                        ? @"UPDATE products SET 
                        quantity = quantity + @qty,
                        stocked_in_date = @date
                    WHERE product_code = @code AND company_id = @companyId"
                        : "UPDATE products SET quantity = quantity - @qty WHERE product_code = @code AND company_id = @companyId";

                    await using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("qty", qty);
                    cmd.Parameters.AddWithValue("code", code);
                    cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

                    if (type == "ADD")
                        cmd.Parameters.AddWithValue("date", DateTime.Today);
                    
                    await cmd.ExecuteNonQueryAsync();
                }

                MessageBox.Show("Stock updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _pendingChanges.Clear();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving stock changes:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── CANCEL ───────────────────────────────────────────────────────────────────

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_pendingChanges.Rows.Count == 0) return;

            var confirm = MessageBox.Show(
                "Are you sure you want to cancel all pending changes?",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _pendingChanges.Clear();
                txtAdd.Text = "";
                txtRemove.Text = "";
            }
        }

        // ─── Back ─────────────────────────────────────────────────────────────────

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_pendingChanges.Rows.Count > 0)
            {
                var confirm = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to go back?",
                    "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;
            }

            AdminDashboard adminDashboard = new AdminDashboard(_username, _companyName);
            adminDashboard.Show();
            this.Close();
        }

        // ─── Shortcut Keys ────────────────────────────────────────────────────────

        private void ManageStocksFrm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: btnBack_Click(sender, e); e.Handled = true; break;
                case Keys.F1: btnAddStock_Click(sender, e); e.Handled = true; break;
                case Keys.F2: btnRemoveStock_Click(sender, e); e.Handled = true; break;
                case Keys.F3: btnSave_Click(sender, e); e.Handled = true; break;
                case Keys.F4: btnCancel_Click(sender, e); e.Handled = true; break;
            }
        }

        private void ShortcutKeyHints()
        {
            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 200;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(btnBack, "ESC");
            toolTip.SetToolTip(btnAddStock, "F1");
            toolTip.SetToolTip(btnRemoveStock, "F2");
            toolTip.SetToolTip(btnSave, "F3");
            toolTip.SetToolTip(btnCancel, "F4");
            AttachHoverEffect(btnBack, "BACK", "ESC");
            AttachHoverEffect(btnAddStock, "ADD STOCK", "F1");
            AttachHoverEffect(btnRemoveStock, "REMOVE STOCK", "F2");
            AttachHoverEffect(btnSave, "SAVE", "F3");
            AttachHoverEffect(btnCancel, "CANCEL", "F4");
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
    }
}