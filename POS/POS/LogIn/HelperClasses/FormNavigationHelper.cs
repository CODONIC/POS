using System.Windows.Forms;

namespace POS
{
    public static class FormNavigationHelper
    {
        public static void NavigateToDashboard(string role, string username, string companyName, Form currentForm)
        {
            Form dashboard = role switch
            {
                "ADMIN" => new AdminDashboard(username, companyName),
                "CASHIER" => new CashierDashboard(username, companyName),
                _ => null
            };

            if (dashboard != null)
            {
                dashboard.Show();
                currentForm.Hide();
            }
            else
            {
                MessageBox.Show("Unknown role.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}