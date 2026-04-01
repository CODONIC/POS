namespace POS.StartUpForms
{
    partial class WelcomeFrm
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
            label1 = new Label();
            label2 = new Label();
            btnNewUser = new RoundedButton();
            btnOldUser = new RoundedButton();
            chckDontShow = new CheckBox();
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
            titleBar.Size = new Size(682, 40);
            titleBar.TabIndex = 18;
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
            label4.Location = new Point(207, 115);
            label4.Name = "label4";
            label4.Size = new Size(282, 24);
            label4.TabIndex = 19;
            label4.Text = "Welcome to TINDERO POS!";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(231, 152);
            label1.Name = "label1";
            label1.Size = new Size(232, 24);
            label1.TabIndex = 20;
            label1.Text = "Before we get started";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(167, 187);
            label2.Name = "label2";
            label2.Size = new Size(371, 24);
            label2.TabIndex = 21;
            label2.Text = "Are you a New User or an Old User?";
            label2.Click += label2_Click;
            // 
            // btnNewUser
            // 
            btnNewUser.BackColor = Color.SteelBlue;
            btnNewUser.BorderColor = Color.Transparent;
            btnNewUser.BorderRadius = 20;
            btnNewUser.BorderSize = 0;
            btnNewUser.FlatAppearance.BorderSize = 0;
            btnNewUser.FlatStyle = FlatStyle.Flat;
            btnNewUser.Font = new Font("Dubai", 13.75F, FontStyle.Bold);
            btnNewUser.ForeColor = Color.White;
            btnNewUser.Location = new Point(146, 264);
            btnNewUser.Name = "btnNewUser";
            btnNewUser.Size = new Size(178, 46);
            btnNewUser.TabIndex = 22;
            btnNewUser.Text = "NEW USER";
            btnNewUser.UseVisualStyleBackColor = false;
            btnNewUser.Click += btnNewUser_Click;
            // 
            // btnOldUser
            // 
            btnOldUser.BackColor = Color.SteelBlue;
            btnOldUser.BorderColor = Color.Transparent;
            btnOldUser.BorderRadius = 20;
            btnOldUser.BorderSize = 0;
            btnOldUser.FlatAppearance.BorderSize = 0;
            btnOldUser.FlatStyle = FlatStyle.Flat;
            btnOldUser.Font = new Font("Dubai", 13.75F, FontStyle.Bold);
            btnOldUser.ForeColor = Color.White;
            btnOldUser.Location = new Point(360, 264);
            btnOldUser.Name = "btnOldUser";
            btnOldUser.Size = new Size(178, 46);
            btnOldUser.TabIndex = 23;
            btnOldUser.Text = "OLD USER";
            btnOldUser.UseVisualStyleBackColor = false;
            btnOldUser.Click += btnOldUser_Click;
            // 
            // chckDontShow
            // 
            chckDontShow.AutoSize = true;
            chckDontShow.Location = new Point(267, 341);
            chckDontShow.Name = "chckDontShow";
            chckDontShow.Size = new Size(154, 19);
            chckDontShow.TabIndex = 24;
            chckDontShow.Text = "Do not show this AGAIN";
            chckDontShow.UseVisualStyleBackColor = true;
            chckDontShow.CheckedChanged += chckDontShow_CheckedChanged;
            // 
            // WelcomeFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 450);
            Controls.Add(chckDontShow);
            Controls.Add(btnOldUser);
            Controls.Add(btnNewUser);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WelcomeFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WelcomeFrm";
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
        private Label label1;
        private Label label2;
        private RoundedButton btnNewUser;
        private RoundedButton btnOldUser;
        private CheckBox chckDontShow;
    }
}