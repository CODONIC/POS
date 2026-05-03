using Npgsql;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace POS
{
    public static class DatabaseService
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get => _connectionString;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Connection string cannot be null or empty.");
                _connectionString = value;
            }
        }

        private static readonly string _configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TinderoPOS", "db.config");

        private static readonly byte[] _entropy =
            Encoding.UTF8.GetBytes("TinderoPOS-v1-salt");

        public static async Task InitializeAsync()
        {
            try
            {
                ConnectionString = Load();

                await using var conn = new NpgsqlConnection(ConnectionString);
                await conn.OpenAsync();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    "No database configuration found.\n\n" +
                    "Please run the DB Config Admin tool to set up the connection before launching the POS.",
                    "Setup Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Invalid connection string: {ex.Message}",
                    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect to database: {ex.Message}",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static NpgsqlConnection GetConnection()
            => new NpgsqlConnection(ConnectionString);

        public static void SaveConnectionString(string plaintext)
        {
            ConnectionString = plaintext;

            Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);

            byte[] encrypted = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plaintext),
                _entropy,
                DataProtectionScope.LocalMachine);

            File.WriteAllBytes(_configPath, encrypted);
        }

        private static string Load()
        {
            if (!File.Exists(_configPath))
                throw new FileNotFoundException("Database config file not found.", _configPath);

            byte[] cipher = File.ReadAllBytes(_configPath);
            byte[] plain = ProtectedData.Unprotect(cipher, _entropy,
                                DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(plain);
        }
    }
}