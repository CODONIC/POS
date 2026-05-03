using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace POS.Cashier
{
    public class PaymentService
    {
        private readonly string _companyId;
        private readonly string _username;

        public PaymentService(string companyId, string username)
        {
            _companyId = companyId;
            _username = username;
        }

        public async Task<long> SaveTransactionAsync(string transactionNumber, decimal subtotal, decimal discountPercentage,
            decimal discountAmount, decimal vatAmount, decimal vatableAmount, decimal totalAmount,
            string paymentMethod, decimal customerPayment, decimal change)
        {
            using var conn = DatabaseService.GetConnection();
            await conn.OpenAsync();

            string sql = @"
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

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@transactionNumber", transactionNumber);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            cmd.Parameters.AddWithValue("@cashierName", _username);
            cmd.Parameters.AddWithValue("@subtotal", subtotal);
            cmd.Parameters.AddWithValue("@discountPercentage", discountPercentage);
            cmd.Parameters.AddWithValue("@discountAmount", discountAmount);
            cmd.Parameters.AddWithValue("@vatAmount", vatAmount);
            cmd.Parameters.AddWithValue("@vatableAmount", vatableAmount);
            cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
            cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
            cmd.Parameters.AddWithValue("@customerPayment", customerPayment);
            cmd.Parameters.AddWithValue("@changeAmount", change);
            cmd.Parameters.AddWithValue("@transactionDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@status", "Completed");

            return (long)await cmd.ExecuteScalarAsync();
        }

        public async Task SaveTransactionItemsAsync(long transactionId, DataTable cartItems)
        {
            using var conn = DatabaseService.GetConnection();
            await conn.OpenAsync();

            var sql = new StringBuilder();
            sql.Append("INSERT INTO transaction_items (transaction_id, product_code, product_name, quantity, price, subtotal) VALUES ");

            var parameters = new List<NpgsqlParameter>();
            for (int i = 0; i < cartItems.Rows.Count; i++)
            {
                var item = cartItems.Rows[i];
                if (i > 0) sql.Append(",");
                sql.Append($"(@transactionId, @productCode{i}, @productName{i}, @quantity{i}, @price{i}, @subtotal{i})");

                parameters.Add(new NpgsqlParameter($"@productCode{i}", item["product_code"].ToString()));
                parameters.Add(new NpgsqlParameter($"@productName{i}", item["product_name"].ToString()));
                parameters.Add(new NpgsqlParameter($"@quantity{i}", Convert.ToInt32(item["quantity"])));
                parameters.Add(new NpgsqlParameter($"@price{i}", Convert.ToDecimal(item["price"])));
                parameters.Add(new NpgsqlParameter($"@subtotal{i}", Convert.ToDecimal(item["subtotal"])));
            }

            using var cmd = new NpgsqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddWithValue("@transactionId", transactionId);
            cmd.Parameters.AddRange(parameters.ToArray());
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateProductQuantitiesAsync(DataTable cartItems)
        {
            using var conn = DatabaseService.GetConnection();
            await conn.OpenAsync();

            using var transaction = await conn.BeginTransactionAsync();
            try
            {
                foreach (DataRow item in cartItems.Rows)
                {
                    string updateQuery = @"
                        UPDATE products 
                        SET quantity = quantity - @soldQuantity 
                        WHERE product_code = @productCode 
                        AND company_id = @companyId::uuid
                        AND quantity >= @soldQuantity";

                    using var cmd = new NpgsqlCommand(updateQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@soldQuantity", Convert.ToInt32(item["quantity"]));
                    cmd.Parameters.AddWithValue("@productCode", item["product_code"].ToString());
                    cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
                    await cmd.ExecuteNonQueryAsync();
                }
                await transaction.CommitAsync();
            }
            catch { await transaction.RollbackAsync(); throw; }
        }
    }
}