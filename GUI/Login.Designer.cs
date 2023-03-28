namespace HPT1000.GUI
{
    partial class Login
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cBoxUsers = new System.Windows.Forms.ComboBox();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grBoxLogin = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tBoxCurrentPassword = new System.Windows.Forms.TextBox();
            this.btnCancelChange = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tBoxNewPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tBoxConfirmNewPassword = new System.Windows.Forms.TextBox();
            this.btnOkChange = new System.Windows.Forms.Button();
            this.grBoxChange = new System.Windows.Forms.GroupBox();
            this.grBoxLogin.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grBoxChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancel.Location = new System.Drawing.Point(6, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLogin.Location = new System.Drawing.Point(134, 15);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 35);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cBoxUsers
            // 
            this.cBoxUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxUsers.FormattingEnabled = true;
            this.cBoxUsers.Location = new System.Drawing.Point(110, 21);
            this.cBoxUsers.Name = "cBoxUsers";
            this.cBoxUsers.Size = new System.Drawing.Size(144, 28);
            this.cBoxUsers.TabIndex = 1;
            this.cBoxUsers.SelectedIndexChanged += new System.EventHandler(this.cBoxUsers_SelectedIndexChanged);
            // 
            // tBoxPassword
            // 
            this.tBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxPassword.Location = new System.Drawing.Point(110, 66);
            this.tBoxPassword.Name = "tBoxPassword";
            this.tBoxPassword.PasswordChar = '*';
            this.tBoxPassword.Size = new System.Drawing.Size(144, 27);
            this.tBoxPassword.TabIndex = 2;
            this.tBoxPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBoxPassword_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(8, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Login:";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(269, 67);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(55, 27);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "edit ...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // grBoxLogin
            // 
            this.grBoxLogin.Controls.Add(this.btnCancel);
            this.grBoxLogin.Controls.Add(this.btnLogin);
            this.grBoxLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grBoxLogin.Location = new System.Drawing.Point(0, 273);
            this.grBoxLogin.Name = "grBoxLogin";
            this.grBoxLogin.Size = new System.Drawing.Size(330, 55);
            this.grBoxLogin.TabIndex = 7;
            this.grBoxLogin.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cBoxUsers);
            this.groupBox2.Controls.Add(this.tBoxPassword);
            this.groupBox2.Controls.Add(this.btnEdit);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 110);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(8, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(141, 20);
            this.label9.TabIndex = 1;
            this.label9.Text = "Curent password:";
            // 
            // tBoxCurrentPassword
            // 
            this.tBoxCurrentPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxCurrentPassword.Location = new System.Drawing.Point(159, 18);
            this.tBoxCurrentPassword.Name = "tBoxCurrentPassword";
            this.tBoxCurrentPassword.PasswordChar = '*';
            this.tBoxCurrentPassword.Size = new System.Drawing.Size(165, 27);
            this.tBoxCurrentPassword.TabIndex = 4;
            // 
            // btnCancelChange
            // 
            this.btnCancelChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancelChange.Location = new System.Drawing.Point(6, 128);
            this.btnCancelChange.Name = "btnCancelChange";
            this.btnCancelChange.Size = new System.Drawing.Size(155, 35);
            this.btnCancelChange.TabIndex = 7;
            this.btnCancelChange.Text = "Cancel";
            this.btnCancelChange.UseVisualStyleBackColor = true;
            this.btnCancelChange.Click += new System.EventHandler(this.btnCancelChange_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(8, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "New password:";
            // 
            // tBoxNewPassword
            // 
            this.tBoxNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxNewPassword.Location = new System.Drawing.Point(159, 55);
            this.tBoxNewPassword.Name = "tBoxNewPassword";
            this.tBoxNewPassword.PasswordChar = '*';
            this.tBoxNewPassword.Size = new System.Drawing.Size(165, 27);
            this.tBoxNewPassword.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(8, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Confirm password:";
            // 
            // tBoxConfirmNewPassword
            // 
            this.tBoxConfirmNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxConfirmNewPassword.Location = new System.Drawing.Point(159, 90);
            this.tBoxConfirmNewPassword.Name = "tBoxConfirmNewPassword";
            this.tBoxConfirmNewPassword.PasswordChar = '*';
            this.tBoxConfirmNewPassword.Size = new System.Drawing.Size(165, 27);
            this.tBoxConfirmNewPassword.TabIndex = 6;
            // 
            // btnOkChange
            // 
            this.btnOkChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOkChange.Location = new System.Drawing.Point(167, 128);
            this.btnOkChange.Name = "btnOkChange";
            this.btnOkChange.Size = new System.Drawing.Size(155, 35);
            this.btnOkChange.TabIndex = 8;
            this.btnOkChange.Text = "OK";
            this.btnOkChange.UseVisualStyleBackColor = true;
            this.btnOkChange.Click += new System.EventHandler(this.btnOkChange_Click);
            // 
            // grBoxChange
            // 
            this.grBoxChange.Controls.Add(this.btnOkChange);
            this.grBoxChange.Controls.Add(this.tBoxConfirmNewPassword);
            this.grBoxChange.Controls.Add(this.label4);
            this.grBoxChange.Controls.Add(this.tBoxNewPassword);
            this.grBoxChange.Controls.Add(this.label8);
            this.grBoxChange.Controls.Add(this.btnCancelChange);
            this.grBoxChange.Controls.Add(this.tBoxCurrentPassword);
            this.grBoxChange.Controls.Add(this.label9);
            this.grBoxChange.Location = new System.Drawing.Point(0, 111);
            this.grBoxChange.Name = "grBoxChange";
            this.grBoxChange.Size = new System.Drawing.Size(327, 168);
            this.grBoxChange.TabIndex = 12;
            this.grBoxChange.TabStop = false;
            this.grBoxChange.Text = "Change data";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(330, 328);
            this.Controls.Add(this.grBoxChange);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grBoxLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.grBoxLogin.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grBoxChange.ResumeLayout(false);
            this.grBoxChange.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ComboBox cBoxUsers;
        private System.Windows.Forms.TextBox tBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEdit;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox grBoxLogin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tBoxCurrentPassword;
        private System.Windows.Forms.Button btnCancelChange;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tBoxNewPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBoxConfirmNewPassword;
        private System.Windows.Forms.Button btnOkChange;
        private System.Windows.Forms.GroupBox grBoxChange;
    }
}