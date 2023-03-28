namespace HPT1000.GUI
{
    partial class PressurePanel
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rBtnNone = new System.Windows.Forms.RadioButton();
            this.cBoxAutomaticMode = new System.Windows.Forms.CheckBox();
            this.buttonUpDown = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rBtnVaporaizer = new System.Windows.Forms.RadioButton();
            this.rBtnGases = new System.Windows.Forms.RadioButton();
            this.dEditSetpoint = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.scrollPressure = new System.Windows.Forms.HScrollBar();
            this.label45 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rBtnNone);
            this.groupBox1.Controls.Add(this.cBoxAutomaticMode);
            this.groupBox1.Controls.Add(this.buttonUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rBtnVaporaizer);
            this.groupBox1.Controls.Add(this.rBtnGases);
            this.groupBox1.Controls.Add(this.dEditSetpoint);
            this.groupBox1.Controls.Add(this.scrollPressure);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.label43);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.Transparent;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 148);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            // 
            // rBtnNone
            // 
            this.rBtnNone.AutoSize = true;
            this.rBtnNone.Location = new System.Drawing.Point(324, 102);
            this.rBtnNone.Name = "rBtnNone";
            this.rBtnNone.Size = new System.Drawing.Size(17, 16);
            this.rBtnNone.TabIndex = 97;
            this.rBtnNone.TabStop = true;
            this.rBtnNone.UseVisualStyleBackColor = true;
            this.rBtnNone.Visible = false;
            // 
            // cBoxAutomaticMode
            // 
            this.cBoxAutomaticMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.cBoxAutomaticMode.AutoSize = true;
            this.cBoxAutomaticMode.Image = global::HPT1000.Properties.Resources.Bistable_OFF;
            this.cBoxAutomaticMode.Location = new System.Drawing.Point(9, 94);
            this.cBoxAutomaticMode.Name = "cBoxAutomaticMode";
            this.cBoxAutomaticMode.Size = new System.Drawing.Size(86, 27);
            this.cBoxAutomaticMode.TabIndex = 96;
            this.cBoxAutomaticMode.UseVisualStyleBackColor = true;
            this.cBoxAutomaticMode.Click += new System.EventHandler(this.cBoxGases_Click);
            // 
            // buttonUpDown
            // 
            this.buttonUpDown.HeightComponent = 35;
            this.buttonUpDown.Location = new System.Drawing.Point(284, 55);
            this.buttonUpDown.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUpDown.Maximum = 1000D;
            this.buttonUpDown.Minimum = 0D;
            this.buttonUpDown.Name = "buttonUpDown";
            this.buttonUpDown.Size = new System.Drawing.Size(25, 35);
            this.buttonUpDown.Step = 0.01D;
            this.buttonUpDown.TabIndex = 94;
            this.buttonUpDown.Value = 0D;
            this.buttonUpDown.WidthComponent = 25;
            this.buttonUpDown.ValueChanged += new System.EventHandler(this.buttonUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 18);
            this.label2.TabIndex = 93;
            this.label2.Text = "Automatic Mode";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 29);
            this.label1.TabIndex = 92;
            this.label1.Text = "Pressure Control";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rBtnVaporaizer
            // 
            this.rBtnVaporaizer.AutoSize = true;
            this.rBtnVaporaizer.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rBtnVaporaizer.ForeColor = System.Drawing.Color.Black;
            this.rBtnVaporaizer.Location = new System.Drawing.Point(117, 116);
            this.rBtnVaporaizer.Name = "rBtnVaporaizer";
            this.rBtnVaporaizer.Size = new System.Drawing.Size(236, 22);
            this.rBtnVaporaizer.TabIndex = 91;
            this.rBtnVaporaizer.TabStop = true;
            this.rBtnVaporaizer.Text = "Control pressure via Vaporiser";
            this.rBtnVaporaizer.UseVisualStyleBackColor = true;
            this.rBtnVaporaizer.Click += new System.EventHandler(this.cBoxGases_Click);
            // 
            // rBtnGases
            // 
            this.rBtnGases.AutoSize = true;
            this.rBtnGases.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rBtnGases.ForeColor = System.Drawing.Color.Black;
            this.rBtnGases.Location = new System.Drawing.Point(117, 91);
            this.rBtnGases.Name = "rBtnGases";
            this.rBtnGases.Size = new System.Drawing.Size(201, 22);
            this.rBtnGases.TabIndex = 90;
            this.rBtnGases.TabStop = true;
            this.rBtnGases.Text = "Control pressure via MFC";
            this.rBtnGases.UseVisualStyleBackColor = true;
            this.rBtnGases.Click += new System.EventHandler(this.cBoxGases_Click);
            // 
            // dEditSetpoint
            // 
            this.dEditSetpoint.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dEditSetpoint.Location = new System.Drawing.Point(194, 60);
            this.dEditSetpoint.Margin = new System.Windows.Forms.Padding(4);
            this.dEditSetpoint.Mask = "0,000";
            this.dEditSetpoint.MaximumValue = 1000D;
            this.dEditSetpoint.MinimumValue = 0D;
            this.dEditSetpoint.Name = "dEditSetpoint";
            this.dEditSetpoint.ReadOnly = false;
            this.dEditSetpoint.Size = new System.Drawing.Size(87, 27);
            this.dEditSetpoint.TabIndex = 87;
            this.dEditSetpoint.Value = 0D;
            this.dEditSetpoint.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditSetpoint_EnterOn);
            // 
            // scrollPressure
            // 
            this.scrollPressure.Location = new System.Drawing.Point(22, 183);
            this.scrollPressure.Name = "scrollPressure";
            this.scrollPressure.Size = new System.Drawing.Size(279, 21);
            this.scrollPressure.TabIndex = 86;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label45.ForeColor = System.Drawing.Color.Green;
            this.label45.Location = new System.Drawing.Point(309, 62);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(52, 18);
            this.label45.TabIndex = 85;
            this.label45.Text = "[mBar]";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label43.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label43.Location = new System.Drawing.Point(125, 62);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(70, 18);
            this.label43.TabIndex = 84;
            this.label43.Text = "Setpoint:";
            // 
            // PressurePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "PressurePanel";
            this.Size = new System.Drawing.Size(362, 157);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rBtnVaporaizer;
        private System.Windows.Forms.RadioButton rBtnGases;
        private Cotrols.DoubleEdit dEditSetpoint;
        private System.Windows.Forms.HScrollBar scrollPressure;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label2;
        private Cotrols.ButtonUpDown buttonUpDown;
        private System.Windows.Forms.CheckBox cBoxAutomaticMode;
        private System.Windows.Forms.RadioButton rBtnNone;
    }
}
