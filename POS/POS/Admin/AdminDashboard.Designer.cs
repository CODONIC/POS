namespace POS
{
    partial class AdminDashboard
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
            btnLogOut = new RoundedButton();
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            label12 = new Label();
            txtSearch = new CustomControls.CustomTextBox();
            label2 = new Label();
            customTextBox1 = new CustomControls.CustomTextBox();
            label3 = new Label();
            customTextBox2 = new CustomControls.CustomTextBox();
            btnManageUsers = new RoundedButton();
            btnManageCategory = new RoundedButton();
            btnManageProducts = new RoundedButton();
            btnManageStocks = new RoundedButton();
            titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(btnLogOut);
            titleBar.Controls.Add(lblAdminName);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(1264, 48);
            titleBar.TabIndex = 17;
            // 
            // btnLogOut
            // 
            btnLogOut.BackColor = Color.SteelBlue;
            btnLogOut.BorderColor = Color.Transparent;
            btnLogOut.BorderRadius = 10;
            btnLogOut.BorderSize = 0;
            btnLogOut.FlatAppearance.BorderSize = 0;
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogOut.ForeColor = Color.White;
            btnLogOut.Location = new Point(907, 3);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(82, 34);
            btnLogOut.TabIndex = 22;
            btnLogOut.Text = "Logout";
            btnLogOut.TextAlign = ContentAlignment.TopCenter;
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1012, 12);
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
            closeButton.Location = new Point(1216, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(48, 48);
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
            titleLabel.Size = new Size(100, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "POS System";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(301, 82);
            label12.Name = "label12";
            label12.Size = new Size(102, 19);
            label12.TabIndex = 46;
            label12.Text = "TOTAL SALES";
            // 
            // txtSearch
            // 
            txtSearch.BorderColor = SystemColors.ButtonFace;
            txtSearch.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtSearch.BorderRadius = 8;
            txtSearch.BorderThickness = 2;
            txtSearch.Enabled = false;
            txtSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = SystemColors.GrayText;
            txtSearch.InnerBackColor = SystemColors.InactiveCaption;
            txtSearch.InnerForeColor = Color.Gray;
            txtSearch.IsPasswordField = false;
            txtSearch.Location = new Point(224, 115);
            txtSearch.Name = "txtSearch";
            txtSearch.PasswordChar = '\0';
            txtSearch.PlaceholderColor = Color.Gray;
            txtSearch.PlaceholderText = "";
            txtSearch.Size = new Size(268, 39);
            txtSearch.TabIndex = 45;
            txtSearch.Text = "0.00";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(577, 82);
            label2.Name = "label2";
            label2.Size = new Size(138, 19);
            label2.TabIndex = 48;
            label2.Text = "TOTAL PRODUCTS";
            // 
            // customTextBox1
            // 
            customTextBox1.BorderColor = SystemColors.ButtonFace;
            customTextBox1.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox1.BorderRadius = 8;
            customTextBox1.BorderThickness = 2;
            customTextBox1.Enabled = false;
            customTextBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox1.ForeColor = SystemColors.GrayText;
            customTextBox1.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox1.InnerForeColor = Color.Gray;
            customTextBox1.IsPasswordField = false;
            customTextBox1.Location = new Point(519, 115);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(268, 39);
            customTextBox1.TabIndex = 47;
            customTextBox1.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.LightSteelBlue;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(887, 82);
            label3.Name = "label3";
            label3.Size = new Size(131, 19);
            label3.TabIndex = 50;
            label3.Text = "PRODUCTS SOLD";
            // 
            // customTextBox2
            // 
            customTextBox2.BorderColor = SystemColors.ButtonFace;
            customTextBox2.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox2.BorderRadius = 8;
            customTextBox2.BorderThickness = 2;
            customTextBox2.Enabled = false;
            customTextBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox2.ForeColor = SystemColors.GrayText;
            customTextBox2.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox2.InnerForeColor = Color.Gray;
            customTextBox2.IsPasswordField = false;
            customTextBox2.Location = new Point(810, 115);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(268, 39);
            customTextBox2.TabIndex = 49;
            customTextBox2.Text = "0";
            // 
            // btnManageUsers
            // 
            btnManageUsers.BackColor = Color.SteelBlue;
            btnManageUsers.BorderColor = Color.Transparent;
            btnManageUsers.BorderRadius = 20;
            btnManageUsers.BorderSize = 0;
            btnManageUsers.FlatAppearance.BorderSize = 0;
            btnManageUsers.FlatStyle = FlatStyle.Flat;
            btnManageUsers.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManageUsers.ForeColor = Color.White;
            btnManageUsers.Location = new Point(136, 220);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(179, 140);
            btnManageUsers.TabIndex = 51;
            btnManageUsers.Text = "MANAGE USERS";
            btnManageUsers.UseVisualStyleBackColor = false;
            btnManageUsers.Click += btnManageUsers_Click;
            // 
            // btnManageCategory
            // 
            btnManageCategory.BackColor = Color.SteelBlue;
            btnManageCategory.BorderColor = Color.Transparent;
            btnManageCategory.BorderRadius = 20;
            btnManageCategory.BorderSize = 0;
            btnManageCategory.FlatAppearance.BorderSize = 0;
            btnManageCategory.FlatStyle = FlatStyle.Flat;
            btnManageCategory.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManageCategory.ForeColor = Color.White;
            btnManageCategory.Location = new Point(402, 220);
            btnManageCategory.Name = "btnManageCategory";
            btnManageCategory.Size = new Size(179, 140);
            btnManageCategory.TabIndex = 52;
            btnManageCategory.Text = "MANAGE CATEGORY";
            btnManageCategory.UseVisualStyleBackColor = false;
            btnManageCategory.Click += btnManageCategory_Click;
            // 
            // btnManageProducts
            // 
            btnManageProducts.BackColor = Color.SteelBlue;
            btnManageProducts.BorderColor = Color.Transparent;
            btnManageProducts.BorderRadius = 20;
            btnManageProducts.BorderSize = 0;
            btnManageProducts.FlatAppearance.BorderSize = 0;
            btnManageProducts.FlatStyle = FlatStyle.Flat;
            btnManageProducts.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManageProducts.ForeColor = Color.White;
            btnManageProducts.Location = new Point(665, 220);
            btnManageProducts.Name = "btnManageProducts";
            btnManageProducts.Size = new Size(179, 140);
            btnManageProducts.TabIndex = 53;
            btnManageProducts.Text = "MANAGE PRODUCTS";
            btnManageProducts.UseVisualStyleBackColor = false;
            btnManageProducts.Click += btnManageProducts_Click;
            // 
            // btnManageStocks
            // 
            btnManageStocks.BackColor = Color.SteelBlue;
            btnManageStocks.BorderColor = Color.Transparent;
            btnManageStocks.BorderRadius = 20;
            btnManageStocks.BorderSize = 0;
            btnManageStocks.FlatAppearance.BorderSize = 0;
            btnManageStocks.FlatStyle = FlatStyle.Flat;
            btnManageStocks.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManageStocks.ForeColor = Color.White;
            btnManageStocks.Location = new Point(919, 220);
            btnManageStocks.Name = "btnManageStocks";
            btnManageStocks.Size = new Size(179, 140);
            btnManageStocks.TabIndex = 54;
            btnManageStocks.Text = "MANAGE STOCKS";
            btnManageStocks.UseVisualStyleBackColor = false;
            btnManageStocks.Click += btnManageStocks_Click;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnManageStocks);
            Controls.Add(btnManageProducts);
            Controls.Add(btnManageCategory);
            Controls.Add(btnManageUsers);
            Controls.Add(label3);
            Controls.Add(customTextBox2);
            Controls.Add(label2);
            Controls.Add(customTextBox1);
            Controls.Add(label12);
            Controls.Add(txtSearch);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdminDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AdminDashboard";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Button closeButton;
        private Label titleLabel;
        private Label lblAdminName;
        private RoundedButton btnLogOut;
        private Label label12;
        private CustomControls.CustomTextBox txtSearch;
        private Label label2;
        private CustomControls.CustomTextBox customTextBox1;
        private Label label3;
        private CustomControls.CustomTextBox customTextBox2;
        private RoundedButton btnManageUsers;
        private RoundedButton btnManageCategory;
        private RoundedButton btnManageProducts;
        private RoundedButton btnManageStocks;
    }
}