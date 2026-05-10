using System;
using System.Threading.Tasks;
using Npgsql;

namespace POS
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Role { get; set; }
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string SessionToken { get; set; }
        public string ErrorMessage { get; set; }
        public ControlToFocus FocusTarget { get; set; }
        public bool IsLockedOut { get; set; }
        public int RemainingSeconds { get; set; }
        public int RemainingAttempts { get; set; }
    }
    public enum ControlToFocus
    {
        None,
        Username,
        Password,
        Company
    }
    public class SessionInfo
    {
        public bool HasActiveSession { get; set; }
        public string DeviceInfo { get; set; }
        public DateTime LoginTime { get; set; }
    }

    public class UserValidationResult
    {
        public string UserId { get; set; }
        public string StoredPassword { get; set; }
        public string Role { get; set; }
        public string CompanyName { get; set; }
    }

    public class LoginService
    {
        private readonly LoginLockoutService _lockoutService = new LoginLockoutService();

        public async Task<LoginResult> AuthenticateAsync(string username, string password, string company)
        {
            var result = new LoginResult();

            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Validate company
                var companyId = await ValidateCompanyAsync(conn, company);
                if (companyId == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Company name not found.";
                    result.FocusTarget = ControlToFocus.Company;
                    return result;
                }

                // CHECK LOCKOUT STATUS FIRST
                var lockoutInfo = await _lockoutService.CheckLockoutStatusAsync(username, companyId);

                if (lockoutInfo.IsLockedOut)
                {
                    result.Success = false;
                    result.IsLockedOut = true;
                    result.RemainingSeconds = lockoutInfo.RemainingSeconds;
                    result.ErrorMessage = $"Account is temporarily locked.\n\nPlease try again in {lockoutInfo.RemainingSeconds / 60} minute(s) and {lockoutInfo.RemainingSeconds % 60} second(s).";
                    result.FocusTarget = ControlToFocus.None;
                    return result;
                }

                // Validate user and get user data
                var userData = await ValidateUserAsync(conn, username, company);
                if (userData == null)
                {
                    // Record failed attempt
                    await _lockoutService.RecordFailedAttemptAsync(username, companyId);
                    var newLockoutInfo = await _lockoutService.CheckLockoutStatusAsync(username, companyId);

                    if (newLockoutInfo.IsLockedOut)
                    {
                        result.Success = false;
                        result.IsLockedOut = true;
                        result.RemainingSeconds = newLockoutInfo.RemainingSeconds;
                        result.ErrorMessage = $"Too many failed attempts. Account is locked for {_lockoutService.GetLockoutMinutes()} minutes.";
                        result.FocusTarget = ControlToFocus.Username;
                        return result;
                    }

                    result.Success = false;
                    result.ErrorMessage = $"Username not found under the specified company.\n\nAttempts remaining: {newLockoutInfo.RemainingAttempts}";
                    result.FocusTarget = ControlToFocus.Username;
                    result.RemainingAttempts = newLockoutInfo.RemainingAttempts;
                    return result;
                }

                // Validate password
                if (userData.StoredPassword != password)
                {
                    // Record failed attempt
                    await _lockoutService.RecordFailedAttemptAsync(username, companyId);
                    var newLockoutInfo = await _lockoutService.CheckLockoutStatusAsync(username, companyId);

                    if (newLockoutInfo.IsLockedOut)
                    {
                        result.Success = false;
                        result.IsLockedOut = true;
                        result.RemainingSeconds = newLockoutInfo.RemainingSeconds;
                        result.ErrorMessage = $"Too many failed attempts. Account is locked for {_lockoutService.GetLockoutMinutes()} minutes.";
                        result.FocusTarget = ControlToFocus.Password;
                        return result;
                    }

                    result.Success = false;
                    result.ErrorMessage = $"Incorrect password.\n\nAttempts remaining: {newLockoutInfo.RemainingAttempts}";
                    result.FocusTarget = ControlToFocus.Password;
                    result.RemainingAttempts = newLockoutInfo.RemainingAttempts;
                    return result;
                }

                // SUCCESSFUL LOGIN - Reset lockout
                await _lockoutService.ResetLockoutAsync(username, companyId);

                // Check for existing active session
                var sessionCheck = await CheckExistingSessionAsync(conn, userData.UserId);

                if (sessionCheck.HasActiveSession)
                {
                    result.Success = false;
                    result.ErrorMessage = "You are already logged in on another device.\n\n" +
                                         "Please log out from the other device first before logging in here.\n\n" +
                                         $"Device: {sessionCheck.DeviceInfo}\n" +
                                         $"Login time: {sessionCheck.LoginTime:yyyy-MM-dd HH:mm:ss}";
                    result.FocusTarget = ControlToFocus.None;
                    return result;
                }

                // Create new session
                string sessionToken = Guid.NewGuid().ToString();
                await CreateSessionAsync(conn, userData.UserId, sessionToken, Environment.MachineName);

                // Success
                result.Success = true;
                result.Role = userData.Role;
                result.CompanyName = userData.CompanyName;
                result.CompanyId = companyId;
                result.UserId = userData.UserId;
                result.SessionToken = sessionToken;

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Login failed: {ex.Message}";
                result.FocusTarget = ControlToFocus.None;
                return result;
            }
        }

        public async Task<bool> ValidateSessionAsync(string userId, string sessionToken)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(sessionToken))
                    return false;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = @"
                    SELECT COUNT(*) FROM public.user_sessions 
                    WHERE user_id = @userId AND session_token = @sessionToken AND is_active = true";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", Guid.Parse(userId));
                cmd.Parameters.AddWithValue("@sessionToken", Guid.Parse(sessionToken));

                long count = (long)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Session validation error: {ex.Message}");
                return false;
            }
        }

        public async Task LogoutSessionAsync(string userId, string sessionToken)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(sessionToken))
                    return;

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = @"
                    UPDATE public.user_sessions 
                    SET is_active = false 
                    WHERE user_id = @userId AND session_token = @sessionToken";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", Guid.Parse(userId));
                cmd.Parameters.AddWithValue("@sessionToken", Guid.Parse(sessionToken));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Session logout error: {ex.Message}");
            }
        }

        public async Task TerminateAllSessionsAsync(string userId)
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = "UPDATE public.user_sessions SET is_active = false WHERE user_id = @userId";
                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", Guid.Parse(userId));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Session termination error: {ex.Message}");
            }
        }

        public async Task<LockoutInfo> GetLockoutStatusAsync(string username, string companyId)
        {
            return await _lockoutService.CheckLockoutStatusAsync(username, companyId);
        }

        public int GetMaxAttempts()
        {
            return _lockoutService.GetMaxAttempts();
        }

        public int GetLockoutMinutes()
        {
            return _lockoutService.GetLockoutMinutes();
        }

        private async Task<string> ValidateCompanyAsync(NpgsqlConnection conn, string company)
        {
            const string sql = "SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@company)";
            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@company", company);
            return (await cmd.ExecuteScalarAsync())?.ToString();
        }

        private async Task<UserValidationResult> ValidateUserAsync(NpgsqlConnection conn, string username, string company)
        {
            const string sql = @"
                SELECT u.id, u.password, r.name AS role, c.name AS company_name
                FROM public.users u
                JOIN public.companies c ON u.company_id = c.id
                JOIN public.roles r ON u.role_id = r.id
                WHERE LOWER(u.username) = LOWER(@username) AND LOWER(c.name) = LOWER(@company)";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@company", company);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new UserValidationResult
            {
                UserId = reader["id"].ToString(),
                StoredPassword = reader["password"].ToString(),
                Role = reader["role"].ToString(),
                CompanyName = reader["company_name"].ToString()
            };
        }


        private async Task<SessionInfo> CheckExistingSessionAsync(NpgsqlConnection conn, string userId)
        {
            const string sql = @"
                SELECT device_info, login_time 
                FROM public.user_sessions 
                WHERE user_id = @userId AND is_active = true 
                LIMIT 1";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", Guid.Parse(userId));

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new SessionInfo
                {
                    HasActiveSession = true,
                    DeviceInfo = reader["device_info"]?.ToString() ?? "Unknown device",
                    LoginTime = Convert.ToDateTime(reader["login_time"])
                };
            }

            return new SessionInfo { HasActiveSession = false };
        }

        private async Task CreateSessionAsync(NpgsqlConnection conn, string userId, string sessionToken, string deviceInfo)
        {
            const string sql = @"
                INSERT INTO public.user_sessions (user_id, session_token, device_info, login_time, last_activity, is_active)
                VALUES (@userId, @sessionToken, @deviceInfo, @loginTime, @lastActivity, true)";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", Guid.Parse(userId));
            cmd.Parameters.AddWithValue("@sessionToken", Guid.Parse(sessionToken));
            cmd.Parameters.AddWithValue("@deviceInfo", deviceInfo ?? "Unknown");
            cmd.Parameters.AddWithValue("@loginTime", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@lastActivity", DateTime.UtcNow);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}