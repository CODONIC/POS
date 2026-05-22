using Npgsql;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Admin
{
    public partial class ProdCategoryFrm : BaseForm
    {
        private readonly string _username, _companyName, _companyId, _userId, _sessionToken;
        private readonly CategoryService _categoryService;
        private string _selectedCategoryId;

        public ProdCategoryFrm(string username, string companyName, string userId, string sessionToken)
        {
            InitializeComponent();
            InitializeTitleBar(closeButton, titleBar, titleLabel);
            _username = username;
            _companyName = companyName;
            _companyId = GetCompanyId(companyName);
            _categoryService = new CategoryService(_companyId);
            _userId = userId;
            _sessionToken = sessionToken;

            lblAdminName.Text = $"{_username} | Admin";
            titleLabel.Text = $"{_companyName} ";
            SetUserContext(_username, _userId, _sessionToken);
            SetUserContext(_username, _companyId);

            SetupDataGridView();
            InitializeShortcuts();
            this.Load += async (s, e) => await LoadCategoriesAsync();
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
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return null; }
        }

        private void SetupDataGridView()
        {
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionChanged += (s, e) => LoadSelectedCategory();
        }

        private async Task LoadCategoriesAsync()
        {
            if (string.IsNullOrEmpty(_companyId)) return;
            try
            {
                var dt = await _categoryService.GetCategoriesAsync(txtSearch.Text.Trim());
                dgvCategories.DataSource = dt;
                dgvCategories.Columns["id"].HeaderText = "ID";
                dgvCategories.Columns["name"].HeaderText = "Category Name";
                dgvCategories.Columns["id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show($"Failed to load categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadSelectedCategory()
        {
            if (dgvCategories.SelectedRows.Count == 0) return;
            var row = dgvCategories.SelectedRows[0];
            if (row.Cells["id"].Value == null || row.Cells["name"].Value == null) return;

            _selectedCategoryId = row.Cells["id"].Value.ToString();
            txtCategoryName.Text = row.Cells["name"].Value.ToString();
        }


        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (sender == txtSearch) txtCategoryName.FocusInner();
                else if (sender == txtCategoryName) txtSearch.FocusInner();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (sender == txtCategoryName) txtSearch.FocusInner();
                else if (sender == txtSearch) txtCategoryName.FocusInner();
                e.Handled = true;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var result = CategoryValidator.ValidateCategoryName(txtCategoryName.Text);
            if (!result.IsValid)
            { MessageBox.Show(result.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (await _categoryService.CategoryExistsAsync(txtCategoryName.Text.Trim()))
            { MessageBox.Show("Category already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var (newId, name) = await _categoryService.AddCategoryAsync(txtCategoryName.Text.Trim());

            await AuditService.LogInsertAsync(_username, _companyId, "categories", newId,
                AuditService.ToJson(("name", name)));

            MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadCategoriesAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var selectionResult = CategoryValidator.ValidateSelection(_selectedCategoryId);
            if (!selectionResult.IsValid)
            { MessageBox.Show(selectionResult.ErrorMessage, "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            var nameResult = CategoryValidator.ValidateCategoryName(txtCategoryName.Text);
            if (!nameResult.IsValid)
            { MessageBox.Show(nameResult.ErrorMessage, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (await _categoryService.CategoryExistsAsync(txtCategoryName.Text.Trim(), _selectedCategoryId))
            { MessageBox.Show("Category name already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string oldName = await _categoryService.GetCategoryNameAsync(_selectedCategoryId);
            await _categoryService.UpdateCategoryAsync(_selectedCategoryId, txtCategoryName.Text.Trim());

            await AuditService.LogUpdateAsync(_username, _companyId, "categories", _selectedCategoryId,
                AuditService.ToJson(("name", oldName)),
                AuditService.ToJson(("name", txtCategoryName.Text.Trim())));

            MessageBox.Show("Category updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadCategoriesAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectionResult = CategoryValidator.ValidateSelection(_selectedCategoryId);
            if (!selectionResult.IsValid)
            { MessageBox.Show(selectionResult.ErrorMessage, "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (CategoryValidator.ConfirmDelete(txtCategoryName.Text) != DialogResult.Yes) return;

            int productCount = await _categoryService.GetProductCountByCategoryAsync(_selectedCategoryId);
            if (productCount > 0)
            {
                MessageBox.Show($"Cannot delete \"{txtCategoryName.Text}\" — it is assigned to {productCount} product(s).",
                    "Delete Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string deletedName = txtCategoryName.Text.Trim();
            string deletedId = _selectedCategoryId;

            await _categoryService.DeleteCategoryAsync(_selectedCategoryId);

            await AuditService.LogDeleteAsync(_username, _companyId, "categories", deletedId,
                AuditService.ToJson(("name", deletedName)));

            MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
            await LoadCategoriesAsync();
        }

        private void ClearFields()
        {
            _selectedCategoryId = null;
            txtCategoryName.Text = "";
            dgvCategories.ClearSelection();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e) => await LoadCategoriesAsync();
        private void btnClear_Click(object sender, EventArgs e) => ClearFields();
        private void btnBack_Click(object sender, EventArgs e) { SetNavigating(true); new AdminDashboard(_username, _companyName, _userId, _sessionToken).Show(); Close(); }

        private void InitializeShortcuts()
        {

            ShortcutHelper.AttachCustomKeyNavigation(txtSearch, txt_KeyDown);
            ShortcutHelper.AttachCustomKeyNavigation(txtCategoryName, txt_KeyDown);


            ShortcutHelper.AttachFunctionShortcuts(this,
                onEscape: (s, ev) => btnBack_Click(s, ev),
                onF1: (s, ev) => btnAdd_Click(s, ev),
                onF2: (s, ev) => btnEdit_Click(s, ev),
                onF3: (s, ev) => btnDelete_Click(s, ev),
                onF4: (s, ev) => btnClear_Click(s, ev)

            );


            ShortcutHelper.SetupTooltips(this,
                (btnBack, "ESC"),
                (btnAdd, "F1"),
                (btnEdit, "F2"),
                (btnDelete, "F3"),
                (btnClear, "F4")
            );


            ShortcutHelper.AttachHoverEffect(btnBack, "BACK", "ESC");
            ShortcutHelper.AttachHoverEffect(btnAdd, "ADD", "F1");
            ShortcutHelper.AttachHoverEffect(btnEdit, "EDIT", "F2");
            ShortcutHelper.AttachHoverEffect(btnDelete, "DELETE", "F3");
            ShortcutHelper.AttachHoverEffect(btnClear, "CLEAR", "F4");
        }

    }
}