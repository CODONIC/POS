using System;
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

            // Basic validation for empty fields
            if (string.IsNullOrEmpty(username) || username == "Username" && string.IsNullOrEmpty(password) || password == "Password")
            {
                MessageBox.Show("Please enter a username and password.", "Login Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
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



            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Check if username exists
                string checkUserSql = "SELECT COUNT(*) FROM users WHERE username = @username";
                await using var checkCmd = new NpgsqlCommand(checkUserSql, conn);
                checkCmd.Parameters.AddWithValue("username", username);
                long userCount = (long)(await checkCmd.ExecuteScalarAsync() ?? 0L);

                if (userCount == 0)
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.FocusInner();
                    return;
                }

                // Check username + password and get role
                string loginSql = "SELECT role FROM users WHERE username = @username AND password = @password";
                await using var loginCmd = new NpgsqlCommand(loginSql, conn);
                loginCmd.Parameters.AddWithValue("username", username);
                loginCmd.Parameters.AddWithValue("password", password);

                var roleResult = await loginCmd.ExecuteScalarAsync();

                if (roleResult == null)
                {
                    MessageBox.Show("Invalid password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.FocusInner();
                    return;
                }

                string role = roleResult.ToString();

                if (role == "ADMIN")
                {
                    AdminDashboard admin = new AdminDashboard();
                    admin.Show();
                    this.Hide();
                }
                else if (role == "CASHIER")
                {
                    CashierDashboard cashier = new CashierDashboard();
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
            else if (e.KeyCode == Keys.Up && sender == txtPassword)
            {
                e.Handled = true;
                this.BeginInvoke((Action)(() =>
                {
                    txtUsername.FocusInner();
                    txtUsername_Enter(txtUsername, EventArgs.Empty);
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

        }
    }
}