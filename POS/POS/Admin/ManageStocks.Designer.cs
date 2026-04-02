namespace POS.Admin
{
    partial class ManageStocks
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
            btnAddStock = new RoundedButton();
            label2 = new Label();
            panel1 = new Panel();
            txtRemove = new CustomControls.CustomTextBox();
            label1 = new Label();
            txtAdd = new CustomControls.CustomTextBox();
            label8 = new Label();
            txtStockInDate = new CustomControls.CustomTextBox();
            label7 = new Label();
            txtCategory = new CustomControls.CustomTextBox();
            label3 = new Label();
            txtUnitPrice = new CustomControls.CustomTextBox();
            label4 = new Label();
            txtDescription = new CustomControls.CustomTextBox();
            label5 = new Label();
            txtProductCode = new CustomControls.CustomTextBox();
            label6 = new Label();
            btnCancel = new RoundedButton();
            btnSave = new RoundedButton();
            dgvPending = new DataGridView();
            dgvAllProducts = new DataGridView();
            label12 = new Label();
            txtSearch = new CustomControls.CustomTextBox();
            btnRemoveStock = new RoundedButton();
            label9 = new Label();
            label10 = new Label();
            titleBar.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPending).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAllProducts).BeginInit();
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
            titleBar.Size = new Size(1565, 38);
            titleBar.TabIndex = 19;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1343, 9);
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
            closeButton.Location = new Point(1528, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(41, 35);
            closeButton.TabIndex = 17;
            closeButton.Text = " X";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(12, 9);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
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
            btnBack.Location = new Point(23, 533);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(68, 34);
            btnBack.TabIndex = 37;
            btnBack.Text = "BACK";
            btnBack.TextAlign = ContentAlignment.TopCenter;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // btnAddStock
            // 
            btnAddStock.BackColor = Color.SteelBlue;
            btnAddStock.BorderColor = Color.Transparent;
            btnAddStock.BorderRadius = 20;
            btnAddStock.BorderSize = 0;
            btnAddStock.FlatAppearance.BorderSize = 0;
            btnAddStock.FlatStyle = FlatStyle.Flat;
            btnAddStock.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddStock.ForeColor = Color.White;
            btnAddStock.Location = new Point(445, 529);
            btnAddStock.Name = "btnAddStock";
            btnAddStock.Size = new Size(121, 40);
            btnAddStock.TabIndex = 38;
            btnAddStock.Text = "ADD STOCK";
            btnAddStock.UseVisualStyleBackColor = false;
            btnAddStock.Click += btnAddStock_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(23, 140);
            label2.Name = "label2";
            label2.Size = new Size(171, 19);
            label2.TabIndex = 40;
            label2.Text = "STOCK INFORMATION";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(txtRemove);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtAdd);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(txtStockInDate);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtCategory);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtUnitPrice);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtProductCode);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(23, 153);
            panel1.Name = "panel1";
            panel1.Size = new Size(534, 352);
            panel1.TabIndex = 39;
            // 
            // txtRemove
            // 
            txtRemove.BorderColor = SystemColors.ButtonFace;
            txtRemove.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtRemove.BorderRadius = 8;
            txtRemove.BorderThickness = 2;
            txtRemove.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtRemove.ForeColor = SystemColors.GrayText;
            txtRemove.InnerBackColor = SystemColors.InactiveCaption;
            txtRemove.InnerForeColor = Color.Black;
            txtRemove.IsPasswordField = false;
            txtRemove.Location = new Point(237, 275);
            txtRemove.Name = "txtRemove";
            txtRemove.PasswordChar = '\0';
            txtRemove.PlaceholderColor = Color.Gray;
            txtRemove.PlaceholderText = "";
            txtRemove.Size = new Size(279, 32);
            txtRemove.TabIndex = 67;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(22, 286);
            label1.Name = "label1";
            label1.Size = new Size(201, 21);
            label1.TabIndex = 66;
            label1.Text = "Total amount to remove";
            // 
            // txtAdd
            // 
            txtAdd.BorderColor = SystemColors.ButtonFace;
            txtAdd.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtAdd.BorderRadius = 8;
            txtAdd.BorderThickness = 2;
            txtAdd.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAdd.ForeColor = SystemColors.GrayText;
            txtAdd.InnerBackColor = SystemColors.InactiveCaption;
            txtAdd.InnerForeColor = Color.Black;
            txtAdd.IsPasswordField = false;
            txtAdd.Location = new Point(237, 234);
            txtAdd.Name = "txtAdd";
            txtAdd.PasswordChar = '\0';
            txtAdd.PlaceholderColor = Color.Gray;
            txtAdd.PlaceholderText = "";
            txtAdd.Size = new Size(279, 32);
            txtAdd.TabIndex = 65;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(22, 245);
            label8.Name = "label8";
            label8.Size = new Size(178, 21);
            label8.TabIndex = 64;
            label8.Text = "Total amount to add ";
            // 
            // txtStockInDate
            // 
            txtStockInDate.BorderColor = SystemColors.ButtonFace;
            txtStockInDate.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtStockInDate.BorderRadius = 8;
            txtStockInDate.BorderThickness = 2;
            txtStockInDate.Enabled = false;
            txtStockInDate.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtStockInDate.ForeColor = SystemColors.GrayText;
            txtStockInDate.InnerBackColor = SystemColors.InactiveCaption;
            txtStockInDate.InnerForeColor = Color.Black;
            txtStockInDate.IsPasswordField = false;
            txtStockInDate.Location = new Point(237, 178);
            txtStockInDate.Name = "txtStockInDate";
            txtStockInDate.PasswordChar = '\0';
            txtStockInDate.PlaceholderColor = Color.Gray;
            txtStockInDate.PlaceholderText = "";
            txtStockInDate.Size = new Size(279, 32);
            txtStockInDate.TabIndex = 63;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(25, 178);
            label7.Name = "label7";
            label7.Size = new Size(118, 21);
            label7.TabIndex = 62;
            label7.Text = "Stock in Date:";
            // 
            // txtCategory
            // 
            txtCategory.BorderColor = SystemColors.ButtonFace;
            txtCategory.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtCategory.BorderRadius = 8;
            txtCategory.BorderThickness = 2;
            txtCategory.Enabled = false;
            txtCategory.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCategory.ForeColor = SystemColors.GrayText;
            txtCategory.InnerBackColor = SystemColors.InactiveCaption;
            txtCategory.InnerForeColor = Color.Black;
            txtCategory.IsPasswordField = false;
            txtCategory.Location = new Point(237, 99);
            txtCategory.Name = "txtCategory";
            txtCategory.PasswordChar = '\0';
            txtCategory.PlaceholderColor = Color.Gray;
            txtCategory.PlaceholderText = "";
            txtCategory.Size = new Size(279, 32);
            txtCategory.TabIndex = 61;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(22, 39);
            label3.Name = "label3";
            label3.Size = new Size(120, 21);
            label3.TabIndex = 54;
            label3.Text = "Product Code";
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.BorderColor = SystemColors.ButtonFace;
            txtUnitPrice.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtUnitPrice.BorderRadius = 8;
            txtUnitPrice.BorderThickness = 2;
            txtUnitPrice.Enabled = false;
            txtUnitPrice.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtUnitPrice.ForeColor = SystemColors.GrayText;
            txtUnitPrice.InnerBackColor = SystemColors.InactiveCaption;
            txtUnitPrice.InnerForeColor = Color.Black;
            txtUnitPrice.IsPasswordField = false;
            txtUnitPrice.Location = new Point(237, 137);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.PasswordChar = '\0';
            txtUnitPrice.PlaceholderColor = Color.Gray;
            txtUnitPrice.PlaceholderText = "";
            txtUnitPrice.Size = new Size(279, 32);
            txtUnitPrice.TabIndex = 60;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(22, 72);
            label4.Name = "label4";
            label4.Size = new Size(163, 21);
            label4.TabIndex = 55;
            label4.Text = "Product Description";
            // 
            // txtDescription
            // 
            txtDescription.BorderColor = SystemColors.ButtonFace;
            txtDescription.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtDescription.BorderRadius = 8;
            txtDescription.BorderThickness = 2;
            txtDescription.Enabled = false;
            txtDescription.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDescription.ForeColor = SystemColors.GrayText;
            txtDescription.InnerBackColor = SystemColors.InactiveCaption;
            txtDescription.InnerForeColor = Color.Black;
            txtDescription.IsPasswordField = false;
            txtDescription.Location = new Point(237, 61);
            txtDescription.Name = "txtDescription";
            txtDescription.PasswordChar = '\0';
            txtDescription.PlaceholderColor = Color.Gray;
            txtDescription.PlaceholderText = "";
            txtDescription.Size = new Size(279, 32);
            txtDescription.TabIndex = 59;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(22, 106);
            label5.Name = "label5";
            label5.Size = new Size(85, 21);
            label5.TabIndex = 56;
            label5.Text = "Category";
            // 
            // txtProductCode
            // 
            txtProductCode.BorderColor = SystemColors.ButtonFace;
            txtProductCode.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtProductCode.BorderRadius = 8;
            txtProductCode.BorderThickness = 2;
            txtProductCode.Enabled = false;
            txtProductCode.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtProductCode.ForeColor = SystemColors.GrayText;
            txtProductCode.InnerBackColor = SystemColors.InactiveCaption;
            txtProductCode.InnerForeColor = Color.Black;
            txtProductCode.IsPasswordField = false;
            txtProductCode.Location = new Point(237, 23);
            txtProductCode.Name = "txtProductCode";
            txtProductCode.PasswordChar = '\0';
            txtProductCode.PlaceholderColor = Color.Gray;
            txtProductCode.PlaceholderText = "";
            txtProductCode.Size = new Size(279, 32);
            txtProductCode.TabIndex = 58;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(22, 142);
            label6.Name = "label6";
            label6.Size = new Size(82, 21);
            label6.TabIndex = 57;
            label6.Text = "Unit Price";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.SteelBlue;
            btnCancel.BorderColor = Color.Transparent;
            btnCancel.BorderRadius = 20;
            btnCancel.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(893, 529);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(108, 40);
            btnCancel.TabIndex = 67;
            btnCancel.Text = "CANCEL";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SteelBlue;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 20;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(777, 529);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(99, 40);
            btnSave.TabIndex = 66;
            btnSave.Text = "SAVE";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // dgvPending
            // 
            dgvPending.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPending.Location = new Point(576, 153);
            dgvPending.Name = "dgvPending";
            dgvPending.Size = new Size(425, 352);
            dgvPending.TabIndex = 41;
            // 
            // dgvAllProducts
            // 
            dgvAllProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAllProducts.Location = new Point(1053, 153);
            dgvAllProducts.Name = "dgvAllProducts";
            dgvAllProducts.Size = new Size(469, 352);
            dgvAllProducts.TabIndex = 42;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(576, 63);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 46;
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
            txtSearch.InnerForeColor = Color.Black;
            txtSearch.IsPasswordField = false;
            txtSearch.Location = new Point(576, 85);
            txtSearch.Name = "txtSearch";
            txtSearch.PasswordChar = '\0';
            txtSearch.PlaceholderColor = Color.Gray;
            txtSearch.PlaceholderText = "";
            txtSearch.Size = new Size(425, 39);
            txtSearch.TabIndex = 45;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnRemoveStock
            // 
            btnRemoveStock.BackColor = Color.SteelBlue;
            btnRemoveStock.BorderColor = Color.Transparent;
            btnRemoveStock.BorderRadius = 20;
            btnRemoveStock.BorderSize = 0;
            btnRemoveStock.FlatAppearance.BorderSize = 0;
            btnRemoveStock.FlatStyle = FlatStyle.Flat;
            btnRemoveStock.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRemoveStock.ForeColor = Color.White;
            btnRemoveStock.Location = new Point(595, 529);
            btnRemoveStock.Name = "btnRemoveStock";
            btnRemoveStock.Size = new Size(160, 40);
            btnRemoveStock.TabIndex = 68;
            btnRemoveStock.Text = "REMOVE STOCK";
            btnRemoveStock.UseVisualStyleBackColor = false;
            btnRemoveStock.Click += btnRemoveStock_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.LightSteelBlue;
            label9.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(1053, 131);
            label9.Name = "label9";
            label9.Size = new Size(117, 19);
            label9.TabIndex = 69;
            label9.Text = "TOTAL STOCKS";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.LightSteelBlue;
            label10.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(576, 131);
            label10.Name = "label10";
            label10.Size = new Size(169, 19);
            label10.TabIndex = 70;
            label10.Text = "STOCKS TO BE ADDED";
            // 
            // ManageStocks
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1565, 593);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(btnRemoveStock);
            Controls.Add(btnCancel);
            Controls.Add(label12);
            Controls.Add(btnSave);
            Controls.Add(txtSearch);
            Controls.Add(dgvAllProducts);
            Controls.Add(dgvPending);
            Controls.Add(label2);
            Controls.Add(btnAddStock);
            Controls.Add(panel1);
            Controls.Add(btnBack);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ManageStocks";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageStocks";
            Load += ManageStocks_Load;
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPending).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAllProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblAdminName;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnBack;
        private RoundedButton btnAddStock;
        private Label label2;
        private Panel panel1;
        private CustomControls.CustomTextBox txtStockInDate;
        private Label label7;
        private CustomControls.CustomTextBox txtCategory;
        private Label label3;
        private CustomControls.CustomTextBox txtUnitPrice;
        private Label label4;
        private CustomControls.CustomTextBox txtDescription;
        private Label label5;
        private CustomControls.CustomTextBox txtProductCode;
        private Label label6;
        private CustomControls.CustomTextBox txtAdd;
        private Label label8;
        private RoundedButton btnCancel;
        private RoundedButton btnSave;
        private DataGridView dgvPending;
        private DataGridView dgvAllProducts;
        private Label label12;
        private CustomControls.CustomTextBox txtSearch;
        private Label label1;
        private CustomControls.CustomTextBox txtRemove;
        private RoundedButton btnRemoveStock;
        private Label label9;
        private Label label10;
    }
}