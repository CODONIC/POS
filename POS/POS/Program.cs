using POS.StartUpForms;

namespace POS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            await DatabaseService.InitializeAsync();
            if (Properties.Settings.Default.DontShowWelcome)
            {
                Application.Run(new LogInForm()); // Skip welcome, go straight to login if user checked the box to skip it
            }
            else
            {
                Application.Run(new WelcomeFrm()); // Show welcome if not set to skip
            }
            //Application.Run(new LogInForm());
            //Application.Run(new CashierDashboard()); 
            //Application.Run(new AdminDashboard());
        }
    }
}