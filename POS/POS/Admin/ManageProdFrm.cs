
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Code here
        }
        // ─── Buttons ───────────────────────────────────────────────────────────────
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Code here
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Code here
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Code here
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
