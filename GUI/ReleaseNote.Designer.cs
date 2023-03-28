namespace HPT1000.GUI
{
    partial class ReleaseNote
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtBox
            // 
            this.rtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtBox.Location = new System.Drawing.Point(0, 0);
            this.rtBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtBox.Name = "rtBox";
            this.rtBox.ReadOnly = true;
            this.rtBox.Size = new System.Drawing.Size(590, 411);
            this.rtBox.TabIndex = 2;
            this.rtBox.Text = "";
            // 
            // ReleaseNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 411);
            this.Controls.Add(this.rtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ReleaseNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Release Notes";
            this.Shown += new System.EventHandler(this.ReleaseNote_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtBox;
    }
}