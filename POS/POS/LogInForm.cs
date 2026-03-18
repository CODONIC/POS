using Supabase;
using System;
namespace POS
{
    public partial class LogIn_Form : BaseForm
    {
        public LogIn_Form()
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
        }

        private async void btnSignInClick(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                var userCheck = await SupabaseService.Client
                    .From<Users>()
                    .Select("*")
                    .Where(u => u.Username == username)
                    .Get();

                if (userCheck.Models.Count == 0)
                {
                    if ((username == "Username" || string.IsNullOrEmpty(username)) &&
                        (password == "Password" || string.IsNullOrEmpty(password)))
                    {
                        MessageBox.Show("Please enter username and password.", "Login Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.FocusInner();
                        return;
                    }
                    if (username == "Username" || string.IsNullOrEmpty(username))
                    {
                        MessageBox.Show("Please enter a username.", "Login Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.FocusInner();
                        return;
                    }
                    if (password == "Password" || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Please enter a password.", "Login Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.FocusInner();
                        return;
                    }

                    MessageBox.Show("Invalid username or password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.FocusInner();
                    return;
                }

                // Username exists, now check password
                var result = await SupabaseService.Client
                    .From<Users>()
                    .Select("*")
                    .Where(u => u.Username == username && u.Password == password)
                    .Get();

                if (result.Models.Count > 0)
                {
                    // Successful login
                    var user = result.Models.First();
                    MessageBox.Show("Login Successful!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // open main POS form
                     MainDashboard main = new MainDashboard();
                     main.Show();
                     this.Hide();
                }
                else
                {
                    if (password == "Password" || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Please enter a password.", "Login Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPassword.FocusInner();
                        return;
                    }
                    MessageBox.Show("Invalid password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.FocusInner();
                    return;
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
            if(txtPassword.Text == "")
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
    }
}