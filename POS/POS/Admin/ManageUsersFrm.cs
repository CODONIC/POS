using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ManageUsersFrm : BaseForm
    {
        private string selectedUserId = null;
        private string _username;
        private string _companyName;
        private string _companyId; 

        public ManageUsersFrm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            SetupDataGridView();
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            _companyId = GetCompanyId(_companyName);
            titleLabel.Text =$"{_companyName} ";
            LoadUserLevels();
            LoadUsers();
        }

        // ─── Setup ────────────────────────────────────────────────────────────────

        private void SetupDataGridView()
        {
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
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

        // ─── Load database ────────────────────────────────────────────────────────

        private void LoadUsers(string search = "")
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT id, username, last_name, first_name, middle_name,
                               contact_number, age, birthdate, role
                        FROM public.users
                        WHERE company_id = @companyId
                          AND (
                               username       ILIKE @search
                            OR last_name      ILIKE @search
                            OR first_name     ILIKE @search
                            OR contact_number ILIKE @search
                          )
                        ORDER BY last_name, first_name";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
                        cmd.Parameters.AddWithValue("@search", $"%{search}%");

                        var adapter = new NpgsqlDataAdapter(cmd);
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        dt.Columns["id"].ColumnName = "ID";
                        dt.Columns["username"].ColumnName = "Username";
                        dt.Columns["last_name"].ColumnName = "Last Name";
                        dt.Columns["first_name"].ColumnName = "First Name";
                        dt.Columns["middle_name"].ColumnName = "Middle Name";
                        dt.Columns["contact_number"].ColumnName = "Contact #";
                        dt.Columns["age"].ColumnName = "Age";
                        dt.Columns["birthdate"].ColumnName = "Birthdate";
                        dt.Columns["role"].ColumnName = "User Level";

                        dgvUsers.DataSource = dt;

                        if (dgvUsers.Columns["ID"] != null)
                            dgvUsers.Columns["ID"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Search ───────────────────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUsers(txtSearch.Text.Trim());
        }

        // ─── Populate text boxes based on selection ───────────────────────────────

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var row = dgvUsers.SelectedRows[0];

            selectedUserId = row.Cells["ID"].Value?.ToString();
            txtUsername.Text = row.Cells["Username"].Value?.ToString();
            txtPassword.Text = "";
            txtLastName.Text = row.Cells["Last Name"].Value?.ToString();
            txtFirstName.Text = row.Cells["First Name"].Value?.ToString();
            txtMiddleName.Text = row.Cells["Middle Name"].Value?.ToString();
            txtContact.Text = row.Cells["Contact #"].Value?.ToString();
            txtAge.Text = row.Cells["Age"].Value?.ToString();

            if (DateTime.TryParse(row.Cells["Birthdate"].Value?.ToString(), out DateTime bd))
                dtpBirthdate.Value = bd;

            cmbUserLevel.SelectedItem = row.Cells["User Level"].Value?.ToString();
        }

        // ─── Load User Levels ─────────────────────────────────────────────────────

        private void LoadUserLevels()
        {
            if (string.IsNullOrEmpty(_companyId)) return;

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT DISTINCT role 
                        FROM public.users 
                        WHERE company_id = @companyId
                        ORDER BY role";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));

                        using (var reader = cmd.ExecuteReader())
                        {
                            cmbUserLevel.Items.Clear();

                            while (reader.Read())
                            {
                                string role = reader["role"].ToString();
                                if (!string.IsNullOrEmpty(role))
                                    cmbUserLevel.Items.Add(role);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user levels:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Back ─────────────────────────────────────────────────────────────────

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard(_username, _companyName);
            admin.Show();
            this.Close();
        }
    }
}