using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS.Admin
{
    public static class TransactionGridBuilder
    {
        public static void SetupColumns(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            var columns = new[]
            {
                new { Name = "transaction_number", Header = "Transaction #",   Width = 130, Fmt = "", Align = "Left" },
                new { Name = "transaction_date",   Header = "Date & Time",     Width = 150, Fmt = "MM/dd/yyyy HH:mm", Align = "Left" },
                new { Name = "cashier_name",       Header = "Cashier",         Width = 130, Fmt = "", Align = "Left" },
                new { Name = "subtotal",           Header = "Subtotal",        Width = 100, Fmt = "C2", Align = "Right" },
                new { Name = "discount_percentage",Header = "Disc %",          Width = 70,  Fmt = "0.00", Align = "Right" },
                new { Name = "discount_amount",    Header = "Disc Amount",     Width = 100, Fmt = "C2", Align = "Right" },
                new { Name = "vatable_amount",     Header = "Vatable",         Width = 100, Fmt = "C2", Align = "Right" },
                new { Name = "vat_amount",         Header = "VAT",             Width = 90,  Fmt = "C2", Align = "Right" },
                new { Name = "total_amount",       Header = "Total",           Width = 110, Fmt = "C2", Align = "Right" },
                new { Name = "payment_method",     Header = "Payment",         Width = 100, Fmt = "", Align = "Left" },
                new { Name = "customer_payment",   Header = "Tendered",        Width = 100, Fmt = "C2", Align = "Right" },
                new { Name = "change_amount",      Header = "Change",          Width = 100, Fmt = "C2", Align = "Right" },
                new { Name = "status",             Header = "Status",          Width = 90,  Fmt = "", Align = "Left" },
            };

            foreach (var col in columns)
            {
                var dgvCol = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = col.Name,
                    Name = col.Name,
                    HeaderText = col.Header,
                    FillWeight = col.Width,
                    DefaultCellStyle = {
                        Format = col.Fmt,
                        Alignment = col.Align == "Right" ?
                            DataGridViewContentAlignment.MiddleRight :
                            DataGridViewContentAlignment.MiddleLeft
                    }
                };
                dgv.Columns.Add(dgvCol);
            }
        }

        public static void FormatStatusCell(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv.Columns[e.ColumnIndex].Name != "status" || e.Value == null)
                return;

            string status = e.Value.ToString().ToLower();
            e.CellStyle.ForeColor = status switch
            {
                "completed" => Color.FromArgb(22, 163, 74),   // green
                "voided" => Color.FromArgb(220, 38, 38),     // red
                "refunded" => Color.FromArgb(234, 88, 12),    // orange
                _ => Color.FromArgb(100, 116, 139)            // slate
            };
            e.CellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
        }
    }
}