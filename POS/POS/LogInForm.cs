using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Npgsql;
using Supabase;
using Supabase.Gotrue;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace POS
{
    public partial class LogIn_Form : Form
    {
        private Panel titleBar;
        private Label titleLabel;
        private Button closeButton;
        private PictureBox logoPictureBox;
        private const int TITLE_BAR_HEIGHT = 40;
        private Supabase.Client supabase;
        public LogIn_Form()
        {
            InitializeComponent();
            InitializeSupabase();

            // Remove default title bar
            this.FormBorderStyle = FormBorderStyle.None;

            // Center on screen and focus
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create custom title bar
            CreateCustomTitleBar();
        }

        private async void InitializeSupabase()
        {
            try
            {
                var url = "https://tvmxjtgypuimgbshtpbf.supabase.co"; // Your Supabase URL
                var key = "sb_publishable_Fv1POfF2Fy0X4uAN9ec1mw_ZXlpLm6K"; // Get from Project Settings → API

                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true,
                    AutoRefreshToken = true
                };

                supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize Supabase: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateCustomTitleBar()
        {
            // Title bar panel
            titleBar = new Panel
            {
                BackColor = ColorTranslator.FromHtml("#2C3E50"),
                Dock = DockStyle.Top,
                Height = TITLE_BAR_HEIGHT
            };

            // Logo
            logoPictureBox = new PictureBox
            {
                Size = new Size(30, 30),
                Location = new Point(10, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White // Placeholder - replace with actual logo
            };

            // Title label
            titleLabel = new Label
            {
                Text = "POS System",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 10)
            };

            // Close button
            closeButton = new Button
            {
                Text = "×",
                Size = new Size(40, TITLE_BAR_HEIGHT),
                FlatStyle = FlatStyle.Flat,
                BackColor = ColorTranslator.FromHtml("#2C3E50"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Dock = DockStyle.Right
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            closeButton.Click += CloseButton_Click;

            // Add controls to title bar
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(logoPictureBox);
            titleBar.Controls.Add(titleLabel);

            // Add title bar to form
            this.Controls.Add(titleBar);
            titleBar.BringToFront();

            // Enable dragging
            titleBar.MouseDown += TitleBar_MouseDown;
            titleLabel.MouseDown += TitleBar_MouseDown;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Point mouseOffset;
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(-e.X, -e.Y);
                titleBar.MouseMove += TitleBar_MouseMove;
                titleBar.MouseUp += TitleBar_MouseUp;
            }
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            titleBar.MouseMove -= TitleBar_MouseMove;
            titleBar.MouseUp -= TitleBar_MouseUp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Activate();
            this.Focus();
        }

        private async void btnSignInClick(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                // First check if username exists
                var userCheck = await supabase
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
                        return;
                    }
                    // Username not found - check if it's the default "Username" placeholder
                    if (username == "Username" || string.IsNullOrEmpty(username))
                    {
                        MessageBox.Show("Please enter a username.", "Login Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Check if both username and password are not found in database
                    // Since username doesn't exist, we can assume both are invalid
                    MessageBox.Show("Invalid username & password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Text = "Username";
                    txtUsername.InnerForeColor = Color.Gray;
                    txtPassword.IsPasswordField = false;
                    txtPassword.Text = "Password";
                    txtPassword.InnerForeColor = Color.Gray;
                    return;
                }

                // Username exists, now check password
                var result = await supabase
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
                    // MainForm main = new MainForm();
                    // main.Show();
                    // this.Hide();
                }
                else
                {
                    if (password == "Password" || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Please enter a password.", "Login Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Username exists but password is wrong
                    MessageBox.Show("Invalid password.", "Login Failed",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.IsPasswordField = false;
                    txtPassword.Text = "Password";
                    txtPassword.InnerForeColor = Color.Gray;
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
                txtPassword.IsPasswordField = false; 
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
    }
}