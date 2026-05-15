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
        private readonly string _username, _companyName, _companyId, _userId, _sessionToken;
        private readonly UserService _userService;
        private string _selectedUserId;

        private string GetSelectedRole() => cmbUserLevel.SelectedIndex >= 0 && cmbUserLevel.SelectedIndex < cmbUserLevel.Items.Count ? cmbUserLevel.Items[cmbUserLevel.SelectedIndex].ToString() : "";
        private void SetSelectedRole(string role) { for (int i = 0; i < cmbUserLevel.Items.Count; i++) { if (cmbUserLevel.Items[i].ToString() == role) { cmbUserLevel.SelectedIndex = i; return; } } }

        public ManageUsersFrm(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username; _companyName = companyName; _userId = userId; _sessionToken = sessionToken;
            _companyId = GetCompanyId(companyName);
            _userService = new UserService(_companyId);
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
            catch { return null; }
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
                if (dgvUsers.Columns["ID"] != null) dgvUsers.Columns["ID"].Visible = false;
            }
            catch { MessageBox.Show($"Error loading users", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async Task LoadRolesAsync()
        {
            try
            {
                var roles = await _userService.GetRolesAsync();
                cmbUserLevel.Items.Clear();
                foreach (var role in roles) cmbUserLevel.Items.Add(role);
            }
            catch { MessageBox.Show($"Error loading roles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            if (DateTime.TryParse(row.Cells["Birthdate"].Value?.ToString(), out DateTime bd)) dtpBirthdate.Value = bd;
            SetSelectedRole(row.Cells["User Level"].Value?.ToString());
        }

        private async Task<bool> ConfirmAdminPasswordAsync() => await ManageUsersHelper.ConfirmAdminPasswordAsync(_username, _userService, _companyId);

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (sender == txtSearch) txtUsername.FocusInner();
                else if (sender == txtUsername) txtPassword.FocusInner();
                else if (sender == txtPassword) txtLastName.FocusInner();
                else if (sender == txtLastName) txtFirstName.FocusInner();
                else if (sender == txtFirstName) txtMiddleName.FocusInner();
                else if (sender == txtMiddleName) txtContact.FocusInner();
                else if (sender == txtContact) txtAge.FocusInner();
                
                
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (sender == txtPassword) txtUsername.FocusInner();
                else if (sender == txtLastName) txtPassword.FocusInner();
                else if (sender == txtFirstName) txtLastName.FocusInner();
                else if (sender == txtMiddleName) txtFirstName.FocusInner();
                else if (sender == txtContact) txtMiddleName.FocusInner();
                else if (sender == txtAge) txtContact.FocusInner();
                else if (sender == txtUsername) txtSearch.FocusInner();
                
                e.Handled = true;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string newRole = GetSelectedRole();
            if (newRole == "ADMIN")
            {
                int currentAdminCount = await _userService.GetAdminCountAsync(_companyId);
                if (currentAdminCount >= 2) { MessageBox.Show("❌ Cannot add another admin.\n\nMaximum of 2 admins per company.", "Admin Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            }
            var result = UserValidator.ValidateFields(txtUsername.Text, txtLastName.Text, txtFirstName.Text, newRole, txtAge.Text, txtPassword.Text, true);
            if (!result.IsValid) { MessageBox.Show(result.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (await _userService.UsernameExistsAsync(txtUsername.Text.Trim())) { MessageBox.Show("Username already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && !await ConfirmAdminPasswordAsync()) return;
            await _userService.AddUserAsync(txtUsername.Text.Trim(), txtPassword.Text, newRole, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtMiddleName.Text.Trim(), txtContact.Text.Trim(), int.TryParse(txtAge.Text, out int age) ? age : (int?)null, dtpBirthdate.Value);
            await AuditService.LogInsertAsync(_username, _companyId, "users", txtUsername.Text.Trim(), AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text), ("last_name", txtLastName.Text), ("role", newRole)));
            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadUsersAsync();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUserId)) { MessageBox.Show("Please select a user to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string newRole = GetSelectedRole();
            var result = UserValidator.ValidateFields(txtUsername.Text, txtLastName.Text, txtFirstName.Text, newRole, txtAge.Text);
            if (!result.IsValid) { MessageBox.Show(result.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string oldRole = await _userService.GetUserRoleAsync(_selectedUserId);
            if (newRole == "ADMIN" && oldRole != "ADMIN")
            {
                if (!await _userService.CanChangeToAdminRoleAsync(_companyId, _selectedUserId))
                { MessageBox.Show($"❌ Cannot change role to ADMIN.\n\nMaximum of 2 admins per company.", "Admin Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            }
            if (oldRole == "ADMIN" && newRole != "ADMIN" && await _userService.GetAdminCountAsync(_companyId) <= 1)
            { MessageBox.Show("❌ Cannot change the last admin.\n\nAt least 1 admin required.", "Last Admin Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            bool changePassword = !string.IsNullOrWhiteSpace(txtPassword.Text);
            if (changePassword && !await ConfirmAdminPasswordAsync()) return;
            await _userService.UpdateUserAsync(_selectedUserId, txtUsername.Text.Trim(), txtPassword.Text, newRole, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtMiddleName.Text.Trim(), txtContact.Text.Trim(), int.TryParse(txtAge.Text, out int age) ? age : (int?)null, dtpBirthdate.Value, changePassword);
            await AuditService.LogUpdateAsync(_username, _companyId, "users", _selectedUserId, AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text), ("last_name", txtLastName.Text), ("role", oldRole)), AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text), ("last_name", txtLastName.Text), ("role", newRole)));
            MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadUsersAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUserId)) { MessageBox.Show("Please select a user to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            string roleToDelete = GetSelectedRole();
            if (roleToDelete == "ADMIN" && await _userService.GetAdminCountAsync(_companyId) <= 1)
            { MessageBox.Show("❌ Cannot delete the last admin account.\n\nAt least 1 admin required.", "Last Admin Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (UserValidator.ConfirmDelete(txtUsername.Text) != DialogResult.Yes) return;
            if (!await ConfirmAdminPasswordAsync()) return;
            await _userService.DeleteUserAsync(_selectedUserId);
            await AuditService.LogDeleteAsync(_username, _companyId, "users", _selectedUserId, AuditService.ToJson(("username", txtUsername.Text), ("first_name", txtFirstName.Text), ("last_name", txtLastName.Text), ("role", roleToDelete)));
            MessageBox.Show("User deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadUsersAsync();
        }

        private void ClearFields()
        {
            _selectedUserId = null;
            txtUsername.Text = txtPassword.Text = txtLastName.Text = txtFirstName.Text = txtMiddleName.Text = txtContact.Text = txtAge.Text = "";
            dtpBirthdate.Value = DateTime.Today;
            cmbUserLevel.SelectedIndex = -1;
            dgvUsers.ClearSelection();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e) => await LoadUsersAsync();
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();
        private void btnBack_Click(object sender, EventArgs e) { SetNavigating(true); new AdminDashboard(_username, _companyName, _userId, _sessionToken).Show(); Close(); }

        private void InitializeShortcuts()
        {
            var controls = new Control[] { txtUsername, txtPassword, txtFirstName, txtLastName, txtMiddleName, txtContact, txtAge, dtpBirthdate, cmbUserLevel, txtSearch, btnAdd, btnUpdate, btnDelete, btnClear, btnBack };
            foreach (var control in controls) ShortcutHelper.AttachCustomKeyNavigation(control, txt_KeyDown);
            ShortcutHelper.AttachFunctionShortcuts(this, (s, ev) => btnBack_Click(s, ev), (s, ev) => btnAdd_Click(s, ev), (s, ev) => btnUpdate_Click(s, ev), (s, ev) => btnDelete_Click(s, ev), (s, ev) => btnClear_Click(s, ev));
            ShortcutHelper.SetupTooltips(this, (btnBack, "ESC"), (btnAdd, "F1"), (btnUpdate, "F2"), (btnDelete, "F3"), (btnClear, "F4"));
            ShortcutHelper.AttachHoverEffect(btnBack, "BACK", "ESC"); ShortcutHelper.AttachHoverEffect(btnAdd, "ADD", "F1"); ShortcutHelper.AttachHoverEffect(btnUpdate, "EDIT", "F2"); ShortcutHelper.AttachHoverEffect(btnDelete, "DELETE", "F3"); ShortcutHelper.AttachHoverEffect(btnClear, "CLEAR", "F4");
        }
    }
}