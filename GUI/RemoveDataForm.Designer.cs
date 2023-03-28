namespace HPT1000.GUI
{
    partial class RemoveDataForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("yutuyi");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("hytjyuk");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("op[[");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("poi[po[");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("iuoip");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTimeEndUp = new System.Windows.Forms.Button();
            this.btnTimeEndDown = new System.Windows.Forms.Button();
            this.btnTimeStartUp = new System.Windows.Forms.Button();
            this.btnTimeStartDown = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cBoxProcesName = new System.Windows.Forms.ComboBox();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.grBoxListArchiveProces = new System.Windows.Forms.GroupBox();
            this.listViewData = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grBoxListArchiveProces.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTimeEndUp);
            this.groupBox1.Controls.Add(this.btnTimeEndDown);
            this.groupBox1.Controls.Add(this.btnTimeStartUp);
            this.groupBox1.Controls.Add(this.btnTimeStartDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cBoxProcesName);
            this.groupBox1.Controls.Add(this.dateTimeEnd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimeStart);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(654, 91);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnTimeEndUp
            // 
            this.btnTimeEndUp.Location = new System.Drawing.Point(564, 63);
            this.btnTimeEndUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimeEndUp.Name = "btnTimeEndUp";
            this.btnTimeEndUp.Size = new System.Drawing.Size(87, 22);
            this.btnTimeEndUp.TabIndex = 19;
            this.btnTimeEndUp.Text = ">>";
            this.btnTimeEndUp.UseVisualStyleBackColor = true;
            this.btnTimeEndUp.Click += new System.EventHandler(this.btnTimeEndUp_Click);
            // 
            // btnTimeEndDown
            // 
            this.btnTimeEndDown.Location = new System.Drawing.Point(474, 63);
            this.btnTimeEndDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimeEndDown.Name = "btnTimeEndDown";
            this.btnTimeEndDown.Size = new System.Drawing.Size(87, 22);
            this.btnTimeEndDown.TabIndex = 18;
            this.btnTimeEndDown.Text = "<<";
            this.btnTimeEndDown.UseVisualStyleBackColor = true;
            this.btnTimeEndDown.Click += new System.EventHandler(this.btnTimeEndDown_Click);
            // 
            // btnTimeStartUp
            // 
            this.btnTimeStartUp.Location = new System.Drawing.Point(331, 63);
            this.btnTimeStartUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimeStartUp.Name = "btnTimeStartUp";
            this.btnTimeStartUp.Size = new System.Drawing.Size(87, 22);
            this.btnTimeStartUp.TabIndex = 17;
            this.btnTimeStartUp.Text = ">>";
            this.btnTimeStartUp.UseVisualStyleBackColor = true;
            this.btnTimeStartUp.Click += new System.EventHandler(this.btnTimeStartUp_Click);
            // 
            // btnTimeStartDown
            // 
            this.btnTimeStartDown.Location = new System.Drawing.Point(241, 63);
            this.btnTimeStartDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimeStartDown.Name = "btnTimeStartDown";
            this.btnTimeStartDown.Size = new System.Drawing.Size(87, 22);
            this.btnTimeStartDown.TabIndex = 16;
            this.btnTimeStartDown.Text = "<<";
            this.btnTimeStartDown.UseVisualStyleBackColor = true;
            this.btnTimeStartDown.Click += new System.EventHandler(this.btnTimeStartDown_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Proces name";
            // 
            // cBoxProcesName
            // 
            this.cBoxProcesName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxProcesName.FormattingEnabled = true;
            this.cBoxProcesName.Location = new System.Drawing.Point(15, 37);
            this.cBoxProcesName.Margin = new System.Windows.Forms.Padding(2);
            this.cBoxProcesName.Name = "cBoxProcesName";
            this.cBoxProcesName.Size = new System.Drawing.Size(177, 25);
            this.cBoxProcesName.TabIndex = 4;
            this.cBoxProcesName.SelectedIndexChanged += new System.EventHandler(this.cBoxProcesName_SelectedIndexChanged);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeEnd.Location = new System.Drawing.Point(474, 38);
            this.dateTimeEnd.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(177, 23);
            this.dateTimeEnd.TabIndex = 11;
            this.dateTimeEnd.ValueChanged += new System.EventHandler(this.dateProces_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(238, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Start date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "End date";
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeStart.Location = new System.Drawing.Point(241, 37);
            this.dateTimeStart.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(177, 23);
            this.dateTimeStart.TabIndex = 8;
            this.dateTimeStart.CloseUp += new System.EventHandler(this.dateTimePicker_CloseUp);
            this.dateTimeStart.ValueChanged += new System.EventHandler(this.dateProces_ValueChanged);
            // 
            // grBoxListArchiveProces
            // 
            this.grBoxListArchiveProces.Controls.Add(this.listViewData);
            this.grBoxListArchiveProces.Location = new System.Drawing.Point(2, 93);
            this.grBoxListArchiveProces.Margin = new System.Windows.Forms.Padding(2);
            this.grBoxListArchiveProces.Name = "grBoxListArchiveProces";
            this.grBoxListArchiveProces.Padding = new System.Windows.Forms.Padding(2);
            this.grBoxListArchiveProces.Size = new System.Drawing.Size(654, 297);
            this.grBoxListArchiveProces.TabIndex = 19;
            this.grBoxListArchiveProces.TabStop = false;
            this.grBoxListArchiveProces.Text = "List archived processes: 21.06.203 - 23.09.2017";
            // 
            // listViewData
            // 
            this.listViewData.CheckBoxes = true;
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "ListViewGroup";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup";
            listViewGroup2.Name = "listViewGroup2";
            this.listViewData.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.StateImageIndex = 0;
            listViewItem9.StateImageIndex = 0;
            listViewItem10.StateImageIndex = 0;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.StateImageIndex = 0;
            this.listViewData.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.listViewData.Location = new System.Drawing.Point(2, 18);
            this.listViewData.Margin = new System.Windows.Forms.Padding(2);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(650, 277);
            this.listViewData.TabIndex = 3;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.List;
            this.listViewData.SelectedIndexChanged += new System.EventHandler(this.listViewData_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(458, 393);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 31);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(558, 393);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 31);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Remove";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // RemoveDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(659, 427);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grBoxListArchiveProces);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RemoveDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemoveDataForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grBoxListArchiveProces.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTimeEndUp;
        private System.Windows.Forms.Button btnTimeEndDown;
        private System.Windows.Forms.Button btnTimeStartUp;
        private System.Windows.Forms.Button btnTimeStartDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBoxProcesName;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.GroupBox grBoxListArchiveProces;
        private System.Windows.Forms.ListView listViewData;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}