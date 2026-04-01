namespace POS
{
    partial class NewCompanyFrm
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
            closeButton = new Button();
            titleLabel = new Label();
            label4 = new Label();
            txtCompanyName = new CustomControls.CustomTextBox();
            btnCreateCompany = new RoundedButton();
            titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(680, 40);
            titleBar.TabIndex = 17;
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
            closeButton.Location = new Point(638, 0);
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
            titleLabel.Location = new Point(12, 9);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(174, 164);
            label4.Name = "label4";
            label4.Size = new Size(323, 24);
            label4.TabIndex = 18;
            label4.Text = "What is your Company Name?";
            // 
            // txtCompanyName
            // 
            txtCompanyName.BorderColor = SystemColors.ButtonFace;
            txtCompanyName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtCompanyName.BorderRadius = 8;
            txtCompanyName.BorderThickness = 2;
            txtCompanyName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCompanyName.ForeColor = SystemColors.GrayText;
            txtCompanyName.InnerBackColor = SystemColors.InactiveCaption;
            txtCompanyName.InnerForeColor = Color.Gray;
            txtCompanyName.IsPasswordField = false;
            txtCompanyName.Location = new Point(230, 221);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.PasswordChar = '\0';
            txtCompanyName.PlaceholderColor = Color.Gray;
            txtCompanyName.PlaceholderText = "";
            txtCompanyName.Size = new Size(220, 45);
            txtCompanyName.TabIndex = 19;
            txtCompanyName.TextChanged += txtCompanyName_TextChanged;
            // 
            // btnCreateCompany
            // 
            btnCreateCompany.BackColor = Color.SteelBlue;
            btnCreateCompany.BorderColor = Color.Transparent;
            btnCreateCompany.BorderRadius = 20;
            btnCreateCompany.BorderSize = 0;
            btnCreateCompany.FlatAppearance.BorderSize = 0;
            btnCreateCompany.FlatStyle = FlatStyle.Flat;
            btnCreateCompany.Font = new Font("Dubai", 13.75F, FontStyle.Bold);
            btnCreateCompany.ForeColor = Color.White;
            btnCreateCompany.Location = new Point(245, 301);
            btnCreateCompany.Name = "btnCreateCompany";
            btnCreateCompany.Size = new Size(178, 46);
            btnCreateCompany.TabIndex = 20;
            btnCreateCompany.Text = "Create ";
            btnCreateCompany.UseVisualStyleBackColor = false;
            btnCreateCompany.Click += btnCreateCompany_Click;
            // 
            // NewCompanyFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 431);
            Controls.Add(btnCreateCompany);
            Controls.Add(txtCompanyName);
            Controls.Add(label4);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "NewCompanyFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NewCompanyFrm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Button closeButton;
        private Label titleLabel;
        private Label label4;
        private CustomControls.CustomTextBox txtCompanyName;
        private RoundedButton btnCreateCompany;
    }
}