using System;
using System.Windows.Forms;
using Npgsql;

namespace POS.StartUpForms
{
    public partial class OldCompanyFrm : BaseForm
    {
        public OldCompanyFrm()
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            string companyName = txtCompanyName.Text.Trim();

            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Please enter a company name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CompanyExists(companyName))
            {
                MessageBox.Show($"Company '{companyName}' found. Access granted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LogInForm logIn = new LogInForm();
                logIn.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show($"Company '{companyName}' does not exist. Please check the name and try again.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CompanyExists(string companyName)
        {
            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM companies WHERE LOWER(name) = LOWER(@name)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", companyName);
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}