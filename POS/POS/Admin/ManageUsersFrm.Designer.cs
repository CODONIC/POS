namespace POS.Admin
{
    partial class ManageUsersFrm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageUsersFrm));
            lblAdminName = new Label();
            titleLabel = new Label();
            titleBar = new Panel();
            closeButton = new Button();
            dgvUsers = new DataGridView();
            label2 = new Label();
            panel1 = new Panel();
            lblStrengthIndicator = new Label();
            label1 = new Label();
            dtpBirthdate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            cmbUserLevel = new Guna.UI2.WinForms.Guna2ComboBox();
            label11 = new Label();
            txtContact = new CustomControls.CustomTextBox();
            label10 = new Label();
            txtMiddleName = new CustomControls.CustomTextBox();
            txtFirstName = new CustomControls.CustomTextBox();
            txtLastName = new CustomControls.CustomTextBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            txtPassword = new CustomControls.CustomTextBox();
            txtAge = new CustomControls.CustomTextBox();
            txtUsername = new CustomControls.CustomTextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            btnAdd = new RoundedButton();
            btnUpdate = new RoundedButton();
            btnDelete = new RoundedButton();
            btnClear = new RoundedButton();
            txtSearch = new CustomControls.CustomTextBox();
            label12 = new Label();
            btnBack = new RoundedButton();
            titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1191, 12);
            lblAdminName.Name = "lblAdminName";
            lblAdminName.Size = new Size(221, 23);
            lblAdminName.TabIndex = 21;
            lblAdminName.Text = "adminName | Admin";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(14, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(129, 28);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Tindero POS";
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.FromArgb(44, 62, 80);
            titleBar.Controls.Add(lblAdminName);
            titleBar.Controls.Add(closeButton);
            titleBar.Controls.Add(titleLabel);
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Margin = new Padding(3, 4, 3, 4);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(1463, 51);
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
            closeButton.Location = new Point(1416, 3);
            closeButton.Margin = new Padding(3, 4, 3, 4);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(47, 47);
            closeButton.TabIndex = 17;
            closeButton.Text = " X";
            closeButton.UseVisualStyleBackColor = false;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AllowUserToOrderColumns = true;
            dgvUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 249, 250);
            dgvUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12.25F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(5, 0, 5, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.GridColor = Color.FromArgb(230, 230, 230);
            dgvUsers.Location = new Point(48, 169);
            dgvUsers.Margin = new Padding(3, 4, 3, 4);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.InactiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvUsers.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(1373, 251);
            dgvUsers.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.LightSteelBlue;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(48, 437);
            label2.Name = "label2";
            label2.Size = new Size(266, 23);
            label2.TabIndex = 23;
            label2.Text = "EMPLOYEE'S INFORMATION";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(lblStrengthIndicator);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dtpBirthdate);
            panel1.Controls.Add(cmbUserLevel);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(txtContact);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txtMiddleName);
            panel1.Controls.Add(txtFirstName);
            panel1.Controls.Add(txtLastName);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtAge);
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(48, 455);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1373, 399);
            panel1.TabIndex = 22;
            panel1.Paint += panel1_Paint;
            // 
            // lblStrengthIndicator
            // 
            lblStrengthIndicator.AutoSize = true;
            lblStrengthIndicator.Font = new Font("Century Gothic", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStrengthIndicator.Location = new Point(420, 159);
            lblStrengthIndicator.Name = "lblStrengthIndicator";
            lblStrengthIndicator.Size = new Size(0, 19);
            lblStrengthIndicator.TabIndex = 59;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(439, 115);
            label1.Name = "label1";
            label1.Size = new Size(0, 23);
            label1.TabIndex = 58;
            // 
            // dtpBirthdate
            // 
            dtpBirthdate.Checked = true;
            dtpBirthdate.CustomizableEdges = customizableEdges1;
            dtpBirthdate.FillColor = SystemColors.InactiveCaption;
            dtpBirthdate.Font = new Font("Segoe UI", 9F);
            dtpBirthdate.Format = DateTimePickerFormat.Long;
            dtpBirthdate.Location = new Point(989, 176);
            dtpBirthdate.Margin = new Padding(3, 4, 3, 4);
            dtpBirthdate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpBirthdate.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpBirthdate.Name = "dtpBirthdate";
            dtpBirthdate.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtpBirthdate.Size = new Size(282, 48);
            dtpBirthdate.TabIndex = 57;
            dtpBirthdate.Value = new DateTime(2026, 5, 12, 18, 57, 31, 485);
            // 
            // cmbUserLevel
            // 
            cmbUserLevel.BackColor = Color.Transparent;
            cmbUserLevel.BorderColor = Color.White;
            cmbUserLevel.CustomizableEdges = customizableEdges3;
            cmbUserLevel.DrawMode = DrawMode.OwnerDrawFixed;
            cmbUserLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserLevel.FillColor = SystemColors.InactiveCaption;
            cmbUserLevel.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbUserLevel.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbUserLevel.Font = new Font("Segoe UI", 10F);
            cmbUserLevel.ForeColor = Color.FromArgb(68, 88, 112);
            cmbUserLevel.ItemHeight = 30;
            cmbUserLevel.Location = new Point(989, 248);
            cmbUserLevel.Margin = new Padding(3, 4, 3, 4);
            cmbUserLevel.Name = "cmbUserLevel";
            cmbUserLevel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbUserLevel.Size = new Size(282, 36);
            cmbUserLevel.TabIndex = 55;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.Control;
            label11.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.Black;
            label11.Location = new Point(883, 256);
            label11.Name = "label11";
            label11.Size = new Size(107, 23);
            label11.TabIndex = 40;
            label11.Text = "User Level:";
            // 
            // txtContact
            // 
            txtContact.BorderColor = SystemColors.ButtonFace;
            txtContact.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtContact.BorderRadius = 8;
            txtContact.BorderThickness = 2;
            txtContact.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtContact.ForeColor = SystemColors.GrayText;
            txtContact.InnerBackColor = SystemColors.InactiveCaption;
            txtContact.InnerForeColor = Color.Black;
            txtContact.IsPasswordField = false;
            txtContact.Location = new Point(989, 31);
            txtContact.Margin = new Padding(3, 4, 3, 4);
            txtContact.Name = "txtContact";
            txtContact.PasswordChar = '\0';
            txtContact.PlaceholderColor = Color.Gray;
            txtContact.PlaceholderText = "";
            txtContact.Size = new Size(282, 52);
            txtContact.TabIndex = 39;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.Control;
            label10.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(883, 43);
            label10.Name = "label10";
            label10.Size = new Size(106, 23);
            label10.TabIndex = 38;
            label10.Text = "Contact #:";
            // 
            // txtMiddleName
            // 
            txtMiddleName.BorderColor = SystemColors.ButtonFace;
            txtMiddleName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtMiddleName.BorderRadius = 8;
            txtMiddleName.BorderThickness = 2;
            txtMiddleName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtMiddleName.ForeColor = SystemColors.GrayText;
            txtMiddleName.InnerBackColor = SystemColors.InactiveCaption;
            txtMiddleName.InnerForeColor = Color.Black;
            txtMiddleName.IsPasswordField = false;
            txtMiddleName.Location = new Point(226, 308);
            txtMiddleName.Margin = new Padding(3, 4, 3, 4);
            txtMiddleName.Name = "txtMiddleName";
            txtMiddleName.PasswordChar = '\0';
            txtMiddleName.PlaceholderColor = Color.Gray;
            txtMiddleName.PlaceholderText = "";
            txtMiddleName.Size = new Size(373, 52);
            txtMiddleName.TabIndex = 37;
            // 
            // txtFirstName
            // 
            txtFirstName.BorderColor = SystemColors.ButtonFace;
            txtFirstName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtFirstName.BorderRadius = 8;
            txtFirstName.BorderThickness = 2;
            txtFirstName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtFirstName.ForeColor = SystemColors.GrayText;
            txtFirstName.InnerBackColor = SystemColors.InactiveCaption;
            txtFirstName.InnerForeColor = Color.Black;
            txtFirstName.IsPasswordField = false;
            txtFirstName.Location = new Point(226, 248);
            txtFirstName.Margin = new Padding(3, 4, 3, 4);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.PasswordChar = '\0';
            txtFirstName.PlaceholderColor = Color.Gray;
            txtFirstName.PlaceholderText = "";
            txtFirstName.Size = new Size(373, 52);
            txtFirstName.TabIndex = 36;
            // 
            // txtLastName
            // 
            txtLastName.BorderColor = SystemColors.ButtonFace;
            txtLastName.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtLastName.BorderRadius = 8;
            txtLastName.BorderThickness = 2;
            txtLastName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtLastName.ForeColor = SystemColors.GrayText;
            txtLastName.InnerBackColor = SystemColors.InactiveCaption;
            txtLastName.InnerForeColor = Color.Black;
            txtLastName.IsPasswordField = false;
            txtLastName.Location = new Point(226, 176);
            txtLastName.Margin = new Padding(3, 4, 3, 4);
            txtLastName.Name = "txtLastName";
            txtLastName.PasswordChar = '\0';
            txtLastName.PlaceholderColor = Color.Gray;
            txtLastName.PlaceholderText = "";
            txtLastName.Size = new Size(373, 52);
            txtLastName.TabIndex = 35;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.Control;
            label9.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(89, 323);
            label9.Name = "label9";
            label9.Size = new Size(142, 23);
            label9.TabIndex = 34;
            label9.Text = "Middle Name:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.Control;
            label8.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(89, 260);
            label8.Name = "label8";
            label8.Size = new Size(111, 23);
            label8.TabIndex = 33;
            label8.Text = "First Name:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.Control;
            label7.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(89, 188);
            label7.Name = "label7";
            label7.Size = new Size(112, 23);
            label7.TabIndex = 32;
            label7.Text = "Last Name:";
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
            txtPassword.InnerForeColor = Color.Black;
            txtPassword.IsPasswordField = false;
            txtPassword.Location = new Point(226, 103);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '\0';
            txtPassword.PlaceholderColor = Color.Gray;
            txtPassword.PlaceholderText = "";
            txtPassword.Size = new Size(373, 52);
            txtPassword.TabIndex = 31;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // txtAge
            // 
            txtAge.BorderColor = SystemColors.ButtonFace;
            txtAge.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtAge.BorderRadius = 8;
            txtAge.BorderThickness = 2;
            txtAge.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAge.ForeColor = SystemColors.GrayText;
            txtAge.InnerBackColor = SystemColors.InactiveCaption;
            txtAge.InnerForeColor = Color.Black;
            txtAge.IsPasswordField = false;
            txtAge.Location = new Point(989, 103);
            txtAge.Margin = new Padding(3, 4, 3, 4);
            txtAge.Name = "txtAge";
            txtAge.PasswordChar = '\0';
            txtAge.PlaceholderColor = Color.Gray;
            txtAge.PlaceholderText = "";
            txtAge.Size = new Size(282, 52);
            txtAge.TabIndex = 29;
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
            txtUsername.InnerForeColor = Color.Black;
            txtUsername.IsPasswordField = false;
            txtUsername.Location = new Point(226, 31);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PasswordChar = '\0';
            txtUsername.PlaceholderColor = Color.Gray;
            txtUsername.PlaceholderText = "";
            txtUsername.Size = new Size(373, 52);
            txtUsername.TabIndex = 28;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.Control;
            label6.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(89, 115);
            label6.Name = "label6";
            label6.Size = new Size(101, 23);
            label6.TabIndex = 25;
            label6.Text = "Password:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.Control;
            label5.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(883, 184);
            label5.Name = "label5";
            label5.Size = new Size(98, 23);
            label5.TabIndex = 24;
            label5.Text = "Birthdate:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(883, 116);
            label4.Name = "label4";
            label4.Size = new Size(54, 23);
            label4.TabIndex = 23;
            label4.Text = "Age:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(89, 44);
            label3.Name = "label3";
            label3.Size = new Size(108, 23);
            label3.TabIndex = 22;
            label3.Text = "Username:";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.BorderColor = Color.Transparent;
            btnAdd.BorderRadius = 20;
            btnAdd.BorderSize = 0;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(398, 871);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(145, 65);
            btnAdd.TabIndex = 36;
            btnAdd.Text = "ADD ";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.SteelBlue;
            btnUpdate.BorderColor = Color.Transparent;
            btnUpdate.BorderRadius = 20;
            btnUpdate.BorderSize = 0;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(561, 871);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(145, 65);
            btnUpdate.TabIndex = 37;
            btnUpdate.Text = "EDIT";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.BorderColor = Color.Transparent;
            btnDelete.BorderRadius = 20;
            btnDelete.BorderSize = 0;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(730, 871);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(145, 65);
            btnDelete.TabIndex = 38;
            btnDelete.Text = "DELETE";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.SteelBlue;
            btnClear.BorderColor = Color.Transparent;
            btnClear.BorderRadius = 20;
            btnClear.BorderSize = 0;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(885, 871);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(145, 65);
            btnClear.TabIndex = 42;
            btnClear.Text = "CLEAR";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // txtSearch
            // 
            txtSearch.BorderColor = SystemColors.ButtonFace;
            txtSearch.BorderFocusColor = Color.FromArgb(30, 45, 61);
            txtSearch.BorderRadius = 8;
            txtSearch.BorderThickness = 2;
            txtSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtSearch.ForeColor = SystemColors.GrayText;
            txtSearch.InnerBackColor = SystemColors.InactiveCaption;
            txtSearch.InnerForeColor = Color.Black;
            txtSearch.IsPasswordField = false;
            txtSearch.Location = new Point(48, 97);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PasswordChar = '\0';
            txtSearch.PlaceholderColor = Color.Gray;
            txtSearch.PlaceholderText = "";
            txtSearch.Size = new Size(599, 52);
            txtSearch.TabIndex = 43;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(48, 68);
            label12.Name = "label12";
            label12.Size = new Size(87, 23);
            label12.TabIndex = 44;
            label12.Text = "SEARCH";
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
            btnBack.Location = new Point(14, 881);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(78, 45);
            btnBack.TabIndex = 23;
            btnBack.Text = "BACK";
            btnBack.TextAlign = ContentAlignment.TopCenter;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // ManageUsersFrm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1463, 960);
            Controls.Add(btnBack);
            Controls.Add(label12);
            Controls.Add(txtSearch);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(dgvUsers);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "ManageUsersFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageUsersFrm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();


        }

        #endregion

        private Label lblAdminName;
        private Label titleLabel;
        private Panel titleBar;
        private Button closeButton;
        private DataGridView dgvUsers;
        private Label label2;
        private Panel panel1;
        private CustomControls.CustomTextBox txtPassword;
        private CustomControls.CustomTextBox txtAge;
        private CustomControls.CustomTextBox txtUsername;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label11;
        private CustomControls.CustomTextBox txtContact;
        private Label label10;
        private CustomControls.CustomTextBox txtMiddleName;
        private CustomControls.CustomTextBox txtFirstName;
        private CustomControls.CustomTextBox txtLastName;
        private Label label9;
        private Label label8;
        private Label label7;
        private RoundedButton btnAdd;
        private RoundedButton btnUpdate;
        private RoundedButton btnDelete;
        private RoundedButton btnClear;
        private CustomControls.CustomTextBox txtSearch;
        private Label label12;
        private RoundedButton btnBack;
        private Guna.UI2.WinForms.Guna2ComboBox cmbUserLevel;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpBirthdate;
        private Label label1;
        private Label lblStrengthIndicator;
    }
}