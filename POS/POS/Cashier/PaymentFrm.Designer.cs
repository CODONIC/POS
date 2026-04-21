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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentFrm));
            titleBar = new Panel();
            lblCashierName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnConfirmPayment = new RoundedButton();
            txtTotalToPay = new CustomControls.CustomTextBox();
            txtCustomerPayment = new CustomControls.CustomTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtDiscountPercent = new CustomControls.CustomTextBox();
            txtChange = new Label();
            btnClear = new RoundedButton();
            label7 = new Label();
            guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            txtTransactionNo = new CustomControls.CustomTextBox();
            label6 = new Label();
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
            // btnConfirmPayment
            // 
            btnConfirmPayment.BackColor = Color.SteelBlue;
            btnConfirmPayment.BorderColor = Color.Transparent;
            btnConfirmPayment.BorderRadius = 10;
            btnConfirmPayment.BorderSize = 0;
            btnConfirmPayment.FlatAppearance.BorderSize = 0;
            btnConfirmPayment.FlatStyle = FlatStyle.Flat;
            btnConfirmPayment.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirmPayment.ForeColor = Color.White;
            btnConfirmPayment.Location = new Point(245, 558);
            btnConfirmPayment.Name = "btnConfirmPayment";
            btnConfirmPayment.Size = new Size(183, 52);
            btnConfirmPayment.TabIndex = 31;
            btnConfirmPayment.Text = "CONFIRM PAYMENT";
            btnConfirmPayment.UseVisualStyleBackColor = false;
            btnConfirmPayment.Click += btnConfirmPayment_Click;
            // 
            // txtTotalToPay
            // 
            txtTotalToPay.BorderColor = SystemColors.ButtonFace;
            txtTotalToPay.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtTotalToPay.BorderRadius = 8;
            txtTotalToPay.BorderThickness = 2;
            txtTotalToPay.Enabled = false;
            txtTotalToPay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTotalToPay.ForeColor = SystemColors.GrayText;
            txtTotalToPay.InnerBackColor = SystemColors.InactiveCaption;
            txtTotalToPay.InnerForeColor = Color.Gray;
            txtTotalToPay.IsPasswordField = false;
            txtTotalToPay.Location = new Point(56, 225);
            txtTotalToPay.Name = "txtTotalToPay";
            txtTotalToPay.PasswordChar = '\0';
            txtTotalToPay.PlaceholderColor = Color.Gray;
            txtTotalToPay.PlaceholderText = "";
            txtTotalToPay.Size = new Size(372, 42);
            txtTotalToPay.TabIndex = 32;
            // 
            // txtCustomerPayment
            // 
            txtCustomerPayment.BorderColor = SystemColors.ButtonFace;
            txtCustomerPayment.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtCustomerPayment.BorderRadius = 8;
            txtCustomerPayment.BorderThickness = 2;
            txtCustomerPayment.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCustomerPayment.ForeColor = SystemColors.GrayText;
            txtCustomerPayment.InnerBackColor = SystemColors.InactiveCaption;
            txtCustomerPayment.InnerForeColor = Color.Black;
            txtCustomerPayment.IsPasswordField = false;
            txtCustomerPayment.Location = new Point(56, 430);
            txtCustomerPayment.Name = "txtCustomerPayment";
            txtCustomerPayment.PasswordChar = '\0';
            txtCustomerPayment.PlaceholderColor = Color.Black;
            txtCustomerPayment.PlaceholderText = "";
            txtCustomerPayment.Size = new Size(372, 42);
            txtCustomerPayment.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(60, 201);
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
            label2.Location = new Point(60, 406);
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
            label3.Location = new Point(62, 487);
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
            label4.Location = new Point(140, 270);
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
            label5.Location = new Point(62, 270);
            label5.Name = "label5";
            label5.Size = new Size(78, 21);
            label5.TabIndex = 44;
            label5.Text = "Discount";
            // 
            // txtDiscountPercent
            // 
            txtDiscountPercent.BorderColor = SystemColors.ButtonFace;
            txtDiscountPercent.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtDiscountPercent.BorderRadius = 8;
            txtDiscountPercent.BorderThickness = 2;
            txtDiscountPercent.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDiscountPercent.ForeColor = SystemColors.GrayText;
            txtDiscountPercent.InnerBackColor = SystemColors.InactiveCaption;
            txtDiscountPercent.InnerForeColor = Color.Black;
            txtDiscountPercent.IsPasswordField = false;
            txtDiscountPercent.Location = new Point(56, 294);
            txtDiscountPercent.Name = "txtDiscountPercent";
            txtDiscountPercent.PasswordChar = '\0';
            txtDiscountPercent.PlaceholderColor = Color.Black;
            txtDiscountPercent.PlaceholderText = "";
            txtDiscountPercent.Size = new Size(372, 42);
            txtDiscountPercent.TabIndex = 43;
            // 
            // txtChange
            // 
            txtChange.AutoSize = true;
            txtChange.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtChange.ForeColor = Color.Black;
            txtChange.Location = new Point(372, 487);
            txtChange.Name = "txtChange";
            txtChange.Size = new Size(56, 21);
            txtChange.TabIndex = 46;
            txtChange.Text = "₱ 0.00";
            // 
            // btnClear
            // 
            btnClear.BackColor = SystemColors.ControlDark;
            btnClear.BorderColor = Color.Transparent;
            btnClear.BorderRadius = 10;
            btnClear.BorderSize = 0;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(56, 558);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(183, 52);
            btnClear.TabIndex = 47;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(62, 339);
            label7.Name = "label7";
            label7.Size = new Size(147, 21);
            label7.TabIndex = 48;
            label7.Text = "Payment Method";
            // 
            // guna2ComboBox1
            // 
            guna2ComboBox1.BackColor = Color.Transparent;
            guna2ComboBox1.BorderColor = Color.White;
            guna2ComboBox1.CustomizableEdges = customizableEdges1;
            guna2ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            guna2ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            guna2ComboBox1.FillColor = SystemColors.InactiveCaption;
            guna2ComboBox1.FocusedColor = Color.FromArgb(94, 148, 255);
            guna2ComboBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2ComboBox1.Font = new Font("Segoe UI", 10F);
            guna2ComboBox1.ForeColor = Color.FromArgb(68, 88, 112);
            guna2ComboBox1.ItemHeight = 30;
            guna2ComboBox1.Location = new Point(62, 367);
            guna2ComboBox1.Name = "guna2ComboBox1";
            guna2ComboBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2ComboBox1.Size = new Size(366, 36);
            guna2ComboBox1.TabIndex = 49;
            // 
            // txtTransactionNo
            // 
            txtTransactionNo.BorderColor = SystemColors.ButtonFace;
            txtTransactionNo.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtTransactionNo.BorderRadius = 8;
            txtTransactionNo.BorderThickness = 2;
            txtTransactionNo.Enabled = false;
            txtTransactionNo.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTransactionNo.ForeColor = SystemColors.GrayText;
            txtTransactionNo.InnerBackColor = SystemColors.InactiveCaption;
            txtTransactionNo.InnerForeColor = Color.Gray;
            txtTransactionNo.IsPasswordField = false;
            txtTransactionNo.Location = new Point(56, 133);
            txtTransactionNo.Name = "txtTransactionNo";
            txtTransactionNo.PasswordChar = '\0';
            txtTransactionNo.PlaceholderColor = Color.Gray;
            txtTransactionNo.PlaceholderText = "";
            txtTransactionNo.Size = new Size(372, 42);
            txtTransactionNo.TabIndex = 50;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(62, 109);
            label6.Name = "label6";
            label6.Size = new Size(117, 21);
            label6.TabIndex = 51;
            label6.Text = "Transaction #";
            // 
            // PaymentFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(499, 660);
            Controls.Add(label6);
            Controls.Add(txtTransactionNo);
            Controls.Add(guna2ComboBox1);
            Controls.Add(label7);
            Controls.Add(btnClear);
            Controls.Add(txtChange);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(txtDiscountPercent);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtCustomerPayment);
            Controls.Add(btnConfirmPayment);
            Controls.Add(txtTotalToPay);
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
        private RoundedButton btnConfirmPayment;
        private CustomControls.CustomTextBox txtTotalToPay;
        private CustomControls.CustomTextBox txtCustomerPayment;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CustomControls.CustomTextBox txtDiscountPercent;
        private Label txtChange;
        private RoundedButton btnClear;
        private Label label7;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox1;
        private CustomControls.CustomTextBox txtTransactionNo;
        private Label label6;
    }
}