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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentFrm));
            titleBar = new Panel();
            lblCashierName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnSave = new RoundedButton();
            customTextBox2 = new CustomControls.CustomTextBox();
            customTextBox1 = new CustomControls.CustomTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            customTextBox4 = new CustomControls.CustomTextBox();
            label6 = new Label();
            roundedButton1 = new RoundedButton();
            label7 = new Label();
            customComboBox1 = new CustomControls.CustomComboBox();
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
            titleBar.Size = new Size(499, 48);
            titleBar.TabIndex = 19;
            // 
            // lblCashierName
            // 
            lblCashierName.AutoSize = true;
            lblCashierName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCashierName.ForeColor = Color.White;
            lblCashierName.Location = new Point(259, 12);
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
            closeButton.Location = new Point(451, 0);
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
            btnSave.Location = new Point(247, 447);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(183, 52);
            btnSave.TabIndex = 31;
            btnSave.Text = "CONFIRM PAYMENT";
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
            customTextBox2.Location = new Point(58, 114);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(372, 42);
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
            customTextBox1.InnerForeColor = Color.Black;
            customTextBox1.IsPasswordField = false;
            customTextBox1.Location = new Point(58, 319);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Black;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(372, 42);
            customTextBox1.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(62, 90);
            label1.Name = "label1";
            label1.Size = new Size(102, 21);
            label1.TabIndex = 35;
            label1.Text = "Total to Pay";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(62, 295);
            label2.Name = "label2";
            label2.Size = new Size(183, 21);
            label2.TabIndex = 36;
            label2.Text = "Customer Payment(₱)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(64, 376);
            label3.Name = "label3";
            label3.Size = new Size(75, 21);
            label3.TabIndex = 37;
            label3.Text = "Change";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(142, 159);
            label4.Name = "label4";
            label4.Size = new Size(22, 21);
            label4.TabIndex = 45;
            label4.Text = "%";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(64, 159);
            label5.Name = "label5";
            label5.Size = new Size(78, 21);
            label5.TabIndex = 44;
            label5.Text = "Discount";
            // 
            // customTextBox4
            // 
            customTextBox4.BorderColor = SystemColors.ButtonFace;
            customTextBox4.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox4.BorderRadius = 8;
            customTextBox4.BorderThickness = 2;
            customTextBox4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox4.ForeColor = SystemColors.GrayText;
            customTextBox4.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox4.InnerForeColor = Color.Black;
            customTextBox4.IsPasswordField = false;
            customTextBox4.Location = new Point(58, 183);
            customTextBox4.Name = "customTextBox4";
            customTextBox4.PasswordChar = '\0';
            customTextBox4.PlaceholderColor = Color.Black;
            customTextBox4.PlaceholderText = "";
            customTextBox4.Size = new Size(372, 42);
            customTextBox4.TabIndex = 43;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(374, 376);
            label6.Name = "label6";
            label6.Size = new Size(56, 21);
            label6.TabIndex = 46;
            label6.Text = "₱ 0.00";
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = SystemColors.ControlDark;
            roundedButton1.BorderColor = Color.Transparent;
            roundedButton1.BorderRadius = 10;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(58, 447);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(183, 52);
            roundedButton1.TabIndex = 47;
            roundedButton1.Text = "CLEAR";
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(64, 228);
            label7.Name = "label7";
            label7.Size = new Size(147, 21);
            label7.TabIndex = 48;
            label7.Text = "Payment Method";
            // 
            // customComboBox1
            // 
            customComboBox1.ArrowColor = Color.FromArgb(100, 180, 255);
            customComboBox1.BorderColor = Color.Transparent;
            customComboBox1.BorderFocusColor = Color.FromArgb(60, 140, 255);
            customComboBox1.BorderRadius = 8;
            customComboBox1.BorderThickness = 2;
            customComboBox1.Font = new Font("Segoe UI", 10F);
            customComboBox1.InnerBackColor = SystemColors.InactiveCaption;
            customComboBox1.InnerForeColor = Color.White;
            customComboBox1.Location = new Point(64, 252);
            customComboBox1.Name = "customComboBox1";
            customComboBox1.PlaceholderColor = Color.FromArgb(120, 150, 200);
            customComboBox1.PlaceholderText = "";
            customComboBox1.SelectedIndex = -1;
            customComboBox1.SelectedItem = null;
            customComboBox1.Size = new Size(366, 36);
            customComboBox1.TabIndex = 50;
            // 
            // PaymentFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 550);
            Controls.Add(customComboBox1);
            Controls.Add(label7);
            Controls.Add(roundedButton1);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(customTextBox4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(customTextBox1);
            Controls.Add(btnSave);
            Controls.Add(customTextBox2);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CustomControls.CustomTextBox customTextBox4;
        private Label label6;
        private RoundedButton roundedButton1;
        private Label label7;
        private CustomControls.CustomComboBox customComboBox1;
    }
}