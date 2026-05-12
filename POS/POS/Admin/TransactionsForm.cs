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
        private readonly string _userId;
        private readonly string _sessionToken;

        public TransactionsForm(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _transactionService = new TransactionService(_companyId);
            _userId = userId;
            _sessionToken = sessionToken;

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName}";
            SetUserContext(_username, _userId, _sessionToken);
            SetUserContext(_username, _companyId);

            TransactionGridBuilder.SetupColumns(dgvTransactions);
            dgvTransactions.CellFormatting += TransactionGridBuilder.FormatStatusCell;

            LoadTransactions();
            txtSearch.TextChanged += (s, e) => FilterTransactions();
            this.KeyPreview = true;
            this.KeyDown += transactionForm_KeyDown;
            InitializeShortcutHints();
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
            SetNavigating(true);
            new AdminDashboard(_username, _companyName, _userId, _sessionToken).Show();
            Close();
        }

        private void transactionForm_KeyDown(object sender, KeyEventArgs e)
        {
            var shortcuts = new Dictionary<Keys, EventHandler>
            {
                { Keys.Escape, btnBack_Click },

            };

            if (shortcuts.TryGetValue(e.KeyCode, out var handler))
            {
                handler?.Invoke(sender, e);
                e.Handled = true;
            }
        }

        private void InitializeShortcutHints()
        {
            var shortcuts = new Dictionary<Button, string>
            {
                { btnBack, "ESC" }
            };

            var toolTip = new ToolTip { InitialDelay = 200, ShowAlways = true };

            foreach (var (button, shortcut) in shortcuts)
            {
                toolTip.SetToolTip(button, shortcut);
                AttachHoverEffect(button);
            }
        }

        private void AttachHoverEffect(Button btn)
        {
            var originalLocation = btn.Location;

            btn.MouseEnter += (s, e) =>
            {
                btn.Location = new Point(originalLocation.X, originalLocation.Y - 3);
                btn.Padding = new Padding(0, 0, 0, 6);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.Location = originalLocation;
                btn.Padding = new Padding(0);
            };
        }
    }
}