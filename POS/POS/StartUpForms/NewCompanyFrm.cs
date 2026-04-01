using System;
using System.Windows.Forms;
using Npgsql;

namespace POS
{
    public partial class NewCompanyFrm : BaseForm
    {
        public NewCompanyFrm()
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
        }

        private async void btnCreateCompany_Click(object sender, EventArgs e)
        {
            string companyName = txtCompanyName.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Please enter a company name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnCreateCompany.Enabled = false;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // ✅ Check if company exists (case-insensitive)
                string checkCompany = "SELECT COUNT(*) FROM companies WHERE LOWER(name) = LOWER(@name)";
                await using var checkCmd = new NpgsqlCommand(checkCompany, conn);
                checkCmd.Parameters.AddWithValue("name", companyName);

                long count = (long)await checkCmd.ExecuteScalarAsync();

                if (count > 0)
                {
                    MessageBox.Show($"A company named '{companyName}' already exists.\nPlease use a different name.",
                        "Duplicate Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompanyName.Focus();
                    return;
                }

                await using var transaction = await conn.BeginTransactionAsync();

                try
                {
                    // ✅ Step 1: Insert company
                    string insertCompany = @"
                    INSERT INTO companies (name) 
                    VALUES (@name) 
                    RETURNING id";

                    await using var companyCmd = new NpgsqlCommand(insertCompany, conn, transaction);
                    companyCmd.Parameters.AddWithValue("name", companyName);

                    var companyId = (Guid)await companyCmd.ExecuteScalarAsync();

                    // ✅ Step 2: Insert admin with generated password (admin-001, admin-002, ...)
                    string insertAdmin = @"
                    WITH next_num AS (
                        SELECT COUNT(*) + 1 AS num FROM users WHERE username = 'admin'
                    )
                    INSERT INTO users (id, username, password, role, company_id)
                    SELECT 
                        gen_random_uuid(),
                        'admin',
                        'admin-' || LPAD(num::TEXT, 3, '0'),
                        'ADMIN',
                        @companyId
                    FROM next_num
                    RETURNING password";

                    await using var adminCmd = new NpgsqlCommand(insertAdmin, conn, transaction);
                    adminCmd.Parameters.AddWithValue("companyId", companyId);

                    // 🔥 Fetch generated password
                    string generatedPassword = (string)await adminCmd.ExecuteScalarAsync();

                    await transaction.CommitAsync();

                    // ✅ Step 3: Show credentials
                    MessageBox.Show(
                        $"Company '{companyName}' created successfully!\n\n" +
                        $"Admin Credentials:\nUsername: admin\nPassword: {generatedPassword}\n\n" +
                        $"⚠️ Save this password. You won't see it again.",
                        "Company Created",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Redirect to login
                    LogInForm login = new LogInForm();
                    login.Show();
                    this.Hide();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating company:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCreateCompany.Enabled = true;
            }
        }
    }
}