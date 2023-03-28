namespace HPT1000.GUI
{
    partial class MFCPanel
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
            this.cBoxGasType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxControlGas = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpDownFlowPercent = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.btnUpDownFlowSccm = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.labNameMFC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dEditFlow_percent = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow_sccm = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.scrollFlow = new System.Windows.Forms.HScrollBar();
            this.label52 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBoxGasType
            // 
            this.cBoxGasType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxGasType.FormattingEnabled = true;
            this.cBoxGasType.Location = new System.Drawing.Point(175, 95);
            this.cBoxGasType.Name = "cBoxGasType";
            this.cBoxGasType.Size = new System.Drawing.Size(65, 26);
            this.cBoxGasType.TabIndex = 68;
            this.cBoxGasType.SelectedIndexChanged += new System.EventHandler(this.cBoxGasType_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxControlGas);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnUpDownFlowPercent);
            this.groupBox1.Controls.Add(this.btnUpDownFlowSccm);
            this.groupBox1.Controls.Add(this.labNameMFC);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dEditFlow_percent);
            this.groupBox1.Controls.Add(this.dEditFlow_sccm);
            this.groupBox1.Controls.Add(this.scrollFlow);
            this.groupBox1.Controls.Add(this.label52);
            this.groupBox1.Controls.Add(this.label50);
            this.groupBox1.Controls.Add(this.label48);
            this.groupBox1.Controls.Add(this.cBoxGasType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 132);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            // 
            // cBoxControlGas
            // 
            this.cBoxControlGas.Appearance = System.Windows.Forms.Appearance.Button;
            this.cBoxControlGas.AutoSize = true;
            this.cBoxControlGas.Image = global::HPT1000.Properties.Resources.interlock_OFF;
            this.cBoxControlGas.Location = new System.Drawing.Point(3, 87);
            this.cBoxControlGas.Name = "cBoxControlGas";
            this.cBoxControlGas.Size = new System.Drawing.Size(29, 29);
            this.cBoxControlGas.TabIndex = 92;
            this.cBoxControlGas.UseVisualStyleBackColor = true;
            this.cBoxControlGas.Click += new System.EventHandler(this.cBoxControlGas_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(185, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 18);
            this.label3.TabIndex = 91;
            this.label3.Text = "Gas:";
            // 
            // btnUpDownFlowPercent
            // 
            this.btnUpDownFlowPercent.HeightComponent = 39;
            this.btnUpDownFlowPercent.Location = new System.Drawing.Point(103, 91);
            this.btnUpDownFlowPercent.Margin = new System.Windows.Forms.Padding(5);
            this.btnUpDownFlowPercent.Maximum = 100D;
            this.btnUpDownFlowPercent.Minimum = 0D;
            this.btnUpDownFlowPercent.Name = "btnUpDownFlowPercent";
            this.btnUpDownFlowPercent.Size = new System.Drawing.Size(27, 39);
            this.btnUpDownFlowPercent.Step = 0.1D;
            this.btnUpDownFlowPercent.TabIndex = 90;
            this.btnUpDownFlowPercent.Value = 0D;
            this.btnUpDownFlowPercent.WidthComponent = 27;
            this.btnUpDownFlowPercent.ValueChanged += new System.EventHandler(this.btnUpDownFlowPercent_ValueChanged);
            // 
            // btnUpDownFlowSccm
            // 
            this.btnUpDownFlowSccm.HeightComponent = 36;
            this.btnUpDownFlowSccm.Location = new System.Drawing.Point(105, 51);
            this.btnUpDownFlowSccm.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpDownFlowSccm.Maximum = 100D;
            this.btnUpDownFlowSccm.Minimum = 0D;
            this.btnUpDownFlowSccm.Name = "btnUpDownFlowSccm";
            this.btnUpDownFlowSccm.Size = new System.Drawing.Size(26, 36);
            this.btnUpDownFlowSccm.Step = 1D;
            this.btnUpDownFlowSccm.TabIndex = 89;
            this.btnUpDownFlowSccm.Value = 0D;
            this.btnUpDownFlowSccm.WidthComponent = 26;
            this.btnUpDownFlowSccm.ValueChanged += new System.EventHandler(this.btnUpDownFlowSccm_ValueChanged);
            // 
            // labNameMFC
            // 
            this.labNameMFC.BackColor = System.Drawing.Color.Gray;
            this.labNameMFC.Dock = System.Windows.Forms.DockStyle.Top;
            this.labNameMFC.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labNameMFC.ForeColor = System.Drawing.Color.White;
            this.labNameMFC.Location = new System.Drawing.Point(3, 23);
            this.labNameMFC.Name = "labNameMFC";
            this.labNameMFC.Size = new System.Drawing.Size(243, 24);
            this.labNameMFC.TabIndex = 88;
            this.labNameMFC.Text = "Mass Flow Controller 1";
            this.labNameMFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 87;
            this.label1.Text = "label1";
            // 
            // dEditFlow_percent
            // 
            this.dEditFlow_percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditFlow_percent.Location = new System.Drawing.Point(41, 95);
            this.dEditFlow_percent.Margin = new System.Windows.Forms.Padding(5);
            this.dEditFlow_percent.Mask = "0.00";
            this.dEditFlow_percent.MaximumValue = 100D;
            this.dEditFlow_percent.MinimumValue = 0D;
            this.dEditFlow_percent.Name = "dEditFlow_percent";
            this.dEditFlow_percent.ReadOnly = false;
            this.dEditFlow_percent.Size = new System.Drawing.Size(62, 26);
            this.dEditFlow_percent.TabIndex = 84;
            this.dEditFlow_percent.Value = 0D;
            this.dEditFlow_percent.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow_percent_EnterOn);
            // 
            // dEditFlow_sccm
            // 
            this.dEditFlow_sccm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditFlow_sccm.Location = new System.Drawing.Point(41, 55);
            this.dEditFlow_sccm.Margin = new System.Windows.Forms.Padding(4);
            this.dEditFlow_sccm.Mask = "0";
            this.dEditFlow_sccm.MaximumValue = 1000000D;
            this.dEditFlow_sccm.MinimumValue = 0D;
            this.dEditFlow_sccm.Name = "dEditFlow_sccm";
            this.dEditFlow_sccm.ReadOnly = false;
            this.dEditFlow_sccm.Size = new System.Drawing.Size(64, 26);
            this.dEditFlow_sccm.TabIndex = 82;
            this.dEditFlow_sccm.Value = 10000D;
            this.dEditFlow_sccm.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow_sccm_EnterOn);
            // 
            // scrollFlow
            // 
            this.scrollFlow.Location = new System.Drawing.Point(14, 134);
            this.scrollFlow.Name = "scrollFlow";
            this.scrollFlow.Size = new System.Drawing.Size(357, 21);
            this.scrollFlow.TabIndex = 81;
            this.scrollFlow.ValueChanged += new System.EventHandler(this.scrollFlow_ValueChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label52.ForeColor = System.Drawing.Color.Green;
            this.label52.Location = new System.Drawing.Point(130, 98);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(29, 18);
            this.label52.TabIndex = 78;
            this.label52.Text = "[%]";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label50.ForeColor = System.Drawing.Color.Green;
            this.label50.Location = new System.Drawing.Point(130, 56);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(53, 18);
            this.label50.TabIndex = 75;
            this.label50.Text = "[sccm]";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label48.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label48.Location = new System.Drawing.Point(2, 56);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(44, 18);
            this.label48.TabIndex = 71;
            this.label48.Text = "Flow:";
            // 
            // MFCPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MFCPanel";
            this.Size = new System.Drawing.Size(249, 144);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cBoxGasType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.HScrollBar scrollFlow;
        private Cotrols.DoubleEdit dEditFlow_percent;
        private Cotrols.DoubleEdit dEditFlow_sccm;
        private System.Windows.Forms.Label labNameMFC;
        private System.Windows.Forms.Label label1;
        private Cotrols.ButtonUpDown btnUpDownFlowPercent;
        private Cotrols.ButtonUpDown btnUpDownFlowSccm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cBoxControlGas;
    }
}
