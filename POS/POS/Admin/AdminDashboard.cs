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

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirm == DialogResult.Yes)
            {
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
            AttachHoverEffect(btnLogOut);
            AttachHoverEffect(btnManageUsers);
            AttachHoverEffect(btnManageCategory);
            AttachHoverEffect(btnManageProducts);
            AttachHoverEffect(btnManageStocks);
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

    }
}