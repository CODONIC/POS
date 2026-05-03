using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace POS.Admin
{
    public class AuditLogService
    {
        private readonly string _companyId;

        public AuditLogService(string companyId)
        {
            _companyId = companyId;
        }

        public async Task<(int totalRecords, List<AuditLogRow> rows)> GetLogsAsync(
            DateTime fromDate, DateTime toDate, string categoryFilter,
            string actionFilter, string searchText, int pageSize, int offset)
        {
            var conditions = new List<string>
            {
                "a.company_id = @companyId",
                "a.created_at BETWEEN @fromDate AND @toDate"
            };

            if (categoryFilter != "All") conditions.Add("a.action_category = @category");
            if (actionFilter != "All") conditions.Add("a.action_type = @action");
            if (!string.IsNullOrEmpty(searchText))
                conditions.Add("(a.username ILIKE @search OR a.table_name ILIKE @search OR a.record_id ILIKE @search OR a.remarks ILIKE @search)");

            string where = string.Join(" AND ", conditions);

            using var conn = DatabaseService.GetConnection();
            await conn.OpenAsync();

            // Get total count
            using var countCmd = new NpgsqlCommand($"SELECT COUNT(*) FROM public.audit_logs a WHERE {where}", conn);
            AddParameters(countCmd, fromDate, toDate, categoryFilter, actionFilter, searchText);
            int totalRecords = Convert.ToInt32(await countCmd.ExecuteScalarAsync());

            // Get data
            using var cmd = new NpgsqlCommand($@"
                SELECT a.id, a.created_at, a.username, a.action_category, a.action_type, 
                       a.table_name, a.record_id, a.old_values, a.new_values, a.remarks
                FROM public.audit_logs a
                WHERE {where}
                ORDER BY a.created_at DESC
                LIMIT @limit OFFSET @offset", conn);

            AddParameters(cmd, fromDate, toDate, categoryFilter, actionFilter, searchText);
            cmd.Parameters.AddWithValue("@limit", pageSize);
            cmd.Parameters.AddWithValue("@offset", offset);

            var rows = new List<AuditLogRow>();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                rows.Add(new AuditLogRow
                {
                    Id = reader["id"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["created_at"]),
                    Username = reader["username"]?.ToString(),
                    Category = reader["action_category"]?.ToString(),
                    Action = reader["action_type"]?.ToString(),
                    TableName = reader["table_name"]?.ToString(),
                    RecordId = reader["record_id"]?.ToString(),
                    OldValues = reader["old_values"]?.ToString(),
                    NewValues = reader["new_values"]?.ToString(),
                    Remarks = reader["remarks"]?.ToString(),
                });
            }

            return (totalRecords, rows);
        }

        private void AddParameters(NpgsqlCommand cmd, DateTime fromDate, DateTime toDate,
                                   string categoryFilter, string actionFilter, string searchText)
        {
            cmd.Parameters.AddWithValue("@companyId", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.Parse(_companyId));
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);
            if (categoryFilter != "All") cmd.Parameters.AddWithValue("@category", categoryFilter);
            if (actionFilter != "All") cmd.Parameters.AddWithValue("@action", actionFilter);
            if (!string.IsNullOrEmpty(searchText)) cmd.Parameters.AddWithValue("@search", $"%{searchText}%");
        }
    }

    public class AuditLogRow
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string Remarks { get; set; }
    }
}