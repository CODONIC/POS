using CustomControls;
using System;
using System.Data;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class StockValidator
    {
        public static ValidationResult ValidateStockSelection(DataGridView grid)
        {
            if (grid.SelectedRows.Count == 0)
            {
                return new ValidationResult(false, "Please select a product first.");
            }
            return new ValidationResult(true, null);
        }

        public static ValidationResult ValidateQuantity(CustomTextBox txtBox, string action, int maxQuantity = int.MaxValue)
        {
            if (!int.TryParse(txtBox.Text.Trim(), out int qty) || qty <= 0)
            {
                return new ValidationResult(false, $"Please enter a valid quantity to {action}.");
            }

            if (qty > maxQuantity)
            {
                return new ValidationResult(false, $"Cannot {action} more than {maxQuantity} units.");
            }

            return new ValidationResult(true, null);
        }

        public static ValidationResult ValidatePendingChanges(DataTable pendingChanges)
        {
            if (pendingChanges == null || pendingChanges.Rows.Count == 0)
            {
                return new ValidationResult(false, "No pending changes to save.");
            }
            return new ValidationResult(true, null);
        }

        public static bool ConfirmCancel(DataTable pendingChanges)
        {
            if (pendingChanges == null || pendingChanges.Rows.Count == 0)
                return true;

            var result = MessageBox.Show(
                "You have unsaved changes. Are you sure you want to cancel?",
                "Confirm Cancel",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }

        public static bool ConfirmBack(DataTable pendingChanges)
        {
            if (pendingChanges == null || pendingChanges.Rows.Count == 0)
                return true;

            var result = MessageBox.Show(
                "You have unsaved changes. Are you sure you want to go back?",
                "Unsaved Changes",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }
    }
}