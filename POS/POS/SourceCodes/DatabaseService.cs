using Npgsql;
using System;
using System.Windows.Forms;

namespace POS
{
    public static class DatabaseService
    {
        private static string _connectionString;

        public static string ConnectionString => _connectionString;

        public static async Task InitializeAsync()
        {
            try
            {
                // CockroachDB connection — fill in your password below
                _connectionString = "Host=tindero-corp-13921.jxf.cockroachlabs.cloud;" +
                                    "Port=26257;" +
                                    "Database=defaultdb;" +
                                    "Username=krizz;" +
                                    "Password=U0wu3qf6U9KXVgBj94g07g ;" +
                                    "SSL Mode=Require;";

                // Test the connection
                await using var conn = new NpgsqlConnection(_connectionString);
                await conn.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to database: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}