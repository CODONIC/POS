namespace POS.Admin
{
    partial class EmployeeLogsFrm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            titleBar = new Panel();
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnBack = new RoundedButton();
            dtpFrom = new Guna.UI2.WinForms.Guna2DateTimePicker();
            label1 = new Label();
            dtpTo = new Guna.UI2.WinForms.Guna2DateTimePicker();
            label2 = new Label();
            cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            cmbAction = new Guna.UI2.WinForms.Guna2ComboBox();
            label12 = new Label();
            txtSearch = new CustomControls.CustomTextBox();
            btnApplyFilter = new RoundedButton();
            btnResetFilter = new RoundedButton();
            dgvAuditLogs = new DataGridView();
            rtbOldValues = new RichTextBox();
            rtbNewValues = new RichTextBox();
            btnPrev = new RoundedButton();
            btnNext = new RoundedButton();
            btnExport = new RoundedButton();
            lblDetailAction = new Label();
            lblDetailTable = new Label();
            lblDetailEmployee = new Label();
            lblDetailTime = new Label();
            lblPagination = new Label();
            label3 = new Label();
            label4 = new Label();
            titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).BeginInit();
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
            titleBar.Size = new Size(1592, 48);
            titleBar.TabIndex = 18;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1347, 12);
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
            closeButton.Location = new Point(1544, 0);
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
            btnBack.Location = new Point(33, 827);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(68, 34);
            btnBack.TabIndex = 25;
            btnBack.Text = "BACK";
            btnBack.TextAlign = ContentAlignment.TopCenter;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // dtpFrom
            // 
            dtpFrom.Checked = true;
            dtpFrom.CustomizableEdges = customizableEdges1;
            dtpFrom.FillColor = SystemColors.InactiveCaption;
            dtpFrom.Font = new Font("Segoe UI", 9F);
            dtpFrom.Format = DateTimePickerFormat.Long;
            dtpFrom.Location = new Point(562, 91);
            dtpFrom.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFrom.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtpFrom.Size = new Size(200, 36);
            dtpFrom.TabIndex = 26;
            dtpFrom.Value = new DateTime(2026, 4, 28, 3, 0, 48, 860);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(511, 91);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 27;
            label1.Text = "From";
            // 
            // dtpTo
            // 
            dtpTo.Checked = true;
            dtpTo.CustomizableEdges = customizableEdges3;
            dtpTo.FillColor = SystemColors.InactiveCaption;
            dtpTo.Font = new Font("Segoe UI", 9F);
            dtpTo.Format = DateTimePickerFormat.Long;
            dtpTo.Location = new Point(818, 91);
            dtpTo.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpTo.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpTo.Name = "dtpTo";
            dtpTo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            dtpTo.Size = new Size(200, 36);
            dtpTo.TabIndex = 28;
            dtpTo.Value = new DateTime(2026, 4, 28, 3, 0, 48, 860);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(772, 91);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 29;
            label2.Text = "To";
            // 
            // cmbCategory
            // 
            cmbCategory.BackColor = Color.Transparent;
            cmbCategory.BorderColor = Color.White;
            cmbCategory.CustomizableEdges = customizableEdges5;
            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FillColor = SystemColors.InactiveCaption;
            cmbCategory.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbCategory.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbCategory.Font = new Font("Segoe UI", 10F);
            cmbCategory.ForeColor = Color.FromArgb(68, 88, 112);
            cmbCategory.ItemHeight = 30;
            cmbCategory.Location = new Point(1077, 91);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cmbCategory.Size = new Size(200, 36);
            cmbCategory.TabIndex = 55;
            // 
            // cmbAction
            // 
            cmbAction.BackColor = Color.Transparent;
            cmbAction.BorderColor = Color.White;
            cmbAction.CustomizableEdges = customizableEdges7;
            cmbAction.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAction.FillColor = SystemColors.InactiveCaption;
            cmbAction.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbAction.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbAction.Font = new Font("Segoe UI", 10F);
            cmbAction.ForeColor = Color.FromArgb(68, 88, 112);
            cmbAction.ItemHeight = 30;
            cmbAction.Location = new Point(1306, 91);
            cmbAction.Name = "cmbAction";
            cmbAction.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cmbAction.Size = new Size(200, 36);
            cmbAction.TabIndex = 56;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(33, 69);
            label12.Name = "label12";
            label12.Size = new Size(69, 19);
            label12.TabIndex = 58;
            label12.Text = "SEARCH";
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
            txtSearch.Location = new Point(33, 91);
            txtSearch.Name = "txtSearch";
            txtSearch.PasswordChar = '\0';
            txtSearch.PlaceholderColor = Color.Gray;
            txtSearch.PlaceholderText = "";
            txtSearch.Size = new Size(471, 39);
            txtSearch.TabIndex = 57;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.BackColor = Color.SteelBlue;
            btnApplyFilter.BorderColor = Color.Transparent;
            btnApplyFilter.BorderRadius = 10;
            btnApplyFilter.BorderSize = 0;
            btnApplyFilter.FlatAppearance.BorderSize = 0;
            btnApplyFilter.FlatStyle = FlatStyle.Flat;
            btnApplyFilter.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnApplyFilter.ForeColor = Color.White;
            btnApplyFilter.Location = new Point(1347, 572);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(81, 36);
            btnApplyFilter.TabIndex = 59;
            btnApplyFilter.Text = "APPLY";
            btnApplyFilter.TextAlign = ContentAlignment.TopCenter;
            btnApplyFilter.UseVisualStyleBackColor = false;
            btnApplyFilter.Click += btnApplyFilter_Click;
            // 
            // btnResetFilter
            // 
            btnResetFilter.BackColor = Color.SteelBlue;
            btnResetFilter.BorderColor = Color.Transparent;
            btnResetFilter.BorderRadius = 10;
            btnResetFilter.BorderSize = 0;
            btnResetFilter.FlatAppearance.BorderSize = 0;
            btnResetFilter.FlatStyle = FlatStyle.Flat;
            btnResetFilter.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnResetFilter.ForeColor = Color.White;
            btnResetFilter.Location = new Point(1450, 572);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(81, 36);
            btnResetFilter.TabIndex = 60;
            btnResetFilter.Text = "RESET";
            btnResetFilter.TextAlign = ContentAlignment.TopCenter;
            btnResetFilter.UseVisualStyleBackColor = false;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // dgvAuditLogs
            // 
            dgvAuditLogs.AllowUserToAddRows = false;
            dgvAuditLogs.AllowUserToDeleteRows = false;
            dgvAuditLogs.AllowUserToOrderColumns = true;
            dgvAuditLogs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 249, 250);
            dgvAuditLogs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvAuditLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditLogs.BackgroundColor = Color.White;
            dgvAuditLogs.BorderStyle = BorderStyle.None;
            dgvAuditLogs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAuditLogs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12.25F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(5, 0, 5, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAuditLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAuditLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAuditLogs.EnableHeadersVisualStyles = false;
            dgvAuditLogs.GridColor = Color.FromArgb(230, 230, 230);
            dgvAuditLogs.Location = new Point(33, 150);
            dgvAuditLogs.Name = "dgvAuditLogs";
            dgvAuditLogs.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvAuditLogs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvAuditLogs.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(64, 64, 64);
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.InactiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(33, 37, 41);
            dgvAuditLogs.RowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvAuditLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAuditLogs.Size = new Size(1244, 625);
            dgvAuditLogs.TabIndex = 61;
            // 
            // rtbOldValues
            // 
            rtbOldValues.Location = new Point(1306, 222);
            rtbOldValues.Name = "rtbOldValues";
            rtbOldValues.Size = new Size(274, 152);
            rtbOldValues.TabIndex = 62;
            rtbOldValues.Text = "";
            // 
            // rtbNewValues
            // 
            rtbNewValues.Location = new Point(1306, 414);
            rtbNewValues.Name = "rtbNewValues";
            rtbNewValues.Size = new Size(274, 152);
            rtbNewValues.TabIndex = 63;
            rtbNewValues.Text = "";
            // 
            // btnPrev
            // 
            btnPrev.BackColor = Color.SteelBlue;
            btnPrev.BorderColor = Color.Transparent;
            btnPrev.BorderRadius = 10;
            btnPrev.BorderSize = 0;
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPrev.ForeColor = Color.White;
            btnPrev.Location = new Point(1132, 798);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(68, 34);
            btnPrev.TabIndex = 64;
            btnPrev.Text = "Prev";
            btnPrev.TextAlign = ContentAlignment.TopCenter;
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.SteelBlue;
            btnNext.BorderColor = Color.Transparent;
            btnNext.BorderRadius = 10;
            btnNext.BorderSize = 0;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(1220, 798);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(68, 34);
            btnNext.TabIndex = 65;
            btnNext.Text = "Next";
            btnNext.TextAlign = ContentAlignment.TopCenter;
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.SteelBlue;
            btnExport.BorderColor = Color.Transparent;
            btnExport.BorderRadius = 10;
            btnExport.BorderSize = 0;
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(1033, 798);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(76, 34);
            btnExport.TabIndex = 66;
            btnExport.Text = "Export";
            btnExport.TextAlign = ContentAlignment.TopCenter;
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // lblDetailAction
            // 
            lblDetailAction.AutoSize = true;
            lblDetailAction.Location = new Point(1306, 760);
            lblDetailAction.Name = "lblDetailAction";
            lblDetailAction.Size = new Size(19, 15);
            lblDetailAction.TabIndex = 67;
            lblDetailAction.Text = "To";
            // 
            // lblDetailTable
            // 
            lblDetailTable.AutoSize = true;
            lblDetailTable.Location = new Point(1306, 650);
            lblDetailTable.Name = "lblDetailTable";
            lblDetailTable.Size = new Size(19, 15);
            lblDetailTable.TabIndex = 68;
            lblDetailTable.Text = "To";
            // 
            // lblDetailEmployee
            // 
            lblDetailEmployee.AutoSize = true;
            lblDetailEmployee.Location = new Point(1306, 676);
            lblDetailEmployee.Name = "lblDetailEmployee";
            lblDetailEmployee.Size = new Size(19, 15);
            lblDetailEmployee.TabIndex = 69;
            lblDetailEmployee.Text = "To";
            // 
            // lblDetailTime
            // 
            lblDetailTime.AutoSize = true;
            lblDetailTime.Location = new Point(1306, 701);
            lblDetailTime.Name = "lblDetailTime";
            lblDetailTime.Size = new Size(19, 15);
            lblDetailTime.TabIndex = 70;
            lblDetailTime.Text = "To";
            // 
            // lblPagination
            // 
            lblPagination.AutoSize = true;
            lblPagination.Location = new Point(1306, 731);
            lblPagination.Name = "lblPagination";
            lblPagination.Size = new Size(19, 15);
            lblPagination.TabIndex = 71;
            lblPagination.Text = "To";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.LightSteelBlue;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(1306, 186);
            label3.Name = "label3";
            label3.Size = new Size(64, 19);
            label3.TabIndex = 72;
            label3.Text = "BEFORE";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.LightSteelBlue;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(1306, 392);
            label4.Name = "label4";
            label4.Size = new Size(53, 19);
            label4.TabIndex = 73;
            label4.Text = "AFTER";
            // 
            // EmployeeLogsFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1592, 877);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lblPagination);
            Controls.Add(lblDetailTime);
            Controls.Add(lblDetailEmployee);
            Controls.Add(lblDetailTable);
            Controls.Add(lblDetailAction);
            Controls.Add(btnExport);
            Controls.Add(btnNext);
            Controls.Add(btnPrev);
            Controls.Add(rtbNewValues);
            Controls.Add(rtbOldValues);
            Controls.Add(dgvAuditLogs);
            Controls.Add(btnResetFilter);
            Controls.Add(btnApplyFilter);
            Controls.Add(label12);
            Controls.Add(txtSearch);
            Controls.Add(cmbAction);
            Controls.Add(cmbCategory);
            Controls.Add(label2);
            Controls.Add(dtpTo);
            Controls.Add(label1);
            Controls.Add(dtpFrom);
            Controls.Add(btnBack);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EmployeeLogsFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EmployeeLogsFrm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditLogs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel titleBar;
        private Label lblAdminName;
        private Button closeButton;
        private Label titleLabel;
        private RoundedButton btnBack;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpFrom;
        private Label label1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTo;
        private Label label2;
        private Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAction;
        private Label label12;
        private CustomControls.CustomTextBox txtSearch;
        private RoundedButton btnApplyFilter;
        private RoundedButton btnResetFilter;
        private DataGridView dgvAuditLogs;
        private RichTextBox rtbOldValues;
        private RichTextBox rtbNewValues;
        private RoundedButton btnPrev;
        private RoundedButton btnNext;
        private RoundedButton btnExport;
        private Label lblDetailAction;
        private Label lblDetailTable;
        private Label lblDetailEmployee;
        private Label lblDetailTime;
        private Label lblPagination;
        private Label label3;
        private Label label4;
    }
}