using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DBConfigSetup
{
    internal class Program
    {
        private static readonly string _configPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TinderoPOS", "db.config");

        private static readonly byte[] _entropy =
            Encoding.UTF8.GetBytes("TinderoPOS-v1-salt");

        static void Main(string[] args)
        {
            // Silent mode: called by installer with arguments
            if (args.Length > 0)
            {
                RunSilent(args);
                return;
            }

            // Interactive mode: developer runs manually
            RunInteractive();
        }

        private static void RunSilent(string[] args)
        {
            try
            {
                string host = GetArg(args, "--host");
                string port = GetArg(args, "--port");
                string database = GetArg(args, "--database");
                string username = GetArg(args, "--username");
                string password = GetArg(args, "--password");
                string ssl = GetArg(args, "--ssl");

                string plaintext =
                    $"Host={host};" +
                    $"Port={port};" +
                    $"Database={database};" +
                    $"Username={username};" +
                    $"Password={password};" +
                    $"SSL Mode={ssl};";

                Save(plaintext);
            }
            catch (Exception ex)
            {
                // Write to a log file since there's no console in silent mode
                File.WriteAllText(
                    Path.Combine(Path.GetDirectoryName(_configPath)!, "setup-error.log"),
                    ex.Message);
            }
        }

        private static void RunInteractive()
        {
            Console.WriteLine("=== TinderoPOS DB Config Setup ===");
            Console.WriteLine();

            Console.Write("Host:      "); string host = Console.ReadLine();
            Console.Write("Port:      "); string port = Console.ReadLine();
            Console.Write("Database:  "); string database = Console.ReadLine();
            Console.Write("Username:  "); string username = Console.ReadLine();
            Console.Write("Password:  "); string password = ReadPasswordMasked();
            Console.Write("SSL Mode:  "); string ssl = Console.ReadLine();

            string plaintext =
                $"Host={host};" +
                $"Port={port};" +
                $"Database={database};" +
                $"Username={username};" +
                $"Password={password};" +
                $"SSL Mode={ssl};";

            Console.WriteLine();
            Console.WriteLine($"Connection string:\n{plaintext}");
            Console.Write("\nConfirm? (y/n): ");

            if (Console.ReadLine()?.Trim().ToLower() != "y")
            {
                Console.WriteLine("Cancelled.");
                return;
            }

            Save(plaintext);
            Console.WriteLine($"\nSaved to:\n{_configPath}");
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        private static void Save(string plaintext)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);

            byte[] encrypted = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(plaintext),
                _entropy,
                DataProtectionScope.LocalMachine);

            File.WriteAllBytes(_configPath, encrypted);
        }

        private static string GetArg(string[] args, string key)
        {
            int index = Array.IndexOf(args, key);
            if (index < 0 || index + 1 >= args.Length)
                throw new ArgumentException($"Missing argument: {key}");
            return args[index + 1];
        }

        private static string ReadPasswordMasked()
        {
            var sb = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Enter)
                {
                    sb.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return sb.ToString();
        }
    }
}