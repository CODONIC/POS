namespace POS.Admin
{
    partial class ManageProdFrm
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
            label12 = new Label();
            txtSearch = new CustomControls.CustomTextBox();
            dgvProducts = new DataGridView();
            panel1 = new Panel();
            cmbCategory = new ComboBox();
            txtReorderLevel = new CustomControls.CustomTextBox();
            txtPrice = new CustomControls.CustomTextBox();
            txtProductName = new CustomControls.CustomTextBox();
            txtProductCode = new CustomControls.CustomTextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnClear = new RoundedButton();
            btnDelete = new RoundedButton();
            btnEdit = new RoundedButton();
            btnAdd = new RoundedButton();
            btnBack = new RoundedButton();
            titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panel1.SuspendLayout();
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
            titleBar.Size = new Size(1280, 48);
            titleBar.TabIndex = 19;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1030, 12);
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
            closeButton.Location = new Point(1236, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(44, 48);
            closeButton.TabIndex = 17;
            closeButton.Text = " X";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(12, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(170, 75);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 48;
            label12.Text = "SEARCH";
            // 
            // txtSearch
            // 
            txtSearch.BorderColor = SystemColors.ButtonFace;
            txtSearch.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtSearch.BorderRadius = 8;
            txtSearch.BorderThickness = 2;
            txtSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = SystemColors.GrayText;
            txtSearch.InnerBackColor = SystemColors.InactiveCaption;
            txtSearch.InnerForeColor = Color.Gray;
            txtSearch.IsPasswordField = false;
            txtSearch.Location = new Point(170, 97);
            txtSearch.Name = "txtSearch";
            txtSearch.PasswordChar = '\0';
            txtSearch.PlaceholderColor = Color.Gray;
            txtSearch.PlaceholderText = "";
            txtSearch.Size = new Size(955, 47);
            txtSearch.TabIndex = 47;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(170, 150);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(955, 254);
            dgvProducts.TabIndex = 46;
            // 
            // panel1
            // 
            panel1.Controls.Add(cmbCategory);
            panel1.Controls.Add(txtReorderLevel);
            panel1.Controls.Add(txtPrice);
            panel1.Controls.Add(txtProductName);
            panel1.Controls.Add(txtProductCode);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(170, 425);
            panel1.Name = "panel1";
            panel1.Size = new Size(955, 206);
            panel1.TabIndex = 49;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(214, 90);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(387, 23);
            cmbCategory.TabIndex = 53;
            // 
            // txtReorderLevel
            // 
            txtReorderLevel.BorderColor = SystemColors.ButtonFace;
            txtReorderLevel.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtReorderLevel.BorderRadius = 8;
            txtReorderLevel.BorderThickness = 2;
            txtReorderLevel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtReorderLevel.ForeColor = SystemColors.GrayText;
            txtReorderLevel.InnerBackColor = SystemColors.InactiveCaption;
            txtReorderLevel.InnerForeColor = Color.Gray;
            txtReorderLevel.IsPasswordField = false;
            txtReorderLevel.Location = new Point(214, 159);
            txtReorderLevel.Name = "txtReorderLevel";
            txtReorderLevel.PasswordChar = '\0';
            txtReorderLevel.PlaceholderColor = Color.Gray;
            txtReorderLevel.PlaceholderText = "";
            txtReorderLevel.Size = new Size(387, 32);
            txtReorderLevel.TabIndex = 52;
            // 
            // txtPrice
            // 
            txtPrice.BorderColor = SystemColors.ButtonFace;
            txtPrice.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtPrice.BorderRadius = 8;
            txtPrice.BorderThickness = 2;
            txtPrice.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPrice.ForeColor = SystemColors.GrayText;
            txtPrice.InnerBackColor = SystemColors.InactiveCaption;
            txtPrice.InnerForeColor = Color.Gray;
            txtPrice.IsPasswordField = false;
            txtPrice.Location = new Point(214, 121);
            txtPrice.Name = "txtPrice";
            txtPrice.PasswordChar = '\0';
            txtPrice.PlaceholderColor = Color.Gray;
            txtPrice.PlaceholderText = "";
            txtPrice.Size = new Size(387, 32);
            txtPrice.TabIndex = 51;
            // 
            // txtProductName
            // 
            txtProductName.BorderColor = SystemColors.ButtonFace;
            txtProductName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtProductName.BorderRadius = 8;
            txtProductName.BorderThickness = 2;
            txtProductName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtProductName.ForeColor = SystemColors.GrayText;
            txtProductName.InnerBackColor = SystemColors.InactiveCaption;
            txtProductName.InnerForeColor = Color.Gray;
            txtProductName.IsPasswordField = false;
            txtProductName.Location = new Point(214, 45);
            txtProductName.Name = "txtProductName";
            txtProductName.PasswordChar = '\0';
            txtProductName.PlaceholderColor = Color.Gray;
            txtProductName.PlaceholderText = "";
            txtProductName.Size = new Size(387, 32);
            txtProductName.TabIndex = 49;
            // 
            // txtProductCode
            // 
            txtProductCode.BorderColor = SystemColors.ButtonFace;
            txtProductCode.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtProductCode.BorderRadius = 8;
            txtProductCode.BorderThickness = 2;
            txtProductCode.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtProductCode.ForeColor = SystemColors.GrayText;
            txtProductCode.InnerBackColor = SystemColors.InactiveCaption;
            txtProductCode.InnerForeColor = Color.Gray;
            txtProductCode.IsPasswordField = false;
            txtProductCode.Location = new Point(214, 7);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.PasswordChar = '\0';
            txtProductCode.PlaceholderColor = Color.Gray;
            txtProductCode.PlaceholderText = "";
            txtProductCode.Size = new Size(387, 32);
            txtProductCode.TabIndex = 48;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(30, 164);
            label7.Name = "label7";
            label7.Size = new Size(120, 21);
            label7.TabIndex = 28;
            label7.Text = "Re-order Level";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(30, 126);
            label6.Name = "label6";
            label6.Size = new Size(82, 21);
            label6.TabIndex = 27;
            label6.Text = "Unit Price";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(30, 90);
            label5.Name = "label5";
            label5.Size = new Size(85, 21);
            label5.TabIndex = 26;
            label5.Text = "Category";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(30, 56);
            label4.Name = "label4";
            label4.Size = new Size(163, 21);
            label4.TabIndex = 25;
            label4.Text = "Product Description";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(30, 23);
            label3.Name = "label3";
            label3.Size = new Size(120, 21);
            label3.TabIndex = 24;
            label3.Text = "Product Code";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(170, 407);
            label2.Name = "label2";
            label2.Size = new Size(121, 19);
            label2.TabIndex = 50;
            label2.Text = "PRODUCT INFO";
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
            btnClear.Location = new Point(728, 650);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(91, 45);
            btnClear.TabIndex = 55;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
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
            btnDelete.Location = new Point(619, 650);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(91, 45);
            btnDelete.TabIndex = 54;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
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
            btnEdit.Location = new Point(505, 650);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(91, 45);
            btnEdit.TabIndex = 53;
            btnEdit.Text = "EDIT";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
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
            btnAdd.Location = new Point(398, 650);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(91, 45);
            btnAdd.TabIndex = 52;
            btnAdd.Text = "ADD";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.SteelBlue;
            btnBack.BorderColor = Color.Transparent;
            btnBack.BorderRadius = 10;
            btnBack.BorderSize = 0;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(24, 661);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(68, 34);
            btnBack.TabIndex = 56;
            btnBack.Text = "BACK";
            btnBack.TextAlign = ContentAlignment.TopCenter;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // ManageProdFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(btnBack);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(label12);
            Controls.Add(txtSearch);
            Controls.Add(dgvProducts);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ManageProdFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageProdFrm";
            Load += ManageProdFrm_Load;
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblAdminName;
        private Button closeButton;
        private Label titleLabel;
        private Label label12;
        private CustomControls.CustomTextBox txtSearch;
        private DataGridView dgvProducts;
        private Panel panel1;
        private Label label2;
        private ComboBox cmbCategory;
        private CustomControls.CustomTextBox txtReorderLevel;
        private CustomControls.CustomTextBox txtPrice;
        private CustomControls.CustomTextBox txtProductName;
        private CustomControls.CustomTextBox txtProductCode;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private RoundedButton btnClear;
        private RoundedButton btnDelete;
        private RoundedButton btnEdit;
        private RoundedButton btnAdd;
        private RoundedButton btnBack;
    }
}