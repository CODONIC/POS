namespace POS.Admin
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            titleBar = new Panel();
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnCompanyInfo = new RoundedButton();
            label1 = new Label();
            label2 = new Label();
            btnChangeVAT = new RoundedButton();
            label3 = new Label();
            label4 = new Label();
            roundedButton4 = new RoundedButton();
            roundedButton5 = new RoundedButton();
            roundedButton6 = new RoundedButton();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
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
            titleBar.Size = new Size(641, 48);
            titleBar.TabIndex = 18;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(406, 12);
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
            closeButton.Location = new Point(591, 0);
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
            // btnCompanyInfo
            // 
            btnCompanyInfo.BackColor = Color.SteelBlue;
            btnCompanyInfo.BorderColor = Color.Transparent;
            btnCompanyInfo.BorderRadius = 20;
            btnCompanyInfo.BorderSize = 0;
            btnCompanyInfo.FlatAppearance.BorderSize = 0;
            btnCompanyInfo.FlatStyle = FlatStyle.Flat;
            btnCompanyInfo.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCompanyInfo.ForeColor = Color.White;
            btnCompanyInfo.Location = new Point(31, 164);
            btnCompanyInfo.Name = "btnCompanyInfo";
            btnCompanyInfo.Size = new Size(200, 61);
            btnCompanyInfo.TabIndex = 37;
            btnCompanyInfo.Text = "Company Info";
            btnCompanyInfo.UseVisualStyleBackColor = false;
            btnCompanyInfo.Click += btnCompanyInfo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 15.75F);
            label1.Location = new Point(31, 77);
            label1.Name = "label1";
            label1.Size = new Size(102, 24);
            label1.TabIndex = 38;
            label1.Text = "SETTINGS ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(31, 101);
            label2.Name = "label2";
            label2.Size = new Size(544, 17);
            label2.TabIndex = 39;
            label2.Text = "───────────────────────────────────────────────────────────────────";
            // 
            // btnChangeVAT
            // 
            btnChangeVAT.BackColor = Color.SteelBlue;
            btnChangeVAT.BorderColor = Color.Transparent;
            btnChangeVAT.BorderRadius = 20;
            btnChangeVAT.BorderSize = 0;
            btnChangeVAT.FlatAppearance.BorderSize = 0;
            btnChangeVAT.FlatStyle = FlatStyle.Flat;
            btnChangeVAT.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnChangeVAT.ForeColor = Color.White;
            btnChangeVAT.Location = new Point(31, 254);
            btnChangeVAT.Name = "btnChangeVAT";
            btnChangeVAT.Size = new Size(200, 57);
            btnChangeVAT.TabIndex = 40;
            btnChangeVAT.Text = "Change VAT";
            btnChangeVAT.UseVisualStyleBackColor = false;
            btnChangeVAT.Click += btnChangeVAT_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 15.75F);
            label3.Location = new Point(31, 445);
            label3.Name = "label3";
            label3.Size = new Size(193, 24);
            label3.TabIndex = 41;
            label3.Text = "Having Problems?";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(31, 469);
            label4.Name = "label4";
            label4.Size = new Size(544, 17);
            label4.TabIndex = 42;
            label4.Text = "───────────────────────────────────────────────────────────────────";
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = SystemColors.InactiveCaption;
            roundedButton4.BorderColor = Color.Transparent;
            roundedButton4.BorderRadius = 20;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(31, 332);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(200, 57);
            roundedButton4.TabIndex = 45;
            roundedButton4.Text = "Themes";
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = SystemColors.InactiveCaption;
            roundedButton5.BorderColor = Color.Transparent;
            roundedButton5.BorderRadius = 20;
            roundedButton5.BorderSize = 0;
            roundedButton5.FlatAppearance.BorderSize = 0;
            roundedButton5.FlatStyle = FlatStyle.Flat;
            roundedButton5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(336, 164);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(200, 57);
            roundedButton5.TabIndex = 46;
            roundedButton5.Text = "Receipt and Printing";
            roundedButton5.UseVisualStyleBackColor = false;
            // 
            // roundedButton6
            // 
            roundedButton6.BackColor = SystemColors.InactiveCaption;
            roundedButton6.BorderColor = Color.Transparent;
            roundedButton6.BorderRadius = 20;
            roundedButton6.BorderSize = 0;
            roundedButton6.FlatAppearance.BorderSize = 0;
            roundedButton6.FlatStyle = FlatStyle.Flat;
            roundedButton6.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton6.ForeColor = Color.White;
            roundedButton6.Location = new Point(336, 250);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(200, 57);
            roundedButton6.TabIndex = 47;
            roundedButton6.Text = "Backup and Restore";
            roundedButton6.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 15.75F);
            label5.Location = new Point(31, 502);
            label5.Name = "label5";
            label5.Size = new Size(156, 24);
            label5.TabIndex = 48;
            label5.Text = "Contact us at ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.MenuHighlight;
            label6.Location = new Point(180, 502);
            label6.Name = "label6";
            label6.Size = new Size(219, 25);
            label6.TabIndex = 49;
            label6.Text = "tinderoco@gmail.com";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.MenuHighlight;
            label7.Location = new Point(124, 543);
            label7.Name = "label7";
            label7.Size = new Size(156, 25);
            label7.TabIndex = 50;
            label7.Text = "0921 242 2023";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 15.75F);
            label8.Location = new Point(31, 543);
            label8.Name = "label8";
            label8.Size = new Size(97, 24);
            label8.TabIndex = 51;
            label8.Text = "Phone #";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(641, 600);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(roundedButton6);
            Controls.Add(roundedButton5);
            Controls.Add(roundedButton4);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnChangeVAT);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCompanyInfo);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SettingsForm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblAdminName;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnCompanyInfo;
        private Label label1;
        private Label label2;
        private RoundedButton btnChangeVAT;
        private Label label3;
        private Label label4;
        private RoundedButton roundedButton4;
        private RoundedButton roundedButton5;
        private RoundedButton roundedButton6;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}