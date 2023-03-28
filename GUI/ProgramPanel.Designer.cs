namespace HPT1000.GUI
{
    partial class ProgramPanel
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
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Pump",
            "Working"}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Gas",
            "Wait"}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Plasma");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Purge");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("Vent");
            this.label4 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.listViewSubprograms = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cBoxPrograms = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panelMotor = new System.Windows.Forms.Panel();
            this.labMotorsState = new System.Windows.Forms.Label();
            this.labMotorTimeTarget = new System.Windows.Forms.Label();
            this.labMotorTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelGas = new System.Windows.Forms.Panel();
            this.labGasMFC3 = new System.Windows.Forms.Label();
            this.labGasMFC2 = new System.Windows.Forms.Label();
            this.labGasMFC1 = new System.Windows.Forms.Label();
            this.labGasVaporiser = new System.Windows.Forms.Label();
            this.labGasSetpointPressure = new System.Windows.Forms.Label();
            this.labGasTimeTarget = new System.Windows.Forms.Label();
            this.labGasTime = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.panelPump = new System.Windows.Forms.Panel();
            this.labPumpTimeTarget = new System.Windows.Forms.Label();
            this.labPumpTime = new System.Windows.Forms.Label();
            this.labPumpSetpoint = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelPlasma = new System.Windows.Forms.Panel();
            this.labPlasmaSetpoint = new System.Windows.Forms.Label();
            this.labPlasmaTimeTarget = new System.Windows.Forms.Label();
            this.labPlasmaTime = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panelVent = new System.Windows.Forms.Panel();
            this.labVentTimeTarget = new System.Windows.Forms.Label();
            this.labVentTime = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panelPurge = new System.Windows.Forms.Panel();
            this.labPurgeTimeTarget = new System.Windows.Forms.Label();
            this.labPurgeTime = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.labProgram = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labStatus1 = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.rBtnManual = new System.Windows.Forms.RadioButton();
            this.rBtnAutomatic = new System.Windows.Forms.RadioButton();
            this.rBtnHide = new System.Windows.Forms.RadioButton();
            this.panel8.SuspendLayout();
            this.panelMotor.SuspendLayout();
            this.panelGas.SuspendLayout();
            this.panelPump.SuspendLayout();
            this.panelPlasma.SuspendLayout();
            this.panelVent.SuspendLayout();
            this.panelPurge.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gray;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(430, 20);
            this.label4.TabIndex = 48;
            this.label4.Text = "OPERATING MODE";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.Gray;
            this.label34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label34.ForeColor = System.Drawing.Color.White;
            this.label34.Location = new System.Drawing.Point(0, 0);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(428, 20);
            this.label34.TabIndex = 55;
            this.label34.Text = "AUTOMATIC";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewSubprograms
            // 
            this.listViewSubprograms.BackColor = System.Drawing.SystemColors.Window;
            this.listViewSubprograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewSubprograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listViewSubprograms.ForeColor = System.Drawing.Color.Black;
            this.listViewSubprograms.FullRowSelect = true;
            this.listViewSubprograms.GridLines = true;
            this.listViewSubprograms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSubprograms.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10});
            this.listViewSubprograms.Location = new System.Drawing.Point(6, 53);
            this.listViewSubprograms.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewSubprograms.MultiSelect = false;
            this.listViewSubprograms.Name = "listViewSubprograms";
            this.listViewSubprograms.Size = new System.Drawing.Size(297, 172);
            this.listViewSubprograms.TabIndex = 54;
            this.listViewSubprograms.UseCompatibleStateImageBehavior = false;
            this.listViewSubprograms.View = System.Windows.Forms.View.Details;
            this.listViewSubprograms.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listViewSubprograms_DrawColumnHeader);
            this.listViewSubprograms.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listViewSubprograms_DrawItem);
            this.listViewSubprograms.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listViewSubprograms_DrawSubItem);
            this.listViewSubprograms.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewSubprograms_ItemSelectionChanged);
            this.listViewSubprograms.SelectedIndexChanged += new System.EventHandler(this.listViewSubprograms_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Subprogram";
            this.columnHeader1.Width = 215;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Status";
            this.columnHeader2.Width = 100;
            // 
            // cBoxPrograms
            // 
            this.cBoxPrograms.BackColor = System.Drawing.SystemColors.Window;
            this.cBoxPrograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cBoxPrograms.ForeColor = System.Drawing.Color.Black;
            this.cBoxPrograms.FormattingEnabled = true;
            this.cBoxPrograms.Location = new System.Drawing.Point(78, 24);
            this.cBoxPrograms.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cBoxPrograms.Name = "cBoxPrograms";
            this.cBoxPrograms.Size = new System.Drawing.Size(147, 24);
            this.cBoxPrograms.TabIndex = 47;
            this.cBoxPrograms.SelectedIndexChanged += new System.EventHandler(this.cBoxPrograms_SelectedIndexChanged);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStop.ForeColor = System.Drawing.Color.Black;
            this.btnStop.Location = new System.Drawing.Point(306, 160);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(64, 64);
            this.btnStop.TabIndex = 53;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.Paint += new System.Windows.Forms.PaintEventHandler(this.btnStop_Paint);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(306, 78);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(64, 64);
            this.btnStart.TabIndex = 52;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.Paint += new System.Windows.Forms.PaintEventHandler(this.btnStart_Paint);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panelMotor);
            this.panel8.Controls.Add(this.panelGas);
            this.panel8.Controls.Add(this.panelPump);
            this.panel8.Controls.Add(this.panelPlasma);
            this.panel8.Controls.Add(this.panelVent);
            this.panel8.Controls.Add(this.label12);
            this.panel8.Controls.Add(this.panelPurge);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Controls.Add(this.label35);
            this.panel8.Location = new System.Drawing.Point(2, 228);
            this.panel8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(414, 241);
            this.panel8.TabIndex = 57;
            // 
            // panelMotor
            // 
            this.panelMotor.Controls.Add(this.labMotorsState);
            this.panelMotor.Controls.Add(this.labMotorTimeTarget);
            this.panelMotor.Controls.Add(this.labMotorTime);
            this.panelMotor.Controls.Add(this.label3);
            this.panelMotor.Enabled = false;
            this.panelMotor.Location = new System.Drawing.Point(3, 137);
            this.panelMotor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelMotor.Name = "panelMotor";
            this.panelMotor.Size = new System.Drawing.Size(422, 26);
            this.panelMotor.TabIndex = 62;
            // 
            // labMotorsState
            // 
            this.labMotorsState.AutoSize = true;
            this.labMotorsState.ForeColor = System.Drawing.Color.Black;
            this.labMotorsState.Location = new System.Drawing.Point(205, 6);
            this.labMotorsState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labMotorsState.Name = "labMotorsState";
            this.labMotorsState.Size = new System.Drawing.Size(134, 15);
            this.labMotorsState.TabIndex = 6;
            this.labMotorsState.Text = "Motors: 1 - OFF 2 - OFF";
            // 
            // labMotorTimeTarget
            // 
            this.labMotorTimeTarget.AutoSize = true;
            this.labMotorTimeTarget.ForeColor = System.Drawing.Color.Black;
            this.labMotorTimeTarget.Location = new System.Drawing.Point(135, 6);
            this.labMotorTimeTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labMotorTimeTarget.Name = "labMotorTimeTarget";
            this.labMotorTimeTarget.Size = new System.Drawing.Size(55, 15);
            this.labMotorTimeTarget.TabIndex = 3;
            this.labMotorTimeTarget.Text = "00:30:00";
            // 
            // labMotorTime
            // 
            this.labMotorTime.AutoSize = true;
            this.labMotorTime.ForeColor = System.Drawing.Color.Black;
            this.labMotorTime.Location = new System.Drawing.Point(63, 6);
            this.labMotorTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labMotorTime.Name = "labMotorTime";
            this.labMotorTime.Size = new System.Drawing.Size(55, 15);
            this.labMotorTime.TabIndex = 2;
            this.labMotorTime.Text = "00:01:35";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(1, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Motor:";
            // 
            // panelGas
            // 
            this.panelGas.Controls.Add(this.labGasMFC3);
            this.panelGas.Controls.Add(this.labGasMFC2);
            this.panelGas.Controls.Add(this.labGasMFC1);
            this.panelGas.Controls.Add(this.labGasVaporiser);
            this.panelGas.Controls.Add(this.labGasSetpointPressure);
            this.panelGas.Controls.Add(this.labGasTimeTarget);
            this.panelGas.Controls.Add(this.labGasTime);
            this.panelGas.Controls.Add(this.label26);
            this.panelGas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panelGas.Location = new System.Drawing.Point(3, 161);
            this.panelGas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelGas.Name = "panelGas";
            this.panelGas.Size = new System.Drawing.Size(368, 79);
            this.panelGas.TabIndex = 35;
            // 
            // labGasMFC3
            // 
            this.labGasMFC3.AutoSize = true;
            this.labGasMFC3.ForeColor = System.Drawing.Color.Black;
            this.labGasMFC3.Location = new System.Drawing.Point(248, 29);
            this.labGasMFC3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasMFC3.Name = "labGasMFC3";
            this.labGasMFC3.Size = new System.Drawing.Size(124, 17);
            this.labGasMFC3.TabIndex = 12;
            this.labGasMFC3.Text = "MFC 3: 1225 sccm";
            // 
            // labGasMFC2
            // 
            this.labGasMFC2.AutoSize = true;
            this.labGasMFC2.ForeColor = System.Drawing.Color.Black;
            this.labGasMFC2.Location = new System.Drawing.Point(125, 29);
            this.labGasMFC2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasMFC2.Name = "labGasMFC2";
            this.labGasMFC2.Size = new System.Drawing.Size(128, 17);
            this.labGasMFC2.TabIndex = 11;
            this.labGasMFC2.Text = "MFC 2:  1000 sccm";
            // 
            // labGasMFC1
            // 
            this.labGasMFC1.AutoSize = true;
            this.labGasMFC1.ForeColor = System.Drawing.Color.Black;
            this.labGasMFC1.Location = new System.Drawing.Point(2, 29);
            this.labGasMFC1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasMFC1.Name = "labGasMFC1";
            this.labGasMFC1.Size = new System.Drawing.Size(128, 17);
            this.labGasMFC1.TabIndex = 10;
            this.labGasMFC1.Text = "MFC 1 : 1000 sccm";
            // 
            // labGasVaporiser
            // 
            this.labGasVaporiser.AutoSize = true;
            this.labGasVaporiser.ForeColor = System.Drawing.Color.Black;
            this.labGasVaporiser.Location = new System.Drawing.Point(2, 52);
            this.labGasVaporiser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasVaporiser.Name = "labGasVaporiser";
            this.labGasVaporiser.Size = new System.Drawing.Size(283, 17);
            this.labGasVaporiser.TabIndex = 9;
            this.labGasVaporiser.Text = "Vaporiser: Cycle - 1000 [ms] Open - 25 [%]";
            // 
            // labGasSetpointPressure
            // 
            this.labGasSetpointPressure.AutoSize = true;
            this.labGasSetpointPressure.ForeColor = System.Drawing.Color.Black;
            this.labGasSetpointPressure.Location = new System.Drawing.Point(202, 6);
            this.labGasSetpointPressure.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasSetpointPressure.Name = "labGasSetpointPressure";
            this.labGasSetpointPressure.Size = new System.Drawing.Size(141, 17);
            this.labGasSetpointPressure.TabIndex = 5;
            this.labGasSetpointPressure.Text = "Setpoint: 0.01 [mBar]";
            // 
            // labGasTimeTarget
            // 
            this.labGasTimeTarget.AutoSize = true;
            this.labGasTimeTarget.ForeColor = System.Drawing.Color.Black;
            this.labGasTimeTarget.Location = new System.Drawing.Point(135, 6);
            this.labGasTimeTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasTimeTarget.Name = "labGasTimeTarget";
            this.labGasTimeTarget.Size = new System.Drawing.Size(64, 17);
            this.labGasTimeTarget.TabIndex = 3;
            this.labGasTimeTarget.Text = "00:30:00";
            // 
            // labGasTime
            // 
            this.labGasTime.AutoSize = true;
            this.labGasTime.ForeColor = System.Drawing.Color.Black;
            this.labGasTime.Location = new System.Drawing.Point(62, 6);
            this.labGasTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labGasTime.Name = "labGasTime";
            this.labGasTime.Size = new System.Drawing.Size(64, 17);
            this.labGasTime.TabIndex = 2;
            this.labGasTime.Text = "00:01:35";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label26.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label26.Location = new System.Drawing.Point(1, 6);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(37, 17);
            this.label26.TabIndex = 0;
            this.label26.Text = "Gas";
            // 
            // panelPump
            // 
            this.panelPump.Controls.Add(this.labPumpTimeTarget);
            this.panelPump.Controls.Add(this.labPumpTime);
            this.panelPump.Controls.Add(this.labPumpSetpoint);
            this.panelPump.Controls.Add(this.label6);
            this.panelPump.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panelPump.Location = new System.Drawing.Point(2, 41);
            this.panelPump.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPump.Name = "panelPump";
            this.panelPump.Size = new System.Drawing.Size(422, 24);
            this.panelPump.TabIndex = 29;
            // 
            // labPumpTimeTarget
            // 
            this.labPumpTimeTarget.AutoSize = true;
            this.labPumpTimeTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labPumpTimeTarget.ForeColor = System.Drawing.Color.Black;
            this.labPumpTimeTarget.Location = new System.Drawing.Point(135, 6);
            this.labPumpTimeTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPumpTimeTarget.Name = "labPumpTimeTarget";
            this.labPumpTimeTarget.Size = new System.Drawing.Size(64, 17);
            this.labPumpTimeTarget.TabIndex = 3;
            this.labPumpTimeTarget.Text = "00:30:00";
            // 
            // labPumpTime
            // 
            this.labPumpTime.AutoSize = true;
            this.labPumpTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labPumpTime.ForeColor = System.Drawing.Color.Black;
            this.labPumpTime.Location = new System.Drawing.Point(64, 6);
            this.labPumpTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPumpTime.Name = "labPumpTime";
            this.labPumpTime.Size = new System.Drawing.Size(64, 17);
            this.labPumpTime.TabIndex = 2;
            this.labPumpTime.Text = "00:01:35";
            // 
            // labPumpSetpoint
            // 
            this.labPumpSetpoint.AutoSize = true;
            this.labPumpSetpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labPumpSetpoint.ForeColor = System.Drawing.Color.Black;
            this.labPumpSetpoint.Location = new System.Drawing.Point(206, 6);
            this.labPumpSetpoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPumpSetpoint.Name = "labPumpSetpoint";
            this.labPumpSetpoint.Size = new System.Drawing.Size(133, 17);
            this.labPumpSetpoint.TabIndex = 1;
            this.labPumpSetpoint.Text = "Setpoint: 0.5 [mBar]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(1, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Pump:";
            // 
            // panelPlasma
            // 
            this.panelPlasma.Controls.Add(this.labPlasmaSetpoint);
            this.panelPlasma.Controls.Add(this.labPlasmaTimeTarget);
            this.panelPlasma.Controls.Add(this.labPlasmaTime);
            this.panelPlasma.Controls.Add(this.label21);
            this.panelPlasma.Location = new System.Drawing.Point(2, 113);
            this.panelPlasma.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPlasma.Name = "panelPlasma";
            this.panelPlasma.Size = new System.Drawing.Size(422, 24);
            this.panelPlasma.TabIndex = 34;
            // 
            // labPlasmaSetpoint
            // 
            this.labPlasmaSetpoint.AutoSize = true;
            this.labPlasmaSetpoint.ForeColor = System.Drawing.Color.Black;
            this.labPlasmaSetpoint.Location = new System.Drawing.Point(206, 6);
            this.labPlasmaSetpoint.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPlasmaSetpoint.Name = "labPlasmaSetpoint";
            this.labPlasmaSetpoint.Size = new System.Drawing.Size(91, 15);
            this.labPlasmaSetpoint.TabIndex = 4;
            this.labPlasmaSetpoint.Text = "Setpoint: 3.5 [A]";
            // 
            // labPlasmaTimeTarget
            // 
            this.labPlasmaTimeTarget.AutoSize = true;
            this.labPlasmaTimeTarget.ForeColor = System.Drawing.Color.Black;
            this.labPlasmaTimeTarget.Location = new System.Drawing.Point(135, 6);
            this.labPlasmaTimeTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPlasmaTimeTarget.Name = "labPlasmaTimeTarget";
            this.labPlasmaTimeTarget.Size = new System.Drawing.Size(55, 15);
            this.labPlasmaTimeTarget.TabIndex = 3;
            this.labPlasmaTimeTarget.Text = "00:30:00";
            // 
            // labPlasmaTime
            // 
            this.labPlasmaTime.AutoSize = true;
            this.labPlasmaTime.ForeColor = System.Drawing.Color.Black;
            this.labPlasmaTime.Location = new System.Drawing.Point(64, 6);
            this.labPlasmaTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPlasmaTime.Name = "labPlasmaTime";
            this.labPlasmaTime.Size = new System.Drawing.Size(55, 15);
            this.labPlasmaTime.TabIndex = 2;
            this.labPlasmaTime.Text = "00:01:35";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label21.Location = new System.Drawing.Point(1, 6);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 17);
            this.label21.TabIndex = 0;
            this.label21.Text = "Plasma:";
            // 
            // panelVent
            // 
            this.panelVent.Controls.Add(this.labVentTimeTarget);
            this.panelVent.Controls.Add(this.labVentTime);
            this.panelVent.Controls.Add(this.label16);
            this.panelVent.Location = new System.Drawing.Point(2, 65);
            this.panelVent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelVent.Name = "panelVent";
            this.panelVent.Size = new System.Drawing.Size(422, 24);
            this.panelVent.TabIndex = 30;
            // 
            // labVentTimeTarget
            // 
            this.labVentTimeTarget.AutoSize = true;
            this.labVentTimeTarget.ForeColor = System.Drawing.Color.Black;
            this.labVentTimeTarget.Location = new System.Drawing.Point(135, 6);
            this.labVentTimeTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labVentTimeTarget.Name = "labVentTimeTarget";
            this.labVentTimeTarget.Size = new System.Drawing.Size(55, 15);
            this.labVentTimeTarget.TabIndex = 3;
            this.labVentTimeTarget.Text = "00:30:00";
            // 
            // labVentTime
            // 
            this.labVentTime.AutoSize = true;
            this.labVentTime.ForeColor = System.Drawing.Color.Black;
            this.labVentTime.Location = new System.Drawing.Point(64, 6);
            this.labVentTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labVentTime.Name = "labVentTime";
            this.labVentTime.Size = new System.Drawing.Size(55, 15);
            this.labVentTime.TabIndex = 2;
            this.labVentTime.Text = "00:01:35";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label16.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label16.Location = new System.Drawing.Point(1, 6);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "Vent:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(250, 23);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 17);
            this.label12.TabIndex = 33;
            this.label12.Text = "Parameters";
            // 
            // panelPurge
            // 
            this.panelPurge.Controls.Add(this.labPurgeTimeTarget);
            this.panelPurge.Controls.Add(this.labPurgeTime);
            this.panelPurge.Controls.Add(this.label18);
            this.panelPurge.Location = new System.Drawing.Point(2, 89);
            this.panelPurge.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPurge.Name = "panelPurge";
            this.panelPurge.Size = new System.Drawing.Size(422, 24);
            this.panelPurge.TabIndex = 31;
            // 
            // labPurgeTimeTarget
            // 
            this.labPurgeTimeTarget.AutoSize = true;
            this.labPurgeTimeTarget.ForeColor = System.Drawing.Color.Black;
            this.labPurgeTimeTarget.Location = new System.Drawing.Point(135, 6);
            this.labPurgeTimeTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPurgeTimeTarget.Name = "labPurgeTimeTarget";
            this.labPurgeTimeTarget.Size = new System.Drawing.Size(55, 15);
            this.labPurgeTimeTarget.TabIndex = 3;
            this.labPurgeTimeTarget.Text = "00:30:00";
            // 
            // labPurgeTime
            // 
            this.labPurgeTime.AutoSize = true;
            this.labPurgeTime.ForeColor = System.Drawing.Color.Black;
            this.labPurgeTime.Location = new System.Drawing.Point(64, 6);
            this.labPurgeTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labPurgeTime.Name = "labPurgeTime";
            this.labPurgeTime.Size = new System.Drawing.Size(55, 15);
            this.labPurgeTime.TabIndex = 2;
            this.labPurgeTime.Text = "00:01:35";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label18.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label18.Location = new System.Drawing.Point(1, 6);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(56, 17);
            this.label18.TabIndex = 0;
            this.label18.Text = "Purge:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(70, 23);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Stage";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(139, 23);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "Target";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Gray;
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(-2, 0);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(428, 20);
            this.label35.TabIndex = 56;
            this.label35.Text = "PROCESS STAGE";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labProgram
            // 
            this.labProgram.AutoSize = true;
            this.labProgram.BackColor = System.Drawing.Color.Transparent;
            this.labProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labProgram.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labProgram.Location = new System.Drawing.Point(4, 26);
            this.labProgram.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labProgram.Name = "labProgram";
            this.labProgram.Size = new System.Drawing.Size(74, 17);
            this.labProgram.TabIndex = 39;
            this.labProgram.Text = "Program:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.labStatus1);
            this.panel6.Controls.Add(this.labStatus);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.label34);
            this.panel6.Controls.Add(this.labProgram);
            this.panel6.Controls.Add(this.listViewSubprograms);
            this.panel6.Controls.Add(this.btnStart);
            this.panel6.Controls.Add(this.btnStop);
            this.panel6.Controls.Add(this.cBoxPrograms);
            this.panel6.Location = new System.Drawing.Point(0, 44);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(374, 463);
            this.panel6.TabIndex = 58;
            // 
            // labStatus1
            // 
            this.labStatus1.AutoSize = true;
            this.labStatus1.BackColor = System.Drawing.Color.Transparent;
            this.labStatus1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labStatus1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labStatus1.Location = new System.Drawing.Point(232, 26);
            this.labStatus1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labStatus1.Name = "labStatus1";
            this.labStatus1.Size = new System.Drawing.Size(59, 17);
            this.labStatus1.TabIndex = 59;
            this.labStatus1.Text = "Status:";
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labStatus.ForeColor = System.Drawing.Color.Black;
            this.labStatus.Location = new System.Drawing.Point(292, 26);
            this.labStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(50, 17);
            this.labStatus.TabIndex = 58;
            this.labStatus.Text = "STOP";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // rBtnManual
            // 
            this.rBtnManual.AutoSize = true;
            this.rBtnManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rBtnManual.ForeColor = System.Drawing.Color.Black;
            this.rBtnManual.Location = new System.Drawing.Point(240, 23);
            this.rBtnManual.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rBtnManual.Name = "rBtnManual";
            this.rBtnManual.Size = new System.Drawing.Size(89, 21);
            this.rBtnManual.TabIndex = 59;
            this.rBtnManual.TabStop = true;
            this.rBtnManual.Text = "MANUAL";
            this.rBtnManual.UseVisualStyleBackColor = true;
            this.rBtnManual.Click += new System.EventHandler(this.rBtnMode_Click);
            // 
            // rBtnAutomatic
            // 
            this.rBtnAutomatic.AutoSize = true;
            this.rBtnAutomatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rBtnAutomatic.ForeColor = System.Drawing.Color.Black;
            this.rBtnAutomatic.Location = new System.Drawing.Point(39, 22);
            this.rBtnAutomatic.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rBtnAutomatic.Name = "rBtnAutomatic";
            this.rBtnAutomatic.Size = new System.Drawing.Size(115, 21);
            this.rBtnAutomatic.TabIndex = 60;
            this.rBtnAutomatic.TabStop = true;
            this.rBtnAutomatic.Text = "AUTOMATIC";
            this.rBtnAutomatic.UseVisualStyleBackColor = true;
            this.rBtnAutomatic.Click += new System.EventHandler(this.rBtnMode_Click);
            // 
            // rBtnHide
            // 
            this.rBtnHide.AutoSize = true;
            this.rBtnHide.Location = new System.Drawing.Point(330, 22);
            this.rBtnHide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rBtnHide.Name = "rBtnHide";
            this.rBtnHide.Size = new System.Drawing.Size(49, 19);
            this.rBtnHide.TabIndex = 61;
            this.rBtnHide.TabStop = true;
            this.rBtnHide.Text = "hide";
            this.rBtnHide.UseVisualStyleBackColor = true;
            this.rBtnHide.Visible = false;
            // 
            // ProgramPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.rBtnHide);
            this.Controls.Add(this.rBtnAutomatic);
            this.Controls.Add(this.rBtnManual);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ProgramPanel";
            this.Size = new System.Drawing.Size(374, 512);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panelMotor.ResumeLayout(false);
            this.panelMotor.PerformLayout();
            this.panelGas.ResumeLayout(false);
            this.panelGas.PerformLayout();
            this.panelPump.ResumeLayout(false);
            this.panelPump.PerformLayout();
            this.panelPlasma.ResumeLayout(false);
            this.panelPlasma.PerformLayout();
            this.panelVent.ResumeLayout(false);
            this.panelVent.PerformLayout();
            this.panelPurge.ResumeLayout(false);
            this.panelPurge.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ListView listViewSubprograms;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox cBoxPrograms;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panelGas;
        private System.Windows.Forms.Label labGasVaporiser;
        private System.Windows.Forms.Label labGasSetpointPressure;
        private System.Windows.Forms.Label labGasTimeTarget;
        private System.Windows.Forms.Label labGasTime;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panelPump;
        private System.Windows.Forms.Label labPumpTimeTarget;
        private System.Windows.Forms.Label labPumpTime;
        private System.Windows.Forms.Label labPumpSetpoint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelPlasma;
        private System.Windows.Forms.Label labPlasmaSetpoint;
        private System.Windows.Forms.Label labPlasmaTimeTarget;
        private System.Windows.Forms.Label labPlasmaTime;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panelVent;
        private System.Windows.Forms.Label labVentTimeTarget;
        private System.Windows.Forms.Label labVentTime;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panelPurge;
        private System.Windows.Forms.Label labPurgeTimeTarget;
        private System.Windows.Forms.Label labPurgeTime;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label labProgram;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.RadioButton rBtnManual;
        private System.Windows.Forms.RadioButton rBtnAutomatic;
        private System.Windows.Forms.RadioButton rBtnHide;
        private System.Windows.Forms.Panel panelMotor;
        private System.Windows.Forms.Label labMotorTimeTarget;
        private System.Windows.Forms.Label labMotorTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labGasMFC3;
        private System.Windows.Forms.Label labGasMFC2;
        private System.Windows.Forms.Label labGasMFC1;
        private System.Windows.Forms.Label labMotorsState;
        private System.Windows.Forms.Label labStatus1;
    }
}
