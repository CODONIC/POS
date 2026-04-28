using Npgsql;
using POS.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace POS
{
    public partial class AdminDashboard : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;

        public AdminDashboard(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;

            // Resolve company ID for audit logging
            _companyId = GetCompanyId(_companyName);
            SetUserContext(_username, _companyId);

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            this.KeyPreview = true;
            this.KeyDown += AdminDashboard_KeyDown;
            ShortcutKeyHints();
        }

        // ─── Resolve company name to ID ───────────────────────────────────────────

        private string GetCompanyId(string companyName)
        {
            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", companyName);
                        var result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error resolving company: {ex.Message}");
                return null;
            }
        }

        private async void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // Log the logout using the stored company ID
                try
                {
                    if (!string.IsNullOrEmpty(_companyId))
                    {
                        await AuditService.LogLogoutAsync(
                            username: _username,
                            companyId: _companyId,
                            deviceInfo: Environment.MachineName
                        );
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Logout audit failed: {ex.Message}");
                }

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

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            TransactionsForm trans = new TransactionsForm(_username, _companyName);
            trans.Show();
            this.Hide();
        }

        private void btnBusinessStats_Click(object sender, EventArgs e)
        {
            BusinessStatsForm businessStats = new BusinessStatsForm(_username, _companyName);
            businessStats.Show();
            this.Hide();
        }

        private async void btnSalesReport_Click(object sender, EventArgs e)
        {
            // Create a date range picker dialog
            using (var dateDialog = new Form())
            {
                dateDialog.Text = "Select Date Range";
                dateDialog.Size = new Size(300, 180);
                dateDialog.StartPosition = FormStartPosition.CenterParent;
                dateDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dateDialog.MaximizeBox = false;
                dateDialog.MinimizeBox = false;

                Label lblFrom = new Label() { Text = "From Date:", Location = new Point(20, 20), Size = new Size(80, 25) };
                DateTimePicker dtpFrom = new DateTimePicker() { Location = new Point(100, 20), Size = new Size(150, 25), MaxDate = DateTime.Today };

                Label lblTo = new Label() { Text = "To Date:", Location = new Point(20, 60), Size = new Size(80, 25) };
                DateTimePicker dtpTo = new DateTimePicker() { Location = new Point(100, 60), Size = new Size(150, 25), MaxDate = DateTime.Today };

                Button btnGenerate = new Button() { Text = "Generate Report", Location = new Point(40, 100), Size = new Size(120, 30), BackColor = Color.FromArgb(59, 130, 246), ForeColor = Color.White };
                Button btnCancel = new Button() { Text = "Cancel", Location = new Point(160, 100), Size = new Size(100, 30), BackColor = Color.Gray, ForeColor = Color.White };

                dtpFrom.Value = DateTime.Today.AddDays(-30);
                dtpTo.Value = DateTime.Today;

                btnCancel.Click += (s, ev) => { dateDialog.DialogResult = DialogResult.Cancel; dateDialog.Close(); };
                btnGenerate.Click += (s, ev) => { dateDialog.DialogResult = DialogResult.OK; dateDialog.Close(); };

                dateDialog.Controls.AddRange(new Control[] { lblFrom, dtpFrom, lblTo, dtpTo, btnGenerate, btnCancel });

                if (dateDialog.ShowDialog() == DialogResult.OK)
                {
                    await GenerateSalesReportCSV(dtpFrom.Value.Date, dtpTo.Value.Date);
                }
            }
        }

        private async Task GenerateSalesReportCSV(DateTime fromDate, DateTime toDate)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                // Fetch sales data
                var salesData = await FetchSalesDataForExportAsync(fromDate, toDate);

                if (salesData == null || salesData.Rows.Count == 0)
                {
                    MessageBox.Show("No sales data found for the selected date range.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Export to CSV
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV Files (*.csv)|*.csv";
                    saveDialog.FileName = $"SalesReport_{_companyName}_{fromDate:yyyyMMdd}_to_{toDate:yyyyMMdd}.csv";
                    saveDialog.Title = "Save Sales Report";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        await Task.Run(() => ExportSalesReportToCSV(salesData, saveDialog.FileName, fromDate, toDate));
                        MessageBox.Show($"Sales report generated successfully!\nSaved to: {saveDialog.FileName}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating sales report:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task<DataTable> FetchSalesDataForExportAsync(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    await conn.OpenAsync();

                    string sql = @"
                SELECT 
                    t.transaction_number AS TransactionNumber,
                    t.transaction_date AS TransactionDate,
                    t.cashier_name AS Cashier,
                    ti.product_name AS Product,
                    ti.quantity AS Quantity,
                    ti.price AS UnitPrice,
                    ti.subtotal AS Subtotal,
                    t.subtotal AS TransactionSubtotal,
                    t.discount_percentage AS DiscountPercentage,
                    t.discount_amount AS DiscountAmount,
                    t.vatable_amount AS VatableAmount,
                    t.vat_amount AS VATAmount,
                    t.total_amount AS TotalAmount,
                    t.payment_method AS PaymentMethod,
                    t.customer_payment AS CustomerPayment,
                    t.change_amount AS ChangeAmount,
                    t.status AS Status
                FROM public.transactions t
                LEFT JOIN public.transaction_items ti ON t.id = ti.transaction_id
                WHERE t.company_id = @companyId
                    AND t.transaction_date >= @fromDate
                    AND t.transaction_date <= @toDate
                    AND LOWER(t.status) = 'completed'
                ORDER BY t.transaction_date DESC, t.transaction_number";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
                        cmd.Parameters.AddWithValue("@fromDate", fromDate);
                        cmd.Parameters.AddWithValue("@toDate", toDate.AddDays(1).AddSeconds(-1));

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch sales data: {ex.Message}", ex);
            }

            return dt;
        }

        private void ExportSalesReportToCSV(DataTable dataTable, string filePath, DateTime fromDate, DateTime toDate)
        {
            var sb = new System.Text.StringBuilder();

            // Calculate summary totals
            decimal totalGrossSales = 0;
            decimal totalDiscount = 0;
            decimal totalVAT = 0;
            decimal totalNetSales = 0;
            int totalTransactions = 0;
            int totalItems = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                totalGrossSales += Convert.ToDecimal(row["TransactionSubtotal"]);
                totalDiscount += Convert.ToDecimal(row["DiscountAmount"]);
                totalVAT += Convert.ToDecimal(row["VATAmount"]);
                totalNetSales += Convert.ToDecimal(row["TotalAmount"]);
                totalItems += Convert.ToInt32(row["Quantity"]);
            }

            // Get unique transaction count
            var uniqueTransactions = new HashSet<string>();
            foreach (DataRow row in dataTable.Rows)
            {
                string transNum = row["TransactionNumber"]?.ToString();
                if (!string.IsNullOrEmpty(transNum))
                    uniqueTransactions.Add(transNum);
            }
            totalTransactions = uniqueTransactions.Count;

            // Helper function to escape CSV values
            string Esc(object value) => $"\"{value?.ToString().Replace("\"", "\"\"") ?? ""}\"";

            // Add Report Header
            sb.AppendLine($"\"Sales Report - {_companyName}\"");
            sb.AppendLine($"\"Period: {fromDate:MM/dd/yyyy} - {toDate:MM/dd/yyyy}\"");
            sb.AppendLine();

            // Add Summary Section
            sb.AppendLine("\"SALES SUMMARY\"");
            sb.AppendLine($"\"Total Transactions:\",\"{totalTransactions:N0}\"");
            sb.AppendLine($"\"Total Items Sold:\",\"{totalItems:N0}\"");
            sb.AppendLine($"\"Gross Sales:\",\"{totalGrossSales:C2}\"");
            sb.AppendLine($"\"Total Discounts:\",\"{totalDiscount:C2}\"");
            sb.AppendLine($"\"Total VAT:\",\"{totalVAT:C2}\"");
            sb.AppendLine($"\"Net Sales:\",\"{totalNetSales:C2}\"");
            sb.AppendLine();

            // Add Detailed Sales Data Header
            sb.AppendLine("\"DETAILED SALES DATA\"");
            sb.AppendLine();

            // Add column headers
            sb.Append("\"Transaction #\",");
            sb.Append("\"Date & Time\",");
            sb.Append("\"Cashier\",");
            sb.Append("\"Product\",");
            sb.Append("\"Quantity\",");
            sb.Append("\"Unit Price\",");
            sb.Append("\"Subtotal\",");
            sb.Append("\"Transaction Subtotal\",");
            sb.Append("\"Discount %\",");
            sb.Append("\"Discount Amount\",");
            sb.Append("\"Vatable Amount\",");
            sb.Append("\"VAT\",");
            sb.Append("\"Total Amount\",");
            sb.Append("\"Payment Method\",");
            sb.Append("\"Tendered\",");
            sb.Append("\"Change\",");
            sb.AppendLine("\"Status\"");

            // Add data rows
            foreach (DataRow row in dataTable.Rows)
            {
                sb.Append($"{Esc(row["TransactionNumber"])},");
                sb.Append($"{Esc(Convert.ToDateTime(row["TransactionDate"]).ToString("MM/dd/yyyy HH:mm:ss"))},");
                sb.Append($"{Esc(row["Cashier"])},");
                sb.Append($"{Esc(row["Product"])},");
                sb.Append($"{Esc(row["Quantity"])},");
                sb.Append($"{Esc(Convert.ToDecimal(row["UnitPrice"]).ToString("F2"))},");
                sb.Append($"{Esc(Convert.ToDecimal(row["Subtotal"]).ToString("F2"))},");
                sb.Append($"{Esc(Convert.ToDecimal(row["TransactionSubtotal"]).ToString("F2"))},");
                sb.Append($"{Esc(row["DiscountPercentage"])},");
                sb.Append($"{Esc(Convert.ToDecimal(row["DiscountAmount"]).ToString("F2"))},");
                sb.Append($"{Esc(Convert.ToDecimal(row["VatableAmount"]).ToString("F2"))},");
                sb.Append($"{Esc(Convert.ToDecimal(row["VATAmount"]).ToString("F2"))},");
                sb.Append($"{Esc(Convert.ToDecimal(row["TotalAmount"]).ToString("F2"))},");
                sb.Append($"{Esc(row["PaymentMethod"])},");
                sb.Append($"{Esc(Convert.ToDecimal(row["CustomerPayment"]).ToString("F2"))},");
                sb.Append($"{Esc(Convert.ToDecimal(row["ChangeAmount"]).ToString("F2"))},");
                sb.AppendLine($"{Esc(row["Status"])}");
            }

            System.IO.File.WriteAllText(filePath, sb.ToString(), System.Text.Encoding.UTF8);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(_username, _companyName);
            settingsForm.Show();
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
                case Keys.F5:
                    btnTransactions_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F6:
                    btnBusinessStats_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F7:
                    btnSalesReport_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F8:
                    btnSettings_Click(sender, e);
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
            toolTip.SetToolTip(btnTransactions, "F5");
            toolTip.SetToolTip(btnBusinessStats, "F6");
            toolTip.SetToolTip(btnSalesReport, "F7");
            toolTip.SetToolTip(btnSettings, "F8");

            AttachHoverEffect(btnLogOut);
            AttachHoverEffect(btnManageUsers);
            AttachHoverEffect(btnManageCategory);
            AttachHoverEffect(btnManageProducts);
            AttachHoverEffect(btnManageStocks);
            AttachHoverEffect(btnTransactions);
            AttachHoverEffect(btnBusinessStats);
            AttachHoverEffect(btnSalesReport);
            AttachHoverEffect(btnSettings);
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

        private void btnAudit_Click(object sender, EventArgs e)
        {
            EmployeeLogsFrm audit = new EmployeeLogsFrm(_username, _companyName);
            audit.Show();
            this.Hide();
        }
    }
}