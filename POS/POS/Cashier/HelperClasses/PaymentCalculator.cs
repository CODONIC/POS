using System;

namespace POS.Cashier
{
    public class PaymentCalculator
    {
        private readonly decimal _originalSubtotal;

        public decimal Subtotal { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal VatableAmount { get; private set; }
        public decimal VatAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public PaymentCalculator(decimal originalSubtotal, decimal initialDiscountPercentage = 0)
        {
            _originalSubtotal = originalSubtotal;
            Recalculate(initialDiscountPercentage);
        }

        public void Recalculate(decimal discountPercentage)
        {
            DiscountPercentage = Math.Clamp(discountPercentage, 0, 100);
            Subtotal = _originalSubtotal;
            DiscountAmount = _originalSubtotal * (DiscountPercentage / 100m);

            decimal discountedAmount = _originalSubtotal - DiscountAmount;
            VatableAmount = discountedAmount / 1.12m;
            VatAmount = VatableAmount * 0.12m;
            TotalAmount = discountedAmount;
        }

        public decimal CalculateChange(decimal customerPayment) => Math.Max(customerPayment - TotalAmount, 0);
        public bool IsPaymentSufficient(decimal customerPayment) => customerPayment >= TotalAmount;
    }
}