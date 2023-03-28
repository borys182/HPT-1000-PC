using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;
using HPT1000.Source.Program;
using HPT1000.Source;

namespace HPT1000.GUI
{
  

    public partial class ProgramsConfigPanel : UserControl
    {
        private HPT1000.Source.Driver.HPT1000 hpt1000 = null;

        private static Color backGradientStartColor = Color.FromArgb(50, 130, 50);
        private static Color backGradientEndColor = Color.FromArgb(150, 255, 100);

        private const double pressureResolution = 1000;    //zmienna okresla ile miejsc po przecinku mozna wprowadzac do zmiennych presure

        private bool        flagRefreshProgram  = false;
        private DB          dataBase            = null;
        private GasTypes    gasTypes            = null;
        private Program     lastSelectedProgram = null; // Wskaznik na obiekt ostatnio zaznaczonego programu
        private Subprogram  lastSelectedSubprogram = null; // Wskaznik na obiekt ostatnio zaznaczonego subprogramu
        private bool        blockEvent          = false; ///< Flaga okresla ze zdarzenia zmiany wartosci komponentow nie pochodza od iterakcji z u serem

        private bool        blockRefreshTree    = false;
        //--------------------------------------------------------------------------------------------------------------------------------------
        public ProgramsConfigPanel()
        {
            InitializeComponent();

            blockEvent = true;

            HideButton();
            HideProgramComponent();
            ClearProgramInfo();
            treeViewProgram.Nodes.Clear();
            RefreshTreeViewPrograms();

            scrollPumpSetpoint.Maximum = (int)pressureResolution * 1100;
            scrollPumpSetpoint.Minimum = 1;

            scrollGasPressure.Maximum = (int)pressureResolution * 1100;
            scrollGasPressure.Minimum = 1;

            scrollGasPressureDevaDown.Maximum   = (int)pressureResolution * 1100;
            scrollGasPressureDevaUp.Minimum     = 1;

            scrollGasPressureDevaUp.Maximum     = (int)pressureResolution * 1100;
            scrollGasPressureDevaUp.Minimum     = 1;

            ShowCorrespondingTabPage();

            //Daj mozliwosc ustawienia backgourndcolor dla tab of pagecontrol
            tabControlProcess.DrawMode = TabDrawMode.OwnerDrawFixed;

            //Ustaw poprawne formaty dla datetime picker
            timePump.Format = DateTimePickerFormat.Custom;
            timePump.CustomFormat = "HH:mm:ss";

            timeGas.Format = DateTimePickerFormat.Custom;
            timeGas.CustomFormat = "HH:mm:ss";

            timePlasma.Format = DateTimePickerFormat.Custom;
            timePlasma.CustomFormat = "HH:mm:ss";

            timePurge.Format = DateTimePickerFormat.Custom;
            timePurge.CustomFormat = "HH:mm:ss";

            timeVent.Format = DateTimePickerFormat.Custom;
            timeVent.CustomFormat = "HH:mm:ss";

            dateTimeMotor1.Format = DateTimePickerFormat.Custom;
            dateTimeMotor1.CustomFormat = "HH:mm:ss";

            dateTimeMotor2.Format = DateTimePickerFormat.Custom;
            dateTimeMotor2.CustomFormat = "HH:mm:ss";

            blockEvent = false;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public TabPage TabPagePump
        {
            get { return tabPagePump; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public TabPage TabPagePlasma
        {
            get { return tabPagePlasma; }
        }//--------------------------------------------------------------------------------------------------------------------------------------
        public TabPage TabPageGas
        {
            get { return tabPageGas; }
        }//--------------------------------------------------------------------------------------------------------------------------------------
        public TabPage TabPagePurge
        {
            get { return tabPagePurge; }
        }//--------------------------------------------------------------------------------------------------------------------------------------
        public TabPage TabPageVent
        {
            get { return tabPageVent; }
        }//--------------------------------------------------------------------------------------------------------------------------------------
        public DB DataBase
        {
            set { dataBase = value; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public HPT1000.Source.Driver.HPT1000 HPT1000
        {
            set { hpt1000 = value; }
            get { return hpt1000; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        public GasTypes TypesGas
        {
            set { gasTypes = value; }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            /*       if (Width <= 0 || Height <= 0)
                       return;
                   Graphics g = e.Graphics;
                   Brush backBr = new System.Drawing.Drawing2D.LinearGradientBrush(new RectangleF(0, 0, Width, Height),
                       backGradientStartColor, backGradientEndColor,
                       System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                   g.FillRectangle(backBr, 0, 0, Width, Height);
                   backBr.Dispose();
             */
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Odświez dane w drzewie na temat aktuanych programow i subprogramow. Jezeli czegos brakuje to dodaj i zaktualizuj nazwe
        public void RefreshTreeViewPrograms()
        {
            if (!blockRefreshTree)
            {
                TreeNode nodePrograms = null;
                bool aExistProgram = false;
                bool aExistSubprogram = false;
                //Jezeli nie istnieje zaden wezel to dodaj pierwszy
                if (treeViewProgram.Nodes.Count == 0)
                    treeViewProgram.Nodes.Add("Programs list", "Programs list", 0, 0);
                if (treeViewProgram.Nodes.Count > 0)
                    nodePrograms = treeViewProgram.Nodes[0];

                if (hpt1000 != null && nodePrograms != null)
                {
                    foreach (Program pr in hpt1000.GetPrograms())
                    {
                        aExistProgram = false;
                        TreeNode nodeProgram = null;
                        if (IsTreeViewContainsObject(pr))
                        {
                            nodeProgram = GetNodeContainsObject(pr);
                            if (nodeProgram != null)
                                nodeProgram.Text = pr.GetName();
                            aExistProgram = true;
                        }
                        else
                        {
                            nodeProgram = new TreeNode(pr.GetName(), 1, 1);
                            nodeProgram.Tag = pr;
                        }
                        int index = 0;
                        foreach (Subprogram sub_pr in pr.GetSubprograms())
                        {
                            TreeNode nodeSubprogram = null;
                            aExistSubprogram = false;
                            if (IsTreeViewContainsObject(sub_pr))
                            {
                                nodeSubprogram = GetNodeContainsObject(sub_pr);
                                if (nodeSubprogram != null)
                                    nodeSubprogram.Text = sub_pr.GetName();
                                aExistSubprogram = true;
                            }
                            else
                            {
                                nodeSubprogram = new TreeNode(sub_pr.GetName(), 2, 2);
                                nodeSubprogram.Tag = sub_pr;
                            }
                            if (nodeProgram != null && !aExistSubprogram)
                                nodeProgram.Nodes.Insert(index, nodeSubprogram);

                            index++;
                        }
                        if (nodePrograms != null && !aExistProgram)
                            nodePrograms.Nodes.Add(nodeProgram);
                    }
                }
                RemoveEmptyNode();//usn wezly nie powiazane juz z zadnym obiektem
            }
        }        
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Ustaw komponenty formatki w zaleznosci od zalogowanego usera
        public void ShowComponetsPersmission(User user)
        {
            if(user != null)
            {
                if(user.Privilige != Types.UserPrivilige.Operator)
                {
                    SetPermissionModifyProgram(true);
                    btnAddNewProgram.Visible    = true;
                    btnRemoveProgram.Visible    = true;
                    btnAddNewSubprogram.Visible = true;
                    btnRemoveSubprogram.Visible = true;
                    btnSave.Visible             = true;
                    treeViewProgram.Size        = new Size(grBoxProgram.Left - grBoxPrograms.Left , grBoxProgramToolBtn.Top - 5 - treeViewProgram.Top); ;//grBoxProgramToolBtn
                    grBoxProgram.Enabled        = true;
                    grBoxSubprogram.Enabled     = true;
                    treeViewProgram.ContextMenuStrip = contextMenu;
                    grBoxSubprogramToolBtn.Visible = true;
                    grBoxProgramToolBtn.Visible = true;
                }
                else
                {
                    SetPermissionModifyProgram(false);
                    btnAddNewProgram.Visible    = false;
                    btnRemoveProgram.Visible    = false;
                    btnAddNewSubprogram.Visible = false;
                    btnRemoveSubprogram.Visible = false;
                    btnSave.Visible             = false;
                    treeViewProgram.Size        = new Size(grBoxProgram.Left - grBoxPrograms.Left , grBoxPrograms.Size.Height - 5 - treeViewProgram.Top);
                    grBoxProgram.Enabled        = false;
                    grBoxSubprogram.Enabled     = false;
                    treeViewProgram.ContextMenuStrip = null;
                    grBoxSubprogramToolBtn.Visible = false;
                    grBoxProgramToolBtn.Visible = false;
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Zablokuj/odblokuj mozeliwosc edytowania programow
        private void SetPermissionModifyProgram(bool permission)
        {
            foreach (TabPage page in tabControlProcess.TabPages)
            {
                foreach (Control ctr in page.Controls)
                {
                    ctr.Enabled = permission;
                }
            }
        }        
        //--------------------------------------------------------------------------------------------------------------------------------------
        void RemoveEmptyNode()
        {
            if (hpt1000 != null && treeViewProgram.Nodes.Count > 0)
            {
                //Usun wezel programu jezeli program juz nie istnieje
                for (int i = 0; i < treeViewProgram.Nodes[0].Nodes.Count; i++)
                {
                    TreeNode node = treeViewProgram.Nodes[0].Nodes[i];
                    if (!IsObjectExist(node.Tag))
                    {
                        treeViewProgram.Nodes[0].Nodes.Remove(node);
                        i = -1;
                    }
                }
                //Usun wezel sub-programu jezeli subprogram juz nie istnieje
                for (int i = 0; i < treeViewProgram.Nodes[0].Nodes.Count; i++)
                {
                    TreeNode node = treeViewProgram.Nodes[0].Nodes[i];
                    for (int j = 0; j < node.Nodes.Count; j++)
                    {
                        TreeNode subNode = node.Nodes[j];
                        if (!IsObjectExist(subNode.Tag))
                        {
                            node.Nodes.Remove(subNode);
                            j = -1;
                        }
                        //treeViewProgram.Nodes[0].Nodes.Remove(subNode);
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void AddNewProgram()
        {
            if (hpt1000 != null)
                hpt1000.NewProgram();
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void AddNewSubProgram()
        {
            Program program = null;
            TreeNode node = treeViewProgram.SelectedNode;

            program = GetProgram();

            if (program != null)
                program.NewSubprogram();
            else
                Logger.AddMsg(Translate.GetText("Can't add new subprogram because not selected program"), Types.MessageType.Error);

            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void RemoveProgram()
        {
            Program program = null;
            TreeNode node = treeViewProgram.SelectedNode;
            DialogResult result = DialogResult.Yes;

            program = GetProgram();

            if (program != null)
                result = MessageBox.Show("Are you sure you want to remove program " + program.GetName(), "Remove Subprogram", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (program == null)
                    Logger.AddMsg(Translate.GetText("Can't remove program because program not selected"), Types.MessageType.Error);

                if (hpt1000.RemoveProgram(program))
                    Logger.AddMsg(Translate.GetText("Program has been removed successfully"), Types.MessageType.Message);
                else
                    Logger.AddMsg(Translate.GetText("Program removed failed"), Types.MessageType.Error);

                RefreshTreeViewPrograms();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void RemoveSubprogram()
        {
            Program     program     = null;
            Subprogram  subProgram  = null;
            TreeNode    node        = treeViewProgram.SelectedNode;
            DialogResult result     = DialogResult.Yes;

            program     = GetProgram();
            subProgram  = GetSubprogram();

            if(subProgram != null)
                result = MessageBox.Show("Are you sure you want to remove subprogram " + subProgram.GetName(), "Remove Subprogram", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
           
                if (subProgram == null || program == null)
                    Logger.AddMsg(Translate.GetText("Can't remove subprogram because subprogram not selected"), Types.MessageType.Error);

                if (program != null && program.RemoveSubprogram(subProgram))
                    Logger.AddMsg(Translate.GetText("Subprogram has been removed successfully"), Types.MessageType.Message);
                else
                    Logger.AddMsg(Translate.GetText("Subprogram remove failed"), Types.MessageType.Error);

                RefreshTreeViewPrograms();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        bool IsObjectExist(object aObj)
        {
            bool aRes = false;
            if (hpt1000 != null && aObj != null)
            {
                foreach (Program program in hpt1000.GetPrograms())
                {
                    if (aObj.Equals(program))
                        aRes = true;
                    foreach (Subprogram subProgram in program.GetSubprograms())
                        if (subProgram.Equals(aObj))
                            aRes = true;
                }
            }
            return aRes;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private bool IsTreeViewContainsObject(object aObj)
        {
            bool aRes = false;
            if (treeViewProgram.Nodes.Count > 0)
            {
                TreeNode node = GetNode(treeViewProgram.Nodes[0], aObj);
                if (node != null)
                    aRes = true;
            }
            return aRes;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private TreeNode GetNodeContainsObject(object aObj)
        {
            TreeNode node = null;

            if (treeViewProgram.Nodes.Count > 0)
                node = GetNode(treeViewProgram.Nodes[0], aObj);

            return node;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        TreeNode GetNode(TreeNode nodeIn, object aObj)
        {
            TreeNode nodeRes = null;
            //sprawdz czy sam go nie mam
            if (nodeIn.Tag != null && nodeIn.Tag.Equals(aObj))
                nodeRes = nodeIn;
            //Ja nie mam tego obiektu u siebie wiec sprawdz czy nie maja go moje wezly
            else
            {
                foreach (TreeNode node in nodeIn.Nodes)
                {
                    if (node.Tag != null && node.Tag.Equals(aObj))
                    {
                        nodeRes = node;
                        break;
                    }
                    else
                        if (nodeRes == null)//jezeli jeszcze nie znalazlem to szukaj dalej
                            nodeRes = GetNode(node, aObj);
                }
            }
            return nodeRes;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void treeViewProgram_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //Sprawdz czy podczas zmiany wezla nie zgubia sie zapisane dane. Wysiwetl komunika dla programu tylko wtedy gdy wszedlem na wezel iiny niz jego subprogram
            if(lastSelectedProgram != null && !lastSelectedProgram.ContainsSubprgoram(e.Node.Text))
                CheckProgramAnyChanges();
            CheckSubprogramAnyChanges();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void treeViewProgram_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeViewProgram.SelectedNode;
            Program program = null;
            Subprogram subProgram = null;

            if (node != null)
            {
                HideButton();

                tabControlProcess.Visible = false;
                //zaznaczono program
                if (node.Parent != null && node.Parent.Parent == null)
                {
                    program = (Program)node.Tag;
                    btnRemoveProgram.Enabled = true;
                }
                //zaznaczono subprogram
                if (node.Parent != null && node.Parent.Parent != null && node.Parent.Parent.Parent == null)
                {
                    program = (Program)node.Parent.Tag;
                    subProgram = (Subprogram)node.Tag;
                    btnRemoveSubprogram.Enabled = true;
                    //Sprawdz indeks wezla i ustaw mozliwosc zmiany mijesca UP/DOWN
                    if (node.Index > 0)
                        btnUpSubprogram.Enabled = true;
                    if (node.Parent != null && node.Index < node.Parent.Nodes.Count - 1)
                        btnDownSubprogram.Enabled = true;

                    tabControlProcess.Visible = true;
                }          
                ClearProgramInfo();
                //Wyswietl info na temat programu
                if (program != null)
                {
                    tBoxNameProgram.Text = program.GetName();
                    tBoxDescProgram.Text = program.GetDescription();

                    foreach (Control ctr in grBoxProgram.Controls)
                        ctr.Enabled = true;

                    btnAddNewSubprogram.Enabled = true;
                }
                //Wyswietl info na temat subprogramu
                if (subProgram != null)
                {
                    tBoxNameSubprogram.Text = subProgram.GetName();
                    tBoxDescSubprgoram.Text = subProgram.GetDescription();

                    ShowInfoStageProcess(subProgram);

                    foreach (Control ctr in grBoxSubprogram.Controls)
                        ctr.Enabled = true;

                    tabControlProcess.Enabled = true;
                }
                //Pokaz odpowienio dopasoway menu dla wybranego wezla
                ShowPopupMenu();
                //Przypisz aktulny wezel jako ostatni. Wazne aby przyspianie odbylo sie po wywolaniu funkcji CheckSubprogramAnyChanges()
                lastSelectedProgram     = program;
                lastSelectedSubprogram  = subProgram;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Zadaniem metody jest sprawdzenie czy nie ma zmian na danym programie. Jezeli zmiany sa to poinformuj usera ze sa zmiany i czy nie che ich czasem zapisac bo w przeciwnym razie zostana utracoen
        /// </summary>
        public void CheckProgramAnyChanges()
        {
            //Sprawdz czy program posiada jakies niezapisane zmiany
            if (lastSelectedProgram != null && lastSelectedProgram.Changes)
            {
                bool changesStore = false; // flaga okresla czy mam zapamietac zmiany czy nie
                //Mozliwosc zmian posiadaja wszyscy poza operatorem
                if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator && ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.None)
                {
                    DialogResult res = MessageBox.Show("Program \'" + lastSelectedProgram.GetName() + "\' has been changed. If data is not stored, changes will be lost. Do you want store changes of program \'" + lastSelectedProgram.GetName() + "\'", "Program changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (res == DialogResult.Yes)
                        changesStore = true;
                }
                lastSelectedProgram.SetEditableParameters(changesStore); //Ustaw wartosci tymczasowe parametrow edycyjnych  jako wartosci rzeczywiste
            }
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Zadaniem metody jest sprawdzenie czy nie ma jakis zmian na danym subprogramie. Jezeli zmiany sa to poinformuj usera ze sa zmiany i czy nie che ich czasem zapisac bo w przeciwnym razie zostana utracoen
        /// </summary>
        public void CheckSubprogramAnyChanges()
        {
            //Sprawdz czy subprogram posiada jakies niezapisane zmiany
            if(lastSelectedSubprogram != null && lastSelectedSubprogram.Changes)
            {
                bool changesStore = false; // flaga okresla czy mam zapamietac zmiany czy nie
                //Mozliwosc zmian posiadaja wszyscy poza operatorem
                if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator && ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.None)
                {
                    DialogResult res = MessageBox.Show("Subprogram \'" + lastSelectedSubprogram.GetName() + "\' has been changed. If data is not stored, changes will be lost. Do you want store changes of subprogram \'" + lastSelectedSubprogram.GetName() + "\'", "Subprogram changes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (res == DialogResult.Yes)
                        changesStore = true;
                }
                lastSelectedSubprogram.SetEditableParameters(changesStore); //Ustaw wartosci tymczasowe parametrow edycyjnych  jako wartosci rzeczywiste
            }
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void ShowCorrespondingTabPage()
        {
            int aIndex = 0; 
            //Usun i dodaj TabPage jezeli dany stage prgoramu jest wybrany. Pamietaj aby je dodawac zawsze w tej samej kolejnosci: pump,gas,power,purge,vent
            if (!cBoxPump.Checked)
                tabControlProcess.TabPages.Remove(tabPagePump);
            else
            {
                if (!tabControlProcess.Contains(tabPagePump))
                    tabControlProcess.TabPages.Insert(aIndex, tabPagePump);
                aIndex++;
            }

            if (!cBoxGas.Checked)
                tabControlProcess.TabPages.Remove(tabPageGas);
            else
            {
                if (!tabControlProcess.Contains(tabPageGas))
                    tabControlProcess.TabPages.Insert(aIndex, tabPageGas);
                aIndex++;
            }

            if (!cBoxPower.Checked)
                tabControlProcess.TabPages.Remove(tabPagePlasma);
            else
            {
                if (!tabControlProcess.Contains(tabPagePlasma))
                    tabControlProcess.TabPages.Insert(aIndex, tabPagePlasma);
                aIndex++;
            }

            if (!cBoxPurge.Checked)
                tabControlProcess.TabPages.Remove(tabPagePurge);
            else
            {
                if (!tabControlProcess.Contains(tabPagePurge))
                    tabControlProcess.TabPages.Insert(aIndex, tabPagePurge);
                aIndex++;
            }

            if (!cBoxVent.Checked)
                tabControlProcess.TabPages.Remove(tabPageVent);
            else
            {
                if (cBoxVent.Checked && !tabControlProcess.Contains(tabPageVent))
                    tabControlProcess.TabPages.Insert(aIndex, tabPageVent);
                aIndex++;
            }

            if (!cBoxMotor.Checked || cBoxMotor.Visible == false)
                tabControlProcess.TabPages.Remove(tabPageMotor);
            else
            {
                if (cBoxMotor.Checked && !tabControlProcess.Contains(tabPageMotor))
                    tabControlProcess.TabPages.Insert(aIndex, tabPageMotor);
                aIndex++;
            }
            //Po dodaniu nowego taba skoryguj mozliwosc konfigurowania subprogramu jezeli jestem operatorem
            if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator || ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.None)
                SetPermissionModifyProgram(false);
            else
                SetPermissionModifyProgram(true);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Dostsuj zakladke gazow do aktualnej konfiguracji przeplywek MFC oraz Vaporaisera
         */ 
        void ShowOnlyActiveFlowDevice()
        {
            if (hpt1000 != null)
            {
                //SPrawdz czy jest widoczny vaporaiser
                if(hpt1000.GetVaporizer() != null)
                {
                    grBoxVaporiser.Visible      = hpt1000.GetVaporizer().GetActive();
                    cBoxVaporiser.Visible       = hpt1000.GetVaporizer().GetActive();
                    rBtnPressureViaVapo.Visible = hpt1000.GetVaporizer().GetActive();
                }
                //Sprawdz ktore przyplywki MFC sa aktywne
                if(hpt1000.GetMFC() != null)
                {
                    //Sprawdz czy jest aktywna MFC 1
                    grBoxMFC1.Visible       = hpt1000.GetMFC().GetActive(1);
                    grBoxGasesMFC1.Visible  = hpt1000.GetMFC().GetActive(1);
                    cBoxMFC1.Visible        = hpt1000.GetMFC().GetActive(1);
                    cBoxGasListMFC1.Visible = hpt1000.GetMFC().GetActive(1);
                    //Sprawdz czy jest aktywna MFC 2
                    grBoxMFC2.Visible       = hpt1000.GetMFC().GetActive(2);
                    grBoxGasesMFC2.Visible  = hpt1000.GetMFC().GetActive(2);
                    cBoxMFC2.Visible        = hpt1000.GetMFC().GetActive(2);
                    cBoxGasListMFC2.Visible = hpt1000.GetMFC().GetActive(2);
                    //Sprawdz czy jest aktywna MFC 3
                    grBoxMFC3.Visible       = hpt1000.GetMFC().GetActive(3);
                    grBoxGasesMFC3.Visible  = hpt1000.GetMFC().GetActive(3);
                    cBoxMFC3.Visible        = hpt1000.GetMFC().GetActive(3);
                    cBoxGasListMFC3.Visible = hpt1000.GetMFC().GetActive(3);

                    rBtnPressureViaGases.Visible = hpt1000.GetMFC().GetActive(1) || hpt1000.GetMFC().GetActive(2) || hpt1000.GetMFC().GetActive(3);

                }
                //Jezeli nie ma nic aktywne to nie pokazuj nic
                if (hpt1000.GetMFC() != null && hpt1000.GetVaporizer() != null && !hpt1000.GetMFC().GetActive(1) && !hpt1000.GetMFC().GetActive(2) && !hpt1000.GetMFC().GetActive(3) && !hpt1000.GetVaporizer().GetActive())
                {
                    grBoxSelectGasLine.Visible  = false;
                    grBoxGasFlow.Visible        = false;
                    grBoxGasPressure.Visible    = false;
                }
                else
                {
                    grBoxSelectGasLine.Visible  = true;
                    grBoxGasFlow.Visible        = true;
                    grBoxGasPressure.Visible    = true;
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoStageProcess(Subprogram subProgram)
        {
            blockEvent = true;

            ShowInfoPumpStage(subProgram.GetPumpProces());
            ShowInfoGasStage(subProgram.GetGasProces());
            ShowInfoPlasmaStage(subProgram.GetPlasmaProces());
            ShowInfoPurgeStage(subProgram.GetPurgeProces());
            ShowInfoVentStage(subProgram.GetVentProces());
            ShowInfoMotorStage(subProgram.GetMotorProces());

            blockEvent = false;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoPumpStage(PumpProces pumpStage)
        {
            if (pumpStage != null)
            {
                cBoxPump.Checked                = pumpStage.Active;
                timePump.Value                  = pumpStage.GetTimeWaitForPumpDown();
                dEditPumpSetpoint.Value         = pumpStage.GetSetpoint();
                int aValue = (int)(pumpStage.GetSetpoint() * pressureResolution);

                if (scrollPumpSetpoint.Maximum > aValue && aValue > scrollPumpSetpoint.Minimum)
                    scrollPumpSetpoint.Value    = aValue;

            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoGasStage(GasProces gasStage)
        {
            ShowOnlyActiveFlowDevice();

            if (gasStage != null)
            {              
                cBoxGas.Checked = gasStage.Active;
                timeGas.Value   = gasStage.GetTimeProcesDuration();

                SetLimitValue();
                SetLimitGasScroll(gasStage);

                dEditFlow1.Value = gasStage.GetGasFlow(1);
                dEditFlow2.Value = gasStage.GetGasFlow(2);
                dEditFlow3.Value = gasStage.GetGasFlow(3);

                dEditFlow1Min.Value = gasStage.GetMinGasFlow(1);
                dEditFlow2Min.Value = gasStage.GetMinGasFlow(2);
                dEditFlow3Min.Value = gasStage.GetMinGasFlow(3);

                dEditFlow1Max.Value = gasStage.GetMaxGasFlow(1);
                dEditFlow2Max.Value = gasStage.GetMaxGasFlow(2);
                dEditFlow3Max.Value = gasStage.GetMaxGasFlow(3);

                dEditGasPressure.Value            = gasStage.GetSetpointPressure();
                dEditGasPressureDevaDown.Value = gasStage.GetMinDeviationPresure();
                dEditGasPressureDevaUp.Value = gasStage.GetMaxDeviationPresure();

                dEditGasVaporCycleTime.Value = gasStage.GetCycleTime();
                dEditGasVaporOnTime.Value = gasStage.GetOnTime();

                dEditGasDevaShareMFC1.Value = gasStage.GetShareDevaition(1);
                dEditGasDevaShareMFC2.Value = gasStage.GetShareDevaition(2);
                dEditGasDevaShareMFC3.Value = gasStage.GetShareDevaition(3);

                dEditGasShareMFC1.Value = gasStage.GetShareGas(1);
                dEditGasShareMFC2.Value = gasStage.GetShareGas(2);
                dEditGasShareMFC3.Value = gasStage.GetShareGas(3);

                SetValueToScroll(scrollGasDevaShareMFC1, (int)gasStage.GetShareDevaition(1));
                SetValueToScroll(scrollGasDevaShareMFC2, (int)gasStage.GetShareDevaition(2));
                SetValueToScroll(scrollGasDevaShareMFC3, (int)gasStage.GetShareDevaition(3));

                SetValueToScroll(scrollGasPressure, (int)(gasStage.GetSetpointPressure() * pressureResolution));
                SetValueToScroll(scrollGasPressureDevaDown, (int)(gasStage.GetMinDeviationPresure() * pressureResolution));
                SetValueToScroll(scrollGasPressureDevaUp, (int)(gasStage.GetMaxDeviationPresure() * pressureResolution));

                SetValueToScroll(scrollGasVaporCycleTime, (int)gasStage.GetCycleTime());
                SetValueToScroll(scrollGasVaporOnTime, (int)gasStage.GetOnTime());

                SetValueToScroll(scrollFlow1, (int)gasStage.GetGasFlow(1));
                SetValueToScroll(scrollFlow2, (int)gasStage.GetGasFlow(2));
                SetValueToScroll(scrollFlow3, (int)gasStage.GetGasFlow(3));

                SetValueToScroll(scrollFlow1Min, (int)gasStage.GetMinGasFlow(1));
                SetValueToScroll(scrollFlow2Min, (int)gasStage.GetMinGasFlow(2));
                SetValueToScroll(scrollFlow3Min, (int)gasStage.GetMinGasFlow(3));

                SetValueToScroll(scrollFlow1Max, (int)gasStage.GetMaxGasFlow(1));
                SetValueToScroll(scrollFlow2Max, (int)gasStage.GetMaxGasFlow(2));
                SetValueToScroll(scrollFlow3Max, (int)gasStage.GetMaxGasFlow(3));

                grBoxSelectGasLine.Enabled = true;
                switch (gasStage.GetModeProces())
                {
                    case Types.GasProcesMode.FlowSP:
                        rBtnModeFlow.Checked        = true;
                        break;
                    case Types.GasProcesMode.Pressure_Vap:
                        rBtnModePressure.Checked    = true;
                        rBtnPressureViaVapo.Checked = true;
                        break;
                    case Types.GasProcesMode.Presure_MFC:
                        rBtnModePressure.Checked     = true;
                        grBoxSelectGasLine.Enabled   = true;
                        rBtnPressureViaGases.Checked = true;
                        break;
                    case Types.GasProcesMode.Unknown:
                        rBtnGasNone.Checked         = true;
                        grBoxSelectGasLine.Enabled  = false;
                        break;
                }

                cBoxMFC1.Checked = gasStage.GetActiveFlow(1);
                cBoxMFC2.Checked = gasStage.GetActiveFlow(2);
                cBoxMFC3.Checked = gasStage.GetActiveFlow(3);

                cBoxGasListMFC1.Enabled = gasStage.GetActiveFlow(1);
                cBoxGasListMFC2.Enabled = gasStage.GetActiveFlow(2);
                cBoxGasListMFC3.Enabled = gasStage.GetActiveFlow(3);

                grBoxMFC1.Enabled = cBoxMFC1.Checked;
                grBoxMFC2.Enabled = cBoxMFC2.Checked;
                grBoxMFC3.Enabled = cBoxMFC3.Checked;

                grBoxGasesMFC1.Enabled = cBoxMFC1.Checked;
                grBoxGasesMFC2.Enabled = cBoxMFC2.Checked;
                grBoxGasesMFC3.Enabled = cBoxMFC3.Checked;

                grBoxGasFlow.Enabled = rBtnModeFlow.Checked;
                grBoxGasPressure.Enabled = rBtnModePressure.Checked;

                cBoxVaporiser.Checked   = gasStage.GetVaporiserActive();
                grBoxVaporiser.Enabled  = gasStage.GetVaporiserActive();

                int aDosing = gasStage.GetDosing();
                if(aDosing >= scrolBarDosing.Minimum && aDosing <= scrolBarDosing.Maximum)
                    scrolBarDosing.Value = aDosing;
                dEditDosing.Value = aDosing;

                ShowGasType(gasStage);
            }
            SetLimitGasScroll(gasStage);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoPlasmaStage(PlasmaProces plasmaStage)
        {
            if (plasmaStage != null)
            {
                cBoxPower.Checked           = plasmaStage.Active;
                timePlasma.Value            = plasmaStage.GetTimeOperate();
                dEditPlasmaSetpoint.Value   = plasmaStage.GetSetpointPercent();
                dEditPlasmaDeviation.Value  = plasmaStage.GetDeviation();
                tBoxSetpointPlasma.Text     = plasmaStage.GetSetpointValue().ToString("0.000");//Pokaz rzeczywista wartosc setpointu

                if (scrollPlasmaSetpoint.Maximum > plasmaStage.GetSetpointPercent()  && scrollPlasmaSetpoint.Minimum < plasmaStage.GetSetpointPercent())
                    scrollPlasmaSetpoint.Value  = plasmaStage.GetSetpointPercent();
                if (scrollPlasmaDevistion.Maximum > plasmaStage.GetDeviation() && scrollPlasmaDevistion.Minimum < plasmaStage.GetDeviation())
                    scrollPlasmaDevistion.Value = (int)plasmaStage.GetDeviation();      
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoPurgeStage(FlushProces purgeStage)
        {
            cBoxPurge.Checked = purgeStage.Active;
            if (purgeStage != null)
            {
                timePurge.Value = purgeStage.GetTimePurge();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoVentStage(VentProces ventStage)
        {
            cBoxVent.Checked = ventStage.Active;
            if (ventStage != null)
            {
                timeVent.Value = ventStage.GetTimeVent();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ShowInfoMotorStage(MotorProces motorStage)
        {
            cBoxMotor.Checked = motorStage.Active;
            if (motorStage != null)
            {
                cBoxActiveMotor1.Checked = motorStage.GetActive(1);
                cBoxActiveMotor2.Checked = motorStage.GetActive(2);

                if (motorStage.GetState(1) == Types.StateFP.OFF)
                {
                    cBoxStateMotor1.Text = "OFF";
                    cBoxStateMotor1.Checked = false;
                }
                if (motorStage.GetState(1) == Types.StateFP.ON)
                {
                    cBoxStateMotor1.Text = "ON";
                    cBoxStateMotor1.Checked = true;
                }
                if (motorStage.GetState(2) == Types.StateFP.OFF)
                {
                    cBoxStateMotor2.Text = "OFF";
                    cBoxStateMotor2.Checked = false;
                }
                if (motorStage.GetState(2) == Types.StateFP.ON)
                {
                    cBoxStateMotor2.Text = "ON";
                    cBoxStateMotor2.Checked = true;
                }
                dateTimeMotor1.Value = motorStage.GetTimeMotor(1);
                dateTimeMotor2.Value = motorStage.GetTimeMotor(2);            
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Funkcja preentuje aktulnie przypisany typ gazu do danej przeplywki
        private void ShowGasType(GasProces gasStage)
        {
            // Kasuj nazwy gazow na group boxie
            grBoxMFC1.Text = "MFC 1 - N/A";
            grBoxMFC2.Text = "MFC 2 - N/A";
            grBoxMFC3.Text = "MFC 3 - N/A";
            grBoxGasesMFC1.Text = "MFC 1 - N/A";
            grBoxGasesMFC2.Text = "MFC 2 - N/A";
            grBoxGasesMFC3.Text = "MFC 3 - N/A";

            //Nie odsiwezaj gdy kist jest rozwinieta. Robie to sprawdzajac czy focusa nie posiadaja dzieci zas gdy focus jest na Parencie to mozna odswiezac
            if (gasStage != null)
            {
                //Pokaz typ gazu dla przeplywki 
                cBoxGasListMFC1.SelectedIndex = -1; 
                for (int i = 0; i < cBoxGasListMFC1.Items.Count; i++)
                {
                    GasType gasType = (GasType)cBoxGasListMFC1.Items[i];
                    if (gasType != null && gasType.ID == gasStage.GetGasType(1))
                    {
                        cBoxGasListMFC1.SelectedIndex = i;
                        grBoxMFC1.Text = "MFC 1 - " + gasType.Name;
                        grBoxGasesMFC1.Text = "MFC 1 - " + gasType.Name;
                    }
                }
                //Pokaz typ gazu dla przeplywki 2
                cBoxGasListMFC2.SelectedIndex = -1;
                for (int i = 0; i < cBoxGasListMFC2.Items.Count; i++)
                {
                    GasType gasType = (GasType)cBoxGasListMFC2.Items[i];
                    if (gasType != null && gasType.ID == gasStage.GetGasType(2))
                    {
                        cBoxGasListMFC2.SelectedIndex = i;
                        grBoxMFC2.Text = "MFC 2 - " + gasType.Name;
                        grBoxGasesMFC2.Text = "MFC 2 - " + gasType.Name;
                    }
            	}
                //Pokaz typ gazu dla przeplywki 3
                cBoxGasListMFC3.SelectedIndex = -1;
                for (int i = 0; i < cBoxGasListMFC3.Items.Count; i++)
                {
                    GasType gasType = (GasType)cBoxGasListMFC3.Items[i];
                    if (gasType != null && gasType.ID == gasStage.GetGasType(3))
                    {
                        cBoxGasListMFC3.SelectedIndex = i;
                        grBoxMFC3.Text = "MFC 3 - " + gasType.Name;
                        grBoxGasesMFC3.Text = "MFC 3 - " + gasType.Name;
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void SetLimitGasScroll(GasProces gasStage)
        {
            if(gasStage != null)
            {
                scrollFlow1.Minimum = gasStage.GetLimitDown(1);
                scrollFlow2.Minimum = gasStage.GetLimitDown(2);
                scrollFlow3.Minimum = gasStage.GetLimitDown(3);

                scrollFlow1.Maximum = gasStage.GetLimitUp(1) + scrollFlow1.LargeChange - 1;
                scrollFlow2.Maximum = gasStage.GetLimitUp(2) + scrollFlow2.LargeChange - 1;
                scrollFlow3.Maximum = gasStage.GetLimitUp(3) + scrollFlow3.LargeChange - 1;

                scrollFlow1Min.Minimum = gasStage.GetLimitDown(1);
                scrollFlow2Min.Minimum = gasStage.GetLimitDown(2);
                scrollFlow3Min.Minimum = gasStage.GetLimitDown(3);

                scrollFlow1Min.Maximum = gasStage.GetLimitUp(1) + scrollFlow1Min.LargeChange - 1;
                scrollFlow2Min.Maximum = gasStage.GetLimitUp(2) + scrollFlow2Min.LargeChange - 1;
                scrollFlow3Min.Maximum = gasStage.GetLimitUp(3) + scrollFlow3Min.LargeChange - 1;


                scrollFlow1Max.Minimum = gasStage.GetLimitDown(1);
                scrollFlow2Max.Minimum = gasStage.GetLimitDown(2);
                scrollFlow3Max.Minimum = gasStage.GetLimitDown(3);

                scrollFlow1Max.Maximum = gasStage.GetLimitUp(1) + scrollFlow1Max.LargeChange - 1;
                scrollFlow2Max.Maximum = gasStage.GetLimitUp(2) + scrollFlow2Max.LargeChange - 1;
                scrollFlow3Max.Maximum = gasStage.GetLimitUp(3) + scrollFlow3Max.LargeChange - 1;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void ClearProgramInfo()
        {
            tBoxDescProgram.Text = "";
            tBoxDescSubprgoram.Text = "";
            tBoxNameProgram.Text = "";
            tBoxNameSubprogram.Text = "";

            cBoxGas.Checked = false;
            cBoxPower.Checked = false;
            cBoxPurge.Checked = false;
            cBoxVent.Checked = false;
            cBoxPump.Checked = false;
            cBoxMotor.Checked = false;

            foreach (Control ctr in grBoxProgram.Controls)
                ctr.Enabled = false;

            foreach (Control ctr in grBoxSubprogram.Controls)
                ctr.Enabled = false;

            tabControlProcess.Enabled = false;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void HideButton()
        {
            //  btnAddNewProgram.Enabled        = false;
            btnAddNewSubprogram.Enabled = false;
            btnRemoveProgram.Enabled = false;
            btnRemoveSubprogram.Enabled = false;
            btnUpSubprogram.Enabled = false;
            btnDownSubprogram.Enabled = false;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        void HideProgramComponent()
        {/*
            grBoxGas.Enabled = false;
            grBoxPlasma.Enabled = false;
            grBoxPump.Enabled = false;
            grBoxPurge.Enabled = false;
            grBoxVent.Enabled = false;
        */}

        //--------------------------------------------------------------------------------------------------------------------------------------
        private void btnAddNewProgram_Click(object sender, EventArgs e)
        {
            AddNewProgram();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void btnAddNewSubprogram_Click(object sender, EventArgs e)
        {
            AddNewSubProgram();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Zwroc program z aktualnie zaznaczonego wezla
        Program GetProgram()
        {
            Program program = null;
            TreeNode node = treeViewProgram.SelectedNode;

            if (node != null && node.Parent != null && node.Parent.Parent == null)
                program = (Program)node.Tag;
            if (node != null && node.Parent != null && node.Parent.Parent != null && node.Parent.Parent.Parent == null)
                program = (Program)node.Parent.Tag;

            return program;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        Subprogram GetSubprogram()
        {
            Subprogram subPrgoram = null;
            TreeNode node = treeViewProgram.SelectedNode;
            if (node != null && node.Parent != null && node.Parent.Parent != null && node.Parent.Parent.Parent == null)
            {
                subPrgoram = (Subprogram)node.Tag;
            }
            return subPrgoram;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        PumpProces GetCurrentPumpProcess()
        {
            PumpProces pumpProces = null;
            if (GetSubprogram() != null)
                pumpProces = (PumpProces)GetSubprogram().PumpProces;
            return pumpProces;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        GasProces GetCurrentGasProcess()
        {
            GasProces gasProces = null;
            if (GetSubprogram() != null)
                gasProces = (GasProces)GetSubprogram().GasProces;
            return gasProces;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        PlasmaProces GetCurrentPlasmaProcess()
        {
            PlasmaProces plasmaProces = null;
            if (GetSubprogram() != null)
                plasmaProces = (PlasmaProces)GetSubprogram().PlasmaProces;
            return plasmaProces;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        VentProces GetCurrentVentProcess()
        {
            VentProces ventProces = null;
            if (GetSubprogram() != null)
                ventProces = (VentProces)GetSubprogram().VentProces;
            return ventProces;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        MotorProces GetCurrentMotorProcess()
        {
            MotorProces motorProces = null;
            if (GetSubprogram() != null)
                motorProces = (MotorProces)GetSubprogram().MotorProces;
            return motorProces;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        FlushProces GetCurrentPurgeProcess()
        {
            FlushProces purgeProces = null;
            if (GetSubprogram() != null)
                purgeProces = (FlushProces)GetSubprogram().PurgeProces;
            return purgeProces;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Funkcja sporawdz czy text zawiera poprawna wartosc zmiennoprzecikowa
        private bool CheckFloatStringValue(string aTxt, out double aValue)
        {
            bool aRes = false;
            
            if (Double.TryParse(aTxt, out aValue))
                aRes = true;
            else
                aValue = -1;

            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Funkcja sporawdz czy text zawiera poprawna wartosc zmiennoprzecikowa
        private bool CheckIntStringValue(string aTxt, out int aValue)
        {
            bool aRes = false;
                        
            if (Int32.TryParse(aTxt, out aValue))
                aRes = true;
            else
                aValue = -1;

            return aRes;
        }       
        //--------------------------------------------------------------------------------------------------------------------------------------
        void SetValueToScroll(HScrollBar scroll, int aValue)
        {
            if(aValue > scroll.Minimum && aValue < scroll.Maximum )
                scroll.Value = aValue;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        //Ustaw odpowiedie dostepne checkboxy oraz ustaw dane w subprogramie
        private void cBoxProcess_CheckedChanged(object sender, EventArgs e)
        {            
            ShowCorrespondingTabPage();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void tBoxNameProgram_KeyUp(object sender, KeyEventArgs e)
        {
            Program program = GetProgram();
            if (program != null)
                program.SetName(tBoxNameProgram.Text,true);
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void tBoxDescProgram_KeyUp(object sender, KeyEventArgs e)
        {
            Program program = GetProgram();
            if (program != null)
                program.SetDescription(tBoxDescProgram.Text);
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void tBoxNameSubprogram_KeyUp(object sender, KeyEventArgs e)
        {
            Subprogram Subprogram = GetSubprogram();
            if (Subprogram != null)
                Subprogram.SetName(tBoxNameSubprogram.Text,true);
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void tBoxDescSubprgoram_KeyUp(object sender, KeyEventArgs e)
        {
            Subprogram Subprogram = GetSubprogram();
            if (Subprogram != null)
                Subprogram.SetDescription(tBoxDescSubprgoram.Text);
            RefreshTreeViewPrograms();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void btnRemoveProgram_Click(object sender, EventArgs e)
        {
            RemoveProgram();
        }
        //--------------------------------------------------------------------------------------------------------------------------------------
        private void btnRemoveSubprogram_Click(object sender, EventArgs e)
        {
            RemoveSubprogram();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void cBoxProcess_Click(object sender, EventArgs e)
        {
            Subprogram subProgram = GetSubprogram();
            if (subProgram != null)
            {
                if (subProgram.PumpProces != null)   subProgram.PumpProces.Active   = cBoxPump.Checked;
                if (subProgram.GasProces != null)    subProgram.GasProces.Active    = cBoxGas.Checked;
                if (subProgram.PlasmaProces != null) subProgram.PlasmaProces.Active = cBoxPower.Checked;
                if (subProgram.PurgeProces != null)  subProgram.PurgeProces.Active  = cBoxPurge.Checked;
                if (subProgram.VentProces != null)   subProgram.VentProces.Active   = cBoxVent.Checked;
                if (subProgram.MotorProces != null)  subProgram.MotorProces.Active  = cBoxMotor.Checked;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //OPROGRAMOWANIE WPROWADZANIA WARTOSCI Z KONTROLEK UMIESZCZONYCH NA TAB PANELU DLA KONKRETNYCH PROCESOW
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void checkFloatValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            double aValue = 0;
            //Jezeli string nie zawiera liczby to nie wpisuj jej do pola 
            if (sender is TextBox && !CheckFloatStringValue(((TextBox)sender).Text + e.KeyChar, out aValue) && (int)e.KeyChar != 8)
                e.Handled = true;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void checkIntValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            int aValue = 0;
            //Jezeli string nie zawiera liczby to nie wpisuj jej do pola 
            if (sender is TextBox && !CheckIntStringValue(((TextBox)sender).Text + e.KeyChar, out aValue) && (int)e.KeyChar != 8)
                e.Handled = true;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //PUMP PROCES
        //----------------------------------------------------------------------------------------
        private bool dEditPumpSetpoint_EnterOn()
        {
            bool res = false;

            if (GetCurrentPumpProcess() != null && !blockEvent)
            {
                GetCurrentPumpProcess().SetSetpoint(dEditPumpSetpoint.Value, true);

                if (scrollPumpSetpoint.Maximum > dEditPumpSetpoint.Value * pressureResolution)
                    scrollPumpSetpoint.Value = (int)(dEditPumpSetpoint.Value * pressureResolution);

                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void timePump_ValueChanged(object sender, EventArgs e)
        {
            if (GetCurrentPumpProcess() != null && !blockEvent)
                GetCurrentPumpProcess().SetTimeWaitForPumpDown(timePump.Value,true);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollPumpSetpoint_ValueChanged(object sender, EventArgs e)
        {
            double aValue = 0;
            if(pressureResolution > 0)
                aValue = scrollPumpSetpoint.Value / pressureResolution;
            if (GetCurrentPumpProcess() != null && !blockEvent)
            {
                GetCurrentPumpProcess().SetSetpoint(aValue,true);
                dEditPumpSetpoint.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //PLASMA
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void timePlasma_ValueChanged(object sender, EventArgs e)
        {
            if (GetCurrentPlasmaProcess() != null && !blockEvent)
                GetCurrentPlasmaProcess().SetTimeOperate(timePlasma.Value,true);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditPlasmaSetpoint_EnterOn()
        {
            bool res = false;

            if (GetCurrentPlasmaProcess() != null && !blockEvent)
            {
                GetCurrentPlasmaProcess().SetSetpointPercent((int)dEditPlasmaSetpoint.Value, true);
                if (scrollPlasmaSetpoint.Maximum > (int)dEditPlasmaSetpoint.Value)
                    scrollPlasmaSetpoint.Value = (int)dEditPlasmaSetpoint.Value;                
                res = true;
            }        
            return res;
        }        
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollPlasmaSetpoint_Scroll(object sender, ScrollEventArgs e)
        {
            int aValue = scrollPlasmaSetpoint.Value;
            if (GetCurrentPlasmaProcess() != null && !blockEvent)
            {
                GetCurrentPlasmaProcess().SetSetpointPercent(aValue,true);
                dEditPlasmaSetpoint.Value = aValue;
                //Pokaz rzeczywista wartosc setpointu
                tBoxSetpointPlasma.Text = GetCurrentPlasmaProcess().GetSetpointValue().ToString("0.000");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditPlasmaDeviation_EnterOn()
        {
            bool res = false;
            if (GetCurrentPlasmaProcess() != null && !blockEvent)
            {
                GetCurrentPlasmaProcess().SetDeviation(dEditPlasmaDeviation.Value, true);
                if (scrollPlasmaDevistion.Maximum > (int)dEditPlasmaDeviation.Value)
                    scrollPlasmaDevistion.Value = (int)dEditPlasmaDeviation.Value;
                res = true;
            }
            return res;
        }
        //-----------------------------------------------------------------------------------------
        private void scrollPlasmaDevistion_Scroll(object sender, ScrollEventArgs e)
        {
            int aValue = scrollPlasmaDevistion.Value;
            if (GetCurrentPlasmaProcess() != null && !blockEvent)
            {
                GetCurrentPlasmaProcess().SetDeviation(aValue,true);
                dEditPlasmaDeviation.Value = aValue;
            }
        }
        //-----------------------------------------------------------------------------------------
        //PURGE
        //----------------------------------------------------------------------------------------
        private void timePyrge_ValueChanged(object sender, EventArgs e)
        {
            if (GetCurrentPurgeProcess() != null && !blockEvent)
                GetCurrentPurgeProcess().SetTimePurge(timePurge.Value,true);
        }
        //----------------------------------------------------------------------------------------
        //VENT
        //----------------------------------------------------------------------------------------
        private void timeVent_ValueChanged(object sender, EventArgs e)
        {
            if (GetCurrentVentProcess() != null && !blockEvent)
                GetCurrentVentProcess().SetTimeVent(timeVent.Value,true);
        }
        //-----------------------------------------------------------------------------------------
        //------------------------------GAS------------------------------------------
        //----------------------------------------------------------------------------------------
        //Metoda ma za zadanie pokaznie tymczasowych wartosci zminenej ShareGas
        private void ShowShareGasTmp()
        {
            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                dEditGasShareMFC1.Value = gasStage.GetShareGasTmp(1);
                dEditGasShareMFC2.Value = gasStage.GetShareGasTmp(2);
                dEditGasShareMFC3.Value = gasStage.GetShareGasTmp(3);
            }
        }
        //----------------------------------------------------------------------------------------

        private void SetModeGas()
        {
            Types.GasProcesMode mode = Types.GasProcesMode.FlowSP;

           if (rBtnModeFlow.Checked)
                mode = Types.GasProcesMode.FlowSP;
            else
            {
                if (rBtnPressureViaGases.Checked)
                    mode = Types.GasProcesMode.Presure_MFC;
                else
                    mode = Types.GasProcesMode.Pressure_Vap;
            }

            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
                ((GasProces)GetSubprogram().GasProces).SetModeProces(mode,true);

            grBoxGasFlow.Enabled        = rBtnModeFlow.Checked;
            grBoxGasPressure.Enabled    = rBtnModePressure.Checked;
            grBoxGasesMFC1.Enabled      = cBoxMFC1.Checked && rBtnPressureViaGases.Checked;
            grBoxGasesMFC2.Enabled      = cBoxMFC2.Checked && rBtnPressureViaGases.Checked;
            grBoxGasesMFC3.Enabled      = cBoxMFC3.Checked && rBtnPressureViaGases.Checked;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void rBtnGasMode_CheckedChanged(object sender, EventArgs e)
        {
            //Ustaw domyslnie tryb flow
            if (((Control)sender).Name == "rBtnModePressure" && ((RadioButton)sender).Checked == true )
                rBtnPressureViaGases.Checked = true;

            SetModeGas();
            grBoxSelectGasLine.Enabled = true;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void rBtnGasModePressure_CheckedChanged(object sender, EventArgs e)
        {
            SetModeGas();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void timeGas_ValueChanged(object sender, EventArgs e)
        {
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
                ((GasProces)GetSubprogram().GasProces).SetTimeProcesDuration(timeGas.Value,true);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void cBoxMFC1_Click(object sender, EventArgs e)
        {
            cBoxGasListMFC1.Enabled = cBoxMFC1.Checked;
            grBoxMFC1.Enabled       = cBoxMFC1.Checked;
            grBoxGasesMFC1.Enabled  = cBoxMFC1.Checked;

            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
                ((GasProces)GetSubprogram().GasProces).SetActiveFlow(cBoxMFC1.Checked,1,false,true);
            ShowShareGasTmp();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void cBoxMFC2_Click(object sender, EventArgs e)
        {
            cBoxGasListMFC2.Enabled = cBoxMFC2.Checked;
            grBoxMFC2.Enabled       = cBoxMFC2.Checked;
            grBoxGasesMFC2.Enabled  = cBoxMFC2.Checked;

            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
                ((GasProces)GetSubprogram().GasProces).SetActiveFlow(cBoxMFC2.Checked, 2, false, true);
            ShowShareGasTmp();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void cBoxMFC3_Click(object sender, EventArgs e)
        {
            cBoxGasListMFC3.Enabled = cBoxMFC3.Checked;
            grBoxMFC3.Enabled       = cBoxMFC3.Checked;
            grBoxGasesMFC3.Enabled  = cBoxMFC3.Checked;

            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
                ((GasProces)GetSubprogram().GasProces).SetActiveFlow(cBoxMFC3.Checked, 3, false, true);
            ShowShareGasTmp();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void cBoxVaporiser_Click(object sender, EventArgs e)
        {
            grBoxVaporiser.Enabled = cBoxVaporiser.Checked;

            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
                ((GasProces)GetSubprogram().GasProces).SetVaporaiserActive(cBoxVaporiser.Checked,true);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow1_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow1.Value  ;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetGasFlow(aValue, 1,true);
                dEditFlow1.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow2_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow2.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetGasFlow(aValue,  2,true);
                dEditFlow2.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow3_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow3.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetGasFlow(aValue, 3,true);
                dEditFlow3.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow1Min_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow1Min.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetMinGasFlow(aValue, 1,true);
                dEditFlow1Min.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow2Min_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow2Min.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetMinGasFlow(aValue, 2,true);
                dEditFlow2Min.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow3Min_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow3Min.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetMinGasFlow(aValue, 3,true);
                dEditFlow3Min.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow1Max_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow1Max.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetMaxGasFlow(aValue, 1,true);
                dEditFlow1Max.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow2Max_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow2Max.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetMaxGasFlow(aValue, 2,true);
                dEditFlow2Max.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollFlow3Max_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollFlow3Max.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetMaxGasFlow(aValue, 3,true);
                dEditFlow3Max.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasVaporCycleTime_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollGasVaporCycleTime.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetCycleTime(aValue,true);
                dEditGasVaporCycleTime.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasVaporOnTime_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollGasVaporOnTime.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetOnTime(aValue,true);
                dEditGasVaporOnTime.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasDevaShareMFC1_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollGasDevaShareMFC1.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetShareDevaition(aValue,1, true);
                dEditGasDevaShareMFC1.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasDevaShareMFC2_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollGasDevaShareMFC2.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetShareDevaition(aValue, 2, true);
                dEditGasDevaShareMFC2.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasDevaShareMFC3_ValueChanged(object sender, EventArgs e)
        {
            int aValue = scrollGasDevaShareMFC3.Value;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetShareDevaition(aValue, 3, true);
                dEditGasDevaShareMFC3.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasPressure_ValueChanged(object sender, EventArgs e)
        {
            double aValue = 0;
            if (pressureResolution > 0)
                aValue = scrollGasPressure.Value / pressureResolution;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                ((GasProces)GetSubprogram().GasProces).SetSetpointPressure(aValue, true);
                dEditGasPressure.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasPressureDevaDown_ValueChanged(object sender, EventArgs e)
        {
            double aValue = 0;
            if (pressureResolution > 0)
                aValue = scrollGasPressureDevaDown.Value / pressureResolution;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                GetCurrentGasProcess().SetMinDeviationPresure(aValue, true);
                dEditGasPressureDevaDown.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void scrollGasPressureDevaUp_ValueChanged(object sender, EventArgs e)
        {
            double aValue = 0;
            if (pressureResolution > 0)
                aValue = scrollGasPressureDevaUp.Value / pressureResolution;
            if (GetSubprogram() != null && GetSubprogram().GasProces != null && !blockEvent)
            {
                GetCurrentGasProcess().SetMaxDeviationPresure(aValue, true);
                dEditGasPressureDevaUp.Value = aValue;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Metoda ma za zadanie ustawienie limitow wartosci parametrow programu
        private void SetLimitValue(int aIDGasType = -1)
        {
            if ((GasProces)GetCurrentGasProcess() != null)
            {               
                dEditFlow1.MinimumValue     = ((GasProces)GetCurrentGasProcess()).GetLimitDown(1);
                dEditFlow1.MaximumValue     = ((GasProces)GetCurrentGasProcess()).GetLimitUp(1, aIDGasType);

                dEditFlow2.MinimumValue     = ((GasProces)GetCurrentGasProcess()).GetLimitDown(2);
                dEditFlow2.MaximumValue     = ((GasProces)GetCurrentGasProcess()).GetLimitUp(2, aIDGasType);

                dEditFlow3.MinimumValue     = ((GasProces)GetCurrentGasProcess()).GetLimitDown(3);
                dEditFlow3.MaximumValue     = ((GasProces)GetCurrentGasProcess()).GetLimitUp(3, aIDGasType);

                dEditFlow1Min.MinimumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitDown(1);
                dEditFlow1Min.MaximumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitUp(1, aIDGasType);

                dEditFlow2Min.MinimumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitDown(2);
                dEditFlow2Min.MaximumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitUp(2, aIDGasType);

                dEditFlow3Min.MinimumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitDown(3);
                dEditFlow3Min.MaximumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitUp(3, aIDGasType);

                dEditFlow1Max.MinimumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitDown(1);
                dEditFlow1Max.MaximumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitUp(1, aIDGasType);

                dEditFlow2Max.MinimumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitDown(2);
                dEditFlow2Max.MaximumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitUp(2, aIDGasType);

                dEditFlow3Max.MinimumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitDown(3);
                dEditFlow3Max.MaximumValue  = ((GasProces)GetCurrentGasProcess()).GetLimitUp(3, aIDGasType);

                dEditGasVaporCycleTime.MinimumValue = scrollGasVaporCycleTime.Minimum;
                dEditGasVaporCycleTime.MaximumValue = scrollGasVaporCycleTime.Maximum;                              
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow1_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetGasFlow(dEditFlow1.Value, 1,true);
                if (scrollFlow1.Maximum >= dEditFlow1.Value && scrollFlow1.Minimum <= dEditFlow1.Value)
                    scrollFlow1.Value = (int)(dEditFlow1.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow2_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetGasFlow(dEditFlow2.Value, 2, true);
                if (scrollFlow2.Maximum >= dEditFlow2.Value && scrollFlow2.Minimum <= dEditFlow2.Value)
                    scrollFlow2.Value = (int)(dEditFlow2.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow3_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetGasFlow(dEditFlow3.Value, 3, true);
                if (scrollFlow3.Maximum >= dEditFlow3.Value && scrollFlow3.Minimum <= dEditFlow3.Value)
                    scrollFlow3.Value = (int)(dEditFlow3.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow1Min_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMinGasFlow((int)dEditFlow1Min.Value, 1, true);
                if (scrollFlow1Min.Maximum >= dEditFlow1Min.Value && scrollFlow1Min.Minimum <= dEditFlow1Min.Value)
                    scrollFlow1Min.Value = (int)(dEditFlow1Min.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow2Min_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMinGasFlow((int)dEditFlow2Min.Value, 2, true);
                if (scrollFlow2Min.Maximum >= dEditFlow2Min.Value && scrollFlow2Min.Minimum <= dEditFlow2Min.Value)
                    scrollFlow2Min.Value = (int)(dEditFlow2Min.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow3Min_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMinGasFlow((int)dEditFlow3Min.Value, 3, true);
                if (scrollFlow3Min.Maximum >= dEditFlow3Min.Value && scrollFlow3Min.Minimum <= dEditFlow3Min.Value)
                    scrollFlow3Min.Value = (int)(dEditFlow3Min.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow1Max_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMaxGasFlow((int)dEditFlow1Max.Value, 1, true);
                if (scrollFlow1Max.Maximum >= dEditFlow1Max.Value && scrollFlow1Max.Minimum <= dEditFlow1Max.Value)
                    scrollFlow1Max.Value = (int)(dEditFlow1Max.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow2Max_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMaxGasFlow((int)dEditFlow1Max.Value, 2, true);
                if (scrollFlow2Max.Maximum >= dEditFlow2Max.Value && scrollFlow2Max.Minimum <= dEditFlow2Max.Value)
                    scrollFlow2Max.Value = (int)(dEditFlow2Max.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditFlow3Max_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMaxGasFlow((int)dEditFlow3Max.Value, 3, true);
                if (scrollFlow3Max.Maximum >= dEditFlow3Max.Value && scrollFlow3Max.Minimum <= dEditFlow3Max.Value)
                    scrollFlow3Max.Value = (int)(dEditFlow3Max.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasDevaShareMFC1_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetShareDevaition((int)dEditGasDevaShareMFC1.Value, 1, true);
                if (scrollGasDevaShareMFC1.Maximum >= dEditGasDevaShareMFC1.Value && scrollGasDevaShareMFC1.Minimum <= dEditGasDevaShareMFC1.Value)
                    scrollGasDevaShareMFC1.Value = (int)(dEditGasDevaShareMFC1.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasDevaShareMFC2_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetShareDevaition((int)dEditGasDevaShareMFC2.Value, 2, true);
                if (scrollGasDevaShareMFC2.Maximum >= dEditGasDevaShareMFC2.Value && scrollGasDevaShareMFC2.Minimum <= dEditGasDevaShareMFC2.Value)
                    scrollGasDevaShareMFC2.Value = (int)(dEditGasDevaShareMFC2.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasDevaShareMFC3_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetShareDevaition((int)dEditGasDevaShareMFC3.Value, 3, true);
                if (scrollGasDevaShareMFC3.Maximum >= dEditGasDevaShareMFC3.Value && scrollGasDevaShareMFC3.Minimum <= dEditGasDevaShareMFC3.Value)
                    scrollGasDevaShareMFC3.Value = (int)(dEditGasDevaShareMFC3.Value);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasShareMFC1_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetShareGas((int)dEditGasShareMFC1.Value, 1, false , true);
                ShowShareGasTmp();
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasShareMFC2_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetShareGas((int)dEditGasShareMFC2.Value, 2, false, true);
                ShowShareGasTmp();
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasShareMFC3_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetShareGas((int)dEditGasShareMFC3.Value, 3, false, true);
                ShowShareGasTmp();
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasVaporCycleTime_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetCycleTime(dEditGasVaporCycleTime.Value, true);
                if (scrollGasVaporCycleTime.Maximum >= dEditGasVaporCycleTime.Value && scrollGasVaporCycleTime.Minimum <= dEditGasVaporCycleTime.Value)
                    scrollGasVaporCycleTime.Value = (int)(dEditGasVaporCycleTime.Value);

                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasVaporOnTime_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetOnTime((int)dEditGasVaporOnTime.Value, true);
                if (scrollGasVaporOnTime.Maximum >= dEditGasVaporOnTime.Value && scrollGasVaporOnTime.Minimum <= dEditGasVaporOnTime.Value)
                    scrollGasVaporOnTime.Value = (int)(dEditGasVaporOnTime.Value);

                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasPressure_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetSetpointPressure(dEditGasPressure.Value, true);
                if (scrollGasPressure.Maximum >= dEditGasPressure.Value * pressureResolution && dEditGasPressure.Value >= 0.001)
                    scrollGasPressure.Value = (int)(dEditGasPressure.Value * pressureResolution);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasPressureDevaUp_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMaxDeviationPresure(dEditGasPressureDevaUp.Value, true);
                if (scrollGasPressureDevaUp.Maximum >= dEditGasPressureDevaUp.Value * pressureResolution && dEditGasPressureDevaUp.Value >= 0.001)
                    scrollGasPressureDevaUp.Value = (int)(dEditGasPressureDevaUp.Value * pressureResolution);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private bool dEditGasPressureDevaDown_EnterOn()
        {
            bool res = false;

            GasProces gasStage = GetCurrentGasProcess();
            if (gasStage != null)
            {
                gasStage.SetMinDeviationPresure(dEditGasPressureDevaDown.Value, true);
                if (scrollGasPressureDevaDown.Maximum >= dEditGasPressureDevaDown.Value * pressureResolution && dEditGasPressureDevaDown.Value >= 0.001)
                    scrollGasPressureDevaDown.Value = (int)(dEditGasPressureDevaDown.Value * pressureResolution);
                res = true;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Funkcja jest wywolywana jako delegat w momencie zmian dokonywanych w liscie programow/subprogramow
        public void RefreshProgram()
        {
            flagRefreshProgram = true;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            //Z uwagi na fakt ze nie mozna odswiez komponentow graficnzych z innego watku niz w ktorycm zostaly one utworzone dlatego odswiezam to przez Timer i flage
            if (flagRefreshProgram)
            {
                RefreshTreeViewPrograms();
                flagRefreshProgram = false;
            }
            //Podswietl aktualnie zaznaczony program/subprogram
            ShowSelectedNode(treeViewProgram.Nodes);
            //Ustaw odpowiednie Share dla wybranej stosownej liczby kanałów
            ShowShareValue();
            //Dostosuj aktuwnpsc motor do ich liczby
            SetMotorActive();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Funkja ma za zadaie ustawienie aktuywnosci motor jezeli jest dostepny tylko 1 to on jest ustawiony z automtu jako aktywny. Jezeli sa dwa to user decyduje ktory ma byc aktywny
        private void SetMotorActive()
        {
            //Pokazuj mozliwosci wyboru motora tylko gdy jest wiecej niz jeden motor dostepny w systemie
            if (MotorDriver.Motor_1_Enable && MotorDriver.Motor_2_Enable)
                grBoxSelectMotor.Visible = true;
            else
                grBoxSelectMotor.Visible = false;

            //Aktywny tylko motor 1 ustaw z automota jego program oraz nie wyswietlaj mozliwosi ustawiania aktywnosci motor 2 bo i tak jest tylko 1
            if (MotorDriver.Motor_1_Enable && !MotorDriver.Motor_2_Enable)
            {
                cBoxActiveMotor1.Checked = true;
                if (GetCurrentMotorProcess() != null)
                {
                    GetCurrentMotorProcess().SetActive(1, true);
                    GetCurrentMotorProcess().SetActive(2, false);
                }
                grBoxMotor1.Enabled  = true;
                grBoxMotor1.Location = new Point(2, labTitelMotor.Size.Height -20);
                grBoxMotor1.Size     = new Size(labTitelMotor.Size.Width - 4, tabPageMotor.Height - labTitelMotor.Size.Height - 5);

            }
            //Aktywny tylko motor 2 ustaw z automota jego program oraz nie wyswietlaj mozliwosi ustawiania aktywnosci motor bo i tak jest tylko 1
            if (!MotorDriver.Motor_1_Enable && MotorDriver.Motor_2_Enable)
            {
                cBoxActiveMotor2.Checked = true;
                if (GetCurrentMotorProcess() != null)
                {
                    GetCurrentMotorProcess().SetActive(1, false);
                    GetCurrentMotorProcess().SetActive(2, true);
                }
                grBoxMotor2.Enabled  = true;
                grBoxMotor2.Location = new Point(2, labTitelMotor.Size.Height - 20);
                grBoxMotor2.Size     = new Size(labTitelMotor.Size.Width - 4, tabPageMotor.Height - labTitelMotor.Size.Height - 5);

            }
            //Dostepne sa oba motory
            if (MotorDriver.Motor_1_Enable && MotorDriver.Motor_2_Enable)
            {
                grBoxMotor1.Location = new Point(2, grBoxSelectMotor.Height + labTitelMotor.Size.Height - 10 );
                grBoxMotor1.Size     = new Size(labTitelMotor.Size.Width / 2 - 10 , tabPageMotor.Height - labTitelMotor.Size.Height - grBoxSelectMotor.Size.Height - 40);

                grBoxMotor2.Location = new Point(labTitelMotor.Size.Width / 2 , grBoxSelectMotor.Height + labTitelMotor.Size.Height - 10);
                grBoxMotor2.Size     = new Size(labTitelMotor.Size.Width / 2 - 10 , tabPageMotor.Height - labTitelMotor.Size.Height - grBoxSelectMotor.Size.Height - 40);

            }
            //Pokaz komponenty w zaleznosci od skonfiguropwanych motorow
            grBoxMotor1.Visible = MotorDriver.Motor_1_Enable;
            grBoxMotor2.Visible = MotorDriver.Motor_2_Enable;
            //Nie wyswietlaj cBoxa umozliwiajacego ustawinie programu motora
            if (!MotorDriver.Motor_1_Enable && !MotorDriver.Motor_2_Enable)
                cBoxMotor.Visible = false;
            else
                cBoxMotor.Visible = true;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void ShowShareValue()
        {
            GasProces gasProces = GetCurrentGasProcess();
            if (gasProces != null)
            {
                if (!dEditGasShareMFC1.Focused)
                    dEditGasShareMFC1.Text = gasProces.GetShareGas(1).ToString();
                if (!dEditGasShareMFC2.Focused)
                    dEditGasShareMFC2.Text = gasProces.GetShareGas(2).ToString();
                if (!dEditGasShareMFC3.Focused)
                    dEditGasShareMFC3.Text = gasProces.GetShareGas(3).ToString();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Podswietl aktualnie zaznaczony program/subprogram
        private void ShowSelectedNode(TreeNodeCollection nodes)
        {
            if (nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    //jezeli mam dzieci to wywolaj funkcje rekurencyjnie jeszcze raz
                    if (node.Nodes.Count > 0)
                        ShowSelectedNode(node.Nodes);

                    if (node.IsSelected)
                    {
                        node.BackColor = SystemColors.Highlight;
                        node.ForeColor = Color.White;
                    }
                    else
                    {
                        node.BackColor = Color.Transparent;
                        node.ForeColor = Color.Black;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void ShowPopupMenu()
        {
            TreeNode node = treeViewProgram.SelectedNode;

            menuItem_AddProgram.Visible       = false;
            menuItem_AddSubprogram.Visible    = false;
            menuItem_RemoveProgram.Visible    = false;
            menuItem_RemoveSubprogram.Visible = false;

            toolStripSeparator.Visible        = true;

            if (node != null)
            {
                //zaznaczono glowny wezel
                if (node.Parent == null)
                {
                    menuItem_AddProgram.Visible = true;
                    toolStripSeparator.Visible  = false;
                }
                //zaznaczono program
                if (node.Parent != null && node.Parent.Parent == null)
                {
                    menuItem_AddSubprogram.Visible = true;
                    menuItem_RemoveProgram.Visible = true; 
                }
                //zaznaczono subprogram
                if (node.Parent != null && node.Parent.Parent != null && node.Parent.Parent.Parent == null)
                {
                    menuItem_AddSubprogram.Visible    = true;
                    menuItem_RemoveSubprogram.Visible = true;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
     /*   private void btnReadFromPLC_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null)
                hpt1000.ReadProgramFromPLC();
        }
    */
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void menuItem_AddProgram_Click(object sender, EventArgs e)
        {
            AddNewProgram();
        }

        private void menuItem_AddSubprogram_Click(object sender, EventArgs e)
        {
            AddNewSubProgram();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void menuItem_RemoveProgram_Click(object sender, EventArgs e)
        {
            RemoveProgram();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void menuItem_RemoveSubprogram_Click(object sender, EventArgs e)
        {
            RemoveSubprogram();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null)
                hpt1000.SaveProgramInDB();
            /*int aRes = 0;
            //zapisz ustawienia programow i subprogramow w bazie danych ale tylko tych wsrod ktorych zostalo cos zmienione
            if (hpt1000 != null && dataBase != null)
            {
                foreach (Program pr in hpt1000.GetPrograms())
                {
                    dataBase.ModifyProgram(pr);
                    foreach (Subprogram subpr in pr.GetSubprograms())
                        aRes += subpr.SaveDataInDB();// dataBase.ModifySubprogram(subpr);
                }
            }
            if(aRes != 0)
                System.Windows.Forms.MessageBox.Show("Data save failed ","Save Data",MessageBoxButtons.OK, MessageBoxIcon.Error);
            */
            RefreshTreeViewPrograms();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        private void ProgramsConfigPanel_VisibleChanged(object sender, EventArgs e)
        {
            RefreshTreeViewPrograms();
            ShowCorespondingVaporiser();
            SetLimitValue();
            treeViewProgram_AfterSelect(this, null);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Metoda ma za zdanie zwrocenie prgroamu do ktorego nalezy dany subprogram
        Program GetParentProgram(Subprogram subprogram)
        {
            Program program = null;
            if (hpt1000 != null)
            {
                foreach (Program pr in hpt1000.GetPrograms())
                {
                    foreach (Subprogram subpr in pr.GetSubprograms())
                    {
                        if (subpr == subprogram)
                        {
                            program = pr;
                            break;
                        }
                    }
                }
            }
            return program;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        TreeNode GetProgramNode(Program program)
        {
            TreeNode node = null;
            if (treeViewProgram.Nodes.Count > 0)
            {
                foreach (TreeNode nd in treeViewProgram.Nodes[0].Nodes)
                {
                    if ((Program)nd.Tag == program)
                        node = nd;
                }
            }
            return node;
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
        //Funkcaj ma za zadanie wyswietleni listy dostepnych typow gazow ktore moga byc przypisane do danego kanalu gazowego
        public void RefreshGasType()
        {
            if (gasTypes != null )
            {
                cBoxGasListMFC1.Items.Clear();
                cBoxGasListMFC2.Items.Clear();
                cBoxGasListMFC3.Items.Clear();
                foreach (GasType gasType in gasTypes.Items)
                {
                    cBoxGasListMFC1.Items.Add(gasType);
                    cBoxGasListMFC2.Items.Add(gasType);
                    cBoxGasListMFC3.Items.Add(gasType);
                }
                //Pokaz gas type dla aktulnie wybranego subprogramu
                GasProces gasStage = GetCurrentGasProcess();
                ShowGasType(gasStage);                
            }
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw dany typ gazu dla przeplywki 1
        private void cBoxGasListMFC1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GasProces gasStage  = GetCurrentGasProcess();
            GasType   typeGas   = (GasType)cBoxGasListMFC1.SelectedItem;
            int aTypeID = -1;
            if (gasStage != null && typeGas != null)
            {
                gasStage.SetGasType(typeGas.ID, 1, true);
                aTypeID = typeGas.ID;
            }
            //Odswiez limity poniewaz zaleza od aktualnie wybranego gazu
            SetLimitValue(aTypeID);
            SetLimitGasScroll(gasStage);
            dEditFlow1.Value = dEditFlow1.Value;
            dEditFlow1_EnterOn();
            //Wyswietl nazwe gazu w groupboxie
            if (typeGas != null)
            {
                grBoxGasesMFC1.Text = "MFC 1 - " + typeGas.Name;
                grBoxMFC1.Text = "MFC 1 - " + typeGas.Name;
            }
            else
            {
                grBoxGasesMFC1.Text = "MFC 1 - N/A";
                grBoxMFC1.Text = "MFC 1 - N/A";
            }
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw dany typ gazu dla przeplywki 2
        private void cBoxGasListMFC2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GasProces gasStage  = GetCurrentGasProcess();
            GasType   typeGas   = (GasType)cBoxGasListMFC2.SelectedItem;
            int aTypeID = -1;

            if (gasStage != null && typeGas != null)
            {
                gasStage.SetGasType(typeGas.ID, 2,true);
                aTypeID = typeGas.ID;
            }
            //Odswiez limity poniewaz zaleza od aktualnie wybranego gazu
            SetLimitValue(aTypeID);
            SetLimitGasScroll(gasStage);
            dEditFlow2.Value = dEditFlow2.Value;
            dEditFlow2_EnterOn();
            //Wyswietl nazwe gazu w groupboxie
            if (typeGas != null)
            {
                grBoxGasesMFC2.Text = "MFC 2 - " + typeGas.Name;
                grBoxMFC2.Text = "MFC 2 - " + typeGas.Name;
            }
            else
            {
                grBoxGasesMFC2.Text = "MFC 2 - N/A";
                grBoxMFC2.Text = "MFC 2 - N/A";
            }
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw dany typ gazu dla przeplywki 3
        private void cBoxGasListMFC3_SelectedIndexChanged(object sender, EventArgs e)
        {
            GasProces gasStage  = GetCurrentGasProcess();
            GasType   typeGas   = (GasType)cBoxGasListMFC3.SelectedItem;
            int aTypeID = -1;

            if (gasStage != null && typeGas != null)
            {
                gasStage.SetGasType(typeGas.ID, 3,true);
                aTypeID = typeGas.ID;
            }
            //Odswiez limity poniewaz zaleza od aktualnie wybranego gazu
            SetLimitValue(aTypeID);
            SetLimitGasScroll(gasStage);
            dEditFlow3.Value = dEditFlow3.Value;
            dEditFlow3_EnterOn();
            //Wyswietl nazwe gazu w groupboxie
            if (typeGas != null)
            {
                grBoxGasesMFC3.Text = "MFC 3 - " + typeGas.Name;
                grBoxMFC3.Text = "MFC 3 - " + typeGas.Name;
            }
            else
            {
                grBoxGasesMFC3.Text = "MFC 3 - N/A";
                grBoxMFC3.Text = "MFC 3 - N/A";
            }
        }
        //-----------------------------------------------------------------------------------------
        private void ShowCorespondingVaporiser()
        {
            bool typeCycle = false;
            bool typeDosing = false;

            if (hpt1000 != null && hpt1000.GetVaporizer() != null)
            {
                Vaporizer vaporaiser = hpt1000.GetVaporizer();
                 if (Vaporizer.GetTypeVaporaizer() == Types.VaporaizerType.Cycle)
                    typeCycle = true;
                if (Vaporizer.GetTypeVaporaizer() == Types.VaporaizerType.Dosing)
                    typeDosing = true;
            }
            labCycle.Visible                = typeCycle;
            labOnTIme.Visible               = typeCycle;
            labUnitCycle.Visible            = typeCycle;
            labUnitOnTime.Visible           = typeCycle;
            dEditGasVaporCycleTime.Visible  = typeCycle;
            dEditGasVaporOnTime.Visible     = typeCycle;
            scrollGasVaporCycleTime.Visible = typeCycle;
            scrollGasVaporOnTime.Visible    = typeCycle;
            labDosing.Visible               = typeDosing;
            labUnitDosing.Visible           = typeDosing;
            scrolBarDosing.Visible          = typeDosing;
            dEditDosing.Visible             = typeDosing;
        }
        //-----------------------------------------------------------------------------------------
        private bool dEditDosing_EnterOn()
        {
            bool aRes = false;
            GasProces gasStage = GetCurrentGasProcess();

            if (gasStage != null)
            {
                double aValue = dEditDosing.Value;
                if (aValue >= scrolBarDosing.Minimum && aValue <= scrolBarDosing.Maximum)
                {
                    gasStage.SetDosing((int)dEditDosing.Value);
                    scrolBarDosing.Value = (int)aValue;
                    aRes = true;
                }
            }
            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        private void scrolBarDosing_ValueChanged(object sender, EventArgs e)
        {
            GasProces gasStage = GetCurrentGasProcess();

            if(gasStage != null && !blockEvent)
            {
                gasStage.SetDosing(scrolBarDosing.Value);
                dEditDosing.Value = scrolBarDosing.Value;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia tlo dla zakladek programow
         */ 
        private void tabControlProcess_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                using (LinearGradientBrush br = new LinearGradientBrush(e.Bounds, ApplicationData.PageHeaderColor1, ApplicationData.PageHeaderColor2, LinearGradientMode.BackwardDiagonal))
                {
                    // Color the Tab Header
                    e.Graphics.FillRectangle(br, e.Bounds);
                    // swap our height and width dimensions
                    var rotatedRectangle = new Rectangle(0, 0, e.Bounds.Height, e.Bounds.Width);

                    // Rotate
                    e.Graphics.ResetTransform();
                    e.Graphics.RotateTransform(-90);

                    // Translate to move the rectangle to the correct position.
                    e.Graphics.TranslateTransform(e.Bounds.Left, e.Bounds.Bottom, MatrixOrder.Append);

                    // Format String
                    var drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;
                    drawFormat.LineAlignment = StringAlignment.Center;

                    // Draw Header Text
                    e.Graphics.DrawString(tabControlProcess.TabPages[e.Index].Text, e.Font, Brushes.White, rotatedRectangle, drawFormat);

                }
            }
            catch (Exception ex) { }
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw czas dla procesu motora 1
        private void dateTimeMotor1_ValueChanged(object sender, EventArgs e)
        {
            if (GetCurrentMotorProcess() != null && !blockEvent)
                GetCurrentMotorProcess().SetTimeMotor(1,dateTimeMotor1.Value,true);
        }
        //-----------------------------------------------------------------------------------------
        //Ustaw czas dla procesu motora 2
        private void dateTimeMotor2_ValueChanged(object sender, EventArgs e)
        {
            if (GetCurrentMotorProcess() != null && !blockEvent)
                GetCurrentMotorProcess().SetTimeMotor(2,dateTimeMotor2.Value, true);
        }
        //-----------------------------------------------------------------------------------------
        private void cBoxStateMotor1_Click(object sender, EventArgs e)
        {
            Types.StateFP aState = Types.StateFP.OFF;
            cBoxStateMotor1.Text = "OFF";
            if (cBoxStateMotor1.Checked)
            {
                cBoxStateMotor1.Text = "ON";
                aState = Types.StateFP.ON;
            }

            if (GetCurrentMotorProcess() != null)
                GetCurrentMotorProcess().SetState(1, aState);
        }
        //-----------------------------------------------------------------------------------------
        private void cBoxStateMotor2_Click(object sender, EventArgs e)
        {
            Types.StateFP aState = Types.StateFP.OFF;
            cBoxStateMotor2.Text = "OFF";
            if (cBoxStateMotor2.Checked)
            {
                cBoxStateMotor2.Text = "ON";
                aState = Types.StateFP.ON;
            }

            if (GetCurrentMotorProcess() != null)
                GetCurrentMotorProcess().SetState(2, aState);
        }
        //-----------------------------------------------------------------------------------------
        private void cBoxActiveMotor1_Click(object sender, EventArgs e)
        {
            grBoxMotor1.Enabled = cBoxActiveMotor1.Checked;
            if (GetCurrentMotorProcess() != null)
                GetCurrentMotorProcess().SetActive(1, cBoxActiveMotor1.Checked);
        }
        //-----------------------------------------------------------------------------------------
        private void cBoxActiveMotor2_Click(object sender, EventArgs e)
        {
            grBoxMotor2.Enabled = cBoxActiveMotor2.Checked;
            if (GetCurrentMotorProcess() != null)
                GetCurrentMotorProcess().SetActive(2, cBoxActiveMotor2.Checked);
        }
        //-----------------------------------------------------------------------------------------
        //Metoda ma za zadanie podniesienie danego subprgramu do gory na liscie progrmow
        private void btnUpSubprogram_Click(object sender, EventArgs e)
        {
            blockRefreshTree = true;
            try
            {
                //Odczytaj aktualny subprogram oraz jego parena program
                Subprogram subprogram = GetSubprogram();
                Program program = GetParentProgram(subprogram);
                if (program != null)
                {
                    program.UpSubprogram(subprogram);
                    //Zmien miejscami wezly w liscie treview
                    TreeNode selectedNode = treeViewProgram.SelectedNode;
                    if (selectedNode != null)
                    {
                        TreeNode parent = selectedNode.Parent;
                        int selectedIndex = selectedNode.Index;
                        if (parent != null && selectedIndex > 0)
                        {
                            selectedNode.Remove();
                            parent.Nodes.Insert(selectedIndex - 1, selectedNode);
                            treeViewProgram.SelectedNode = selectedNode;
                        }
                    }
                }
            }
            finally { blockRefreshTree = false; }
        }
        //-----------------------------------------------------------------------------------------
        //Metoda ma za zadanie obnizenie danego subprgramu do gory na liscie progrmow
        private void btnDownSubprogram_Click(object sender, EventArgs e)
        {
            blockRefreshTree = true;
            try
            {

                //Odczytaj aktualny subprogram
                Subprogram subprogram = GetSubprogram();
                //Pobierz program do ktroego nalezy dany subprogram
                Program program = GetParentProgram(subprogram);
                if (program != null)
                {
                    program.DownSubprogram(subprogram);
                    //Zmien miejscami wezly w liscie treview
                    TreeNode selectedNode = treeViewProgram.SelectedNode;
                    if (selectedNode != null)
                    {
                        TreeNode parent = selectedNode.Parent;
                        int selectedIndex = selectedNode.Index;
                        if (parent != null && selectedIndex < parent.Nodes.Count - 1)
                        {
                            selectedNode.Remove();
                            parent.Nodes.Insert(selectedIndex + 1, selectedNode);
                            treeViewProgram.SelectedNode = selectedNode;
                        }
                    }
                }
            }
            finally { blockRefreshTree = false; }
        }      
    }
}
