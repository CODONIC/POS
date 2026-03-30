namespace POS
{
    partial class LogInForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogInForm));
            pictureBox1 = new PictureBox();
            btnSignIn = new RoundedButton();
            panel1 = new Panel();
            label3 = new Label();
            label2 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtUsername = new CustomControls.CustomTextBox();
            txtPassword = new CustomControls.CustomTextBox();
            label1 = new Label();
            titleBar = new Panel();
            closeButton = new Button();
            titleLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(453, 72);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(113, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // btnSignIn
            // 
            btnSignIn.BackColor = Color.SteelBlue;
            btnSignIn.BorderColor = Color.Transparent;
            btnSignIn.BorderRadius = 20;
            btnSignIn.BorderSize = 0;
            btnSignIn.FlatAppearance.BorderSize = 0;
            btnSignIn.FlatStyle = FlatStyle.Flat;
            btnSignIn.Font = new Font("Dubai", 13.75F, FontStyle.Bold);
            btnSignIn.ForeColor = Color.White;
            btnSignIn.Location = new Point(397, 346);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(220, 46);
            btnSignIn.TabIndex = 9;
            btnSignIn.Text = "Login";
            btnSignIn.UseVisualStyleBackColor = false;
            btnSignIn.Click += btnSignInClick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(41, 128, 185);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Location = new Point(0, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(324, 574);
            panel1.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(12, 328);
            label3.Name = "label3";
            label3.Size = new Size(140, 20);
            label3.TabIndex = 6;
            label3.Text = "Username: admin ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(12, 348);
            label2.Name = "label2";
            label2.Size = new Size(156, 20);
            label2.TabIndex = 5;
            label2.Text = "Password: admin123";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.ButtonFace;
            label8.Location = new Point(102, 527);
            label8.Name = "label8";
            label8.Size = new Size(103, 17);
            label8.TabIndex = 4;
            label8.Text = "Developed by ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ButtonFace;
            label7.Location = new Point(12, 251);
            label7.Name = "label7";
            label7.Size = new Size(81, 24);
            label7.TabIndex = 3;
            label7.Text = "System";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Location = new Point(12, 223);
            label6.Name = "label6";
            label6.Size = new Size(292, 24);
            label6.TabIndex = 2;
            label6.Text = "Point of Sales Management";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(71, 544);
            label5.Name = "label5";
            label5.Size = new Size(166, 21);
            label5.TabIndex = 1;
            label5.Text = "Tindero Corporation";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(12, 196);
            label4.Name = "label4";
            label4.Size = new Size(178, 24);
            label4.TabIndex = 0;
            label4.Text = "Welcome to the";
            // 
            // txtUsername
            // 
            txtUsername.BorderColor = SystemColors.ButtonFace;
            txtUsername.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtUsername.BorderRadius = 8;
            txtUsername.BorderThickness = 2;
            txtUsername.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtUsername.ForeColor = SystemColors.GrayText;
            txtUsername.InnerBackColor = SystemColors.InactiveCaption;
            txtUsername.InnerForeColor = Color.Gray;
            txtUsername.IsPasswordField = false;
            txtUsername.Location = new Point(397, 244);
            txtUsername.Name = "txtUsername";
            txtUsername.PasswordChar = '\0';
            txtUsername.PlaceholderColor = Color.Gray;
            txtUsername.PlaceholderText = "";
            txtUsername.Size = new Size(220, 45);
            txtUsername.TabIndex = 13;
            txtUsername.Text = "Username";
            txtUsername.Enter += txtUsername_Enter;
            txtUsername.KeyDown += txt_KeyDown;
            txtUsername.Leave += txtUsername_Leave;
            // 
            // txtPassword
            // 
            txtPassword.BorderColor = SystemColors.ButtonFace;
            txtPassword.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtPassword.BorderRadius = 8;
            txtPassword.BorderThickness = 2;
            txtPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPassword.ForeColor = SystemColors.GrayText;
            txtPassword.InnerBackColor = SystemColors.InactiveCaption;
            txtPassword.InnerForeColor = Color.Gray;
            txtPassword.IsPasswordField = false;
            txtPassword.Location = new Point(397, 295);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '\0';
            txtPassword.PlaceholderColor = Color.Gray;
            txtPassword.PlaceholderText = "";
            txtPassword.Size = new Size(220, 45);
            txtPassword.TabIndex = 14;
            txtPassword.Text = "Password";
            txtPassword.Enter += txtPassword_Enter;
            txtPassword.KeyDown += txt_KeyDown;
            txtPassword.Leave += txtPassword_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gill Sans Ultra Bold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(30, 45, 61);
            label1.Location = new Point(467, 187);
            label1.Name = "label1";
            label1.Size = new Size(86, 25);
            label1.TabIndex = 15;
            label1.Text = "Sign In";
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(679, 40);
            titleBar.TabIndex = 16;
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
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(679, 604);
            ControlBox = false;
            Controls.Add(titleBar);
            Controls.Add(label1);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(panel1);
            Controls.Add(btnSignIn);
            Controls.Add(pictureBox1);
            ForeColor = SystemColors.ActiveCaption;
            FormBorderStyle = FormBorderStyle.None;
            Name = "LogInForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogIn Form";
            Load += LogInForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomControls.CustomTextBox txtUsername;
        private PictureBox pictureBox1;
        private RoundedButton btnSignIn;
        private Panel panel1;
        private Label label5;
        private Label label4;
        private Label label8;
        private Label label7;
        private Label label6;
        private CustomControls.CustomTextBox txtPassword;
        private Label label1;
        private Panel titleBar;
        private Label titleLabel;
        private Button closeButton;
        private Label label3;
        private Label label2;
    }
}