namespace HPT1000.GUI
{
    partial class MaintancePanel
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
            this.label30 = new System.Windows.Forms.Label();
            this.rBtnTime = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.dEditRotatoryVanePump = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.tBoxTimeWorkFP = new System.Windows.Forms.TextBox();
            this.rBtnInterval = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.btnUpDownTimeFP = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dEditInterval = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpDownInterval = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.dateTimeNextMaintance = new System.Windows.Forms.DateTimePicker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cBoxLeakTest = new System.Windows.Forms.CheckBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.labLeaktestStatus = new System.Windows.Forms.Label();
            this.dEditChamberVolume = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditSetpoint = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.btnUpDownSetpoint = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.btnUpDownChamberVolume = new HPT1000.GUI.Cotrols.ButtonUpDown();
            this.actualTimeDurationLeakProces = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.timeLeakMesure = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tBoxLeak = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labTimeWorkFP = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.labOperetingHour = new System.Windows.Forms.Label();
            this.labProcesNumber = new System.Windows.Forms.Label();
            this.labTimeNextGlobalMaintanence = new System.Windows.Forms.Label();
            this.labTimeLastMaintance = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labStatusOilChange = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btnOliChange = new System.Windows.Forms.Button();
            this.labStatusMaintenance = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMaintanceMade = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.rBtnTime);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.dEditRotatoryVanePump);
            this.groupBox1.Controls.Add(this.tBoxTimeWorkFP);
            this.groupBox1.Controls.Add(this.rBtnInterval);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.btnUpDownTimeFP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.dEditInterval);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnUpDownInterval);
            this.groupBox1.Controls.Add(this.dateTimeNextMaintance);
            this.groupBox1.Location = new System.Drawing.Point(12, 323);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1251, 181);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Schedule";
            // 
            // label30
            // 
            this.label30.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label30.Location = new System.Drawing.Point(719, 20);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(369, 30);
            this.label30.TabIndex = 103;
            this.label30.Text = "Determine oil change schedule";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rBtnTime
            // 
            this.rBtnTime.AutoSize = true;
            this.rBtnTime.Location = new System.Drawing.Point(45, 141);
            this.rBtnTime.Name = "rBtnTime";
            this.rBtnTime.Size = new System.Drawing.Size(222, 22);
            this.rBtnTime.TabIndex = 110;
            this.rBtnTime.TabStop = true;
            this.rBtnTime.Text = "Set next maintenance as date";
            this.rBtnTime.UseVisualStyleBackColor = true;
            this.rBtnTime.Click += new System.EventHandler(this.rBtnInterval_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Green;
            this.label12.Location = new System.Drawing.Point(1115, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 18);
            this.label12.TabIndex = 26;
            this.label12.Text = "h";
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(18, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(369, 30);
            this.label11.TabIndex = 108;
            this.label11.Text = "Determine maintenance schedule";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Green;
            this.label9.Location = new System.Drawing.Point(1115, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 18);
            this.label9.TabIndex = 25;
            this.label9.Text = "h";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Green;
            this.label25.Location = new System.Drawing.Point(436, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(58, 18);
            this.label25.TabIndex = 107;
            this.label25.Text = "months";
            // 
            // dEditRotatoryVanePump
            // 
            this.dEditRotatoryVanePump.Location = new System.Drawing.Point(963, 62);
            this.dEditRotatoryVanePump.Margin = new System.Windows.Forms.Padding(4);
            this.dEditRotatoryVanePump.Mask = "0";
            this.dEditRotatoryVanePump.MaximumValue = 1000000D;
            this.dEditRotatoryVanePump.MinimumValue = 0D;
            this.dEditRotatoryVanePump.Name = "dEditRotatoryVanePump";
            this.dEditRotatoryVanePump.ReadOnly = false;
            this.dEditRotatoryVanePump.Size = new System.Drawing.Size(117, 30);
            this.dEditRotatoryVanePump.TabIndex = 13;
            this.dEditRotatoryVanePump.Value = 0D;
            this.dEditRotatoryVanePump.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditRotatoryVanePump_EnterOn);
            // 
            // tBoxTimeWorkFP
            // 
            this.tBoxTimeWorkFP.Enabled = false;
            this.tBoxTimeWorkFP.Location = new System.Drawing.Point(964, 103);
            this.tBoxTimeWorkFP.Name = "tBoxTimeWorkFP";
            this.tBoxTimeWorkFP.Size = new System.Drawing.Size(117, 24);
            this.tBoxTimeWorkFP.TabIndex = 12;
            this.tBoxTimeWorkFP.Text = "654";
            // 
            // rBtnInterval
            // 
            this.rBtnInterval.AutoSize = true;
            this.rBtnInterval.Location = new System.Drawing.Point(324, 141);
            this.rBtnInterval.Name = "rBtnInterval";
            this.rBtnInterval.Size = new System.Drawing.Size(240, 22);
            this.rBtnInterval.TabIndex = 109;
            this.rBtnInterval.TabStop = true;
            this.rBtnInterval.Text = "Set next maintenance as interval";
            this.rBtnInterval.UseVisualStyleBackColor = true;
            this.rBtnInterval.Click += new System.EventHandler(this.rBtnInterval_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label24.Location = new System.Drawing.Point(747, 102);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(172, 18);
            this.label24.TabIndex = 11;
            this.label24.Text = "Fore pump working time:";
            // 
            // btnUpDownTimeFP
            // 
            this.btnUpDownTimeFP.HeightComponent = 38;
            this.btnUpDownTimeFP.Location = new System.Drawing.Point(1087, 59);
            this.btnUpDownTimeFP.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpDownTimeFP.Maximum = 1000000D;
            this.btnUpDownTimeFP.Minimum = 0D;
            this.btnUpDownTimeFP.Name = "btnUpDownTimeFP";
            this.btnUpDownTimeFP.Size = new System.Drawing.Size(28, 38);
            this.btnUpDownTimeFP.Step = 1D;
            this.btnUpDownTimeFP.TabIndex = 9;
            this.btnUpDownTimeFP.Value = 0D;
            this.btnUpDownTimeFP.WidthComponent = 28;
            this.btnUpDownTimeFP.ValueChanged += new System.EventHandler(this.btnUpDownRotatoryFP_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(42, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 18);
            this.label1.TabIndex = 102;
            this.label1.Text = "Next maintenance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label8.Location = new System.Drawing.Point(747, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 18);
            this.label8.TabIndex = 3;
            this.label8.Text = "Oil change intervals:";
            // 
            // dEditInterval
            // 
            this.dEditInterval.Location = new System.Drawing.Point(271, 61);
            this.dEditInterval.Margin = new System.Windows.Forms.Padding(4);
            this.dEditInterval.Mask = "0";
            this.dEditInterval.MaximumValue = 1000D;
            this.dEditInterval.MinimumValue = 0D;
            this.dEditInterval.Name = "dEditInterval";
            this.dEditInterval.ReadOnly = false;
            this.dEditInterval.Size = new System.Drawing.Size(117, 32);
            this.dEditInterval.TabIndex = 106;
            this.dEditInterval.Value = 0D;
            this.dEditInterval.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditInterval_EnterOn);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(42, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 18);
            this.label2.TabIndex = 103;
            this.label2.Text = "Maintenance interval:";
            // 
            // btnUpDownInterval
            // 
            this.btnUpDownInterval.HeightComponent = 38;
            this.btnUpDownInterval.Location = new System.Drawing.Point(397, 59);
            this.btnUpDownInterval.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpDownInterval.Maximum = 100D;
            this.btnUpDownInterval.Minimum = 0D;
            this.btnUpDownInterval.Name = "btnUpDownInterval";
            this.btnUpDownInterval.Size = new System.Drawing.Size(30, 38);
            this.btnUpDownInterval.Step = 1D;
            this.btnUpDownInterval.TabIndex = 105;
            this.btnUpDownInterval.Value = 0D;
            this.btnUpDownInterval.WidthComponent = 30;
            this.btnUpDownInterval.ValueChanged += new System.EventHandler(this.btnUpDownInterval_ValueChanged);
            // 
            // dateTimeNextMaintance
            // 
            this.dateTimeNextMaintance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeNextMaintance.Location = new System.Drawing.Point(271, 101);
            this.dateTimeNextMaintance.Name = "dateTimeNextMaintance";
            this.dateTimeNextMaintance.Size = new System.Drawing.Size(117, 24);
            this.dateTimeNextMaintance.TabIndex = 104;
            this.dateTimeNextMaintance.ValueChanged += new System.EventHandler(this.dateTimeNextMaintance_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.cBoxLeakTest);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.labLeaktestStatus);
            this.groupBox3.Controls.Add(this.dEditChamberVolume);
            this.groupBox3.Controls.Add(this.dEditSetpoint);
            this.groupBox3.Controls.Add(this.btnUpDownSetpoint);
            this.groupBox3.Controls.Add(this.btnUpDownChamberVolume);
            this.groupBox3.Controls.Add(this.actualTimeDurationLeakProces);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.timeLeakMesure);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.tBoxLeak);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(12, 518);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1249, 261);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Diagnosis";
            // 
            // cBoxLeakTest
            // 
            this.cBoxLeakTest.Appearance = System.Windows.Forms.Appearance.Button;
            this.cBoxLeakTest.Image = global::HPT1000.Properties.Resources.on;
            this.cBoxLeakTest.Location = new System.Drawing.Point(1124, 196);
            this.cBoxLeakTest.Name = "cBoxLeakTest";
            this.cBoxLeakTest.Size = new System.Drawing.Size(60, 60);
            this.cBoxLeakTest.TabIndex = 107;
            this.cBoxLeakTest.UseVisualStyleBackColor = true;
            this.cBoxLeakTest.Click += new System.EventHandler(this.cBoxLeakTest_Click);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label36.Location = new System.Drawing.Point(743, 71);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(112, 18);
            this.label36.TabIndex = 106;
            this.label36.Text = "Process status:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label27.Location = new System.Drawing.Point(698, 29);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(455, 18);
            this.label27.TabIndex = 105;
            this.label27.Text = "Leak Test Information (start from chamber at atmospheric pressure)";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label35.Location = new System.Drawing.Point(18, 23);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(369, 30);
            this.label35.TabIndex = 104;
            this.label35.Text = "Determine leak test parameters";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labLeaktestStatus
            // 
            this.labLeaktestStatus.AutoSize = true;
            this.labLeaktestStatus.BackColor = System.Drawing.Color.White;
            this.labLeaktestStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labLeaktestStatus.ForeColor = System.Drawing.Color.Black;
            this.labLeaktestStatus.Location = new System.Drawing.Point(961, 71);
            this.labLeaktestStatus.Name = "labLeaktestStatus";
            this.labLeaktestStatus.Size = new System.Drawing.Size(208, 18);
            this.labLeaktestStatus.TabIndex = 33;
            this.labLeaktestStatus.Text = "PUMP DOWN TO SETPOINT";
            this.labLeaktestStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dEditChamberVolume
            // 
            this.dEditChamberVolume.Location = new System.Drawing.Point(271, 159);
            this.dEditChamberVolume.Margin = new System.Windows.Forms.Padding(6);
            this.dEditChamberVolume.Mask = "0.00";
            this.dEditChamberVolume.MaximumValue = 1000000D;
            this.dEditChamberVolume.MinimumValue = 0D;
            this.dEditChamberVolume.Name = "dEditChamberVolume";
            this.dEditChamberVolume.ReadOnly = false;
            this.dEditChamberVolume.Size = new System.Drawing.Size(117, 30);
            this.dEditChamberVolume.TabIndex = 31;
            this.dEditChamberVolume.Value = 0D;
            this.dEditChamberVolume.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditChamberVolume_EnterOn);
            // 
            // dEditSetpoint
            // 
            this.dEditSetpoint.Location = new System.Drawing.Point(270, 64);
            this.dEditSetpoint.Margin = new System.Windows.Forms.Padding(6);
            this.dEditSetpoint.Mask = "0.000";
            this.dEditSetpoint.MaximumValue = 1000D;
            this.dEditSetpoint.MinimumValue = 0D;
            this.dEditSetpoint.Name = "dEditSetpoint";
            this.dEditSetpoint.ReadOnly = false;
            this.dEditSetpoint.Size = new System.Drawing.Size(117, 30);
            this.dEditSetpoint.TabIndex = 30;
            this.dEditSetpoint.Value = 0D;
            this.dEditSetpoint.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditSetpoint_EnterOn);
            // 
            // btnUpDownSetpoint
            // 
            this.btnUpDownSetpoint.HeightComponent = 40;
            this.btnUpDownSetpoint.Location = new System.Drawing.Point(396, 62);
            this.btnUpDownSetpoint.Margin = new System.Windows.Forms.Padding(5);
            this.btnUpDownSetpoint.Maximum = 1000D;
            this.btnUpDownSetpoint.Minimum = 0D;
            this.btnUpDownSetpoint.Name = "btnUpDownSetpoint";
            this.btnUpDownSetpoint.Size = new System.Drawing.Size(30, 40);
            this.btnUpDownSetpoint.Step = 0.01D;
            this.btnUpDownSetpoint.TabIndex = 29;
            this.btnUpDownSetpoint.Value = 0D;
            this.btnUpDownSetpoint.WidthComponent = 30;
            this.btnUpDownSetpoint.ValueChanged += new System.EventHandler(this.btnUpDownSetpoint_ValueChanged);
            // 
            // btnUpDownChamberVolume
            // 
            this.btnUpDownChamberVolume.HeightComponent = 39;
            this.btnUpDownChamberVolume.Location = new System.Drawing.Point(397, 157);
            this.btnUpDownChamberVolume.Margin = new System.Windows.Forms.Padding(6);
            this.btnUpDownChamberVolume.Maximum = 1000000D;
            this.btnUpDownChamberVolume.Minimum = 0D;
            this.btnUpDownChamberVolume.Name = "btnUpDownChamberVolume";
            this.btnUpDownChamberVolume.Size = new System.Drawing.Size(30, 39);
            this.btnUpDownChamberVolume.Step = 0.01D;
            this.btnUpDownChamberVolume.TabIndex = 28;
            this.btnUpDownChamberVolume.Value = 0D;
            this.btnUpDownChamberVolume.WidthComponent = 30;
            this.btnUpDownChamberVolume.ValueChanged += new System.EventHandler(this.btnUpDownChamberVolume_ValueChanged);
            // 
            // actualTimeDurationLeakProces
            // 
            this.actualTimeDurationLeakProces.Enabled = false;
            this.actualTimeDurationLeakProces.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.actualTimeDurationLeakProces.Location = new System.Drawing.Point(964, 115);
            this.actualTimeDurationLeakProces.Name = "actualTimeDurationLeakProces";
            this.actualTimeDurationLeakProces.Size = new System.Drawing.Size(117, 24);
            this.actualTimeDurationLeakProces.TabIndex = 27;
            this.actualTimeDurationLeakProces.Value = new System.DateTime(2016, 12, 5, 0, 30, 0, 0);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Green;
            this.label21.Location = new System.Drawing.Point(1115, 123);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 18);
            this.label21.TabIndex = 26;
            this.label21.Text = "hh:mm:ss";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label22.Location = new System.Drawing.Point(743, 118);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(188, 18);
            this.label22.TabIndex = 25;
            this.label22.Text = "Time to leak measurement:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Green;
            this.label20.Location = new System.Drawing.Point(436, 120);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 18);
            this.label20.TabIndex = 24;
            this.label20.Text = "hh:mm:ss";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Green;
            this.label18.Location = new System.Drawing.Point(436, 165);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 18);
            this.label18.TabIndex = 22;
            this.label18.Text = "L";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(42, 165);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 18);
            this.label19.TabIndex = 19;
            this.label19.Text = "Chamber volume:";
            // 
            // timeLeakMesure
            // 
            this.timeLeakMesure.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeLeakMesure.Location = new System.Drawing.Point(273, 115);
            this.timeLeakMesure.Name = "timeLeakMesure";
            this.timeLeakMesure.ShowUpDown = true;
            this.timeLeakMesure.Size = new System.Drawing.Size(117, 24);
            this.timeLeakMesure.TabIndex = 8;
            this.timeLeakMesure.Value = new System.DateTime(2016, 12, 5, 0, 30, 0, 0);
            this.timeLeakMesure.ValueChanged += new System.EventHandler(this.timeLeakMesure_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label17.Location = new System.Drawing.Point(42, 116);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(138, 18);
            this.label17.TabIndex = 16;
            this.label17.Text = "Measuring duration:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Green;
            this.label16.Location = new System.Drawing.Point(436, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 18);
            this.label16.TabIndex = 15;
            this.label16.Text = "mbar";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Green;
            this.label15.Location = new System.Drawing.Point(1115, 169);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 18);
            this.label15.TabIndex = 14;
            this.label15.Text = "mbar.L/s";
            // 
            // tBoxLeak
            // 
            this.tBoxLeak.Enabled = false;
            this.tBoxLeak.Location = new System.Drawing.Point(964, 166);
            this.tBoxLeak.Name = "tBoxLeak";
            this.tBoxLeak.Size = new System.Drawing.Size(117, 24);
            this.tBoxLeak.TabIndex = 13;
            this.tBoxLeak.Text = "0.200";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label13.Location = new System.Drawing.Point(743, 165);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 18);
            this.label13.TabIndex = 9;
            this.label13.Text = "Leakage rate:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label14.Location = new System.Drawing.Point(42, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(172, 18);
            this.label14.TabIndex = 10;
            this.label14.Text = "Pumping down pressure:";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.labTimeWorkFP);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.labOperetingHour);
            this.groupBox4.Controls.Add(this.labProcesNumber);
            this.groupBox4.Controls.Add(this.labTimeNextGlobalMaintanence);
            this.groupBox4.Controls.Add(this.labTimeLastMaintance);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(12, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1251, 170);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Information";
            // 
            // labTimeWorkFP
            // 
            this.labTimeWorkFP.AutoSize = true;
            this.labTimeWorkFP.Enabled = false;
            this.labTimeWorkFP.Location = new System.Drawing.Point(961, 100);
            this.labTimeWorkFP.Name = "labTimeWorkFP";
            this.labTimeWorkFP.Size = new System.Drawing.Size(32, 18);
            this.labTimeWorkFP.TabIndex = 106;
            this.labTimeWorkFP.Text = "423";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Enabled = false;
            this.label33.ForeColor = System.Drawing.Color.Green;
            this.label33.Location = new System.Drawing.Point(1115, 100);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(16, 18);
            this.label33.TabIndex = 105;
            this.label33.Text = "h";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label34.Location = new System.Drawing.Point(747, 100);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(172, 18);
            this.label34.TabIndex = 103;
            this.label34.Text = "Fore pump working time:";
            // 
            // labOperetingHour
            // 
            this.labOperetingHour.AutoSize = true;
            this.labOperetingHour.Enabled = false;
            this.labOperetingHour.Location = new System.Drawing.Point(268, 100);
            this.labOperetingHour.Name = "labOperetingHour";
            this.labOperetingHour.Size = new System.Drawing.Size(32, 18);
            this.labOperetingHour.TabIndex = 102;
            this.labOperetingHour.Text = "567";
            // 
            // labProcesNumber
            // 
            this.labProcesNumber.AutoSize = true;
            this.labProcesNumber.Enabled = false;
            this.labProcesNumber.Location = new System.Drawing.Point(268, 136);
            this.labProcesNumber.Name = "labProcesNumber";
            this.labProcesNumber.Size = new System.Drawing.Size(40, 18);
            this.labProcesNumber.TabIndex = 101;
            this.labProcesNumber.Text = "1234";
            // 
            // labTimeNextGlobalMaintanence
            // 
            this.labTimeNextGlobalMaintanence.AutoSize = true;
            this.labTimeNextGlobalMaintanence.Enabled = false;
            this.labTimeNextGlobalMaintanence.Location = new System.Drawing.Point(960, 62);
            this.labTimeNextGlobalMaintanence.Name = "labTimeNextGlobalMaintanence";
            this.labTimeNextGlobalMaintanence.Size = new System.Drawing.Size(80, 18);
            this.labTimeNextGlobalMaintanence.TabIndex = 99;
            this.labTimeNextGlobalMaintanence.Text = "18.08.2017";
            // 
            // labTimeLastMaintance
            // 
            this.labTimeLastMaintance.AutoSize = true;
            this.labTimeLastMaintance.Enabled = false;
            this.labTimeLastMaintance.Location = new System.Drawing.Point(268, 62);
            this.labTimeLastMaintance.Name = "labTimeLastMaintance";
            this.labTimeLastMaintance.Size = new System.Drawing.Size(80, 18);
            this.labTimeLastMaintance.TabIndex = 98;
            this.labTimeLastMaintance.Text = "01.03.2016";
            // 
            // label28
            // 
            this.label28.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label28.Location = new System.Drawing.Point(18, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(867, 30);
            this.label28.TabIndex = 97;
            this.label28.Text = "Shows information about last and next maintenance plus current operational data";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label23.Location = new System.Drawing.Point(747, 62);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(130, 18);
            this.label23.TabIndex = 12;
            this.label23.Text = "Next maintenance:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(42, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 18);
            this.label7.TabIndex = 7;
            this.label7.Text = "Operating hours:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(42, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "Number of processes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(42, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = "Last maintenance:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labStatusOilChange);
            this.groupBox5.Controls.Add(this.label32);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.btnOliChange);
            this.groupBox5.Controls.Add(this.labStatusMaintenance);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.btnMaintanceMade);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(12, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1251, 134);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Status";
            // 
            // labStatusOilChange
            // 
            this.labStatusOilChange.BackColor = System.Drawing.Color.Gray;
            this.labStatusOilChange.ForeColor = System.Drawing.Color.White;
            this.labStatusOilChange.Location = new System.Drawing.Point(268, 91);
            this.labStatusOilChange.Name = "labStatusOilChange";
            this.labStatusOilChange.Size = new System.Drawing.Size(423, 35);
            this.labStatusOilChange.TabIndex = 114;
            this.labStatusOilChange.Text = "OIL CHANGE NOT REQUIRED";
            this.labStatusOilChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label32.Location = new System.Drawing.Point(42, 96);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(128, 18);
            this.label32.TabIndex = 113;
            this.label32.Text = "Fore pump status:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label26.Location = new System.Drawing.Point(719, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(283, 18);
            this.label26.TabIndex = 112;
            this.label26.Text = "Confirm maintenance done or oil changed";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label29
            // 
            this.label29.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label29.Location = new System.Drawing.Point(18, 20);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(578, 30);
            this.label29.TabIndex = 111;
            this.label29.Text = "Shows information if maintenance is required";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOliChange
            // 
            this.btnOliChange.Location = new System.Drawing.Point(964, 90);
            this.btnOliChange.Name = "btnOliChange";
            this.btnOliChange.Size = new System.Drawing.Size(220, 30);
            this.btnOliChange.TabIndex = 109;
            this.btnOliChange.Text = "OIL CHANGED";
            this.btnOliChange.UseVisualStyleBackColor = true;
            this.btnOliChange.Click += new System.EventHandler(this.btnOliChange_Click);
            // 
            // labStatusMaintenance
            // 
            this.labStatusMaintenance.BackColor = System.Drawing.Color.Gray;
            this.labStatusMaintenance.ForeColor = System.Drawing.Color.White;
            this.labStatusMaintenance.Location = new System.Drawing.Point(268, 54);
            this.labStatusMaintenance.Name = "labStatusMaintenance";
            this.labStatusMaintenance.Size = new System.Drawing.Size(423, 35);
            this.labStatusMaintenance.TabIndex = 110;
            this.labStatusMaintenance.Text = "MAINTENANCE NOT REQUIRED";
            this.labStatusMaintenance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Location = new System.Drawing.Point(747, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 18);
            this.label10.TabIndex = 108;
            this.label10.Text = "Confirm oil change:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(747, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 18);
            this.label3.TabIndex = 105;
            this.label3.Text = "Confirm maintenance:";
            // 
            // btnMaintanceMade
            // 
            this.btnMaintanceMade.Location = new System.Drawing.Point(963, 55);
            this.btnMaintanceMade.Name = "btnMaintanceMade";
            this.btnMaintanceMade.Size = new System.Drawing.Size(220, 30);
            this.btnMaintanceMade.TabIndex = 106;
            this.btnMaintanceMade.Text = "MAINTENANCE DONE";
            this.btnMaintanceMade.UseVisualStyleBackColor = true;
            this.btnMaintanceMade.Click += new System.EventHandler(this.btnMaintanceMade_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(42, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 18);
            this.label4.TabIndex = 107;
            this.label4.Text = "Machine status:";
            // 
            // MaintancePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "MaintancePanel";
            this.Size = new System.Drawing.Size(1274, 790);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker timeLeakMesure;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tBoxLeak;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DateTimePicker actualTimeDurationLeakProces;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private Cotrols.ButtonUpDown btnUpDownTimeFP;
        private System.Windows.Forms.Label label23;
        private Cotrols.ButtonUpDown btnUpDownSetpoint;
        private System.Windows.Forms.TextBox tBoxTimeWorkFP;
        private System.Windows.Forms.Label label24;
        private Cotrols.DoubleEdit dEditRotatoryVanePump;
        private Cotrols.DoubleEdit dEditChamberVolume;
        private Cotrols.DoubleEdit dEditSetpoint;
        private Cotrols.ButtonUpDown btnUpDownChamberVolume;
        private System.Windows.Forms.Label labLeaktestStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label labTimeNextGlobalMaintanence;
        private System.Windows.Forms.Label labTimeLastMaintance;
        private System.Windows.Forms.Label labOperetingHour;
        private System.Windows.Forms.Label labProcesNumber;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.RadioButton rBtnTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.RadioButton rBtnInterval;
        private Cotrols.DoubleEdit dEditInterval;
        private Cotrols.ButtonUpDown btnUpDownInterval;
        private System.Windows.Forms.DateTimePicker dateTimeNextMaintance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labStatusOilChange;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btnOliChange;
        private System.Windows.Forms.Label labStatusMaintenance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMaintanceMade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label labTimeWorkFP;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.CheckBox cBoxLeakTest;
    }
}
