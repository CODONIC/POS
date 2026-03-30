namespace POS.Admin
{
    partial class ProdCategoryFrm
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
            btnBack = new RoundedButton();
            dataGridView1 = new DataGridView();
            customTextBox3 = new CustomControls.CustomTextBox();
            label12 = new Label();
            label2 = new Label();
            customTextBox1 = new CustomControls.CustomTextBox();
            roundedButton1 = new RoundedButton();
            roundedButton2 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            roundedButton4 = new RoundedButton();
            titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            titleBar.TabIndex = 18;
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
            closeButton.Location = new Point(1239, 5);
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
            // btnBack
            // 
            btnBack.BackColor = Color.SteelBlue;
            btnBack.BorderColor = Color.Transparent;
            btnBack.BorderRadius = 20;
            btnBack.BorderSize = 0;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(12, 663);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(91, 45);
            btnBack.TabIndex = 36;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(158, 172);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(955, 254);
            dataGridView1.TabIndex = 37;
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
            customTextBox3.Location = new Point(158, 104);
            customTextBox3.Name = "customTextBox3";
            customTextBox3.PasswordChar = '\0';
            customTextBox3.PlaceholderColor = Color.Gray;
            customTextBox3.PlaceholderText = "";
            customTextBox3.Size = new Size(955, 47);
            customTextBox3.TabIndex = 38;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(158, 82);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 45;
            label12.Text = "SEARCH";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(158, 438);
            label2.Name = "label2";
            label2.Size = new Size(134, 19);
            label2.TabIndex = 46;
            label2.Text = "CATEGORY INFO";
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
            customTextBox1.Location = new Point(158, 479);
            customTextBox1.Name = "customTextBox1";
            customTextBox1.PasswordChar = '\0';
            customTextBox1.PlaceholderColor = Color.Gray;
            customTextBox1.PlaceholderText = "";
            customTextBox1.Size = new Size(955, 104);
            customTextBox1.TabIndex = 47;
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
            roundedButton1.Location = new Point(366, 626);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(91, 45);
            roundedButton1.TabIndex = 48;
            roundedButton1.Text = "SAVE";
            roundedButton1.UseVisualStyleBackColor = false;
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
            roundedButton2.Location = new Point(489, 626);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(91, 45);
            roundedButton2.TabIndex = 49;
            roundedButton2.Text = "EDIT";
            roundedButton2.UseVisualStyleBackColor = false;
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
            roundedButton3.Location = new Point(647, 626);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(91, 45);
            roundedButton3.TabIndex = 50;
            roundedButton3.Text = "DELETE";
            roundedButton3.UseVisualStyleBackColor = false;
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
            roundedButton4.Location = new Point(773, 626);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(91, 45);
            roundedButton4.TabIndex = 51;
            roundedButton4.Text = "CLEAR";
            roundedButton4.UseVisualStyleBackColor = false;
            // 
            // ProdCategoryFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(roundedButton4);
            Controls.Add(roundedButton3);
            Controls.Add(roundedButton2);
            Controls.Add(roundedButton1);
            Controls.Add(customTextBox1);
            Controls.Add(label2);
            Controls.Add(label12);
            Controls.Add(customTextBox3);
            Controls.Add(dataGridView1);
            Controls.Add(btnBack);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProdCategoryFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ProdCategoryFrm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label label1;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnBack;
        private DataGridView dataGridView1;
        private CustomControls.CustomTextBox customTextBox3;
        private Label label12;
        private Label label2;
        private CustomControls.CustomTextBox customTextBox1;
        private RoundedButton roundedButton1;
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton4;
    }
}