namespace POS.StartUpForms
{
    partial class OldCompanyFrm
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
            btnAuthenticate = new RoundedButton();
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
            titleBar.Size = new Size(683, 40);
            titleBar.TabIndex = 19;
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
            label4.Location = new Point(164, 141);
            label4.Name = "label4";
            label4.Size = new Size(367, 24);
            label4.TabIndex = 20;
            label4.Text = "Enter your Existing Company Name";
            // 
            // txtCompanyName
            // 
            txtCompanyName.BorderColor = SystemColors.ButtonFace;
            txtCompanyName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtCompanyName.BorderRadius = 8;
            txtCompanyName.BorderThickness = 2;
            txtCompanyName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCompanyName.ForeColor = SystemColors.Desktop;
            txtCompanyName.InnerBackColor = SystemColors.InactiveCaption;
            txtCompanyName.InnerForeColor = Color.Gray;
            txtCompanyName.IsPasswordField = false;
            txtCompanyName.Location = new Point(231, 189);
            txtCompanyName.Name = "txtCompanyName";
            txtCompanyName.PasswordChar = '\0';
            txtCompanyName.PlaceholderColor = Color.Gray;
            txtCompanyName.PlaceholderText = "";
            txtCompanyName.Size = new Size(220, 45);
            txtCompanyName.TabIndex = 21;
            // 
            // btnAuthenticate
            // 
            btnAuthenticate.BackColor = Color.SteelBlue;
            btnAuthenticate.BorderColor = Color.Transparent;
            btnAuthenticate.BorderRadius = 20;
            btnAuthenticate.BorderSize = 0;
            btnAuthenticate.FlatAppearance.BorderSize = 0;
            btnAuthenticate.FlatStyle = FlatStyle.Flat;
            btnAuthenticate.Font = new Font("Dubai", 13.75F, FontStyle.Bold);
            btnAuthenticate.ForeColor = Color.White;
            btnAuthenticate.Location = new Point(231, 255);
            btnAuthenticate.Name = "btnAuthenticate";
            btnAuthenticate.Size = new Size(220, 46);
            btnAuthenticate.TabIndex = 22;
            btnAuthenticate.Text = "Authenticate";
            btnAuthenticate.UseVisualStyleBackColor = false;
            btnAuthenticate.Click += btnAuthenticate_Click;
            // 
            // OldCompanyFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 450);
            Controls.Add(btnAuthenticate);
            Controls.Add(txtCompanyName);
            Controls.Add(label4);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OldCompanyFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "OldCompanyFrm";
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
        private RoundedButton btnAuthenticate;
    }
}