namespace HPT1000
{
    partial class FlowGasPanel
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
            this.labValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labValue
            // 
            this.labValue.BackColor = System.Drawing.Color.Gray;
            this.labValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.labValue.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labValue.ForeColor = System.Drawing.Color.Gold;
            this.labValue.Location = new System.Drawing.Point(0, 0);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(156, 19);
            this.labValue.TabIndex = 87;
            this.labValue.Text = "1321 sccm - 30,51 %";
            // 
            // FlowGasPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.labValue);
            this.Name = "FlowGasPanel";
            this.Size = new System.Drawing.Size(156, 19);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labValue;
    }
}
