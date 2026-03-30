namespace POS.Admin
{
    partial class ManageProdFrm
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
            label1 = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            label12 = new Label();
            customTextBox3 = new CustomControls.CustomTextBox();
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            comboBox1 = new ComboBox();
            customTextBox6 = new CustomControls.CustomTextBox();
            customTextBox5 = new CustomControls.CustomTextBox();
            customTextBox2 = new CustomControls.CustomTextBox();
            customTextBox1 = new CustomControls.CustomTextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            roundedButton4 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            roundedButton2 = new RoundedButton();
            roundedButton1 = new RoundedButton();
            btnBack = new RoundedButton();
            titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(label1);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(1280, 48);
            titleBar.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(1005, 10);
            label1.Name = "label1";
            label1.Size = new Size(179, 21);
            label1.TabIndex = 21;
            label1.Text = "adminName | Admin";
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
            closeButton.Location = new Point(1239, 3);
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
            titleLabel.Location = new Point(12, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(104, 21);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(170, 75);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 48;
            label12.Text = "SEARCH";
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
            customTextBox3.Location = new Point(170, 97);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(955, 47);
            customTextBox3.TabIndex = 47;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(170, 150);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(955, 254);
            dataGridView1.TabIndex = 46;
            // 
            // panel1
            // 
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(customTextBox6);
            panel1.Controls.Add(customTextBox5);
            panel1.Controls.Add(customTextBox2);
            panel1.Controls.Add(customTextBox1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(170, 425);
            panel1.Name = "panel1";
            panel1.Size = new Size(955, 206);
            panel1.TabIndex = 49;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(214, 90);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(387, 23);
            comboBox1.TabIndex = 53;
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
            customTextBox6.Location = new Point(214, 159);
            customTextBox6.Name = "customTextBox6";
            customTextBox6.PasswordChar = '\0';
            customTextBox6.PlaceholderColor = Color.Gray;
            customTextBox6.PlaceholderText = "";
            customTextBox6.Size = new Size(387, 32);
            customTextBox6.TabIndex = 52;
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
            customTextBox5.Location = new Point(214, 121);
            customTextBox5.Name = "customTextBox5";
            customTextBox5.PasswordChar = '\0';
            customTextBox5.PlaceholderColor = Color.Gray;
            customTextBox5.PlaceholderText = "";
            customTextBox5.Size = new Size(387, 32);
            customTextBox5.TabIndex = 51;
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
            customTextBox2.Location = new Point(214, 45);
            customTextBox2.Name = "customTextBox2";
            customTextBox2.PasswordChar = '\0';
            customTextBox2.PlaceholderColor = Color.Gray;
            customTextBox2.PlaceholderText = "";
            customTextBox2.Size = new Size(387, 32);
            customTextBox2.TabIndex = 49;
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
            customTextBox1.Location = new Point(214, 7);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(387, 32);
            customTextBox1.TabIndex = 48;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(30, 164);
            label7.Name = "label7";
            label7.Size = new Size(120, 21);
            label7.TabIndex = 28;
            label7.Text = "Re-order Level";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(30, 126);
            label6.Name = "label6";
            label6.Size = new Size(82, 21);
            label6.TabIndex = 27;
            label6.Text = "Unit Price";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(30, 90);
            label5.Name = "label5";
            label5.Size = new Size(85, 21);
            label5.TabIndex = 26;
            label5.Text = "Category";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(30, 56);
            label4.Name = "label4";
            label4.Size = new Size(163, 21);
            label4.TabIndex = 25;
            label4.Text = "Product Description";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(30, 23);
            label3.Name = "label3";
            label3.Size = new Size(120, 21);
            label3.TabIndex = 24;
            label3.Text = "Product Code";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(170, 407);
            label2.Name = "label2";
            label2.Size = new Size(121, 19);
            label2.TabIndex = 50;
            label2.Text = "PRODUCT INFO";
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.SteelBlue;
            roundedButton4.BorderColor = Color.Transparent;
            roundedButton4.BorderRadius = 20;
            roundedButton4.BorderSize = 0;
            roundedButton4.FlatAppearance.BorderSize = 0;
            roundedButton4.FlatStyle = FlatStyle.Flat;
            roundedButton4.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(779, 650);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(91, 45);
            roundedButton4.TabIndex = 55;
            roundedButton4.Text = "CLEAR";
            roundedButton4.UseVisualStyleBackColor = false;
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
            roundedButton3.Location = new Point(653, 650);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(91, 45);
            roundedButton3.TabIndex = 54;
            roundedButton3.Text = "DELETE";
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
            roundedButton2.Location = new Point(495, 650);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(91, 45);
            roundedButton2.TabIndex = 53;
            roundedButton2.Text = "EDIT";
            roundedButton2.UseVisualStyleBackColor = false;
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
            roundedButton1.Location = new Point(372, 650);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(91, 45);
            roundedButton1.TabIndex = 52;
            roundedButton1.Text = "SAVE";
            roundedButton1.UseVisualStyleBackColor = false;
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
            btnBack.Location = new Point(24, 661);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(68, 34);
            btnBack.TabIndex = 56;
            btnBack.Text = "BACK";
            btnBack.TextAlign = ContentAlignment.TopCenter;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // ManageProdFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(btnBack);
            Controls.Add(roundedButton4);
            Controls.Add(roundedButton3);
            Controls.Add(roundedButton2);
            Controls.Add(roundedButton1);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(label12);
            Controls.Add(customTextBox3);
            Controls.Add(dataGridView1);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ManageProdFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageProdFrm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label label1;
        private Button closeButton;
        private Label titleLabel;
        private Label label12;
        private CustomControls.CustomTextBox customTextBox3;
        private DataGridView dataGridView1;
        private Panel panel1;
        private Label label2;
        private ComboBox comboBox1;
        private CustomControls.CustomTextBox customTextBox6;
        private CustomControls.CustomTextBox customTextBox5;
        private CustomControls.CustomTextBox customTextBox2;
        private CustomControls.CustomTextBox customTextBox1;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private RoundedButton roundedButton4;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton1;
        private RoundedButton btnBack;
    }
}