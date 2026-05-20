using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Npgsql;

namespace POS
{
    public static class CrashRecoveryService
    {
        private static readonly List<ActiveSession> _activeSessions = new List<ActiveSession>();
        private static System.Timers.Timer _heartbeatTimer;
        private static bool _isCleaningUp = false;

        public class ActiveSession
        {
            public string UserId { get; set; }
            public string SessionToken { get; set; }
            public DateTime StartedAt { get; set; }
            public DateTime LastHeartbeat { get; set; }
        }

        public static void RegisterActiveSession(string userId, string sessionToken)
        {
            var session = new ActiveSession
            {
                UserId = userId,
                SessionToken = sessionToken,
                StartedAt = DateTime.Now,
                LastHeartbeat = DateTime.Now
            };

            _activeSessions.Add(session);
            StartHeartbeat();
            Debug.WriteLine($"Session registered for crash recovery: User {userId}");
        }

        public static void UnregisterActiveSession(string sessionToken)
        {
            var session = _activeSessions.Find(s => s.SessionToken == sessionToken);
            if (session != null)
            {
                _activeSessions.Remove(session);
                Debug.WriteLine($"Session unregistered from crash recovery");
            }

            if (_activeSessions.Count == 0)
            {
                StopHeartbeat();
            }
        }

        private static void StartHeartbeat()
        {
            if (_heartbeatTimer == null)
            {
                _heartbeatTimer = new System.Timers.Timer(30000); // Every 30 seconds
                _heartbeatTimer.Elapsed += async (s, e) => await UpdateHeartbeatAsync();
                _heartbeatTimer.Start();
            }
        }

        private static void StopHeartbeat()
        {
            if (_heartbeatTimer != null)
            {
                _heartbeatTimer.Stop();
                _heartbeatTimer.Dispose();
                _heartbeatTimer = null;
            }
        }

        private static async Task UpdateHeartbeatAsync()
        {
            foreach (var session in _activeSessions)
            {
                session.LastHeartbeat = DateTime.Now;

                try
                {
                    using var conn = DatabaseService.GetConnection();
                    await conn.OpenAsync();

                    const string sql = @"
                        UPDATE public.user_sessions 
                        SET last_activity = @lastActivity 
                        WHERE user_id = @userId AND session_token = @sessionToken AND is_active = true";

                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", Guid.Parse(session.UserId));
                    cmd.Parameters.AddWithValue("@sessionToken", Guid.Parse(session.SessionToken));
                    cmd.Parameters.AddWithValue("@lastActivity", DateTime.UtcNow);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Heartbeat update failed: {ex.Message}");
                }
            }
        }

        public static async Task TerminateAllSessionsOnCrashAsync()
        {
            if (_isCleaningUp) return;
            _isCleaningUp = true;

            Debug.WriteLine($"Crash recovery: Terminating {_activeSessions.Count} active sessions");

            var sessionsToTerminate = new List<ActiveSession>(_activeSessions);

            foreach (var session in sessionsToTerminate)
            {
                try
                {
                    await TerminateSessionAsync(session.UserId, session.SessionToken);
                    Debug.WriteLine($"Session terminated for user {session.UserId}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to terminate session: {ex.Message}");
                }
            }

            _activeSessions.Clear();
            _isCleaningUp = false;
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error terminating session: {ex.Message}");
            }
        }

        // Call this at app startup to clean up stale sessions
        public static async Task CleanupStaleSessionsAsync()
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Terminate sessions with no heartbeat for > 2 minutes
                const string sql = @"
                    UPDATE public.user_sessions 
                    SET is_active = false 
                    WHERE is_active = true 
                    AND last_activity < NOW() - INTERVAL '2 minutes'";

                using var cmd = new NpgsqlCommand(sql, conn);
                int updated = await cmd.ExecuteNonQueryAsync();

                if (updated > 0)
                {
                    Debug.WriteLine($"Cleaned up {updated} stale sessions");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Stale session cleanup failed: {ex.Message}");
            }
        }

        public static int GetActiveSessionCount()
        {
            return _activeSessions.Count;
        }
    }
}