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

    }
}