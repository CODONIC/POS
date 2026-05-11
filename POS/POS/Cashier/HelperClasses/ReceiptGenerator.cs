using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace POS.Cashier
{
    public class ReceiptGenerator
    {
        private readonly string _companyName;
        private readonly string _cashierName;
        private readonly string _transactionNumber;
        private readonly DateTime _transactionDate;
        private readonly DataTable _cartItems;
        private readonly decimal _subtotal;
        private readonly decimal _discountPercentage;
        private readonly decimal _discountAmount;
        private readonly decimal _vatableAmount;
        private readonly decimal _vatAmount;
        private readonly decimal _totalAmount;
        private readonly decimal _customerPayment;
        private readonly decimal _changeAmount;
        private readonly string _paymentMethod;
        private PrintDocument _printDocument;

        public ReceiptGenerator(
            string companyName,
            string cashierName,
            string transactionNumber,
            DataTable cartItems,
            decimal subtotal,
            decimal discountPercentage,
            decimal discountAmount,
            decimal vatableAmount,
            decimal vatAmount,
            decimal totalAmount,
            decimal customerPayment,
            decimal changeAmount,
            string paymentMethod)
        {
            _companyName = companyName;
            _cashierName = cashierName;
            _transactionNumber = transactionNumber;
            _transactionDate = DateTime.Now;
            _cartItems = cartItems;
            _subtotal = subtotal;
            _discountPercentage = discountPercentage;
            _discountAmount = discountAmount;
            _vatableAmount = vatableAmount;
            _vatAmount = vatAmount;
            _totalAmount = totalAmount;
            _customerPayment = customerPayment;
            _changeAmount = changeAmount;
            _paymentMethod = paymentMethod;
        }

        public string GenerateReceiptText()
        {
            var sb = new StringBuilder();
            int lineWidth = 48;

            // Header
            sb.AppendLine(new string('=', lineWidth));
            sb.AppendLine(CenterText(_companyName, lineWidth));
            sb.AppendLine(CenterText("Insert Branch Location Here", lineWidth));
            sb.AppendLine(new string('-', lineWidth));
            sb.AppendLine(CenterText("COUNTER", lineWidth));
            sb.AppendLine(new string('-', lineWidth));

            // Transaction Info
            sb.AppendLine($"Date: {_transactionDate:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Cashier: {_cashierName}");
            sb.AppendLine($"Transaction #: {_transactionNumber}");
            sb.AppendLine(new string('-', lineWidth));

            // Items Header
            sb.AppendLine($"{"Qty",3} {"Item",-25} {"Price",8} {"Total",8}");
            sb.AppendLine(new string('-', lineWidth));

            // Items
            foreach (DataRow item in _cartItems.Rows)
            {
                string productName = item["product_name"].ToString();
                int quantity = Convert.ToInt32(item["quantity"]);
                decimal price = Convert.ToDecimal(item["price"]);
                decimal subtotal = Convert.ToDecimal(item["subtotal"]);

                // Truncate long names
                if (productName.Length > 25)
                    productName = productName.Substring(0, 22) + "...";

                sb.AppendLine($"{quantity,3} {productName,-25} {price,8:F2} {subtotal,8:F2}");
            }

            sb.AppendLine(new string('-', lineWidth));

            // Totals
            sb.AppendLine($"{"Subtotal:",-35} {_subtotal,12:F2}");

            if (_discountPercentage > 0)
            {
                sb.AppendLine($"{"Discount (" + _discountPercentage + "%):",-35} -{_discountAmount,12:F2}");
            }

            sb.AppendLine($"{"VATable Amount:",-35} {_vatableAmount,12:F2}");
            sb.AppendLine($"{"VAT (12%):",-35} {_vatAmount,12:F2}");
            sb.AppendLine(new string('-', lineWidth));
            sb.AppendLine($"{"TOTAL:",-35} {_totalAmount,12:F2}");
            sb.AppendLine(new string('-', lineWidth));

            // Payment
            sb.AppendLine($"{"Payment Method:",-35} {_paymentMethod}");
            sb.AppendLine($"{"Amount Paid:",-35} {_customerPayment,12:F2}");
            sb.AppendLine($"{"Change:",-35} {_changeAmount,12:F2}");
            sb.AppendLine(new string('=', lineWidth));

            // Footer
            sb.AppendLine(CenterText("THANK YOU FOR YOUR PURCHASE!", lineWidth));
            sb.AppendLine(CenterText("Please come again!", lineWidth));
            sb.AppendLine(new string('-', lineWidth));
            sb.AppendLine(CenterText("No returns/refunds!", lineWidth));
            sb.AppendLine(CenterText("Check items before leaving", lineWidth));
            sb.AppendLine(new string('=', lineWidth));
            sb.AppendLine(CenterText("TINDERO COMPANY", lineWidth));
            sb.AppendLine(CenterText("www.tindero-pos.com", lineWidth));
            sb.AppendLine(new string('=', lineWidth));

            return sb.ToString();
        }

        private string CenterText(string text, int width)
        {
            if (text.Length >= width)
                return text;

            int padding = (width - text.Length) / 2;
            return text.PadLeft(padding + text.Length).PadRight(width);
        }

        public void PrintReceipt(bool showPreview = false)
        {
            _printDocument = new PrintDocument();

            // Try to find a receipt printer
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.ToLower().Contains("receipt") ||
                    printer.ToLower().Contains("thermal") ||
                    printer.ToLower().Contains("epson") ||
                    printer.ToLower().Contains("pos"))
                {
                    _printDocument.PrinterSettings.PrinterName = printer;
                    break;
                }
            }

            _printDocument.PrintPage += PrintDocument_PrintPage;

            if (showPreview)
            {
                PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                previewDialog.Document = _printDocument;
                previewDialog.WindowState = FormWindowState.Maximized;
                previewDialog.ShowDialog();
            }
            else
            {
                try
                {
                    _printDocument.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Print error: {ex.Message}\n\nWould you like to preview instead?",
                        "Print Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (MessageBox.Show("Preview receipt?", "Print Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PrintReceipt(true);
                    }
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            string receiptText = GenerateReceiptText();
            Font printFont = new Font("Courier New", 9);
            float lineHeight = printFont.GetHeight(e.Graphics);
            float y = 10;
            float leftMargin = 10;

            using (StringReader reader = new StringReader(receiptText))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, y);
                    y += lineHeight;
                }
            }
        }

        public void SaveReceiptToFile()
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Text Files (*.txt)|*.txt|PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"Receipt_{_transactionNumber}_{_transactionDate:yyyyMMdd_HHmmss}.txt";
                saveDialog.Title = "Save Receipt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string receiptText = GenerateReceiptText();
                    System.IO.File.WriteAllText(saveDialog.FileName, receiptText);
                    MessageBox.Show($"Receipt saved to:\n{saveDialog.FileName}",
                        "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void ShowReceiptDialog()
        {
            using (Form receiptForm = new Form())
            {
                receiptForm.Text = $"Receipt - {_transactionNumber}";
                receiptForm.Size = new Size(450, 600);
                receiptForm.StartPosition = FormStartPosition.CenterParent;
                receiptForm.MinimizeBox = false;
                receiptForm.MaximizeBox = false;

                TextBox txtReceipt = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Vertical,
                    Font = new Font("Courier New", 9),
                    WordWrap = false
                };
                txtReceipt.Text = GenerateReceiptText();

                Panel buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 50 };

                Button btnPrint = new Button
                {
                    Text = "Print",
                    Size = new Size(100, 35),
                    Location = new Point(20, 8),
                    BackColor = Color.FromArgb(59, 130, 246),
                    ForeColor = Color.White
                };
                btnPrint.Click += (s, e) => PrintReceipt(true);

                Button btnSave = new Button
                {
                    Text = "Save",
                    Size = new Size(100, 35),
                    Location = new Point(130, 8),
                    BackColor = Color.FromArgb(16, 185, 129),
                    ForeColor = Color.White
                };
                btnSave.Click += (s, e) => SaveReceiptToFile();

                Button btnClose = new Button
                {
                    Text = "Close",
                    Size = new Size(100, 35),
                    Location = new Point(240, 8),
                    BackColor = Color.Gray,
                    ForeColor = Color.White
                };
                btnClose.Click += (s, e) => receiptForm.Close();

                buttonPanel.Controls.AddRange(new Control[] { btnPrint, btnSave, btnClose });

                receiptForm.Controls.Add(txtReceipt);
                receiptForm.Controls.Add(buttonPanel);
                receiptForm.ShowDialog();
            }
        }
    }
}