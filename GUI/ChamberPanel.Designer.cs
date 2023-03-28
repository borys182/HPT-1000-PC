namespace HPT1000.GUI
{
    partial class ChamberPanel
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labCurent = new System.Windows.Forms.Label();
            this.labVoltage = new System.Windows.Forms.Label();
            this.labPressure = new System.Windows.Forms.Label();
            this.labLeakTestStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Image = global::HPT1000.Properties.Resources.Chamber1;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(232, 294);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            // 
            // labCurent
            // 
            this.labCurent.AutoSize = true;
            this.labCurent.BackColor = System.Drawing.Color.Gray;
            this.labCurent.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labCurent.ForeColor = System.Drawing.Color.Gold;
            this.labCurent.Location = new System.Drawing.Point(36, 48);
            this.labCurent.Name = "labCurent";
            this.labCurent.Size = new System.Drawing.Size(62, 18);
            this.labCurent.TabIndex = 6;
            this.labCurent.Text = "54,34 A";
            this.labCurent.Visible = false;
            // 
            // labVoltage
            // 
            this.labVoltage.AutoSize = true;
            this.labVoltage.BackColor = System.Drawing.Color.Gray;
            this.labVoltage.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labVoltage.ForeColor = System.Drawing.Color.Gold;
            this.labVoltage.Location = new System.Drawing.Point(113, 48);
            this.labVoltage.Name = "labVoltage";
            this.labVoltage.Size = new System.Drawing.Size(62, 18);
            this.labVoltage.TabIndex = 7;
            this.labVoltage.Text = "54,65 V";
            this.labVoltage.Visible = false;
            // 
            // labPressure
            // 
            this.labPressure.AutoSize = true;
            this.labPressure.BackColor = System.Drawing.Color.Gray;
            this.labPressure.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labPressure.ForeColor = System.Drawing.Color.Gold;
            this.labPressure.Location = new System.Drawing.Point(22, 109);
            this.labPressure.Name = "labPressure";
            this.labPressure.Size = new System.Drawing.Size(88, 18);
            this.labPressure.TabIndex = 8;
            this.labPressure.Text = "54,65 mBar";
            // 
            // labLeakTestStatus
            // 
            this.labLeakTestStatus.AutoSize = true;
            this.labLeakTestStatus.BackColor = System.Drawing.Color.LightGray;
            this.labLeakTestStatus.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labLeakTestStatus.ForeColor = System.Drawing.Color.Green;
            this.labLeakTestStatus.Location = new System.Drawing.Point(10, 70);
            this.labLeakTestStatus.Name = "labLeakTestStatus";
            this.labLeakTestStatus.Size = new System.Drawing.Size(115, 18);
            this.labLeakTestStatus.TabIndex = 9;
            this.labLeakTestStatus.Text = "Leak test runing";
            // 
            // ChamberPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.labLeakTestStatus);
            this.Controls.Add(this.labPressure);
            this.Controls.Add(this.labVoltage);
            this.Controls.Add(this.labCurent);
            this.Controls.Add(this.pictureBox);
            this.Name = "ChamberPanel";
            this.Size = new System.Drawing.Size(232, 294);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labCurent;
        private System.Windows.Forms.Label labVoltage;
        private System.Windows.Forms.Label labPressure;
        private System.Windows.Forms.Label labLeakTestStatus;
    }
}
