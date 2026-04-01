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

                //Check if company exists
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

                //Check if username exists under that company
                string userSql = @"
            SELECT u.password, u.role, c.name AS company_name
            FROM users u
            JOIN companies c ON u.company_id = c.id
            WHERE u.username = @username
            AND LOWER(c.name) = LOWER(@company)";

                await using var userCmd = new NpgsqlCommand(userSql, conn);
                userCmd.Parameters.AddWithValue("username", username);
                userCmd.Parameters.AddWithValue("company", company);
                await using var userReader = await userCmd.ExecuteReaderAsync();

                if (!await userReader.ReadAsync())
                {
                    MessageBox.Show("Username not found under the specified company.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtUsername.FocusInner();
                    return;
                }

                string storedPassword = userReader["password"].ToString();
                string role = userReader["role"].ToString();
                string companyName = userReader["company_name"].ToString();
                await userReader.CloseAsync();

                //Check if password is correct
                if (storedPassword != password)
                {
                    MessageBox.Show("Incorrect password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.FocusInner();
                    return;
                }

                if (Properties.Settings.Default.RememberUserComp)
                {
                    Properties.Settings.Default.SavedUsername = username;
                    Properties.Settings.Default.SavedCompany = company;
                    Properties.Settings.Default.Save();
                }


                if (role == "ADMIN")
                {
                    AdminDashboard admin = new AdminDashboard(username, companyName);
                    admin.Show();
                    this.Hide();
                }
                else if (role == "CASHIER")
                {
                    CashierDashboard cashier = new CashierDashboard(username, companyName);
                    cashier.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Unknown role. Contact administrator.", "Access Denied",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            if (txtUsername.Text == "")
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
            if (txtPassword.Text == "")
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
            if (txtCompany.Text == "")
            {
                txtCompany.Text = "Company Name";
                txtCompany.InnerForeColor = Color.Gray;
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && sender == txtUsername)
            {
                e.Handled = true;
                this.BeginInvoke((Action)(() =>
                {
                    txtPassword.FocusInner();
                    txtPassword_Enter(txtPassword, EventArgs.Empty);
                }));
            }
            else if (e.KeyCode == Keys.Down && sender == txtPassword)
            {
                e.Handled = true;
                this.BeginInvoke((Action)(() =>
                {
                    txtCompany.FocusInner();
                    txtCompany_Enter(txtCompany, EventArgs.Empty);
                }));
            }
            else if (e.KeyCode == Keys.Up && sender == txtPassword)
            {
                e.Handled = true;
                this.BeginInvoke((Action)(() =>
                {
                    txtUsername.FocusInner();
                    txtUsername_Enter(txtUsername, EventArgs.Empty);
                }));
            }
            else if (e.KeyCode == Keys.Up && sender == txtCompany)
            {
                e.Handled = true;
                this.BeginInvoke((Action)(() =>
                {
                    txtPassword.FocusInner();
                    txtPassword_Enter(txtPassword, EventArgs.Empty);
                }));
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnSignInClick(sender, e);
                e.Handled = true;
            }
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.RememberUserComp)
            {
                chckUserComp.Checked = true;

                this.BeginInvoke((Action)(() =>
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
                }));
            }
        }

        private void chckUserComp_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RememberUserComp = chckUserComp.Checked;

            if (!chckUserComp.Checked)
            {
                Properties.Settings.Default.SavedUsername = string.Empty;
                Properties.Settings.Default.SavedCompany = string.Empty;
            }

            Properties.Settings.Default.Save();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (Properties.Settings.Default.RememberUserComp)
            {
                string currentUsername = txtUsername.Text.Trim();
                string currentCompany = txtCompany.Text.Trim();

                Properties.Settings.Default.SavedUsername =
                    (currentUsername != "Username" && !string.IsNullOrEmpty(currentUsername))
                    ? currentUsername : Properties.Settings.Default.SavedUsername;

                Properties.Settings.Default.SavedCompany =
                    (currentCompany != "Company Name" && !string.IsNullOrEmpty(currentCompany))
                    ? currentCompany : Properties.Settings.Default.SavedCompany;
            }

            Properties.Settings.Default.Save();
        }
    }
}