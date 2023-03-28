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
using HPT1000.Source.Program;
using HPT1000.Source.Chamber;
using HPT1000.Source;

namespace HPT1000.GUI
{
    public partial class ProgramPanel : UserControl
    {
        private Source.Driver.HPT1000   hpt1000 = null;

        private bool                    flagRefreshProgram  = false;
        private int                     timerWaitMode       = 0;
        private int						timeWaitRefreshMode = 10; // 1s

        private bool                    fBlockFunAction     = false;               //Flaga jest wykorzystywana do blokowania wykonywania funkcji akcji ktore nie sa wynikiem ingernecji usera
        private bool                    actualProgramWasSet = false;

        private Color _Color1 = Color.Gainsboro;
        private Color _Color2 = Color.White;
        private float _ColorAngle = 30f;

        private ProcessInformationForm  procesInfoForm = new ProcessInformationForm();

        //---------------------------------------------------------------------------------------
        public HPT1000.Source.Driver.HPT1000 HPT1000
        {
            set { hpt1000 = value; }
            get { return hpt1000; }
        }
        //---------------------------------------------------------------------------------------
        public Color Color1
        {
            get { return _Color1; }
            set
            {
                _Color1 = value;
                this.Invalidate(); // Tell the Form to repaint itself
            }
        }
        //---------------------------------------------------------------------------------------
        public Color Color2
        {
            get { return _Color2; }
            set
            {
                _Color2 = value;
                this.Invalidate(); // Tell the Form to repaint itself
            }
        }
        //---------------------------------------------------------------------------------------
        public float ColorAngle
        {
            get { return _ColorAngle; }
            set
            {
                _ColorAngle = value;
                this.Invalidate(); // Tell the Form to repaint itself
            }
        }
        //---------------------------------------------------------------------------------------
        public Subprogram CurentSubProgram
        {
            get
            {
                Subprogram aActualSubprogram = null;
                foreach (ListViewItem item in listViewSubprograms.Items)
                {
                    Subprogram subPr = (Subprogram)item.Tag;
                    if (subPr.GetStatus() == Types.StatusProgram.Run)
                        aActualSubprogram = subPr;
                }
                return aActualSubprogram;
            }
        }
        //---------------------------------------------------------------------------------------
        public ProgramPanel()
        {
            InitializeComponent();
            ClearPanel();
            labStatus.Text = "";
        }
        //---------------------------------------------------------------------------------------
        private void ClearPanel(bool aOnlySubprogramData = false)
        {
            if (!aOnlySubprogramData)
            {
                cBoxPrograms.Items.Clear();
                listViewSubprograms.Items.Clear();
            }

            labPumpSetpoint.Text    = "";
            labPumpTime.Text        = "--:--:--";
            labPumpTimeTarget.Text  = "--:--:--";

            labPurgeTime.Text       = "--:--:--";
            labPurgeTimeTarget.Text = "--:--:--";

            labVentTime.Text        = "--:--:--";
            labVentTimeTarget.Text  = "--:--:--";

            labPlasmaSetpoint.Text  = "";
            labPlasmaTime.Text      = "--:--:--";
            labPlasmaTimeTarget.Text = "--:--:--";

            labGasMFC1.Text = "";
            labGasMFC2.Text = "";
            labGasMFC3.Text = "";
            //    labGasMode.Text             = "";
            labGasSetpointPressure.Text = "";
            labGasTime.Text             = "--:--:--";
            labGasTimeTarget.Text       = "--:--:--";
            labGasVaporiser.Text        = "";

            labMotorsState.Text         = "";
            labMotorTime.Text           = "--:--:--";
            labMotorTimeTarget.Text     = "--:--:--";


            panelGas.Enabled = false;
            panelPump.Enabled = false;
            panelPurge.Enabled = false;
            panelVent.Enabled = false;
            panelPlasma.Enabled = false;
            panelMotor.Enabled = false;
            
        }
        //---------------------------------------------------------------------------------------
        //WYnus odswiezenie listy programow w controlce comboBox
        public void RefreshProgram()
        {
            flagRefreshProgram = true;
        }
        //---------------------------------------------------------------------------------------
        //Odsiwiez dane na temat programow
        private void UpdateListPrograms()
        {
            if (hpt1000 != null)
            {
                int selectedIndex = cBoxPrograms.SelectedIndex;

                cBoxPrograms.Items.Clear();
                foreach (Program pr in hpt1000.GetPrograms())
                    cBoxPrograms.Items.Add(pr);

                cBoxPrograms.Refresh();
                if (cBoxPrograms.Items.Count > selectedIndex)
                    cBoxPrograms.SelectedIndex = selectedIndex;
            }
        }
        //---------------------------------------------------------------------------------------
        //Odsiwiez dane na temat aktualnie wykonywanego programu w PLC. Wyswietl dane takze po zakonczeniu programu
        public void RefreshPanel()
        {
            Program     aActualPrgoram    = null;
            Subprogram  aActualSubprogram = null;
            bool        aLockPanel        = false; //falga okresla czym blokwoac wybor progrmau i subprogramu

            if (hpt1000 != null)
            {
                //ustaw tryb pracy ale ze zwloka aby mozna go zdarzyc odczytac z PLC
                if (timerWaitMode > timeWaitRefreshMode)
                {
                    if (hpt1000.GetMode() == Types.Mode.Automatic)
                        rBtnAutomatic.Checked = true;
                    if (hpt1000.GetMode() == Types.Mode.Manual)
                        rBtnManual.Checked = true;
                    if (hpt1000.GetMode() != Types.Mode.Automatic && hpt1000.GetMode() != Types.Mode.Manual)
                        rBtnHide.Checked = true;
              
                }
				if(timerWaitMode <= timeWaitRefreshMode)
                	timerWaitMode++;
                //ustaw aktualny program jako ten wybrany z ComboBoxa
                if (cBoxPrograms.SelectedItem != null)
                    aActualPrgoram = (Program)cBoxPrograms.SelectedItem;

                //ustaw aktualny subproram jako ten wybrany z listy
                if (listViewSubprograms.SelectedItems.Count > 0)
                    aActualSubprogram = (Subprogram)listViewSubprograms.SelectedItems[0].Tag;

                //Sprawdz czy jest zaladowany do PLC jakis program lub czy nie wykonuje sie jakis program. Jezeli tak to ustaw go jako aktualny
                foreach (Program program in hpt1000.GetPrograms())
                {
                    if (program.IsRun() || (program.ExistInPLC && !actualProgramWasSet))
                    {
                        aActualPrgoram = program;
                        //Znajdz aktualnie wykonywany subprogram wgrany do PLC z programu
                        aActualSubprogram = aActualPrgoram.GetActualSubprogram();
                        //Ustaw combobox na danym prgramie
                        SetProgramInComboBox(aActualPrgoram);

                        aLockPanel = true;
                        actualProgramWasSet = true;
                        break;
                    }
                }
                //Wyswietl dane na temat programy=u i subprogrmau pod warunieime ze jestem w trybie autoamtic
                if (hpt1000.GetMode() == Types.Mode.Automatic)
                {
                    ShowProgramConfig(aActualPrgoram);
                    ShowSubprogramConfig(aActualSubprogram);
                    labStatus.Enabled = true;
                }
                else//Wyczysc panel jezeli nie jestem w trybie autoamtic
                {
                    ClearPanel(true);
                    aLockPanel = true;
                    labStatus.Enabled = false;
                }
                //Ustaw dostepnosc wyboru programu i subprogramu do sprawdzenia ihc parametrow
                cBoxPrograms.Enabled        = !aLockPanel;
  //              listViewSubprograms.Enabled = !aLockPanel;
                btnStart.Enabled            = !aLockPanel;
                labProgram.Enabled          = !aLockPanel;
                labStatus1.Enabled           = !aLockPanel;

                if (hpt1000.GetMode() == Types.Mode.Automatic)
                    btnStop.Enabled = true;
                else
                    btnStop.Enabled = false;

                //brak mozliwosci zmiany trybu na manual gdy jest uruchomony program
                if (cBoxPrograms.SelectedItem != null && ((Program)cBoxPrograms.SelectedItem).Status == Types.StatusProgram.Run)
                    rBtnManual.Enabled = false; 
                else
                    rBtnManual.Enabled = true;
                //Kolorowanie itempow w zaleznosci od statusu Subprogramu
                ShowColorStatusSubprogram();
            }
        }
        //--------------------------------------------------------------------------------------
        //Ustaw dostpenosc funkcji w zaleznosci od zalogowanego usera
        public void ShowComponetsPersmission(User user)
        {
            if (user != null && hpt1000 != null)
            {
                //Tryb manualny dostepny tylko dla administratora
                if (user.Privilige != Types.UserPrivilige.Operator)
                {
                    rBtnManual.Visible = true;
                }
                else 
                {
                    rBtnManual.Visible = false;
                }
            }
        }
        //--------------------------------------------------------------------------------------
        void SetProgramInComboBox(Program program)
        {
            for(int i = 0; i < cBoxPrograms.Items.Count; i++)
            {
                if (cBoxPrograms.Items[i] == program)
                    cBoxPrograms.SelectedIndex = i;
            }
        }
        //--------------------------------------------------------------------------------------
        //Pokaz dane programu
        void ShowProgramConfig(Program pr)
        {
            if (pr != null)
            {
                //Podaj mi dla ktorych subprogramow mam tworzyc liste. Czy user przeglada parametry czy wyswietlam dane z PLC aktulanie wykonywanego prgoramu
                List<Subprogram> aSubprograms = pr.GetSubprograms();
                /*    if (pr.IsRun())
                        aSubprograms = pr.GetSubprogramsPLC();
                */
                labStatus.Text = pr.Status.ToString().ToUpper();
                labStatus.ForeColor = Color.Black;
                if (pr.Status == Types.StatusProgram.Error)
                    labStatus.ForeColor = Color.Red;
                if (pr.Status == Types.StatusProgram.Run)
                    labStatus.ForeColor = Color.Green;

                //uzupelnij liste sub programow. Jezeli jest mniej w listView to dodaj jezeli jest za duzo to usun
                if (aSubprograms != null)
                {
                    //ListView zawiera za malo wpisow to je utworz
                    int aCountNewItem = aSubprograms.Count - listViewSubprograms.Items.Count;
                    AddItem(aCountNewItem);
                    //ListView zawiera za duzo wpisow to je usun
                    int aCountRemoveItem = listViewSubprograms.Items.Count - aSubprograms.Count;
                    RemoveItem(aCountRemoveItem);
                    //Aktualizuj dane dla kolejnych itemow (subprogramow)
                    int i = 0;
                    foreach (Subprogram subPr in aSubprograms)
                    {
                        if (listViewSubprograms.Items.Count > i)
                        {
                            ListViewItem item = listViewSubprograms.Items[i];
                            if(item.Text != subPr.GetName())
                                item.Text   = subPr.GetName();
                            if(item.SubItems.Count > 1 && item.SubItems[1].Text != subPr.GetStatus().ToString())
                                item.SubItems[1].Text = subPr.GetStatus().ToString();
                            item.Tag    = subPr;
                            i++;
                        }
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------
        //Kolorowanie itempow w zaleznosci od statusu Subprogramu
        private void ShowColorStatusSubprogram()
        {
            for(int i = 0; i < listViewSubprograms.Items.Count; i++)
            {
                ListViewItem item = listViewSubprograms.Items[i];
                Subprogram subprogram = (Subprogram)item.Tag;
                if (subprogram != null)
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                    switch (subprogram.GetStatus())
                    {
                        case Types.StatusProgram.Done:
                            item.ForeColor = Color.Blue;
                            break;
                          case Types.StatusProgram.Error:
                            item.ForeColor = Color.Red;
                            break;
                        case Types.StatusProgram.Run:
                            item.ForeColor = Color.White;
                            item.BackColor = Color.DodgerBlue;
                            break;
                        case Types.StatusProgram.Wait:
                            item.ForeColor = Color.Orange;
                            break;
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------
        void AddItem(int aCount)
        {
            for (int i = 0; i < aCount; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = "New item";
                item.SubItems.Add("New subitem");
                item.Tag = null;
                listViewSubprograms.Items.Add(item);
            }
        }
        //--------------------------------------------------------------------------------------
        void RemoveItem(int aCount)
        {
            for (int i = 0; i < aCount; i++)
            {
                if (listViewSubprograms.Items.Count > 0)
                    listViewSubprograms.Items.Remove(listViewSubprograms.Items[0]);
            }
        }
        //--------------------------------------------------------------------------------------
        //Pokaz dane subprogramu
        void ShowSubprogramConfig(Subprogram subProgram)
        {
            fBlockFunAction = true;
            if (subProgram != null)
            {
                //podswietl aktualnie prezentowany sub program
                foreach(ListViewItem item in listViewSubprograms.Items)
                {
                    if (item.Tag == subProgram)
                        item.Selected = true;
                }
                ShowPumpProces(subProgram.GetPumpProces());
                ShowPlasmaProces(subProgram.GetPlasmaProces());
                ShowPurgeProces(subProgram.GetPurgeProces());
                ShowVentProces(subProgram.GetVentProces());
                ShowGasProces(subProgram.GetGasProces());
                ShowMotorProces(subProgram.GetMotorProces());
            }
            fBlockFunAction = false;
        }
        //--------------------------------------------------------------------------------------
        //Odsiwez dane na temat procesu pompowania. Pamietaj aby odswiezac dane tylko wtedy gdy sie cos zmienilo
        void ShowPumpProces(PumpProces pumpProces)
        {
            if (pumpProces != null)
            {
        //        if (pumpProces.Active && !panelPump.Font.Bold)
        //            panelPump.Font = new Font(panelPump.Font, FontStyle.Bold);
        //        if (!pumpProces.Active && panelPump.Font.Bold)
       //             panelPump.Font = new Font(panelPump.Font, FontStyle.Regular);

                if (panelPump.Enabled != pumpProces.Active)
                    panelPump.Enabled = pumpProces.Active;

                labPumpTime.Text        = pumpProces.TimeWorking.ToString("HH:mm:ss");
                labPumpTimeTarget.Text = pumpProces.GetTimeWaitForPumpDown().ToString("HH:mm:ss");
                labPumpSetpoint.Text    = "Setpoint - " + pumpProces.GetSetpoint().ToString("F3") + " mBar";
            }
        }
        //--------------------------------------------------------------------------------------
        //Odsiwez dane na temat procesu purgowania. Pamietaj aby odswiezac dane tylko wtedy gdy sie cos zmienilo
        void ShowPurgeProces(FlushProces purgeProces)
        {
            if (purgeProces != null)
            {
                if (panelPurge.Enabled != purgeProces.Active)
                    panelPurge.Enabled = purgeProces.Active;

        //        if (purgeProces.Active && !panelPump.Font.Bold)
        //            panelPurge.Font = new Font(panelPurge.Font, FontStyle.Bold);

       //         if (!purgeProces.Active && panelPump.Font.Bold)
      //              panelPurge.Font = new Font(panelPurge.Font, FontStyle.Regular);

                labPurgeTime.Text       = purgeProces.TimeWorking.ToString("HH:mm:ss");
                labPurgeTimeTarget.Text = purgeProces.GetTimePurge().ToString("HH:mm:ss");
            }
        }
        //--------------------------------------------------------------------------------------
        //Odsiwez dane na temat procesu ventowania. Pamietaj aby odswiezac dane tylko wtedy gdy sie cos zmienilo
        void ShowVentProces(VentProces ventProces)
        {
            if (ventProces != null)
            {
                if(panelVent.Enabled != ventProces.Active)
                    panelVent.Enabled = ventProces.Active;

      //          if (ventProces.Active && !panelVent.Font.Bold)
      //              panelVent.Font = new Font(panelVent.Font, FontStyle.Bold);

      //          if (!ventProces.Active && panelVent.Font.Bold)
     //               panelVent.Font = new Font(panelVent.Font, FontStyle.Regular);

                labVentTime.Text        = ventProces.TimeWorking.ToString("HH:mm:ss");
                labVentTimeTarget.Text  = ventProces.GetTimeVent().ToString("HH:mm:ss");
            }
        }
        //--------------------------------------------------------------------------------------
        public ProcessInformationForm GetProcessInfoForm()
        {
            return this.procesInfoForm;
        }
        //--------------------------------------------------------------------------------------
        //Odsiwez dane na temat procesu plasmy. Pamietaj aby odswiezac dane tylko wtedy gdy sie cos zmienilo
        void ShowPlasmaProces(PlasmaProces plasmaProces)
        {
            if (plasmaProces != null)
            {
                if(panelPlasma.Enabled != plasmaProces.Active)
                    panelPlasma.Enabled = plasmaProces.Active;

                string aUnit = "";
                if (plasmaProces.GetWorkMode() == Types.WorkModeHV.Curent) aUnit    = " A";
                if (plasmaProces.GetWorkMode() == Types.WorkModeHV.Voltage) aUnit   = " V";
                if (plasmaProces.GetWorkMode() == Types.WorkModeHV.Power) aUnit     = " %";

       //         if (plasmaProces.Active && !panelPlasma.Font.Bold)
       //             panelPlasma.Font = new Font(panelPlasma.Font, FontStyle.Bold);

       //         if (!plasmaProces.Active && panelPlasma.Font.Bold)
      //              panelPlasma.Font = new Font(panelPlasma.Font, FontStyle.Regular);

                labPlasmaTime.Text = plasmaProces.TimeWorking.ToString("HH:mm:ss");
                labPlasmaTimeTarget.Text = plasmaProces.GetTimeOperate().ToString("HH:mm:ss");
                labPlasmaSetpoint.Text = "Setpoint - " + plasmaProces.GetSetpointPercent().ToString("F0") + aUnit;
            }
        }
        //--------------------------------------------------------------------------------------
        //Odsiwez dane na temat procesu dozowania gazu. Pamietaj aby odswiezac dane tylko wtedy gdy sie cos zmienilo
        void ShowGasProces(GasProces gasProces)
        {
            if (gasProces != null)
            {
                if(panelGas.Enabled != gasProces.Active)
                    panelGas.Enabled = gasProces.Active;

      //          if (gasProces.Active && !panelGas.Font.Bold)
     //               panelGas.Font = new Font(panelGas.Font, FontStyle.Bold);

       //         if (!gasProces.Active && panelGas.Font.Bold)
      //              panelGas.Font = new Font(panelGas.Font, FontStyle.Regular);

                labGasTime.Text         = gasProces.TimeWorking.ToString("HH:mm:ss");
                labGasTimeTarget.Text   = gasProces.GetTimeProcesDuration().ToString("HH:mm:ss");

                labGasSetpointPressure.Visible  = true;
       //       labGasVaporiser.Visible         = true;
                switch (gasProces.GetModeProces())
                {
                    case Types.GasProcesMode.FlowSP:
                        //labGasMode.Text = "";//"Mode: Dosing gases to chamber accordnig set flow";
                        labGasSetpointPressure.Visible = false;
                        labGasMFC1.Text = "MFC 1: " + gasProces.GetGasFlow(1).ToString() + " sccm";
                        labGasMFC2.Text = "MFC 2: " + gasProces.GetGasFlow(2).ToString() + " sccm";
                        labGasMFC3.Text = "MFC 3: " + gasProces.GetGasFlow(3).ToString() + " sccm";
                        if(Vaporizer.GetTypeVaporaizer() == Types.VaporaizerType.Cycle)
                            labGasVaporiser.Text = "Vaporiser: Cycle time - " + gasProces.GetCycleTime().ToString() + " ms " + " On time - " + gasProces.GetOnTime().ToString() + " %";
                        if (Vaporizer.GetTypeVaporaizer() == Types.VaporaizerType.Dosing)
                            labGasVaporiser.Text = "Vaporiser dosing - " + gasProces.GetDosing().ToString() + " uL/min";
                        labGasVaporiser.Visible = gasProces.GetVaporiserActive();
                        break;
                    case Types.GasProcesMode.Pressure_Vap:
                      //  labGasMode.Text = "";// "Mode: Control pressure via vaporiator";
                        labGasSetpointPressure.Text = "Setpoint - " + gasProces.GetSetpointPressure().ToString("F3") + " mBar";
                        labGasMFC1.Text = "MFC 1: 0 %";
                        labGasMFC2.Text = "MFC 2: 0 %";
                        labGasMFC3.Text = "MFC 3: 0 %";
                        labGasVaporiser.Text = "Vaporiser: 100 %";
                        labGasVaporiser.Visible = true;
                        break;
                    case Types.GasProcesMode.Presure_MFC:
                     //   labGasMode.Text = "";// "Mode: Control pressure via mas flow control";
                        labGasSetpointPressure.Text = "Setpoint - " + gasProces.GetSetpointPressure().ToString("F3") + " mBar";
                        labGasMFC1.Text = "MFC 1: " + gasProces.GetShareGas(1).ToString() + " %";
                        labGasMFC2.Text = "MFC 2: " + gasProces.GetShareGas(2).ToString() + " %";
                        labGasMFC3.Text = "MFC 3: " + gasProces.GetShareGas(3).ToString() + " %";
                        labGasVaporiser.Text = "Vaporiser: 0 %";
                        labGasVaporiser.Visible = false;
                        break;
                }
                labGasMFC1.Visible = gasProces.GetActiveFlow(1);
                labGasMFC2.Visible = gasProces.GetActiveFlow(2);
                labGasMFC3.Visible = gasProces.GetActiveFlow(3);         
            }
        }
        //--------------------------------------------------------------------------------------
        //Odsiwez dane na temat procesu mtora. Pamietaj aby odswiezac dane tylko wtedy gdy sie cos zmienilo
        void ShowMotorProces(MotorProces motorProces)
        {
            if (motorProces != null)
            {
                if (panelMotor.Enabled != motorProces.Active)
                    panelMotor.Enabled = motorProces.Active;

           //     if (motorProces.Active && !panelMotor.Font.Bold)
           //         panelMotor.Font = new Font(panelMotor.Font, FontStyle.Bold);

           //     if (!motorProces.Active && panelMotor.Font.Bold)
           //         panelMotor.Font = new Font(panelMotor.Font, FontStyle.Regular);

                string aTime    = "00:00:00";
                string aState   = "";
                if (motorProces.GetActive(1) && !motorProces.GetActive(2) && motorProces.Active)
                {
                    aTime = motorProces.GetTimeMotor(1).ToString("HH:mm:ss");
                    aState = "Motor 1 - " + motorProces.GetState(1).ToString();
                }
                if (!motorProces.GetActive(1) && motorProces.GetActive(2) && motorProces.Active)
                {
                    aTime = motorProces.GetTimeMotor(2).ToString("HH:mm:ss");
                    aState = "Motor 2 - " + motorProces.GetState(2).ToString();
                }
                if (motorProces.GetActive(1) && motorProces.GetActive(2) && motorProces.Active)
                {
                    if (motorProces.GetTimeMotor(1) > motorProces.GetTimeMotor(2))
                        aTime = motorProces.GetTimeMotor(1).ToString("HH:mm:ss");
                    else
                        aTime = motorProces.GetTimeMotor(2).ToString("HH:mm:ss");
                    aState = "Motors: 1 - " + motorProces.StateMotor1_Read.ToString() + " 2 - " + motorProces.StateMotor2_Read.ToString();
                }

                labMotorTime.Text       = motorProces.TimeWorking.ToString("HH:mm:ss");
                labMotorTimeTarget.Text = aTime;
                labMotorsState.Text     = aState;
            }
        }
        //--------------------------------------------------------------------------------------
        private void cBoxPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!fBlockFunAction)
            {
                Program program = (Program)cBoxPrograms.SelectedItem;
                ShowProgramConfig(program);
            }
        }
        //--------------------------------------------------------------------------------------
        private void listViewSubprograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewSubprograms.SelectedItems.Count > 0 && !fBlockFunAction)
            {
                Subprogram subProgram = (Subprogram)listViewSubprograms.SelectedItems[0].Tag;
                ShowSubprogramConfig(subProgram);
            }
        }
        //--------------------------------------------------------------------------------------
        //Wgraj program do PLC oraz uruchom go
        private void btnStart_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();
            Program program = (Program)cBoxPrograms.SelectedItem;
            if (program != null)
            {
                //Pokaz okno gdzie user moze wprowadzic dodatkowe dane na temat wykonywanego procesu
                DialogResult result = MessageBox.Show("Do you want to enter additional information about the process ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    this.procesInfoForm.ShowDialog();
                else
                    this.procesInfoForm.Clear();
                program.SetProcessInformation(this.procesInfoForm.GetProcessInformation());
                //Wystartuj program w PLC
                aErr = program.StartProgram();
            }
            else
                aErr.SetErrorApp(Types.EventType.NO_SELECT_PROGRAM_TO_RUN);
            //Zapamietaj ostatnio wyslany program do PLC - pamietaj ze na nasze potzreby narazie wystarczy jego referenacja ale poznij sienie dziw ze cos sie w nim zmienia
            if (!aErr.IsError())
                ApplicationData.LastLoadProgramToPLC = program;

            Logger.AddError(aErr);
        }
        //--------------------------------------------------------------------------------------
        private void btnStop_Click(object sender, EventArgs e)
        {
            ItemLogger aErr;

            Program program = Factory.CreateProgram(0);
            program.SetPtrPLC(hpt1000.GetPLC());
            aErr = program.StopProgram();

            Logger.AddError(aErr);
        }
        //--------------------------------------------------------------------------------------
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            //Z uwagi na fakt ze nie mozna odswiez komponentow graficnzych z innego watku niz w ktorycm zostaly one utworzone dlatego odswiezam to przez Timer i flage
            if (flagRefreshProgram)
            {
                UpdateListPrograms();
                flagRefreshProgram = false;
            }
        }
        //--------------------------------------------------------------------------------------
        private void rBtnMode_Click(object sender, EventArgs e)
        {
            Types.Mode aMode = Types.Mode.None;

            if (rBtnAutomatic.Checked)
                aMode = Types.Mode.Automatic;
            if (rBtnManual.Checked)
                aMode = Types.Mode.Manual;

            if (hpt1000 != null)
                hpt1000.SetMode(aMode);

            timerWaitMode = 0;
        }
        //--------------------------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Getting the graphics object
            Graphics g = pevent.Graphics;

            // Creating the rectangle for the gradient
            Rectangle rBackground = new Rectangle(0, 0, this.Width, this.Height);

            // Creating the lineargradient
            LinearGradientBrush bBackground = new LinearGradientBrush( rBackground,_Color1, _Color2, LinearGradientMode.BackwardDiagonal);
        
            // Draw the gradient onto the form
            g.FillRectangle(bBackground, rBackground);

            // Disposing of the resources held by the brush
            bBackground.Dispose();
        }
        //--------------------------------------------------------------------------------------

        private void btnStart_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnStart.ClientRectangle,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset);
        }
        //--------------------------------------------------------------------------------------
        private void btnStop_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnStop.ClientRectangle,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 4, ButtonBorderStyle.Outset);
        }
        //--------------------------------------------------------------------------------------
        private void listViewSubprograms_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont = new Font("Microsoft Sans Serif", 11, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }

                // Draw the background for an unselected item.
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.LightGray, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                e.DrawText();
            }
        }
        //--------------------------------------------------------------------------------------
        private void listViewSubprograms_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                // Draw the background and focus rectangle for a selected item.
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                e.DrawFocusRectangle();
            }
            else
            {
                // Draw the background for an unselected item.
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Gray, LinearGradientMode.Horizontal))
                {
                    //   e.Graphics.FillRectangle(brush, e.Bounds);
                }
            } 
        }
        //--------------------------------------------------------------------------------------
        private void listViewSubprograms_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.Left;

            using (StringFormat sf = new StringFormat())
            {
                // Store the column text alignment, letting it default
                // to Left if it has not been set to Center or Right.
                switch (e.Header.TextAlign)
                {
                    case HorizontalAlignment.Center:
                        sf.Alignment = StringAlignment.Center;
                        flags = TextFormatFlags.HorizontalCenter;
                        break;
                    case HorizontalAlignment.Right:
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                        break;
                }

                // Draw the subitem text in red to highlight it. 
                if (((ListView)sender).Name == "listViewErrors")
                    e.Graphics.DrawString(e.SubItem.Text, listViewSubprograms.Font, Brushes.Red, e.Bounds, sf);
                else
                    e.Graphics.DrawString(e.SubItem.Text, listViewSubprograms.Font, Brushes.Black, e.Bounds, sf);
            }
        }
        //--------------------------------------------------------------------------------------
        private void listViewSubprograms_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //Brak mozliwosci zmiany itemow podczas gdy program jest uruchomiony
            Program program = (Program)cBoxPrograms.SelectedItem;
            if (program != null && program.Status == Types.StatusProgram.Run)
                e.Item.Selected = false;
        }
        //--------------------------------------------------------------------------------------
    }

}
