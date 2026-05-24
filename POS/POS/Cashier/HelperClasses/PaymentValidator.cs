using System.Windows.Forms;

namespace POS.Cashier
{
    public static class PaymentValidator
    {
        public static bool ValidatePayment(decimal customerPayment, decimal totalAmount, out string errorMessage)
        {
            if (customerPayment <= 0)
            {
                errorMessage = "Please enter a valid payment amount.";
                return false;
            }
            if (customerPayment < totalAmount)
            {
                errorMessage = $"Insufficient payment amount!\n\nTotal to Pay: ₱{totalAmount:F2}\nCustomer Payment: ₱{customerPayment:F2}";
                return false;
            }
            errorMessage = null;
            return true;
        }

        public static DialogResult ConfirmPayment(string transactionNumber, decimal subtotal, decimal discountPercentage,
            decimal discountAmount, decimal vatableAmount, decimal vatAmount, decimal totalAmount,
            string paymentMethod, decimal customerPayment, decimal change, decimal vatRate = 12m)
        {
            return MessageBox.Show(
                $"Payment Confirmation:\n\n" +
                $"Transaction #: {transactionNumber}\n" +
                $"Subtotal: ₱{subtotal:F2}\n" +
                $"Discount ({discountPercentage}%): -₱{discountAmount:F2}\n" +
                $"VATable Amount: ₱{vatableAmount:F2}\n" +
                $"VAT ({vatRate}%): ₱{vatAmount:F2}\n" +
                $"Total to Pay: ₱{totalAmount:F2}\n" +
                $"Payment Method: {paymentMethod}\n" +
                $"Customer Payment: ₱{customerPayment:F2}\n" +
                $"Change: ₱{change:F2}\n\n" +
                $"Proceed with payment?",
                "Confirm Payment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
        }
    }
}
