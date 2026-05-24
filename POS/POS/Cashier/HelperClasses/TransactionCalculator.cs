using System;
using System.Data;
using System.Linq;

namespace POS.Cashier
{
    public class TransactionCalculator
    {
        public decimal Subtotal { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal VatableAmount { get; private set; }
        public decimal VatAmount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal VatRate { get; private set; }

        public void CalculateAmounts(DataTable cartTable, decimal discountPercentage = 0, decimal vatRate = 12m)
        {
            VatRate = vatRate;
            DiscountPercentage = discountPercentage;

            Subtotal = cartTable.AsEnumerable()
                .Sum(r => Convert.ToDecimal(r["subtotal"]));

            DiscountAmount = Subtotal * (DiscountPercentage / 100m);
            decimal discountedAmount = Subtotal - DiscountAmount;

            // VAT-INCLUSIVE breakdown (BIR standard)
            decimal vatMultiplier = vatRate / 100m;
            VatableAmount = discountedAmount / (1m + vatMultiplier);
            VatAmount = VatableAmount * vatMultiplier;
            TotalAmount = discountedAmount;
        }

        public void Reset()
        {
            Subtotal = 0;
            DiscountPercentage = 0;
            DiscountAmount = 0;
            VatableAmount = 0;
            VatAmount = 0;
            TotalAmount = 0;
            VatRate = 0;
        }
    }
}
