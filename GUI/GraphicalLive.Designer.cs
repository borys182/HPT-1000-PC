namespace HPT1000.GUI
{
    partial class GraphicalLive
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-1D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 60D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 10D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 85D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 20D);
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 1D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 1D);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicalLive));
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grBoxSeries = new System.Windows.Forms.GroupBox();
            this.cBoxDosingValve = new System.Windows.Forms.CheckBox();
            this.cBoxMotor1 = new System.Windows.Forms.CheckBox();
            this.cBoxMotor2 = new System.Windows.Forms.CheckBox();
            this.cBoxFlow1 = new System.Windows.Forms.CheckBox();
            this.cBoxFlow3 = new System.Windows.Forms.CheckBox();
            this.cBoxFlow2 = new System.Windows.Forms.CheckBox();
            this.cBoxPressure = new System.Windows.Forms.CheckBox();
            this.cBoxPower = new System.Windows.Forms.CheckBox();
            this.cBoxVoltage = new System.Windows.Forms.CheckBox();
            this.cBoxCurent = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnZoomReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.grBoxSeries.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            chartArea1.Area3DStyle.Inclination = 15;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisX.Title = "time [s]";
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY.Title = "Pressure [mBar]";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Near;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea1.BorderColor = System.Drawing.Color.BlanchedAlmond;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.LineColor = System.Drawing.Color.MidnightBlue;
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
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisX.MajorGrid.Interval = 0D;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisX.MinorGrid.Enabled = true;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea2.AxisY.Interval = 1D;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.MajorGrid.Interval = 1D;
            chartArea2.AxisY.MajorGrid.IntervalOffset = 1D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.Maximum = 1D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.MinorGrid.Enabled = true;
            chartArea2.AxisY.MinorGrid.Interval = 1D;
            chartArea2.AxisY.MinorGrid.IntervalOffset = 1D;
            chartArea2.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.Title = "Motor";
            chartArea2.AxisY.TitleAlignment = System.Drawing.StringAlignment.Near;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea2.BorderColor = System.Drawing.Color.BlanchedAlmond;
            chartArea2.CursorY.LineColor = System.Drawing.Color.MidnightBlue;
            chartArea2.Name = "ChartAreaMotor";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 13F;
            chartArea2.Position.Width = 79F;
            chartArea2.Position.X = 20F;
            chartArea2.Position.Y = 1F;
            chartArea2.ShadowColor = System.Drawing.Color.Transparent;
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(-2, 30);
            this.chart.Margin = new System.Windows.Forms.Padding(2);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartAreaPressure";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            series2.ChartArea = "ChartAreaPressure";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series2";
            dataPoint4.MarkerBorderColor = System.Drawing.Color.Black;
            dataPoint4.MarkerBorderWidth = 1;
            dataPoint4.MarkerColor = System.Drawing.Color.Red;
            dataPoint4.MarkerImage = "";
            dataPoint4.MarkerSize = 10;
            dataPoint4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series2.Points.Add(dataPoint1);
            series2.Points.Add(dataPoint2);
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series3.ChartArea = "ChartAreaMotor";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series3";
            series3.Points.Add(dataPoint6);
            series3.Points.Add(dataPoint7);
            series3.Points.Add(dataPoint8);
            series3.Points.Add(dataPoint9);
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(779, 648);
            this.chart.TabIndex = 0;
            this.chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDown);
            this.chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_MouseMove);
            this.chart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_MouseUp);
            // 
            // grBoxSeries
            // 
            this.grBoxSeries.Controls.Add(this.cBoxDosingValve);
            this.grBoxSeries.Controls.Add(this.cBoxMotor1);
            this.grBoxSeries.Controls.Add(this.cBoxMotor2);
            this.grBoxSeries.Controls.Add(this.cBoxFlow1);
            this.grBoxSeries.Controls.Add(this.cBoxFlow3);
            this.grBoxSeries.Controls.Add(this.cBoxFlow2);
            this.grBoxSeries.Controls.Add(this.cBoxPressure);
            this.grBoxSeries.Controls.Add(this.cBoxPower);
            this.grBoxSeries.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grBoxSeries.Location = new System.Drawing.Point(0, 676);
            this.grBoxSeries.Margin = new System.Windows.Forms.Padding(2);
            this.grBoxSeries.Name = "grBoxSeries";
            this.grBoxSeries.Padding = new System.Windows.Forms.Padding(2);
            this.grBoxSeries.Size = new System.Drawing.Size(782, 79);
            this.grBoxSeries.TabIndex = 11;
            this.grBoxSeries.TabStop = false;
            this.grBoxSeries.Text = "Series";
            // 
            // cBoxDosingValve
            // 
            this.cBoxDosingValve.AutoSize = true;
            this.cBoxDosingValve.Checked = true;
            this.cBoxDosingValve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxDosingValve.Location = new System.Drawing.Point(314, 49);
            this.cBoxDosingValve.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxDosingValve.Name = "cBoxDosingValve";
            this.cBoxDosingValve.Size = new System.Drawing.Size(127, 24);
            this.cBoxDosingValve.TabIndex = 18;
            this.cBoxDosingValve.Text = "Dosing valve";
            this.cBoxDosingValve.UseVisualStyleBackColor = true;
            this.cBoxDosingValve.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxMotor1
            // 
            this.cBoxMotor1.AutoSize = true;
            this.cBoxMotor1.Checked = true;
            this.cBoxMotor1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxMotor1.Location = new System.Drawing.Point(12, 49);
            this.cBoxMotor1.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxMotor1.Name = "cBoxMotor1";
            this.cBoxMotor1.Size = new System.Drawing.Size(88, 24);
            this.cBoxMotor1.TabIndex = 17;
            this.cBoxMotor1.Text = "Motor 1";
            this.cBoxMotor1.UseVisualStyleBackColor = true;
            this.cBoxMotor1.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxMotor2
            // 
            this.cBoxMotor2.AutoSize = true;
            this.cBoxMotor2.Checked = true;
            this.cBoxMotor2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxMotor2.Location = new System.Drawing.Point(185, 49);
            this.cBoxMotor2.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxMotor2.Name = "cBoxMotor2";
            this.cBoxMotor2.Size = new System.Drawing.Size(88, 24);
            this.cBoxMotor2.TabIndex = 16;
            this.cBoxMotor2.Text = "Motor 2";
            this.cBoxMotor2.UseVisualStyleBackColor = true;
            this.cBoxMotor2.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // cBoxFlow1
            // 
            this.cBoxFlow1.AutoSize = true;
            this.cBoxFlow1.Checked = true;
            this.cBoxFlow1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxFlow1.Location = new System.Drawing.Point(314, 21);
            this.cBoxFlow1.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxFlow1.Name = "cBoxFlow1";
            this.cBoxFlow1.Size = new System.Drawing.Size(141, 24);
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
            this.cBoxFlow3.Location = new System.Drawing.Point(636, 21);
            this.cBoxFlow3.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxFlow3.Name = "cBoxFlow3";
            this.cBoxFlow3.Size = new System.Drawing.Size(141, 24);
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
            this.cBoxFlow2.Location = new System.Drawing.Point(480, 21);
            this.cBoxFlow2.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxFlow2.Name = "cBoxFlow2";
            this.cBoxFlow2.Size = new System.Drawing.Size(141, 24);
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
            this.cBoxPressure.Location = new System.Drawing.Point(12, 21);
            this.cBoxPressure.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxPressure.Name = "cBoxPressure";
            this.cBoxPressure.Size = new System.Drawing.Size(160, 24);
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
            this.cBoxPower.Location = new System.Drawing.Point(185, 21);
            this.cBoxPower.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxPower.Name = "cBoxPower";
            this.cBoxPower.Size = new System.Drawing.Size(114, 24);
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
            this.cBoxVoltage.Location = new System.Drawing.Point(12, 436);
            this.cBoxVoltage.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxVoltage.Name = "cBoxVoltage";
            this.cBoxVoltage.Size = new System.Drawing.Size(118, 24);
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
            this.cBoxCurent.Location = new System.Drawing.Point(10, 479);
            this.cBoxCurent.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.cBoxCurent.Name = "cBoxCurent";
            this.cBoxCurent.Size = new System.Drawing.Size(102, 24);
            this.cBoxCurent.TabIndex = 16;
            this.cBoxCurent.Text = "Curent[A]";
            this.cBoxCurent.UseVisualStyleBackColor = true;
            this.cBoxCurent.Visible = false;
            this.cBoxCurent.CheckedChanged += new System.EventHandler(this.cBox_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnClear,
            this.toolStripSeparator3,
            this.toolStripBtnZoomReset,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(782, 27);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnClear
            // 
            this.toolStripBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnClear.Image = global::HPT1000.Properties.Resources.ClearChart;
            this.toolStripBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnClear.Name = "toolStripBtnClear";
            this.toolStripBtnClear.Size = new System.Drawing.Size(24, 24);
            this.toolStripBtnClear.Text = "toolStripButton1";
            this.toolStripBtnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripBtnZoomReset
            // 
            this.toolStripBtnZoomReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnZoomReset.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnZoomReset.Image")));
            this.toolStripBtnZoomReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnZoomReset.Name = "toolStripBtnZoomReset";
            this.toolStripBtnZoomReset.Size = new System.Drawing.Size(83, 24);
            this.toolStripBtnZoomReset.Text = "Shift Reset";
            this.toolStripBtnZoomReset.Click += new System.EventHandler(this.toolStripBtnZoomReset_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // GraphicalLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cBoxCurent);
            this.Controls.Add(this.cBoxVoltage);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grBoxSeries);
            this.Controls.Add(this.chart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "GraphicalLive";
            this.Size = new System.Drawing.Size(782, 755);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.grBoxSeries.ResumeLayout(false);
            this.grBoxSeries.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.GroupBox grBoxSeries;
        private System.Windows.Forms.CheckBox cBoxVoltage;
        private System.Windows.Forms.CheckBox cBoxCurent;
        private System.Windows.Forms.CheckBox cBoxFlow1;
        private System.Windows.Forms.CheckBox cBoxFlow3;
        private System.Windows.Forms.CheckBox cBoxFlow2;
        private System.Windows.Forms.CheckBox cBoxPressure;
        private System.Windows.Forms.CheckBox cBoxPower;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripBtnZoomReset;
        private System.Windows.Forms.CheckBox cBoxDosingValve;
        private System.Windows.Forms.CheckBox cBoxMotor1;
        private System.Windows.Forms.CheckBox cBoxMotor2;
    }
}
