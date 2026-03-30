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
            label1 = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            customTextBox3 = new CustomControls.CustomTextBox();
            customTextBox1 = new CustomControls.CustomTextBox();
            customTextBox2 = new CustomControls.CustomTextBox();
            btnManageUsers = new RoundedButton();
            btnManageProd = new RoundedButton();
            btnManageCategory = new RoundedButton();
            btnManageStocks = new RoundedButton();
            titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(btnLogOut);
            titleBar.Controls.Add(label1);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(1005, 10);
            label1.Name = "label1";
            label1.Size = new Size(179, 21);
            label1.TabIndex = 21;
            label1.Text = "adminName | Admin";
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
            closeButton.Location = new Point(1223, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(41, 40);
            closeButton.TabIndex = 17;
            closeButton.Text = " X";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(12, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(226, 109);
            label2.Name = "label2";
            label2.Size = new Size(91, 21);
            label2.TabIndex = 23;
            label2.Text = "Total Sales";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(590, 109);
            label3.Name = "label3";
            label3.Size = new Size(96, 21);
            label3.TabIndex = 24;
            label3.Text = "Total Items";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(931, 109);
            label4.Name = "label4";
            label4.Size = new Size(132, 21);
            label4.TabIndex = 25;
            label4.Text = "Total Items Sold";
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
            customTextBox3.InnerForeColor = Color.Gray;
            customTextBox3.IsPasswordField = false;
            customTextBox3.Location = new Point(117, 133);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(302, 47);
            customTextBox3.TabIndex = 32;
            customTextBox3.Text = "0.00";
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
            customTextBox1.Location = new Point(487, 133);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(294, 47);
            customTextBox1.TabIndex = 33;
            customTextBox1.Text = "0";
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
            customTextBox2.Location = new Point(851, 133);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(304, 47);
            customTextBox2.TabIndex = 34;
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
            btnManageUsers.Location = new Point(181, 300);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(192, 163);
            btnManageUsers.TabIndex = 35;
            btnManageUsers.Text = "Manage Users";
            btnManageUsers.UseVisualStyleBackColor = false;
            btnManageUsers.Click += btnManageUsers_Click;
            // 
            // btnManageProd
            // 
            btnManageProd.BackColor = Color.SteelBlue;
            btnManageProd.BorderColor = Color.Transparent;
            btnManageProd.BorderRadius = 20;
            btnManageProd.BorderSize = 0;
            btnManageProd.FlatAppearance.BorderSize = 0;
            btnManageProd.FlatStyle = FlatStyle.Flat;
            btnManageProd.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnManageProd.ForeColor = Color.White;
            btnManageProd.Location = new Point(644, 300);
            btnManageProd.Name = "btnManageProd";
            btnManageProd.Size = new Size(192, 163);
            btnManageProd.TabIndex = 36;
            btnManageProd.Text = "Manage Products";
            btnManageProd.UseVisualStyleBackColor = false;
            btnManageProd.Click += btnManageProd_Click;
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
            btnManageCategory.Location = new Point(416, 300);
            btnManageCategory.Name = "btnManageCategory";
            btnManageCategory.Size = new Size(192, 163);
            btnManageCategory.TabIndex = 37;
            btnManageCategory.Text = "Manage Product Categories";
            btnManageCategory.UseVisualStyleBackColor = false;
            btnManageCategory.Click += btnManageCategory_Click;
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
            btnManageStocks.Location = new Point(871, 300);
            btnManageStocks.Name = "btnManageStocks";
            btnManageStocks.Size = new Size(192, 163);
            btnManageStocks.TabIndex = 38;
            btnManageStocks.Text = "Manage Stocks";
            btnManageStocks.UseVisualStyleBackColor = false;
            btnManageStocks.Click += btnManageStocks_Click;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(btnManageStocks);
            Controls.Add(btnManageCategory);
            Controls.Add(btnManageProd);
            Controls.Add(btnManageUsers);
            Controls.Add(customTextBox2);
            Controls.Add(customTextBox1);
            Controls.Add(customTextBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
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
        private Label label1;
        private RoundedButton btnLogOut;
        private Label label2;
        private Label label3;
        private Label label4;
        private CustomControls.CustomTextBox customTextBox3;
        private CustomControls.CustomTextBox customTextBox1;
        private CustomControls.CustomTextBox customTextBox2;
        private RoundedButton btnManageUsers;
        private RoundedButton btnManageProd;
        private RoundedButton btnManageCategory;
        private RoundedButton btnManageStocks;
    }
}