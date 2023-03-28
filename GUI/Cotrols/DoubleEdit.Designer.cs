namespace HPT1000.GUI.Cotrols
{
    partial class DoubleEdit
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
            this.tBox = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.grBoxFocuse = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // tBox
            // 
            this.tBox.Location = new System.Drawing.Point(3, 0);
            this.tBox.Name = "tBox";
            this.tBox.Size = new System.Drawing.Size(227, 22);
            this.tBox.TabIndex = 0;
            this.tBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBox_KeyPress);
            this.tBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBox_KeyUp);
            this.tBox.Validated += new System.EventHandler(this.tBox_Validated);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // grBoxFocuse
            // 
            this.grBoxFocuse.Location = new System.Drawing.Point(75, 28);
            this.grBoxFocuse.Name = "grBoxFocuse";
            this.grBoxFocuse.Size = new System.Drawing.Size(92, 29);
            this.grBoxFocuse.TabIndex = 1;
            this.grBoxFocuse.TabStop = false;
            this.grBoxFocuse.Text = "groupBox1";
            // 
            // DoubleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grBoxFocuse);
            this.Controls.Add(this.tBox);
            this.Name = "DoubleEdit";
            this.Size = new System.Drawing.Size(230, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox grBoxFocuse;
    }
}
