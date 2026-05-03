using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Cashier
{
    public partial class PaymentFrm : BaseForm
    {
        private readonly PaymentService _paymentService;
        private readonly PaymentCalculator _calculator;
        private readonly string _transactionNumber;
        private readonly DataTable _cartItems;
        private readonly decimal _originalSubtotal;

        public PaymentFrm(string username, string companyName, string transactionNumber,
                          DataTable cartItems, decimal subtotal, decimal discountPercentage,
                          decimal discountAmount, decimal vatableAmount, decimal vatAmount, decimal totalAmount)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);

            var companyId = GetCompanyId(companyName);
            _paymentService = new PaymentService(companyId, username);
            _calculator = new PaymentCalculator(subtotal, discountPercentage);
            _transactionNumber = transactionNumber;
            _cartItems = cartItems;
            _originalSubtotal = subtotal;

            InitializeForm(username, companyName);
        }

        private string GetCompanyId(string companyName)
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@name", companyName);
                return cmd.ExecuteScalar()?.ToString();
            }
            catch { return null; }
        }

        private void InitializeForm(string username, string companyName)
        {
            lblCashierName.Text = $"{username} | Cashier";
            titleLabel.Text = $"{companyName} ";

            guna2ComboBox1.Items.AddRange(new[] { "Cash", "Credit Card", "Debit Card", "GCash", "PayMaya" });
            guna2ComboBox1.SelectedIndex = 0;
            guna2ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            txtDiscountPercent.Text = _calculator.DiscountPercentage.ToString();
            txtTotalToPay.Text = _calculator.TotalAmount.ToString("F2");
            txtTransactionNo.Text = _transactionNumber;
            txtCustomerPayment.Text = "0";
            txtChange.Text = "₱ 0.00";
            btnConfirmPayment.Enabled = false;

            txtDiscountPercent.TextChanged += (s, e) => { _calculator.Recalculate(GetDiscountPercent()); UpdateDisplay(); CalculateChange(); };
            txtCustomerPayment.TextChanged += (s, e) => CalculateChange();
            txtCustomerPayment.KeyPress += (s, e) => e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') ||
                (e.KeyChar == '.' && (s as TextBox).Text.Contains("."));
        }

        private decimal GetDiscountPercent() => decimal.TryParse(txtDiscountPercent.Text, out var pct) ? Math.Clamp(pct, 0, 100) : 0;

        private void UpdateDisplay()
        {
            txtDiscountPercent.Text = _calculator.DiscountPercentage.ToString();
            txtTotalToPay.Text = _calculator.TotalAmount.ToString("F2");
        }

        private void CalculateChange()
        {
            decimal payment = decimal.TryParse(txtCustomerPayment.Text, out var p) ? p : 0;
            decimal change = _calculator.CalculateChange(payment);
            txtChange.Text = $"₱ {change:F2}";
            btnConfirmPayment.Enabled = _calculator.IsPaymentSufficient(payment);
        }

        private async void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtCustomerPayment.Text, out decimal payment) || payment <= 0)
            { ShowError("Please enter a valid payment amount.", "Invalid Payment"); return; }

            if (!_calculator.IsPaymentSufficient(payment))
            { ShowError($"Insufficient payment!\nTotal: ₱{_calculator.TotalAmount:F2}\nPayment: ₱{payment:F2}", "Payment Error"); return; }

            string method = guna2ComboBox1.SelectedItem?.ToString() ?? "Cash";
            decimal change = _calculator.CalculateChange(payment);

            if (PaymentValidator.ConfirmPayment(_transactionNumber, _originalSubtotal, _calculator.DiscountPercentage,
                _calculator.DiscountAmount, _calculator.VatableAmount, _calculator.VatAmount, _calculator.TotalAmount,
                method, payment, change) != DialogResult.Yes) return;

            try
            {
                var transactionId = await _paymentService.SaveTransactionAsync(_transactionNumber, _originalSubtotal,
                    _calculator.DiscountPercentage, _calculator.DiscountAmount, _calculator.VatAmount,
                    _calculator.VatableAmount, _calculator.TotalAmount, method, payment, change);

                await _paymentService.SaveTransactionItemsAsync(transactionId, _cartItems);
                await _paymentService.UpdateProductQuantitiesAsync(_cartItems);

                ShowSuccess($"Payment Successful!\nChange: ₱{change:F2}\nThank you!", "Payment Complete");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { ShowError($"Failed to save transaction: {ex.Message}", "Database Error"); }
        }

        private void btnClear_Click(object sender, EventArgs e) { txtCustomerPayment.Text = "0"; txtChange.Text = "₱ 0.00"; btnConfirmPayment.Enabled = false; txtCustomerPayment.Focus(); }
        public override void CloseButton_Click(object sender, EventArgs e) => Close();

        private void ShowError(string msg, string title) => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void ShowSuccess(string msg, string title) => MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}