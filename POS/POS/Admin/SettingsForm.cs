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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace POS.Admin
{
    public partial class SettingsForm : BaseForm
    {
        private string _username;
        private string _companyName;
        private readonly string _userId;
        private readonly string _sessionToken;

        protected override bool RequireExitConfirmation => false;

        public SettingsForm(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            _userId = userId;
            _sessionToken = sessionToken;
            SetUserContext(_username, _userId, _sessionToken);
        }

        public override void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override Task PerformLogoutAsync()
        {
            return Task.CompletedTask;
        }

        // ── Company Info ────────────────────────────────────────────────────────
        private async void btnCompanyInfo_Click(object sender, EventArgs e)
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(
                    @"SELECT c.name, c.contact_number
                      FROM companies c
                      INNER JOIN users u ON u.company_id = c.id
                      WHERE u.id = @userId", conn);
                cmd.Parameters.AddWithValue("userId", Guid.Parse(_userId));

                await using var reader = await cmd.ExecuteReaderAsync();

                string currentName = "";
                string currentContact = "";

                if (await reader.ReadAsync())
                {
                    currentName = reader.IsDBNull(0) ? "" : reader.GetString(0);
                    currentContact = reader.IsDBNull(1) ? "" : reader.GetString(1);
                }

                var dialog = new CompanyInfoDialog(_userId, currentName, currentContact);
                var result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    await RefreshCompanyNameAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load company info: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Change VAT ──────────────────────────────────────────────────────────
        private async void btnChangeVAT_Click(object sender, EventArgs e)
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(
                    @"SELECT c.vat_rate
                      FROM companies c
                      INNER JOIN users u ON u.company_id = c.id
                      WHERE u.id = @userId", conn);
                cmd.Parameters.AddWithValue("userId", Guid.Parse(_userId));

                var result = await cmd.ExecuteScalarAsync();
                decimal currentVat = result != null && result != DBNull.Value
                    ? Convert.ToDecimal(result)
                    : 12.00m;

                var dialog = new ChangeVatDialog(_userId, currentVat);
                dialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load VAT info: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Helpers ─────────────────────────────────────────────────────────────
        private async Task RefreshCompanyNameAsync()
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(
                    "SELECT c.name FROM companies c INNER JOIN users u ON u.company_id = c.id WHERE u.id = @userId", conn);
                cmd.Parameters.AddWithValue("userId", Guid.Parse(_userId));

                var name = await cmd.ExecuteScalarAsync();
                if (name != null && name != DBNull.Value)
                {
                    _companyName = name.ToString();
                    titleLabel.Text = $"{_companyName} ";
                }
            }
            catch { /* silently ignore refresh errors */ }
        }
    }
}
