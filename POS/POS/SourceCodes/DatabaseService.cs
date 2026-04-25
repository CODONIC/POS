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
                // CockroachDB connection 
                _connectionString = "Host=tindero-comp-15354.jxf.cockroachlabs.cloud;" +
                                    "Port=26257;" +
                                    "Database=tinderodb;" +
                                    "Username=krizz;" +
                                    "Password=UFZfxznt1P7k6XSmIw17zQ;" +
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