using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ProdCategoryFrm : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;
        private string _selectedCategoryId = null;
        public ProdCategoryFrm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            _companyId = GetCompanyId(_companyName);
            this.KeyPreview = true;
            this.KeyDown += ProdCategoryFrm_KeyDown;
            ShortcutKeyHints();
        }

        // ─── Search ───────────────────────────────────────────────────────────────

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadCategoriesAsync(txtSearch.Text.Trim());
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

        // ─── Load ─────────────────────────────────────────────────────────────────

        private async void ProdCategoryFrm_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync(string search = "")
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                string sql = @"
            SELECT id, name 
            FROM categories 
            WHERE company_id = @companyId
              AND name ILIKE @search
            ORDER BY name";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
                cmd.Parameters.AddWithValue("search", $"%{search}%");

                await using var reader = await cmd.ExecuteReaderAsync();

                var dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("name");

                while (await reader.ReadAsync())
                {
                    dt.Rows.Add(reader["id"].ToString(), reader["name"].ToString());
                }

                dgvCategories.DataSource = dt;
                dgvCategories.Columns["id"].HeaderText = "ID";
                dgvCategories.Columns["name"].HeaderText = "Category Name";
                dgvCategories.Columns["id"].Visible = false;
                dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCategories.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load categories: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Selection ────────────────────────────────────────────────────────────

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvCategories.SelectedRows[0];
            _selectedCategoryId = row.Cells["id"].Value?.ToString();
            txtCategoryName.Text = row.Cells["name"].Value?.ToString();
        }

        // ─── ADD ──────────────────────────────────────────────────────────────────────

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            //CODE HERE
        }

        // ─── EDIT ─────────────────────────────────────────────────────────────────────

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            //CODE HERE
        }

        // ─── DELETE ───────────────────────────────────────────────────────────────────

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            //CODE HERE
        }

        // ─── CLEAR ────────────────────────────────────────────────────────────────────

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            _selectedCategoryId = null;
            txtCategoryName.Text = "";
            dgvCategories.ClearSelection();
        }

        // ─── Back ─────────────────────────────────────────────────────────────────

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard(_username, _companyName);
            admin.Show();
            this.Close();
        }

        // ─── Shortcut Keys ────────────────────────────────────────────────────────────

        private void ProdCategoryFrm_KeyDown(object sender, KeyEventArgs e)
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

    }
}