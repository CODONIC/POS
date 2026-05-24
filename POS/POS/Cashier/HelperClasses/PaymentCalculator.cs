using System;

namespace POS.Cashier
{
    public class PaymentCalculator
{
    private readonly decimal _vatRate;
    public decimal DiscountPercentage { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal VatableAmount { get; private set; }
    public decimal VatAmount { get; private set; }
    public decimal TotalAmount { get; private set; }

    private readonly decimal _originalSubtotal;

    public PaymentCalculator(decimal subtotal, decimal discountPercentage, decimal vatRate = 12m)
    {
        _originalSubtotal = subtotal;
        _vatRate = vatRate; // store it just like ReceiptGenerator does
        DiscountPercentage = discountPercentage;
        Recalculate(discountPercentage);
    }

    public void Recalculate(decimal discountPercentage)
    {
        DiscountPercentage = discountPercentage;
        DiscountAmount = _originalSubtotal * (discountPercentage / 100m);
        decimal discountedSubtotal = _originalSubtotal - DiscountAmount;

        // Use _vatRate just like ReceiptGenerator uses _vatRate
        VatableAmount = discountedSubtotal / (1 + _vatRate / 100m);
        VatAmount = VatableAmount * (_vatRate / 100m);
        TotalAmount = VatableAmount + VatAmount;
    }

    public decimal CalculateChange(decimal payment) => payment - TotalAmount;
    public bool IsPaymentSufficient(decimal payment) => payment >= TotalAmount;
}
}