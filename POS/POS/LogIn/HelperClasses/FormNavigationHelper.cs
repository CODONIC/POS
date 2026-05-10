using POS.Inventory_Manager;
using System.Windows.Forms;

namespace POS
{
    public static class FormNavigationHelper
    {
        public static void NavigateToDashboard(string role, string username, string companyName,
                                               string userId, string sessionToken, Form currentForm)
        {
            Form dashboard = role switch
            {
                "ADMIN" => new AdminDashboard(username, companyName, userId, sessionToken),
                "CASHIER" => new CashierDashboard(username, companyName, userId, sessionToken),
                "INVENTORY MANAGER" => new InventoryManagerDashboard(username, companyName, userId, sessionToken),
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