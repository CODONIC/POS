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
            //Shortcut keys HINTS:
            btnLogOut.MouseEnter += (s, e) => btnLogOut.Text = "Logout (ESC)";
            btnLogOut.MouseLeave += (s, e) => btnLogOut.Text = "Logout";

            btnManageUsers.MouseEnter += (s, e) => btnManageUsers.Text = "Manage Users (F1)";
            btnManageUsers.MouseLeave += (s, e) => btnManageUsers.Text = "Manage Users";

            btnManageCategory.MouseEnter += (s, e) => btnManageCategory.Text = "Manage Category (F2)";
            btnManageCategory.MouseLeave += (s, e) => btnManageCategory.Text = "Manage Category";

            btnManageProducts.MouseEnter += (s, e) => btnManageProducts.Text = "Manage Products (F3)";
            btnManageProducts.MouseLeave += (s, e) => btnManageProducts.Text = "Manage Products";

            btnManageStocks.MouseEnter += (s, e) => btnManageStocks.Text = "Manage Stocks (F4)";
            btnManageStocks.MouseLeave += (s, e) => btnManageStocks.Text = "Manage Stocks";
        }

    }
}