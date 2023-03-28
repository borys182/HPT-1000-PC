namespace HPT1000.GUI
{
    partial class MotorPanel
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
            this.labName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxMotor = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.Gray;
            this.labName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labName.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labName.ForeColor = System.Drawing.Color.White;
            this.labName.Location = new System.Drawing.Point(3, 23);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(79, 24);
            this.labName.TabIndex = 51;
            this.labName.Text = "Motor 1";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxMotor);
            this.groupBox1.Controls.Add(this.labName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(85, 148);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            // 
            // cBoxMotor
            // 
            this.cBoxMotor.Appearance = System.Windows.Forms.Appearance.Button;
            this.cBoxMotor.AutoSize = true;
            this.cBoxMotor.Image = global::HPT1000.Properties.Resources.MotorERR;
            this.cBoxMotor.Location = new System.Drawing.Point(4, 51);
            this.cBoxMotor.Name = "cBoxMotor";
            this.cBoxMotor.Size = new System.Drawing.Size(76, 74);
            this.cBoxMotor.TabIndex = 52;
            this.cBoxMotor.UseVisualStyleBackColor = true;
            this.cBoxMotor.Click += new System.EventHandler(this.cBoxMotor_Click);
            // 
            // MotorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MotorPanel";
            this.Size = new System.Drawing.Size(85, 152);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cBoxMotor;
    }
}
