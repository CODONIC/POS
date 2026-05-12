using System;
using System.Threading.Tasks;
using Npgsql;

namespace POS
{
    public static class CrashRecoveryService
    {
        // Track active sessions for this app instance
        private static readonly List<ActiveSession> _activeSessions = new List<ActiveSession>();

        public class ActiveSession
        {
            public string UserId { get; set; }
            public string SessionToken { get; set; }
            public DateTime StartedAt { get; set; }
        }

        public static void RegisterActiveSession(string userId, string sessionToken)
        {
            var session = new ActiveSession
            {
                UserId = userId,
                SessionToken = sessionToken,
                StartedAt = DateTime.Now
            };

            _activeSessions.Add(session);
        }

        public static void UnregisterActiveSession(string sessionToken)
        {
            var session = _activeSessions.FirstOrDefault(s => s.SessionToken == sessionToken);
            if (session != null)
            {
                _activeSessions.Remove(session);
            }
        }

        public static async Task TerminateAllSessionsOnCrashAsync()
        {
            foreach (var session in _activeSessions)
            {
                try
                {
                    await TerminateSessionAsync(session.UserId, session.SessionToken);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to terminate session: {ex.Message}");
                }
            }

            _activeSessions.Clear();
        }

        private static async Task TerminateSessionAsync(string userId, string sessionToken)
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = @"
                    UPDATE public.user_sessions 
                    SET is_active = false 
                    WHERE user_id = @userId AND session_token = @sessionToken";

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", Guid.Parse(userId));
                cmd.Parameters.AddWithValue("@sessionToken", Guid.Parse(sessionToken));
                await cmd.ExecuteNonQueryAsync();

                System.Diagnostics.Debug.WriteLine($"Session terminated for user {userId} due to crash recovery");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error terminating session: {ex.Message}");
            }
        }
    }
}