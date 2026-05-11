using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ManageUsersFrm : BaseForm
    {
        private readonly string _username, _companyName, _companyId;
        private readonly UserService _userService;
        private string _selectedUserId;
        private readonly string _userId;
        private readonly string _sessionToken;

        public ManageUsersFrm(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _userService = new UserService(_companyId);
            _userId = userId;
            _sessionToken = sessionToken;

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            SetUserContext(_username, _userId, _sessionToken);
            SetUserContext(_username, _companyId);

            SetupDataGridView();
            InitializeShortcuts();
            this.Load += async (s, e) => { await LoadRolesAsync(); await LoadUsersAsync(); };
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
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.SelectionChanged += (s, e) => LoadSelectedUser();
        }

        private async Task LoadUsersAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;
            try
            {
                var dt = await _userService.GetUsersAsync(txtSearch.Text.Trim());
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
            catch (Exception ex) { MessageBox.Show($"Error loading users:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async Task LoadRolesAsync()
        {
            try
            {
                var roles = await _userService.GetRolesAsync();
                cmbUserLevel.Items.Clear();
                foreach (var role in roles)
                    cmbUserLevel.Items.Add(role);
            }
            catch (Exception ex) { MessageBox.Show($"Error loading roles:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadSelectedUser()
        {
            if (dgvUsers.SelectedRows.Count == 0) return;
            var row = dgvUsers.SelectedRows[0];
            _selectedUserId = row.Cells["ID"].Value?.ToString();
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

        private async Task<bool> ConfirmAdminPasswordAsync()
        {
            return await ManageUsersHelper.ConfirmAdminPasswordAsync(_username, _userService, _companyId);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            // STEP 1: Get the selected role first
            string newRole = cmbUserLevel.SelectedItem?.ToString() ?? "";

            // STEP 2: Check admin limit IMMEDIATELY if trying to add an admin
            if (newRole == "ADMIN")
            {
                int currentAdminCount = await _userService.GetAdminCountAsync(_companyId);

                if (currentAdminCount >= 2)
                {
                    MessageBox.Show(
                        "❌ Cannot add another admin.\n\n" +
                        $"Maximum of 2 admins per company is allowed.\n\n" +
                        $"Current admins: {currentAdminCount}/2\n\n" +
                        "Please remove or change an existing admin's role before adding a new one.",
                        "Admin Limit Reached",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return; // EXIT immediately - don't proceed
                }
            }

            // STEP 3: Now validate other fields
            var result = UserValidator.ValidateFields(txtUsername.Text, txtLastName.Text, txtFirstName.Text,
                cmbUserLevel.SelectedItem, txtAge.Text, txtPassword.Text, true);

            if (!result.IsValid)
            { MessageBox.Show(result.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            // STEP 4: Check username exists
            if (await _userService.UsernameExistsAsync(txtUsername.Text.Trim()))
            { MessageBox.Show("Username already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            // STEP 5: Password confirmation (only after all other checks pass)
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (!await ConfirmAdminPasswordAsync())
                    return;
            }

            // STEP 6: Add the user
            await _userService.AddUserAsync(txtUsername.Text.Trim(), txtPassword.Text, newRole,
                txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtMiddleName.Text.Trim(), txtContact.Text.Trim(),
                int.TryParse(txtAge.Text, out int age) ? age : (int?)null, dtpBirthdate.Value);

            await AuditService.LogInsertAsync(_username, _companyId, "users", txtUsername.Text.Trim(),
                AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text),
                ("last_name", txtLastName.Text), ("role", newRole)));

            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadUsersAsync();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUserId))
            { MessageBox.Show("Please select a user to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var result = UserValidator.ValidateFields(txtUsername.Text, txtLastName.Text, txtFirstName.Text,
                cmbUserLevel.SelectedItem, txtAge.Text);

            if (!result.IsValid)
            { MessageBox.Show(result.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string oldRole = await _userService.GetUserRoleAsync(_selectedUserId);
            string newRole = cmbUserLevel.SelectedItem?.ToString() ?? "";

            // Check if user is trying to change role to ADMIN when limit is reached
            if (newRole == "ADMIN" && oldRole != "ADMIN")
            {
                bool canChangeToAdmin = await _userService.CanChangeToAdminRoleAsync(_companyId, _selectedUserId);

                if (!canChangeToAdmin)
                {
                    int currentAdminCount = await _userService.GetAdminCountAsync(_companyId);
                    MessageBox.Show(
                        $"❌ Cannot change role to ADMIN.\n\n" +
                        $"Maximum of 2 admins per company is allowed.\n\n" +
                        $"Current admins: {currentAdminCount}/2\n\n" +
                        $"Please remove or change an existing admin's role before adding a new one.",
                        "Admin Limit Reached",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            // Check if user is trying to change the last admin to non-admin
            if (oldRole == "ADMIN" && newRole != "ADMIN")
            {
                int adminCount = await _userService.GetAdminCountAsync(_companyId);
                if (adminCount <= 1)
                {
                    MessageBox.Show(
                        "❌ Cannot change the last admin to a different role.\n\n" +
                        "At least 1 admin is required per company.\n\n" +
                        "Please add another admin first before changing this user's role.",
                        "Last Admin Restriction",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            bool changePassword = !string.IsNullOrWhiteSpace(txtPassword.Text);

            if (changePassword)
            {
                if (!await ConfirmAdminPasswordAsync())
                    return;
            }

            var oldValues = await _userService.GetUserOldValuesAsync(_selectedUserId);

            await _userService.UpdateUserAsync(_selectedUserId, txtUsername.Text.Trim(), txtPassword.Text,
                newRole, txtFirstName.Text.Trim(), txtLastName.Text.Trim(),
                txtMiddleName.Text.Trim(), txtContact.Text.Trim(), int.TryParse(txtAge.Text, out int age) ? age : (int?)null,
                dtpBirthdate.Value, changePassword);

            await AuditService.LogUpdateAsync(_username, _companyId, "users", _selectedUserId,
                AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text),
                ("last_name", txtLastName.Text), ("role", oldRole)),
                AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text),
                ("last_name", txtLastName.Text), ("role", newRole)));

            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadUsersAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUserId))
            {
                MessageBox.Show("Please select a user to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string roleToDelete = cmbUserLevel.SelectedItem?.ToString() ?? "";

            // Check if deleting an admin would leave less than 1 admin (should have at least 1)
            if (roleToDelete == "ADMIN")
            {
                int adminCount = await _userService.GetAdminCountAsync(_companyId);
                if (adminCount <= 1)
                {
                    MessageBox.Show(
                        "❌ Cannot delete the last admin account.\n\n" +
                        "At least 1 admin is required per company.\n\n" +
                        "Please add another admin first before deleting this one.",
                        "Last Admin Restriction",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            if (UserValidator.ConfirmDelete(txtUsername.Text) != DialogResult.Yes)
                return;

            if (!await ConfirmAdminPasswordAsync())
                return;

            await _userService.DeleteUserAsync(_selectedUserId);
            await AuditService.LogDeleteAsync(_username, _companyId, "users", _selectedUserId,
                AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text),
                ("last_name", txtLastName.Text), ("role", roleToDelete)));

            MessageBox.Show("User deleted successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadUsersAsync();
        }

        private void ClearFields()
        {
            _selectedUserId = null;
            txtUsername.Text = txtPassword.Text = txtLastName.Text = txtFirstName.Text =
            txtMiddleName.Text = txtContact.Text = txtAge.Text = "";
            dtpBirthdate.Value = DateTime.Today;
            cmbUserLevel.SelectedIndex = -1;
            dgvUsers.ClearSelection();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e) => await LoadUsersAsync();
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();
        private void btnBack_Click(object sender, EventArgs e) { SetNavigating(true); new AdminDashboard(_username, _companyName, _userId, _sessionToken).Show(); Close(); }

        private void InitializeShortcuts()
        {
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape) btnBack_Click(s, e);
                else if (e.KeyCode == Keys.F1) btnAdd_Click(s, e);
                else if (e.KeyCode == Keys.F2) btnUpdate_Click(s, e);
                else if (e.KeyCode == Keys.F3) btnDelete_Click(s, e);
                else if (e.KeyCode == Keys.F4) btnClear_Click(s, e);
                e.Handled = true;
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };
            toolTip.SetToolTip(btnBack, "ESC"); toolTip.SetToolTip(btnAdd, "F1");
            toolTip.SetToolTip(btnUpdate, "F2"); toolTip.SetToolTip(btnDelete, "F3");
            toolTip.SetToolTip(btnClear, "F4");

            ManageUsersHelper.AttachHoverEffect(btnBack, "BACK", "ESC");
            ManageUsersHelper.AttachHoverEffect(btnAdd, "ADD", "F1");
            ManageUsersHelper.AttachHoverEffect(btnUpdate, "EDIT", "F2");
            ManageUsersHelper.AttachHoverEffect(btnDelete, "DELETE", "F3");
            ManageUsersHelper.AttachHoverEffect(btnClear, "CLEAR", "F4");
        }
    }
}