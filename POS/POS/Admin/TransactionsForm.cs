using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql; 

namespace POS.Admin
{
    public partial class TransactionsForm : BaseForm
    {
        private string _username;
        private string _companyName;
        private DataTable _allTransactions = new DataTable();

        public TransactionsForm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";

            SetupDataGridView();
            LoadTransactions();
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        // ─── DataGridView Setup ───────────────────────────────────────────────

        private void SetupDataGridView()
        {
            dgvTransactions.AutoGenerateColumns = false;
            dgvTransactions.ReadOnly = true;
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.AllowUserToDeleteRows = false;
            dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTransactions.MultiSelect = false;
            dgvTransactions.RowHeadersVisible = false;
            dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTransactions.BackgroundColor = Color.White;
            dgvTransactions.BorderStyle = BorderStyle.None;
            dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // Column definitions
            var columns = new[]
            {
                new { Name = "transaction_number", Header = "Transaction #",   Width = 130, Fmt = ""         },
                new { Name = "transaction_date",   Header = "Date & Time",     Width = 150, Fmt = "MM/dd/yyyy HH:mm" },
                new { Name = "cashier_name",       Header = "Cashier",         Width = 130, Fmt = ""         },
                new { Name = "subtotal",           Header = "Subtotal",        Width = 100, Fmt = "C2"       },
                new { Name = "discount_percentage",Header = "Disc %",          Width = 70,  Fmt = "0.00"     },
                new { Name = "discount_amount",    Header = "Disc Amount",     Width = 100, Fmt = "C2"       },
                new { Name = "vatable_amount",     Header = "Vatable",         Width = 100, Fmt = "C2"       },
                new { Name = "vat_amount",         Header = "VAT",             Width = 90,  Fmt = "C2"       },
                new { Name = "total_amount",       Header = "Total",           Width = 110, Fmt = "C2"       },
                new { Name = "payment_method",     Header = "Payment",         Width = 100, Fmt = ""         },
                new { Name = "customer_payment",   Header = "Tendered",        Width = 100, Fmt = "C2"       },
                new { Name = "change_amount",      Header = "Change",          Width = 100, Fmt = "C2"       },
                new { Name = "status",             Header = "Status",          Width = 90,  Fmt = ""         },
            };

            foreach (var col in columns)
            {
                var dgvCol = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = col.Name,
                    Name = col.Name,
                    HeaderText = col.Header,
                    FillWeight = col.Width,
                    DefaultCellStyle = { Format = col.Fmt, Alignment = DataGridViewContentAlignment.MiddleLeft }
                };

                // Right-align currency columns
                if (col.Fmt == "C2" || col.Fmt == "0.00")
                    dgvCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvTransactions.Columns.Add(dgvCol);
            }

            // Style the Status column with color via CellFormatting
            dgvTransactions.CellFormatting += DgvTransactions_CellFormatting;
        }

        private void DgvTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTransactions.Columns[e.ColumnIndex].Name != "status" || e.Value == null)
                return;

            string status = e.Value.ToString().ToLower();
            e.CellStyle.ForeColor = status switch
            {
                "completed" => Color.FromArgb(22, 163, 74),   // green
                "voided" => Color.FromArgb(220, 38, 38),   // red
                "refunded" => Color.FromArgb(234, 88, 12),   // orange
                _ => Color.FromArgb(100, 116, 139)  // slate
            };
            e.CellStyle.Font = new Font(dgvTransactions.Font, FontStyle.Bold);
        }

        // ─── Data Loading ─────────────────────────────────────────────────────

        private void LoadTransactions()
        {
            try
            {
                string connStr = DatabaseService.ConnectionString; 
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();

                    string sql = @"
                        SELECT
                            transaction_number,
                            transaction_date,
                            cashier_name,
                            subtotal,
                            discount_percentage,
                            discount_amount,
                            vatable_amount,
                            vat_amount,
                            total_amount,
                            payment_method,
                            customer_payment,
                            change_amount,
                            status
                        FROM public.transactions
                        ORDER BY transaction_date DESC";

                    using (var adapter = new NpgsqlDataAdapter(sql, conn))
                    {
                        _allTransactions.Clear();
                        adapter.Fill(_allTransactions);
                    }
                }

                dgvTransactions.DataSource = _allTransactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Search ───────────────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().Replace("'", "''"); // basic sanitize for filter

            if (string.IsNullOrEmpty(keyword))
            {
                // Reset — show everything
                (_allTransactions as DataTable).DefaultView.RowFilter = string.Empty;
            }
            else
            {
                // Filter across key visible columns
                (_allTransactions as DataTable).DefaultView.RowFilter =
                    $"transaction_number LIKE '%{keyword}%' OR " +
                    $"cashier_name LIKE '%{keyword}%' OR " +
                    $"payment_method LIKE '%{keyword}%' OR " +
                    $"status LIKE '%{keyword}%'";
            }

            dgvTransactions.DataSource = (_allTransactions as DataTable).DefaultView;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard(_username, _companyName);
            admin.Show();
            this.Close();
        }
    }
}