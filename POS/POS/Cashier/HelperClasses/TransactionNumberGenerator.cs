using System;

namespace POS.Cashier
{
    public static class TransactionNumberGenerator
    {
        private static readonly Random _random = new Random();

        public static string Generate()
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string timePart = DateTime.Now.ToString("HHmmss");
            string randomPart = _random.Next(100, 999).ToString();
            return $"TXN-{datePart}-{timePart}-{randomPart}";
        }
    }
}