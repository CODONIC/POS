using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace POS.Admin
{
    public class StockService
    {
        private readonly string _companyId;
        private readonly string _connectionString;

        public StockService(string companyId)
        {
            _companyId = companyId;
            _connectionString = DatabaseService.ConnectionString;
        }

        public async Task<DataTable> GetProductsAsync(string search = "")
        {
            var dt = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                SELECT p.product_code, p.product_name, c.name AS category, 
                       p.price, p.quantity, p.reorder_level, p.stocked_in_date
                FROM products p
                LEFT JOIN categories c ON p.category_id = c.id
                WHERE p.company_id = @companyId
                  AND (p.product_code ILIKE @search OR p.product_name ILIKE @search OR c.name ILIKE @search)
                ORDER BY p.product_name";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            cmd.Parameters.AddWithValue("search", $"%{search}%");

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public async Task<int> GetCurrentQuantityAsync(string productCode)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "SELECT quantity FROM products WHERE product_code = @code AND company_id = @companyId";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("code", productCode);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task UpdateStockAsync(string productCode, int quantityChange, string changeType)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = changeType == "ADD"
                ? @"UPDATE products SET quantity = quantity + @qty, stocked_in_date = @date 
                    WHERE product_code = @code AND company_id = @companyId"
                : "UPDATE products SET quantity = quantity - @qty WHERE product_code = @code AND company_id = @companyId";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("qty", quantityChange);
            cmd.Parameters.AddWithValue("code", productCode);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

            if (changeType == "ADD")
                cmd.Parameters.AddWithValue("date", DateTime.Today);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}