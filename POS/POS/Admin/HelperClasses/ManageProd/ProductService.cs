using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace POS.Admin
{
    public class ProductService
    {
        private readonly string _companyId;
        private readonly string _connectionString;

        public ProductService(string companyId)
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
                SELECT p.product_code, p.product_name, p.price, 
                       p.quantity, p.reorder_level, c.name AS category
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

        public async Task<List<CategoryItem>> GetCategoriesAsync()
        {
            var categories = new List<CategoryItem>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "SELECT id, name FROM categories WHERE company_id = @companyId ORDER BY name";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                categories.Add(new CategoryItem
                {
                    Id = reader["id"].ToString(),
                    Name = reader["name"].ToString()
                });
            }
            return categories;
        }

        public async Task<bool> ProductExistsAsync(string productCode)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "SELECT COUNT(*) FROM products WHERE product_code = @code AND company_id = @companyId";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("code", productCode);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

            return (long)await cmd.ExecuteScalarAsync() > 0;
        }

        public async Task AddProductAsync(string code, string name, decimal price, int reorderLevel, string categoryId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                INSERT INTO products (product_code, product_name, price, quantity, reorder_level, category_id, company_id)
                VALUES (@code, @name, @price, 0, @reorderLevel, @categoryId, @companyId)";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("code", code);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("price", price);
            cmd.Parameters.AddWithValue("reorderLevel", reorderLevel);
            cmd.Parameters.AddWithValue("categoryId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(categoryId));
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<(string name, decimal price, int reorder, string category)> GetProductOldValuesAsync(string productCode)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                SELECT p.product_name, p.price, p.reorder_level, c.name AS category
                FROM products p
                LEFT JOIN categories c ON p.category_id = c.id
                WHERE p.product_code = @code AND p.company_id = @companyId";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("code", productCode);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return (
                    name: reader["product_name"].ToString(),
                    price: Convert.ToDecimal(reader["price"]),
                    reorder: Convert.ToInt32(reader["reorder_level"]),
                    category: reader["category"]?.ToString()
                );
            }
            return (null, 0, 0, null);
        }

        public async Task UpdateProductAsync(string productCode, string name, decimal price, int reorderLevel, string categoryId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                UPDATE products 
                SET product_name = @name, price = @price, reorder_level = @reorderLevel, category_id = @categoryId
                WHERE product_code = @code AND company_id = @companyId";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("price", price);
            cmd.Parameters.AddWithValue("reorderLevel", reorderLevel);
            cmd.Parameters.AddWithValue("categoryId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(categoryId));
            cmd.Parameters.AddWithValue("code", productCode);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteProductAsync(string productCode)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "DELETE FROM products WHERE product_code = @code AND company_id = @companyId";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("code", productCode);
            cmd.Parameters.AddWithValue("companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public class CategoryItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}