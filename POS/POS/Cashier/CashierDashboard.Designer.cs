namespace POS
{
    partial class CashierDashboard
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
            components = new System.ComponentModel.Container();
            titleBar = new Panel();
            btnLogOut = new RoundedButton();
            closeButton = new Button();
            lblCashierName = new Label();
            titleLabel = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            panel1 = new Panel();
            customTextBox3 = new CustomControls.CustomTextBox();
            customTextBox2 = new CustomControls.CustomTextBox();
            customTextBox1 = new CustomControls.CustomTextBox();
            txtprodCode = new CustomControls.CustomTextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label7 = new Label();
            panel2 = new Panel();
            label8 = new Label();
            usersBindingSource = new BindingSource(components);
            panel3 = new Panel();
            customTextBox7 = new CustomControls.CustomTextBox();
            customTextBox6 = new CustomControls.CustomTextBox();
            customTextBox5 = new CustomControls.CustomTextBox();
            customTextBox4 = new CustomControls.CustomTextBox();
            roundedButton4 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            roundedButton2 = new RoundedButton();
            roundedButton1 = new RoundedButton();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            dgvProducts = new DataGridView();
            titleBar.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)usersBindingSource).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(btnLogOut);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(lblCashierName);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(1280, 43);
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
            btnLogOut.Location = new Point(877, 3);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(82, 34);
            btnLogOut.TabIndex = 21;
            btnLogOut.Text = "Logout";
            btnLogOut.TextAlign = ContentAlignment.TopCenter;
            btnLogOut.UseVisualStyleBackColor = false;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BackColor = Color.FromArgb(44, 62, 80);
            closeButton.BackgroundImageLayout = ImageLayout.None;
            closeButton.Cursor = Cursors.Hand;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(1239, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(41, 43);
            closeButton.TabIndex = 17;
            closeButton.Text = " X";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // lblCashierName
            // 
            lblCashierName.AutoSize = true;
            lblCashierName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCashierName.ForeColor = Color.White;
            lblCashierName.Location = new Point(1003, 10);
            lblCashierName.Name = "lblCashierName";
            lblCashierName.Size = new Size(212, 21);
            lblCashierName.TabIndex = 20;
            lblCashierName.Text = "employeeName | Cashier";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(12, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(100, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "POS System";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(customTextBox3);
            panel1.Controls.Add(customTextBox2);
            panel1.Controls.Add(customTextBox1);
            panel1.Controls.Add(txtprodCode);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(44, 73);
            panel1.Name = "panel1";
            panel1.Size = new Size(843, 162);
            panel1.TabIndex = 18;
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
            customTextBox3.Location = new Point(133, 88);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(194, 39);
            customTextBox3.TabIndex = 31;
            // 
            // customTextBox2
            // 
            customTextBox2.BorderColor = SystemColors.ButtonFace;
            customTextBox2.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox2.BorderRadius = 8;
            customTextBox2.BorderThickness = 2;
            customTextBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox2.ForeColor = SystemColors.GrayText;
            customTextBox2.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox2.InnerForeColor = Color.Gray;
            customTextBox2.IsPasswordField = false;
            customTextBox2.Location = new Point(479, 87);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(95, 39);
            customTextBox2.TabIndex = 30;
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
            customTextBox1.Location = new Point(479, 34);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(95, 39);
            customTextBox1.TabIndex = 29;
            // 
            // txtprodCode
            // 
            txtprodCode.BorderColor = SystemColors.ButtonFace;
            txtprodCode.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtprodCode.BorderRadius = 8;
            txtprodCode.BorderThickness = 2;
            txtprodCode.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtprodCode.ForeColor = SystemColors.GrayText;
            txtprodCode.InnerBackColor = SystemColors.InactiveCaption;
            txtprodCode.InnerForeColor = Color.Gray;
            txtprodCode.IsPasswordField = false;
            txtprodCode.Location = new Point(133, 34);
            txtprodCode.Name = "txtprodCode";
            txtprodCode.PasswordChar = '\0';
            txtprodCode.PlaceholderColor = Color.Gray;
            txtprodCode.PlaceholderText = "";
            txtprodCode.Size = new Size(194, 39);
            txtprodCode.TabIndex = 28;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.Control;
            label6.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(13, 97);
            label6.Name = "label6";
            label6.Size = new Size(117, 18);
            label6.TabIndex = 25;
            label6.Text = "Product Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(399, 97);
            label5.Name = "label5";
            label5.Size = new Size(49, 18);
            label5.TabIndex = 24;
            label5.Text = "Price:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(399, 44);
            label4.Name = "label4";
            label4.Size = new Size(74, 18);
            label4.TabIndex = 23;
            label4.Text = "Quantity:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(13, 44);
            label3.Name = "label3";
            label3.Size = new Size(114, 18);
            label3.TabIndex = 22;
            label3.Text = "Product Code:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(44, 63);
            label2.Name = "label2";
            label2.Size = new Size(154, 19);
            label2.TabIndex = 21;
            label2.Text = "ITEM INFORMATION";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.Control;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(117, 66);
            label7.Name = "label7";
            label7.Size = new Size(40, 19);
            label7.TabIndex = 22;
            label7.Text = "0.00";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(label7);
            panel2.Location = new Point(953, 73);
            panel2.Name = "panel2";
            panel2.Size = new Size(274, 162);
            panel2.TabIndex = 26;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.LightSteelBlue;
            label8.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(953, 63);
            label8.Name = "label8";
            label8.Size = new Size(101, 19);
            label8.TabIndex = 27;
            label8.Text = "TOTAL PRICE";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlLight;
            panel3.Controls.Add(customTextBox7);
            panel3.Controls.Add(customTextBox6);
            panel3.Controls.Add(customTextBox5);
            panel3.Controls.Add(customTextBox4);
            panel3.Controls.Add(roundedButton4);
            panel3.Controls.Add(roundedButton3);
            panel3.Controls.Add(roundedButton2);
            panel3.Controls.Add(roundedButton1);
            panel3.Controls.Add(label12);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(label9);
            panel3.Location = new Point(953, 263);
            panel3.Name = "panel3";
            panel3.Size = new Size(274, 493);
            panel3.TabIndex = 27;
            // 
            // customTextBox7
            // 
            customTextBox7.BorderColor = SystemColors.ButtonFace;
            customTextBox7.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox7.BorderRadius = 8;
            customTextBox7.BorderThickness = 1;
            customTextBox7.Enabled = false;
            customTextBox7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox7.ForeColor = SystemColors.GrayText;
            customTextBox7.InnerBackColor = SystemColors.ScrollBar;
            customTextBox7.InnerForeColor = Color.Gray;
            customTextBox7.IsPasswordField = false;
            customTextBox7.Location = new Point(108, 9);
            customTextBox7.Name = "customTextBox7";
            customTextBox7.PasswordChar = '\0';
            customTextBox7.PlaceholderColor = Color.Gray;
            customTextBox7.PlaceholderText = "";
            customTextBox7.Size = new Size(133, 30);
            customTextBox7.TabIndex = 35;
            customTextBox7.Text = "0.00";
            // 
            // customTextBox6
            // 
            customTextBox6.BorderColor = SystemColors.ButtonFace;
            customTextBox6.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox6.BorderRadius = 8;
            customTextBox6.BorderThickness = 1;
            customTextBox6.Enabled = false;
            customTextBox6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox6.ForeColor = SystemColors.GrayText;
            customTextBox6.InnerBackColor = SystemColors.ScrollBar;
            customTextBox6.InnerForeColor = Color.Gray;
            customTextBox6.IsPasswordField = false;
            customTextBox6.Location = new Point(108, 45);
            customTextBox6.Name = "customTextBox6";
            customTextBox6.PasswordChar = '\0';
            customTextBox6.PlaceholderColor = Color.Gray;
            customTextBox6.PlaceholderText = "";
            customTextBox6.Size = new Size(133, 30);
            customTextBox6.TabIndex = 34;
            customTextBox6.Text = "0.00";
            // 
            // customTextBox5
            // 
            customTextBox5.BorderColor = SystemColors.ButtonFace;
            customTextBox5.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox5.BorderRadius = 8;
            customTextBox5.BorderThickness = 1;
            customTextBox5.Enabled = false;
            customTextBox5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox5.ForeColor = SystemColors.GrayText;
            customTextBox5.InnerBackColor = SystemColors.ScrollBar;
            customTextBox5.InnerForeColor = Color.Gray;
            customTextBox5.IsPasswordField = false;
            customTextBox5.Location = new Point(108, 80);
            customTextBox5.Name = "customTextBox5";
            customTextBox5.PasswordChar = '\0';
            customTextBox5.PlaceholderColor = Color.Gray;
            customTextBox5.PlaceholderText = "";
            customTextBox5.Size = new Size(133, 30);
            customTextBox5.TabIndex = 33;
            customTextBox5.Text = "0.00";
            // 
            // customTextBox4
            // 
            customTextBox4.BorderColor = SystemColors.ButtonFace;
            customTextBox4.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox4.BorderRadius = 8;
            customTextBox4.BorderThickness = 1;
            customTextBox4.Enabled = false;
            customTextBox4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox4.ForeColor = SystemColors.GrayText;
            customTextBox4.InnerBackColor = SystemColors.ScrollBar;
            customTextBox4.InnerForeColor = Color.Gray;
            customTextBox4.IsPasswordField = false;
            customTextBox4.Location = new Point(12, 167);
            customTextBox4.Name = "customTextBox4";
            customTextBox4.PasswordChar = '\0';
            customTextBox4.PlaceholderColor = Color.Gray;
            customTextBox4.PlaceholderText = "";
            customTextBox4.Size = new Size(250, 39);
            customTextBox4.TabIndex = 32;
            customTextBox4.Text = "12345678";
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.MediumSlateBlue;
            roundedButton4.BorderColor = Color.Transparent;
            roundedButton4.BorderRadius = 20;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(59, 401);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(150, 40);
            roundedButton4.TabIndex = 29;
            roundedButton4.Text = "CLEAR CART";
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.MediumSlateBlue;
            roundedButton3.BorderColor = Color.Transparent;
            roundedButton3.BorderRadius = 20;
            roundedButton3.BorderSize = 0;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(59, 355);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(150, 40);
            roundedButton3.TabIndex = 28;
            roundedButton3.Text = "PAYMENT";
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.MediumSlateBlue;
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 20;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(59, 309);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(150, 40);
            roundedButton2.TabIndex = 27;
            roundedButton2.Text = "ADD DISCOUNT";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.MediumSlateBlue;
            roundedButton1.BorderColor = Color.Transparent;
            roundedButton1.BorderRadius = 20;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(59, 263);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(150, 40);
            roundedButton1.TabIndex = 26;
            roundedButton1.Text = "NEW TRANSACTION";
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = SystemColors.Control;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(12, 133);
            label12.Name = "label12";
            label12.Size = new Size(160, 19);
            label12.TabIndex = 25;
            label12.Text = "TRANSACTION NO#:";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.Control;
            label11.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.Black;
            label11.Location = new Point(12, 91);
            label11.Name = "label11";
            label11.Size = new Size(79, 19);
            label11.TabIndex = 24;
            label11.Text = "VATABLE:";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.Control;
            label10.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(12, 56);
            label10.Name = "label10";
            label10.Size = new Size(43, 19);
            label10.TabIndex = 23;
            label10.Text = "VAT:";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.Control;
            label9.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(12, 19);
            label9.Name = "label9";
            label9.Size = new Size(90, 19);
            label9.TabIndex = 22;
            label9.Text = "DISCOUNT:";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvProducts
            // 
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(44, 296);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Size = new Size(843, 460);
            dgvProducts.TabIndex = 28;
            // 
            // CashierDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 768);
            Controls.Add(dgvProducts);
            Controls.Add(panel3);
            Controls.Add(label8);
            Controls.Add(panel2);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(titleBar);
            Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CashierDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += CashierDashboard_Load;
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)usersBindingSource).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Button closeButton;
        private Label titleLabel;
        private ContextMenuStrip contextMenuStrip1;
        private Label lblCashierName;
        private Panel panel1;
        private Label label2;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label7;
        private Panel panel2;
        private Label label8;
        private CustomControls.CustomTextBox customTextBox3;
        private CustomControls.CustomTextBox customTextBox2;
        private CustomControls.CustomTextBox customTextBox1;
        private CustomControls.CustomTextBox txtprodCode;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn baseUrlDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn requestClientOptionsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tableNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn primaryKeyDataGridViewTextBoxColumn;
        private BindingSource usersBindingSource;
        private Panel panel3;
        private Label label9;
        private Label label12;
        private Label label11;
        private Label label10;
        private CustomControls.CustomTextBox customTextBox4;
        private RoundedButton roundedButton4;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton1;
        private CustomControls.CustomTextBox customTextBox7;
        private CustomControls.CustomTextBox customTextBox6;
        private CustomControls.CustomTextBox customTextBox5;
        private RoundedButton btnLogOut;
        private DataGridView dgvProducts;
    }
}