using DnsClient;
using Npgsql;
using POS.StartUpForms;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class NewCompanyFrm : BaseForm
    {
        public NewCompanyFrm()
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            AttachKeyHandlers();
        }

        private void AttachKeyHandlers()
        {
            // Attach KeyDown to all textboxes
            Control[] fields = { txtCompanyName, txtEmailAdd, txtContactNum };

            foreach (var field in fields)
            {
                field.KeyDown += Fields_KeyDown;
                field.KeyDown += GlobalKeys_KeyDown;
            }
        }

        private void Fields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (sender == txtCompanyName) txtEmailAdd.FocusInner();
                else if (sender == txtEmailAdd) txtContactNum.FocusInner();
                else if (sender == txtContactNum) btnCreateCompany_Click(sender, e); // Enter on last field = submit

                if (e.KeyCode == Keys.Down) e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (sender == txtEmailAdd) txtCompanyName.FocusInner();
                else if (sender == txtContactNum) txtEmailAdd.FocusInner();
                e.Handled = true;
            }
        }

        private void GlobalKeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
                new WelcomeFrm().Show();
                e.Handled = true;
            }
        }

        // Override ProcessCmdKey to catch Enter on the last field and ESC globally
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Hide();
                new WelcomeFrm().Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e) { }

        private async void btnCreateCompany_Click(object sender, EventArgs e)
        {
            string companyName = txtCompanyName.Text.Trim();
            string emailAddress = txtEmailAdd.Text.Trim();
            string contactNumber = txtContactNum.Text.Trim();

            // ── Step 1: Basic field validation ──────────────────────────────────────
            if (string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Please enter a company name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(emailAddress))
            {
                if (!IsValidEmail(emailAddress))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmailAdd.Focus();
                    return;
                }

                btnCreateCompany.Enabled = false;
                btnCreateCompany.Text = "Validating...";

                bool emailValid = await Task.Run(() => HasValidMxRecord(emailAddress.Split('@')[1]));

                btnCreateCompany.Enabled = true;
                btnCreateCompany.Text = "Create Company";

                if (!emailValid)
                {
                    MessageBox.Show("The email domain does not exist or cannot receive emails.\nPlease use a real email address.",
                        "Invalid Email Domain", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmailAdd.Focus();
                    return;
                }
            }

            if (!string.IsNullOrEmpty(contactNumber) && !IsValidPhoneNumber(contactNumber))
            {
                MessageBox.Show("Please enter a valid contact number (7-15 digits only).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNum.Focus();
                return;
            }

            // ── Step 2: DB duplicate checks BEFORE disabling button ─────────────────
            try
            {
                await using var connCheck = DatabaseService.GetConnection();
                await connCheck.OpenAsync();

                // Duplicate company name
                await using var checkCmd = new NpgsqlCommand(
                    @"SELECT COUNT(*) FROM companies 
              WHERE LOWER(name) = LOWER(@name) OR
                    LOWER(@name) LIKE '%' || LOWER(name) || '%' OR
                    LOWER(name) LIKE '%' || LOWER(@name) || '%' OR
                    similarity(LOWER(name), LOWER(@name)) > 0.6", connCheck);
                checkCmd.Parameters.AddWithValue("name", companyName);
                if ((long)await checkCmd.ExecuteScalarAsync() > 0)
                {
                    MessageBox.Show($"A company with a similar name to '{companyName}' already exists.\nPlease choose a different name.",
                        "Duplicate Company", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCompanyName.Focus();
                    return;
                }

                // Duplicate email
                if (!string.IsNullOrEmpty(emailAddress))
                {
                    await using var emailCheckCmd = new NpgsqlCommand(
                        "SELECT COUNT(*) FROM companies WHERE LOWER(address) = LOWER(@email)", connCheck);
                    emailCheckCmd.Parameters.AddWithValue("email", emailAddress);
                    if ((long)await emailCheckCmd.ExecuteScalarAsync() > 0)
                    {
                        MessageBox.Show($"The email '{emailAddress}' is already registered to another company.\nPlease use a different email address.",
                            "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmailAdd.Focus();
                        return;
                    }
                }

                // Duplicate contact number
                if (!string.IsNullOrEmpty(contactNumber))
                {
                    string cleanedInput = contactNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
                    await using var phoneCheckCmd = new NpgsqlCommand(
                        @"SELECT COUNT(*) FROM companies 
                  WHERE REGEXP_REPLACE(contact_number, '[^0-9]', '', 'g') = @contact", connCheck);
                    phoneCheckCmd.Parameters.AddWithValue("contact", cleanedInput);
                    if ((long)await phoneCheckCmd.ExecuteScalarAsync() > 0)
                    {
                        MessageBox.Show($"The contact number '{contactNumber}' is already registered to another company.\nPlease use a different contact number.",
                            "Duplicate Contact Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtContactNum.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking duplicates:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ── Step 3: All checks passed — proceed with creation ───────────────────
            try
            {
                btnCreateCompany.Enabled = false;
                btnCreateCompany.Text = "Creating...";

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

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
                    string insertCompany = @"
                INSERT INTO companies (name, address, contact_number) 
                VALUES (@name, @email, @contact) 
                RETURNING id";

                    await using var companyCmd = new NpgsqlCommand(insertCompany, conn, transaction);
                    companyCmd.Parameters.AddWithValue("name", companyName);
                    companyCmd.Parameters.AddWithValue("email", string.IsNullOrEmpty(emailAddress) ? DBNull.Value : (object)emailAddress);
                    companyCmd.Parameters.AddWithValue("contact", string.IsNullOrEmpty(contactNumber) ? DBNull.Value : (object)contactNumber);
                    var companyId = (Guid)await companyCmd.ExecuteScalarAsync();

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

                    if (passwordObj == null)
                    {
                        await transaction.RollbackAsync();
                        MessageBox.Show("Failed to create admin user. Please try again.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string generatedPassword = passwordObj.ToString();
                    await transaction.CommitAsync();

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
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    new LogInForm().Show();
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
                btnCreateCompany.Text = "Create Company";
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email) return false;
                string domain = email.Split('@')[1];
                return HasValidMxRecord(domain);
            }
            catch { return false; }
        }

        private bool HasValidMxRecord(string domain)
        {
            try
            {
                var lookup = new LookupClient();
                var result = lookup.Query(domain, QueryType.MX);
                return result.Answers.MxRecords().Any();
            }
            catch { return false; }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string cleaned = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            return System.Text.RegularExpressions.Regex.IsMatch(cleaned, @"^\d{7,15}$");
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new WelcomeFrm().Show();
        }
    }
}