using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Cashier
{
    public partial class PaymentFrm : BaseForm
    {
        private string _username;
        private string _companyName;
        private string _companyId;
        private string _transactionNumber;
        private DataTable _cartItems;

        // Received pre-calculated values
        private decimal _subtotal;
        private decimal _discountPercentage;
        private decimal _discountAmount;
        private decimal _vatableAmount;
        private decimal _vatAmount;
        private decimal _totalAmount;

        public PaymentFrm(string username, string companyName, string transactionNumber,
                          DataTable cartItems, decimal subtotal, decimal discountPercentage,
                          decimal discountAmount, decimal vatableAmount, decimal vatAmount, decimal totalAmount)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);

            _username = username;
            _companyName = companyName;
            _transactionNumber = transactionNumber;
            _cartItems = cartItems;
            _subtotal = subtotal;
            _discountPercentage = discountPercentage;
            _discountAmount = discountAmount;
            _vatableAmount = vatableAmount;
            _vatAmount = vatAmount;
            _totalAmount = totalAmount;

            // Get company ID
            _companyId = GetCompanyId(_companyName);

            // Initialize payment method combo box
            InitializePaymentMethodComboBox();

            // Display pre-calculated values
            DisplayAmounts();

            // Set default values
            txtCustomerPayment.Text = "0";
            txtChange.Text = "₱ 0.00";
            lblCashierName.Text = $"{_username} | Cashier";
            titleLabel.Text = $"{_companyName} ";

            // Calculate initial change
            CalculateChange();
        }

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
                MessageBox.Show($"Error resolving company:\n{ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void InitializePaymentMethodComboBox()
        {
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Cash");
            guna2ComboBox1.Items.Add("Credit Card");
            guna2ComboBox1.Items.Add("Debit Card");
            guna2ComboBox1.Items.Add("GCash");
            guna2ComboBox1.Items.Add("PayMaya");
            guna2ComboBox1.Items.Add("Bank Transfer");
            guna2ComboBox1.SelectedIndex = 0;
            guna2ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

         private void DisplayAmounts()
         {
             // Display all the pre-calculated values
             //txtSubtotal.Text = _subtotal.ToString("F2");
             txtDiscountPercent.Text = _discountPercentage.ToString();
             //txtDiscountAmount.Text = _discountAmount.ToString("F2");
             //txtVatable.Text = _vatableAmount.ToString("F2");
             //txtVAT.Text = _vatAmount.ToString("F2");
             txtTotalToPay.Text = _totalAmount.ToString("F2");
             txtTransactionNo.Text = _transactionNumber;
        }

        private void CalculateChange()
        {
            if (string.IsNullOrEmpty(txtCustomerPayment.Text) ||
                !decimal.TryParse(txtCustomerPayment.Text, out decimal customerPayment))
            {
                customerPayment = 0;
            }

            decimal change = customerPayment - _totalAmount;

            if (change < 0)
            {
                change = 0;
                txtChange.Text = "₱ 0.00";
                
                btnConfirmPayment.Enabled = false;
            }
            else
            {
                txtChange.Text = $"₱ {change:F2}";
                
                btnConfirmPayment.Enabled = true;
            }
        }

        private void txtCustomerPayment_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void txtCustomerPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private async void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerPayment.Text) ||
                !decimal.TryParse(txtCustomerPayment.Text, out decimal customerPayment))
            {
                MessageBox.Show("Please enter a valid payment amount.", "Invalid Payment",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (customerPayment < _totalAmount)
            {
                MessageBox.Show($"Insufficient payment amount!\n\nTotal to Pay: ₱{_totalAmount:F2}\nCustomer Payment: ₱{customerPayment:F2}",
                    "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal change = customerPayment - _totalAmount;
            string paymentMethod = guna2ComboBox1.SelectedItem?.ToString() ?? "Cash";

            // Confirm payment
            DialogResult result = MessageBox.Show(
                $"Payment Confirmation:\n\n" +
                $"Transaction #: {_transactionNumber}\n" +
                $"Subtotal: ₱{_subtotal:F2}\n" +
                $"Discount ({_discountPercentage}%): -₱{_discountAmount:F2}\n" +
                $"VATable Amount: ₱{_vatableAmount:F2}\n" +
                $"VAT (12%): ₱{_vatAmount:F2}\n" +
                $"Total to Pay: ₱{_totalAmount:F2}\n" +
                $"Payment Method: {paymentMethod}\n" +
                $"Customer Payment: ₱{customerPayment:F2}\n" +
                $"Change: ₱{change:F2}\n\n" +
                $"Proceed with payment?",
                "Confirm Payment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool saved = await SaveTransactionToDatabase(customerPayment, change, paymentMethod);

                if (saved)
                {
                    await UpdateProductQuantities();

                    MessageBox.Show($"Payment Successful!\n\nChange: ₱{change:F2}\n\nThank you for your purchase!",
                        "Payment Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save transaction. Please try again.",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task<bool> SaveTransactionToDatabase(decimal customerPayment, decimal change, string paymentMethod)
        {
            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    await conn.OpenAsync();

                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        // Insert into transactions table
                        string insertTransaction = @"
                            INSERT INTO transactions (
                                transaction_number, company_id, cashier_name, 
                                subtotal, discount_percentage, discount_amount, 
                                vat_amount, vatable_amount, total_amount, 
                                payment_method, customer_payment, change_amount, 
                                transaction_date, status
                            ) VALUES (
                                @transactionNumber, @companyId::uuid, @cashierName,
                                @subtotal, @discountPercentage, @discountAmount,
                                @vatAmount, @vatableAmount, @totalAmount,
                                @paymentMethod, @customerPayment, @changeAmount,
                                @transactionDate, @status
                            ) RETURNING id";

                        long transactionId;
                        using (var cmd = new NpgsqlCommand(insertTransaction, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@transactionNumber", _transactionNumber);
                            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
                            cmd.Parameters.AddWithValue("@cashierName", _username);
                            cmd.Parameters.AddWithValue("@subtotal", _subtotal);
                            cmd.Parameters.AddWithValue("@discountPercentage", _discountPercentage);
                            cmd.Parameters.AddWithValue("@discountAmount", _discountAmount);
                            cmd.Parameters.AddWithValue("@vatAmount", _vatAmount);
                            cmd.Parameters.AddWithValue("@vatableAmount", _vatableAmount);
                            cmd.Parameters.AddWithValue("@totalAmount", _totalAmount);
                            cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                            cmd.Parameters.AddWithValue("@customerPayment", customerPayment);
                            cmd.Parameters.AddWithValue("@changeAmount", change);
                            cmd.Parameters.AddWithValue("@transactionDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@status", "Completed");

                            transactionId = (long)await cmd.ExecuteScalarAsync();
                        }

                        // Insert transaction items
                        if (_cartItems.Rows.Count > 0)
                        {
                            var insertItemSql = new StringBuilder();
                            insertItemSql.Append(@"
                                INSERT INTO transaction_items (
                                    transaction_id, product_code, product_name, 
                                    quantity, price, subtotal
                                ) VALUES ");

                            var parameters = new List<NpgsqlParameter>();
                            int paramIndex = 0;

                            for (int i = 0; i < _cartItems.Rows.Count; i++)
                            {
                                DataRow item = _cartItems.Rows[i];
                                if (i > 0) insertItemSql.Append(",");
                                insertItemSql.Append($"(@transactionId, @productCode{paramIndex}, @productName{paramIndex}, @quantity{paramIndex}, @price{paramIndex}, @subtotal{paramIndex})");

                                parameters.Add(new NpgsqlParameter($"@productCode{paramIndex}", item["product_code"].ToString()));
                                parameters.Add(new NpgsqlParameter($"@productName{paramIndex}", item["product_name"].ToString()));
                                parameters.Add(new NpgsqlParameter($"@quantity{paramIndex}", Convert.ToInt32(item["quantity"])));
                                parameters.Add(new NpgsqlParameter($"@price{paramIndex}", Convert.ToDecimal(item["price"])));
                                parameters.Add(new NpgsqlParameter($"@subtotal{paramIndex}", Convert.ToDecimal(item["subtotal"])));
                                paramIndex++;
                            }

                            using (var cmd = new NpgsqlCommand(insertItemSql.ToString(), conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                                cmd.Parameters.AddRange(parameters.ToArray());
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task UpdateProductQuantities()
        {
            try
            {
                using (var conn = DatabaseService.GetConnection())
                {
                    await conn.OpenAsync();

                    using (var transaction = await conn.BeginTransactionAsync())
                    {
                        foreach (DataRow item in _cartItems.Rows)
                        {
                            string productCode = item["product_code"].ToString();
                            int quantitySold = Convert.ToInt32(item["quantity"]);

                            string updateQuery = @"
                                UPDATE products 
                                SET quantity = quantity - @soldQuantity 
                                WHERE product_code = @productCode 
                                AND company_id = @companyId::uuid
                                AND quantity >= @soldQuantity";

                            using (var cmd = new NpgsqlCommand(updateQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@soldQuantity", quantitySold);
                                cmd.Parameters.AddWithValue("@productCode", productCode);
                                cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        await transaction.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating inventory: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerPayment.Text = "0";
            txtChange.Text = "₱ 0.00";
            
            btnConfirmPayment.Enabled = true;
            txtCustomerPayment.Focus();
            
        }

        public override void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}