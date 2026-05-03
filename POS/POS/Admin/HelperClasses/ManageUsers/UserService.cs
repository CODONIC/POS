using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace POS.Admin
{
    public class UserService
    {
        private readonly string _companyId;
        private readonly string _connectionString;

        public UserService(string companyId)
        {
            _companyId = companyId;
            _connectionString = DatabaseService.ConnectionString;
        }

        public async Task<DataTable> GetUsersAsync(string search = "")
        {
            var dt = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = @"
                SELECT u.id, u.username, u.last_name, u.first_name, u.middle_name,
                       u.contact_number, u.age, u.birthdate, r.name AS role
                FROM public.users u
                JOIN public.roles r ON u.role_id = r.id
                WHERE u.company_id = @companyId
                  AND (u.username ILIKE @search OR u.last_name ILIKE @search 
                       OR u.first_name ILIKE @search OR u.contact_number ILIKE @search)
                ORDER BY u.last_name, u.first_name";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            cmd.Parameters.AddWithValue("@search", $"%{search}%");

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(
                "SELECT COUNT(*) FROM public.users WHERE username = @username AND company_id = @companyId", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));

            return (long)await cmd.ExecuteScalarAsync() > 0;
        }

        public async Task<DataTable> GetUserOldValuesAsync(string userId)
        {
            var dt = new DataTable();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(
                @"SELECT u.username, u.first_name, u.last_name, u.middle_name,
                         u.contact_number, u.age, r.name AS role
                  FROM public.users u
                  JOIN public.roles r ON u.role_id = r.id
                  WHERE u.id = @id", conn);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(userId));

            using var adapter = new NpgsqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }

        public async Task AddUserAsync(string username, string password, string role, string firstName,
                                       string lastName, string middleName, string contact, int? age, DateTime birthdate)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = @"
                INSERT INTO public.users
                    (username, password, role_id, first_name, last_name,
                     middle_name, contact_number, age, birthdate, company_id)
                VALUES
                    (@username, @password, (SELECT id FROM public.roles WHERE name = @role), 
                     @first_name, @last_name, @middle_name, @contact_number, @age, @birthdate, @companyId)";

            using var cmd = new NpgsqlCommand(query, conn);
            AddUserParameters(cmd, username, password, role, firstName, lastName, middleName, contact, age, birthdate);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateUserAsync(string userId, string username, string password, string role,
                                          string firstName, string lastName, string middleName,
                                          string contact, int? age, DateTime birthdate, bool changePassword)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = changePassword
                ? @"UPDATE public.users SET username = @username, password = @password,
                       role_id = (SELECT id FROM public.roles WHERE name = @role),
                       first_name = @first_name, last_name = @last_name, middle_name = @middle_name,
                       contact_number = @contact_number, age = @age, birthdate = @birthdate
                    WHERE id = @id"
                : @"UPDATE public.users SET username = @username,
                       role_id = (SELECT id FROM public.roles WHERE name = @role),
                       first_name = @first_name, last_name = @last_name, middle_name = @middle_name,
                       contact_number = @contact_number, age = @age, birthdate = @birthdate
                    WHERE id = @id";

            using var cmd = new NpgsqlCommand(query, conn);
            AddUserParameters(cmd, username, changePassword ? password : null, role, firstName,
                             lastName, middleName, contact, age, birthdate, changePassword);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(userId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("DELETE FROM public.users WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", Guid.Parse(userId));
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<string>> GetRolesAsync()
        {
            var roles = new List<string>();
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand("SELECT name FROM public.roles ORDER BY name", conn);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                roles.Add(reader["name"].ToString());

            return roles;
        }

        private void AddUserParameters(NpgsqlCommand cmd, string username, string password, string role,
                                       string firstName, string lastName, string middleName, string contact,
                                       int? age, DateTime birthdate, bool includePassword = true)
        {
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@first_name", firstName);
            cmd.Parameters.AddWithValue("@last_name", lastName);
            cmd.Parameters.AddWithValue("@middle_name", string.IsNullOrEmpty(middleName) ? DBNull.Value : (object)middleName);
            cmd.Parameters.AddWithValue("@contact_number", string.IsNullOrEmpty(contact) ? DBNull.Value : (object)contact);
            cmd.Parameters.AddWithValue("@age", age.HasValue ? (object)age.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@birthdate", birthdate);
            cmd.Parameters.AddWithValue("@companyId", Guid.Parse(_companyId));
            if (includePassword && !string.IsNullOrEmpty(password))
                cmd.Parameters.AddWithValue("@password", password);
        }
    }
}