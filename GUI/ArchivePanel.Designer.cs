namespace HPT1000.GUI
{
    partial class ArchivePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchivePanel));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 1D);
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLoadData = new System.Windows.Forms.ToolStripButton();
            this.brnRemoveData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dropDownBtnExportToFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuItemPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.labSummaryProces = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grBoxSeries = new System.Windows.Forms.GroupBox();
            this.cBoxDosingValve = new System.Windows.Forms.CheckBox();
            this.cBoxMotor2 = new System.Windows.Forms.CheckBox();
            this.cBoxMotor1 = new System.Windows.Forms.CheckBox();
            this.cBoxFlow1 = new System.Windows.Forms.CheckBox();
            this.cBoxFlow3 = new System.Windows.Forms.CheckBox();
            this.cBoxFlow2 = new System.Windows.Forms.CheckBox();
            this.cBoxPressure = new System.Windows.Forms.CheckBox();
            this.cBoxPower = new System.Windows.Forms.CheckBox();
            this.cBoxVoltage = new System.Windows.Forms.CheckBox();
            this.cBoxCurent = new System.Windows.Forms.CheckBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtBoxSummary = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.grBoxSeries.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.btnLoadData,
            this.toolStripSeparator3,
            this.brnRemoveData,
            this.toolStripSeparator1,
            this.dropDownBtnExportToFile,
            this.toolStripSeparator2,
            this.labSummaryProces,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1019, 27);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // btnLoadData
            // 
            this.btnLoadData.AutoSize = false;
            this.btnLoadData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLoadData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(95, 24);
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // brnRemoveData
            // 
            this.brnRemoveData.AutoSize = false;
            this.brnRemoveData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.brnRemoveData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.brnRemoveData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.brnRemoveData.Name = "brnRemoveData";
            this.brnRemoveData.Size = new System.Drawing.Size(87, 24);
            this.brnRemoveData.Text = "Remove Data";
            this.brnRemoveData.Click += new System.EventHandler(this.brnRemoveData_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // dropDownBtnExportToFile
            // 
            this.dropDownBtnExportToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dropDownBtnExportToFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPDF,
            this.menuItemCSV});
            this.dropDownBtnExportToFile.Enabled = false;
            this.dropDownBtnExportToFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dropDownBtnExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("dropDownBtnExportToFile.Image")));
            this.dropDownBtnExportToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropDownBtnExportToFile.Name = "dropDownBtnExportToFile";
            this.dropDownBtnExportToFile.Size = new System.Drawing.Size(93, 24);
            this.dropDownBtnExportToFile.Text = "Export to file";
            // 
            // menuItemPDF
            // 
            this.menuItemPDF.Name = "menuItemPDF";
            this.menuItemPDF.Size = new System.Drawing.Size(104, 22);
            this.menuItemPDF.Text = "*.PDF";
            this.menuItemPDF.Click += new System.EventHandler(this.btnExportToPDF_Click);
            // 
            // menuItemCSV
            // 
            this.menuItemCSV.Name = "menuItemCSV";
            this.menuItemCSV.Size = new System.Drawing.Size(104, 22);
            this.menuItemCSV.Text = "*.CSV";
            this.menuItemCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // labSummaryProces
            // 
            this.labSummaryProces.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labSummaryProces.ForeColor = System.Drawing.Color.Green;
            this.labSummaryProces.Name = "labSummaryProces";
            this.labSummaryProces.Size = new System.Drawing.Size(361, 24);
            this.labSummaryProces.Text = "Proces: \'Motor\' range date: 26.08.2017 11.06.43 - 27.08.2017 14.36.43";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.LightGray;
            this.chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.HorizontalCenter;
            this.chart.BorderlineColor = System.Drawing.Color.Black;
            this.chart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            this.chart.BorderlineWidth = 0;
            this.chart.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            this.chart.CausesValidation = false;
            chartArea1.AxisX.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisX.Title = "time (s)";
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.Title = "Pressure [mBar]";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Near;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea1.BorderColor = System.Drawing.Color.BlanchedAlmond;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 90F;
            chartArea1.InnerPlotPosition.Width = 86.867F;
            chartArea1.InnerPlotPosition.X = 11.78686F;
            chartArea1.Name = "ChartAreaPressure";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 85F;
            chartArea1.Position.Width = 82F;
            chartArea1.Position.X = 18F;
            chartArea1.Position.Y = 15F;
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.Crossing = -1.7976931348623157E+308D;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.IsMarginVisible = false;
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.MinorGrid.Enabled = true;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.Interval = 1D;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.MajorGrid.Interval = 1D;
            chartArea2.AxisY.MajorGrid.IntervalOffset = 1D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea2.AxisY.Maximum = 1D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.MinorGrid.Interval = 1D;
            chartArea2.AxisY.MinorGrid.IntervalOffset = 1D;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.Title = "Motor";
            chartArea2.AxisY.TitleAlignment = System.Drawing.StringAlignment.Near;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartAreaMotor";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 13F;
            chartArea2.Position.Width = 79F;
            chartArea2.Position.X = 20F;
            chartArea2.Position.Y = 1F;
            chartArea2.ShadowColor = System.Drawing.Color.Gainsboro;
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(1, 1);
            this.chart.Margin = new System.Windows.Forms.Padding(2);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartAreaPressure";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            series2.ChartArea = "ChartAreaMotor";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series2";
            series2.Points.Add(dataPoint1);
            series2.Points.Add(dataPoint2);
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            series3.ChartArea = "ChartAreaPressure";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series3";
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(1010, 530);
            this.chart.TabIndex = 12;
            // 
            // grBoxSeries
            // 
            this.grBoxSeries.Controls.Add(this.cBoxDosingValve);
            this.grBoxSeries.Controls.Add(this.cBoxMotor2);
            this.grBoxSeries.Controls.Add(this.cBoxMotor1);
            this.grBoxSeries.Controls.Add(this.cBoxFlow1);
            this.grBoxSeries.Controls.Add(this.cBoxFlow3);
            this.grBoxSeries.Controls.Add(this.cBoxFlow2);
            this.grBoxSeries.Controls.Add(this.cBoxPressure);
            this.grBoxSeries.Controls.Add(this.cBoxPower);
            this.grBoxSeries.Location = new System.Drawing.Point(5, 532);
            this.grBoxSeries.Margin = new System.Windows.Forms.Padding(2);
            this.grBoxSeries.Name = "grBoxSeries";
            this.grBoxSeries.Padding = new System.Windows.Forms.Padding(2);
            this.grBoxSeries.Size = new System.Drawing.Size(1005, 41);
            this.grBoxSeries.TabIndex = 14;
            this.grBoxSeries.TabStop = false;
            this.grBoxSeries.Text = "Series";
            // 
            // cBoxDosingValve
            // 
            this.cBoxDosingValve.AutoSize = true;
            this.cBoxDosingValve.Checked = true;
            this.cBoxDosingValve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxDosingValve.Location = new System.Drawing.Point(613, 17);
            this.cBoxDosingValve.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxDosingValve.Name = "cBoxDosingValve";
            this.cBoxDosingValve.Size = new System.Drawing.Size(88, 21);
            this.cBoxDosingValve.TabIndex = 20;
            this.cBoxDosingValve.Text = "Vaporiser";
            this.cBoxDosingValve.UseVisualStyleBackColor = true;
            this.cBoxDosingValve.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxMotor2
            // 
            this.cBoxMotor2.AutoSize = true;
            this.cBoxMotor2.Checked = true;
            this.cBoxMotor2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxMotor2.Location = new System.Drawing.Point(798, 17);
            this.cBoxMotor2.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxMotor2.Name = "cBoxMotor2";
            this.cBoxMotor2.Size = new System.Drawing.Size(75, 21);
            this.cBoxMotor2.TabIndex = 19;
            this.cBoxMotor2.Text = "Motor 2";
            this.cBoxMotor2.UseVisualStyleBackColor = true;
            this.cBoxMotor2.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxMotor1
            // 
            this.cBoxMotor1.AutoSize = true;
            this.cBoxMotor1.Checked = true;
            this.cBoxMotor1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxMotor1.Location = new System.Drawing.Point(713, 17);
            this.cBoxMotor1.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxMotor1.Name = "cBoxMotor1";
            this.cBoxMotor1.Size = new System.Drawing.Size(75, 21);
            this.cBoxMotor1.TabIndex = 18;
            this.cBoxMotor1.Text = "Motor 1";
            this.cBoxMotor1.UseVisualStyleBackColor = true;
            this.cBoxMotor1.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxFlow1
            // 
            this.cBoxFlow1.AutoSize = true;
            this.cBoxFlow1.Checked = true;
            this.cBoxFlow1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxFlow1.Location = new System.Drawing.Point(247, 17);
            this.cBoxFlow1.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxFlow1.Name = "cBoxFlow1";
            this.cBoxFlow1.Size = new System.Drawing.Size(115, 21);
            this.cBoxFlow1.TabIndex = 15;
            this.cBoxFlow1.Text = "Flow 1: [sccm]";
            this.cBoxFlow1.UseVisualStyleBackColor = true;
            this.cBoxFlow1.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxFlow3
            // 
            this.cBoxFlow3.AutoSize = true;
            this.cBoxFlow3.Checked = true;
            this.cBoxFlow3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxFlow3.Location = new System.Drawing.Point(494, 17);
            this.cBoxFlow3.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxFlow3.Name = "cBoxFlow3";
            this.cBoxFlow3.Size = new System.Drawing.Size(115, 21);
            this.cBoxFlow3.TabIndex = 14;
            this.cBoxFlow3.Text = "Flow 3: [sccm]";
            this.cBoxFlow3.UseVisualStyleBackColor = true;
            this.cBoxFlow3.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxFlow2
            // 
            this.cBoxFlow2.AutoSize = true;
            this.cBoxFlow2.Checked = true;
            this.cBoxFlow2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxFlow2.Location = new System.Drawing.Point(370, 17);
            this.cBoxFlow2.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxFlow2.Name = "cBoxFlow2";
            this.cBoxFlow2.Size = new System.Drawing.Size(115, 21);
            this.cBoxFlow2.TabIndex = 13;
            this.cBoxFlow2.Text = "Flow 2: [sccm]";
            this.cBoxFlow2.UseVisualStyleBackColor = true;
            this.cBoxFlow2.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxPressure
            // 
            this.cBoxPressure.AutoSize = true;
            this.cBoxPressure.Checked = true;
            this.cBoxPressure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxPressure.Location = new System.Drawing.Point(7, 17);
            this.cBoxPressure.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxPressure.Name = "cBoxPressure";
            this.cBoxPressure.Size = new System.Drawing.Size(133, 21);
            this.cBoxPressure.TabIndex = 12;
            this.cBoxPressure.Text = "Pressure: [mBar]";
            this.cBoxPressure.UseVisualStyleBackColor = true;
            this.cBoxPressure.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxPower
            // 
            this.cBoxPower.AutoSize = true;
            this.cBoxPower.Checked = true;
            this.cBoxPower.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxPower.Location = new System.Drawing.Point(146, 17);
            this.cBoxPower.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxPower.Name = "cBoxPower";
            this.cBoxPower.Size = new System.Drawing.Size(95, 21);
            this.cBoxPower.TabIndex = 11;
            this.cBoxPower.Text = "Power: [W]";
            this.cBoxPower.UseVisualStyleBackColor = true;
            this.cBoxPower.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxVoltage
            // 
            this.cBoxVoltage.AutoSize = true;
            this.cBoxVoltage.Checked = true;
            this.cBoxVoltage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxVoltage.Location = new System.Drawing.Point(97, 561);
            this.cBoxVoltage.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxVoltage.Name = "cBoxVoltage";
            this.cBoxVoltage.Size = new System.Drawing.Size(100, 21);
            this.cBoxVoltage.TabIndex = 17;
            this.cBoxVoltage.Text = "Voltage: [V]";
            this.cBoxVoltage.UseVisualStyleBackColor = true;
            this.cBoxVoltage.Visible = false;
            this.cBoxVoltage.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxCurent
            // 
            this.cBoxCurent.AutoSize = true;
            this.cBoxCurent.Checked = true;
            this.cBoxCurent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxCurent.Location = new System.Drawing.Point(5, 561);
            this.cBoxCurent.Margin = new System.Windows.Forms.Padding(8, 2, 2, 2);
            this.cBoxCurent.Name = "cBoxCurent";
            this.cBoxCurent.Size = new System.Drawing.Size(86, 21);
            this.cBoxCurent.TabIndex = 16;
            this.cBoxCurent.Text = "Curent[A]";
            this.cBoxCurent.UseVisualStyleBackColor = true;
            this.cBoxCurent.Visible = false;
            this.cBoxCurent.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1019, 605);
            this.tabControl.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chart);
            this.tabPage1.Controls.Add(this.grBoxSeries);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1011, 575);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chart";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtBoxSummary);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1011, 575);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Summary";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtBoxSummary
            // 
            this.txtBoxSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxSummary.Location = new System.Drawing.Point(2, 2);
            this.txtBoxSummary.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxSummary.Name = "txtBoxSummary";
            this.txtBoxSummary.Size = new System.Drawing.Size(1007, 571);
            this.txtBoxSummary.TabIndex = 0;
            this.txtBoxSummary.Text = "";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "pdf";
            this.saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.CheckIfFileHasCorrectExtension);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // ArchivePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cBoxVoltage);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cBoxCurent);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ArchivePanel";
            this.Size = new System.Drawing.Size(1019, 632);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.grBoxSeries.ResumeLayout(false);
            this.grBoxSeries.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.GroupBox grBoxSeries;
        private System.Windows.Forms.CheckBox cBoxVoltage;
        private System.Windows.Forms.CheckBox cBoxCurent;
        private System.Windows.Forms.CheckBox cBoxFlow1;
        private System.Windows.Forms.CheckBox cBoxFlow3;
        private System.Windows.Forms.CheckBox cBoxFlow2;
        private System.Windows.Forms.CheckBox cBoxPressure;
        private System.Windows.Forms.CheckBox cBoxPower;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox cBoxDosingValve;
        private System.Windows.Forms.CheckBox cBoxMotor2;
        private System.Windows.Forms.CheckBox cBoxMotor1;
        private System.Windows.Forms.ToolStripLabel labSummaryProces;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton btnLoadData;
        private System.Windows.Forms.RichTextBox txtBoxSummary;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripDropDownButton dropDownBtnExportToFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemPDF;
        private System.Windows.Forms.ToolStripMenuItem menuItemCSV;
        private System.Windows.Forms.ToolStripButton brnRemoveData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
