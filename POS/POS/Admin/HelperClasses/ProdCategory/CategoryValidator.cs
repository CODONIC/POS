using System;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class CategoryValidator
    {
        public static ValidationResult ValidateCategoryName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new ValidationResult(false, "Please enter a category name.");

            return new ValidationResult(true, null);
        }

        public static ValidationResult ValidateSelection(string selectedId)
        {
            if (string.IsNullOrEmpty(selectedId))
                return new ValidationResult(false, "Please select a category to edit or delete.");

            return new ValidationResult(true, null);
        }

        public static DialogResult ConfirmDelete(string categoryName)
        {
            return MessageBox.Show(
                $"Are you sure you want to delete \"{categoryName}\"?\nThis cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}