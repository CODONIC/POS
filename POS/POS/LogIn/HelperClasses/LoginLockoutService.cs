using System;
using System.Threading.Tasks;
using Npgsql;

namespace POS
{
    public class LockoutInfo
    {
        public bool IsLockedOut { get; set; }
        public int AttemptCount { get; set; }
        public DateTime? LockoutUntil { get; set; }
        public int RemainingSeconds { get; set; }
        public int RemainingAttempts { get; set; }
    }

    public class LoginLockoutService
    {
        private readonly int _maxAttempts = 5;
        private readonly int _lockoutMinutes = 5;

        public async Task<LockoutInfo> CheckLockoutStatusAsync(string username, string companyId)
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = @"
                    SELECT attempt_count, lockout_until 
                    FROM public.login_lockouts 
                    WHERE LOWER(username) = LOWER(@username) AND company_id = @companyId";

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@companyId", Guid.Parse(companyId));

                await using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    int attemptCount = reader.GetInt32(0);
                    DateTime? lockoutUntil = reader.IsDBNull(1) ? null : reader.GetDateTime(1);

                    // Check if lockout has expired
                    if (lockoutUntil.HasValue && DateTime.UtcNow >= lockoutUntil.Value)
                    {
                        // Lockout expired, delete record
                        await ResetLockoutAsync(username, companyId);
                        return new LockoutInfo
                        {
                            IsLockedOut = false,
                            AttemptCount = 0,
                            RemainingAttempts = _maxAttempts
                        };
                    }

                    if (lockoutUntil.HasValue && DateTime.UtcNow < lockoutUntil.Value)
                    {
                        var remaining = lockoutUntil.Value - DateTime.UtcNow;
                        return new LockoutInfo
                        {
                            IsLockedOut = true,
                            AttemptCount = attemptCount,
                            LockoutUntil = lockoutUntil,
                            RemainingSeconds = Math.Max(0, (int)remaining.TotalSeconds),
                            RemainingAttempts = 0
                        };
                    }

                    return new LockoutInfo
                    {
                        IsLockedOut = false,
                        AttemptCount = attemptCount,
                        RemainingAttempts = Math.Max(0, _maxAttempts - attemptCount)
                    };
                }

                return new LockoutInfo
                {
                    IsLockedOut = false,
                    AttemptCount = 0,
                    RemainingAttempts = _maxAttempts
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CheckLockoutStatus error: {ex.Message}");
                return new LockoutInfo
                {
                    IsLockedOut = false,
                    AttemptCount = 0,
                    RemainingAttempts = _maxAttempts
                };
            }
        }

        public async Task RecordFailedAttemptAsync(string username, string companyId)
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                // Check if record exists
                const string checkSql = @"
                    SELECT attempt_count, lockout_until 
                    FROM public.login_lockouts 
                    WHERE LOWER(username) = LOWER(@username) AND company_id = @companyId";

                await using var checkCmd = new NpgsqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@username", username);
                checkCmd.Parameters.AddWithValue("@companyId", Guid.Parse(companyId));

                await using var reader = await checkCmd.ExecuteReaderAsync();
                bool exists = await reader.ReadAsync();
                int currentAttempts = exists ? reader.GetInt32(0) : 0;
                DateTime? existingLockoutUntil = exists && !reader.IsDBNull(1) ? reader.GetDateTime(1) : null;

                await reader.CloseAsync();

                int newAttempts = currentAttempts + 1;

                if (newAttempts >= _maxAttempts)
                {
                    // Lock the account
                    DateTime lockoutUntil = DateTime.UtcNow.AddMinutes(_lockoutMinutes);

                    const string upsertSql = @"
                        INSERT INTO public.login_lockouts (username, company_id, attempt_count, last_attempt_time, lockout_until)
                        VALUES (@username, @companyId, @attemptCount, @lastAttempt, @lockoutUntil)
                        ON CONFLICT (username, company_id) 
                        DO UPDATE SET 
                            attempt_count = @attemptCount,
                            last_attempt_time = @lastAttempt,
                            lockout_until = @lockoutUntil";

                    await using var upsertCmd = new NpgsqlCommand(upsertSql, conn);
                    upsertCmd.Parameters.AddWithValue("@username", username.ToLower());
                    upsertCmd.Parameters.AddWithValue("@companyId", Guid.Parse(companyId));
                    upsertCmd.Parameters.AddWithValue("@attemptCount", newAttempts);
                    upsertCmd.Parameters.AddWithValue("@lastAttempt", DateTime.UtcNow);
                    upsertCmd.Parameters.AddWithValue("@lockoutUntil", lockoutUntil);
                    await upsertCmd.ExecuteNonQueryAsync();
                }
                else
                {
                    // Just update attempt count
                    const string updateSql = @"
                        INSERT INTO public.login_lockouts (username, company_id, attempt_count, last_attempt_time)
                        VALUES (@username, @companyId, @attemptCount, @lastAttempt)
                        ON CONFLICT (username, company_id) 
                        DO UPDATE SET 
                            attempt_count = EXCLUDED.attempt_count,
                            last_attempt_time = EXCLUDED.last_attempt_time";

                    await using var updateCmd = new NpgsqlCommand(updateSql, conn);
                    updateCmd.Parameters.AddWithValue("@username", username.ToLower());
                    updateCmd.Parameters.AddWithValue("@companyId", Guid.Parse(companyId));
                    updateCmd.Parameters.AddWithValue("@attemptCount", newAttempts);
                    updateCmd.Parameters.AddWithValue("@lastAttempt", DateTime.UtcNow);
                    await updateCmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"RecordFailedAttempt error: {ex.Message}");
            }
        }

        public async Task ResetLockoutAsync(string username, string companyId)
        {
            try
            {
                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                const string sql = "DELETE FROM public.login_lockouts WHERE LOWER(username) = LOWER(@username) AND company_id = @companyId";
                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@companyId", Guid.Parse(companyId));
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ResetLockout error: {ex.Message}");
            }
        }

        public int GetMaxAttempts() => _maxAttempts;
        public int GetLockoutMinutes() => _lockoutMinutes;
    }
}