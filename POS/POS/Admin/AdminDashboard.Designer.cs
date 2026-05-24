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
            btnManageUsers = new RoundedButton();
            btnManageCategory = new RoundedButton();
            btnManageProducts = new RoundedButton();
            btnManageStocks = new RoundedButton();
            btnBusinessStats = new RoundedButton();
            btnTransactions = new RoundedButton();
            btnSalesReport = new RoundedButton();
            btnSettings = new RoundedButton();
            btnAudit = new RoundedButton();
            btnSubscriptionOffers = new Button();
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
            btnManageUsers.Location = new Point(200, 132);
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
            btnManageCategory.Location = new Point(429, 132);
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
            btnManageProducts.Location = new Point(651, 132);
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
            btnManageStocks.Location = new Point(874, 132);
            btnManageStocks.Name = "btnManageStocks";
            btnManageStocks.Size = new Size(180, 180);
            btnManageStocks.TabIndex = 54;
            btnManageStocks.Text = "INVENTORY ";
            btnManageStocks.TextAlign = ContentAlignment.BottomCenter;
            btnManageStocks.UseVisualStyleBackColor = false;
            btnManageStocks.Click += btnManageStocks_Click;
            // 
            // btnBusinessStats
            // 
            btnBusinessStats.BackColor = Color.SteelBlue;
            btnBusinessStats.BorderColor = Color.Transparent;
            btnBusinessStats.BorderRadius = 20;
            btnBusinessStats.BorderSize = 0;
            btnBusinessStats.FlatAppearance.BorderSize = 0;
            btnBusinessStats.FlatStyle = FlatStyle.Flat;
            btnBusinessStats.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBusinessStats.ForeColor = Color.White;
            btnBusinessStats.Image = (Image)resources.GetObject("btnBusinessStats.Image");
            btnBusinessStats.Location = new Point(429, 346);
            btnBusinessStats.Name = "btnBusinessStats";
            btnBusinessStats.Size = new Size(180, 180);
            btnBusinessStats.TabIndex = 56;
            btnBusinessStats.Text = "BUSINESS STATS";
            btnBusinessStats.TextAlign = ContentAlignment.BottomCenter;
            btnBusinessStats.UseVisualStyleBackColor = false;
            btnBusinessStats.Click += btnBusinessStats_Click;
            // 
            // btnTransactions
            // 
            btnTransactions.BackColor = Color.SteelBlue;
            btnTransactions.BorderColor = Color.Transparent;
            btnTransactions.BorderRadius = 20;
            btnTransactions.BorderSize = 0;
            btnTransactions.FlatAppearance.BorderSize = 0;
            btnTransactions.FlatStyle = FlatStyle.Flat;
            btnTransactions.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTransactions.ForeColor = Color.White;
            btnTransactions.Image = (Image)resources.GetObject("btnTransactions.Image");
            btnTransactions.Location = new Point(200, 346);
            btnTransactions.Name = "btnTransactions";
            btnTransactions.Size = new Size(180, 180);
            btnTransactions.TabIndex = 57;
            btnTransactions.Text = "TRANSACTIONS";
            btnTransactions.TextAlign = ContentAlignment.BottomCenter;
            btnTransactions.UseVisualStyleBackColor = false;
            btnTransactions.Click += btnTransactions_Click;
            // 
            // btnSalesReport
            // 
            btnSalesReport.BackColor = Color.SteelBlue;
            btnSalesReport.BorderColor = Color.Transparent;
            btnSalesReport.BorderRadius = 20;
            btnSalesReport.BorderSize = 0;
            btnSalesReport.FlatAppearance.BorderSize = 0;
            btnSalesReport.FlatStyle = FlatStyle.Flat;
            btnSalesReport.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalesReport.ForeColor = Color.White;
            btnSalesReport.Image = (Image)resources.GetObject("btnSalesReport.Image");
            btnSalesReport.Location = new Point(651, 346);
            btnSalesReport.Name = "btnSalesReport";
            btnSalesReport.Size = new Size(180, 180);
            btnSalesReport.TabIndex = 58;
            btnSalesReport.Text = "SALES REPORTS";
            btnSalesReport.TextAlign = ContentAlignment.BottomCenter;
            btnSalesReport.UseVisualStyleBackColor = false;
            btnSalesReport.Click += btnSalesReport_Click;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.SteelBlue;
            btnSettings.BorderColor = Color.Transparent;
            btnSettings.BorderRadius = 20;
            btnSettings.BorderSize = 0;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSettings.ForeColor = Color.White;
            btnSettings.Image = (Image)resources.GetObject("btnSettings.Image");
            btnSettings.Location = new Point(1157, 579);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(80, 80);
            btnSettings.TabIndex = 59;
            btnSettings.TextAlign = ContentAlignment.BottomCenter;
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnAudit
            // 
            btnAudit.BackColor = Color.SteelBlue;
            btnAudit.BorderColor = Color.Transparent;
            btnAudit.BorderRadius = 20;
            btnAudit.BorderSize = 0;
            btnAudit.FlatAppearance.BorderSize = 0;
            btnAudit.FlatStyle = FlatStyle.Flat;
            btnAudit.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAudit.ForeColor = Color.White;
            btnAudit.Image = (Image)resources.GetObject("btnAudit.Image");
            btnAudit.Location = new Point(874, 346);
            btnAudit.Name = "btnAudit";
            btnAudit.Size = new Size(180, 180);
            btnAudit.TabIndex = 60;
            btnAudit.Text = "EMPLOYEE LOGS";
            btnAudit.TextAlign = ContentAlignment.BottomCenter;
            btnAudit.UseVisualStyleBackColor = false;
            btnAudit.Click += btnAudit_Click;
            //
            // btnSubscriptionOffers
            btnSubscriptionOffers.BackColor = Color.Black;
            btnSubscriptionOffers.ForeColor = Color.White;
            btnSubscriptionOffers.FlatStyle = FlatStyle.Flat;
            btnSubscriptionOffers.FlatAppearance.BorderSize = 0;
            btnSubscriptionOffers.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 40, 40);
            btnSubscriptionOffers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSubscriptionOffers.Cursor = Cursors.Hand;
            btnSubscriptionOffers.Location = new Point(1090, 62);
            btnSubscriptionOffers.Size = new Size(160, 36);
            btnSubscriptionOffers.Text = "Subscription Offers";
            btnSubscriptionOffers.UseVisualStyleBackColor = false;
            btnSubscriptionOffers.Click += btnSubscriptionOffers_Click;
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnAudit);
            Controls.Add(btnSettings);
            Controls.Add(btnSalesReport);
            Controls.Add(btnTransactions);
            Controls.Add(btnBusinessStats);
            Controls.Add(btnLogOut);
            Controls.Add(btnManageStocks);
            Controls.Add(btnManageProducts);
            Controls.Add(btnManageCategory);
            Controls.Add(btnManageUsers);
            Controls.Add(titleBar);
            Controls.Add(btnSubscriptionOffers);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AdminDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AdminDashboard";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ResumeLayout(false);


        }


        #endregion

        private Panel titleBar;
        private Button closeButton;
        private Label titleLabel;
        private Label lblAdminName;
        private RoundedButton btnLogOut;
        private RoundedButton btnManageUsers;
        private RoundedButton btnManageCategory;
        private RoundedButton btnManageProducts;
        private RoundedButton btnManageStocks;
        private RoundedButton btnBusinessStats;
        private RoundedButton btnTransactions;
        private RoundedButton btnSalesReport;
        private RoundedButton btnSettings;
        private RoundedButton btnAudit;
        private Button btnSubscriptionOffers;
    }
}