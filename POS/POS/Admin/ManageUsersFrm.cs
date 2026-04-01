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
            titleLabel.Text = $"{_companyName} ";
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
    SELECT u.id, u.username, u.last_name, u.first_name, u.middle_name,
           u.contact_number, u.age, u.birthdate, r.name AS role
    FROM public.users u
    JOIN public.roles r ON u.role_id = r.id
    WHERE u.company_id = @companyId
      AND (
           u.username       ILIKE @search
        OR u.last_name      ILIKE @search
        OR u.first_name     ILIKE @search
        OR u.contact_number ILIKE @search
      )
    ORDER BY u.last_name, u.first_name";

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

        // ─── ADD ──────────────────────────────────────────────────────────────────

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required when adding a new user.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    // Check for duplicate username
                    using (var checkCmd = new NpgsqlCommand(
                        "SELECT COUNT(*) FROM public.users WHERE username = @username AND company_id = @company_id", conn))
                    {
                        checkCmd.Parameters.AddWithValue("username", txtUsername.Text.Trim());
                        checkCmd.Parameters.AddWithValue("company_id",
                            NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
                        long count = (long)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Username already exists in this company. Please choose another.",
                                "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string query = @"
    INSERT INTO public.users
        (username, password, role_id, first_name, last_name,
         middle_name, contact_number, age, birthdate, company_id)
    VALUES
        (@username, @password, (SELECT id FROM public.roles WHERE name = @role), @first_name, @last_name,
         @middle_name, @contact_number, @age, @birthdate, @company_id)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        AddAllParams(cmd, includePassword: true);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("User added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── UPDATE ───────────────────────────────────────────────────────────────

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedUserId))
            {
                MessageBox.Show("Please select a user from the list to update.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateFields()) return;

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    bool changePassword = !string.IsNullOrWhiteSpace(txtPassword.Text);

                    string query = changePassword
     ? @"UPDATE public.users SET
            username       = @username,
            password       = @password,
            role_id        = (SELECT id FROM public.roles WHERE name = @role),
            first_name     = @first_name,
            last_name      = @last_name,
            middle_name    = @middle_name,
            contact_number = @contact_number,
            age            = @age,
            birthdate      = @birthdate
        WHERE id = @id"
     : @"UPDATE public.users SET
            username       = @username,
            role_id        = (SELECT id FROM public.roles WHERE name = @role),
            first_name     = @first_name,
            last_name      = @last_name,
            middle_name    = @middle_name,
            contact_number = @contact_number,
            age            = @age,
            birthdate      = @birthdate
        WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        AddAllParams(cmd, includePassword: changePassword);
                        cmd.Parameters.AddWithValue("id",
                            NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(selectedUserId));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("User updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── DELETE ───────────────────────────────────────────────────────────────

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedUserId))
            {
                MessageBox.Show("Please select a user from the list to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete \"{txtUsername.Text}\"?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(
                        "DELETE FROM public.users WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("id",
                            NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(selectedUserId));
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("User deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── CLEAR ────────────────────────────────────────────────────────────────

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            selectedUserId = null;
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtLastName.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtContact.Text = "";
            txtAge.Text = "";
            dtpBirthdate.Value = DateTime.Today;
            cmbUserLevel.SelectedIndex = -1;
            dgvUsers.ClearSelection();
        }


        // ─── Helpers ──────────────────────────────────────────────────────────────

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            { MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            { MessageBox.Show("Last Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            { MessageBox.Show("First Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (cmbUserLevel.SelectedItem == null)
            { MessageBox.Show("Please select a User Level (ADMIN or CASHIER).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (!string.IsNullOrWhiteSpace(txtAge.Text) && !int.TryParse(txtAge.Text, out _))
            { MessageBox.Show("Age must be a valid number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            return true;
        }

        /// <summary>
        /// Adds shared parameters to a command. Pass includePassword=true for INSERT and password-changing UPDATE.
        /// </summary>
        private void AddAllParams(NpgsqlCommand cmd, bool includePassword)
        {
            cmd.Parameters.AddWithValue("username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("role", cmbUserLevel.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("first_name", txtFirstName.Text.Trim());
            cmd.Parameters.AddWithValue("last_name", txtLastName.Text.Trim());
            cmd.Parameters.AddWithValue("middle_name", (object)txtMiddleName.Text.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("contact_number", (object)txtContact.Text.Trim() ?? DBNull.Value);
            cmd.Parameters.AddWithValue("age", int.TryParse(txtAge.Text, out int a) ? (object)a : DBNull.Value);
            cmd.Parameters.AddWithValue("birthdate", dtpBirthdate.Value.Date);
            cmd.Parameters.AddWithValue("company_id",NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            if (includePassword)
                cmd.Parameters.AddWithValue("password", txtPassword.Text);
        }

        // ─── Load User Levels ─────────────────────────────────────────────────────

        private void LoadUserLevels()
        {
            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT name FROM public.roles ORDER BY name";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbUserLevel.Items.Clear();

                        while (reader.Read())
                        {
                            cmbUserLevel.Items.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles:\n{ex.Message}", "Database Error",
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