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

        public void CalculateAmounts(DataTable cartTable, decimal discountPercentage = 0)
        {
            DiscountPercentage = discountPercentage;
            Subtotal = cartTable.AsEnumerable()
                .Sum(r => Convert.ToDecimal(r["subtotal"]));

            DiscountAmount = Subtotal * (DiscountPercentage / 100m);
            decimal discountedAmount = Subtotal - DiscountAmount;

            // VAT-INCLUSIVE breakdown (BIR standard)
            VatableAmount = discountedAmount / 1.12m;
            VatAmount = VatableAmount * 0.12m;
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
        }
    }
}