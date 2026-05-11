using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class ManageUsersHelper
    {
        // ─── Password Confirmation Dialog ────────────────────────────────────────
        public static async Task<bool> ConfirmAdminPasswordAsync(string username, UserService userService, string companyId)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Admin Password Confirmation";
                dialog.Size = new Size(450, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.BackColor = Color.White;
                dialog.Font = new Font("Segoe UI", 10);

                // Create TableLayoutPanel for symmetry
                var layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 3,
                    RowCount = 5,
                    Padding = new Padding(20)
                };

                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));

                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));

                // Message Label
                var lblMessage = new Label
                {
                    Text = "Confirm your password to continue:",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 11)
                };
                layout.Controls.Add(lblMessage, 1, 1);

                // Admin Label
                var lblAdmin = new Label
                {
                    Text = $"Admin: {username}",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(59, 130, 246)
                };
                layout.Controls.Add(lblAdmin, 1, 2);

                // Password TextBox
                var txtAdminPassword = new TextBox
                {
                    Location = new Point(20, 80),
                    Size = new Size(250, 30),
                    PasswordChar = '●',
                    UseSystemPasswordChar = true,
                    TextAlign = HorizontalAlignment.Center,
                    Font = new Font("Segoe UI", 11)
                };
                layout.Controls.Add(txtAdminPassword, 1, 3);

                // Button Panel
                var buttonPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Height = 45
                };

                var btnConfirm = new Button
                {
                    Text = "Confirm",
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(59, 130, 246),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    DialogResult = DialogResult.OK,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                btnConfirm.FlatAppearance.BorderSize = 0;

                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Size = new Size(100, 35),
                    BackColor = Color.FromArgb(156, 163, 175),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    DialogResult = DialogResult.Cancel,
                    Margin = new Padding(20, 0, 0, 0),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                btnConfirm.Location = new Point(
                (buttonPanel.Width / 2) - btnConfirm.Width - 10,
                (buttonPanel.Height - btnConfirm.Height) / 2
                 );

                // Position cancel button in center-right
                btnCancel.Location = new Point(
                    (buttonPanel.Width / 2) + 10,
                    (buttonPanel.Height - btnCancel.Height) / 2
                );
                btnCancel.FlatAppearance.BorderSize = 0;

                buttonPanel.Controls.Add(btnConfirm);
                buttonPanel.Controls.Add(btnCancel);
                buttonPanel.Controls.Add(new Label { Width = 0 }); // Spacer
                buttonPanel.Resize += (s, e) =>
                {
                    btnConfirm.Location = new Point(
                        (buttonPanel.Width / 2) - btnConfirm.Width - 10,
                        (buttonPanel.Height - btnConfirm.Height) / 2
                    );
                    btnCancel.Location = new Point(
                        (buttonPanel.Width / 2) + 10,
                        (buttonPanel.Height - btnCancel.Height) / 2
                    );
                };
                layout.Controls.Add(buttonPanel, 1, 4);

                dialog.Controls.Add(layout);
                dialog.AcceptButton = btnConfirm;
                dialog.CancelButton = btnCancel;

                // Center the password text horizontally
                txtAdminPassword.Anchor = AnchorStyles.None;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string enteredPassword = txtAdminPassword.Text;
                    var result = await userService.VerifyAdminPasswordAsync(username, enteredPassword, companyId);
                    if (!result)
                    {
                        MessageBox.Show("Incorrect admin password. Operation cancelled.", "Authentication Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }

        public static void AttachHoverEffect(Button btn, string defaultText, string shortcut)
        {
            Point originalLocation = btn.Location;
            btn.MouseEnter += (s, e) => { btn.Text = $"{defaultText}\n({shortcut})"; btn.Location = new Point(originalLocation.X, originalLocation.Y - 3); };
            btn.MouseLeave += (s, e) => { btn.Text = defaultText; btn.Location = originalLocation; };
        }
    }
}