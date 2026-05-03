using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace POS.Admin
{
    public class CategoryService
    {
        private readonly string _companyId;
        private readonly string _connectionString;

        public CategoryService(string companyId)
        {
            _companyId = companyId;
            _connectionString = DatabaseService.ConnectionString;
        }

        public async Task<DataTable> GetCategoriesAsync(string search = "")
        {
            var dt = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                SELECT id, name 
                FROM categories 
                WHERE company_id = @companyId AND name ILIKE @search
                ORDER BY name";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            cmd.Parameters.AddWithValue("@search", $"%{search}%");

            using var reader = await cmd.ExecuteReaderAsync();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));

            while (await reader.ReadAsync())
            {
                dt.Rows.Add(reader["id"].ToString(), reader["name"].ToString());
            }
            return dt;
        }

        public async Task<bool> CategoryExistsAsync(string name, string excludeId = null)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = string.IsNullOrEmpty(excludeId)
                ? @"SELECT COUNT(*) FROM categories WHERE LOWER(name) = LOWER(@name) AND company_id = @companyId"
                : @"SELECT COUNT(*) FROM categories WHERE LOWER(name) = LOWER(@name) AND company_id = @companyId AND id != @id";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            if (!string.IsNullOrEmpty(excludeId))
                cmd.Parameters.AddWithValue("@id", Guid.Parse(excludeId));

            return (long)await cmd.ExecuteScalarAsync() > 0;
        }

        public async Task<(string id, string name)> AddCategoryAsync(string name)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"INSERT INTO categories (name, company_id) VALUES (@name, @companyId) RETURNING id";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));

            var newId = (await cmd.ExecuteScalarAsync())?.ToString();
            return (newId, name);
        }

        public async Task<string> GetCategoryNameAsync(string categoryId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT name FROM categories WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(categoryId));
            return (await cmd.ExecuteScalarAsync())?.ToString();
        }

        public async Task UpdateCategoryAsync(string categoryId, string newName)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"UPDATE categories SET name = @name WHERE id = @id AND company_id = @companyId";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", newName);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(categoryId));
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> GetProductCountByCategoryAsync(string categoryId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM products WHERE category_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(categoryId));
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = "DELETE FROM categories WHERE id = @id AND company_id = @companyId";
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(categoryId));
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            await cmd.ExecuteNonQueryAsync();
        }
    }
}