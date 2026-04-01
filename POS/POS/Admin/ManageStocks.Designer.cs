namespace POS.Admin
{
    partial class ManageStocks
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
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnBack = new RoundedButton();
            roundedButton1 = new RoundedButton();
            label2 = new Label();
            panel1 = new Panel();
            roundedButton3 = new RoundedButton();
            roundedButton2 = new RoundedButton();
            customTextBox6 = new CustomControls.CustomTextBox();
            label8 = new Label();
            customTextBox4 = new CustomControls.CustomTextBox();
            label7 = new Label();
            customTextBox3 = new CustomControls.CustomTextBox();
            label3 = new Label();
            customTextBox5 = new CustomControls.CustomTextBox();
            label4 = new Label();
            customTextBox2 = new CustomControls.CustomTextBox();
            label5 = new Label();
            customTextBox1 = new CustomControls.CustomTextBox();
            label6 = new Label();
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            label12 = new Label();
            customTextBox8 = new CustomControls.CustomTextBox();
            titleBar.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
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
            titleBar.Size = new Size(1404, 38);
            titleBar.TabIndex = 19;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1178, 9);
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
            closeButton.Location = new Point(1363, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(41, 35);
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
            // btnBack
            // 
            btnBack.BackColor = Color.SteelBlue;
            btnBack.BorderColor = Color.Transparent;
            btnBack.BorderRadius = 10;
            btnBack.BorderSize = 0;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(23, 661);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(68, 34);
            btnBack.TabIndex = 37;
            btnBack.Text = "BACK";
            btnBack.TextAlign = ContentAlignment.TopCenter;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.SteelBlue;
            roundedButton1.BorderColor = Color.Transparent;
            roundedButton1.BorderRadius = 20;
            roundedButton1.BorderSize = 0;
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(25, 294);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(137, 40);
            roundedButton1.TabIndex = 38;
            roundedButton1.Text = "ADD STOCK";
            roundedButton1.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(23, 140);
            label2.Name = "label2";
            label2.Size = new Size(171, 19);
            label2.TabIndex = 40;
            label2.Text = "STOCK INFORMATION";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(roundedButton3);
            panel1.Controls.Add(roundedButton2);
            panel1.Controls.Add(customTextBox6);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(customTextBox4);
            panel1.Controls.Add(roundedButton1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(customTextBox3);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(customTextBox5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(customTextBox2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(customTextBox1);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(23, 153);
            panel1.Name = "panel1";
            panel1.Size = new Size(534, 352);
            panel1.TabIndex = 39;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.SteelBlue;
            roundedButton3.BorderColor = Color.Transparent;
            roundedButton3.BorderRadius = 20;
            roundedButton3.BorderSize = 0;
            roundedButton3.FlatAppearance.BorderSize = 0;
            roundedButton3.FlatStyle = FlatStyle.Flat;
            roundedButton3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(379, 294);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(137, 40);
            roundedButton3.TabIndex = 67;
            roundedButton3.Text = "CANCEL";
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.SteelBlue;
            roundedButton2.BorderColor = Color.Transparent;
            roundedButton2.BorderRadius = 20;
            roundedButton2.BorderSize = 0;
            roundedButton2.FlatAppearance.BorderSize = 0;
            roundedButton2.FlatStyle = FlatStyle.Flat;
            roundedButton2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(206, 294);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(137, 40);
            roundedButton2.TabIndex = 66;
            roundedButton2.Text = "SAVE";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // customTextBox6
            // 
            customTextBox6.BorderColor = SystemColors.ButtonFace;
            customTextBox6.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox6.BorderRadius = 8;
            customTextBox6.BorderThickness = 2;
            customTextBox6.Enabled = false;
            customTextBox6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox6.ForeColor = SystemColors.GrayText;
            customTextBox6.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox6.InnerForeColor = Color.Gray;
            customTextBox6.IsPasswordField = false;
            customTextBox6.Location = new Point(206, 234);
            customTextBox6.Name = "customTextBox6";
            customTextBox6.PasswordChar = '\0';
            customTextBox6.PlaceholderColor = Color.Gray;
            customTextBox6.PlaceholderText = "";
            customTextBox6.Size = new Size(310, 32);
            customTextBox6.TabIndex = 65;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(25, 245);
            label8.Name = "label8";
            label8.Size = new Size(119, 21);
            label8.TabIndex = 64;
            label8.Text = "Add Quantity";
            // 
            // customTextBox4
            // 
            customTextBox4.BorderColor = SystemColors.ButtonFace;
            customTextBox4.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox4.BorderRadius = 8;
            customTextBox4.BorderThickness = 2;
            customTextBox4.Enabled = false;
            customTextBox4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox4.ForeColor = SystemColors.GrayText;
            customTextBox4.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox4.InnerForeColor = Color.Gray;
            customTextBox4.IsPasswordField = false;
            customTextBox4.Location = new Point(206, 178);
            customTextBox4.Name = "customTextBox4";
            customTextBox4.PasswordChar = '\0';
            customTextBox4.PlaceholderColor = Color.Gray;
            customTextBox4.PlaceholderText = "";
            customTextBox4.Size = new Size(310, 32);
            customTextBox4.TabIndex = 63;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(25, 178);
            label7.Name = "label7";
            label7.Size = new Size(118, 21);
            label7.TabIndex = 62;
            label7.Text = "Stock in Date:";
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
            customTextBox3.Location = new Point(206, 99);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(310, 32);
            customTextBox3.TabIndex = 61;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(22, 39);
            label3.Name = "label3";
            label3.Size = new Size(120, 21);
            label3.TabIndex = 54;
            label3.Text = "Product Code";
            // 
            // customTextBox5
            // 
            customTextBox5.BorderColor = SystemColors.ButtonFace;
            customTextBox5.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox5.BorderRadius = 8;
            customTextBox5.BorderThickness = 2;
            customTextBox5.Enabled = false;
            customTextBox5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox5.ForeColor = SystemColors.GrayText;
            customTextBox5.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox5.InnerForeColor = Color.Gray;
            customTextBox5.IsPasswordField = false;
            customTextBox5.Location = new Point(206, 137);
            customTextBox5.Name = "customTextBox5";
            customTextBox5.PasswordChar = '\0';
            customTextBox5.PlaceholderColor = Color.Gray;
            customTextBox5.PlaceholderText = "";
            customTextBox5.Size = new Size(310, 32);
            customTextBox5.TabIndex = 60;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(22, 72);
            label4.Name = "label4";
            label4.Size = new Size(163, 21);
            label4.TabIndex = 55;
            label4.Text = "Product Description";
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
            customTextBox2.Location = new Point(206, 61);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(310, 32);
            customTextBox2.TabIndex = 59;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(22, 106);
            label5.Name = "label5";
            label5.Size = new Size(85, 21);
            label5.TabIndex = 56;
            label5.Text = "Category";
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
            customTextBox1.Location = new Point(206, 23);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(310, 32);
            customTextBox1.TabIndex = 58;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(22, 142);
            label6.Name = "label6";
            label6.Size = new Size(82, 21);
            label6.TabIndex = 57;
            label6.Text = "Unit Price";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(576, 153);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(382, 352);
            dataGridView1.TabIndex = 41;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(991, 153);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(387, 352);
            dataGridView2.TabIndex = 42;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(576, 63);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 46;
            label12.Text = "SEARCH";
            // 
            // customTextBox8
            // 
            customTextBox8.BorderColor = SystemColors.ButtonFace;
            customTextBox8.BorderFocusColor = Color.FromArgb(30, 45, 61);
            customTextBox8.BorderRadius = 8;
            customTextBox8.BorderThickness = 2;
            customTextBox8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customTextBox8.ForeColor = SystemColors.GrayText;
            customTextBox8.InnerBackColor = SystemColors.InactiveCaption;
            customTextBox8.InnerForeColor = Color.Gray;
            customTextBox8.IsPasswordField = false;
            customTextBox8.Location = new Point(576, 85);
            customTextBox8.Name = "customTextBox8";
            customTextBox8.PasswordChar = '\0';
            customTextBox8.PlaceholderColor = Color.Gray;
            customTextBox8.PlaceholderText = "";
            customTextBox8.Size = new Size(802, 39);
            customTextBox8.TabIndex = 45;
            // 
            // ManageStocks
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 720);
            Controls.Add(label12);
            Controls.Add(customTextBox8);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(btnBack);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ManageStocks";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageStocks";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblAdminName;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnBack;
        private RoundedButton roundedButton1;
        private Label label2;
        private Panel panel1;
        private CustomControls.CustomTextBox customTextBox4;
        private Label label7;
        private CustomControls.CustomTextBox customTextBox3;
        private Label label3;
        private CustomControls.CustomTextBox customTextBox5;
        private Label label4;
        private CustomControls.CustomTextBox customTextBox2;
        private Label label5;
        private CustomControls.CustomTextBox customTextBox1;
        private Label label6;
        private CustomControls.CustomTextBox customTextBox6;
        private Label label8;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton2;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label12;
        private CustomControls.CustomTextBox customTextBox8;
    }
}