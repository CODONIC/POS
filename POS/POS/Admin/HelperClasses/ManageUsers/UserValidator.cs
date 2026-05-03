using System;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class UserValidator
    {
        public static ValidationResult ValidateFields(string username, string lastName, string firstName,
                                                      object roleSelected, string age, string password = null, bool isAdd = false)
        {
            if (string.IsNullOrWhiteSpace(username))
                return new ValidationResult(false, "Username is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                return new ValidationResult(false, "Last Name is required.");

            if (string.IsNullOrWhiteSpace(firstName))
                return new ValidationResult(false, "First Name is required.");

            if (roleSelected == null)
                return new ValidationResult(false, "Please select a User Level (ADMIN or CASHIER).");

            if (isAdd && string.IsNullOrWhiteSpace(password))
                return new ValidationResult(false, "Password is required when adding a new user.");

            if (!string.IsNullOrWhiteSpace(age) && !int.TryParse(age, out _))
                return new ValidationResult(false, "Age must be a valid number.");

            return new ValidationResult(true, null);
        }

        public static DialogResult ConfirmDelete(string username)
        {
            return MessageBox.Show(
                $"Are you sure you want to delete \"{username}\"?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public static DialogResult ConfirmCancel()
        {
            return MessageBox.Show(
                "Are you sure you want to cancel? Unsaved changes will be lost.",
                "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}