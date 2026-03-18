using Supabase;
using System;
using System.Windows.Forms;

namespace POS
{
    public static class SupabaseService
    {
        private static Supabase.Client _client;

        public static Supabase.Client Client => _client;

        public static async Task InitializeAsync()
        {
            try
            {
                var url = "https://tvmxjtgypuimgbshtpbf.supabase.co";
                var key = "sb_publishable_Fv1POfF2Fy0X4uAN9ec1mw_ZXlpLm6K";

                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true,
                    AutoRefreshToken = true
                };

                _client = new Supabase.Client(url, key, options);
                await _client.InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize Supabase: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}