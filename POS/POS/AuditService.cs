using System;
using System.Threading.Tasks;
using Npgsql;

namespace POS
{
    public static class AuditService
    {
        // ─────────────────────────────────────────────
        //  LOGIN
        // ─────────────────────────────────────────────
        public static async Task LogLoginAsync(string username, string companyId, string ipAddress = null, string deviceInfo = null)
        {
            await InsertAsync(
                username: username,
                companyId: companyId,
                actionType: "LOGIN",
                category: "AUTH",
                tableName: null,
                recordId: null,
                oldValues: null,
                newValues: null,
                ipAddress: ipAddress,
                deviceInfo: deviceInfo,
                remarks: $"User '{username}' logged in."
            );
        }

        // ─────────────────────────────────────────────
        //  LOGOUT
        // ─────────────────────────────────────────────
        public static async Task LogLogoutAsync(string username, string companyId, string ipAddress = null, string deviceInfo = null)
        {
            await InsertAsync(
                username: username,
                companyId: companyId,
                actionType: "LOGOUT",
                category: "AUTH",
                tableName: null,
                recordId: null,
                oldValues: null,
                newValues: null,
                ipAddress: ipAddress,
                deviceInfo: deviceInfo,
                remarks: $"User '{username}' logged out."
            );
        }

        // ─────────────────────────────────────────────
        //  DATA CHANGES (INSERT / UPDATE / DELETE)
        // ─────────────────────────────────────────────
        public static async Task LogInsertAsync(string username, string companyId, string tableName, string recordId, string newValuesJson, string remarks = null)
        {
            await InsertAsync(
                username: username,
                companyId: companyId,
                actionType: "INSERT",
                category: "DATA_CHANGE",
                tableName: tableName,
                recordId: recordId,
                oldValues: null,
                newValues: newValuesJson,
                remarks: remarks ?? $"New record added to {tableName}."
            );
        }

        public static async Task LogUpdateAsync(string username, string companyId, string tableName, string recordId, string oldValuesJson, string newValuesJson, string remarks = null)
        {
            await InsertAsync(
                username: username,
                companyId: companyId,
                actionType: "UPDATE",
                category: "DATA_CHANGE",
                tableName: tableName,
                recordId: recordId,
                oldValues: oldValuesJson,
                newValues: newValuesJson,
                remarks: remarks ?? $"Record updated in {tableName}."
            );
        }

        public static async Task LogDeleteAsync(string username, string companyId, string tableName, string recordId, string oldValuesJson, string remarks = null)
        {
            await InsertAsync(
                username: username,
                companyId: companyId,
                actionType: "DELETE",
                category: "DATA_CHANGE",
                tableName: tableName,
                recordId: recordId,
                oldValues: oldValuesJson,
                newValues: null,
                remarks: remarks ?? $"Record deleted from {tableName}."
            );
        }

        // ─────────────────────────────────────────────
        //  CORE INSERT
        // ─────────────────────────────────────────────
        private static async Task InsertAsync(
            string username,
            string companyId,
            string actionType,
            string category,
            string tableName,
            string recordId,
            string oldValues,
            string newValues,
            string ipAddress = null,
            string deviceInfo = null,
            string remarks = null)
        {
            try
            {
                const string sql = @"
                    INSERT INTO public.audit_logs
                        (username, company_id, action_type, action_category,
                         table_name, record_id, old_values, new_values,
                         ip_address, device_info, remarks, created_at)
                    VALUES
                        (@username, @companyId, @actionType, @category,
                         @tableName, @recordId, @oldValues::jsonb, @newValues::jsonb,
                         @ipAddress, @deviceInfo, @remarks, now())";

                await using var conn = DatabaseService.GetConnection();
                await conn.OpenAsync();

                await using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", (object)username ?? DBNull.Value);
                cmd.Parameters.Add("@companyId", NpgsqlTypes.NpgsqlDbType.Uuid).Value =
                    !string.IsNullOrEmpty(companyId) ? Guid.Parse(companyId) : DBNull.Value;
                cmd.Parameters.AddWithValue("@actionType", (object)actionType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@category", (object)category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@tableName", (object)tableName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@recordId", (object)recordId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@oldValues", (object)oldValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@newValues", (object)newValues ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ipAddress", (object)ipAddress ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@deviceInfo", (object)deviceInfo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@remarks", (object)remarks ?? DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Never crash the app over a failed audit log
                System.Diagnostics.Debug.WriteLine($"[AuditService] Failed to write log: {ex.Message}");
            }
        }

        // ─────────────────────────────────────────────
        //  HELPER — build a simple JSON snapshot
        //  Usage: AuditService.ToJson(("price", "99.00"), ("name", "Burger"))
        // ─────────────────────────────────────────────
        public static string ToJson(params (string key, object value)[] fields)
        {
            var sb = new System.Text.StringBuilder("{");
            for (int i = 0; i < fields.Length; i++)
            {
                string val = fields[i].value == null ? "null" : $"\"{fields[i].value}\"";
                sb.Append($"\"{fields[i].key}\":{val}");
                if (i < fields.Length - 1) sb.Append(",");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}