using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class TransactionsForm : BaseForm
    {
        private readonly string _username, _companyName, _companyId;
        private readonly TransactionService _transactionService;
        private DataTable _allTransactions;

        public TransactionsForm(string username, string companyName)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _transactionService = new TransactionService(_companyId);

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";
            SetUserContext(_username, _companyId);

            TransactionGridBuilder.SetupColumns(dgvTransactions);
            dgvTransactions.CellFormatting += TransactionGridBuilder.FormatStatusCell;

            LoadTransactions();
            txtSearch.TextChanged += (s, e) => FilterTransactions();
        }

        private string GetCompanyId(string companyName)
        {
            try
            {
                using var conn = DatabaseService.GetConnection();
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT id FROM public.companies WHERE LOWER(name) = LOWER(@name) LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@name", companyName);
                return cmd.ExecuteScalar()?.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error resolving company: {ex.Message}");
                return null;
            }
        }

        private void LoadTransactions()
        {
            try
            {
                _allTransactions = _transactionService.GetAllTransactions();
                dgvTransactions.DataSource = _allTransactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterTransactions()
        {
            if (_allTransactions == null) return;

            string keyword = txtSearch.Text.Trim();
            dgvTransactions.DataSource = _transactionService.SearchTransactions(_allTransactions, keyword);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new AdminDashboard(_username, _companyName).Show();
            Close();
        }
    }
}