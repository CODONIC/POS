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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminDashboard));
            titleBar = new Panel();
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnLogOut = new RoundedButton();
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
            roundedButton2 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            roundedButton4 = new RoundedButton();
            roundedButton5 = new RoundedButton();
            titleBar.SuspendLayout();
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
            titleBar.Size = new Size(1264, 48);
            titleBar.TabIndex = 17;
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
            btnLogOut.Location = new Point(26, 622);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(86, 37);
            btnLogOut.TabIndex = 22;
            btnLogOut.Text = "Logout";
            btnLogOut.TextAlign = ContentAlignment.TopCenter;
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(285, 82);
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
            txtSearch.Location = new Point(208, 115);
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
            label2.Location = new Point(561, 82);
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
            customTextBox1.Location = new Point(503, 115);
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
            label3.Location = new Point(871, 82);
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
            customTextBox2.Location = new Point(794, 115);
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
            btnManageUsers.Image = (Image)resources.GetObject("btnManageUsers.Image");
            btnManageUsers.Location = new Point(208, 196);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(180, 180);
            btnManageUsers.TabIndex = 51;
            btnManageUsers.Text = "USERS";
            btnManageUsers.TextAlign = ContentAlignment.BottomCenter;
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
            btnManageCategory.Image = (Image)resources.GetObject("btnManageCategory.Image");
            btnManageCategory.Location = new Point(437, 196);
            btnManageCategory.Name = "btnManageCategory";
            btnManageCategory.Size = new Size(180, 180);
            btnManageCategory.TabIndex = 52;
            btnManageCategory.Text = "CATEGORIES";
            btnManageCategory.TextAlign = ContentAlignment.BottomCenter;
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
            btnManageProducts.Image = (Image)resources.GetObject("btnManageProducts.Image");
            btnManageProducts.Location = new Point(659, 196);
            btnManageProducts.Name = "btnManageProducts";
            btnManageProducts.Size = new Size(180, 180);
            btnManageProducts.TabIndex = 53;
            btnManageProducts.Text = "PRODUCTS";
            btnManageProducts.TextAlign = ContentAlignment.BottomCenter;
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
            btnManageStocks.Image = (Image)resources.GetObject("btnManageStocks.Image");
            btnManageStocks.Location = new Point(882, 196);
            btnManageStocks.Name = "btnManageStocks";
            btnManageStocks.Size = new Size(180, 180);
            btnManageStocks.TabIndex = 54;
            btnManageStocks.Text = "INVENTORY ";
            btnManageStocks.TextAlign = ContentAlignment.BottomCenter;
            btnManageStocks.UseVisualStyleBackColor = false;
            btnManageStocks.Click += btnManageStocks_Click;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.SteelBlue;
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 20;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Image = (Image)resources.GetObject("roundedButton2.Image");
            roundedButton2.Location = new Point(437, 410);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(180, 180);
            roundedButton2.TabIndex = 56;
            roundedButton2.Text = "BUSINESS STATS";
            roundedButton2.TextAlign = ContentAlignment.BottomCenter;
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.SteelBlue;
            roundedButton3.BorderColor = Color.Transparent;
            roundedButton3.BorderRadius = 20;
            roundedButton3.BorderSize = 0;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Image = (Image)resources.GetObject("roundedButton3.Image");
            roundedButton3.Location = new Point(208, 410);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(180, 180);
            roundedButton3.TabIndex = 57;
            roundedButton3.Text = "TRANSACTIONS";
            roundedButton3.TextAlign = ContentAlignment.BottomCenter;
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.SteelBlue;
            roundedButton4.BorderColor = Color.Transparent;
            roundedButton4.BorderRadius = 20;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Image = (Image)resources.GetObject("roundedButton4.Image");
            roundedButton4.Location = new Point(659, 410);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(180, 180);
            roundedButton4.TabIndex = 58;
            roundedButton4.Text = "SALES REPORTS";
            roundedButton4.TextAlign = ContentAlignment.BottomCenter;
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = Color.SteelBlue;
            roundedButton5.BorderColor = Color.Transparent;
            roundedButton5.BorderRadius = 20;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Image = (Image)resources.GetObject("roundedButton5.Image");
            roundedButton5.Location = new Point(882, 410);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(180, 180);
            roundedButton5.TabIndex = 59;
            roundedButton5.Text = "SETTINGS";
            roundedButton5.TextAlign = ContentAlignment.BottomCenter;
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(roundedButton5);
            Controls.Add(roundedButton4);
            Controls.Add(roundedButton3);
            Controls.Add(roundedButton2);
            Controls.Add(btnLogOut);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton4;
        private RoundedButton roundedButton5;
    }
}