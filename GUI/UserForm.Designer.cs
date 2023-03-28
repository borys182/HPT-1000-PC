namespace HPT1000.GUI
{
    partial class UserForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.rBtnImmediately = new System.Windows.Forms.RadioButton();
            this.tBoxName = new System.Windows.Forms.TextBox();
            this.cBoxUserChangeAccount = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cBoxLevelPriviliges = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.rBtnAfterData = new System.Windows.Forms.RadioButton();
            this.tBoxSurname = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tBoxLogin = new System.Windows.Forms.TextBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.cBoxEditPsw = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tBoxConfirmPassword = new System.Windows.Forms.TextBox();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.cBoxDisableAcount = new System.Windows.Forms.CheckBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(34, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(34, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Level privilage:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(400, 428);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 35);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Create";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rBtnImmediately
            // 
            this.rBtnImmediately.AutoSize = true;
            this.rBtnImmediately.Checked = true;
            this.rBtnImmediately.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rBtnImmediately.Location = new System.Drawing.Point(53, 310);
            this.rBtnImmediately.Name = "rBtnImmediately";
            this.rBtnImmediately.Size = new System.Drawing.Size(119, 24);
            this.rBtnImmediately.TabIndex = 10;
            this.rBtnImmediately.TabStop = true;
            this.rBtnImmediately.Text = "Immediately";
            this.rBtnImmediately.UseVisualStyleBackColor = true;
            // 
            // tBoxName
            // 
            this.tBoxName.Location = new System.Drawing.Point(201, 21);
            this.tBoxName.Name = "tBoxName";
            this.tBoxName.Size = new System.Drawing.Size(175, 27);
            this.tBoxName.TabIndex = 1;
            // 
            // cBoxUserChangeAccount
            // 
            this.cBoxUserChangeAccount.AutoSize = true;
            this.cBoxUserChangeAccount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxUserChangeAccount.Location = new System.Drawing.Point(39, 248);
            this.cBoxUserChangeAccount.Name = "cBoxUserChangeAccount";
            this.cBoxUserChangeAccount.Size = new System.Drawing.Size(250, 24);
            this.cBoxUserChangeAccount.TabIndex = 8;
            this.cBoxUserChangeAccount.Text = "block user to change account";
            this.cBoxUserChangeAccount.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 430);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "reguired file";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(10, 435);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(34, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Surname:";
            // 
            // cBoxLevelPriviliges
            // 
            this.cBoxLevelPriviliges.FormattingEnabled = true;
            this.cBoxLevelPriviliges.Location = new System.Drawing.Point(201, 207);
            this.cBoxLevelPriviliges.Name = "cBoxLevelPriviliges";
            this.cBoxLevelPriviliges.Size = new System.Drawing.Size(176, 28);
            this.cBoxLevelPriviliges.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(34, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Password:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(34, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Password confirm:";
            // 
            // rBtnAfterData
            // 
            this.rBtnAfterData.AutoSize = true;
            this.rBtnAfterData.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rBtnAfterData.Location = new System.Drawing.Point(53, 341);
            this.rBtnAfterData.Name = "rBtnAfterData";
            this.rBtnAfterData.Size = new System.Drawing.Size(71, 24);
            this.rBtnAfterData.TabIndex = 11;
            this.rBtnAfterData.TabStop = true;
            this.rBtnAfterData.Text = "Date:";
            this.rBtnAfterData.UseVisualStyleBackColor = true;
            // 
            // tBoxSurname
            // 
            this.tBoxSurname.Location = new System.Drawing.Point(201, 57);
            this.tBoxSurname.Name = "tBoxSurname";
            this.tBoxSurname.Size = new System.Drawing.Size(175, 27);
            this.tBoxSurname.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(34, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Login:";
            // 
            // tBoxLogin
            // 
            this.tBoxLogin.Location = new System.Drawing.Point(201, 95);
            this.tBoxLogin.Name = "tBoxLogin";
            this.tBoxLogin.Size = new System.Drawing.Size(175, 27);
            this.tBoxLogin.TabIndex = 3;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.dateStart);
            this.groupBox.Controls.Add(this.label14);
            this.groupBox.Controls.Add(this.label13);
            this.groupBox.Controls.Add(this.dateEnd);
            this.groupBox.Controls.Add(this.cBoxEditPsw);
            this.groupBox.Controls.Add(this.label11);
            this.groupBox.Controls.Add(this.label9);
            this.groupBox.Controls.Add(this.btnCancel);
            this.groupBox.Controls.Add(this.tBoxConfirmPassword);
            this.groupBox.Controls.Add(this.tBoxPassword);
            this.groupBox.Controls.Add(this.btnOK);
            this.groupBox.Controls.Add(this.tBoxLogin);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.tBoxSurname);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label8);
            this.groupBox.Controls.Add(this.rBtnImmediately);
            this.groupBox.Controls.Add(this.tBoxName);
            this.groupBox.Controls.Add(this.rBtnAfterData);
            this.groupBox.Controls.Add(this.cBoxUserChangeAccount);
            this.groupBox.Controls.Add(this.cBoxDisableAcount);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.label6);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.cBoxLevelPriviliges);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(521, 470);
            this.groupBox.TabIndex = 18;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "User data";
            // 
            // dateStart
            // 
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateStart.Location = new System.Drawing.Point(201, 338);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(176, 27);
            this.dateStart.TabIndex = 12;
            this.dateStart.ValueChanged += new System.EventHandler(this.dateStart_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label14.Location = new System.Drawing.Point(137, 379);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 20);
            this.label14.TabIndex = 28;
            this.label14.Text = "End:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label13.Location = new System.Drawing.Point(137, 343);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 20);
            this.label13.TabIndex = 27;
            this.label13.Text = "Start:";
            // 
            // dateEnd
            // 
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnd.Location = new System.Drawing.Point(201, 374);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(176, 27);
            this.dateEnd.TabIndex = 13;
            this.dateEnd.ValueChanged += new System.EventHandler(this.dateEnd_ValueChanged);
            // 
            // cBoxEditPsw
            // 
            this.cBoxEditPsw.AutoSize = true;
            this.cBoxEditPsw.Location = new System.Drawing.Point(382, 137);
            this.cBoxEditPsw.Name = "cBoxEditPsw";
            this.cBoxEditPsw.Size = new System.Drawing.Size(135, 24);
            this.cBoxEditPsw.TabIndex = 5;
            this.cBoxEditPsw.Text = "edit password";
            this.cBoxEditPsw.UseVisualStyleBackColor = true;
            this.cBoxEditPsw.CheckStateChanged += new System.EventHandler(this.cBoxEditPsw_CheckStateChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(12, 214);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(12, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "*";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tBoxConfirmPassword
            // 
            this.tBoxConfirmPassword.Location = new System.Drawing.Point(201, 170);
            this.tBoxConfirmPassword.Name = "tBoxConfirmPassword";
            this.tBoxConfirmPassword.PasswordChar = '*';
            this.tBoxConfirmPassword.Size = new System.Drawing.Size(175, 27);
            this.tBoxConfirmPassword.TabIndex = 6;
            // 
            // tBoxPassword
            // 
            this.tBoxPassword.Location = new System.Drawing.Point(201, 132);
            this.tBoxPassword.Name = "tBoxPassword";
            this.tBoxPassword.PasswordChar = '*';
            this.tBoxPassword.Size = new System.Drawing.Size(175, 27);
            this.tBoxPassword.TabIndex = 4;
            // 
            // cBoxDisableAcount
            // 
            this.cBoxDisableAcount.AutoSize = true;
            this.cBoxDisableAcount.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxDisableAcount.Location = new System.Drawing.Point(39, 280);
            this.cBoxDisableAcount.Name = "cBoxDisableAcount";
            this.cBoxDisableAcount.Size = new System.Drawing.Size(180, 24);
            this.cBoxDisableAcount.TabIndex = 9;
            this.cBoxDisableAcount.Text = "disable this account";
            this.cBoxDisableAcount.UseVisualStyleBackColor = true;
            this.cBoxDisableAcount.Click += new System.EventHandler(this.cBoxDisableAcount_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(521, 470);
            this.Controls.Add(this.groupBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rBtnImmediately;
        private System.Windows.Forms.TextBox tBoxName;
        private System.Windows.Forms.CheckBox cBoxUserChangeAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBoxLevelPriviliges;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rBtnAfterData;
        private System.Windows.Forms.TextBox tBoxSurname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tBoxLogin;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tBoxConfirmPassword;
        private System.Windows.Forms.TextBox tBoxPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cBoxDisableAcount;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.CheckBox cBoxEditPsw;
    }
}