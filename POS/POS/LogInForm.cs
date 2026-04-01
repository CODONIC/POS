using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace POS
{
    public partial class LogInForm : BaseForm
    {
        public LogInForm()
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
        }

        private async void btnSignInClick(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string company = txtCompany.Text.Trim();

            //Save immediately if checkbox is checked (even before validation)
            if (chckUserComp.Checked)
            {
                SaveUserCompany(username, company);
            }

            if (string.IsNullOrEmpty(username) || username == "Username" && string.IsNullOrEmpty(password) || password == "Password" && string.IsNullOrEmpty(company) || company == "Company Name")
            {
                MessageBox.Show("Please fill all of the text boxes.", "Login Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.FocusInner();
                return;
            }

            if (string.IsNullOrEmpty(username) || username == "Username")
            {
                MessageBox.Show("Please enter a username.", "Login Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.FocusInner();
                return;
            }

            if (string.IsNullOrEmpty(password) || password == "Password")
            {
                MessageBox.Show("Please enter a password.", "Login Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.FocusInner();
                return;
            }

            if (string.IsNullOrEmpty(company) || company == "Company Name")
            {
                MessageBox.Show("Please enter a company name.", "Login Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCompany.FocusInner();
                return;
            }

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Check company
                string companySql = "SELECT id FROM companies WHERE LOWER(name) = LOWER(@company)";
                await using var companyCmd = new NpgsqlCommand(companySql, conn);
                companyCmd.Parameters.AddWithValue("company", company);
                var companyId = await companyCmd.ExecuteScalarAsync();

                if (companyId == null)
                {
                    MessageBox.Show("Company name not found.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCompany.Clear();
                    txtCompany.FocusInner();
                    return;
                }

                // Check user
                string userSql = @"
    SELECT u.password, r.name AS role, c.name AS company_name
    FROM users u
    JOIN companies c ON u.company_id = c.id
    JOIN roles r ON u.role_id = r.id
    WHERE u.username = @username
    AND LOWER(c.name) = LOWER(@company)";

                await using var userCmd = new NpgsqlCommand(userSql, conn);
                userCmd.Parameters.AddWithValue("username", username);
                userCmd.Parameters.AddWithValue("company", company);

                await using var reader = await userCmd.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                {
                    MessageBox.Show("Username not found under the specified company.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtUsername.FocusInner();
                    return;
                }

                string storedPassword = reader["password"].ToString();
                string role = reader["role"].ToString();
                string companyName = reader["company_name"].ToString();
                await reader.CloseAsync();

                if (storedPassword != password)
                {
                    MessageBox.Show("Incorrect password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.FocusInner();
                    return;
                }

                //Save again on success (clean values)
                if (chckUserComp.Checked)
                {
                    SaveUserCompany(username, company);
                }

                if (role == "ADMIN")
                {
                    new AdminDashboard(username, companyName).Show();
                    this.Hide();
                }
                else if (role == "CASHIER")
                {
                    new CashierDashboard(username, companyName).Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Unknown role.", "Access Denied",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Centralized save method
        private void SaveUserCompany(string username, string company)
        {
            if (username != "Username" && !string.IsNullOrEmpty(username))
                Properties.Settings.Default.SavedUsername = username;

            if (company != "Company Name" && !string.IsNullOrEmpty(company))
                Properties.Settings.Default.SavedCompany = company;

            Properties.Settings.Default.RememberUserComp = true;
            Properties.Settings.Default.Save();
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            bool remember = Properties.Settings.Default.RememberUserComp;
            chckUserComp.Checked = remember; // Only trust the bool flag

            if (remember)
            {
                
                string savedUsername = Properties.Settings.Default.SavedUsername;
                string savedCompany = Properties.Settings.Default.SavedCompany;

                if (!string.IsNullOrEmpty(savedUsername))
                {
                    txtUsername.Text = savedUsername;
                    txtUsername.InnerForeColor = Color.Black;
                }

                if (!string.IsNullOrEmpty(savedCompany))
                {
                    txtCompany.Text = savedCompany;
                    txtCompany.InnerForeColor = Color.Black;
                }
                
            }
        }
        private void LogInForm_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberUserComp)
            {
                txtPassword.FocusInner();
            }
        }

        private void chckUserComp_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RememberUserComp = chckUserComp.Checked;

            if (chckUserComp.Checked)
            {
                SaveUserCompany(txtUsername.Text.Trim(), txtCompany.Text.Trim());
            }
            else
            {
                Properties.Settings.Default.SavedUsername = string.Empty;
                Properties.Settings.Default.SavedCompany = string.Empty;
                Properties.Settings.Default.Save();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (chckUserComp.Checked)
            {
                SaveUserCompany(txtUsername.Text.Trim(), txtCompany.Text.Trim());
                
            }
            else
            {
                Properties.Settings.Default.SavedUsername = string.Empty;
                Properties.Settings.Default.SavedCompany = string.Empty;
                Properties.Settings.Default.RememberUserComp = false;
                Properties.Settings.Default.Save(); // <-- this is the critical line
            }
        }

        // ---------------- UI EVENTS ----------------

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.InnerForeColor = Color.Black;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                txtUsername.Text = "Username";
                txtUsername.InnerForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.InnerForeColor = Color.Black;
                txtPassword.IsPasswordField = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Text = "Password";
                txtPassword.InnerForeColor = Color.Gray;
                txtPassword.IsPasswordField = false;
            }
        }

        private void txtCompany_Enter(object sender, EventArgs e)
        {
            if (txtCompany.Text == "Company Name")
            {
                txtCompany.Text = "";
                txtCompany.InnerForeColor = Color.Black;
            }
        }

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                txtCompany.Text = "Company Name";
                txtCompany.InnerForeColor = Color.Gray;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (sender == txtUsername)
                {
                    txtPassword.FocusInner();
                }
                else if (sender == txtPassword)
                {
                    txtCompany.FocusInner();
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (sender == txtCompany)
                {
                    txtPassword.FocusInner();
                }
                else if (sender == txtPassword)
                {
                    txtUsername.FocusInner();
                }

                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSignInClick(sender, e);
                e.Handled = true;
            }
        }

    }
}