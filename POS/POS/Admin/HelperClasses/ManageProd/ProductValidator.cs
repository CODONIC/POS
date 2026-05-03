using System;
using CustomControls;

namespace POS.Admin
{
    public static class ProductValidator
    {
        public static ValidationResult ValidateInputs(string productCode, string productName,
                                                       string priceText, string reorderText,
                                                       object categorySelected)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                return new ValidationResult(false, "Product code is required.");

            if (string.IsNullOrWhiteSpace(productName))
                return new ValidationResult(false, "Product name is required.");

            if (!decimal.TryParse(priceText, out decimal price) || price < 0)
                return new ValidationResult(false, "Please enter a valid price (0 or greater).");

            if (!int.TryParse(reorderText, out int reorder) || reorder < 0)
                return new ValidationResult(false, "Please enter a valid reorder level (0 or greater).");

            if (categorySelected == null)
                return new ValidationResult(false, "Please select a category.");

            return new ValidationResult(true, null);
        }
    }
}