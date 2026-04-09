namespace POS.Cashier
{
    partial class PaymentFrm
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
            lblCashierName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnSave = new RoundedButton();
            customTextBox2 = new CustomControls.CustomTextBox();
            customTextBox1 = new CustomControls.CustomTextBox();
            customTextBox3 = new CustomControls.CustomTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnDiscount = new RoundedButton();
            titleBar.SuspendLayout();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(lblCashierName);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(698, 48);
            titleBar.TabIndex = 19;
            // 
            // lblCashierName
            // 
            lblCashierName.AutoSize = true;
            lblCashierName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCashierName.ForeColor = Color.White;
            lblCashierName.Location = new Point(445, 12);
            lblCashierName.Name = "lblCashierName";
            lblCashierName.Size = new Size(186, 21);
            lblCashierName.TabIndex = 21;
            lblCashierName.Text = "cashierName |Cashier";
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
            closeButton.Location = new Point(649, 0);
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
            // btnSave
            // 
            btnSave.BackColor = Color.SteelBlue;
            btnSave.BorderColor = Color.Transparent;
            btnSave.BorderRadius = 10;
            btnSave.BorderSize = 0;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(228, 343);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(82, 34);
            btnSave.TabIndex = 31;
            btnSave.Text = "Save";
            btnSave.TextAlign = ContentAlignment.TopCenter;
            btnSave.UseVisualStyleBackColor = false;
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
            customTextBox2.Location = new Point(226, 157);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(242, 42);
            customTextBox2.TabIndex = 32;
            // 
            // customTextBox1
            // 
            customTextBox1.BorderColor = SystemColors.ButtonFace;
            customTextBox1.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox1.BorderRadius = 8;
            customTextBox1.BorderThickness = 2;
            customTextBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox1.ForeColor = SystemColors.GrayText;
            customTextBox1.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox1.InnerForeColor = Color.Gray;
            customTextBox1.IsPasswordField = false;
            customTextBox1.Location = new Point(228, 226);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(242, 42);
            customTextBox1.TabIndex = 33;
            // 
            // customTextBox3
            // 
            customTextBox3.BorderColor = SystemColors.ButtonFace;
            customTextBox3.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox3.BorderRadius = 8;
            customTextBox3.BorderThickness = 2;
            customTextBox3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox3.ForeColor = SystemColors.GrayText;
            customTextBox3.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox3.InnerForeColor = Color.Gray;
            customTextBox3.IsPasswordField = false;
            customTextBox3.Location = new Point(226, 295);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(242, 42);
            customTextBox3.TabIndex = 34;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(226, 119);
            label1.Name = "label1";
            label1.Size = new Size(186, 21);
            label1.TabIndex = 35;
            label1.Text = "Total Ammount to Pay";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(228, 202);
            label2.Name = "label2";
            label2.Size = new Size(209, 21);
            label2.TabIndex = 36;
            label2.Text = "Customer Payment(cash)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(228, 271);
            label3.Name = "label3";
            label3.Size = new Size(75, 21);
            label3.TabIndex = 37;
            label3.Text = "Change";
            // 
            // btnDiscount
            // 
            btnDiscount.BackColor = Color.MediumSlateBlue;
            btnDiscount.BorderColor = Color.Transparent;
            btnDiscount.BorderRadius = 20;
            btnDiscount.BorderSize = 0;
            btnDiscount.FlatAppearance.BorderSize = 0;
            btnDiscount.FlatStyle = FlatStyle.Flat;
            btnDiscount.ForeColor = Color.White;
            btnDiscount.Location = new Point(367, 342);
            btnDiscount.Name = "btnDiscount";
            btnDiscount.Size = new Size(101, 40);
            btnDiscount.TabIndex = 38;
            btnDiscount.Text = "ADD DISCOUNT";
            btnDiscount.UseVisualStyleBackColor = false;
            btnDiscount.Click += btnDiscount_Click;
            // 
            // PaymentFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 450);
            Controls.Add(btnDiscount);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(customTextBox3);
            Controls.Add(customTextBox1);
            Controls.Add(btnSave);
            Controls.Add(customTextBox2);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PaymentFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PaymentFrm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblCashierName;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnSave;
        private CustomControls.CustomTextBox customTextBox2;
        private CustomControls.CustomTextBox customTextBox1;
        private CustomControls.CustomTextBox customTextBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private RoundedButton btnDiscount;
    }
}