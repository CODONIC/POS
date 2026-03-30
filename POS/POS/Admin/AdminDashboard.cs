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
        public AdminDashboard()
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
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
                this.Close();
            }
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            ManageUsersFrm users = new ManageUsersFrm();
            users.Show();
            this.Close();
        }

        private void btnManageCategory_Click(object sender, EventArgs e)
        {
            ProdCategoryFrm categories = new ProdCategoryFrm();
            categories.Show();
            this.Close();
        }

        private void btnManageProd_Click(object sender, EventArgs e)
        {
            ManageProdFrm prod = new ManageProdFrm();
            prod.Show();
            this.Close();
        }

        private void btnManageStocks_Click(object sender, EventArgs e)
        {
            ManageStocks stocks = new ManageStocks();
            stocks.Show();
            this.Close();
        }
    }
}
