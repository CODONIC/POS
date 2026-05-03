using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace POS
{
    public class SalesReportExporter
    {
        private readonly string _companyName;
        private readonly string _companyId;
        private readonly Form _parentForm;

        public SalesReportExporter(string companyName, string companyId, Form parentForm)
        {
            _companyName = companyName;
            _companyId = companyId;
            _parentForm = parentForm;
        }

        public async Task ShowDateRangeDialogAndExport()
        {
            using var dateDialog = CreateDateRangeDialog();

            if (dateDialog.ShowDialog(_parentForm) == DialogResult.OK)
            {
                var (dtpFrom, dtpTo) = ((DateTimePicker, DateTimePicker))dateDialog.Tag;
                await GenerateSalesReportCSV(dtpFrom.Value.Date, dtpTo.Value.Date);
            }
        }

        private Form CreateDateRangeDialog()
        {
            var dialog = new Form
            {
                Text = "Select Date Range",
                Size = new Size(300, 180),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var dtpFrom = new DateTimePicker
            {
                Location = new Point(100, 20),
                Size = new Size(150, 25),
                MaxDate = DateTime.Today,
                Value = DateTime.Today.AddDays(-30)
            };

            var dtpTo = new DateTimePicker
            {
                Location = new Point(100, 60),
                Size = new Size(150, 25),
                MaxDate = DateTime.Today,
                Value = DateTime.Today
            };

            var btnGenerate = new Button
            {
                Text = "Generate Report",
                Location = new Point(40, 100),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                DialogResult = DialogResult.OK
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(160, 100),
                Size = new Size(100, 30),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                DialogResult = DialogResult.Cancel
            };

            dialog.Controls.AddRange(new Control[]
            {
                new Label { Text = "From Date:", Location = new Point(20, 20), Size = new Size(80, 25) },
                dtpFrom,
                new Label { Text = "To Date:", Location = new Point(20, 60), Size = new Size(80, 25) },
                dtpTo,
                btnGenerate,
                btnCancel
            });

            dialog.Tag = (dtpFrom, dtpTo);
            return dialog;
        }

        private async Task GenerateSalesReportCSV(DateTime fromDate, DateTime toDate)
        {
            try
            {
                _parentForm.Cursor = Cursors.WaitCursor;

                var salesData = await FetchSalesDataForExportAsync(fromDate, toDate);

                if (salesData?.Rows.Count == 0)
                {
                    MessageBox.Show("No sales data found for the selected date range.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv",
                    FileName = $"SalesReport_{_companyName}_{fromDate:yyyyMMdd}_to_{toDate:yyyyMMdd}.csv",
                    Title = "Save Sales Report"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    await Task.Run(() => ExportSalesReportToCSV(salesData, saveDialog.FileName, fromDate, toDate));
                    MessageBox.Show($"Sales report generated successfully!\nSaved to: {saveDialog.FileName}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating sales report:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _parentForm.Cursor = Cursors.Default;
            }
        }

        private async Task<DataTable> FetchSalesDataForExportAsync(DateTime fromDate, DateTime toDate)
        {
            var dt = new DataTable();

            const string sql = @"
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

            try
            {
                using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate.AddDays(1).AddSeconds(-1));

                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch sales data: {ex.Message}", ex);
            }

            return dt;
        }

        private void ExportSalesReportToCSV(DataTable dataTable, string filePath, DateTime fromDate, DateTime toDate)
        {
            var sb = new StringBuilder();

            var (totalTransactions, totalItems, totalGrossSales, totalDiscount, totalVAT, totalNetSales) =
                CalculateSalesTotals(dataTable);

            sb.AppendLine($"\"Sales Report - {_companyName}\"");
            sb.AppendLine($"\"Period: {fromDate:MM/dd/yyyy} - {toDate:MM/dd/yyyy}\"");
            sb.AppendLine();
            sb.AppendLine("\"SALES SUMMARY\"");
            sb.AppendLine($"\"Total Transactions:\",\"{totalTransactions:N0}\"");
            sb.AppendLine($"\"Total Items Sold:\",\"{totalItems:N0}\"");
            sb.AppendLine($"\"Gross Sales:\",\"{totalGrossSales:C2}\"");
            sb.AppendLine($"\"Total Discounts:\",\"{totalDiscount:C2}\"");
            sb.AppendLine($"\"Total VAT:\",\"{totalVAT:C2}\"");
            sb.AppendLine($"\"Net Sales:\",\"{totalNetSales:C2}\"");
            sb.AppendLine();
            sb.AppendLine("\"DETAILED SALES DATA\"");
            sb.AppendLine();

            var headers = new[] { "Transaction #", "Date & Time", "Cashier", "Product", "Quantity", "Unit Price",
                "Subtotal", "Transaction Subtotal", "Discount %", "Discount Amount", "Vatable Amount", "VAT",
                "Total Amount", "Payment Method", "Tendered", "Change", "Status" };
            sb.AppendLine(string.Join(",", headers.Select(h => $"\"{h}\"")));

            foreach (DataRow row in dataTable.Rows)
            {
                sb.AppendLine(string.Join(",", new[]
                {
                    EscapeCsv(row["TransactionNumber"]),
                    EscapeCsv(Convert.ToDateTime(row["TransactionDate"]).ToString("MM/dd/yyyy HH:mm:ss")),
                    EscapeCsv(row["Cashier"]),
                    EscapeCsv(row["Product"]),
                    EscapeCsv(row["Quantity"]),
                    EscapeCsv(Convert.ToDecimal(row["UnitPrice"]).ToString("F2")),
                    EscapeCsv(Convert.ToDecimal(row["Subtotal"]).ToString("F2")),
                    EscapeCsv(Convert.ToDecimal(row["TransactionSubtotal"]).ToString("F2")),
                    EscapeCsv(row["DiscountPercentage"]),
                    EscapeCsv(Convert.ToDecimal(row["DiscountAmount"]).ToString("F2")),
                    EscapeCsv(Convert.ToDecimal(row["VatableAmount"]).ToString("F2")),
                    EscapeCsv(Convert.ToDecimal(row["VATAmount"]).ToString("F2")),
                    EscapeCsv(Convert.ToDecimal(row["TotalAmount"]).ToString("F2")),
                    EscapeCsv(row["PaymentMethod"]),
                    EscapeCsv(Convert.ToDecimal(row["CustomerPayment"]).ToString("F2")),
                    EscapeCsv(Convert.ToDecimal(row["ChangeAmount"]).ToString("F2")),
                    EscapeCsv(row["Status"])
                }));
            }

            System.IO.File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private (int transactions, int items, decimal gross, decimal discount, decimal vat, decimal net)
            CalculateSalesTotals(DataTable dataTable)
        {
            decimal gross = 0, discount = 0, vat = 0, net = 0;
            int items = 0;
            var uniqueTransactions = new HashSet<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                gross += Convert.ToDecimal(row["TransactionSubtotal"]);
                discount += Convert.ToDecimal(row["DiscountAmount"]);
                vat += Convert.ToDecimal(row["VATAmount"]);
                net += Convert.ToDecimal(row["TotalAmount"]);
                items += Convert.ToInt32(row["Quantity"]);

                string transNum = row["TransactionNumber"]?.ToString();
                if (!string.IsNullOrEmpty(transNum)) uniqueTransactions.Add(transNum);
            }

            return (uniqueTransactions.Count, items, gross, discount, vat, net);
        }

        private static string EscapeCsv(object value) =>
            $"\"{value?.ToString().Replace("\"", "\"\"") ?? ""}\"";
    }
}