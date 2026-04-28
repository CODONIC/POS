using Npgsql;
using POS.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class AdminDashboard : BaseForm
    {
        private string _username;
        private string _companyName;

        public AdminDashboard(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            this.KeyPreview = true;
            this.KeyDown += AdminDashboard_KeyDown;
            ShortcutKeyHints();


        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // Resolve company ID from name then log the logout
                try
                {
                    await using var conn = DatabaseService.GetConnection();
                    await conn.OpenAsync();

                    const string sql = "SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1";
                    await using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", _companyName);

                    var result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        await AuditService.LogLogoutAsync(
                            username: _username,
                            companyId: result.ToString(),
                            deviceInfo: Environment.MachineName
                        );
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Logout audit failed: {ex.Message}");
                }

                LogInForm login = new LogInForm();
                login.Show();
                this.Hide();
            }
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            ManageUsersFrm users = new ManageUsersFrm(_username, _companyName);
            users.Show();
            this.Hide();
        }

        private void btnManageCategory_Click(object sender, EventArgs e)
        {
            ProdCategoryFrm categories = new ProdCategoryFrm(_username, _companyName);
            categories.Show();
            this.Hide();
        }

        private void btnManageProducts_Click(object sender, EventArgs e)
        {
            ManageProdFrm prod = new ManageProdFrm(_username, _companyName);
            prod.Show();
            this.Hide();
        }

        private void btnManageStocks_Click(object sender, EventArgs e)
        {
            ManageStocks stocks = new ManageStocks(_username, _companyName);
            stocks.Show();
            this.Hide();
        }



        private void btnTransactions_Click(object sender, EventArgs e)
        {
            TransactionsForm trans = new TransactionsForm(_username, _companyName);
            trans.Show();
            this.Hide();
        }

        private void btnBusinessStats_Click(object sender, EventArgs e)
        {
            BusinessStatsForm businessStats = new BusinessStatsForm(_username, _companyName);
            businessStats.Show();
            this.Hide();
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            //Generate pdf report
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(_username, _companyName);
            settingsForm.Show();

        }


        // ─── Shortcut Keys ────────────────────────────────────────────────────────────

        private void AdminDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btnLogOut_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F1:
                    btnManageUsers_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F2:
                    btnManageCategory_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F3:
                    btnManageProducts_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F4:
                    btnManageStocks_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F5:
                    btnTransactions_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F6:
                    btnBusinessStats_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F7:
                    btnSalesReport_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F8:
                    btnSettings_Click(sender, e);
                    e.Handled = true;
                    break;

            }
        }


        private void ShortcutKeyHints()
        {
            //Shortcut keys:

            ToolTip toolTip = new ToolTip();
            toolTip.InitialDelay = 200; // ms before tooltip appears
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(btnLogOut, "ESC");
            toolTip.SetToolTip(btnManageUsers, "F1");
            toolTip.SetToolTip(btnManageCategory, "F2");
            toolTip.SetToolTip(btnManageProducts, "F3");
            toolTip.SetToolTip(btnManageStocks, "F4");
            toolTip.SetToolTip(btnTransactions, "F5");
            toolTip.SetToolTip(btnBusinessStats, "F6");
            toolTip.SetToolTip(btnSalesReport, "F7");
            toolTip.SetToolTip(btnSettings, "F8");
            AttachHoverEffect(btnLogOut);
            AttachHoverEffect(btnManageUsers);
            AttachHoverEffect(btnManageCategory);
            AttachHoverEffect(btnManageProducts);
            AttachHoverEffect(btnManageStocks);
            AttachHoverEffect(btnTransactions);
            AttachHoverEffect(btnBusinessStats);
            AttachHoverEffect(btnSalesReport);
            AttachHoverEffect(btnSettings);
        }
        private void AttachHoverEffect(Button btn)
        {
            Point originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
                btn.Padding = new Padding(0, 0, 0, 6); // push text up
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Location = originalLocation;
                btn.Padding = new Padding(0); // reset
            };
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            EmployeeLogsFrm audit = new EmployeeLogsFrm(_username, _companyName);
            audit.Show();
            this.Hide();
        }
    }
}