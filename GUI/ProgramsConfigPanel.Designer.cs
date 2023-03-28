namespace HPT1000.GUI
{
    partial class ProgramsConfigPanel
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node4");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Subprograms", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Program 1", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Subprograms", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Program 2", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Programs list", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramsConfigPanel));
            this.grBoxGasPressure = new System.Windows.Forms.GroupBox();
            this.dEditGasPressureDevaUp = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditGasPressureDevaDown = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditGasPressure = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.rBtnPressureViaVapo = new System.Windows.Forms.RadioButton();
            this.rBtnPressureViaGases = new System.Windows.Forms.RadioButton();
            this.grBoxGasesMFC3 = new System.Windows.Forms.GroupBox();
            this.dEditGasDevaShareMFC3 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditGasShareMFC3 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.scrollGasDevaShareMFC3 = new System.Windows.Forms.HScrollBar();
            this.label31 = new System.Windows.Forms.Label();
            this.grBoxGasesMFC2 = new System.Windows.Forms.GroupBox();
            this.dEditGasDevaShareMFC2 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditGasShareMFC2 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.scrollGasDevaShareMFC2 = new System.Windows.Forms.HScrollBar();
            this.label27 = new System.Windows.Forms.Label();
            this.grBoxGasesMFC1 = new System.Windows.Forms.GroupBox();
            this.dEditGasShareMFC1 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditGasDevaShareMFC1 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.scrollGasDevaShareMFC1 = new System.Windows.Forms.HScrollBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.scrollGasPressureDevaDown = new System.Windows.Forms.HScrollBar();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.scrollGasPressureDevaUp = new System.Windows.Forms.HScrollBar();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.scrollGasPressure = new System.Windows.Forms.HScrollBar();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.timeVent = new System.Windows.Forms.DateTimePicker();
            this.label150 = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.tabPageVent = new System.Windows.Forms.TabPage();
            this.grBoxVent = new System.Windows.Forms.GroupBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.timePurge = new System.Windows.Forms.DateTimePicker();
            this.label149 = new System.Windows.Forms.Label();
            this.tabPagePurge = new System.Windows.Forms.TabPage();
            this.grBoxPurge = new System.Windows.Forms.GroupBox();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.label35 = new System.Windows.Forms.Label();
            this.labUnit2SetpointPlasma = new System.Windows.Forms.Label();
            this.labUnit1SetpointPlasma = new System.Windows.Forms.Label();
            this.scrollPlasmaDevistion = new System.Windows.Forms.HScrollBar();
            this.scrollPlasmaSetpoint = new System.Windows.Forms.HScrollBar();
            this.label144 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.timePlasma = new System.Windows.Forms.DateTimePicker();
            this.label86 = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.tabPagePlasma = new System.Windows.Forms.TabPage();
            this.label33 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.grBoxPlasma = new System.Windows.Forms.GroupBox();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.dEditPlasmaDeviation = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditPlasmaSetpoint = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.tBoxSetpointPlasma = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.timeGas = new System.Windows.Forms.DateTimePicker();
            this.label85 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.tabPagePump = new System.Windows.Forms.TabPage();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.grBoxPump = new System.Windows.Forms.GroupBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.dEditPumpSetpoint = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.scrollPumpSetpoint = new System.Windows.Forms.HScrollBar();
            this.label82 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.timePump = new System.Windows.Forms.DateTimePicker();
            this.label81 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.grBoxProgram = new System.Windows.Forms.GroupBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.tBoxNameProgram = new System.Windows.Forms.TextBox();
            this.tBoxDescProgram = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cBoxMFC3 = new System.Windows.Forms.CheckBox();
            this.cBoxGasListMFC1 = new System.Windows.Forms.ComboBox();
            this.cBoxMFC2 = new System.Windows.Forms.CheckBox();
            this.btnRemoveSubprogram = new System.Windows.Forms.Button();
            this.tabControlProcess = new System.Windows.Forms.TabControl();
            this.tabPageGas = new System.Windows.Forms.TabPage();
            this.grBoxGas = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rBtnGasNone = new System.Windows.Forms.RadioButton();
            this.rBtnModePressure = new System.Windows.Forms.RadioButton();
            this.rBtnModeFlow = new System.Windows.Forms.RadioButton();
            this.grBoxSelectGasLine = new System.Windows.Forms.GroupBox();
            this.cBoxVaporiser = new System.Windows.Forms.CheckBox();
            this.cBoxGasListMFC3 = new System.Windows.Forms.ComboBox();
            this.cBoxMFC1 = new System.Windows.Forms.CheckBox();
            this.cBoxGasListMFC2 = new System.Windows.Forms.ComboBox();
            this.grBoxGasFlow = new System.Windows.Forms.GroupBox();
            this.grBoxMFC1 = new System.Windows.Forms.GroupBox();
            this.dEditFlow1Max = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow1Min = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow1 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.scrollFlow1Min = new System.Windows.Forms.HScrollBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.scrollFlow1Max = new System.Windows.Forms.HScrollBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.scrollFlow1 = new System.Windows.Forms.HScrollBar();
            this.label10 = new System.Windows.Forms.Label();
            this.grBoxMFC3 = new System.Windows.Forms.GroupBox();
            this.dEditFlow3Max = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow3Min = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow3 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label17 = new System.Windows.Forms.Label();
            this.scrollFlow3Min = new System.Windows.Forms.HScrollBar();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.scrollFlow3Max = new System.Windows.Forms.HScrollBar();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.scrollFlow3 = new System.Windows.Forms.HScrollBar();
            this.label22 = new System.Windows.Forms.Label();
            this.grBoxMFC2 = new System.Windows.Forms.GroupBox();
            this.dEditFlow2Max = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow2Min = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditFlow2 = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.scrollFlow2Min = new System.Windows.Forms.HScrollBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.scrollFlow2Max = new System.Windows.Forms.HScrollBar();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.scrollFlow2 = new System.Windows.Forms.HScrollBar();
            this.label16 = new System.Windows.Forms.Label();
            this.grBoxVaporiser = new System.Windows.Forms.GroupBox();
            this.dEditGasVaporCycleTime = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditGasVaporOnTime = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.dEditDosing = new HPT1000.GUI.Cotrols.DoubleEdit();
            this.scrolBarDosing = new System.Windows.Forms.HScrollBar();
            this.labUnitDosing = new System.Windows.Forms.Label();
            this.labDosing = new System.Windows.Forms.Label();
            this.scrollGasVaporOnTime = new System.Windows.Forms.HScrollBar();
            this.labUnitOnTime = new System.Windows.Forms.Label();
            this.scrollGasVaporCycleTime = new System.Windows.Forms.HScrollBar();
            this.labUnitCycle = new System.Windows.Forms.Label();
            this.labOnTIme = new System.Windows.Forms.Label();
            this.labCycle = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabPageMotor = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.grBoxSelectMotor = new System.Windows.Forms.GroupBox();
            this.cBoxActiveMotor1 = new System.Windows.Forms.CheckBox();
            this.cBoxActiveMotor2 = new System.Windows.Forms.CheckBox();
            this.grBoxMotor2 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.cBoxStateMotor2 = new System.Windows.Forms.CheckBox();
            this.dateTimeMotor2 = new System.Windows.Forms.DateTimePicker();
            this.label42 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.grBoxMotor1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.cBoxStateMotor1 = new System.Windows.Forms.CheckBox();
            this.dateTimeMotor1 = new System.Windows.Forms.DateTimePicker();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.labTitelMotor = new System.Windows.Forms.Label();
            this.grBoxSubprogram = new System.Windows.Forms.GroupBox();
            this.cBoxMotor = new System.Windows.Forms.CheckBox();
            this.label78 = new System.Windows.Forms.Label();
            this.tBoxDescSubprgoram = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.tBoxNameSubprogram = new System.Windows.Forms.TextBox();
            this.label153 = new System.Windows.Forms.Label();
            this.cBoxPump = new System.Windows.Forms.CheckBox();
            this.cBoxGas = new System.Windows.Forms.CheckBox();
            this.cBoxPower = new System.Windows.Forms.CheckBox();
            this.cBoxVent = new System.Windows.Forms.CheckBox();
            this.cBoxPurge = new System.Windows.Forms.CheckBox();
            this.treeViewProgram = new System.Windows.Forms.TreeView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_AddProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_AddSubprogram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_RemoveProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_RemoveSubprogram = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnRemoveProgram = new System.Windows.Forms.Button();
            this.grBoxPrograms = new System.Windows.Forms.GroupBox();
            this.grBoxSubprogramToolBtn = new System.Windows.Forms.GroupBox();
            this.btnAddNewSubprogram = new System.Windows.Forms.Button();
            this.btnDownSubprogram = new System.Windows.Forms.Button();
            this.btnUpSubprogram = new System.Windows.Forms.Button();
            this.grBoxProgramToolBtn = new System.Windows.Forms.GroupBox();
            this.btnAddNewProgram = new System.Windows.Forms.Button();
            this.label69 = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.grBoxGasPressure.SuspendLayout();
            this.grBoxGasesMFC3.SuspendLayout();
            this.grBoxGasesMFC2.SuspendLayout();
            this.grBoxGasesMFC1.SuspendLayout();
            this.tabPageVent.SuspendLayout();
            this.grBoxVent.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.tabPagePurge.SuspendLayout();
            this.grBoxPurge.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.tabPagePlasma.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.grBoxPlasma.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.tabPagePump.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.grBoxPump.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.grBoxProgram.SuspendLayout();
            this.tabControlProcess.SuspendLayout();
            this.tabPageGas.SuspendLayout();
            this.grBoxGas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grBoxSelectGasLine.SuspendLayout();
            this.grBoxGasFlow.SuspendLayout();
            this.grBoxMFC1.SuspendLayout();
            this.grBoxMFC3.SuspendLayout();
            this.grBoxMFC2.SuspendLayout();
            this.grBoxVaporiser.SuspendLayout();
            this.tabPageMotor.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.grBoxSelectMotor.SuspendLayout();
            this.grBoxMotor2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.grBoxMotor1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grBoxSubprogram.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.grBoxPrograms.SuspendLayout();
            this.grBoxSubprogramToolBtn.SuspendLayout();
            this.grBoxProgramToolBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // grBoxGasPressure
            // 
            this.grBoxGasPressure.Controls.Add(this.dEditGasPressureDevaUp);
            this.grBoxGasPressure.Controls.Add(this.dEditGasPressureDevaDown);
            this.grBoxGasPressure.Controls.Add(this.dEditGasPressure);
            this.grBoxGasPressure.Controls.Add(this.rBtnPressureViaVapo);
            this.grBoxGasPressure.Controls.Add(this.rBtnPressureViaGases);
            this.grBoxGasPressure.Controls.Add(this.grBoxGasesMFC3);
            this.grBoxGasPressure.Controls.Add(this.grBoxGasesMFC2);
            this.grBoxGasPressure.Controls.Add(this.grBoxGasesMFC1);
            this.grBoxGasPressure.Controls.Add(this.label105);
            this.grBoxGasPressure.Controls.Add(this.scrollGasPressureDevaDown);
            this.grBoxGasPressure.Controls.Add(this.label103);
            this.grBoxGasPressure.Controls.Add(this.label104);
            this.grBoxGasPressure.Controls.Add(this.scrollGasPressureDevaUp);
            this.grBoxGasPressure.Controls.Add(this.label101);
            this.grBoxGasPressure.Controls.Add(this.label102);
            this.grBoxGasPressure.Controls.Add(this.scrollGasPressure);
            this.grBoxGasPressure.Controls.Add(this.label98);
            this.grBoxGasPressure.Controls.Add(this.label99);
            this.grBoxGasPressure.Location = new System.Drawing.Point(7, 443);
            this.grBoxGasPressure.Name = "grBoxGasPressure";
            this.grBoxGasPressure.Size = new System.Drawing.Size(732, 242);
            this.grBoxGasPressure.TabIndex = 12;
            this.grBoxGasPressure.TabStop = false;
            this.grBoxGasPressure.Text = "Pressure mode";
            // 
            // dEditGasPressureDevaUp
            // 
            this.dEditGasPressureDevaUp.Location = new System.Drawing.Point(583, 27);
            this.dEditGasPressureDevaUp.Mask = "0.001";
            this.dEditGasPressureDevaUp.MaximumValue = 1000D;
            this.dEditGasPressureDevaUp.MinimumValue = 0.001D;
            this.dEditGasPressureDevaUp.Name = "dEditGasPressureDevaUp";
            this.dEditGasPressureDevaUp.ReadOnly = false;
            this.dEditGasPressureDevaUp.Size = new System.Drawing.Size(80, 26);
            this.dEditGasPressureDevaUp.TabIndex = 48;
            this.dEditGasPressureDevaUp.Value = 0.001D;
            this.dEditGasPressureDevaUp.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasPressureDevaUp_EnterOn);
            // 
            // dEditGasPressureDevaDown
            // 
            this.dEditGasPressureDevaDown.Location = new System.Drawing.Point(581, 59);
            this.dEditGasPressureDevaDown.Mask = "0.001";
            this.dEditGasPressureDevaDown.MaximumValue = 1000D;
            this.dEditGasPressureDevaDown.MinimumValue = 0.001D;
            this.dEditGasPressureDevaDown.Name = "dEditGasPressureDevaDown";
            this.dEditGasPressureDevaDown.ReadOnly = false;
            this.dEditGasPressureDevaDown.Size = new System.Drawing.Size(80, 26);
            this.dEditGasPressureDevaDown.TabIndex = 47;
            this.dEditGasPressureDevaDown.Value = 0.001D;
            this.dEditGasPressureDevaDown.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasPressureDevaDown_EnterOn);
            // 
            // dEditGasPressure
            // 
            this.dEditGasPressure.Location = new System.Drawing.Point(192, 30);
            this.dEditGasPressure.Mask = "0.001";
            this.dEditGasPressure.MaximumValue = 1000D;
            this.dEditGasPressure.MinimumValue = 0.001D;
            this.dEditGasPressure.Name = "dEditGasPressure";
            this.dEditGasPressure.ReadOnly = false;
            this.dEditGasPressure.Size = new System.Drawing.Size(80, 26);
            this.dEditGasPressure.TabIndex = 46;
            this.dEditGasPressure.Value = 1000D;
            this.dEditGasPressure.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasPressure_EnterOn);
            // 
            // rBtnPressureViaVapo
            // 
            this.rBtnPressureViaVapo.AutoSize = true;
            this.rBtnPressureViaVapo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rBtnPressureViaVapo.Location = new System.Drawing.Point(569, 165);
            this.rBtnPressureViaVapo.Name = "rBtnPressureViaVapo";
            this.rBtnPressureViaVapo.Size = new System.Drawing.Size(141, 22);
            this.rBtnPressureViaVapo.TabIndex = 45;
            this.rBtnPressureViaVapo.TabStop = true;
            this.rBtnPressureViaVapo.Text = "Control via vapor";
            this.rBtnPressureViaVapo.UseVisualStyleBackColor = true;
            this.rBtnPressureViaVapo.CheckedChanged += new System.EventHandler(this.rBtnGasModePressure_CheckedChanged);
            // 
            // rBtnPressureViaGases
            // 
            this.rBtnPressureViaGases.AutoSize = true;
            this.rBtnPressureViaGases.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rBtnPressureViaGases.Location = new System.Drawing.Point(567, 203);
            this.rBtnPressureViaGases.Name = "rBtnPressureViaGases";
            this.rBtnPressureViaGases.Size = new System.Drawing.Size(144, 22);
            this.rBtnPressureViaGases.TabIndex = 44;
            this.rBtnPressureViaGases.TabStop = true;
            this.rBtnPressureViaGases.Text = "Control via gases";
            this.rBtnPressureViaGases.UseVisualStyleBackColor = true;
            this.rBtnPressureViaGases.CheckedChanged += new System.EventHandler(this.rBtnGasModePressure_CheckedChanged);
            // 
            // grBoxGasesMFC3
            // 
            this.grBoxGasesMFC3.Controls.Add(this.dEditGasDevaShareMFC3);
            this.grBoxGasesMFC3.Controls.Add(this.dEditGasShareMFC3);
            this.grBoxGasesMFC3.Controls.Add(this.label28);
            this.grBoxGasesMFC3.Controls.Add(this.label29);
            this.grBoxGasesMFC3.Controls.Add(this.label30);
            this.grBoxGasesMFC3.Controls.Add(this.scrollGasDevaShareMFC3);
            this.grBoxGasesMFC3.Controls.Add(this.label31);
            this.grBoxGasesMFC3.Location = new System.Drawing.Point(375, 87);
            this.grBoxGasesMFC3.Name = "grBoxGasesMFC3";
            this.grBoxGasesMFC3.Size = new System.Drawing.Size(176, 148);
            this.grBoxGasesMFC3.TabIndex = 42;
            this.grBoxGasesMFC3.TabStop = false;
            this.grBoxGasesMFC3.Text = "MFC 3 - N/A";
            // 
            // dEditGasDevaShareMFC3
            // 
            this.dEditGasDevaShareMFC3.Location = new System.Drawing.Point(110, 50);
            this.dEditGasDevaShareMFC3.Mask = "0";
            this.dEditGasDevaShareMFC3.MaximumValue = 100D;
            this.dEditGasDevaShareMFC3.MinimumValue = 0D;
            this.dEditGasDevaShareMFC3.Name = "dEditGasDevaShareMFC3";
            this.dEditGasDevaShareMFC3.ReadOnly = false;
            this.dEditGasDevaShareMFC3.Size = new System.Drawing.Size(58, 27);
            this.dEditGasDevaShareMFC3.TabIndex = 44;
            this.dEditGasDevaShareMFC3.Value = 0D;
            this.dEditGasDevaShareMFC3.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasDevaShareMFC3_EnterOn);
            // 
            // dEditGasShareMFC3
            // 
            this.dEditGasShareMFC3.Location = new System.Drawing.Point(17, 111);
            this.dEditGasShareMFC3.Mask = "0";
            this.dEditGasShareMFC3.MaximumValue = 100D;
            this.dEditGasShareMFC3.MinimumValue = 0D;
            this.dEditGasShareMFC3.Name = "dEditGasShareMFC3";
            this.dEditGasShareMFC3.ReadOnly = false;
            this.dEditGasShareMFC3.Size = new System.Drawing.Size(58, 28);
            this.dEditGasShareMFC3.TabIndex = 43;
            this.dEditGasShareMFC3.Value = 0D;
            this.dEditGasShareMFC3.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasShareMFC3_EnterOn);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label28.Location = new System.Drawing.Point(5, 24);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(102, 18);
            this.label28.TabIndex = 41;
            this.label28.Text = "Max deviation:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.Green;
            this.label29.Location = new System.Drawing.Point(147, 74);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(21, 18);
            this.label29.TabIndex = 40;
            this.label29.Text = "%";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label30.Location = new System.Drawing.Point(5, 87);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(81, 18);
            this.label30.TabIndex = 39;
            this.label30.Text = "Gas share:";
            // 
            // scrollGasDevaShareMFC3
            // 
            this.scrollGasDevaShareMFC3.Location = new System.Drawing.Point(7, 49);
            this.scrollGasDevaShareMFC3.Maximum = 109;
            this.scrollGasDevaShareMFC3.Name = "scrollGasDevaShareMFC3";
            this.scrollGasDevaShareMFC3.Size = new System.Drawing.Size(100, 22);
            this.scrollGasDevaShareMFC3.TabIndex = 36;
            this.scrollGasDevaShareMFC3.Value = 10;
            this.scrollGasDevaShareMFC3.ValueChanged += new System.EventHandler(this.scrollGasDevaShareMFC3_ValueChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Green;
            this.label31.Location = new System.Drawing.Point(79, 116);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(21, 18);
            this.label31.TabIndex = 35;
            this.label31.Text = "%";
            // 
            // grBoxGasesMFC2
            // 
            this.grBoxGasesMFC2.Controls.Add(this.dEditGasDevaShareMFC2);
            this.grBoxGasesMFC2.Controls.Add(this.dEditGasShareMFC2);
            this.grBoxGasesMFC2.Controls.Add(this.label24);
            this.grBoxGasesMFC2.Controls.Add(this.label25);
            this.grBoxGasesMFC2.Controls.Add(this.label26);
            this.grBoxGasesMFC2.Controls.Add(this.scrollGasDevaShareMFC2);
            this.grBoxGasesMFC2.Controls.Add(this.label27);
            this.grBoxGasesMFC2.Location = new System.Drawing.Point(189, 87);
            this.grBoxGasesMFC2.Name = "grBoxGasesMFC2";
            this.grBoxGasesMFC2.Size = new System.Drawing.Size(176, 148);
            this.grBoxGasesMFC2.TabIndex = 42;
            this.grBoxGasesMFC2.TabStop = false;
            this.grBoxGasesMFC2.Text = "MFC 2 - N/A";
            // 
            // dEditGasDevaShareMFC2
            // 
            this.dEditGasDevaShareMFC2.Location = new System.Drawing.Point(112, 50);
            this.dEditGasDevaShareMFC2.Mask = "0";
            this.dEditGasDevaShareMFC2.MaximumValue = 100D;
            this.dEditGasDevaShareMFC2.MinimumValue = 0D;
            this.dEditGasDevaShareMFC2.Name = "dEditGasDevaShareMFC2";
            this.dEditGasDevaShareMFC2.ReadOnly = false;
            this.dEditGasDevaShareMFC2.Size = new System.Drawing.Size(58, 27);
            this.dEditGasDevaShareMFC2.TabIndex = 44;
            this.dEditGasDevaShareMFC2.Value = 0D;
            this.dEditGasDevaShareMFC2.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasDevaShareMFC2_EnterOn);
            // 
            // dEditGasShareMFC2
            // 
            this.dEditGasShareMFC2.Location = new System.Drawing.Point(18, 112);
            this.dEditGasShareMFC2.Mask = "0";
            this.dEditGasShareMFC2.MaximumValue = 100D;
            this.dEditGasShareMFC2.MinimumValue = 0D;
            this.dEditGasShareMFC2.Name = "dEditGasShareMFC2";
            this.dEditGasShareMFC2.ReadOnly = false;
            this.dEditGasShareMFC2.Size = new System.Drawing.Size(58, 28);
            this.dEditGasShareMFC2.TabIndex = 43;
            this.dEditGasShareMFC2.Value = 0D;
            this.dEditGasShareMFC2.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasShareMFC2_EnterOn);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label24.Location = new System.Drawing.Point(5, 24);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(102, 18);
            this.label24.TabIndex = 41;
            this.label24.Text = "Max deviation:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Green;
            this.label25.Location = new System.Drawing.Point(150, 75);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(21, 18);
            this.label25.TabIndex = 40;
            this.label25.Text = "%";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label26.Location = new System.Drawing.Point(5, 87);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(81, 18);
            this.label26.TabIndex = 39;
            this.label26.Text = "Gas share:";
            // 
            // scrollGasDevaShareMFC2
            // 
            this.scrollGasDevaShareMFC2.Location = new System.Drawing.Point(7, 49);
            this.scrollGasDevaShareMFC2.Maximum = 109;
            this.scrollGasDevaShareMFC2.Name = "scrollGasDevaShareMFC2";
            this.scrollGasDevaShareMFC2.Size = new System.Drawing.Size(100, 22);
            this.scrollGasDevaShareMFC2.TabIndex = 36;
            this.scrollGasDevaShareMFC2.Value = 10;
            this.scrollGasDevaShareMFC2.ValueChanged += new System.EventHandler(this.scrollGasDevaShareMFC2_ValueChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Green;
            this.label27.Location = new System.Drawing.Point(79, 116);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(21, 18);
            this.label27.TabIndex = 35;
            this.label27.Text = "%";
            // 
            // grBoxGasesMFC1
            // 
            this.grBoxGasesMFC1.Controls.Add(this.dEditGasShareMFC1);
            this.grBoxGasesMFC1.Controls.Add(this.dEditGasDevaShareMFC1);
            this.grBoxGasesMFC1.Controls.Add(this.label3);
            this.grBoxGasesMFC1.Controls.Add(this.label1);
            this.grBoxGasesMFC1.Controls.Add(this.label2);
            this.grBoxGasesMFC1.Controls.Add(this.scrollGasDevaShareMFC1);
            this.grBoxGasesMFC1.Controls.Add(this.label4);
            this.grBoxGasesMFC1.Location = new System.Drawing.Point(5, 87);
            this.grBoxGasesMFC1.Name = "grBoxGasesMFC1";
            this.grBoxGasesMFC1.Size = new System.Drawing.Size(176, 148);
            this.grBoxGasesMFC1.TabIndex = 27;
            this.grBoxGasesMFC1.TabStop = false;
            this.grBoxGasesMFC1.Text = "MFC 1 - Oxygen";
            // 
            // dEditGasShareMFC1
            // 
            this.dEditGasShareMFC1.Location = new System.Drawing.Point(19, 112);
            this.dEditGasShareMFC1.Mask = "0";
            this.dEditGasShareMFC1.MaximumValue = 100D;
            this.dEditGasShareMFC1.MinimumValue = 0D;
            this.dEditGasShareMFC1.Name = "dEditGasShareMFC1";
            this.dEditGasShareMFC1.ReadOnly = false;
            this.dEditGasShareMFC1.Size = new System.Drawing.Size(58, 28);
            this.dEditGasShareMFC1.TabIndex = 43;
            this.dEditGasShareMFC1.Value = 0D;
            this.dEditGasShareMFC1.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasShareMFC1_EnterOn);
            // 
            // dEditGasDevaShareMFC1
            // 
            this.dEditGasDevaShareMFC1.Location = new System.Drawing.Point(111, 50);
            this.dEditGasDevaShareMFC1.Mask = "0";
            this.dEditGasDevaShareMFC1.MaximumValue = 100D;
            this.dEditGasDevaShareMFC1.MinimumValue = 0D;
            this.dEditGasDevaShareMFC1.Name = "dEditGasDevaShareMFC1";
            this.dEditGasDevaShareMFC1.ReadOnly = false;
            this.dEditGasDevaShareMFC1.Size = new System.Drawing.Size(52, 27);
            this.dEditGasDevaShareMFC1.TabIndex = 42;
            this.dEditGasDevaShareMFC1.Value = 0D;
            this.dEditGasDevaShareMFC1.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasDevaShareMFC1_EnterOn);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(5, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 18);
            this.label3.TabIndex = 41;
            this.label3.Text = "Max deviation:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(142, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 18);
            this.label1.TabIndex = 40;
            this.label1.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(5, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 39;
            this.label2.Text = "Gas share:";
            // 
            // scrollGasDevaShareMFC1
            // 
            this.scrollGasDevaShareMFC1.Location = new System.Drawing.Point(7, 49);
            this.scrollGasDevaShareMFC1.Maximum = 109;
            this.scrollGasDevaShareMFC1.Name = "scrollGasDevaShareMFC1";
            this.scrollGasDevaShareMFC1.Size = new System.Drawing.Size(100, 22);
            this.scrollGasDevaShareMFC1.TabIndex = 36;
            this.scrollGasDevaShareMFC1.Value = 10;
            this.scrollGasDevaShareMFC1.ValueChanged += new System.EventHandler(this.scrollGasDevaShareMFC1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(79, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 18);
            this.label4.TabIndex = 35;
            this.label4.Text = "%";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label105.Location = new System.Drawing.Point(448, 55);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(13, 18);
            this.label105.TabIndex = 41;
            this.label105.Text = "-";
            // 
            // scrollGasPressureDevaDown
            // 
            this.scrollGasPressureDevaDown.Location = new System.Drawing.Point(475, 58);
            this.scrollGasPressureDevaDown.Maximum = 1000000;
            this.scrollGasPressureDevaDown.Minimum = 1;
            this.scrollGasPressureDevaDown.Name = "scrollGasPressureDevaDown";
            this.scrollGasPressureDevaDown.Size = new System.Drawing.Size(100, 22);
            this.scrollGasPressureDevaDown.TabIndex = 40;
            this.scrollGasPressureDevaDown.Value = 10;
            this.scrollGasPressureDevaDown.ValueChanged += new System.EventHandler(this.scrollGasPressureDevaDown_ValueChanged);
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.ForeColor = System.Drawing.Color.Green;
            this.label103.Location = new System.Drawing.Point(674, 61);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(44, 18);
            this.label103.TabIndex = 39;
            this.label103.Text = "mBar";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label104.Location = new System.Drawing.Point(444, 29);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(17, 18);
            this.label104.TabIndex = 37;
            this.label104.Text = "+";
            // 
            // scrollGasPressureDevaUp
            // 
            this.scrollGasPressureDevaUp.Location = new System.Drawing.Point(475, 27);
            this.scrollGasPressureDevaUp.Maximum = 1000000;
            this.scrollGasPressureDevaUp.Minimum = 1;
            this.scrollGasPressureDevaUp.Name = "scrollGasPressureDevaUp";
            this.scrollGasPressureDevaUp.Size = new System.Drawing.Size(100, 22);
            this.scrollGasPressureDevaUp.TabIndex = 36;
            this.scrollGasPressureDevaUp.Value = 10;
            this.scrollGasPressureDevaUp.ValueChanged += new System.EventHandler(this.scrollGasPressureDevaUp_ValueChanged);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.ForeColor = System.Drawing.Color.Green;
            this.label101.Location = new System.Drawing.Point(674, 31);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(44, 18);
            this.label101.TabIndex = 35;
            this.label101.Text = "mBar";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label102.Location = new System.Drawing.Point(343, 30);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(102, 18);
            this.label102.TabIndex = 33;
            this.label102.Text = "Max deviation:";
            // 
            // scrollGasPressure
            // 
            this.scrollGasPressure.Location = new System.Drawing.Point(87, 29);
            this.scrollGasPressure.Maximum = 1000000;
            this.scrollGasPressure.Minimum = 1;
            this.scrollGasPressure.Name = "scrollGasPressure";
            this.scrollGasPressure.Size = new System.Drawing.Size(100, 22);
            this.scrollGasPressure.TabIndex = 32;
            this.scrollGasPressure.Value = 10;
            this.scrollGasPressure.ValueChanged += new System.EventHandler(this.scrollGasPressure_ValueChanged);
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.ForeColor = System.Drawing.Color.Green;
            this.label98.Location = new System.Drawing.Point(275, 31);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(44, 18);
            this.label98.TabIndex = 31;
            this.label98.Text = "mBar";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label99.Location = new System.Drawing.Point(14, 30);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(66, 18);
            this.label99.TabIndex = 29;
            this.label99.Text = "Setpoint:";
            // 
            // timeVent
            // 
            this.timeVent.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeVent.Location = new System.Drawing.Point(333, 62);
            this.timeVent.Name = "timeVent";
            this.timeVent.ShowUpDown = true;
            this.timeVent.Size = new System.Drawing.Size(104, 24);
            this.timeVent.TabIndex = 20;
            this.timeVent.ValueChanged += new System.EventHandler(this.timeVent_ValueChanged);
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label150.Location = new System.Drawing.Point(230, 67);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(92, 18);
            this.label150.TabIndex = 22;
            this.label150.Text = "Venting time:";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.ForeColor = System.Drawing.Color.Green;
            this.label151.Location = new System.Drawing.Point(462, 65);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(82, 18);
            this.label151.TabIndex = 21;
            this.label151.Text = "[hh:mm:ss]";
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(186, 82);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(0, 18);
            this.label152.TabIndex = 19;
            // 
            // tabPageVent
            // 
            this.tabPageVent.BackColor = System.Drawing.Color.White;
            this.tabPageVent.Controls.Add(this.grBoxVent);
            this.tabPageVent.Controls.Add(this.label36);
            this.tabPageVent.Controls.Add(this.label152);
            this.tabPageVent.Location = new System.Drawing.Point(4, 4);
            this.tabPageVent.Name = "tabPageVent";
            this.tabPageVent.Size = new System.Drawing.Size(756, 719);
            this.tabPageVent.TabIndex = 4;
            this.tabPageVent.Text = "VENTING";
            // 
            // grBoxVent
            // 
            this.grBoxVent.Controls.Add(this.groupBox23);
            this.grBoxVent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoxVent.Location = new System.Drawing.Point(0, 28);
            this.grBoxVent.Name = "grBoxVent";
            this.grBoxVent.Size = new System.Drawing.Size(756, 691);
            this.grBoxVent.TabIndex = 25;
            this.grBoxVent.TabStop = false;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.timeVent);
            this.groupBox23.Controls.Add(this.label151);
            this.groupBox23.Controls.Add(this.label150);
            this.groupBox23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox23.Location = new System.Drawing.Point(3, 20);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(750, 668);
            this.groupBox23.TabIndex = 24;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Parameters";
            // 
            // label36
            // 
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label36.ForeColor = System.Drawing.Color.Maroon;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(756, 28);
            this.label36.TabIndex = 23;
            this.label36.Text = "Process: Vent chamber";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label147.Location = new System.Drawing.Point(216, 65);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(99, 18);
            this.label147.TabIndex = 18;
            this.label147.Text = "Flushing time:";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.ForeColor = System.Drawing.Color.Green;
            this.label148.Location = new System.Drawing.Point(462, 65);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(82, 18);
            this.label148.TabIndex = 17;
            this.label148.Text = "[hh:mm:ss]";
            // 
            // timePurge
            // 
            this.timePurge.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePurge.Location = new System.Drawing.Point(333, 63);
            this.timePurge.Name = "timePurge";
            this.timePurge.ShowUpDown = true;
            this.timePurge.Size = new System.Drawing.Size(108, 24);
            this.timePurge.TabIndex = 16;
            this.timePurge.ValueChanged += new System.EventHandler(this.timePyrge_ValueChanged);
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(-11, 81);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(0, 18);
            this.label149.TabIndex = 15;
            // 
            // tabPagePurge
            // 
            this.tabPagePurge.BackColor = System.Drawing.Color.White;
            this.tabPagePurge.Controls.Add(this.grBoxPurge);
            this.tabPagePurge.Controls.Add(this.label35);
            this.tabPagePurge.Location = new System.Drawing.Point(4, 4);
            this.tabPagePurge.Name = "tabPagePurge";
            this.tabPagePurge.Size = new System.Drawing.Size(756, 719);
            this.tabPagePurge.TabIndex = 3;
            this.tabPagePurge.Text = "PURGE";
            // 
            // grBoxPurge
            // 
            this.grBoxPurge.Controls.Add(this.groupBox22);
            this.grBoxPurge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoxPurge.Location = new System.Drawing.Point(0, 28);
            this.grBoxPurge.Name = "grBoxPurge";
            this.grBoxPurge.Size = new System.Drawing.Size(756, 691);
            this.grBoxPurge.TabIndex = 22;
            this.grBoxPurge.TabStop = false;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.timePurge);
            this.groupBox22.Controls.Add(this.label149);
            this.groupBox22.Controls.Add(this.label147);
            this.groupBox22.Controls.Add(this.label148);
            this.groupBox22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox22.Location = new System.Drawing.Point(3, 20);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(750, 668);
            this.groupBox22.TabIndex = 19;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Parameters";
            // 
            // label35
            // 
            this.label35.Dock = System.Windows.Forms.DockStyle.Top;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label35.ForeColor = System.Drawing.Color.Maroon;
            this.label35.Location = new System.Drawing.Point(0, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(756, 28);
            this.label35.TabIndex = 21;
            this.label35.Text = "Flushing time:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labUnit2SetpointPlasma
            // 
            this.labUnit2SetpointPlasma.AutoSize = true;
            this.labUnit2SetpointPlasma.ForeColor = System.Drawing.Color.Green;
            this.labUnit2SetpointPlasma.Location = new System.Drawing.Point(580, 126);
            this.labUnit2SetpointPlasma.Name = "labUnit2SetpointPlasma";
            this.labUnit2SetpointPlasma.Size = new System.Drawing.Size(29, 18);
            this.labUnit2SetpointPlasma.TabIndex = 36;
            this.labUnit2SetpointPlasma.Text = "[%]";
            this.labUnit2SetpointPlasma.Visible = false;
            // 
            // labUnit1SetpointPlasma
            // 
            this.labUnit1SetpointPlasma.AutoSize = true;
            this.labUnit1SetpointPlasma.ForeColor = System.Drawing.Color.Green;
            this.labUnit1SetpointPlasma.Location = new System.Drawing.Point(580, 85);
            this.labUnit1SetpointPlasma.Name = "labUnit1SetpointPlasma";
            this.labUnit1SetpointPlasma.Size = new System.Drawing.Size(31, 18);
            this.labUnit1SetpointPlasma.TabIndex = 35;
            this.labUnit1SetpointPlasma.Text = "[W]";
            this.labUnit1SetpointPlasma.Visible = false;
            // 
            // scrollPlasmaDevistion
            // 
            this.scrollPlasmaDevistion.Location = new System.Drawing.Point(333, 122);
            this.scrollPlasmaDevistion.Maximum = 109;
            this.scrollPlasmaDevistion.Name = "scrollPlasmaDevistion";
            this.scrollPlasmaDevistion.Size = new System.Drawing.Size(150, 22);
            this.scrollPlasmaDevistion.TabIndex = 34;
            this.scrollPlasmaDevistion.Value = 10;
            this.scrollPlasmaDevistion.Visible = false;
            this.scrollPlasmaDevistion.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollPlasmaDevistion_Scroll);
            // 
            // scrollPlasmaSetpoint
            // 
            this.scrollPlasmaSetpoint.Location = new System.Drawing.Point(333, 44);
            this.scrollPlasmaSetpoint.Maximum = 109;
            this.scrollPlasmaSetpoint.Name = "scrollPlasmaSetpoint";
            this.scrollPlasmaSetpoint.Size = new System.Drawing.Size(150, 22);
            this.scrollPlasmaSetpoint.TabIndex = 32;
            this.scrollPlasmaSetpoint.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollPlasmaSetpoint_Scroll);
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label144.Location = new System.Drawing.Point(212, 124);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(102, 18);
            this.label144.TabIndex = 30;
            this.label144.Text = "Max deviation:";
            this.label144.Visible = false;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(-67, 138);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(46, 18);
            this.label143.TabIndex = 19;
            this.label143.Text = "Mode";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label142.Location = new System.Drawing.Point(212, 44);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(62, 18);
            this.label142.TabIndex = 15;
            this.label142.Text = "Setpoint";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label139.Location = new System.Drawing.Point(212, 29);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(109, 18);
            this.label139.TabIndex = 14;
            this.label139.Text = "Operation time:";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.ForeColor = System.Drawing.Color.Green;
            this.label140.Location = new System.Drawing.Point(462, 33);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(82, 18);
            this.label140.TabIndex = 13;
            this.label140.Text = "[hh:mm:ss]";
            // 
            // timePlasma
            // 
            this.timePlasma.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePlasma.Location = new System.Drawing.Point(333, 26);
            this.timePlasma.Name = "timePlasma";
            this.timePlasma.ShowUpDown = true;
            this.timePlasma.Size = new System.Drawing.Size(100, 24);
            this.timePlasma.TabIndex = 12;
            this.timePlasma.ValueChanged += new System.EventHandler(this.timePlasma_ValueChanged);
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.BackColor = System.Drawing.Color.Transparent;
            this.label86.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label86.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label86.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label86.Location = new System.Drawing.Point(53, 23);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(124, 20);
            this.label86.TabIndex = 10;
            this.label86.Text = "Operation time:";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(-44, 15);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(0, 18);
            this.label141.TabIndex = 11;
            // 
            // tabPagePlasma
            // 
            this.tabPagePlasma.BackColor = System.Drawing.Color.White;
            this.tabPagePlasma.Controls.Add(this.label33);
            this.tabPagePlasma.Controls.Add(this.groupBox17);
            this.tabPagePlasma.Location = new System.Drawing.Point(4, 4);
            this.tabPagePlasma.Name = "tabPagePlasma";
            this.tabPagePlasma.Size = new System.Drawing.Size(756, 719);
            this.tabPagePlasma.TabIndex = 2;
            this.tabPagePlasma.Text = "PLASMA";
            // 
            // label33
            // 
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label33.ForeColor = System.Drawing.Color.Maroon;
            this.label33.Location = new System.Drawing.Point(0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(756, 28);
            this.label33.TabIndex = 38;
            this.label33.Text = "Process set parameters to achive plasma";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.grBoxPlasma);
            this.groupBox17.Controls.Add(this.label141);
            this.groupBox17.Controls.Add(this.label143);
            this.groupBox17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox17.Location = new System.Drawing.Point(0, 0);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(756, 719);
            this.groupBox17.TabIndex = 37;
            this.groupBox17.TabStop = false;
            // 
            // grBoxPlasma
            // 
            this.grBoxPlasma.Controls.Add(this.groupBox20);
            this.grBoxPlasma.Controls.Add(this.groupBox21);
            this.grBoxPlasma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoxPlasma.Location = new System.Drawing.Point(3, 20);
            this.grBoxPlasma.Name = "grBoxPlasma";
            this.grBoxPlasma.Size = new System.Drawing.Size(750, 696);
            this.grBoxPlasma.TabIndex = 40;
            this.grBoxPlasma.TabStop = false;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.timePlasma);
            this.groupBox20.Controls.Add(this.label139);
            this.groupBox20.Controls.Add(this.label140);
            this.groupBox20.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox20.Location = new System.Drawing.Point(3, 20);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(744, 62);
            this.groupBox20.TabIndex = 38;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Time";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.dEditPlasmaDeviation);
            this.groupBox21.Controls.Add(this.dEditPlasmaSetpoint);
            this.groupBox21.Controls.Add(this.tBoxSetpointPlasma);
            this.groupBox21.Controls.Add(this.label37);
            this.groupBox21.Controls.Add(this.scrollPlasmaDevistion);
            this.groupBox21.Controls.Add(this.label144);
            this.groupBox21.Controls.Add(this.labUnit2SetpointPlasma);
            this.groupBox21.Controls.Add(this.scrollPlasmaSetpoint);
            this.groupBox21.Controls.Add(this.labUnit1SetpointPlasma);
            this.groupBox21.Controls.Add(this.label142);
            this.groupBox21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox21.Location = new System.Drawing.Point(3, 128);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(744, 565);
            this.groupBox21.TabIndex = 39;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Parameters";
            // 
            // dEditPlasmaDeviation
            // 
            this.dEditPlasmaDeviation.Location = new System.Drawing.Point(493, 126);
            this.dEditPlasmaDeviation.Mask = "0";
            this.dEditPlasmaDeviation.MaximumValue = 100D;
            this.dEditPlasmaDeviation.MinimumValue = 0D;
            this.dEditPlasmaDeviation.Name = "dEditPlasmaDeviation";
            this.dEditPlasmaDeviation.ReadOnly = false;
            this.dEditPlasmaDeviation.Size = new System.Drawing.Size(78, 27);
            this.dEditPlasmaDeviation.TabIndex = 41;
            this.dEditPlasmaDeviation.Value = 0D;
            this.dEditPlasmaDeviation.Visible = false;
            this.dEditPlasmaDeviation.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditPlasmaDeviation_EnterOn);
            // 
            // dEditPlasmaSetpoint
            // 
            this.dEditPlasmaSetpoint.Location = new System.Drawing.Point(493, 44);
            this.dEditPlasmaSetpoint.Mask = "0";
            this.dEditPlasmaSetpoint.MaximumValue = 100D;
            this.dEditPlasmaSetpoint.MinimumValue = 0D;
            this.dEditPlasmaSetpoint.Name = "dEditPlasmaSetpoint";
            this.dEditPlasmaSetpoint.ReadOnly = false;
            this.dEditPlasmaSetpoint.Size = new System.Drawing.Size(82, 27);
            this.dEditPlasmaSetpoint.TabIndex = 40;
            this.dEditPlasmaSetpoint.Value = 0D;
            this.dEditPlasmaSetpoint.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditPlasmaSetpoint_EnterOn);
            // 
            // tBoxSetpointPlasma
            // 
            this.tBoxSetpointPlasma.Enabled = false;
            this.tBoxSetpointPlasma.Location = new System.Drawing.Point(495, 82);
            this.tBoxSetpointPlasma.Name = "tBoxSetpointPlasma";
            this.tBoxSetpointPlasma.Size = new System.Drawing.Size(80, 24);
            this.tBoxSetpointPlasma.TabIndex = 39;
            this.tBoxSetpointPlasma.Visible = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.ForeColor = System.Drawing.Color.Green;
            this.label37.Location = new System.Drawing.Point(581, 46);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 18);
            this.label37.TabIndex = 38;
            this.label37.Text = "[%]";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.ForeColor = System.Drawing.Color.Green;
            this.label84.Location = new System.Drawing.Point(320, 24);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(82, 18);
            this.label84.TabIndex = 9;
            this.label84.Text = "[hh:mm:ss]";
            // 
            // timeGas
            // 
            this.timeGas.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timeGas.Location = new System.Drawing.Point(196, 19);
            this.timeGas.Name = "timeGas";
            this.timeGas.ShowUpDown = true;
            this.timeGas.Size = new System.Drawing.Size(100, 24);
            this.timeGas.TabIndex = 8;
            this.timeGas.ValueChanged += new System.EventHandler(this.timeGas_ValueChanged);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(88, 41);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(0, 18);
            this.label85.TabIndex = 7;
            // 
            // label127
            // 
            this.label127.BackColor = System.Drawing.Color.Gray;
            this.label127.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label127.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label127.ForeColor = System.Drawing.Color.White;
            this.label127.Location = new System.Drawing.Point(473, 10);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(753, 25);
            this.label127.TabIndex = 55;
            this.label127.Text = "STAGE INFORMATION";
            this.label127.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPagePump
            // 
            this.tabPagePump.BackColor = System.Drawing.Color.White;
            this.tabPagePump.Controls.Add(this.label32);
            this.tabPagePump.Controls.Add(this.groupBox14);
            this.tabPagePump.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabPagePump.Location = new System.Drawing.Point(4, 4);
            this.tabPagePump.Name = "tabPagePump";
            this.tabPagePump.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePump.Size = new System.Drawing.Size(756, 719);
            this.tabPagePump.TabIndex = 0;
            this.tabPagePump.Text = "PUMPING";
            // 
            // label32
            // 
            this.label32.Dock = System.Windows.Forms.DockStyle.Top;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label32.ForeColor = System.Drawing.Color.Maroon;
            this.label32.Location = new System.Drawing.Point(3, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(750, 28);
            this.label32.TabIndex = 9;
            this.label32.Text = "Process: Pump down chamber to set pressure";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.grBoxPump);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox14.Location = new System.Drawing.Point(3, 3);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(750, 713);
            this.groupBox14.TabIndex = 7;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "groupBox14";
            // 
            // grBoxPump
            // 
            this.grBoxPump.Controls.Add(this.groupBox19);
            this.grBoxPump.Controls.Add(this.groupBox18);
            this.grBoxPump.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoxPump.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grBoxPump.Location = new System.Drawing.Point(3, 18);
            this.grBoxPump.Name = "grBoxPump";
            this.grBoxPump.Size = new System.Drawing.Size(744, 692);
            this.grBoxPump.TabIndex = 8;
            this.grBoxPump.TabStop = false;
            // 
            // groupBox19
            // 
            this.groupBox19.BackColor = System.Drawing.Color.White;
            this.groupBox19.Controls.Add(this.dEditPumpSetpoint);
            this.groupBox19.Controls.Add(this.scrollPumpSetpoint);
            this.groupBox19.Controls.Add(this.label82);
            this.groupBox19.Controls.Add(this.label80);
            this.groupBox19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox19.Location = new System.Drawing.Point(3, 123);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(738, 566);
            this.groupBox19.TabIndex = 28;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Parameters";
            // 
            // dEditPumpSetpoint
            // 
            this.dEditPumpSetpoint.Location = new System.Drawing.Point(490, 48);
            this.dEditPumpSetpoint.Mask = "0.000";
            this.dEditPumpSetpoint.MaximumValue = 1000D;
            this.dEditPumpSetpoint.MinimumValue = 0.001D;
            this.dEditPumpSetpoint.Name = "dEditPumpSetpoint";
            this.dEditPumpSetpoint.ReadOnly = false;
            this.dEditPumpSetpoint.Size = new System.Drawing.Size(74, 26);
            this.dEditPumpSetpoint.TabIndex = 27;
            this.dEditPumpSetpoint.Value = 0.001D;
            this.dEditPumpSetpoint.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditPumpSetpoint_EnterOn);
            // 
            // scrollPumpSetpoint
            // 
            this.scrollPumpSetpoint.Location = new System.Drawing.Point(333, 48);
            this.scrollPumpSetpoint.Maximum = 120000;
            this.scrollPumpSetpoint.Minimum = 1;
            this.scrollPumpSetpoint.Name = "scrollPumpSetpoint";
            this.scrollPumpSetpoint.Size = new System.Drawing.Size(150, 22);
            this.scrollPumpSetpoint.TabIndex = 26;
            this.scrollPumpSetpoint.Value = 10;
            this.scrollPumpSetpoint.ValueChanged += new System.EventHandler(this.scrollPumpSetpoint_ValueChanged);
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label82.ForeColor = System.Drawing.Color.Green;
            this.label82.Location = new System.Drawing.Point(579, 48);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(52, 18);
            this.label82.TabIndex = 4;
            this.label82.Text = "[mBar]";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label80.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label80.Location = new System.Drawing.Point(125, 48);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(172, 18);
            this.label80.TabIndex = 0;
            this.label80.Text = "Pumping down pressure:";
            // 
            // groupBox18
            // 
            this.groupBox18.BackColor = System.Drawing.Color.White;
            this.groupBox18.Controls.Add(this.timePump);
            this.groupBox18.Controls.Add(this.label81);
            this.groupBox18.Controls.Add(this.label83);
            this.groupBox18.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox18.Location = new System.Drawing.Point(3, 23);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(738, 100);
            this.groupBox18.TabIndex = 27;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Time";
            // 
            // timePump
            // 
            this.timePump.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.timePump.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePump.Location = new System.Drawing.Point(333, 34);
            this.timePump.Name = "timePump";
            this.timePump.ShowUpDown = true;
            this.timePump.Size = new System.Drawing.Size(101, 24);
            this.timePump.TabIndex = 5;
            this.timePump.ValueChanged += new System.EventHandler(this.timePump_ValueChanged);
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label81.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label81.Location = new System.Drawing.Point(125, 39);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(172, 18);
            this.label81.TabIndex = 2;
            this.label81.Text = "Max pumping down time:";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label83.ForeColor = System.Drawing.Color.Green;
            this.label83.Location = new System.Drawing.Point(462, 39);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(82, 18);
            this.label83.TabIndex = 6;
            this.label83.Text = "[hh:mm:ss]";
            // 
            // grBoxProgram
            // 
            this.grBoxProgram.Controls.Add(this.label76);
            this.grBoxProgram.Controls.Add(this.label77);
            this.grBoxProgram.Controls.Add(this.tBoxNameProgram);
            this.grBoxProgram.Controls.Add(this.tBoxDescProgram);
            this.grBoxProgram.Location = new System.Drawing.Point(242, 18);
            this.grBoxProgram.Name = "grBoxProgram";
            this.grBoxProgram.Size = new System.Drawing.Size(218, 264);
            this.grBoxProgram.TabIndex = 46;
            this.grBoxProgram.TabStop = false;
            this.grBoxProgram.Text = "Program";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.BackColor = System.Drawing.Color.Transparent;
            this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label76.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label76.Location = new System.Drawing.Point(5, 27);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(58, 20);
            this.label76.TabIndex = 26;
            this.label76.Text = "Name:";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.BackColor = System.Drawing.Color.Transparent;
            this.label77.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label77.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label77.Location = new System.Drawing.Point(5, 93);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(100, 20);
            this.label77.TabIndex = 27;
            this.label77.Text = "Description:";
            // 
            // tBoxNameProgram
            // 
            this.tBoxNameProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxNameProgram.Location = new System.Drawing.Point(10, 57);
            this.tBoxNameProgram.Name = "tBoxNameProgram";
            this.tBoxNameProgram.Size = new System.Drawing.Size(205, 27);
            this.tBoxNameProgram.TabIndex = 28;
            this.tBoxNameProgram.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBoxNameProgram_KeyUp);
            // 
            // tBoxDescProgram
            // 
            this.tBoxDescProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxDescProgram.Location = new System.Drawing.Point(10, 125);
            this.tBoxDescProgram.Multiline = true;
            this.tBoxDescProgram.Name = "tBoxDescProgram";
            this.tBoxDescProgram.Size = new System.Drawing.Size(205, 118);
            this.tBoxDescProgram.TabIndex = 29;
            this.tBoxDescProgram.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBoxDescProgram_KeyUp);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::HPT1000.Properties.Resources.Save_32x32;
            this.btnSave.Location = new System.Drawing.Point(123, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 45);
            this.btnSave.TabIndex = 43;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cBoxMFC3
            // 
            this.cBoxMFC3.AutoSize = true;
            this.cBoxMFC3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxMFC3.Location = new System.Drawing.Point(377, 26);
            this.cBoxMFC3.Name = "cBoxMFC3";
            this.cBoxMFC3.Size = new System.Drawing.Size(79, 22);
            this.cBoxMFC3.TabIndex = 18;
            this.cBoxMFC3.Text = "MFC 3.";
            this.cBoxMFC3.UseVisualStyleBackColor = true;
            this.cBoxMFC3.Click += new System.EventHandler(this.cBoxMFC3_Click);
            // 
            // cBoxGasListMFC1
            // 
            this.cBoxGasListMFC1.FormattingEnabled = true;
            this.cBoxGasListMFC1.Location = new System.Drawing.Point(95, 24);
            this.cBoxGasListMFC1.Name = "cBoxGasListMFC1";
            this.cBoxGasListMFC1.Size = new System.Drawing.Size(80, 26);
            this.cBoxGasListMFC1.TabIndex = 17;
            this.cBoxGasListMFC1.SelectedIndexChanged += new System.EventHandler(this.cBoxGasListMFC1_SelectedIndexChanged);
            // 
            // cBoxMFC2
            // 
            this.cBoxMFC2.AutoSize = true;
            this.cBoxMFC2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxMFC2.Location = new System.Drawing.Point(196, 26);
            this.cBoxMFC2.Name = "cBoxMFC2";
            this.cBoxMFC2.Size = new System.Drawing.Size(79, 22);
            this.cBoxMFC2.TabIndex = 15;
            this.cBoxMFC2.Text = "MFC 2.";
            this.cBoxMFC2.UseVisualStyleBackColor = true;
            this.cBoxMFC2.Click += new System.EventHandler(this.cBoxMFC2_Click);
            // 
            // btnRemoveSubprogram
            // 
            this.btnRemoveSubprogram.Image = global::HPT1000.Properties.Resources.Cancel_32x32;
            this.btnRemoveSubprogram.Location = new System.Drawing.Point(67, 21);
            this.btnRemoveSubprogram.Name = "btnRemoveSubprogram";
            this.btnRemoveSubprogram.Size = new System.Drawing.Size(45, 45);
            this.btnRemoveSubprogram.TabIndex = 43;
            this.btnRemoveSubprogram.UseVisualStyleBackColor = true;
            this.btnRemoveSubprogram.Click += new System.EventHandler(this.btnRemoveSubprogram_Click);
            // 
            // tabControlProcess
            // 
            this.tabControlProcess.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlProcess.Controls.Add(this.tabPagePump);
            this.tabControlProcess.Controls.Add(this.tabPageGas);
            this.tabControlProcess.Controls.Add(this.tabPagePlasma);
            this.tabControlProcess.Controls.Add(this.tabPagePurge);
            this.tabControlProcess.Controls.Add(this.tabPageVent);
            this.tabControlProcess.Controls.Add(this.tabPageMotor);
            this.tabControlProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControlProcess.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControlProcess.ItemSize = new System.Drawing.Size(90, 45);
            this.tabControlProcess.Location = new System.Drawing.Point(466, 49);
            this.tabControlProcess.Multiline = true;
            this.tabControlProcess.Name = "tabControlProcess";
            this.tabControlProcess.SelectedIndex = 0;
            this.tabControlProcess.Size = new System.Drawing.Size(809, 727);
            this.tabControlProcess.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlProcess.TabIndex = 56;
            this.tabControlProcess.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlProcess_DrawItem);
            // 
            // tabPageGas
            // 
            this.tabPageGas.BackColor = System.Drawing.Color.White;
            this.tabPageGas.Controls.Add(this.grBoxGas);
            this.tabPageGas.Controls.Add(this.label23);
            this.tabPageGas.Controls.Add(this.label85);
            this.tabPageGas.Location = new System.Drawing.Point(4, 4);
            this.tabPageGas.Name = "tabPageGas";
            this.tabPageGas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGas.Size = new System.Drawing.Size(756, 719);
            this.tabPageGas.TabIndex = 1;
            this.tabPageGas.Text = "GAS";
            // 
            // grBoxGas
            // 
            this.grBoxGas.Controls.Add(this.groupBox1);
            this.grBoxGas.Controls.Add(this.groupBox5);
            this.grBoxGas.Controls.Add(this.grBoxGasPressure);
            this.grBoxGas.Controls.Add(this.grBoxSelectGasLine);
            this.grBoxGas.Controls.Add(this.grBoxGasFlow);
            this.grBoxGas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grBoxGas.Location = new System.Drawing.Point(3, 23);
            this.grBoxGas.Name = "grBoxGas";
            this.grBoxGas.Size = new System.Drawing.Size(750, 693);
            this.grBoxGas.TabIndex = 34;
            this.grBoxGas.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timeGas);
            this.groupBox1.Controls.Add(this.label84);
            this.groupBox1.Controls.Add(this.label86);
            this.groupBox1.Location = new System.Drawing.Point(6, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 55);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rBtnGasNone);
            this.groupBox5.Controls.Add(this.rBtnModePressure);
            this.groupBox5.Controls.Add(this.rBtnModeFlow);
            this.groupBox5.Location = new System.Drawing.Point(467, 11);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(266, 55);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mode";
            // 
            // rBtnGasNone
            // 
            this.rBtnGasNone.AutoSize = true;
            this.rBtnGasNone.Location = new System.Drawing.Point(256, 31);
            this.rBtnGasNone.Name = "rBtnGasNone";
            this.rBtnGasNone.Size = new System.Drawing.Size(84, 22);
            this.rBtnGasNone.TabIndex = 2;
            this.rBtnGasNone.TabStop = true;
            this.rBtnGasNone.Text = "Uknown";
            this.rBtnGasNone.UseVisualStyleBackColor = true;
            this.rBtnGasNone.Visible = false;
            // 
            // rBtnModePressure
            // 
            this.rBtnModePressure.AutoSize = true;
            this.rBtnModePressure.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rBtnModePressure.Location = new System.Drawing.Point(162, 20);
            this.rBtnModePressure.Name = "rBtnModePressure";
            this.rBtnModePressure.Size = new System.Drawing.Size(89, 22);
            this.rBtnModePressure.TabIndex = 1;
            this.rBtnModePressure.TabStop = true;
            this.rBtnModePressure.Text = "Pressure";
            this.rBtnModePressure.UseVisualStyleBackColor = true;
            this.rBtnModePressure.CheckedChanged += new System.EventHandler(this.rBtnGasMode_CheckedChanged);
            // 
            // rBtnModeFlow
            // 
            this.rBtnModeFlow.AutoSize = true;
            this.rBtnModeFlow.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rBtnModeFlow.Location = new System.Drawing.Point(41, 20);
            this.rBtnModeFlow.Name = "rBtnModeFlow";
            this.rBtnModeFlow.Size = new System.Drawing.Size(73, 22);
            this.rBtnModeFlow.TabIndex = 0;
            this.rBtnModeFlow.TabStop = true;
            this.rBtnModeFlow.Text = "Gases";
            this.rBtnModeFlow.UseVisualStyleBackColor = true;
            this.rBtnModeFlow.CheckedChanged += new System.EventHandler(this.rBtnGasMode_CheckedChanged);
            // 
            // grBoxSelectGasLine
            // 
            this.grBoxSelectGasLine.Controls.Add(this.cBoxVaporiser);
            this.grBoxSelectGasLine.Controls.Add(this.cBoxGasListMFC3);
            this.grBoxSelectGasLine.Controls.Add(this.cBoxMFC2);
            this.grBoxSelectGasLine.Controls.Add(this.cBoxMFC3);
            this.grBoxSelectGasLine.Controls.Add(this.cBoxMFC1);
            this.grBoxSelectGasLine.Controls.Add(this.cBoxGasListMFC2);
            this.grBoxSelectGasLine.Controls.Add(this.cBoxGasListMFC1);
            this.grBoxSelectGasLine.Location = new System.Drawing.Point(7, 79);
            this.grBoxSelectGasLine.Name = "grBoxSelectGasLine";
            this.grBoxSelectGasLine.Size = new System.Drawing.Size(725, 62);
            this.grBoxSelectGasLine.TabIndex = 26;
            this.grBoxSelectGasLine.TabStop = false;
            this.grBoxSelectGasLine.Text = "Select gas lines";
            // 
            // cBoxVaporiser
            // 
            this.cBoxVaporiser.AutoSize = true;
            this.cBoxVaporiser.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxVaporiser.Location = new System.Drawing.Point(560, 26);
            this.cBoxVaporiser.Name = "cBoxVaporiser";
            this.cBoxVaporiser.Size = new System.Drawing.Size(93, 22);
            this.cBoxVaporiser.TabIndex = 23;
            this.cBoxVaporiser.Text = "Vaporiser";
            this.cBoxVaporiser.UseVisualStyleBackColor = true;
            this.cBoxVaporiser.Click += new System.EventHandler(this.cBoxVaporiser_Click);
            // 
            // cBoxGasListMFC3
            // 
            this.cBoxGasListMFC3.FormattingEnabled = true;
            this.cBoxGasListMFC3.Location = new System.Drawing.Point(460, 24);
            this.cBoxGasListMFC3.Name = "cBoxGasListMFC3";
            this.cBoxGasListMFC3.Size = new System.Drawing.Size(80, 26);
            this.cBoxGasListMFC3.TabIndex = 22;
            this.cBoxGasListMFC3.SelectedIndexChanged += new System.EventHandler(this.cBoxGasListMFC3_SelectedIndexChanged);
            // 
            // cBoxMFC1
            // 
            this.cBoxMFC1.AutoSize = true;
            this.cBoxMFC1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxMFC1.Location = new System.Drawing.Point(12, 26);
            this.cBoxMFC1.Name = "cBoxMFC1";
            this.cBoxMFC1.Size = new System.Drawing.Size(79, 22);
            this.cBoxMFC1.TabIndex = 21;
            this.cBoxMFC1.Text = "MFC 1.";
            this.cBoxMFC1.UseVisualStyleBackColor = true;
            this.cBoxMFC1.Click += new System.EventHandler(this.cBoxMFC1_Click);
            // 
            // cBoxGasListMFC2
            // 
            this.cBoxGasListMFC2.FormattingEnabled = true;
            this.cBoxGasListMFC2.Location = new System.Drawing.Point(278, 24);
            this.cBoxGasListMFC2.Name = "cBoxGasListMFC2";
            this.cBoxGasListMFC2.Size = new System.Drawing.Size(80, 26);
            this.cBoxGasListMFC2.TabIndex = 20;
            this.cBoxGasListMFC2.SelectedIndexChanged += new System.EventHandler(this.cBoxGasListMFC2_SelectedIndexChanged);
            // 
            // grBoxGasFlow
            // 
            this.grBoxGasFlow.Controls.Add(this.grBoxMFC1);
            this.grBoxGasFlow.Controls.Add(this.grBoxMFC3);
            this.grBoxGasFlow.Controls.Add(this.grBoxMFC2);
            this.grBoxGasFlow.Controls.Add(this.grBoxVaporiser);
            this.grBoxGasFlow.Location = new System.Drawing.Point(7, 151);
            this.grBoxGasFlow.Name = "grBoxGasFlow";
            this.grBoxGasFlow.Size = new System.Drawing.Size(733, 278);
            this.grBoxGasFlow.TabIndex = 25;
            this.grBoxGasFlow.TabStop = false;
            this.grBoxGasFlow.Text = "Gases mode";
            // 
            // grBoxMFC1
            // 
            this.grBoxMFC1.Controls.Add(this.dEditFlow1Max);
            this.grBoxMFC1.Controls.Add(this.dEditFlow1Min);
            this.grBoxMFC1.Controls.Add(this.dEditFlow1);
            this.grBoxMFC1.Controls.Add(this.label5);
            this.grBoxMFC1.Controls.Add(this.scrollFlow1Min);
            this.grBoxMFC1.Controls.Add(this.label6);
            this.grBoxMFC1.Controls.Add(this.label7);
            this.grBoxMFC1.Controls.Add(this.scrollFlow1Max);
            this.grBoxMFC1.Controls.Add(this.label8);
            this.grBoxMFC1.Controls.Add(this.label9);
            this.grBoxMFC1.Controls.Add(this.scrollFlow1);
            this.grBoxMFC1.Controls.Add(this.label10);
            this.grBoxMFC1.Location = new System.Drawing.Point(6, 26);
            this.grBoxMFC1.Name = "grBoxMFC1";
            this.grBoxMFC1.Size = new System.Drawing.Size(176, 242);
            this.grBoxMFC1.TabIndex = 26;
            this.grBoxMFC1.TabStop = false;
            this.grBoxMFC1.Text = "MFC 1 - Oxygen";
            // 
            // dEditFlow1Max
            // 
            this.dEditFlow1Max.Location = new System.Drawing.Point(110, 191);
            this.dEditFlow1Max.Mask = "0";
            this.dEditFlow1Max.MaximumValue = 1000D;
            this.dEditFlow1Max.MinimumValue = 0D;
            this.dEditFlow1Max.Name = "dEditFlow1Max";
            this.dEditFlow1Max.ReadOnly = false;
            this.dEditFlow1Max.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow1Max.TabIndex = 36;
            this.dEditFlow1Max.Value = 0D;
            this.dEditFlow1Max.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow1Max_EnterOn);
            // 
            // dEditFlow1Min
            // 
            this.dEditFlow1Min.Location = new System.Drawing.Point(110, 124);
            this.dEditFlow1Min.Mask = "0";
            this.dEditFlow1Min.MaximumValue = 1000D;
            this.dEditFlow1Min.MinimumValue = 0D;
            this.dEditFlow1Min.Name = "dEditFlow1Min";
            this.dEditFlow1Min.ReadOnly = false;
            this.dEditFlow1Min.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow1Min.TabIndex = 35;
            this.dEditFlow1Min.Value = 0D;
            this.dEditFlow1Min.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow1Min_EnterOn);
            // 
            // dEditFlow1
            // 
            this.dEditFlow1.Location = new System.Drawing.Point(110, 55);
            this.dEditFlow1.Mask = "0";
            this.dEditFlow1.MaximumValue = 1000D;
            this.dEditFlow1.MinimumValue = 0D;
            this.dEditFlow1.Name = "dEditFlow1";
            this.dEditFlow1.ReadOnly = false;
            this.dEditFlow1.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow1.TabIndex = 34;
            this.dEditFlow1.Value = 0D;
            this.dEditFlow1.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow1_EnterOn);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(5, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 18);
            this.label5.TabIndex = 33;
            this.label5.Text = "Gas flow min:";
            // 
            // scrollFlow1Min
            // 
            this.scrollFlow1Min.Location = new System.Drawing.Point(7, 123);
            this.scrollFlow1Min.Maximum = 1000;
            this.scrollFlow1Min.Name = "scrollFlow1Min";
            this.scrollFlow1Min.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow1Min.TabIndex = 32;
            this.scrollFlow1Min.Value = 10;
            this.scrollFlow1Min.ValueChanged += new System.EventHandler(this.scrollFlow1Min_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.Location = new System.Drawing.Point(126, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "sccm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(5, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 18);
            this.label7.TabIndex = 29;
            this.label7.Text = "Gas flow max:";
            // 
            // scrollFlow1Max
            // 
            this.scrollFlow1Max.Location = new System.Drawing.Point(7, 191);
            this.scrollFlow1Max.Maximum = 1000;
            this.scrollFlow1Max.Name = "scrollFlow1Max";
            this.scrollFlow1Max.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow1Max.TabIndex = 28;
            this.scrollFlow1Max.Value = 10;
            this.scrollFlow1Max.ValueChanged += new System.EventHandler(this.scrollFlow1Max_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Green;
            this.label8.Location = new System.Drawing.Point(126, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "sccm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(5, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 18);
            this.label9.TabIndex = 25;
            this.label9.Text = "Gas flow:";
            // 
            // scrollFlow1
            // 
            this.scrollFlow1.Location = new System.Drawing.Point(7, 54);
            this.scrollFlow1.Name = "scrollFlow1";
            this.scrollFlow1.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow1.TabIndex = 24;
            this.scrollFlow1.Value = 10;
            this.scrollFlow1.ValueChanged += new System.EventHandler(this.scrollFlow1_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Green;
            this.label10.Location = new System.Drawing.Point(126, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 18);
            this.label10.TabIndex = 23;
            this.label10.Text = "sccm";
            // 
            // grBoxMFC3
            // 
            this.grBoxMFC3.Controls.Add(this.dEditFlow3Max);
            this.grBoxMFC3.Controls.Add(this.dEditFlow3Min);
            this.grBoxMFC3.Controls.Add(this.dEditFlow3);
            this.grBoxMFC3.Controls.Add(this.label17);
            this.grBoxMFC3.Controls.Add(this.scrollFlow3Min);
            this.grBoxMFC3.Controls.Add(this.label18);
            this.grBoxMFC3.Controls.Add(this.label19);
            this.grBoxMFC3.Controls.Add(this.scrollFlow3Max);
            this.grBoxMFC3.Controls.Add(this.label20);
            this.grBoxMFC3.Controls.Add(this.label21);
            this.grBoxMFC3.Controls.Add(this.scrollFlow3);
            this.grBoxMFC3.Controls.Add(this.label22);
            this.grBoxMFC3.Location = new System.Drawing.Point(371, 26);
            this.grBoxMFC3.Name = "grBoxMFC3";
            this.grBoxMFC3.Size = new System.Drawing.Size(176, 242);
            this.grBoxMFC3.TabIndex = 34;
            this.grBoxMFC3.TabStop = false;
            this.grBoxMFC3.Text = "MFC 3 - N/A";
            // 
            // dEditFlow3Max
            // 
            this.dEditFlow3Max.Location = new System.Drawing.Point(109, 191);
            this.dEditFlow3Max.Mask = "0";
            this.dEditFlow3Max.MaximumValue = 1000D;
            this.dEditFlow3Max.MinimumValue = 0D;
            this.dEditFlow3Max.Name = "dEditFlow3Max";
            this.dEditFlow3Max.ReadOnly = false;
            this.dEditFlow3Max.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow3Max.TabIndex = 37;
            this.dEditFlow3Max.Value = 0D;
            this.dEditFlow3Max.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow3Max_EnterOn);
            // 
            // dEditFlow3Min
            // 
            this.dEditFlow3Min.Location = new System.Drawing.Point(109, 123);
            this.dEditFlow3Min.Mask = "0";
            this.dEditFlow3Min.MaximumValue = 1000D;
            this.dEditFlow3Min.MinimumValue = 0D;
            this.dEditFlow3Min.Name = "dEditFlow3Min";
            this.dEditFlow3Min.ReadOnly = false;
            this.dEditFlow3Min.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow3Min.TabIndex = 36;
            this.dEditFlow3Min.Value = 0D;
            this.dEditFlow3Min.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow3Min_EnterOn);
            // 
            // dEditFlow3
            // 
            this.dEditFlow3.Location = new System.Drawing.Point(109, 54);
            this.dEditFlow3.Mask = "0";
            this.dEditFlow3.MaximumValue = 1000D;
            this.dEditFlow3.MinimumValue = 0D;
            this.dEditFlow3.Name = "dEditFlow3";
            this.dEditFlow3.ReadOnly = false;
            this.dEditFlow3.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow3.TabIndex = 35;
            this.dEditFlow3.Value = 0D;
            this.dEditFlow3.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow3_EnterOn);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label17.Location = new System.Drawing.Point(5, 96);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 18);
            this.label17.TabIndex = 33;
            this.label17.Text = "Gas flow min:";
            // 
            // scrollFlow3Min
            // 
            this.scrollFlow3Min.Location = new System.Drawing.Point(7, 123);
            this.scrollFlow3Min.Maximum = 1000;
            this.scrollFlow3Min.Name = "scrollFlow3Min";
            this.scrollFlow3Min.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow3Min.TabIndex = 32;
            this.scrollFlow3Min.Value = 10;
            this.scrollFlow3Min.ValueChanged += new System.EventHandler(this.scrollFlow3Min_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Green;
            this.label18.Location = new System.Drawing.Point(125, 218);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(45, 18);
            this.label18.TabIndex = 31;
            this.label18.Text = "sccm";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(5, 164);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 18);
            this.label19.TabIndex = 29;
            this.label19.Text = "Gas flow max:";
            // 
            // scrollFlow3Max
            // 
            this.scrollFlow3Max.Location = new System.Drawing.Point(7, 191);
            this.scrollFlow3Max.Maximum = 1000;
            this.scrollFlow3Max.Name = "scrollFlow3Max";
            this.scrollFlow3Max.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow3Max.TabIndex = 28;
            this.scrollFlow3Max.Value = 10;
            this.scrollFlow3Max.ValueChanged += new System.EventHandler(this.scrollFlow3Max_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Green;
            this.label20.Location = new System.Drawing.Point(125, 149);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(45, 18);
            this.label20.TabIndex = 27;
            this.label20.Text = "sccm";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label21.Location = new System.Drawing.Point(5, 28);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 18);
            this.label21.TabIndex = 25;
            this.label21.Text = "Gas flow:";
            // 
            // scrollFlow3
            // 
            this.scrollFlow3.Location = new System.Drawing.Point(7, 54);
            this.scrollFlow3.Maximum = 1000;
            this.scrollFlow3.Name = "scrollFlow3";
            this.scrollFlow3.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow3.TabIndex = 24;
            this.scrollFlow3.Value = 10;
            this.scrollFlow3.ValueChanged += new System.EventHandler(this.scrollFlow3_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Green;
            this.label22.Location = new System.Drawing.Point(125, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(45, 18);
            this.label22.TabIndex = 23;
            this.label22.Text = "sccm";
            // 
            // grBoxMFC2
            // 
            this.grBoxMFC2.Controls.Add(this.dEditFlow2Max);
            this.grBoxMFC2.Controls.Add(this.dEditFlow2Min);
            this.grBoxMFC2.Controls.Add(this.dEditFlow2);
            this.grBoxMFC2.Controls.Add(this.label11);
            this.grBoxMFC2.Controls.Add(this.scrollFlow2Min);
            this.grBoxMFC2.Controls.Add(this.label12);
            this.grBoxMFC2.Controls.Add(this.label13);
            this.grBoxMFC2.Controls.Add(this.scrollFlow2Max);
            this.grBoxMFC2.Controls.Add(this.label14);
            this.grBoxMFC2.Controls.Add(this.label15);
            this.grBoxMFC2.Controls.Add(this.scrollFlow2);
            this.grBoxMFC2.Controls.Add(this.label16);
            this.grBoxMFC2.Location = new System.Drawing.Point(189, 26);
            this.grBoxMFC2.Name = "grBoxMFC2";
            this.grBoxMFC2.Size = new System.Drawing.Size(176, 242);
            this.grBoxMFC2.TabIndex = 27;
            this.grBoxMFC2.TabStop = false;
            this.grBoxMFC2.Text = "MFC 2 - N/A";
            // 
            // dEditFlow2Max
            // 
            this.dEditFlow2Max.Location = new System.Drawing.Point(110, 191);
            this.dEditFlow2Max.Mask = "0";
            this.dEditFlow2Max.MaximumValue = 1000D;
            this.dEditFlow2Max.MinimumValue = 0D;
            this.dEditFlow2Max.Name = "dEditFlow2Max";
            this.dEditFlow2Max.ReadOnly = false;
            this.dEditFlow2Max.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow2Max.TabIndex = 37;
            this.dEditFlow2Max.Value = 0D;
            this.dEditFlow2Max.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow2Max_EnterOn);
            // 
            // dEditFlow2Min
            // 
            this.dEditFlow2Min.Location = new System.Drawing.Point(110, 123);
            this.dEditFlow2Min.Mask = "0";
            this.dEditFlow2Min.MaximumValue = 1000D;
            this.dEditFlow2Min.MinimumValue = 0D;
            this.dEditFlow2Min.Name = "dEditFlow2Min";
            this.dEditFlow2Min.ReadOnly = false;
            this.dEditFlow2Min.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow2Min.TabIndex = 36;
            this.dEditFlow2Min.Value = 0D;
            this.dEditFlow2Min.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow2Min_EnterOn);
            // 
            // dEditFlow2
            // 
            this.dEditFlow2.Location = new System.Drawing.Point(110, 54);
            this.dEditFlow2.Mask = "0";
            this.dEditFlow2.MaximumValue = 1000D;
            this.dEditFlow2.MinimumValue = 0D;
            this.dEditFlow2.Name = "dEditFlow2";
            this.dEditFlow2.ReadOnly = false;
            this.dEditFlow2.Size = new System.Drawing.Size(61, 26);
            this.dEditFlow2.TabIndex = 35;
            this.dEditFlow2.Value = 0D;
            this.dEditFlow2.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditFlow2_EnterOn);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label11.Location = new System.Drawing.Point(5, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 18);
            this.label11.TabIndex = 33;
            this.label11.Text = "Gas flow min:";
            // 
            // scrollFlow2Min
            // 
            this.scrollFlow2Min.Location = new System.Drawing.Point(7, 123);
            this.scrollFlow2Min.Maximum = 1000;
            this.scrollFlow2Min.Name = "scrollFlow2Min";
            this.scrollFlow2Min.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow2Min.TabIndex = 32;
            this.scrollFlow2Min.Value = 10;
            this.scrollFlow2Min.ValueChanged += new System.EventHandler(this.scrollFlow2Min_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Green;
            this.label12.Location = new System.Drawing.Point(126, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 18);
            this.label12.TabIndex = 31;
            this.label12.Text = "sccm";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label13.Location = new System.Drawing.Point(5, 164);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(103, 18);
            this.label13.TabIndex = 29;
            this.label13.Text = "Gas flow max:";
            // 
            // scrollFlow2Max
            // 
            this.scrollFlow2Max.Location = new System.Drawing.Point(7, 191);
            this.scrollFlow2Max.Maximum = 1000;
            this.scrollFlow2Max.Name = "scrollFlow2Max";
            this.scrollFlow2Max.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow2Max.TabIndex = 28;
            this.scrollFlow2Max.Value = 10;
            this.scrollFlow2Max.ValueChanged += new System.EventHandler(this.scrollFlow2Max_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Green;
            this.label14.Location = new System.Drawing.Point(126, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 18);
            this.label14.TabIndex = 27;
            this.label14.Text = "sccm";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label15.Location = new System.Drawing.Point(5, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 18);
            this.label15.TabIndex = 25;
            this.label15.Text = "Gas flow:";
            // 
            // scrollFlow2
            // 
            this.scrollFlow2.Location = new System.Drawing.Point(7, 54);
            this.scrollFlow2.Maximum = 1000;
            this.scrollFlow2.Name = "scrollFlow2";
            this.scrollFlow2.Size = new System.Drawing.Size(100, 22);
            this.scrollFlow2.TabIndex = 24;
            this.scrollFlow2.Value = 10;
            this.scrollFlow2.ValueChanged += new System.EventHandler(this.scrollFlow2_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Green;
            this.label16.Location = new System.Drawing.Point(126, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 18);
            this.label16.TabIndex = 23;
            this.label16.Text = "sccm";
            // 
            // grBoxVaporiser
            // 
            this.grBoxVaporiser.Controls.Add(this.dEditGasVaporCycleTime);
            this.grBoxVaporiser.Controls.Add(this.dEditGasVaporOnTime);
            this.grBoxVaporiser.Controls.Add(this.dEditDosing);
            this.grBoxVaporiser.Controls.Add(this.scrolBarDosing);
            this.grBoxVaporiser.Controls.Add(this.labUnitDosing);
            this.grBoxVaporiser.Controls.Add(this.labDosing);
            this.grBoxVaporiser.Controls.Add(this.scrollGasVaporOnTime);
            this.grBoxVaporiser.Controls.Add(this.labUnitOnTime);
            this.grBoxVaporiser.Controls.Add(this.scrollGasVaporCycleTime);
            this.grBoxVaporiser.Controls.Add(this.labUnitCycle);
            this.grBoxVaporiser.Controls.Add(this.labOnTIme);
            this.grBoxVaporiser.Controls.Add(this.labCycle);
            this.grBoxVaporiser.Location = new System.Drawing.Point(553, 26);
            this.grBoxVaporiser.Name = "grBoxVaporiser";
            this.grBoxVaporiser.Size = new System.Drawing.Size(176, 242);
            this.grBoxVaporiser.TabIndex = 12;
            this.grBoxVaporiser.TabStop = false;
            this.grBoxVaporiser.Text = "Vaporiser";
            // 
            // dEditGasVaporCycleTime
            // 
            this.dEditGasVaporCycleTime.Location = new System.Drawing.Point(110, 54);
            this.dEditGasVaporCycleTime.Mask = "0";
            this.dEditGasVaporCycleTime.MaximumValue = 10000D;
            this.dEditGasVaporCycleTime.MinimumValue = 0D;
            this.dEditGasVaporCycleTime.Name = "dEditGasVaporCycleTime";
            this.dEditGasVaporCycleTime.ReadOnly = false;
            this.dEditGasVaporCycleTime.Size = new System.Drawing.Size(59, 28);
            this.dEditGasVaporCycleTime.TabIndex = 45;
            this.dEditGasVaporCycleTime.Value = 0D;
            this.dEditGasVaporCycleTime.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasVaporCycleTime_EnterOn);
            // 
            // dEditGasVaporOnTime
            // 
            this.dEditGasVaporOnTime.Location = new System.Drawing.Point(110, 130);
            this.dEditGasVaporOnTime.Mask = "0";
            this.dEditGasVaporOnTime.MaximumValue = 100D;
            this.dEditGasVaporOnTime.MinimumValue = 0D;
            this.dEditGasVaporOnTime.Name = "dEditGasVaporOnTime";
            this.dEditGasVaporOnTime.ReadOnly = false;
            this.dEditGasVaporOnTime.Size = new System.Drawing.Size(58, 30);
            this.dEditGasVaporOnTime.TabIndex = 44;
            this.dEditGasVaporOnTime.Value = 0D;
            this.dEditGasVaporOnTime.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditGasVaporOnTime_EnterOn);
            // 
            // dEditDosing
            // 
            this.dEditDosing.Location = new System.Drawing.Point(15, 54);
            this.dEditDosing.Margin = new System.Windows.Forms.Padding(4);
            this.dEditDosing.Mask = "0";
            this.dEditDosing.MaximumValue = 1000D;
            this.dEditDosing.MinimumValue = 0D;
            this.dEditDosing.Name = "dEditDosing";
            this.dEditDosing.ReadOnly = false;
            this.dEditDosing.Size = new System.Drawing.Size(73, 28);
            this.dEditDosing.TabIndex = 34;
            this.dEditDosing.Value = 0D;
            this.dEditDosing.EnterOn += new HPT1000.GUI.Cotrols.DoubleEdit.MakeOperation(this.dEditDosing_EnterOn);
            // 
            // scrolBarDosing
            // 
            this.scrolBarDosing.Location = new System.Drawing.Point(17, 95);
            this.scrolBarDosing.Maximum = 1009;
            this.scrolBarDosing.Name = "scrolBarDosing";
            this.scrolBarDosing.Size = new System.Drawing.Size(100, 22);
            this.scrolBarDosing.TabIndex = 33;
            this.scrolBarDosing.ValueChanged += new System.EventHandler(this.scrolBarDosing_ValueChanged);
            // 
            // labUnitDosing
            // 
            this.labUnitDosing.AutoSize = true;
            this.labUnitDosing.ForeColor = System.Drawing.Color.Green;
            this.labUnitDosing.Location = new System.Drawing.Point(111, 58);
            this.labUnitDosing.Name = "labUnitDosing";
            this.labUnitDosing.Size = new System.Drawing.Size(52, 18);
            this.labUnitDosing.TabIndex = 32;
            this.labUnitDosing.Text = "uL/min";
            // 
            // labDosing
            // 
            this.labDosing.AutoSize = true;
            this.labDosing.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labDosing.Location = new System.Drawing.Point(5, 28);
            this.labDosing.Name = "labDosing";
            this.labDosing.Size = new System.Drawing.Size(59, 18);
            this.labDosing.TabIndex = 31;
            this.labDosing.Text = "Dosing:";
            // 
            // scrollGasVaporOnTime
            // 
            this.scrollGasVaporOnTime.Location = new System.Drawing.Point(7, 130);
            this.scrollGasVaporOnTime.Maximum = 109;
            this.scrollGasVaporOnTime.Name = "scrollGasVaporOnTime";
            this.scrollGasVaporOnTime.Size = new System.Drawing.Size(100, 22);
            this.scrollGasVaporOnTime.TabIndex = 30;
            this.scrollGasVaporOnTime.Value = 10;
            this.scrollGasVaporOnTime.ValueChanged += new System.EventHandler(this.scrollGasVaporOnTime_ValueChanged);
            // 
            // labUnitOnTime
            // 
            this.labUnitOnTime.AutoSize = true;
            this.labUnitOnTime.ForeColor = System.Drawing.Color.Green;
            this.labUnitOnTime.Location = new System.Drawing.Point(146, 157);
            this.labUnitOnTime.Name = "labUnitOnTime";
            this.labUnitOnTime.Size = new System.Drawing.Size(21, 18);
            this.labUnitOnTime.TabIndex = 29;
            this.labUnitOnTime.Text = "%";
            // 
            // scrollGasVaporCycleTime
            // 
            this.scrollGasVaporCycleTime.Location = new System.Drawing.Point(7, 54);
            this.scrollGasVaporCycleTime.Maximum = 10009;
            this.scrollGasVaporCycleTime.Name = "scrollGasVaporCycleTime";
            this.scrollGasVaporCycleTime.Size = new System.Drawing.Size(100, 22);
            this.scrollGasVaporCycleTime.TabIndex = 27;
            this.scrollGasVaporCycleTime.Value = 10;
            this.scrollGasVaporCycleTime.ValueChanged += new System.EventHandler(this.scrollGasVaporCycleTime_ValueChanged);
            // 
            // labUnitCycle
            // 
            this.labUnitCycle.AutoSize = true;
            this.labUnitCycle.ForeColor = System.Drawing.Color.Green;
            this.labUnitCycle.Location = new System.Drawing.Point(142, 86);
            this.labUnitCycle.Name = "labUnitCycle";
            this.labUnitCycle.Size = new System.Drawing.Size(29, 18);
            this.labUnitCycle.TabIndex = 26;
            this.labUnitCycle.Text = "ms";
            // 
            // labOnTIme
            // 
            this.labOnTIme.AutoSize = true;
            this.labOnTIme.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labOnTIme.Location = new System.Drawing.Point(5, 101);
            this.labOnTIme.Name = "labOnTIme";
            this.labOnTIme.Size = new System.Drawing.Size(64, 18);
            this.labOnTIme.TabIndex = 2;
            this.labOnTIme.Text = "On time:";
            // 
            // labCycle
            // 
            this.labCycle.AutoSize = true;
            this.labCycle.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labCycle.Location = new System.Drawing.Point(5, 28);
            this.labCycle.Name = "labCycle";
            this.labCycle.Size = new System.Drawing.Size(77, 18);
            this.labCycle.TabIndex = 1;
            this.labCycle.Text = "Cycle time";
            // 
            // label23
            // 
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label23.ForeColor = System.Drawing.Color.Maroon;
            this.label23.Location = new System.Drawing.Point(3, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(750, 20);
            this.label23.TabIndex = 27;
            this.label23.Text = "Process: Gas mixing and dosing to set pressure";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageMotor
            // 
            this.tabPageMotor.Controls.Add(this.groupBox7);
            this.tabPageMotor.Controls.Add(this.labTitelMotor);
            this.tabPageMotor.Location = new System.Drawing.Point(4, 4);
            this.tabPageMotor.Name = "tabPageMotor";
            this.tabPageMotor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotor.Size = new System.Drawing.Size(756, 719);
            this.tabPageMotor.TabIndex = 5;
            this.tabPageMotor.Text = "MOTOR";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.grBoxSelectMotor);
            this.groupBox7.Controls.Add(this.grBoxMotor2);
            this.groupBox7.Controls.Add(this.grBoxMotor1);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 31);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(750, 685);
            this.groupBox7.TabIndex = 33;
            this.groupBox7.TabStop = false;
            // 
            // grBoxSelectMotor
            // 
            this.grBoxSelectMotor.Controls.Add(this.cBoxActiveMotor1);
            this.grBoxSelectMotor.Controls.Add(this.cBoxActiveMotor2);
            this.grBoxSelectMotor.Location = new System.Drawing.Point(1, 2);
            this.grBoxSelectMotor.Name = "grBoxSelectMotor";
            this.grBoxSelectMotor.Size = new System.Drawing.Size(744, 64);
            this.grBoxSelectMotor.TabIndex = 35;
            this.grBoxSelectMotor.TabStop = false;
            this.grBoxSelectMotor.Text = "Select motor";
            // 
            // cBoxActiveMotor1
            // 
            this.cBoxActiveMotor1.AutoSize = true;
            this.cBoxActiveMotor1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxActiveMotor1.Location = new System.Drawing.Point(13, 30);
            this.cBoxActiveMotor1.Name = "cBoxActiveMotor1";
            this.cBoxActiveMotor1.Size = new System.Drawing.Size(82, 22);
            this.cBoxActiveMotor1.TabIndex = 34;
            this.cBoxActiveMotor1.Text = "Motor 1";
            this.cBoxActiveMotor1.UseVisualStyleBackColor = true;
            this.cBoxActiveMotor1.Click += new System.EventHandler(this.cBoxActiveMotor1_Click);
            // 
            // cBoxActiveMotor2
            // 
            this.cBoxActiveMotor2.AutoSize = true;
            this.cBoxActiveMotor2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxActiveMotor2.Location = new System.Drawing.Point(384, 28);
            this.cBoxActiveMotor2.Name = "cBoxActiveMotor2";
            this.cBoxActiveMotor2.Size = new System.Drawing.Size(82, 22);
            this.cBoxActiveMotor2.TabIndex = 34;
            this.cBoxActiveMotor2.Text = "Motor 2";
            this.cBoxActiveMotor2.UseVisualStyleBackColor = true;
            this.cBoxActiveMotor2.Click += new System.EventHandler(this.cBoxActiveMotor2_Click);
            // 
            // grBoxMotor2
            // 
            this.grBoxMotor2.Controls.Add(this.groupBox9);
            this.grBoxMotor2.Enabled = false;
            this.grBoxMotor2.Location = new System.Drawing.Point(376, 72);
            this.grBoxMotor2.Name = "grBoxMotor2";
            this.grBoxMotor2.Size = new System.Drawing.Size(365, 611);
            this.grBoxMotor2.TabIndex = 32;
            this.grBoxMotor2.TabStop = false;
            this.grBoxMotor2.Text = "Motor Driver 2";
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.White;
            this.groupBox9.Controls.Add(this.groupBox8);
            this.groupBox9.Controls.Add(this.dateTimeMotor2);
            this.groupBox9.Controls.Add(this.label42);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(3, 20);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(359, 588);
            this.groupBox9.TabIndex = 30;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Time";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.White;
            this.groupBox8.Controls.Add(this.label43);
            this.groupBox8.Controls.Add(this.cBoxStateMotor2);
            this.groupBox8.Location = new System.Drawing.Point(3, 335);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(359, 102);
            this.groupBox8.TabIndex = 29;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Parameters";
            this.groupBox8.Visible = false;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label43.Location = new System.Drawing.Point(55, 55);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(90, 18);
            this.label43.TabIndex = 38;
            this.label43.Text = "State motor:";
            // 
            // cBoxStateMotor2
            // 
            this.cBoxStateMotor2.AutoSize = true;
            this.cBoxStateMotor2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxStateMotor2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxStateMotor2.Location = new System.Drawing.Point(182, 54);
            this.cBoxStateMotor2.Name = "cBoxStateMotor2";
            this.cBoxStateMotor2.Size = new System.Drawing.Size(64, 24);
            this.cBoxStateMotor2.TabIndex = 37;
            this.cBoxStateMotor2.Text = "OFF";
            this.cBoxStateMotor2.UseVisualStyleBackColor = true;
            this.cBoxStateMotor2.Click += new System.EventHandler(this.cBoxStateMotor2_Click);
            // 
            // dateTimeMotor2
            // 
            this.dateTimeMotor2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimeMotor2.Location = new System.Drawing.Point(130, 56);
            this.dateTimeMotor2.Name = "dateTimeMotor2";
            this.dateTimeMotor2.ShowUpDown = true;
            this.dateTimeMotor2.Size = new System.Drawing.Size(101, 24);
            this.dateTimeMotor2.TabIndex = 5;
            this.dateTimeMotor2.ValueChanged += new System.EventHandler(this.dateTimeMotor2_ValueChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.ForeColor = System.Drawing.Color.Green;
            this.label42.Location = new System.Drawing.Point(241, 61);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(82, 18);
            this.label42.TabIndex = 6;
            this.label42.Text = "[hh:mm:ss]";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label39.Location = new System.Drawing.Point(17, 61);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(99, 18);
            this.label39.TabIndex = 2;
            this.label39.Text = "Time operate:";
            // 
            // grBoxMotor1
            // 
            this.grBoxMotor1.Controls.Add(this.groupBox3);
            this.grBoxMotor1.Enabled = false;
            this.grBoxMotor1.Location = new System.Drawing.Point(5, 72);
            this.grBoxMotor1.Name = "grBoxMotor1";
            this.grBoxMotor1.Size = new System.Drawing.Size(365, 611);
            this.grBoxMotor1.TabIndex = 31;
            this.grBoxMotor1.TabStop = false;
            this.grBoxMotor1.Text = "Motor Driver 1";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.dateTimeMotor1);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 588);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Time";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.cBoxStateMotor1);
            this.groupBox4.Location = new System.Drawing.Point(3, 334);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(359, 103);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parameters";
            this.groupBox4.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label38.Location = new System.Drawing.Point(57, 55);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(90, 18);
            this.label38.TabIndex = 38;
            this.label38.Text = "State motor:";
            // 
            // cBoxStateMotor1
            // 
            this.cBoxStateMotor1.AutoSize = true;
            this.cBoxStateMotor1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxStateMotor1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxStateMotor1.Location = new System.Drawing.Point(180, 54);
            this.cBoxStateMotor1.Name = "cBoxStateMotor1";
            this.cBoxStateMotor1.Size = new System.Drawing.Size(64, 24);
            this.cBoxStateMotor1.TabIndex = 37;
            this.cBoxStateMotor1.Text = "OFF";
            this.cBoxStateMotor1.UseVisualStyleBackColor = true;
            this.cBoxStateMotor1.Click += new System.EventHandler(this.cBoxStateMotor1_Click);
            // 
            // dateTimeMotor1
            // 
            this.dateTimeMotor1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimeMotor1.Location = new System.Drawing.Point(139, 56);
            this.dateTimeMotor1.Name = "dateTimeMotor1";
            this.dateTimeMotor1.ShowUpDown = true;
            this.dateTimeMotor1.Size = new System.Drawing.Size(101, 24);
            this.dateTimeMotor1.TabIndex = 5;
            this.dateTimeMotor1.ValueChanged += new System.EventHandler(this.dateTimeMotor1_ValueChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.Color.Green;
            this.label41.Location = new System.Drawing.Point(263, 61);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(82, 18);
            this.label41.TabIndex = 6;
            this.label41.Text = "[hh:mm:ss]";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label40.Location = new System.Drawing.Point(24, 61);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(99, 18);
            this.label40.TabIndex = 2;
            this.label40.Text = "Time operate:";
            // 
            // labTitelMotor
            // 
            this.labTitelMotor.Dock = System.Windows.Forms.DockStyle.Top;
            this.labTitelMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labTitelMotor.ForeColor = System.Drawing.Color.Maroon;
            this.labTitelMotor.Location = new System.Drawing.Point(3, 3);
            this.labTitelMotor.Name = "labTitelMotor";
            this.labTitelMotor.Size = new System.Drawing.Size(750, 28);
            this.labTitelMotor.TabIndex = 24;
            this.labTitelMotor.Text = "Process motor chamber";
            this.labTitelMotor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grBoxSubprogram
            // 
            this.grBoxSubprogram.Controls.Add(this.cBoxMotor);
            this.grBoxSubprogram.Controls.Add(this.label78);
            this.grBoxSubprogram.Controls.Add(this.tBoxDescSubprgoram);
            this.grBoxSubprogram.Controls.Add(this.label79);
            this.grBoxSubprogram.Controls.Add(this.tBoxNameSubprogram);
            this.grBoxSubprogram.Controls.Add(this.label153);
            this.grBoxSubprogram.Controls.Add(this.cBoxPump);
            this.grBoxSubprogram.Controls.Add(this.cBoxGas);
            this.grBoxSubprogram.Controls.Add(this.cBoxPower);
            this.grBoxSubprogram.Controls.Add(this.cBoxVent);
            this.grBoxSubprogram.Controls.Add(this.cBoxPurge);
            this.grBoxSubprogram.Location = new System.Drawing.Point(242, 287);
            this.grBoxSubprogram.Name = "grBoxSubprogram";
            this.grBoxSubprogram.Size = new System.Drawing.Size(218, 449);
            this.grBoxSubprogram.TabIndex = 47;
            this.grBoxSubprogram.TabStop = false;
            this.grBoxSubprogram.Text = "Sub-program";
            // 
            // cBoxMotor
            // 
            this.cBoxMotor.AutoSize = true;
            this.cBoxMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxMotor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxMotor.Location = new System.Drawing.Point(49, 409);
            this.cBoxMotor.Name = "cBoxMotor";
            this.cBoxMotor.Size = new System.Drawing.Size(74, 24);
            this.cBoxMotor.TabIndex = 43;
            this.cBoxMotor.Text = "Motor";
            this.cBoxMotor.UseVisualStyleBackColor = true;
            this.cBoxMotor.CheckedChanged += new System.EventHandler(this.cBoxProcess_CheckedChanged);
            this.cBoxMotor.Click += new System.EventHandler(this.cBoxProcess_Click);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.BackColor = System.Drawing.Color.Transparent;
            this.label78.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label78.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label78.Location = new System.Drawing.Point(3, 89);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(100, 20);
            this.label78.TabIndex = 41;
            this.label78.Text = "Description:";
            // 
            // tBoxDescSubprgoram
            // 
            this.tBoxDescSubprgoram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxDescSubprgoram.Location = new System.Drawing.Point(10, 119);
            this.tBoxDescSubprgoram.Multiline = true;
            this.tBoxDescSubprgoram.Name = "tBoxDescSubprgoram";
            this.tBoxDescSubprgoram.Size = new System.Drawing.Size(205, 118);
            this.tBoxDescSubprgoram.TabIndex = 42;
            this.tBoxDescSubprgoram.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBoxDescSubprgoram_KeyUp);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.BackColor = System.Drawing.Color.Transparent;
            this.label79.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label79.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label79.Location = new System.Drawing.Point(3, 30);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(58, 20);
            this.label79.TabIndex = 32;
            this.label79.Text = "Name:";
            // 
            // tBoxNameSubprogram
            // 
            this.tBoxNameSubprogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tBoxNameSubprogram.Location = new System.Drawing.Point(10, 58);
            this.tBoxNameSubprogram.Name = "tBoxNameSubprogram";
            this.tBoxNameSubprogram.Size = new System.Drawing.Size(205, 27);
            this.tBoxNameSubprogram.TabIndex = 33;
            this.tBoxNameSubprogram.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBoxNameSubprogram_KeyUp);
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.BackColor = System.Drawing.Color.Transparent;
            this.label153.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label153.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label153.Location = new System.Drawing.Point(5, 253);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(66, 20);
            this.label153.TabIndex = 35;
            this.label153.Text = "Stages:";
            // 
            // cBoxPump
            // 
            this.cBoxPump.AutoSize = true;
            this.cBoxPump.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxPump.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxPump.Location = new System.Drawing.Point(49, 277);
            this.cBoxPump.Name = "cBoxPump";
            this.cBoxPump.Size = new System.Drawing.Size(96, 24);
            this.cBoxPump.TabIndex = 36;
            this.cBoxPump.Text = "Pumping";
            this.cBoxPump.UseVisualStyleBackColor = true;
            this.cBoxPump.CheckedChanged += new System.EventHandler(this.cBoxProcess_CheckedChanged);
            this.cBoxPump.Click += new System.EventHandler(this.cBoxProcess_Click);
            // 
            // cBoxGas
            // 
            this.cBoxGas.AutoSize = true;
            this.cBoxGas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxGas.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxGas.Location = new System.Drawing.Point(49, 304);
            this.cBoxGas.Name = "cBoxGas";
            this.cBoxGas.Size = new System.Drawing.Size(62, 24);
            this.cBoxGas.TabIndex = 37;
            this.cBoxGas.Text = "Gas";
            this.cBoxGas.UseVisualStyleBackColor = true;
            this.cBoxGas.CheckedChanged += new System.EventHandler(this.cBoxProcess_CheckedChanged);
            this.cBoxGas.Click += new System.EventHandler(this.cBoxProcess_Click);
            // 
            // cBoxPower
            // 
            this.cBoxPower.AutoSize = true;
            this.cBoxPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxPower.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxPower.Location = new System.Drawing.Point(49, 331);
            this.cBoxPower.Name = "cBoxPower";
            this.cBoxPower.Size = new System.Drawing.Size(87, 24);
            this.cBoxPower.TabIndex = 38;
            this.cBoxPower.Text = "Plasma";
            this.cBoxPower.UseVisualStyleBackColor = true;
            this.cBoxPower.CheckedChanged += new System.EventHandler(this.cBoxProcess_CheckedChanged);
            this.cBoxPower.Click += new System.EventHandler(this.cBoxProcess_Click);
            // 
            // cBoxVent
            // 
            this.cBoxVent.AutoSize = true;
            this.cBoxVent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxVent.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxVent.Location = new System.Drawing.Point(49, 384);
            this.cBoxVent.Name = "cBoxVent";
            this.cBoxVent.Size = new System.Drawing.Size(87, 24);
            this.cBoxVent.TabIndex = 40;
            this.cBoxVent.Text = "Venting";
            this.cBoxVent.UseVisualStyleBackColor = true;
            this.cBoxVent.CheckedChanged += new System.EventHandler(this.cBoxProcess_CheckedChanged);
            this.cBoxVent.Click += new System.EventHandler(this.cBoxProcess_Click);
            // 
            // cBoxPurge
            // 
            this.cBoxPurge.AutoSize = true;
            this.cBoxPurge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxPurge.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cBoxPurge.Location = new System.Drawing.Point(49, 358);
            this.cBoxPurge.Name = "cBoxPurge";
            this.cBoxPurge.Size = new System.Drawing.Size(75, 24);
            this.cBoxPurge.TabIndex = 39;
            this.cBoxPurge.Text = "Purge";
            this.cBoxPurge.UseVisualStyleBackColor = true;
            this.cBoxPurge.CheckedChanged += new System.EventHandler(this.cBoxProcess_CheckedChanged);
            this.cBoxPurge.Click += new System.EventHandler(this.cBoxProcess_Click);
            // 
            // treeViewProgram
            // 
            this.treeViewProgram.AllowDrop = true;
            this.treeViewProgram.ContextMenuStrip = this.contextMenu;
            this.treeViewProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.treeViewProgram.ImageIndex = 0;
            this.treeViewProgram.ImageList = this.imageList1;
            this.treeViewProgram.Location = new System.Drawing.Point(6, 24);
            this.treeViewProgram.Name = "treeViewProgram";
            treeNode1.ImageIndex = 3;
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.ImageIndex = 4;
            treeNode2.Name = "Node4";
            treeNode2.Text = "Node4";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "Node3";
            treeNode3.Text = "Subprograms";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Program 1";
            treeNode5.Name = "Node11";
            treeNode5.Text = "Node11";
            treeNode6.ImageIndex = 1;
            treeNode6.Name = "Node10";
            treeNode6.Text = "Subprograms";
            treeNode7.Name = "Node9";
            treeNode7.Text = "Program 2";
            treeNode8.Name = "Node2";
            treeNode8.Text = "Programs list";
            this.treeViewProgram.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this.treeViewProgram.SelectedImageIndex = 0;
            this.treeViewProgram.Size = new System.Drawing.Size(235, 564);
            this.treeViewProgram.TabIndex = 45;
            this.treeViewProgram.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewProgram_BeforeSelect);
            this.treeViewProgram.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewProgram_AfterSelect);
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_AddProgram,
            this.menuItem_AddSubprogram,
            this.toolStripSeparator,
            this.menuItem_RemoveProgram,
            this.menuItem_RemoveSubprogram});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(218, 106);
            // 
            // menuItem_AddProgram
            // 
            this.menuItem_AddProgram.Name = "menuItem_AddProgram";
            this.menuItem_AddProgram.Size = new System.Drawing.Size(217, 24);
            this.menuItem_AddProgram.Text = "Add program";
            this.menuItem_AddProgram.Click += new System.EventHandler(this.menuItem_AddProgram_Click);
            // 
            // menuItem_AddSubprogram
            // 
            this.menuItem_AddSubprogram.Name = "menuItem_AddSubprogram";
            this.menuItem_AddSubprogram.Size = new System.Drawing.Size(217, 24);
            this.menuItem_AddSubprogram.Text = "Add subprogram";
            this.menuItem_AddSubprogram.Click += new System.EventHandler(this.menuItem_AddSubprogram_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(214, 6);
            // 
            // menuItem_RemoveProgram
            // 
            this.menuItem_RemoveProgram.Name = "menuItem_RemoveProgram";
            this.menuItem_RemoveProgram.Size = new System.Drawing.Size(217, 24);
            this.menuItem_RemoveProgram.Text = "Remove program";
            this.menuItem_RemoveProgram.Click += new System.EventHandler(this.menuItem_RemoveProgram_Click);
            // 
            // menuItem_RemoveSubprogram
            // 
            this.menuItem_RemoveSubprogram.Name = "menuItem_RemoveSubprogram";
            this.menuItem_RemoveSubprogram.Size = new System.Drawing.Size(217, 24);
            this.menuItem_RemoveSubprogram.Text = "Remove subprogram";
            this.menuItem_RemoveSubprogram.Click += new System.EventHandler(this.menuItem_RemoveSubprogram_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Program.png");
            this.imageList1.Images.SetKeyName(1, "Plasma.jpg");
            this.imageList1.Images.SetKeyName(2, "Subprogram.png");
            this.imageList1.Images.SetKeyName(3, "Purge.jpg");
            // 
            // btnRemoveProgram
            // 
            this.btnRemoveProgram.Image = global::HPT1000.Properties.Resources.Cancel_32x32;
            this.btnRemoveProgram.Location = new System.Drawing.Point(67, 21);
            this.btnRemoveProgram.Name = "btnRemoveProgram";
            this.btnRemoveProgram.Size = new System.Drawing.Size(45, 45);
            this.btnRemoveProgram.TabIndex = 41;
            this.btnRemoveProgram.UseVisualStyleBackColor = true;
            this.btnRemoveProgram.Click += new System.EventHandler(this.btnRemoveProgram_Click);
            // 
            // grBoxPrograms
            // 
            this.grBoxPrograms.Controls.Add(this.grBoxSubprogramToolBtn);
            this.grBoxPrograms.Controls.Add(this.grBoxProgramToolBtn);
            this.grBoxPrograms.Controls.Add(this.treeViewProgram);
            this.grBoxPrograms.Controls.Add(this.grBoxSubprogram);
            this.grBoxPrograms.Controls.Add(this.grBoxProgram);
            this.grBoxPrograms.Location = new System.Drawing.Point(4, 38);
            this.grBoxPrograms.Name = "grBoxPrograms";
            this.grBoxPrograms.Size = new System.Drawing.Size(463, 745);
            this.grBoxPrograms.TabIndex = 53;
            this.grBoxPrograms.TabStop = false;
            this.grBoxPrograms.Text = "Programs";
            // 
            // grBoxSubprogramToolBtn
            // 
            this.grBoxSubprogramToolBtn.Controls.Add(this.btnAddNewSubprogram);
            this.grBoxSubprogramToolBtn.Controls.Add(this.btnRemoveSubprogram);
            this.grBoxSubprogramToolBtn.Controls.Add(this.btnDownSubprogram);
            this.grBoxSubprogramToolBtn.Controls.Add(this.btnUpSubprogram);
            this.grBoxSubprogramToolBtn.Location = new System.Drawing.Point(6, 664);
            this.grBoxSubprogramToolBtn.Name = "grBoxSubprogramToolBtn";
            this.grBoxSubprogramToolBtn.Size = new System.Drawing.Size(232, 70);
            this.grBoxSubprogramToolBtn.TabIndex = 51;
            this.grBoxSubprogramToolBtn.TabStop = false;
            this.grBoxSubprogramToolBtn.Text = "Subprogram";
            // 
            // btnAddNewSubprogram
            // 
            this.btnAddNewSubprogram.Image = global::HPT1000.Properties.Resources.Add_32x32;
            this.btnAddNewSubprogram.Location = new System.Drawing.Point(9, 21);
            this.btnAddNewSubprogram.Name = "btnAddNewSubprogram";
            this.btnAddNewSubprogram.Size = new System.Drawing.Size(45, 45);
            this.btnAddNewSubprogram.TabIndex = 42;
            this.btnAddNewSubprogram.UseVisualStyleBackColor = true;
            this.btnAddNewSubprogram.Click += new System.EventHandler(this.btnAddNewSubprogram_Click);
            // 
            // btnDownSubprogram
            // 
            this.btnDownSubprogram.Image = global::HPT1000.Properties.Resources.Next_32x32;
            this.btnDownSubprogram.Location = new System.Drawing.Point(180, 21);
            this.btnDownSubprogram.Name = "btnDownSubprogram";
            this.btnDownSubprogram.Size = new System.Drawing.Size(45, 45);
            this.btnDownSubprogram.TabIndex = 27;
            this.btnDownSubprogram.UseVisualStyleBackColor = true;
            this.btnDownSubprogram.Click += new System.EventHandler(this.btnDownSubprogram_Click);
            // 
            // btnUpSubprogram
            // 
            this.btnUpSubprogram.Image = global::HPT1000.Properties.Resources.Previous_32x32;
            this.btnUpSubprogram.Location = new System.Drawing.Point(123, 21);
            this.btnUpSubprogram.Name = "btnUpSubprogram";
            this.btnUpSubprogram.Size = new System.Drawing.Size(45, 45);
            this.btnUpSubprogram.TabIndex = 49;
            this.btnUpSubprogram.UseVisualStyleBackColor = true;
            this.btnUpSubprogram.Click += new System.EventHandler(this.btnUpSubprogram_Click);
            // 
            // grBoxProgramToolBtn
            // 
            this.grBoxProgramToolBtn.Controls.Add(this.btnSave);
            this.grBoxProgramToolBtn.Controls.Add(this.btnAddNewProgram);
            this.grBoxProgramToolBtn.Controls.Add(this.btnRemoveProgram);
            this.grBoxProgramToolBtn.Location = new System.Drawing.Point(6, 591);
            this.grBoxProgramToolBtn.Name = "grBoxProgramToolBtn";
            this.grBoxProgramToolBtn.Size = new System.Drawing.Size(232, 70);
            this.grBoxProgramToolBtn.TabIndex = 50;
            this.grBoxProgramToolBtn.TabStop = false;
            this.grBoxProgramToolBtn.Text = "Program";
            // 
            // btnAddNewProgram
            // 
            this.btnAddNewProgram.Image = global::HPT1000.Properties.Resources.Add_32x32;
            this.btnAddNewProgram.Location = new System.Drawing.Point(9, 21);
            this.btnAddNewProgram.Name = "btnAddNewProgram";
            this.btnAddNewProgram.Size = new System.Drawing.Size(45, 45);
            this.btnAddNewProgram.TabIndex = 34;
            this.btnAddNewProgram.UseVisualStyleBackColor = true;
            this.btnAddNewProgram.Click += new System.EventHandler(this.btnAddNewProgram_Click);
            // 
            // label69
            // 
            this.label69.BackColor = System.Drawing.Color.Gray;
            this.label69.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label69.ForeColor = System.Drawing.Color.White;
            this.label69.Location = new System.Drawing.Point(5, 10);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(462, 25);
            this.label69.TabIndex = 54;
            this.label69.Text = "PROGRAM INFORMATION";
            this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // ProgramsConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label127);
            this.Controls.Add(this.tabControlProcess);
            this.Controls.Add(this.grBoxPrograms);
            this.Controls.Add(this.label69);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ProgramsConfigPanel";
            this.Size = new System.Drawing.Size(1270, 794);
            this.VisibleChanged += new System.EventHandler(this.ProgramsConfigPanel_VisibleChanged);
            this.grBoxGasPressure.ResumeLayout(false);
            this.grBoxGasPressure.PerformLayout();
            this.grBoxGasesMFC3.ResumeLayout(false);
            this.grBoxGasesMFC3.PerformLayout();
            this.grBoxGasesMFC2.ResumeLayout(false);
            this.grBoxGasesMFC2.PerformLayout();
            this.grBoxGasesMFC1.ResumeLayout(false);
            this.grBoxGasesMFC1.PerformLayout();
            this.tabPageVent.ResumeLayout(false);
            this.tabPageVent.PerformLayout();
            this.grBoxVent.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.tabPagePurge.ResumeLayout(false);
            this.grBoxPurge.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.tabPagePlasma.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.grBoxPlasma.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.tabPagePump.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.grBoxPump.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.grBoxProgram.ResumeLayout(false);
            this.grBoxProgram.PerformLayout();
            this.tabControlProcess.ResumeLayout(false);
            this.tabPageGas.ResumeLayout(false);
            this.tabPageGas.PerformLayout();
            this.grBoxGas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grBoxSelectGasLine.ResumeLayout(false);
            this.grBoxSelectGasLine.PerformLayout();
            this.grBoxGasFlow.ResumeLayout(false);
            this.grBoxMFC1.ResumeLayout(false);
            this.grBoxMFC1.PerformLayout();
            this.grBoxMFC3.ResumeLayout(false);
            this.grBoxMFC3.PerformLayout();
            this.grBoxMFC2.ResumeLayout(false);
            this.grBoxMFC2.PerformLayout();
            this.grBoxVaporiser.ResumeLayout(false);
            this.grBoxVaporiser.PerformLayout();
            this.tabPageMotor.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.grBoxSelectMotor.ResumeLayout(false);
            this.grBoxSelectMotor.PerformLayout();
            this.grBoxMotor2.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.grBoxMotor1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grBoxSubprogram.ResumeLayout(false);
            this.grBoxSubprogram.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.grBoxPrograms.ResumeLayout(false);
            this.grBoxSubprogramToolBtn.ResumeLayout(false);
            this.grBoxProgramToolBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grBoxGasPressure;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.HScrollBar scrollGasPressureDevaDown;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.HScrollBar scrollGasPressureDevaUp;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.HScrollBar scrollGasPressure;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.DateTimePicker timeVent;
        private System.Windows.Forms.Label label150;
        private System.Windows.Forms.Label label151;
        private System.Windows.Forms.Label label152;
        private System.Windows.Forms.TabPage tabPageVent;
        private System.Windows.Forms.Label label147;
        private System.Windows.Forms.Label label148;
        private System.Windows.Forms.DateTimePicker timePurge;
        private System.Windows.Forms.Label label149;
        private System.Windows.Forms.TabPage tabPagePurge;
        private System.Windows.Forms.Label labUnit2SetpointPlasma;
        private System.Windows.Forms.Label labUnit1SetpointPlasma;
        private System.Windows.Forms.HScrollBar scrollPlasmaDevistion;
        private System.Windows.Forms.HScrollBar scrollPlasmaSetpoint;
        private System.Windows.Forms.Label label144;
        private System.Windows.Forms.Label label143;
        private System.Windows.Forms.Label label142;
        private System.Windows.Forms.Label label139;
        private System.Windows.Forms.Label label140;
        private System.Windows.Forms.DateTimePicker timePlasma;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label141;
        private System.Windows.Forms.TabPage tabPagePlasma;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.DateTimePicker timeGas;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.TabPage tabPagePump;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.DateTimePicker timePump;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Button btnAddNewProgram;
        private System.Windows.Forms.GroupBox grBoxProgram;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.TextBox tBoxNameProgram;
        private System.Windows.Forms.TextBox tBoxDescProgram;
        private System.Windows.Forms.CheckBox cBoxMFC3;
        private System.Windows.Forms.ComboBox cBoxGasListMFC1;
        private System.Windows.Forms.CheckBox cBoxMFC2;
        private System.Windows.Forms.Button btnRemoveSubprogram;
        private System.Windows.Forms.TabControl tabControlProcess;
        private System.Windows.Forms.TabPage tabPageGas;
        private System.Windows.Forms.GroupBox grBoxVaporiser;
        private System.Windows.Forms.HScrollBar scrollGasVaporOnTime;
        private System.Windows.Forms.Label labUnitOnTime;
        private System.Windows.Forms.HScrollBar scrollGasVaporCycleTime;
        private System.Windows.Forms.Label labUnitCycle;
        private System.Windows.Forms.Label labOnTIme;
        private System.Windows.Forms.Label labCycle;
        private System.Windows.Forms.CheckBox cBoxMFC1;
        private System.Windows.Forms.ComboBox cBoxGasListMFC2;
        private System.Windows.Forms.GroupBox grBoxSubprogram;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.TextBox tBoxDescSubprgoram;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox tBoxNameSubprogram;
        private System.Windows.Forms.Label label153;
        private System.Windows.Forms.CheckBox cBoxPump;
        private System.Windows.Forms.CheckBox cBoxGas;
        private System.Windows.Forms.CheckBox cBoxPower;
        private System.Windows.Forms.CheckBox cBoxVent;
        private System.Windows.Forms.CheckBox cBoxPurge;
        private System.Windows.Forms.TreeView treeViewProgram;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnRemoveProgram;
        private System.Windows.Forms.GroupBox grBoxPrograms;
        private System.Windows.Forms.Button btnAddNewSubprogram;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grBoxGasFlow;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rBtnModePressure;
        private System.Windows.Forms.RadioButton rBtnModeFlow;
        private System.Windows.Forms.GroupBox grBoxGasesMFC1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar scrollGasDevaShareMFC1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grBoxMFC1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.HScrollBar scrollFlow1Min;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.HScrollBar scrollFlow1Max;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.HScrollBar scrollFlow1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grBoxSelectGasLine;
        private System.Windows.Forms.CheckBox cBoxVaporiser;
        private System.Windows.Forms.ComboBox cBoxGasListMFC3;
        private System.Windows.Forms.GroupBox grBoxMFC3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.HScrollBar scrollFlow3Min;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.HScrollBar scrollFlow3Max;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.HScrollBar scrollFlow3;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox grBoxMFC2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.HScrollBar scrollFlow2Min;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.HScrollBar scrollFlow2Max;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.HScrollBar scrollFlow2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox grBoxGasesMFC3;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.HScrollBar scrollGasDevaShareMFC3;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.GroupBox grBoxGasesMFC2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.HScrollBar scrollGasDevaShareMFC2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox grBoxPump;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.GroupBox grBoxGas;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.HScrollBar scrollPumpSetpoint;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.GroupBox grBoxVent;
        private System.Windows.Forms.GroupBox grBoxPurge;
        private System.Windows.Forms.GroupBox grBoxPlasma;
        private System.Windows.Forms.RadioButton rBtnPressureViaVapo;
        private System.Windows.Forms.RadioButton rBtnPressureViaGases;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItem_AddProgram;
        private System.Windows.Forms.ToolStripMenuItem menuItem_AddSubprogram;
        private System.Windows.Forms.ToolStripMenuItem menuItem_RemoveProgram;
        private System.Windows.Forms.ToolStripMenuItem menuItem_RemoveSubprogram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.Button btnSave;
        private Cotrols.DoubleEdit dEditDosing;
        private System.Windows.Forms.HScrollBar scrolBarDosing;
        private System.Windows.Forms.Label labUnitDosing;
        private System.Windows.Forms.Label labDosing;
        private System.Windows.Forms.TextBox tBoxSetpointPlasma;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.CheckBox cBoxMotor;
        private System.Windows.Forms.TabPage tabPageMotor;
        private System.Windows.Forms.GroupBox grBoxMotor2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.DateTimePicker dateTimeMotor2;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.CheckBox cBoxStateMotor2;
        private System.Windows.Forms.GroupBox grBoxMotor1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dateTimeMotor1;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.CheckBox cBoxStateMotor1;
        private System.Windows.Forms.Label labTitelMotor;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox cBoxActiveMotor2;
        private System.Windows.Forms.CheckBox cBoxActiveMotor1;
        private System.Windows.Forms.GroupBox grBoxSelectMotor;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rBtnGasNone;
        private System.Windows.Forms.Button btnDownSubprogram;
        private System.Windows.Forms.Button btnUpSubprogram;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox grBoxSubprogramToolBtn;
        private System.Windows.Forms.GroupBox grBoxProgramToolBtn;
        private Cotrols.DoubleEdit dEditPumpSetpoint;
        private Cotrols.DoubleEdit dEditPlasmaSetpoint;
        private Cotrols.DoubleEdit dEditPlasmaDeviation;
        private Cotrols.DoubleEdit dEditFlow1Max;
        private Cotrols.DoubleEdit dEditFlow1Min;
        private Cotrols.DoubleEdit dEditFlow1;
        private Cotrols.DoubleEdit dEditFlow3Max;
        private Cotrols.DoubleEdit dEditFlow3Min;
        private Cotrols.DoubleEdit dEditFlow3;
        private Cotrols.DoubleEdit dEditFlow2Max;
        private Cotrols.DoubleEdit dEditFlow2Min;
        private Cotrols.DoubleEdit dEditFlow2;
        private Cotrols.DoubleEdit dEditGasPressureDevaUp;
        private Cotrols.DoubleEdit dEditGasPressureDevaDown;
        private Cotrols.DoubleEdit dEditGasPressure;
        private Cotrols.DoubleEdit dEditGasDevaShareMFC3;
        private Cotrols.DoubleEdit dEditGasShareMFC3;
        private Cotrols.DoubleEdit dEditGasDevaShareMFC2;
        private Cotrols.DoubleEdit dEditGasShareMFC2;
        private Cotrols.DoubleEdit dEditGasShareMFC1;
        private Cotrols.DoubleEdit dEditGasDevaShareMFC1;
        private Cotrols.DoubleEdit dEditGasVaporOnTime;
        private Cotrols.DoubleEdit dEditGasVaporCycleTime;
    }
}
