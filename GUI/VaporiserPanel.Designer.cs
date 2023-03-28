namespace HPT1000.GUI
{
    partial class VaporiserPanel
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
            this.cBoxGasControl = new System.Windows.Forms.CheckBox();
            this.btnUpDownDosing = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.btnUpDownCycleTime = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.btnUpDownOnTime = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.dEditDosing = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.labUnitDosing = new System.Windows.Forms.Label();
            this.labDosing = new System.Windows.Forms.Label();
            this.scrolDosing = new System.Windows.Forms.HScrollBar();
            this.dEditOnTime = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditCycleTImne = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.labCycle = new System.Windows.Forms.Label();
            this.labUnit = new System.Windows.Forms.Label();
            this.labUnitCycle = new System.Windows.Forms.Label();
            this.labOnTime = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxGasControl);
            this.groupBox1.Controls.Add(this.btnUpDownDosing);
            this.groupBox1.Controls.Add(this.btnUpDownCycleTime);
            this.groupBox1.Controls.Add(this.btnUpDownOnTime);
            this.groupBox1.Controls.Add(this.dEditDosing);
            this.groupBox1.Controls.Add(this.labUnitDosing);
            this.groupBox1.Controls.Add(this.labDosing);
            this.groupBox1.Controls.Add(this.scrolDosing);
            this.groupBox1.Controls.Add(this.dEditOnTime);
            this.groupBox1.Controls.Add(this.dEditCycleTImne);
            this.groupBox1.Controls.Add(this.labCycle);
            this.groupBox1.Controls.Add(this.labUnit);
            this.groupBox1.Controls.Add(this.labUnitCycle);
            this.groupBox1.Controls.Add(this.labOnTime);
            this.groupBox1.Controls.Add(this.label74);
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(196, 100);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // cBoxGasControl
            // 
            this.cBoxGasControl.Appearance = System.Windows.Forms.Appearance.Button;
            this.cBoxGasControl.AutoSize = true;
            this.cBoxGasControl.Image = global::HPT1000.Properties.Resources.interlock_OFF;
            this.cBoxGasControl.Location = new System.Drawing.Point(165, 66);
            this.cBoxGasControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxGasControl.Name = "cBoxGasControl";
            this.cBoxGasControl.Size = new System.Drawing.Size(29, 29);
            this.cBoxGasControl.TabIndex = 67;
            this.cBoxGasControl.UseVisualStyleBackColor = true;
            this.cBoxGasControl.Click += new System.EventHandler(this.cBoxGasControl_Click);
            // 
            // btnUpDownDosing
            // 
            this.btnUpDownDosing.HeightComponent = 32;
            this.btnUpDownDosing.Location = new System.Drawing.Point(116, 42);
            this.btnUpDownDosing.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpDownDosing.Maximum = 1000000D;
            this.btnUpDownDosing.Minimum = 0D;
            this.btnUpDownDosing.Name = "btnUpDownDosing";
            this.btnUpDownDosing.Size = new System.Drawing.Size(20, 32);
            this.btnUpDownDosing.Step = 1D;
            this.btnUpDownDosing.TabIndex = 81;
            this.btnUpDownDosing.Value = 0D;
            this.btnUpDownDosing.WidthComponent = 20;
            this.btnUpDownDosing.ValueChanged += new System.EventHandler(this.btnUpDownDosing_ValueChanged);
            // 
            // btnUpDownCycleTime
            // 
            this.btnUpDownCycleTime.HeightComponent = 32;
            this.btnUpDownCycleTime.Location = new System.Drawing.Point(123, 38);
            this.btnUpDownCycleTime.Maximum = 1000000D;
            this.btnUpDownCycleTime.Minimum = 0D;
            this.btnUpDownCycleTime.Name = "btnUpDownCycleTime";
            this.btnUpDownCycleTime.Size = new System.Drawing.Size(22, 32);
            this.btnUpDownCycleTime.Step = 0.001D;
            this.btnUpDownCycleTime.TabIndex = 80;
            this.btnUpDownCycleTime.Value = 0D;
            this.btnUpDownCycleTime.WidthComponent = 22;
            this.btnUpDownCycleTime.ValueChanged += new System.EventHandler(this.btnUpDownCycleTime_ValueChanged);
            // 
            // btnUpDownOnTime
            // 
            this.btnUpDownOnTime.HeightComponent = 28;
            this.btnUpDownOnTime.Location = new System.Drawing.Point(122, 68);
            this.btnUpDownOnTime.Maximum = 1000000D;
            this.btnUpDownOnTime.Minimum = 0D;
            this.btnUpDownOnTime.Name = "btnUpDownOnTime";
            this.btnUpDownOnTime.Size = new System.Drawing.Size(21, 28);
            this.btnUpDownOnTime.Step = 1D;
            this.btnUpDownOnTime.TabIndex = 79;
            this.btnUpDownOnTime.Value = 0D;
            this.btnUpDownOnTime.WidthComponent = 21;
            this.btnUpDownOnTime.ValueChanged += new System.EventHandler(this.btnUpDownOnTime_ValueChanged);
            // 
            // dEditDosing
            // 
            this.dEditDosing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditDosing.Location = new System.Drawing.Point(64, 44);
            this.dEditDosing.Mask = "0";
            this.dEditDosing.MaximumValue = 1000D;
            this.dEditDosing.MinimumValue = 0D;
            this.dEditDosing.Name = "dEditDosing";
            this.dEditDosing.ReadOnly = false;
            this.dEditDosing.Size = new System.Drawing.Size(53, 23);
            this.dEditDosing.TabIndex = 78;
            this.dEditDosing.Value = 0D;
            this.dEditDosing.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditDosing_EnterOn);
            // 
            // labUnitDosing
            // 
            this.labUnitDosing.AutoSize = true;
            this.labUnitDosing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labUnitDosing.ForeColor = System.Drawing.Color.Green;
            this.labUnitDosing.Location = new System.Drawing.Point(138, 47);
            this.labUnitDosing.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labUnitDosing.Name = "labUnitDosing";
            this.labUnitDosing.Size = new System.Drawing.Size(51, 15);
            this.labUnitDosing.TabIndex = 77;
            this.labUnitDosing.Text = "[uL/min]";
            // 
            // labDosing
            // 
            this.labDosing.AutoSize = true;
            this.labDosing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labDosing.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labDosing.Location = new System.Drawing.Point(2, 47);
            this.labDosing.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labDosing.Name = "labDosing";
            this.labDosing.Size = new System.Drawing.Size(49, 15);
            this.labDosing.TabIndex = 76;
            this.labDosing.Text = "Dosing:";
            // 
            // scrolDosing
            // 
            this.scrolDosing.Location = new System.Drawing.Point(10, 118);
            this.scrolDosing.Maximum = 1000;
            this.scrolDosing.Name = "scrolDosing";
            this.scrolDosing.Size = new System.Drawing.Size(294, 29);
            this.scrolDosing.TabIndex = 75;
            this.scrolDosing.ValueChanged += new System.EventHandler(this.scrolDosing_ValueChanged);
            // 
            // dEditOnTime
            // 
            this.dEditOnTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditOnTime.Location = new System.Drawing.Point(66, 72);
            this.dEditOnTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dEditOnTime.Mask = "0";
            this.dEditOnTime.MaximumValue = 100000D;
            this.dEditOnTime.MinimumValue = 0D;
            this.dEditOnTime.Name = "dEditOnTime";
            this.dEditOnTime.ReadOnly = false;
            this.dEditOnTime.Size = new System.Drawing.Size(49, 22);
            this.dEditOnTime.TabIndex = 74;
            this.dEditOnTime.Value = 123D;
            this.dEditOnTime.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditOnTime_EnterOn);
            // 
            // dEditCycleTImne
            // 
            this.dEditCycleTImne.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditCycleTImne.Location = new System.Drawing.Point(66, 42);
            this.dEditCycleTImne.Mask = "0.000";
            this.dEditCycleTImne.MaximumValue = 30D;
            this.dEditCycleTImne.MinimumValue = 0D;
            this.dEditCycleTImne.Name = "dEditCycleTImne";
            this.dEditCycleTImne.ReadOnly = false;
            this.dEditCycleTImne.Size = new System.Drawing.Size(57, 21);
            this.dEditCycleTImne.TabIndex = 73;
            this.dEditCycleTImne.Value = 30D;
            this.dEditCycleTImne.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditCycleTImne_EnterOn);
            // 
            // labCycle
            // 
            this.labCycle.AutoSize = true;
            this.labCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labCycle.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labCycle.Location = new System.Drawing.Point(2, 42);
            this.labCycle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labCycle.Name = "labCycle";
            this.labCycle.Size = new System.Drawing.Size(66, 15);
            this.labCycle.TabIndex = 70;
            this.labCycle.Text = "Cycle time:";
            // 
            // labUnit
            // 
            this.labUnit.AutoSize = true;
            this.labUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labUnit.ForeColor = System.Drawing.Color.Green;
            this.labUnit.Location = new System.Drawing.Point(144, 72);
            this.labUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labUnit.Name = "labUnit";
            this.labUnit.Size = new System.Drawing.Size(24, 15);
            this.labUnit.TabIndex = 67;
            this.labUnit.Text = "[%]";
            this.labUnit.Click += new System.EventHandler(this.labUnit_Click);
            this.labUnit.MouseLeave += new System.EventHandler(this.labUnit_MouseLeave);
            this.labUnit.MouseHover += new System.EventHandler(this.labUnit_MouseHover);
            // 
            // labUnitCycle
            // 
            this.labUnitCycle.AutoSize = true;
            this.labUnitCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labUnitCycle.ForeColor = System.Drawing.Color.Green;
            this.labUnitCycle.Location = new System.Drawing.Point(144, 42);
            this.labUnitCycle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labUnitCycle.Name = "labUnitCycle";
            this.labUnitCycle.Size = new System.Drawing.Size(32, 15);
            this.labUnitCycle.TabIndex = 66;
            this.labUnitCycle.Text = "[sec]";
            // 
            // labOnTime
            // 
            this.labOnTime.AutoSize = true;
            this.labOnTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labOnTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labOnTime.Location = new System.Drawing.Point(2, 74);
            this.labOnTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labOnTime.Name = "labOnTime";
            this.labOnTime.Size = new System.Drawing.Size(55, 15);
            this.labOnTime.TabIndex = 63;
            this.labOnTime.Text = "ON time:";
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.Gray;
            this.label74.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label74.Dock = System.Windows.Forms.DockStyle.Top;
            this.label74.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label74.ForeColor = System.Drawing.Color.White;
            this.label74.Location = new System.Drawing.Point(2, 18);
            this.label74.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(192, 20);
            this.label74.TabIndex = 60;
            this.label74.Text = "Vaporiser";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VaporiserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "VaporiserPanel";
            this.Size = new System.Drawing.Size(248, 126);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labUnit;
        private System.Windows.Forms.Label labUnitCycle;
        private System.Windows.Forms.Label labOnTime;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label labCycle;
        private Cotrols.DoubleEdit dEditCycleTImne;
        private Cotrols.DoubleEdit dEditOnTime;
        private Cotrols.DoubleEdit dEditDosing;
        private System.Windows.Forms.Label labUnitDosing;
        private System.Windows.Forms.Label labDosing;
        private System.Windows.Forms.HScrollBar scrolDosing;
        private Cotrols.ButtonUpDown btnUpDownCycleTime;
        private Cotrols.ButtonUpDown btnUpDownOnTime;
        private Cotrols.ButtonUpDown btnUpDownDosing;
        private System.Windows.Forms.CheckBox cBoxGasControl;
    }
}
