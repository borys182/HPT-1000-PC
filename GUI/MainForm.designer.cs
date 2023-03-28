namespace HPT1000.GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnLogin = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnLogout = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labStatusAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.borderLab1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnConfirm = new System.Windows.Forms.ToolStripSplitButton();
            this.borderLab2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labStatusMsgAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripStatusLabel();
            this.labStatusMaintance = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.liveGraphicalPanel = new HPT1000.GUI.GraphicalLive();
            this.grBoxSystem = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labVersion = new System.Windows.Forms.Label();
            this.motorPanel2 = new HPT1000.GUI.MotorPanel();
            this.chamberPanel = new HPT1000.GUI.ChamberPanel();
            this.flowMFC3 = new HPT1000.FlowGasPanel();
            this.flowMFC2 = new HPT1000.FlowGasPanel();
            this.flowMFC1 = new HPT1000.FlowGasPanel();
            this.mfcPanel3 = new HPT1000.GUI.MFCPanel();
            this.mfcPanel2 = new HPT1000.GUI.MFCPanel();
            this.mfcPanel1 = new HPT1000.GUI.MFCPanel();
            this.pictureCornerUp = new System.Windows.Forms.PictureBox();
            this.picturelineMFC = new System.Windows.Forms.PictureBox();
            this.pumpComponent = new HPT1000.GUI.PumpComponent();
            this.valve_Vent = new HPT1000.GUI.ValvePanel();
            this.valve_SV = new HPT1000.GUI.ValvePanel();
            this.valve_Gas = new HPT1000.GUI.ValvePanel();
            this.valve_Purge = new HPT1000.GUI.ValvePanel();
            this.pictureArrowUp1 = new System.Windows.Forms.PictureBox();
            this.pictureArrowUp2 = new System.Windows.Forms.PictureBox();
            this.pictureArrowDown = new System.Windows.Forms.PictureBox();
            this.pictureLineVaporizer = new System.Windows.Forms.PictureBox();
            this.pictureCornerDown = new System.Windows.Forms.PictureBox();
            this.pictureBoxLineGV = new System.Windows.Forms.PictureBox();
            this.pictureLineMFC3 = new System.Windows.Forms.PictureBox();
            this.vaporiserPanel = new HPT1000.GUI.VaporiserPanel();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.pictureBox28 = new System.Windows.Forms.PictureBox();
            this.pictureBox26 = new System.Windows.Forms.PictureBox();
            this.pictureLineMFC2 = new System.Windows.Forms.PictureBox();
            this.pictureLineMFC1 = new System.Windows.Forms.PictureBox();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.pictureCornerUp2 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.pictureCornerUp1 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.generatorPanel = new HPT1000.GUI.GeneratorPanel();
            this.motorPanel1 = new HPT1000.GUI.MotorPanel();
            this.pressurePanel = new HPT1000.GUI.PressurePanel();
            this.btnLiveModeData = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.interlockPanel_Vacuum = new HPT1000.GUI.InterlockPanel();
            this.interlockPanel_HV = new HPT1000.GUI.InterlockPanel();
            this.interlockPanel_Thermal = new HPT1000.GUI.InterlockPanel();
            this.interlockPanel_Door = new HPT1000.GUI.InterlockPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.programPanel = new HPT1000.GUI.ProgramPanel();
            this.tabPagePrograms = new System.Windows.Forms.TabPage();
            this.programsConfigPanel = new HPT1000.GUI.ProgramsConfigPanel();
            this.tabPageAlerts = new System.Windows.Forms.TabPage();
            this.alertsPanel = new HPT1000.GUI.AlertsPanel();
            this.tabPageArchive = new System.Windows.Forms.TabPage();
            this.archivePanel = new HPT1000.GUI.ArchivePanel();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.settingsPanel = new HPT1000.GUI.SettingsPanel();
            this.tabPageMaintenance = new System.Windows.Forms.TabPage();
            this.maintancePanel = new HPT1000.GUI.MaintancePanel();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.servicePanel = new HPT1000.GUI.ServicePanel();
            this.tabPageUser = new System.Windows.Forms.TabPage();
            this.userManagerPanel = new HPT1000.GUI.UserManagerPanel();
            this.adminPanel = new HPT1000.GUI.AdminPanel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerKeyboard = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.grBoxSystem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturelineMFC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArrowUp1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArrowUp2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArrowDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineVaporizer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLineGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineMFC3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineMFC2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineMFC1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerUp2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerUp1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPagePrograms.SuspendLayout();
            this.tabPageAlerts.SuspendLayout();
            this.tabPageArchive.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabPageMaintenance.SuspendLayout();
            this.tabPageService.SuspendLayout();
            this.tabPageUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(9, 8);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(230, 35);
            this.btnLogin.TabIndex = 20;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Program.png");
            this.imageList1.Images.SetKeyName(1, "Subprogram.png");
            this.imageList1.Images.SetKeyName(2, "Pump.jpg");
            this.imageList1.Images.SetKeyName(3, "Gas.png");
            this.imageList1.Images.SetKeyName(4, "Plasma.jpg");
            this.imageList1.Images.SetKeyName(5, "Purge.jpg");
            this.imageList1.Images.SetKeyName(6, "Vent.png");
            this.imageList1.Images.SetKeyName(7, "Valve.png");
            this.imageList1.Images.SetKeyName(8, "test.png");
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(244, 8);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(230, 35);
            this.btnLogout.TabIndex = 23;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labStatusUser,
            this.toolStripStatusLabel1,
            this.statusLabel,
            this.toolStripStatusLabel2,
            this.labStatusAction,
            this.borderLab1,
            this.btnConfirm,
            this.borderLab2,
            this.labStatusMsgAction,
            this.toolStripSeparator,
            this.labStatusMaintance});
            this.statusStrip1.Location = new System.Drawing.Point(0, 854);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1282, 26);
            this.statusStrip1.TabIndex = 25;
            // 
            // labStatusUser
            // 
            this.labStatusUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labStatusUser.Name = "labStatusUser";
            this.labStatusUser.Size = new System.Drawing.Size(129, 21);
            this.labStatusUser.Text = "Login user: None";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(114, 21);
            this.statusLabel.Text = "Connection fail";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // labStatusAction
            // 
            this.labStatusAction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labStatusAction.ForeColor = System.Drawing.Color.Green;
            this.labStatusAction.Name = "labStatusAction";
            this.labStatusAction.Size = new System.Drawing.Size(169, 21);
            this.labStatusAction.Text = "Correct set parameters";
            // 
            // borderLab1
            // 
            this.borderLab1.Name = "borderLab1";
            this.borderLab1.Size = new System.Drawing.Size(13, 21);
            this.borderLab1.Text = "|";
            // 
            // btnConfirm
            // 
            this.btnConfirm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConfirm.DropDownButtonWidth = 0;
            this.btnConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(78, 24);
            this.btnConfirm.Text = "CONFIRM";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // borderLab2
            // 
            this.borderLab2.Name = "borderLab2";
            this.borderLab2.Size = new System.Drawing.Size(13, 21);
            this.borderLab2.Text = "|";
            // 
            // labStatusMsgAction
            // 
            this.labStatusMsgAction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labStatusMsgAction.ForeColor = System.Drawing.Color.Green;
            this.labStatusMsgAction.Name = "labStatusMsgAction";
            this.labStatusMsgAction.Size = new System.Drawing.Size(81, 21);
            this.labStatusMsgAction.Text = "Event logs";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(13, 21);
            this.toolStripSeparator.Text = "|";
            // 
            // labStatusMaintance
            // 
            this.labStatusMaintance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labStatusMaintance.ForeColor = System.Drawing.Color.Red;
            this.labStatusMaintance.Name = "labStatusMaintance";
            this.labStatusMaintance.Size = new System.Drawing.Size(202, 21);
            this.labStatusMaintance.Text = "PLEASE MAKE MAINTANCE";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPagePrograms);
            this.tabControlMain.Controls.Add(this.tabPageAlerts);
            this.tabControlMain.Controls.Add(this.tabPageArchive);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Controls.Add(this.tabPageMaintenance);
            this.tabControlMain.Controls.Add(this.tabPageService);
            this.tabControlMain.Controls.Add(this.tabPageUser);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControlMain.ImageList = this.imageList1;
            this.tabControlMain.ItemSize = new System.Drawing.Size(120, 45);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlMain.Multiline = true;
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.ShowToolTips = true;
            this.tabControlMain.Size = new System.Drawing.Size(1282, 854);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 26;
            this.tabControlMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlMain_DrawItem);
            this.tabControlMain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlMain_Selecting);
            // 
            // tabPageMain
            // 
            this.tabPageMain.BackColor = System.Drawing.Color.Transparent;
            this.tabPageMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageMain.Controls.Add(this.btnLogout);
            this.tabPageMain.Controls.Add(this.liveGraphicalPanel);
            this.tabPageMain.Controls.Add(this.grBoxSystem);
            this.tabPageMain.Controls.Add(this.btnLiveModeData);
            this.tabPageMain.Controls.Add(this.groupBox2);
            this.tabPageMain.Controls.Add(this.groupBox1);
            this.tabPageMain.Controls.Add(this.btnLogin);
            this.tabPageMain.Location = new System.Drawing.Point(4, 49);
            this.tabPageMain.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageMain.Size = new System.Drawing.Size(1274, 801);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "MAIN SCREEN     ";
            // 
            // liveGraphicalPanel
            // 
            this.liveGraphicalPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.liveGraphicalPanel.Location = new System.Drawing.Point(1321, 752);
            this.liveGraphicalPanel.Margin = new System.Windows.Forms.Padding(2);
            this.liveGraphicalPanel.Name = "liveGraphicalPanel";
            this.liveGraphicalPanel.Size = new System.Drawing.Size(71, 39);
            this.liveGraphicalPanel.TabIndex = 110;
            // 
            // grBoxSystem
            // 
            this.grBoxSystem.Controls.Add(this.label3);
            this.grBoxSystem.Controls.Add(this.labVersion);
            this.grBoxSystem.Controls.Add(this.motorPanel2);
            this.grBoxSystem.Controls.Add(this.chamberPanel);
            this.grBoxSystem.Controls.Add(this.flowMFC3);
            this.grBoxSystem.Controls.Add(this.flowMFC2);
            this.grBoxSystem.Controls.Add(this.flowMFC1);
            this.grBoxSystem.Controls.Add(this.mfcPanel3);
            this.grBoxSystem.Controls.Add(this.mfcPanel2);
            this.grBoxSystem.Controls.Add(this.mfcPanel1);
            this.grBoxSystem.Controls.Add(this.pictureCornerUp);
            this.grBoxSystem.Controls.Add(this.picturelineMFC);
            this.grBoxSystem.Controls.Add(this.pumpComponent);
            this.grBoxSystem.Controls.Add(this.valve_Vent);
            this.grBoxSystem.Controls.Add(this.valve_SV);
            this.grBoxSystem.Controls.Add(this.valve_Gas);
            this.grBoxSystem.Controls.Add(this.valve_Purge);
            this.grBoxSystem.Controls.Add(this.pictureArrowUp1);
            this.grBoxSystem.Controls.Add(this.pictureArrowUp2);
            this.grBoxSystem.Controls.Add(this.pictureArrowDown);
            this.grBoxSystem.Controls.Add(this.pictureLineVaporizer);
            this.grBoxSystem.Controls.Add(this.pictureCornerDown);
            this.grBoxSystem.Controls.Add(this.pictureBoxLineGV);
            this.grBoxSystem.Controls.Add(this.pictureLineMFC3);
            this.grBoxSystem.Controls.Add(this.vaporiserPanel);
            this.grBoxSystem.Controls.Add(this.label37);
            this.grBoxSystem.Controls.Add(this.label36);
            this.grBoxSystem.Controls.Add(this.pictureBox28);
            this.grBoxSystem.Controls.Add(this.pictureBox26);
            this.grBoxSystem.Controls.Add(this.pictureLineMFC2);
            this.grBoxSystem.Controls.Add(this.pictureLineMFC1);
            this.grBoxSystem.Controls.Add(this.pictureBox17);
            this.grBoxSystem.Controls.Add(this.pictureCornerUp2);
            this.grBoxSystem.Controls.Add(this.pictureBox16);
            this.grBoxSystem.Controls.Add(this.pictureCornerUp1);
            this.grBoxSystem.Controls.Add(this.pictureBox8);
            this.grBoxSystem.Controls.Add(this.generatorPanel);
            this.grBoxSystem.Controls.Add(this.motorPanel1);
            this.grBoxSystem.Controls.Add(this.pressurePanel);
            this.grBoxSystem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.grBoxSystem.Location = new System.Drawing.Point(482, -1);
            this.grBoxSystem.Margin = new System.Windows.Forms.Padding(2);
            this.grBoxSystem.Name = "grBoxSystem";
            this.grBoxSystem.Padding = new System.Windows.Forms.Padding(2);
            this.grBoxSystem.Size = new System.Drawing.Size(782, 740);
            this.grBoxSystem.TabIndex = 25;
            this.grBoxSystem.TabStop = false;
            this.grBoxSystem.Text = "System";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(81, 702);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 119;
            this.label3.Text = "Vent";
            // 
            // labVersion
            // 
            this.labVersion.AutoSize = true;
            this.labVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labVersion.Location = new System.Drawing.Point(500, 715);
            this.labVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labVersion.Name = "labVersion";
            this.labVersion.Size = new System.Drawing.Size(260, 18);
            this.labVersion.TabIndex = 111;
            this.labVersion.Text = "Program version 1.0.10 Firmware 1.12";
            this.labVersion.Click += new System.EventHandler(this.labVersion_Click);
            // 
            // motorPanel2
            // 
            this.motorPanel2.BackColor = System.Drawing.Color.Transparent;
            this.motorPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.motorPanel2.Location = new System.Drawing.Point(301, 9);
            this.motorPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.motorPanel2.Name = "motorPanel2";
            this.motorPanel2.Size = new System.Drawing.Size(104, 149);
            this.motorPanel2.TabIndex = 118;
            // 
            // chamberPanel
            // 
            this.chamberPanel.Location = new System.Drawing.Point(156, 268);
            this.chamberPanel.Margin = new System.Windows.Forms.Padding(2);
            this.chamberPanel.Name = "chamberPanel";
            this.chamberPanel.Size = new System.Drawing.Size(130, 165);
            this.chamberPanel.TabIndex = 117;
            // 
            // flowMFC3
            // 
            this.flowMFC3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowMFC3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.flowMFC3.Location = new System.Drawing.Point(370, 509);
            this.flowMFC3.Margin = new System.Windows.Forms.Padding(6);
            this.flowMFC3.Name = "flowMFC3";
            this.flowMFC3.Size = new System.Drawing.Size(158, 18);
            this.flowMFC3.TabIndex = 116;
            // 
            // flowMFC2
            // 
            this.flowMFC2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowMFC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.flowMFC2.Location = new System.Drawing.Point(370, 369);
            this.flowMFC2.Margin = new System.Windows.Forms.Padding(5);
            this.flowMFC2.Name = "flowMFC2";
            this.flowMFC2.Size = new System.Drawing.Size(158, 18);
            this.flowMFC2.TabIndex = 115;
            // 
            // flowMFC1
            // 
            this.flowMFC1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowMFC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.flowMFC1.Location = new System.Drawing.Point(370, 228);
            this.flowMFC1.Margin = new System.Windows.Forms.Padding(5);
            this.flowMFC1.Name = "flowMFC1";
            this.flowMFC1.Size = new System.Drawing.Size(158, 16);
            this.flowMFC1.TabIndex = 114;
            // 
            // mfcPanel3
            // 
            this.mfcPanel3.BackColor = System.Drawing.Color.Transparent;
            this.mfcPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mfcPanel3.Location = new System.Drawing.Point(531, 449);
            this.mfcPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.mfcPanel3.Name = "mfcPanel3";
            this.mfcPanel3.Size = new System.Drawing.Size(248, 135);
            this.mfcPanel3.TabIndex = 113;
            // 
            // mfcPanel2
            // 
            this.mfcPanel2.BackColor = System.Drawing.Color.Transparent;
            this.mfcPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mfcPanel2.Location = new System.Drawing.Point(531, 311);
            this.mfcPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.mfcPanel2.Name = "mfcPanel2";
            this.mfcPanel2.Size = new System.Drawing.Size(248, 135);
            this.mfcPanel2.TabIndex = 112;
            // 
            // mfcPanel1
            // 
            this.mfcPanel1.BackColor = System.Drawing.Color.Transparent;
            this.mfcPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mfcPanel1.Location = new System.Drawing.Point(531, 168);
            this.mfcPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.mfcPanel1.Name = "mfcPanel1";
            this.mfcPanel1.Size = new System.Drawing.Size(248, 135);
            this.mfcPanel1.TabIndex = 111;
            // 
            // pictureCornerUp
            // 
            this.pictureCornerUp.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureCornerUp.ErrorImage")));
            this.pictureCornerUp.Image = ((System.Drawing.Image)(resources.GetObject("pictureCornerUp.Image")));
            this.pictureCornerUp.Location = new System.Drawing.Point(360, 250);
            this.pictureCornerUp.Margin = new System.Windows.Forms.Padding(2);
            this.pictureCornerUp.Name = "pictureCornerUp";
            this.pictureCornerUp.Size = new System.Drawing.Size(31, 31);
            this.pictureCornerUp.TabIndex = 109;
            this.pictureCornerUp.TabStop = false;
            // 
            // picturelineMFC
            // 
            this.picturelineMFC.Image = ((System.Drawing.Image)(resources.GetObject("picturelineMFC.Image")));
            this.picturelineMFC.Location = new System.Drawing.Point(360, 266);
            this.picturelineMFC.Margin = new System.Windows.Forms.Padding(2);
            this.picturelineMFC.Name = "picturelineMFC";
            this.picturelineMFC.Size = new System.Drawing.Size(4, 378);
            this.picturelineMFC.TabIndex = 108;
            this.picturelineMFC.TabStop = false;
            // 
            // pumpComponent
            // 
            this.pumpComponent.Location = new System.Drawing.Point(189, 559);
            this.pumpComponent.Margin = new System.Windows.Forms.Padding(5);
            this.pumpComponent.Name = "pumpComponent";
            this.pumpComponent.Size = new System.Drawing.Size(65, 52);
            this.pumpComponent.TabIndex = 107;
            // 
            // valve_Vent
            // 
            this.valve_Vent.Location = new System.Drawing.Point(88, 472);
            this.valve_Vent.Margin = new System.Windows.Forms.Padding(6);
            this.valve_Vent.Name = "valve_Vent";
            this.valve_Vent.Size = new System.Drawing.Size(30, 50);
            this.valve_Vent.TabIndex = 106;
            this.valve_Vent.Vertical = true;
            // 
            // valve_SV
            // 
            this.valve_SV.Location = new System.Drawing.Point(208, 472);
            this.valve_SV.Margin = new System.Windows.Forms.Padding(6);
            this.valve_SV.Name = "valve_SV";
            this.valve_SV.Size = new System.Drawing.Size(30, 50);
            this.valve_SV.TabIndex = 105;
            this.valve_SV.Vertical = true;
            // 
            // valve_Gas
            // 
            this.valve_Gas.Location = new System.Drawing.Point(295, 322);
            this.valve_Gas.Margin = new System.Windows.Forms.Padding(6);
            this.valve_Gas.Name = "valve_Gas";
            this.valve_Gas.Size = new System.Drawing.Size(50, 30);
            this.valve_Gas.TabIndex = 104;
            this.valve_Gas.Vertical = false;
            // 
            // valve_Purge
            // 
            this.valve_Purge.Location = new System.Drawing.Point(24, 472);
            this.valve_Purge.Margin = new System.Windows.Forms.Padding(5);
            this.valve_Purge.Name = "valve_Purge";
            this.valve_Purge.Size = new System.Drawing.Size(30, 50);
            this.valve_Purge.TabIndex = 103;
            this.valve_Purge.Vertical = true;
            // 
            // pictureArrowUp1
            // 
            this.pictureArrowUp1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureArrowUp1.ErrorImage")));
            this.pictureArrowUp1.Image = ((System.Drawing.Image)(resources.GetObject("pictureArrowUp1.Image")));
            this.pictureArrowUp1.Location = new System.Drawing.Point(21, 636);
            this.pictureArrowUp1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureArrowUp1.Name = "pictureArrowUp1";
            this.pictureArrowUp1.Size = new System.Drawing.Size(35, 61);
            this.pictureArrowUp1.TabIndex = 100;
            this.pictureArrowUp1.TabStop = false;
            // 
            // pictureArrowUp2
            // 
            this.pictureArrowUp2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureArrowUp2.ErrorImage")));
            this.pictureArrowUp2.Image = ((System.Drawing.Image)(resources.GetObject("pictureArrowUp2.Image")));
            this.pictureArrowUp2.Location = new System.Drawing.Point(85, 636);
            this.pictureArrowUp2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureArrowUp2.Name = "pictureArrowUp2";
            this.pictureArrowUp2.Size = new System.Drawing.Size(35, 61);
            this.pictureArrowUp2.TabIndex = 99;
            this.pictureArrowUp2.TabStop = false;
            // 
            // pictureArrowDown
            // 
            this.pictureArrowDown.ErrorImage = null;
            this.pictureArrowDown.Image = ((System.Drawing.Image)(resources.GetObject("pictureArrowDown.Image")));
            this.pictureArrowDown.Location = new System.Drawing.Point(208, 636);
            this.pictureArrowDown.Margin = new System.Windows.Forms.Padding(2);
            this.pictureArrowDown.Name = "pictureArrowDown";
            this.pictureArrowDown.Size = new System.Drawing.Size(31, 61);
            this.pictureArrowDown.TabIndex = 98;
            this.pictureArrowDown.TabStop = false;
            // 
            // pictureLineVaporizer
            // 
            this.pictureLineVaporizer.Image = ((System.Drawing.Image)(resources.GetObject("pictureLineVaporizer.Image")));
            this.pictureLineVaporizer.Location = new System.Drawing.Point(389, 671);
            this.pictureLineVaporizer.Margin = new System.Windows.Forms.Padding(2);
            this.pictureLineVaporizer.Name = "pictureLineVaporizer";
            this.pictureLineVaporizer.Size = new System.Drawing.Size(145, 4);
            this.pictureLineVaporizer.TabIndex = 97;
            this.pictureLineVaporizer.TabStop = false;
            // 
            // pictureCornerDown
            // 
            this.pictureCornerDown.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureCornerDown.ErrorImage")));
            this.pictureCornerDown.Image = ((System.Drawing.Image)(resources.GetObject("pictureCornerDown.Image")));
            this.pictureCornerDown.Location = new System.Drawing.Point(361, 649);
            this.pictureCornerDown.Margin = new System.Windows.Forms.Padding(2);
            this.pictureCornerDown.Name = "pictureCornerDown";
            this.pictureCornerDown.Size = new System.Drawing.Size(31, 31);
            this.pictureCornerDown.TabIndex = 96;
            this.pictureCornerDown.TabStop = false;
            // 
            // pictureBoxLineGV
            // 
            this.pictureBoxLineGV.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLineGV.Image")));
            this.pictureBoxLineGV.Location = new System.Drawing.Point(280, 338);
            this.pictureBoxLineGV.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxLineGV.Name = "pictureBoxLineGV";
            this.pictureBoxLineGV.Size = new System.Drawing.Size(82, 4);
            this.pictureBoxLineGV.TabIndex = 95;
            this.pictureBoxLineGV.TabStop = false;
            // 
            // pictureLineMFC3
            // 
            this.pictureLineMFC3.Image = ((System.Drawing.Image)(resources.GetObject("pictureLineMFC3.Image")));
            this.pictureLineMFC3.Location = new System.Drawing.Point(364, 534);
            this.pictureLineMFC3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureLineMFC3.Name = "pictureLineMFC3";
            this.pictureLineMFC3.Size = new System.Drawing.Size(215, 4);
            this.pictureLineMFC3.TabIndex = 94;
            this.pictureLineMFC3.TabStop = false;
            // 
            // vaporiserPanel
            // 
            this.vaporiserPanel.BackColor = System.Drawing.Color.Transparent;
            this.vaporiserPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vaporiserPanel.ForeColor = System.Drawing.Color.Transparent;
            this.vaporiserPanel.Location = new System.Drawing.Point(530, 588);
            this.vaporiserPanel.Margin = new System.Windows.Forms.Padding(2);
            this.vaporiserPanel.Name = "vaporiserPanel";
            this.vaporiserPanel.Size = new System.Drawing.Size(249, 124);
            this.vaporiserPanel.TabIndex = 92;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label37.Location = new System.Drawing.Point(172, 699);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label37.Size = new System.Drawing.Size(99, 20);
            this.label37.TabIndex = 90;
            this.label37.Text = "Atmosphere";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label36.Location = new System.Drawing.Point(14, 702);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label36.Size = new System.Drawing.Size(53, 20);
            this.label36.TabIndex = 89;
            this.label36.Text = "Purge";
            // 
            // pictureBox28
            // 
            this.pictureBox28.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox28.Image")));
            this.pictureBox28.Location = new System.Drawing.Point(129, 378);
            this.pictureBox28.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox28.Name = "pictureBox28";
            this.pictureBox28.Size = new System.Drawing.Size(99, 4);
            this.pictureBox28.TabIndex = 88;
            this.pictureBox28.TabStop = false;
            // 
            // pictureBox26
            // 
            this.pictureBox26.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox26.Image")));
            this.pictureBox26.Location = new System.Drawing.Point(64, 308);
            this.pictureBox26.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox26.Name = "pictureBox26";
            this.pictureBox26.Size = new System.Drawing.Size(186, 4);
            this.pictureBox26.TabIndex = 86;
            this.pictureBox26.TabStop = false;
            // 
            // pictureLineMFC2
            // 
            this.pictureLineMFC2.Image = ((System.Drawing.Image)(resources.GetObject("pictureLineMFC2.Image")));
            this.pictureLineMFC2.Location = new System.Drawing.Point(364, 394);
            this.pictureLineMFC2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureLineMFC2.Name = "pictureLineMFC2";
            this.pictureLineMFC2.Size = new System.Drawing.Size(214, 4);
            this.pictureLineMFC2.TabIndex = 84;
            this.pictureLineMFC2.TabStop = false;
            // 
            // pictureLineMFC1
            // 
            this.pictureLineMFC1.Image = ((System.Drawing.Image)(resources.GetObject("pictureLineMFC1.Image")));
            this.pictureLineMFC1.Location = new System.Drawing.Point(389, 250);
            this.pictureLineMFC1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureLineMFC1.Name = "pictureLineMFC1";
            this.pictureLineMFC1.Size = new System.Drawing.Size(188, 4);
            this.pictureLineMFC1.TabIndex = 83;
            this.pictureLineMFC1.TabStop = false;
            // 
            // pictureBox17
            // 
            this.pictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox17.Image")));
            this.pictureBox17.Location = new System.Drawing.Point(101, 405);
            this.pictureBox17.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(4, 239);
            this.pictureBox17.TabIndex = 80;
            this.pictureBox17.TabStop = false;
            // 
            // pictureCornerUp2
            // 
            this.pictureCornerUp2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureCornerUp2.ErrorImage")));
            this.pictureCornerUp2.Image = ((System.Drawing.Image)(resources.GetObject("pictureCornerUp2.Image")));
            this.pictureCornerUp2.Location = new System.Drawing.Point(101, 378);
            this.pictureCornerUp2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureCornerUp2.Name = "pictureCornerUp2";
            this.pictureCornerUp2.Size = new System.Drawing.Size(31, 31);
            this.pictureCornerUp2.TabIndex = 79;
            this.pictureCornerUp2.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox16.Image")));
            this.pictureBox16.Location = new System.Drawing.Point(38, 339);
            this.pictureBox16.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(4, 305);
            this.pictureBox16.TabIndex = 78;
            this.pictureBox16.TabStop = false;
            // 
            // pictureCornerUp1
            // 
            this.pictureCornerUp1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureCornerUp1.ErrorImage")));
            this.pictureCornerUp1.Image = ((System.Drawing.Image)(resources.GetObject("pictureCornerUp1.Image")));
            this.pictureCornerUp1.Location = new System.Drawing.Point(38, 308);
            this.pictureCornerUp1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureCornerUp1.Name = "pictureCornerUp1";
            this.pictureCornerUp1.Size = new System.Drawing.Size(28, 31);
            this.pictureCornerUp1.TabIndex = 77;
            this.pictureCornerUp1.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(221, 432);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(4, 202);
            this.pictureBox8.TabIndex = 71;
            this.pictureBox8.TabStop = false;
            // 
            // generatorPanel
            // 
            this.generatorPanel.BackColor = System.Drawing.Color.Transparent;
            this.generatorPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.generatorPanel.Location = new System.Drawing.Point(9, 9);
            this.generatorPanel.Margin = new System.Windows.Forms.Padding(2);
            this.generatorPanel.Name = "generatorPanel";
            this.generatorPanel.Size = new System.Drawing.Size(179, 245);
            this.generatorPanel.TabIndex = 50;
            // 
            // motorPanel1
            // 
            this.motorPanel1.BackColor = System.Drawing.Color.Transparent;
            this.motorPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.motorPanel1.Location = new System.Drawing.Point(192, 8);
            this.motorPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.motorPanel1.Name = "motorPanel1";
            this.motorPanel1.Size = new System.Drawing.Size(100, 149);
            this.motorPanel1.TabIndex = 49;
            // 
            // pressurePanel
            // 
            this.pressurePanel.BackColor = System.Drawing.Color.Transparent;
            this.pressurePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pressurePanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.pressurePanel.Location = new System.Drawing.Point(412, 8);
            this.pressurePanel.Margin = new System.Windows.Forms.Padding(2);
            this.pressurePanel.Name = "pressurePanel";
            this.pressurePanel.Size = new System.Drawing.Size(366, 149);
            this.pressurePanel.TabIndex = 48;
            // 
            // btnLiveModeData
            // 
            this.btnLiveModeData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLiveModeData.Location = new System.Drawing.Point(482, 742);
            this.btnLiveModeData.Margin = new System.Windows.Forms.Padding(2);
            this.btnLiveModeData.Name = "btnLiveModeData";
            this.btnLiveModeData.Size = new System.Drawing.Size(782, 39);
            this.btnLiveModeData.TabIndex = 30;
            this.btnLiveModeData.Text = "SWITCH TO GRAPHICAL VIEW";
            this.btnLiveModeData.UseVisualStyleBackColor = true;
            this.btnLiveModeData.Click += new System.EventHandler(this.btnLiveModeData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.interlockPanel_Vacuum);
            this.groupBox2.Controls.Add(this.interlockPanel_HV);
            this.groupBox2.Controls.Add(this.interlockPanel_Thermal);
            this.groupBox2.Controls.Add(this.interlockPanel_Door);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(9, 705);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(465, 76);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interlocks";
            // 
            // interlockPanel_Vacuum
            // 
            this.interlockPanel_Vacuum.AutoSize = true;
            this.interlockPanel_Vacuum.Location = new System.Drawing.Point(152, 21);
            this.interlockPanel_Vacuum.Margin = new System.Windows.Forms.Padding(6);
            this.interlockPanel_Vacuum.Name = "interlockPanel_Vacuum";
            this.interlockPanel_Vacuum.Size = new System.Drawing.Size(24, 24);
            this.interlockPanel_Vacuum.TabIndex = 111;
            // 
            // interlockPanel_HV
            // 
            this.interlockPanel_HV.AutoSize = true;
            this.interlockPanel_HV.Location = new System.Drawing.Point(270, 21);
            this.interlockPanel_HV.Margin = new System.Windows.Forms.Padding(6);
            this.interlockPanel_HV.Name = "interlockPanel_HV";
            this.interlockPanel_HV.Size = new System.Drawing.Size(24, 24);
            this.interlockPanel_HV.TabIndex = 109;
            // 
            // interlockPanel_Thermal
            // 
            this.interlockPanel_Thermal.AutoSize = true;
            this.interlockPanel_Thermal.Location = new System.Drawing.Point(391, 21);
            this.interlockPanel_Thermal.Margin = new System.Windows.Forms.Padding(6);
            this.interlockPanel_Thermal.Name = "interlockPanel_Thermal";
            this.interlockPanel_Thermal.Size = new System.Drawing.Size(24, 24);
            this.interlockPanel_Thermal.TabIndex = 108;
            // 
            // interlockPanel_Door
            // 
            this.interlockPanel_Door.AutoSize = true;
            this.interlockPanel_Door.Location = new System.Drawing.Point(30, 21);
            this.interlockPanel_Door.Margin = new System.Windows.Forms.Padding(5);
            this.interlockPanel_Door.Name = "interlockPanel_Door";
            this.interlockPanel_Door.Size = new System.Drawing.Size(24, 24);
            this.interlockPanel_Door.TabIndex = 107;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(343, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 20);
            this.label6.TabIndex = 106;
            this.label6.Text = "Thermal switch";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(234, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 20);
            this.label5.TabIndex = 105;
            this.label5.Text = "HV Interlock";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(105, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 103;
            this.label2.Text = "Vacuum switch";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(0, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 102;
            this.label1.Text = "Door switch";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.programPanel);
            this.groupBox1.Location = new System.Drawing.Point(9, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(468, 660);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program";
            // 
            // programPanel
            // 
            this.programPanel.BackColor = System.Drawing.Color.Transparent;
            this.programPanel.Color1 = System.Drawing.Color.LightGray;
            this.programPanel.Color2 = System.Drawing.Color.WhiteSmoke;
            this.programPanel.ColorAngle = 90F;
            this.programPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.programPanel.ForeColor = System.Drawing.Color.Transparent;
            this.programPanel.HPT1000 = null;
            this.programPanel.Location = new System.Drawing.Point(2, 22);
            this.programPanel.Margin = new System.Windows.Forms.Padding(2);
            this.programPanel.Name = "programPanel";
            this.programPanel.Size = new System.Drawing.Size(464, 636);
            this.programPanel.TabIndex = 0;
            // 
            // tabPagePrograms
            // 
            this.tabPagePrograms.Controls.Add(this.programsConfigPanel);
            this.tabPagePrograms.Location = new System.Drawing.Point(4, 49);
            this.tabPagePrograms.Margin = new System.Windows.Forms.Padding(2);
            this.tabPagePrograms.Name = "tabPagePrograms";
            this.tabPagePrograms.Size = new System.Drawing.Size(1274, 801);
            this.tabPagePrograms.TabIndex = 6;
            this.tabPagePrograms.Text = "PROGRAMS     ";
            this.tabPagePrograms.UseVisualStyleBackColor = true;
            // 
            // programsConfigPanel
            // 
            this.programsConfigPanel.BackColor = System.Drawing.SystemColors.Control;
            this.programsConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programsConfigPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.programsConfigPanel.HPT1000 = null;
            this.programsConfigPanel.Location = new System.Drawing.Point(0, 0);
            this.programsConfigPanel.Margin = new System.Windows.Forms.Padding(2);
            this.programsConfigPanel.Name = "programsConfigPanel";
            this.programsConfigPanel.Size = new System.Drawing.Size(1274, 801);
            this.programsConfigPanel.TabIndex = 1;
            // 
            // tabPageAlerts
            // 
            this.tabPageAlerts.Controls.Add(this.alertsPanel);
            this.tabPageAlerts.Location = new System.Drawing.Point(4, 49);
            this.tabPageAlerts.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageAlerts.Name = "tabPageAlerts";
            this.tabPageAlerts.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageAlerts.Size = new System.Drawing.Size(1274, 801);
            this.tabPageAlerts.TabIndex = 2;
            this.tabPageAlerts.Text = "ALERTS     ";
            this.tabPageAlerts.UseVisualStyleBackColor = true;
            // 
            // alertsPanel
            // 
            this.alertsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.alertsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertsPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alertsPanel.HPT1000 = null;
            this.alertsPanel.Location = new System.Drawing.Point(2, 2);
            this.alertsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.alertsPanel.Name = "alertsPanel";
            this.alertsPanel.Size = new System.Drawing.Size(1270, 797);
            this.alertsPanel.TabIndex = 0;
            // 
            // tabPageArchive
            // 
            this.tabPageArchive.Controls.Add(this.archivePanel);
            this.tabPageArchive.Location = new System.Drawing.Point(4, 49);
            this.tabPageArchive.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageArchive.Name = "tabPageArchive";
            this.tabPageArchive.Size = new System.Drawing.Size(1274, 801);
            this.tabPageArchive.TabIndex = 3;
            this.tabPageArchive.Text = "ARCHIVE     ";
            this.tabPageArchive.UseVisualStyleBackColor = true;
            // 
            // archivePanel
            // 
            this.archivePanel.BackColor = System.Drawing.SystemColors.Control;
            this.archivePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.archivePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.archivePanel.Location = new System.Drawing.Point(0, 0);
            this.archivePanel.Margin = new System.Windows.Forms.Padding(2);
            this.archivePanel.Name = "archivePanel";
            this.archivePanel.Size = new System.Drawing.Size(1274, 801);
            this.archivePanel.TabIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.Transparent;
            this.tabPageSettings.Controls.Add(this.settingsPanel);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 49);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(1274, 801);
            this.tabPageSettings.TabIndex = 4;
            this.tabPageSettings.Text = "SETTINGS     ";
            // 
            // settingsPanel
            // 
            this.settingsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(1274, 801);
            this.settingsPanel.TabIndex = 0;
            // 
            // tabPageMaintenance
            // 
            this.tabPageMaintenance.Controls.Add(this.maintancePanel);
            this.tabPageMaintenance.Location = new System.Drawing.Point(4, 49);
            this.tabPageMaintenance.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageMaintenance.Name = "tabPageMaintenance";
            this.tabPageMaintenance.Size = new System.Drawing.Size(1274, 801);
            this.tabPageMaintenance.TabIndex = 5;
            this.tabPageMaintenance.Text = "MAINTENANCE     ";
            this.tabPageMaintenance.UseVisualStyleBackColor = true;
            // 
            // maintancePanel
            // 
            this.maintancePanel.BackColor = System.Drawing.Color.Transparent;
            this.maintancePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maintancePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maintancePanel.Location = new System.Drawing.Point(0, 0);
            this.maintancePanel.Margin = new System.Windows.Forms.Padding(2);
            this.maintancePanel.Name = "maintancePanel";
            this.maintancePanel.Size = new System.Drawing.Size(1274, 801);
            this.maintancePanel.TabIndex = 0;
            // 
            // tabPageService
            // 
            this.tabPageService.Controls.Add(this.servicePanel);
            this.tabPageService.Location = new System.Drawing.Point(4, 49);
            this.tabPageService.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Size = new System.Drawing.Size(1274, 801);
            this.tabPageService.TabIndex = 7;
            this.tabPageService.Text = "SERVICE     ";
            this.tabPageService.UseVisualStyleBackColor = true;
            // 
            // servicePanel
            // 
            this.servicePanel.BackColor = System.Drawing.SystemColors.Control;
            this.servicePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.servicePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.servicePanel.Location = new System.Drawing.Point(0, 0);
            this.servicePanel.Margin = new System.Windows.Forms.Padding(2);
            this.servicePanel.Name = "servicePanel";
            this.servicePanel.Size = new System.Drawing.Size(1274, 801);
            this.servicePanel.TabIndex = 0;
            // 
            // tabPageUser
            // 
            this.tabPageUser.Controls.Add(this.userManagerPanel);
            this.tabPageUser.Controls.Add(this.adminPanel);
            this.tabPageUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabPageUser.Location = new System.Drawing.Point(4, 49);
            this.tabPageUser.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageUser.Name = "tabPageUser";
            this.tabPageUser.Size = new System.Drawing.Size(1274, 801);
            this.tabPageUser.TabIndex = 8;
            this.tabPageUser.Text = "USER MANAGER  ";
            this.tabPageUser.UseVisualStyleBackColor = true;
            // 
            // userManagerPanel
            // 
            this.userManagerPanel.BackColor = System.Drawing.SystemColors.Control;
            this.userManagerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userManagerPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userManagerPanel.Location = new System.Drawing.Point(0, 0);
            this.userManagerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.userManagerPanel.Name = "userManagerPanel";
            this.userManagerPanel.Size = new System.Drawing.Size(1274, 801);
            this.userManagerPanel.TabIndex = 1;
            // 
            // adminPanel
            // 
            this.adminPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adminPanel.Location = new System.Drawing.Point(0, 0);
            this.adminPanel.Margin = new System.Windows.Forms.Padding(5);
            this.adminPanel.Name = "adminPanel";
            this.adminPanel.Size = new System.Drawing.Size(1274, 801);
            this.adminPanel.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerKeyboard
            // 
            this.timerKeyboard.Enabled = true;
            this.timerKeyboard.Tick += new System.EventHandler(this.timerKeyboard_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1282, 880);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Portals";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.grBoxSystem.ResumeLayout(false);
            this.grBoxSystem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturelineMFC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArrowUp1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArrowUp2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureArrowDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineVaporizer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLineGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineMFC3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineMFC2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLineMFC1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerUp2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCornerUp1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPagePrograms.ResumeLayout(false);
            this.tabPageAlerts.ResumeLayout(false);
            this.tabPageArchive.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageMaintenance.ResumeLayout(false);
            this.tabPageService.ResumeLayout(false);
            this.tabPageUser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPageAlerts;
        private System.Windows.Forms.TabPage tabPageArchive;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TabPage tabPageMaintenance;
        private System.Windows.Forms.TabPage tabPagePrograms;
        private ProgramsConfigPanel programsConfigPanel;
        private ProgramPanel programPanel;
        private System.Windows.Forms.GroupBox grBoxSystem;
        private System.Windows.Forms.PictureBox pictureArrowUp1;
        private System.Windows.Forms.PictureBox pictureArrowUp2;
        private System.Windows.Forms.PictureBox pictureArrowDown;
        private System.Windows.Forms.PictureBox pictureLineVaporizer;
        private System.Windows.Forms.PictureBox pictureCornerDown;
        private System.Windows.Forms.PictureBox pictureBoxLineGV;
        private System.Windows.Forms.PictureBox pictureLineMFC3;
        private VaporiserPanel vaporiserPanel;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.PictureBox pictureBox28;
        private System.Windows.Forms.PictureBox pictureBox26;
        private System.Windows.Forms.PictureBox pictureLineMFC2;
        private System.Windows.Forms.PictureBox pictureLineMFC1;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.PictureBox pictureCornerUp2;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.PictureBox pictureCornerUp1;
        private System.Windows.Forms.PictureBox pictureBox8;
        private GeneratorPanel generatorPanel;
        private MotorPanel motorPanel1;
        private PressurePanel pressurePanel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel labStatusAction;
        private SettingsPanel settingsPanel;
        private AlertsPanel alertsPanel;
        private ValvePanel valve_Vent;
        private ValvePanel valve_SV;
        private ValvePanel valve_Gas;
        private ValvePanel valve_Purge;
        private PumpComponent pumpComponent;
        private System.Windows.Forms.PictureBox picturelineMFC;
        private System.Windows.Forms.PictureBox pictureCornerUp;
        private System.Windows.Forms.ToolStripStatusLabel labStatusUser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private InterlockPanel interlockPanel_Vacuum;
        private InterlockPanel interlockPanel_HV;
        private InterlockPanel interlockPanel_Thermal;
        private InterlockPanel interlockPanel_Door;
        private System.Windows.Forms.Button btnLiveModeData;
        private System.Windows.Forms.TabPage tabPageService;
        private System.Windows.Forms.TabPage tabPageUser;
        private ServicePanel servicePanel;
        private AdminPanel adminPanel;
        private GraphicalLive liveGraphicalPanel;
        private System.Windows.Forms.ToolStripSplitButton btnConfirm;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel borderLab1;
        private System.Windows.Forms.ToolStripStatusLabel borderLab2;
        private System.Windows.Forms.ToolStripStatusLabel labStatusMsgAction;
        private UserManagerPanel userManagerPanel;
        private ArchivePanel archivePanel;
        private MaintancePanel maintancePanel;
        private FlowGasPanel flowMFC1;
        private MFCPanel mfcPanel3;
        private MFCPanel mfcPanel2;
        private MFCPanel mfcPanel1;
        private FlowGasPanel flowMFC2;
        private FlowGasPanel flowMFC3;
        private ChamberPanel chamberPanel;
        private MotorPanel motorPanel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSeparator;
        private System.Windows.Forms.ToolStripStatusLabel labStatusMaintance;
        private System.Windows.Forms.Label labVersion;
        private System.Windows.Forms.Timer timerKeyboard;
        private System.Windows.Forms.Label label3;
    }
}

