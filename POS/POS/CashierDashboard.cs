using Npgsql;
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
    public partial class CashierDashboard : BaseForm
    {
        private string _username;
        public CashierDashboard(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            lblCashierName.Text = $"{_username} | Cashier";
        }
        private async void CashierDashboard_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                string sql = "SELECT product_code, product_name, price, quantity FROM products ORDER BY product_code";

                await using var cmd = new NpgsqlCommand(sql, conn);
                await using var reader = await cmd.ExecuteReaderAsync();

                // Load data into a DataTable
                var table = new DataTable();
                table.Load(reader);

                // Bind to DataGridView (must run on UI thread)
                dgvProducts.Invoke((Action)(() =>
                {
                    dgvProducts.DataSource = table;

                    // Rename column headers
                    dgvProducts.Columns["product_code"].HeaderText = "Product Code";
                    dgvProducts.Columns["product_name"].HeaderText = "Product Name";
                    dgvProducts.Columns["price"].HeaderText = "Price";
                    dgvProducts.Columns["quantity"].HeaderText = "Quantity";
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        
    }
}
