using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class AuditLogExporter
    {
        public static void ExportToCsv(List<AuditLogRow> rows, string companyName)
        {
            if (rows == null || rows.Count == 0)
            {
                MessageBox.Show("No records to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"AuditLog_{companyName}_{DateTime.Today:yyyyMMdd}.csv"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("\"Timestamp\",\"Employee\",\"Category\",\"Action\",\"Table Affected\",\"Record ID\",\"Remarks\",\"Old Values\",\"New Values\"");

                foreach (var log in rows)
                {
                    string Esc(string s) => $"\"{s?.Replace("\"", "\"\"") ?? ""}\"";

                    // Convert UTC to Local Time and format with 12-hour clock + AM/PM
                    string localTimestamp = log.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss tt");

                    sb.AppendLine(string.Join(",",
                        Esc(localTimestamp),  // ← Changed from log.CreatedAt.ToString()
                        Esc(log.Username),
                        Esc(log.Category),
                        Esc(log.Action),
                        Esc(log.TableName),
                        Esc(log.RecordId),
                        Esc(log.Remarks),
                        Esc(log.OldValues),
                        Esc(log.NewValues)));
                }

                System.IO.File.WriteAllText(dialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Exported successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}