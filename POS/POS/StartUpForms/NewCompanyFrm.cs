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
            string emailAddress = txtEmailAdd.Text.Trim();
            string contactNumber = txtContactNum.Text.Trim();

            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Please enter a company name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Optional: Add email validation if needed
            if (!string.IsNullOrEmpty(emailAddress) && !IsValidEmail(emailAddress))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailAdd.Focus();
                return;
            }

            // Optional: Add contact number validation if needed
            if (!string.IsNullOrEmpty(contactNumber) && !IsValidPhoneNumber(contactNumber))
            {
                MessageBox.Show("Please enter a valid contact number (7-15 digits only).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNum.Focus();
                return;
            }

            try
            {
                btnCreateCompany.Enabled = false;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Check if company already exists
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

                // FIX: Verify the ADMIN role exists BEFORE starting the transaction
                string checkRole = "SELECT id FROM roles WHERE LOWER(name) = 'admin'";
                await using var checkRoleCmd = new NpgsqlCommand(checkRole, conn);
                var roleIdObj = await checkRoleCmd.ExecuteScalarAsync();

                if (roleIdObj == null)
                {
                    MessageBox.Show(
                        "The ADMIN role does not exist in the database.\n\nPlease run:\nINSERT INTO roles (name) VALUES ('ADMIN');",
                        "Missing Role", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid adminRoleId = (Guid)roleIdObj;

                await using var transaction = await conn.BeginTransactionAsync();

                try
                {
                    // Insert company with email and contact number
                    // Updated to match your database schema: email_address and contact_number
                    string insertCompany = @"
                        INSERT INTO companies (name, address, contact_number) 
                        VALUES (@name, @email, @contact) 
                        RETURNING id";

                    await using var companyCmd = new NpgsqlCommand(insertCompany, conn, transaction);
                    companyCmd.Parameters.AddWithValue("name", companyName);
                    companyCmd.Parameters.AddWithValue("email", string.IsNullOrEmpty(emailAddress) ? DBNull.Value : (object)emailAddress);
                    companyCmd.Parameters.AddWithValue("contact", string.IsNullOrEmpty(contactNumber) ? DBNull.Value : (object)contactNumber);
                    var companyId = (Guid)await companyCmd.ExecuteScalarAsync();

                    // FIX: Use the pre-fetched roleId directly instead of a subquery
                    // that could silently return NULL inside the INSERT
                    string insertAdmin = @"
                        WITH next_num AS (
                            SELECT COUNT(*) + 1 AS num FROM users WHERE username = 'admin'
                        )
                        INSERT INTO users (id, username, password, role_id, company_id)
                        SELECT 
                            gen_random_uuid(),
                            'admin',
                            'admin-' || LPAD(num::TEXT, 3, '0'),
                            @roleId,
                            @companyId
                        FROM next_num
                        RETURNING password";

                    await using var adminCmd = new NpgsqlCommand(insertAdmin, conn, transaction);
                    adminCmd.Parameters.AddWithValue("roleId", adminRoleId);
                    adminCmd.Parameters.AddWithValue("companyId", companyId);

                    var passwordObj = await adminCmd.ExecuteScalarAsync();

                    // FIX: Check that the user was actually inserted
                    if (passwordObj == null)
                    {
                        await transaction.RollbackAsync();
                        MessageBox.Show("Failed to create admin user. Please try again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string generatedPassword = passwordObj.ToString();

                    await transaction.CommitAsync();

                    // Build success message
                    string successMessage = $"Company '{companyName}' created successfully!\n\n" +
                        $"Admin Credentials:\nUsername: admin\nPassword: {generatedPassword}\n\n" +
                        $"⚠️ Save this password. You won't see it again.\n\n";

                    if (!string.IsNullOrEmpty(emailAddress) || !string.IsNullOrEmpty(contactNumber))
                    {
                        successMessage += "Company Contact Information:\n";
                        if (!string.IsNullOrEmpty(emailAddress))
                            successMessage += $"Email: {emailAddress}\n";
                        if (!string.IsNullOrEmpty(contactNumber))
                            successMessage += $"Contact Number: {contactNumber}\n";
                    }

                    MessageBox.Show(successMessage, "Company Created",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

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

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Helper method to validate phone number
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Remove common separators and spaces
            string cleaned = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            // Check if it contains only digits and is between 7-15 digits long
            return System.Text.RegularExpressions.Regex.IsMatch(cleaned, @"^\d{7,15}$");
        }
    }
}