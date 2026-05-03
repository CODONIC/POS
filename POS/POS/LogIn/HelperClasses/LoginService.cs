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
        public string ErrorMessage { get; set; }
        public ControlToFocus FocusTarget { get; set; }
    }

    public enum ControlToFocus
    {
        None,
        Username,
        Password,
        Company
    }

    public class LoginService
    {
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

                // Validate user
                var userData = await ValidateUserAsync(conn, username, company);
                if (userData == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Username not found under the specified company.";
                    result.FocusTarget = ControlToFocus.Username;
                    return result;
                }

                // Validate password
                if (userData.StoredPassword != password)
                {
                    result.Success = false;
                    result.ErrorMessage = "Incorrect password.";
                    result.FocusTarget = ControlToFocus.Password;
                    return result;
                }

                // Success
                result.Success = true;
                result.Role = userData.Role;
                result.CompanyName = userData.CompanyName;
                result.CompanyId = companyId;

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

        private async Task<string> ValidateCompanyAsync(NpgsqlConnection conn, string company)
        {
            const string sql = "SELECT id FROM companies WHERE LOWER(name) = LOWER(@company)";
            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@company", company);
            return (await cmd.ExecuteScalarAsync())?.ToString();
        }

        private async Task<UserValidationResult> ValidateUserAsync(NpgsqlConnection conn, string username, string company)
        {
            const string sql = @"
                SELECT u.password, r.name AS role, c.name AS company_name
                FROM users u
                JOIN companies c ON u.company_id = c.id
                JOIN roles r ON u.role_id = r.id
                WHERE LOWER(u.username) = LOWER(@username)
                AND LOWER(c.name) = LOWER(@company)";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@company", company);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new UserValidationResult
            {
                StoredPassword = reader["password"].ToString(),
                Role = reader["role"].ToString(),
                CompanyName = reader["company_name"].ToString()
            };
        }

        private class UserValidationResult
        {
            public string StoredPassword { get; set; }
            public string Role { get; set; }
            public string CompanyName { get; set; }
        }
    }
}