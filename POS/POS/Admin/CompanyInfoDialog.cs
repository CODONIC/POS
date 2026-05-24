using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace POS.Admin
{
    public class CompanyInfoDialog : Form
    {
        private TextBox txtCompanyName;
        private TextBox txtContactNumber;
        private TextBox txtPassword;
        private Button btnSave;
        private Button btnCancel;
        private Label lblStatus;

        private readonly string _userId;

        public CompanyInfoDialog(string userId, string currentCompanyName, string currentContactNumber)
        {
            _userId = userId;
            InitializeComponents();
            txtCompanyName.Text = currentCompanyName;
            txtContactNumber.Text = currentContactNumber;
        }

        private void InitializeComponents()
        {
            this.Text = "Company Info";
            this.Size = new Size(420, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(236, 240, 245);

            // Company Name
            var lblCompanyName = new Label
            {
                Text = "Company Name:",
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 9f)
            };
            txtCompanyName = new TextBox
            {
                Location = new Point(160, 27),
                Width = 210,
                Font = new Font("Segoe UI", 9f)
            };

            // Contact Number
            var lblContactNumber = new Label
            {
                Text = "Contact Number:",
                Location = new Point(20, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 9f)
            };
            txtContactNumber = new TextBox
            {
                Location = new Point(160, 67),
                Width = 210,
                Font = new Font("Segoe UI", 9f)
            };

            // Admin Password
            var lblPassword = new Label
            {
                Text = "Admin Password:",
                Location = new Point(20, 110),
                AutoSize = true,
                Font = new Font("Segoe UI", 9f)
            };
            txtPassword = new TextBox
            {
                Location = new Point(160, 107),
                Width = 210,
                PasswordChar = '*',
                Font = new Font("Segoe UI", 9f)
            };

            // Status label
            lblStatus = new Label
            {
                Location = new Point(20, 145),
                Size = new Size(370, 20),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.Red,
                Text = ""
            };

            // Buttons
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(160, 175),
                Size = new Size(90, 32),
                BackColor = Color.FromArgb(78, 115, 163),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(270, 175),
                Size = new Size(90, 32),
                BackColor = Color.FromArgb(180, 190, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[]
            {
                lblCompanyName, txtCompanyName,
                lblContactNumber, txtContactNumber,
                lblPassword, txtPassword,
                lblStatus, btnSave, btnCancel
            });
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                string.IsNullOrWhiteSpace(txtContactNumber.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblStatus.Text = "Please fill in all fields.";
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Saving...";

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Verify admin password and get company_id
                await using var authCmd = new NpgsqlCommand(
                    "SELECT company_id FROM users WHERE id = @id AND password = @password", conn);
                authCmd.Parameters.AddWithValue("id", Guid.Parse(_userId));
                authCmd.Parameters.AddWithValue("password", txtPassword.Text);

                var companyId = await authCmd.ExecuteScalarAsync();

                if (companyId == null || companyId == DBNull.Value)
                {
                    lblStatus.Text = "Incorrect admin password.";
                    return;
                }

                // Update company info
                await using var updateCmd = new NpgsqlCommand(
                    "UPDATE companies SET name = @name, contact_number = @contact WHERE id = @id", conn);
                updateCmd.Parameters.AddWithValue("name", txtCompanyName.Text.Trim());
                updateCmd.Parameters.AddWithValue("contact", txtContactNumber.Text.Trim());
                updateCmd.Parameters.AddWithValue("id", (Guid)companyId);
                await updateCmd.ExecuteNonQueryAsync();

                MessageBox.Show("Company info updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = "Save";
            }
        }
    }
}
