namespace POS.Admin
{
    partial class ProdCategoryFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            titleBar = new Panel();
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnBack = new RoundedButton();
            dgvCategories = new DataGridView();
            customTextBox3 = new CustomControls.CustomTextBox();
            label12 = new Label();
            label2 = new Label();
            txtCategoryName = new CustomControls.CustomTextBox();
            btnAdd = new RoundedButton();
            btnEdit = new RoundedButton();
            btnDelete = new RoundedButton();
            btnClear = new RoundedButton();
            titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(lblAdminName);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(670, 48);
            titleBar.TabIndex = 18;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(425, 13);
            lblAdminName.Name = "lblAdminName";
            lblAdminName.Size = new Size(179, 21);
            lblAdminName.TabIndex = 21;
            lblAdminName.Text = "adminName | Admin";
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.FromArgb(44, 62, 80);
            closeButton.BackgroundImageLayout = ImageLayout.None;
            closeButton.Cursor = Cursors.Hand;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(622, 1);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(48, 47);
            closeButton.TabIndex = 17;
            closeButton.Text = " X";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(12, 13);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.SteelBlue;
            btnBack.BorderColor = Color.Transparent;
            btnBack.BorderRadius = 20;
            btnBack.BorderSize = 0;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(12, 672);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(74, 36);
            btnBack.TabIndex = 36;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // dgvCategories
            // 
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Location = new Point(110, 169);
            dgvCategories.Name = "dgvCategories";
            dgvCategories.Size = new Size(423, 254);
            dgvCategories.TabIndex = 37;
            dgvCategories.SelectionChanged += dgvCategories_SelectionChanged;
            // 
            // customTextBox3
            // 
            customTextBox3.BorderColor = SystemColors.ButtonFace;
            customTextBox3.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox3.BorderRadius = 8;
            customTextBox3.BorderThickness = 2;
            customTextBox3.Enabled = false;
            customTextBox3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox3.ForeColor = SystemColors.GrayText;
            customTextBox3.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox3.InnerForeColor = Color.Black;
            customTextBox3.IsPasswordField = false;
            customTextBox3.Location = new Point(110, 101);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(423, 47);
            customTextBox3.TabIndex = 38;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(110, 62);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 45;
            label12.Text = "SEARCH";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(110, 439);
            label2.Name = "label2";
            label2.Size = new Size(134, 19);
            label2.TabIndex = 46;
            label2.Text = "CATEGORY INFO";
            // 
            // txtCategoryName
            // 
            txtCategoryName.BorderColor = SystemColors.ButtonFace;
            txtCategoryName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtCategoryName.BorderRadius = 8;
            txtCategoryName.BorderThickness = 2;
            txtCategoryName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCategoryName.ForeColor = Color.Black;
            txtCategoryName.InnerBackColor = SystemColors.InactiveCaption;
            txtCategoryName.InnerForeColor = Color.Black;
            txtCategoryName.IsPasswordField = false;
            txtCategoryName.Location = new Point(110, 476);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.PasswordChar = '\0';
            txtCategoryName.PlaceholderColor = Color.Gray;
            txtCategoryName.PlaceholderText = "";
            txtCategoryName.Size = new Size(423, 104);
            txtCategoryName.TabIndex = 47;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.BorderColor = Color.Transparent;
            btnAdd.BorderRadius = 20;
            btnAdd.BorderSize = 0;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(110, 596);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(91, 45);
            btnAdd.TabIndex = 48;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.SteelBlue;
            btnEdit.BorderColor = Color.Transparent;
            btnEdit.BorderRadius = 20;
            btnEdit.BorderSize = 0;
            btnEdit.FlatAppearance.BorderSize = 0;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(216, 596);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(91, 45);
            btnEdit.TabIndex = 49;
            btnEdit.Text = "EDIT";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.BorderColor = Color.Transparent;
            btnDelete.BorderRadius = 20;
            btnDelete.BorderSize = 0;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(327, 596);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(91, 45);
            btnDelete.TabIndex = 50;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.SteelBlue;
            btnClear.BorderColor = Color.Transparent;
            btnClear.BorderRadius = 20;
            btnClear.BorderSize = 0;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(442, 596);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(91, 45);
            btnClear.TabIndex = 51;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // ProdCategoryFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(670, 720);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(txtCategoryName);
            Controls.Add(label2);
            Controls.Add(label12);
            Controls.Add(customTextBox3);
            Controls.Add(dgvCategories);
            Controls.Add(btnBack);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProdCategoryFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProdCategoryFrm";
            Load += ProdCategoryFrm_Load;
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblAdminName;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnBack;
        private DataGridView dgvCategories;
        private CustomControls.CustomTextBox customTextBox3;
        private Label label12;
        private Label label2;
        private CustomControls.CustomTextBox txtCategoryName;
        private RoundedButton btnAdd;
        private RoundedButton btnEdit;
        private RoundedButton btnDelete;
        private RoundedButton btnClear;

    }
}