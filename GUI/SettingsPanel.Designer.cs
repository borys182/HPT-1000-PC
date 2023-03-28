namespace HPT1000.GUI
{
    partial class SettingsPanel
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("MFC1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("MFC2");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("MFC3");
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGlobal = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panelAddressIP = new System.Windows.Forms.Panel();
            this.btnSetIP = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cBoxComm = new System.Windows.Forms.ComboBox();
            this.cBoxDummyMode = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labConnectionExplain = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.cBoxLanguge = new System.Windows.Forms.ComboBox();
            this.tabPageAcq = new System.Windows.Forms.TabPage();
            this.grBoxAcq = new System.Windows.Forms.GroupBox();
            this.cBoxAskAcq = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.grBoxAcqPara = new System.Windows.Forms.GroupBox();
            this.rBtnAcqDuringProcess = new System.Windows.Forms.RadioButton();
            this.label40 = new System.Windows.Forms.Label();
            this.grBoxParameter = new System.Windows.Forms.GroupBox();
            this.listViewDevices = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cBoxMode = new System.Windows.Forms.ComboBox();
            this.treeViewDevices = new System.Windows.Forms.TreeView();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxParaActive = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labParaUnit = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rBtnAcqAllTime = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.labPressure = new System.Windows.Forms.Label();
            this.labUnitPressure = new System.Windows.Forms.Label();
            this.cBoxActivePressure = new System.Windows.Forms.CheckBox();
            this.cBoxEnabledAcq = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dEdit_ChartWindow_Sec = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditChartWindow_Minute = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditChartWindow_Hour = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditIP3 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditIP4 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditIP2 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditIP1 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditDifferencesValue = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFrqAcq = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditAcqPressure = new HPT1000.GUI.Cotrols.DoubleEdit();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageGlobal.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelAddressIP.SuspendLayout();
            this.tabPageAcq.SuspendLayout();
            this.grBoxAcq.SuspendLayout();
            this.grBoxAcqPara.SuspendLayout();
            this.grBoxParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGlobal);
            this.tabControl1.Controls.Add(this.tabPageAcq);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(165, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1274, 794);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 108;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPageGlobal
            // 
            this.tabPageGlobal.BackColor = System.Drawing.Color.White;
            this.tabPageGlobal.Controls.Add(this.panel1);
            this.tabPageGlobal.Controls.Add(this.label21);
            this.tabPageGlobal.Controls.Add(this.label20);
            this.tabPageGlobal.Controls.Add(this.panelAddressIP);
            this.tabPageGlobal.Controls.Add(this.label15);
            this.tabPageGlobal.Controls.Add(this.label14);
            this.tabPageGlobal.Controls.Add(this.cBoxComm);
            this.tabPageGlobal.Controls.Add(this.cBoxDummyMode);
            this.tabPageGlobal.Controls.Add(this.label4);
            this.tabPageGlobal.Controls.Add(this.labConnectionExplain);
            this.tabPageGlobal.Controls.Add(this.label1);
            this.tabPageGlobal.Controls.Add(this.label45);
            this.tabPageGlobal.Controls.Add(this.cBoxLanguge);
            this.tabPageGlobal.Location = new System.Drawing.Point(4, 44);
            this.tabPageGlobal.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageGlobal.Name = "tabPageGlobal";
            this.tabPageGlobal.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageGlobal.Size = new System.Drawing.Size(1266, 746);
            this.tabPageGlobal.TabIndex = 0;
            this.tabPageGlobal.Text = "GLOBAL     ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dEdit_ChartWindow_Sec);
            this.panel1.Controls.Add(this.dEditChartWindow_Minute);
            this.panel1.Controls.Add(this.dEditChartWindow_Hour);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Location = new System.Drawing.Point(286, 458);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 41);
            this.panel1.TabIndex = 124;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Green;
            this.label24.Location = new System.Drawing.Point(156, 8);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(93, 20);
            this.label24.TabIndex = 124;
            this.label24.Text = "[hh:mm:ss]";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(40, 8);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(14, 20);
            this.label23.TabIndex = 121;
            this.label23.Text = ":";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(92, 8);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(14, 20);
            this.label22.TabIndex = 120;
            this.label22.Text = ":";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label21.Location = new System.Drawing.Point(98, 462);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(115, 20);
            this.label21.TabIndex = 118;
            this.label21.Text = "Chart window:";
            // 
            // label20
            // 
            this.label20.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label20.Location = new System.Drawing.Point(68, 395);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(739, 32);
            this.label20.TabIndex = 117;
            this.label20.Text = "Determine how many data should be present on one screen of chart";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelAddressIP
            // 
            this.panelAddressIP.Controls.Add(this.btnSetIP);
            this.panelAddressIP.Controls.Add(this.label19);
            this.panelAddressIP.Controls.Add(this.label17);
            this.panelAddressIP.Controls.Add(this.label18);
            this.panelAddressIP.Controls.Add(this.dEditIP3);
            this.panelAddressIP.Controls.Add(this.dEditIP4);
            this.panelAddressIP.Controls.Add(this.dEditIP2);
            this.panelAddressIP.Controls.Add(this.dEditIP1);
            this.panelAddressIP.Controls.Add(this.label16);
            this.panelAddressIP.Location = new System.Drawing.Point(160, 299);
            this.panelAddressIP.Margin = new System.Windows.Forms.Padding(4);
            this.panelAddressIP.Name = "panelAddressIP";
            this.panelAddressIP.Size = new System.Drawing.Size(509, 51);
            this.panelAddressIP.TabIndex = 3;
            // 
            // btnSetIP
            // 
            this.btnSetIP.Location = new System.Drawing.Point(375, 9);
            this.btnSetIP.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(90, 38);
            this.btnSetIP.TabIndex = 123;
            this.btnSetIP.Text = "Set";
            this.btnSetIP.UseVisualStyleBackColor = true;
            this.btnSetIP.Click += new System.EventHandler(this.btnSetIP_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(285, 22);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 20);
            this.label19.TabIndex = 128;
            this.label19.Text = ".";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(228, 22);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 20);
            this.label17.TabIndex = 127;
            this.label17.Text = ".";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(171, 22);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 20);
            this.label18.TabIndex = 124;
            this.label18.Text = ".";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label16.Location = new System.Drawing.Point(21, 18);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 20);
            this.label16.TabIndex = 118;
            this.label16.Text = "Address IP:";
            // 
            // label15
            // 
            this.label15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label15.Location = new System.Drawing.Point(68, 622);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(578, 32);
            this.label15.TabIndex = 116;
            this.label15.Text = "Determine if program should generate random values";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label14.Location = new System.Drawing.Point(68, 80);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(430, 31);
            this.label14.TabIndex = 115;
            this.label14.Text = "Select language for application";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cBoxComm
            // 
            this.cBoxComm.FormattingEnabled = true;
            this.cBoxComm.Items.AddRange(new object[] {
            "USB",
            "TCP"});
            this.cBoxComm.Location = new System.Drawing.Point(292, 261);
            this.cBoxComm.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxComm.Name = "cBoxComm";
            this.cBoxComm.Size = new System.Drawing.Size(132, 28);
            this.cBoxComm.TabIndex = 2;
            this.cBoxComm.SelectedIndexChanged += new System.EventHandler(this.cBoxComm_SelectedIndexChanged);
            // 
            // cBoxDummyMode
            // 
            this.cBoxDummyMode.AutoSize = true;
            this.cBoxDummyMode.Location = new System.Drawing.Point(292, 668);
            this.cBoxDummyMode.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxDummyMode.Name = "cBoxDummyMode";
            this.cBoxDummyMode.Size = new System.Drawing.Size(135, 24);
            this.cBoxDummyMode.TabIndex = 4;
            this.cBoxDummyMode.Text = "Dummy mode";
            this.cBoxDummyMode.UseVisualStyleBackColor = true;
            this.cBoxDummyMode.CheckedChanged += new System.EventHandler(this.cBoxDummyMode_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label4.Location = new System.Drawing.Point(98, 668);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.TabIndex = 110;
            this.label4.Text = "Dummy mode:";
            // 
            // labConnectionExplain
            // 
            this.labConnectionExplain.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labConnectionExplain.Location = new System.Drawing.Point(68, 212);
            this.labConnectionExplain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labConnectionExplain.Name = "labConnectionExplain";
            this.labConnectionExplain.Size = new System.Drawing.Size(430, 30);
            this.labConnectionExplain.TabIndex = 114;
            this.labConnectionExplain.Text = "Select type of connection with PLC";
            this.labConnectionExplain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(98, 269);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 112;
            this.label1.Text = "Connection type:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label45.Location = new System.Drawing.Point(98, 125);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(146, 20);
            this.label45.TabIndex = 108;
            this.label45.Text = "Start-up language:";
            // 
            // cBoxLanguge
            // 
            this.cBoxLanguge.FormattingEnabled = true;
            this.cBoxLanguge.Items.AddRange(new object[] {
            "English"});
            this.cBoxLanguge.Location = new System.Drawing.Point(292, 122);
            this.cBoxLanguge.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxLanguge.Name = "cBoxLanguge";
            this.cBoxLanguge.Size = new System.Drawing.Size(142, 28);
            this.cBoxLanguge.TabIndex = 1;
            // 
            // tabPageAcq
            // 
            this.tabPageAcq.BackColor = System.Drawing.Color.White;
            this.tabPageAcq.Controls.Add(this.grBoxAcq);
            this.tabPageAcq.Location = new System.Drawing.Point(4, 44);
            this.tabPageAcq.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageAcq.Name = "tabPageAcq";
            this.tabPageAcq.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageAcq.Size = new System.Drawing.Size(1266, 746);
            this.tabPageAcq.TabIndex = 1;
            this.tabPageAcq.Text = "ACQUISITION ";
            // 
            // grBoxAcq
            // 
            this.grBoxAcq.Controls.Add(this.cBoxAskAcq);
            this.grBoxAcq.Controls.Add(this.label25);
            this.grBoxAcq.Controls.Add(this.grBoxAcqPara);
            this.grBoxAcq.Controls.Add(this.cBoxEnabledAcq);
            this.grBoxAcq.Controls.Add(this.label13);
            this.grBoxAcq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoxAcq.Location = new System.Drawing.Point(2, 2);
            this.grBoxAcq.Margin = new System.Windows.Forms.Padding(2);
            this.grBoxAcq.Name = "grBoxAcq";
            this.grBoxAcq.Padding = new System.Windows.Forms.Padding(2);
            this.grBoxAcq.Size = new System.Drawing.Size(1262, 742);
            this.grBoxAcq.TabIndex = 8;
            this.grBoxAcq.TabStop = false;
            this.grBoxAcq.Text = "Acquisition configuration";
            // 
            // cBoxAskAcq
            // 
            this.cBoxAskAcq.AutoSize = true;
            this.cBoxAskAcq.Location = new System.Drawing.Point(711, 57);
            this.cBoxAskAcq.Name = "cBoxAskAcq";
            this.cBoxAskAcq.Size = new System.Drawing.Size(113, 24);
            this.cBoxAskAcq.TabIndex = 112;
            this.cBoxAskAcq.Text = "Enable ask";
            this.cBoxAskAcq.UseVisualStyleBackColor = true;
            this.cBoxAskAcq.CheckedChanged += new System.EventHandler(this.cBoxAskAcq_CheckedChanged);
            // 
            // label25
            // 
            this.label25.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label25.Location = new System.Drawing.Point(670, 22);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(566, 22);
            this.label25.TabIndex = 111;
            this.label25.Text = "Determine if ask when acquisition of data is disabled";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grBoxAcqPara
            // 
            this.grBoxAcqPara.Controls.Add(this.rBtnAcqDuringProcess);
            this.grBoxAcqPara.Controls.Add(this.label40);
            this.grBoxAcqPara.Controls.Add(this.grBoxParameter);
            this.grBoxAcqPara.Controls.Add(this.dEditAcqPressure);
            this.grBoxAcqPara.Controls.Add(this.rBtnAcqAllTime);
            this.grBoxAcqPara.Controls.Add(this.label7);
            this.grBoxAcqPara.Controls.Add(this.labPressure);
            this.grBoxAcqPara.Controls.Add(this.labUnitPressure);
            this.grBoxAcqPara.Controls.Add(this.cBoxActivePressure);
            this.grBoxAcqPara.Location = new System.Drawing.Point(6, 88);
            this.grBoxAcqPara.Margin = new System.Windows.Forms.Padding(4);
            this.grBoxAcqPara.Name = "grBoxAcqPara";
            this.grBoxAcqPara.Padding = new System.Windows.Forms.Padding(4);
            this.grBoxAcqPara.Size = new System.Drawing.Size(1248, 630);
            this.grBoxAcqPara.TabIndex = 110;
            this.grBoxAcqPara.TabStop = false;
            // 
            // rBtnAcqDuringProcess
            // 
            this.rBtnAcqDuringProcess.AutoSize = true;
            this.rBtnAcqDuringProcess.Location = new System.Drawing.Point(661, 118);
            this.rBtnAcqDuringProcess.Margin = new System.Windows.Forms.Padding(2);
            this.rBtnAcqDuringProcess.Name = "rBtnAcqDuringProcess";
            this.rBtnAcqDuringProcess.Size = new System.Drawing.Size(233, 24);
            this.rBtnAcqDuringProcess.TabIndex = 6;
            this.rBtnAcqDuringProcess.TabStop = true;
            this.rBtnAcqDuringProcess.Text = "Acquisioton during process";
            this.rBtnAcqDuringProcess.UseVisualStyleBackColor = true;
            this.rBtnAcqDuringProcess.Visible = false;
            this.rBtnAcqDuringProcess.CheckedChanged += new System.EventHandler(this.rBtnAcqDuringProcess_CheckedChanged);
            // 
            // label40
            // 
            this.label40.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label40.Location = new System.Drawing.Point(20, 16);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(812, 52);
            this.label40.TabIndex = 100;
            this.label40.Text = "Parameter determine level of pressure when parameter data of chamber will be save" +
    "d in data archive. Data will be saved when pressure will be less than set value";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grBoxParameter
            // 
            this.grBoxParameter.Controls.Add(this.listViewDevices);
            this.grBoxParameter.Controls.Add(this.label12);
            this.grBoxParameter.Controls.Add(this.label10);
            this.grBoxParameter.Controls.Add(this.btnSave);
            this.grBoxParameter.Controls.Add(this.cBoxMode);
            this.grBoxParameter.Controls.Add(this.treeViewDevices);
            this.grBoxParameter.Controls.Add(this.label11);
            this.grBoxParameter.Controls.Add(this.label2);
            this.grBoxParameter.Controls.Add(this.cBoxParaActive);
            this.grBoxParameter.Controls.Add(this.label9);
            this.grBoxParameter.Controls.Add(this.label8);
            this.grBoxParameter.Controls.Add(this.labParaUnit);
            this.grBoxParameter.Controls.Add(this.dEditDifferencesValue);
            this.grBoxParameter.Controls.Add(this.label3);
            this.grBoxParameter.Controls.Add(this.dEditFrqAcq);
            this.grBoxParameter.Controls.Add(this.label5);
            this.grBoxParameter.Controls.Add(this.label6);
            this.grBoxParameter.Location = new System.Drawing.Point(8, 139);
            this.grBoxParameter.Margin = new System.Windows.Forms.Padding(2);
            this.grBoxParameter.Name = "grBoxParameter";
            this.grBoxParameter.Padding = new System.Windows.Forms.Padding(2);
            this.grBoxParameter.Size = new System.Drawing.Size(1234, 481);
            this.grBoxParameter.TabIndex = 5;
            this.grBoxParameter.TabStop = false;
            // 
            // listViewDevices
            // 
            this.listViewDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.GridLines = true;
            this.listViewDevices.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listViewDevices.Location = new System.Drawing.Point(14, 56);
            this.listViewDevices.Margin = new System.Windows.Forms.Padding(4);
            this.listViewDevices.MultiSelect = false;
            this.listViewDevices.Name = "listViewDevices";
            this.listViewDevices.OwnerDraw = true;
            this.listViewDevices.Size = new System.Drawing.Size(356, 368);
            this.listViewDevices.TabIndex = 110;
            this.listViewDevices.UseCompatibleStateImageBehavior = false;
            this.listViewDevices.View = System.Windows.Forms.View.Details;
            this.listViewDevices.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listViewDevices_DrawColumnHeader);
            this.listViewDevices.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listViewDevices_DrawItem);
            this.listViewDevices.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listViewDevices_DrawSubItem);
            this.listViewDevices.SelectedIndexChanged += new System.EventHandler(this.listViewDevices_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Channel name";
            this.columnHeader1.Width = 250;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label12.Location = new System.Drawing.Point(451, 421);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 20);
            this.label12.TabIndex = 109;
            this.label12.Text = "Mode:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(146, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "List channels data";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 429);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(356, 39);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save configuration";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cBoxMode
            // 
            this.cBoxMode.FormattingEnabled = true;
            this.cBoxMode.Location = new System.Drawing.Point(650, 417);
            this.cBoxMode.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxMode.Name = "cBoxMode";
            this.cBoxMode.Size = new System.Drawing.Size(148, 28);
            this.cBoxMode.TabIndex = 11;
            this.cBoxMode.SelectedIndexChanged += new System.EventHandler(this.cBoxMode_SelectedValueChanged);
            // 
            // treeViewDevices
            // 
            this.treeViewDevices.Location = new System.Drawing.Point(969, 25);
            this.treeViewDevices.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewDevices.Name = "treeViewDevices";
            this.treeViewDevices.Size = new System.Drawing.Size(134, 49);
            this.treeViewDevices.TabIndex = 1;
            this.treeViewDevices.Visible = false;
            this.treeViewDevices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDevices_AfterSelect);
            // 
            // label11
            // 
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(415, 381);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(770, 22);
            this.label11.TabIndex = 107;
            this.label11.Text = "Determines is value will be save on event frequency , difference value or mixed b" +
    "oth";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(406, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(792, 22);
            this.label2.TabIndex = 106;
            this.label2.Text = "Determine is value of channel is saved in archive";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cBoxParaActive
            // 
            this.cBoxParaActive.AutoSize = true;
            this.cBoxParaActive.Location = new System.Drawing.Point(456, 92);
            this.cBoxParaActive.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxParaActive.Name = "cBoxParaActive";
            this.cBoxParaActive.Size = new System.Drawing.Size(257, 24);
            this.cBoxParaActive.TabIndex = 8;
            this.cBoxParaActive.Text = "Enabled parameter acquisition";
            this.cBoxParaActive.UseVisualStyleBackColor = true;
            this.cBoxParaActive.CheckedChanged += new System.EventHandler(this.cBoxParaActive_CheckedChanged);
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(406, 246);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(796, 42);
            this.label9.TabIndex = 104;
            this.label9.Text = "Parameter determines level of differences between actual and last value when data" +
    " of chamber parameters should be saved in archive.";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label8.Location = new System.Drawing.Point(406, 140);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(796, 24);
            this.label8.TabIndex = 103;
            this.label8.Text = "Parameter determines how often data of chamber parameters should be saved in  arc" +
    "hive.";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labParaUnit
            // 
            this.labParaUnit.AutoSize = true;
            this.labParaUnit.ForeColor = System.Drawing.Color.Green;
            this.labParaUnit.Location = new System.Drawing.Point(830, 309);
            this.labParaUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labParaUnit.Name = "labParaUnit";
            this.labParaUnit.Size = new System.Drawing.Size(35, 20);
            this.labParaUnit.TabIndex = 5;
            this.labParaUnit.Text = "[W]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(451, 313);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Difference values:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(830, 184);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "[s]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(451, 184);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Frequency:";
            // 
            // rBtnAcqAllTime
            // 
            this.rBtnAcqAllTime.AutoSize = true;
            this.rBtnAcqAllTime.Enabled = false;
            this.rBtnAcqAllTime.Location = new System.Drawing.Point(977, 118);
            this.rBtnAcqAllTime.Margin = new System.Windows.Forms.Padding(2);
            this.rBtnAcqAllTime.Name = "rBtnAcqAllTime";
            this.rBtnAcqAllTime.Size = new System.Drawing.Size(171, 24);
            this.rBtnAcqAllTime.TabIndex = 7;
            this.rBtnAcqAllTime.TabStop = true;
            this.rBtnAcqAllTime.Text = "Acquisition all time";
            this.rBtnAcqAllTime.UseVisualStyleBackColor = true;
            this.rBtnAcqAllTime.Visible = false;
            this.rBtnAcqAllTime.CheckedChanged += new System.EventHandler(this.rBtnAcqAllTime_CheckedChanged);
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(20, 118);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(1074, 25);
            this.label7.TabIndex = 102;
            this.label7.Text = "Determines when channels data of chamber should be saved in data archive only dur" +
    "ing a process or all time";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Visible = false;
            // 
            // labPressure
            // 
            this.labPressure.AutoSize = true;
            this.labPressure.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labPressure.Location = new System.Drawing.Point(52, 85);
            this.labPressure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPressure.Name = "labPressure";
            this.labPressure.Size = new System.Drawing.Size(153, 20);
            this.labPressure.TabIndex = 3;
            this.labPressure.Text = "Start acq pressure:";
            // 
            // labUnitPressure
            // 
            this.labUnitPressure.AutoSize = true;
            this.labUnitPressure.ForeColor = System.Drawing.Color.Green;
            this.labUnitPressure.Location = new System.Drawing.Point(439, 85);
            this.labUnitPressure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labUnitPressure.Name = "labUnitPressure";
            this.labUnitPressure.Size = new System.Drawing.Size(60, 20);
            this.labUnitPressure.TabIndex = 4;
            this.labUnitPressure.Text = "[mBar]";
            // 
            // cBoxActivePressure
            // 
            this.cBoxActivePressure.AutoSize = true;
            this.cBoxActivePressure.Location = new System.Drawing.Point(551, 84);
            this.cBoxActivePressure.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxActivePressure.Name = "cBoxActivePressure";
            this.cBoxActivePressure.Size = new System.Drawing.Size(127, 24);
            this.cBoxActivePressure.TabIndex = 5;
            this.cBoxActivePressure.Text = "Active option";
            this.cBoxActivePressure.UseVisualStyleBackColor = true;
            this.cBoxActivePressure.CheckedChanged += new System.EventHandler(this.cBoxActivePressure_CheckedChanged);
            // 
            // cBoxEnabledAcq
            // 
            this.cBoxEnabledAcq.AutoSize = true;
            this.cBoxEnabledAcq.Location = new System.Drawing.Point(62, 62);
            this.cBoxEnabledAcq.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxEnabledAcq.Name = "cBoxEnabledAcq";
            this.cBoxEnabledAcq.Size = new System.Drawing.Size(206, 24);
            this.cBoxEnabledAcq.TabIndex = 3;
            this.cBoxEnabledAcq.Text = "Enabled live acquisition";
            this.cBoxEnabledAcq.UseVisualStyleBackColor = true;
            this.cBoxEnabledAcq.CheckedChanged += new System.EventHandler(this.cBoxEnabledAcq_CheckedChanged);
            // 
            // label13
            // 
            this.label13.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label13.Location = new System.Drawing.Point(26, 29);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(584, 22);
            this.label13.TabIndex = 107;
            this.label13.Text = "Determine if data should be automatically saved in archive";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dEdit_ChartWindow_Sec
            // 
            this.dEdit_ChartWindow_Sec.Location = new System.Drawing.Point(112, 6);
            this.dEdit_ChartWindow_Sec.Margin = new System.Windows.Forms.Padding(5);
            this.dEdit_ChartWindow_Sec.Mask = "0";
            this.dEdit_ChartWindow_Sec.MaximumValue = 59D;
            this.dEdit_ChartWindow_Sec.MinimumValue = 0D;
            this.dEdit_ChartWindow_Sec.Name = "dEdit_ChartWindow_Sec";
            this.dEdit_ChartWindow_Sec.ReadOnly = false;
            this.dEdit_ChartWindow_Sec.Size = new System.Drawing.Size(31, 31);
            this.dEdit_ChartWindow_Sec.TabIndex = 126;
            this.dEdit_ChartWindow_Sec.Value = 59D;
            this.dEdit_ChartWindow_Sec.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.eEditChartWindow_Hour_EnterOn);
            // 
            // dEditChartWindow_Minute
            // 
            this.dEditChartWindow_Minute.Location = new System.Drawing.Point(56, 6);
            this.dEditChartWindow_Minute.Margin = new System.Windows.Forms.Padding(5);
            this.dEditChartWindow_Minute.Mask = "0";
            this.dEditChartWindow_Minute.MaximumValue = 59D;
            this.dEditChartWindow_Minute.MinimumValue = 0D;
            this.dEditChartWindow_Minute.Name = "dEditChartWindow_Minute";
            this.dEditChartWindow_Minute.ReadOnly = false;
            this.dEditChartWindow_Minute.Size = new System.Drawing.Size(31, 31);
            this.dEditChartWindow_Minute.TabIndex = 127;
            this.dEditChartWindow_Minute.Value = 36D;
            this.dEditChartWindow_Minute.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.eEditChartWindow_Hour_EnterOn);
            // 
            // dEditChartWindow_Hour
            // 
            this.dEditChartWindow_Hour.Location = new System.Drawing.Point(2, 6);
            this.dEditChartWindow_Hour.Margin = new System.Windows.Forms.Padding(4);
            this.dEditChartWindow_Hour.Mask = "0";
            this.dEditChartWindow_Hour.MaximumValue = 24D;
            this.dEditChartWindow_Hour.MinimumValue = 0D;
            this.dEditChartWindow_Hour.Name = "dEditChartWindow_Hour";
            this.dEditChartWindow_Hour.ReadOnly = false;
            this.dEditChartWindow_Hour.Size = new System.Drawing.Size(31, 31);
            this.dEditChartWindow_Hour.TabIndex = 125;
            this.dEditChartWindow_Hour.Value = 24D;
            this.dEditChartWindow_Hour.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.eEditChartWindow_Hour_EnterOn);
            // 
            // dEditIP3
            // 
            this.dEditIP3.Location = new System.Drawing.Point(239, 12);
            this.dEditIP3.Margin = new System.Windows.Forms.Padding(4);
            this.dEditIP3.Mask = "0";
            this.dEditIP3.MaximumValue = 255D;
            this.dEditIP3.MinimumValue = 0D;
            this.dEditIP3.Name = "dEditIP3";
            this.dEditIP3.ReadOnly = false;
            this.dEditIP3.Size = new System.Drawing.Size(44, 29);
            this.dEditIP3.TabIndex = 121;
            this.dEditIP3.Value = 1D;
            // 
            // dEditIP4
            // 
            this.dEditIP4.Location = new System.Drawing.Point(298, 12);
            this.dEditIP4.Margin = new System.Windows.Forms.Padding(4);
            this.dEditIP4.Mask = "0";
            this.dEditIP4.MaximumValue = 255D;
            this.dEditIP4.MinimumValue = 0D;
            this.dEditIP4.Name = "dEditIP4";
            this.dEditIP4.ReadOnly = false;
            this.dEditIP4.Size = new System.Drawing.Size(44, 29);
            this.dEditIP4.TabIndex = 122;
            this.dEditIP4.Value = 1D;
            // 
            // dEditIP2
            // 
            this.dEditIP2.Location = new System.Drawing.Point(182, 12);
            this.dEditIP2.Margin = new System.Windows.Forms.Padding(4);
            this.dEditIP2.Mask = "0";
            this.dEditIP2.MaximumValue = 255D;
            this.dEditIP2.MinimumValue = 0D;
            this.dEditIP2.Name = "dEditIP2";
            this.dEditIP2.ReadOnly = false;
            this.dEditIP2.Size = new System.Drawing.Size(44, 30);
            this.dEditIP2.TabIndex = 120;
            this.dEditIP2.Value = 168D;
            // 
            // dEditIP1
            // 
            this.dEditIP1.Location = new System.Drawing.Point(128, 12);
            this.dEditIP1.Margin = new System.Windows.Forms.Padding(4);
            this.dEditIP1.Mask = "0";
            this.dEditIP1.MaximumValue = 255D;
            this.dEditIP1.MinimumValue = 0D;
            this.dEditIP1.Name = "dEditIP1";
            this.dEditIP1.ReadOnly = false;
            this.dEditIP1.Size = new System.Drawing.Size(44, 30);
            this.dEditIP1.TabIndex = 119;
            this.dEditIP1.Value = 192D;
            // 
            // dEditDifferencesValue
            // 
            this.dEditDifferencesValue.Location = new System.Drawing.Point(650, 309);
            this.dEditDifferencesValue.Margin = new System.Windows.Forms.Padding(2);
            this.dEditDifferencesValue.Mask = "0.000";
            this.dEditDifferencesValue.MaximumValue = 10000000D;
            this.dEditDifferencesValue.MinimumValue = 0D;
            this.dEditDifferencesValue.Name = "dEditDifferencesValue";
            this.dEditDifferencesValue.ReadOnly = false;
            this.dEditDifferencesValue.Size = new System.Drawing.Size(148, 29);
            this.dEditDifferencesValue.TabIndex = 10;
            this.dEditDifferencesValue.Value = 0D;
            this.dEditDifferencesValue.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditDifferencesValue_EnterOn);
            // 
            // dEditFrqAcq
            // 
            this.dEditFrqAcq.Location = new System.Drawing.Point(650, 181);
            this.dEditFrqAcq.Margin = new System.Windows.Forms.Padding(2);
            this.dEditFrqAcq.Mask = "0.000";
            this.dEditFrqAcq.MaximumValue = 3600000D;
            this.dEditFrqAcq.MinimumValue = 0.5D;
            this.dEditFrqAcq.Name = "dEditFrqAcq";
            this.dEditFrqAcq.ReadOnly = false;
            this.dEditFrqAcq.Size = new System.Drawing.Size(148, 29);
            this.dEditFrqAcq.TabIndex = 9;
            this.dEditFrqAcq.Value = 0.5D;
            this.dEditFrqAcq.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFrqAcq_EnterOn);
            // 
            // dEditAcqPressure
            // 
            this.dEditAcqPressure.Location = new System.Drawing.Point(244, 80);
            this.dEditAcqPressure.Margin = new System.Windows.Forms.Padding(5);
            this.dEditAcqPressure.Mask = "0.000";
            this.dEditAcqPressure.MaximumValue = 1200D;
            this.dEditAcqPressure.MinimumValue = 0D;
            this.dEditAcqPressure.Name = "dEditAcqPressure";
            this.dEditAcqPressure.ReadOnly = false;
            this.dEditAcqPressure.Size = new System.Drawing.Size(146, 34);
            this.dEditAcqPressure.TabIndex = 4;
            this.dEditAcqPressure.Value = 0D;
            this.dEditAcqPressure.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditAcqPressure_EnterOn);
            // 
            // SettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsPanel";
            this.Size = new System.Drawing.Size(1274, 794);
            this.Load += new System.EventHandler(this.SettingsPanel_Load);
            this.VisibleChanged += new System.EventHandler(this.SettingsPanel_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageGlobal.ResumeLayout(false);
            this.tabPageGlobal.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelAddressIP.ResumeLayout(false);
            this.panelAddressIP.PerformLayout();
            this.tabPageAcq.ResumeLayout(false);
            this.grBoxAcq.ResumeLayout(false);
            this.grBoxAcq.PerformLayout();
            this.grBoxAcqPara.ResumeLayout(false);
            this.grBoxAcqPara.PerformLayout();
            this.grBoxParameter.ResumeLayout(false);
            this.grBoxParameter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGlobal;
        private System.Windows.Forms.ComboBox cBoxComm;
        private System.Windows.Forms.CheckBox cBoxDummyMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labConnectionExplain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox cBoxLanguge;
        private System.Windows.Forms.TabPage tabPageAcq;
        private System.Windows.Forms.GroupBox grBoxAcq;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panelAddressIP;
        private System.Windows.Forms.Button btnSetIP;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private Cotrols.DoubleEdit dEditIP3;
        private Cotrols.DoubleEdit dEditIP4;
        private Cotrols.DoubleEdit dEditIP2;
        private Cotrols.DoubleEdit dEditIP1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private Cotrols.DoubleEdit dEdit_ChartWindow_Sec;
        private Cotrols.DoubleEdit dEditChartWindow_Minute;
        private Cotrols.DoubleEdit dEditChartWindow_Hour;
        private System.Windows.Forms.CheckBox cBoxEnabledAcq;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TreeView treeViewDevices;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rBtnAcqDuringProcess;
        private System.Windows.Forms.RadioButton rBtnAcqAllTime;
        private System.Windows.Forms.CheckBox cBoxActivePressure;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label40;
        private Cotrols.DoubleEdit dEditAcqPressure;
        private System.Windows.Forms.Label labUnitPressure;
        private System.Windows.Forms.Label labPressure;
        private System.Windows.Forms.GroupBox grBoxParameter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cBoxMode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cBoxParaActive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labParaUnit;
        private Cotrols.DoubleEdit dEditDifferencesValue;
        private System.Windows.Forms.Label label3;
        private Cotrols.DoubleEdit dEditFrqAcq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grBoxAcqPara;
        private System.Windows.Forms.ListView listViewDevices;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox cBoxAskAcq;
        private System.Windows.Forms.Label label25;
    }
}
