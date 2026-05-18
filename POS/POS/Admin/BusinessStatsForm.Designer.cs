namespace POS.Admin
{
    partial class BusinessStatsForm
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
            LiveChartsCore.SkiaSharpView.SKCharts.SKDefaultLegend skDefaultLegend1 = new LiveChartsCore.SkiaSharpView.SKCharts.SKDefaultLegend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BusinessStatsForm));
            LiveChartsCore.Drawing.Padding padding1 = new LiveChartsCore.Drawing.Padding();
            LiveChartsCore.SkiaSharpView.SKCharts.SKDefaultTooltip skDefaultTooltip1 = new LiveChartsCore.SkiaSharpView.SKCharts.SKDefaultTooltip();
            LiveChartsCore.Drawing.Padding padding2 = new LiveChartsCore.Drawing.Padding();
            titleBar = new Panel();
            lblAdminName = new Label();
            closeButton = new Button();
            titleLabel = new Label();
            btnBack = new RoundedButton();
            dtpFrom = new Guna.UI2.WinForms.Guna2DateTimePicker();
            dtpTo = new Guna.UI2.WinForms.Guna2DateTimePicker();
            cmbQuickFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            btnRefresh = new RoundedButton();
            lblLoadingIndicator = new Label();
            lblTotalRevenue = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            lblTotalTransactions = new Label();
            lblAvgTransValue = new Label();
            lblTotalItemsSold = new Label();
            lblTotalDiscount = new Label();
            lblTotalVAT = new Label();
            lblCompletedTrans = new Label();
            lblVoidedTrans = new Label();
            lblLowStockCount = new Label();
            lblOutOfStockCount = new Label();
            label12 = new Label();
            label11 = new Label();
            label13 = new Label();
            label14 = new Label();
            lblGrossSales = new Label();
            label1 = new Label();
            label15 = new Label();
            label4 = new Label();
            lblTotalProducts = new Label();
            cartesianChart1 = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            btnToggleChart = new RoundedButton();
            btnBestSellers = new RoundedButton();
            panel1 = new Panel();
            titleBar.SuspendLayout();
            panel1.SuspendLayout();
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
            titleBar.Size = new Size(1280, 48);
            titleBar.TabIndex = 18;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAdminName.ForeColor = Color.White;
            lblAdminName.Location = new Point(1036, 12);
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
            closeButton.Location = new Point(1232, 0);
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
            btnBack.Location = new Point(23, 744);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(68, 34);
            btnBack.TabIndex = 24;
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
            dtpFrom.Location = new Point(1017, 212);
            dtpFrom.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpFrom.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.ShadowDecoration.CustomizableEdges = customizableEdges2;
            dtpFrom.Size = new Size(200, 36);
            dtpFrom.TabIndex = 25;
            dtpFrom.Value = new DateTime(2026, 4, 25, 20, 39, 32, 778);
            // 
            // dtpTo
            // 
            dtpTo.Checked = true;
            dtpTo.CustomizableEdges = customizableEdges3;
            dtpTo.FillColor = SystemColors.InactiveCaption;
            dtpTo.Font = new Font("Segoe UI", 9F);
            dtpTo.Format = DateTimePickerFormat.Long;
            dtpTo.Location = new Point(1017, 287);
            dtpTo.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpTo.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtpTo.Name = "dtpTo";
            dtpTo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            dtpTo.Size = new Size(200, 36);
            dtpTo.TabIndex = 26;
            dtpTo.Value = new DateTime(2026, 4, 25, 20, 39, 32, 778);
            // 
            // cmbQuickFilter
            // 
            cmbQuickFilter.BackColor = Color.Transparent;
            cmbQuickFilter.BorderColor = Color.White;
            cmbQuickFilter.CustomizableEdges = customizableEdges5;
            cmbQuickFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbQuickFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuickFilter.FillColor = SystemColors.InactiveCaption;
            cmbQuickFilter.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbQuickFilter.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbQuickFilter.Font = new Font("Segoe UI", 10F);
            cmbQuickFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cmbQuickFilter.ItemHeight = 30;
            cmbQuickFilter.Location = new Point(976, 129);
            cmbQuickFilter.Name = "cmbQuickFilter";
            cmbQuickFilter.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cmbQuickFilter.Size = new Size(241, 36);
            cmbQuickFilter.TabIndex = 55;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.SteelBlue;
            btnRefresh.BorderColor = Color.Transparent;
            btnRefresh.BorderRadius = 10;
            btnRefresh.BorderSize = 0;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(1054, 339);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(82, 34);
            btnRefresh.TabIndex = 56;
            btnRefresh.Text = "Refresh";
            btnRefresh.TextAlign = ContentAlignment.TopCenter;
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblLoadingIndicator
            // 
            lblLoadingIndicator.AutoSize = true;
            lblLoadingIndicator.Location = new Point(1165, 351);
            lblLoadingIndicator.Name = "lblLoadingIndicator";
            lblLoadingIndicator.Size = new Size(50, 15);
            lblLoadingIndicator.TabIndex = 57;
            lblLoadingIndicator.Text = "Loading";
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalRevenue.Location = new Point(112, 111);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(72, 21);
            lblTotalRevenue.TabIndex = 58;
            lblTotalRevenue.Text = "Total Rev";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(928, 423);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 60;
            label2.Text = "Total Transactions";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(928, 449);
            label3.Name = "label3";
            label3.Size = new Size(122, 15);
            label3.TabIndex = 61;
            label3.Text = "Avg Transaction Value";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(928, 567);
            label5.Name = "label5";
            label5.Size = new Size(115, 15);
            label5.TabIndex = 63;
            label5.Text = "Total Discount Given";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(928, 593);
            label6.Name = "label6";
            label6.Size = new Size(107, 15);
            label6.TabIndex = 64;
            label6.Text = "Total VAT Collected";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(929, 477);
            label7.Name = "label7";
            label7.Size = new Size(66, 15);
            label7.TabIndex = 65;
            label7.Text = "Completed";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(929, 502);
            label8.Name = "label8";
            label8.Size = new Size(43, 15);
            label8.TabIndex = 66;
            label8.Text = "Voided";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(928, 642);
            label9.Name = "label9";
            label9.Size = new Size(93, 15);
            label9.TabIndex = 67;
            label9.Text = "Low Stock Items";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(928, 671);
            label10.Name = "label10";
            label10.Size = new Size(73, 15);
            label10.TabIndex = 68;
            label10.Text = "Out of Stock";
            // 
            // lblTotalTransactions
            // 
            lblTotalTransactions.AutoSize = true;
            lblTotalTransactions.Location = new Point(1160, 423);
            lblTotalTransactions.Name = "lblTotalTransactions";
            lblTotalTransactions.Size = new Size(54, 15);
            lblTotalTransactions.TabIndex = 69;
            lblTotalTransactions.Text = "Total Rev";
            // 
            // lblAvgTransValue
            // 
            lblAvgTransValue.AutoSize = true;
            lblAvgTransValue.Location = new Point(1160, 449);
            lblAvgTransValue.Name = "lblAvgTransValue";
            lblAvgTransValue.Size = new Size(54, 15);
            lblAvgTransValue.TabIndex = 70;
            lblAvgTransValue.Text = "Total Rev";
            // 
            // lblTotalItemsSold
            // 
            lblTotalItemsSold.AutoSize = true;
            lblTotalItemsSold.Font = new Font("Segoe UI", 12F);
            lblTotalItemsSold.Location = new Point(532, 111);
            lblTotalItemsSold.Name = "lblTotalItemsSold";
            lblTotalItemsSold.Size = new Size(72, 21);
            lblTotalItemsSold.TabIndex = 71;
            lblTotalItemsSold.Text = "Total Rev";
            // 
            // lblTotalDiscount
            // 
            lblTotalDiscount.AutoSize = true;
            lblTotalDiscount.Location = new Point(1160, 567);
            lblTotalDiscount.Name = "lblTotalDiscount";
            lblTotalDiscount.Size = new Size(54, 15);
            lblTotalDiscount.TabIndex = 72;
            lblTotalDiscount.Text = "Total Rev";
            // 
            // lblTotalVAT
            // 
            lblTotalVAT.AutoSize = true;
            lblTotalVAT.Location = new Point(1160, 593);
            lblTotalVAT.Name = "lblTotalVAT";
            lblTotalVAT.Size = new Size(54, 15);
            lblTotalVAT.TabIndex = 73;
            lblTotalVAT.Text = "Total Rev";
            // 
            // lblCompletedTrans
            // 
            lblCompletedTrans.AutoSize = true;
            lblCompletedTrans.Location = new Point(1161, 477);
            lblCompletedTrans.Name = "lblCompletedTrans";
            lblCompletedTrans.Size = new Size(54, 15);
            lblCompletedTrans.TabIndex = 74;
            lblCompletedTrans.Text = "Total Rev";
            // 
            // lblVoidedTrans
            // 
            lblVoidedTrans.AutoSize = true;
            lblVoidedTrans.Location = new Point(1161, 502);
            lblVoidedTrans.Name = "lblVoidedTrans";
            lblVoidedTrans.Size = new Size(54, 15);
            lblVoidedTrans.TabIndex = 75;
            lblVoidedTrans.Text = "Total Rev";
            // 
            // lblLowStockCount
            // 
            lblLowStockCount.AutoSize = true;
            lblLowStockCount.Location = new Point(1160, 642);
            lblLowStockCount.Name = "lblLowStockCount";
            lblLowStockCount.Size = new Size(54, 15);
            lblLowStockCount.TabIndex = 76;
            lblLowStockCount.Text = "Total Rev";
            // 
            // lblOutOfStockCount
            // 
            lblOutOfStockCount.AutoSize = true;
            lblOutOfStockCount.Location = new Point(1160, 671);
            lblOutOfStockCount.Name = "lblOutOfStockCount";
            lblOutOfStockCount.Size = new Size(54, 15);
            lblOutOfStockCount.TabIndex = 77;
            lblOutOfStockCount.Text = "Total Rev";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.LightSteelBlue;
            label12.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Black;
            label12.Location = new Point(1017, 190);
            label12.Name = "label12";
            label12.Size = new Size(53, 19);
            label12.TabIndex = 78;
            label12.Text = "FROM";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.LightSteelBlue;
            label11.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.Black;
            label11.Location = new Point(1017, 265);
            label11.Name = "label11";
            label11.Size = new Size(29, 19);
            label11.TabIndex = 79;
            label11.Text = "TO";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.LightSteelBlue;
            label13.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Black;
            label13.Location = new Point(976, 89);
            label13.Name = "label13";
            label13.Size = new Size(52, 19);
            label13.TabIndex = 80;
            label13.Text = "FILTER";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = Color.LightSteelBlue;
            label14.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.Black;
            label14.Location = new Point(84, 77);
            label14.Name = "label14";
            label14.Size = new Size(125, 19);
            label14.TabIndex = 81;
            label14.Text = "TOTAL REVENUE";
            // 
            // lblGrossSales
            // 
            lblGrossSales.AutoSize = true;
            lblGrossSales.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGrossSales.Location = new Point(289, 111);
            lblGrossSales.Name = "lblGrossSales";
            lblGrossSales.Size = new Size(86, 21);
            lblGrossSales.TabIndex = 82;
            lblGrossSales.Text = "gross sales";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.LightSteelBlue;
            label1.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(277, 77);
            label1.Name = "label1";
            label1.Size = new Size(107, 19);
            label1.TabIndex = 83;
            label1.Text = "GROSS SALES";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.LightSteelBlue;
            label15.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Black;
            label15.Location = new Point(502, 77);
            label15.Name = "label15";
            label15.Size = new Size(143, 19);
            label15.TabIndex = 84;
            label15.Text = "TOTAL ITEMS SOLD";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.LightSteelBlue;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(695, 77);
            label4.Name = "label4";
            label4.Size = new Size(138, 19);
            label4.TabIndex = 85;
            label4.Text = "TOTAL PRODUCTS";
            // 
            // lblTotalProducts
            // 
            lblTotalProducts.AutoSize = true;
            lblTotalProducts.Font = new Font("Segoe UI", 12F);
            lblTotalProducts.Location = new Point(725, 110);
            lblTotalProducts.Name = "lblTotalProducts";
            lblTotalProducts.Size = new Size(72, 21);
            lblTotalProducts.TabIndex = 86;
            lblTotalProducts.Text = "Total Rev";
            // 
            // cartesianChart1
            // 
            cartesianChart1.AutoScroll = true;
            cartesianChart1.AutoUpdateEnabled = true;
            cartesianChart1.ChartTheme = null;
            skDefaultLegend1.AnimationsSpeed = TimeSpan.Parse("00:00:00.1500000");
            skDefaultLegend1.Content = null;
            skDefaultLegend1.IsValid = false;
            skDefaultLegend1.Opacity = 1F;
            padding1.Bottom = 0F;
            padding1.Left = 0F;
            padding1.Right = 0F;
            padding1.Top = 0F;
            skDefaultLegend1.Padding = padding1;
            skDefaultLegend1.RemoveOnCompleted = false;
            skDefaultLegend1.RotateTransform = 0F;
            skDefaultLegend1.X = 0F;
            skDefaultLegend1.Y = 0F;
            cartesianChart1.Legend = skDefaultLegend1;
            cartesianChart1.Location = new Point(3, 1);
            cartesianChart1.MatchAxesScreenDataRatio = false;
            cartesianChart1.Name = "cartesianChart1";
            cartesianChart1.Size = new Size(800, 560);
            cartesianChart1.TabIndex = 87;
            skDefaultTooltip1.AnimationsSpeed = TimeSpan.Parse("00:00:00.1500000");
            skDefaultTooltip1.Content = null;
            skDefaultTooltip1.IsValid = false;
            skDefaultTooltip1.Opacity = 1F;
            padding2.Bottom = 0F;
            padding2.Left = 0F;
            padding2.Right = 0F;
            padding2.Top = 0F;
            skDefaultTooltip1.Padding = padding2;
            skDefaultTooltip1.RemoveOnCompleted = false;
            skDefaultTooltip1.RotateTransform = 0F;
            skDefaultTooltip1.Wedge = 10;
            skDefaultTooltip1.X = 0F;
            skDefaultTooltip1.Y = 0F;
            cartesianChart1.Tooltip = skDefaultTooltip1;
            cartesianChart1.TooltipFindingStrategy = LiveChartsCore.Measure.TooltipFindingStrategy.Automatic;
            cartesianChart1.UpdaterThrottler = TimeSpan.Parse("00:00:00.0500000");
            // 
            // btnToggleChart
            // 
            btnToggleChart.BackColor = Color.SteelBlue;
            btnToggleChart.BorderColor = Color.Transparent;
            btnToggleChart.BorderRadius = 10;
            btnToggleChart.BorderSize = 0;
            btnToggleChart.FlatAppearance.BorderSize = 0;
            btnToggleChart.FlatStyle = FlatStyle.Flat;
            btnToggleChart.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnToggleChart.ForeColor = Color.White;
            btnToggleChart.Location = new Point(763, 738);
            btnToggleChart.Name = "btnToggleChart";
            btnToggleChart.Size = new Size(121, 39);
            btnToggleChart.TabIndex = 88;
            btnToggleChart.Text = "Toggle Chart";
            btnToggleChart.TextAlign = ContentAlignment.TopCenter;
            btnToggleChart.UseVisualStyleBackColor = false;
            btnToggleChart.Click += btnToggleChart_Click;
            // 
            // btnBestSellers
            // 
            btnBestSellers.BackColor = Color.SteelBlue;
            btnBestSellers.BorderColor = Color.Transparent;
            btnBestSellers.BorderRadius = 10;
            btnBestSellers.BorderSize = 0;
            btnBestSellers.FlatAppearance.BorderSize = 0;
            btnBestSellers.FlatStyle = FlatStyle.Flat;
            btnBestSellers.Font = new Font("Dubai", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBestSellers.ForeColor = Color.White;
            btnBestSellers.Location = new Point(623, 738);
            btnBestSellers.Name = "btnBestSellers";
            btnBestSellers.Size = new Size(121, 39);
            btnBestSellers.TabIndex = 89;
            btnBestSellers.Text = "Best Sellers";
            btnBestSellers.TextAlign = ContentAlignment.TopCenter;
            btnBestSellers.UseVisualStyleBackColor = false;
            btnBestSellers.Click += btnBestSellers_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(cartesianChart1);
            panel1.Location = new Point(64, 168);
            panel1.Name = "panel1";
            panel1.Size = new Size(820, 564);
            panel1.TabIndex = 90;
            // 
            // BusinessStatsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 789);
            Controls.Add(panel1);
            Controls.Add(btnBestSellers);
            Controls.Add(btnToggleChart);
            Controls.Add(lblTotalProducts);
            Controls.Add(label4);
            Controls.Add(label15);
            Controls.Add(label1);
            Controls.Add(lblGrossSales);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(lblOutOfStockCount);
            Controls.Add(lblLowStockCount);
            Controls.Add(lblVoidedTrans);
            Controls.Add(lblCompletedTrans);
            Controls.Add(lblTotalVAT);
            Controls.Add(lblTotalDiscount);
            Controls.Add(lblTotalItemsSold);
            Controls.Add(lblAvgTransValue);
            Controls.Add(lblTotalTransactions);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblTotalRevenue);
            Controls.Add(lblLoadingIndicator);
            Controls.Add(btnRefresh);
            Controls.Add(cmbQuickFilter);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(btnBack);
            Controls.Add(titleBar);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BusinessStatsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BusinessStatsForm";
            titleBar.ResumeLayout(false);
            titleBar.PerformLayout();
            panel1.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTo;
        private Guna.UI2.WinForms.Guna2ComboBox cmbQuickFilter;
        private RoundedButton btnRefresh;
        private Label lblLoadingIndicator;
        private Label lblTotalRevenue;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label lblTotalTransactions;
        private Label lblAvgTransValue;
        private Label lblTotalItemsSold;
        private Label lblTotalDiscount;
        private Label lblTotalVAT;
        private Label lblCompletedTrans;
        private Label lblVoidedTrans;
        private Label lblLowStockCount;
        private Label lblOutOfStockCount;
        private Label label12;
        private Label label11;
        private Label label13;
        private Label label14;
        private Label lblGrossSales;
        private Label label1;
        private Label label15;
        private Label label4;
        private Label lblTotalProducts;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart cartesianChart1;
        private RoundedButton btnToggleChart;
        private RoundedButton btnBestSellers;
        private Panel panel1;
    }
}