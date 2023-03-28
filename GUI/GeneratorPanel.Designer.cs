namespace HPT1000.GUI
{
    partial class GeneratorPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labVoltage = new System.Windows.Forms.Label();
            this.labCurent = new System.Windows.Forms.Label();
            this.labPower = new System.Windows.Forms.Label();
            this.scrollSetpoint = new System.Windows.Forms.HScrollBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.labUnitSP = new System.Windows.Forms.Label();
            this.cBoxOperate = new System.Windows.Forms.CheckBox();
            this.buttonUpDown = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.dEditSetpoint = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.labVoltage);
            this.groupBox1.Controls.Add(this.labCurent);
            this.groupBox1.Controls.Add(this.labPower);
            this.groupBox1.Controls.Add(this.buttonUpDown);
            this.groupBox1.Controls.Add(this.scrollSetpoint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.dEditSetpoint);
            this.groupBox1.Controls.Add(this.labUnitSP);
            this.groupBox1.Controls.Add(this.cBoxOperate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 226);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            // 
            // labVoltage
            // 
            this.labVoltage.BackColor = System.Drawing.Color.Gray;
            this.labVoltage.Font = new System.Drawing.Font("Roboto", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labVoltage.ForeColor = System.Drawing.Color.Gold;
            this.labVoltage.Location = new System.Drawing.Point(5, 169);
            this.labVoltage.Name = "labVoltage";
            this.labVoltage.Size = new System.Drawing.Size(165, 19);
            this.labVoltage.TabIndex = 88;
            this.labVoltage.Text = "Voltage: 1145.241 V";
            // 
            // labCurent
            // 
            this.labCurent.BackColor = System.Drawing.Color.Gray;
            this.labCurent.Font = new System.Drawing.Font("Roboto", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labCurent.ForeColor = System.Drawing.Color.Gold;
            this.labCurent.Location = new System.Drawing.Point(5, 202);
            this.labCurent.Name = "labCurent";
            this.labCurent.Size = new System.Drawing.Size(165, 19);
            this.labCurent.TabIndex = 87;
            this.labCurent.Text = "Curent:    1.54 A";
            // 
            // labPower
            // 
            this.labPower.BackColor = System.Drawing.Color.Gray;
            this.labPower.Font = new System.Drawing.Font("Roboto", 9.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labPower.ForeColor = System.Drawing.Color.Gold;
            this.labPower.Location = new System.Drawing.Point(5, 137);
            this.labPower.Name = "labPower";
            this.labPower.Size = new System.Drawing.Size(165, 19);
            this.labPower.TabIndex = 86;
            this.labPower.Text = "Power:     45.54 W";
            // 
            // scrollSetpoint
            // 
            this.scrollSetpoint.Location = new System.Drawing.Point(7, 259);
            this.scrollSetpoint.Name = "scrollSetpoint";
            this.scrollSetpoint.Size = new System.Drawing.Size(240, 31);
            this.scrollSetpoint.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gray;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 25);
            this.label2.TabIndex = 81;
            this.label2.Text = "Generator";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label39.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label39.Location = new System.Drawing.Point(5, 60);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(55, 18);
            this.label39.TabIndex = 80;
            this.label39.Text = "Power:";
            // 
            // labUnitSP
            // 
            this.labUnitSP.AutoSize = true;
            this.labUnitSP.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labUnitSP.ForeColor = System.Drawing.Color.Green;
            this.labUnitSP.Location = new System.Drawing.Point(148, 60);
            this.labUnitSP.Name = "labUnitSP";
            this.labUnitSP.Size = new System.Drawing.Size(27, 18);
            this.labUnitSP.TabIndex = 81;
            this.labUnitSP.Text = "[%]";
            // 
            // cBoxOperate
            // 
            this.cBoxOperate.Appearance = System.Windows.Forms.Appearance.Button;
            this.cBoxOperate.AutoSize = true;
            this.cBoxOperate.BackColor = System.Drawing.Color.Transparent;
            this.cBoxOperate.Image = global::HPT1000.Properties.Resources.Bistable_OFF;
            this.cBoxOperate.Location = new System.Drawing.Point(45, 93);
            this.cBoxOperate.Name = "cBoxOperate";
            this.cBoxOperate.Size = new System.Drawing.Size(86, 27);
            this.cBoxOperate.TabIndex = 83;
            this.cBoxOperate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBoxOperate.UseVisualStyleBackColor = false;
            this.cBoxOperate.Click += new System.EventHandler(this.cBoxOperate_Click);
            // 
            // buttonUpDown
            // 
            this.buttonUpDown.HeightComponent = 37;
            this.buttonUpDown.Location = new System.Drawing.Point(120, 52);
            this.buttonUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUpDown.Maximum = 100D;
            this.buttonUpDown.Minimum = 0D;
            this.buttonUpDown.Name = "buttonUpDown";
            this.buttonUpDown.Size = new System.Drawing.Size(26, 37);
            this.buttonUpDown.Step = 0.01D;
            this.buttonUpDown.TabIndex = 85;
            this.buttonUpDown.Value = 0D;
            this.buttonUpDown.WidthComponent = 26;
            this.buttonUpDown.ValueChanged += new System.EventHandler(this.buttonUpDown_ValueChanged);
            // 
            // dEditSetpoint
            // 
            this.dEditSetpoint.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditSetpoint.Location = new System.Drawing.Point(59, 56);
            this.dEditSetpoint.Margin = new System.Windows.Forms.Padding(5);
            this.dEditSetpoint.Mask = "0,01";
            this.dEditSetpoint.MaximumValue = 100D;
            this.dEditSetpoint.MinimumValue = 0D;
            this.dEditSetpoint.Name = "dEditSetpoint";
            this.dEditSetpoint.ReadOnly = false;
            this.dEditSetpoint.Size = new System.Drawing.Size(58, 27);
            this.dEditSetpoint.TabIndex = 84;
            this.dEditSetpoint.Value = 99.99D;
            this.dEditSetpoint.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditSetpoint_EnterOn);
            // 
            // GeneratorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "GeneratorPanel";
            this.Size = new System.Drawing.Size(178, 231);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label39;
        private Cotrols.DoubleEdit dEditSetpoint;
        private System.Windows.Forms.Label labUnitSP;
        private System.Windows.Forms.CheckBox cBoxOperate;
        private System.Windows.Forms.HScrollBar scrollSetpoint;
        private Cotrols.ButtonUpDown buttonUpDown;
        private System.Windows.Forms.Label labVoltage;
        private System.Windows.Forms.Label labCurent;
        private System.Windows.Forms.Label labPower;
    }
}
