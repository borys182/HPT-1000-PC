namespace KeyboardScreen
{
    partial class Keybord
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Keybord));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.keybordTxtControl = new KeyboardScreen.KeybordTxtControl();
            this.keybordNumControl = new KeyboardScreen.KeybordNumControl();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // keybordTxtControl
            // 
            this.keybordTxtControl.BackColor = System.Drawing.Color.Black;
            this.keybordTxtControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keybordTxtControl.Location = new System.Drawing.Point(0, 0);
            this.keybordTxtControl.Name = "keybordTxtControl";
            this.keybordTxtControl.Size = new System.Drawing.Size(705, 302);
            this.keybordTxtControl.TabIndex = 2;
            // 
            // keybordNumControl
            // 
            this.keybordNumControl.BackColor = System.Drawing.Color.Black;
            this.keybordNumControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keybordNumControl.Location = new System.Drawing.Point(0, 0);
            this.keybordNumControl.Name = "keybordNumControl";
            this.keybordNumControl.Size = new System.Drawing.Size(705, 302);
            this.keybordNumControl.TabIndex = 1;
            // 
            // Keybord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(705, 302);
            this.Controls.Add(this.keybordTxtControl);
            this.Controls.Add(this.keybordNumControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Keybord";
            this.Text = "KeyboardScreen";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private KeyboardScreen.KeybordNumControl keybordNumControl;
        private System.Windows.Forms.Timer timer;
        private KeyboardScreen.KeybordTxtControl keybordTxtControl;
    }
}

