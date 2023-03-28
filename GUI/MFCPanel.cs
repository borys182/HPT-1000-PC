using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;


namespace HPT1000.GUI
{
    /// <summary>
    /// Klasa jest odpowiedzialna za sterowanie reczne przeplywem gazu. Ma mozliwosc ustawic przeplyw wyrazony w procentach badz sccm
    /// </summary>
    public partial class MFCPanel : UserControl
    {
        MFC      mfc        = null;     ///<Referencja obiektu pozwalajaca ustawic przpelyw w urzadzeniu
        GasTypes gasTypes   = null;     ///< Typ gasu kotry jest podpiety poda dana przplyweke
        int      channelId  = 0;        ///< Id kanalu MFC z ktorym jest powiazany komponent 

        int timerRefresh        = 0;    ///<Zmienn sluzy do ustawienia zwolki czasowej podczas odswiezania wartosci nastaw przpleywu aby mozna bylo spokojnie je tam wprowadzic
        int timeWaitOnRefresh   = 0;   ///3 s zwolko czasowej na odswiezeni danych po ich wprowadzeniu

        //-----------------------------------------------------------------------------------------
        /**
         * Konstruktor
         */ 
        public MFCPanel()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja odswieza ustawienia przeplywu odczytane z urzadzenia
         */ 
        public void RefreshData()
        {
            if (mfc != null)
            {
                //Jezeli minela zwloka czasowa to odswiez dane. W ten sposob daje czas na odczytanie wczesniej ustawionych wartosci
                if(timerRefresh > timeWaitOnRefresh)
                {
                    dEditFlow_sccm.Value        = mfc.GetSetpoint(channelId, Types.UnitFlow.sccm);
                    btnUpDownFlowSccm.Value     = mfc.GetSetpoint(channelId, Types.UnitFlow.sccm);
                    cBoxControlGas.Checked      = mfc.GetState(channelId);

                    if (Factory.Hpt1000.GetMode() == Types.Mode.Automatic)
                    {
                        if (Factory.Hpt1000.GetPressureControl().GetMode() == Types.GasProcesMode.Presure_MFC)
                        {
                            dEditFlow_percent.Value     = mfc.GetPercentPID(channelId);
                            btnUpDownFlowPercent.Value  = mfc.GetPercentPID(channelId);
                            btnUpDownFlowPercent.Step   = 1;
                            dEditFlow_percent.Mask      = "0";
                        }
                        else
                        {
                            btnUpDownFlowPercent.Value  = mfc.GetSetpoint(channelId, Types.UnitFlow.percent);
                            dEditFlow_percent.Value     = mfc.GetSetpoint(channelId, Types.UnitFlow.percent);
                            btnUpDownFlowPercent.Step   = 0.1;
                            dEditFlow_percent.Mask      = "0.00";
                        }
                    }
                    else
                    {
                        if (Factory.Hpt1000.GetPressureControl().GetMode() == Types.GasProcesMode.Presure_MFC)
                        {
                            dEditFlow_percent.Value = mfc.GetPercentPID(channelId);
                            btnUpDownFlowPercent.Value = mfc.GetPercentPID(channelId);
                            btnUpDownFlowPercent.Step = 1;
                            dEditFlow_percent.Mask = "0";
                        }
                        else
                        {
                            btnUpDownFlowPercent.Value = mfc.GetSetpoint(channelId, Types.UnitFlow.percent);
                            dEditFlow_percent.Value = mfc.GetSetpoint(channelId, Types.UnitFlow.percent);
                            btnUpDownFlowPercent.Step = 0.1;
                            dEditFlow_percent.Mask = "0.00";
                        }
                    }
                    if (mfc.GetState(channelId))
                        cBoxControlGas.Image = Properties.Resources.Interlock_ON;
                    else
                        cBoxControlGas.Image = Properties.Resources.interlock_OFF;
                }

                if (timerRefresh <= timeWaitOnRefresh)
                    timerRefresh++;

                ShowGasType(); //Pokaz powiazany z dana przeplywka gas
            }
            //Ustaw limity komponentow dla wprowadzania danych przeplywu
            SetLimit();
            //DOstosuj panel do uprawnieni usera
            AdjustPanelToPriviligesOfUser();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Fnkcja ma za zadanie odswiezanie listy dastepmych gazow
         */ 
        public void RefreshGasType()
        {
            if (gasTypes != null && !cBoxGasType.Focused)
            {
                //Wyczysc liste oraz utworz nowa
                cBoxGasType.Items.Clear(); 
                foreach (GasType gasType in gasTypes.Items)
                {
                    cBoxGasType.Items.Add(gasType);
                }
                ShowGasType();//Pokaz powiazany z dana przeplywka gas
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dostosowanie panelu do uprawnien usera
         */
        private void AdjustPanelToPriviligesOfUser()
        {
            //Dostosuj mozliwe do ustawienia parametry w zaleznosci od trybu sterowania gazami
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator)
                ShowCorrespondingComponent();

            if (ApplicationData.UserChanged)
            {
                bool state = true;
                if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator)
                    state = false;

                dEditFlow_percent.Enabled    = state;
                dEditFlow_sccm.Enabled       = state;
                cBoxControlGas.Enabled       = state;
                btnUpDownFlowPercent.Enabled = state;
                btnUpDownFlowSccm.Enabled    = state;
                cBoxGasType.Enabled          = state;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Dostosuj mozliwe do ustawienia parametry w zaleznosci od trybu sterowania gazami
         */
        private void ShowCorrespondingComponent()
        {
            if(Factory.GasProcessMode == Types.GasProcesMode.FlowSP)
            {
                dEditFlow_sccm.Enabled      = true;
                btnUpDownFlowSccm.Enabled   = true;
            }
            else
            {
                dEditFlow_sccm.Enabled      = false;
                btnUpDownFlowSccm.Enabled   = false;
            }
            //Zablokuj mozliwosc zmiany gazu w trybie automatic
            if (Factory.Hpt1000 != null && Factory.Hpt1000.GetMode() == Types.Mode.Automatic)
                cBoxGasType.Enabled = false;
            else
                cBoxGasType.Enabled = true;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie pokazanie typu gazu powiazanego z dana przeplywka
         */
        private void ShowGasType()
        {
            //Nie odsiwezaj gdy kist jest rozwinieta. Robie to sprawdzajac czy focusa nie posiadaja dzieci zas gdy focus jest na Parencie to mozna odswiezac
            if(mfc != null && (!cBoxGasType.ContainsFocus))
            {
                for (int i = 0; i < cBoxGasType.Items.Count; i++)
                {
                    GasType gasType = (GasType)cBoxGasType.Items[i];
                    if (gasType != null && gasType.ID == mfc.GetGasType(channelId))
                        cBoxGasType.SelectedIndex = i;
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Ustaw referencje obiektu pozawalajacego komunikowac sie z urzadzeniem
         */ 
        public void SetMFC(MFC aMFCPtr, GasTypes aGasTypes ,int aChannelID)
        {
            mfc         = aMFCPtr;
            gasTypes    = aGasTypes;

            channelId   = aChannelID;
            labNameMFC.Text = "Mass Flow Controller " + aChannelID.ToString();          
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Odczytaj limity urzadzenia i ustaw je w komponentach niepozwalajac w ten sposob ustawic za duzej wartosci
         */ 
        private void SetLimit()
        {
            if (mfc != null)
            {
                scrollFlow.Maximum          = (int)mfc.GetMaxActualFlow(channelId);
                dEditFlow_sccm.MaximumValue = (int)mfc.GetMaxActualFlow(channelId);
                btnUpDownFlowSccm.Maximum   = mfc.GetMaxActualFlow(channelId);
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia scrol bar na wartosc setpointa przeplywu
         */ 
        private void SetScrollValue(int aValue)
        {
            if (aValue <= scrollFlow.Maximum && aValue >= scrollFlow.Minimum)
                scrollFlow.Value = aValue;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zdarzenia zmiany wartosci setpointa ustawiona ze scrollbara
         */
        private void scrollFlow_ValueChanged(object sender, EventArgs e)
        {
            dEditFlow_sccm.Value = scrollFlow.Value;

            dEditFlow_sccm.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia przeplyw gazu wyrazony w sccm
         */
        private bool dEditFlow_sccm_EnterOn()
        {
            bool aRes = false;
            ItemLogger aErr = new ItemLogger();

            if (mfc != null)
                aErr = mfc.SetFlow(channelId, (int)dEditFlow_sccm.Value, Types.UnitFlow.sccm);

            if (!aErr.IsError())
            {
                aRes = true;
                timerRefresh = 0;
            }
            SetScrollValue((int)dEditFlow_sccm.Value);
            btnUpDownFlowSccm.Value = dEditFlow_sccm.Value;
            
            //Pokaz komunikat tlyko gdy jest blad
            if (aErr.IsError())
                Logger.AddError(aErr);

            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda ustawia przeplyw wyrazony w percentach
          */
        private bool dEditFlow_percent_EnterOn()
        {
            bool aRes = false;
            ItemLogger aErr = new ItemLogger();

            if (mfc != null && Factory.Hpt1000 != null && Factory.Hpt1000.GetPressureControl() != null)
            {
                //Sterowanie stabilizacja cisnienia w komorze za pomoca PID i MFC
                if(Factory.Hpt1000.GetPressureControl().GetMode() == Types.GasProcesMode.Presure_MFC)
                    aErr = mfc.SetPercentPID(channelId, (float)dEditFlow_percent.Value);
                else
                    aErr = mfc.SetFlow(channelId, (float)dEditFlow_percent.Value, Types.UnitFlow.percent);
            }

            if (!aErr.IsError())
            {
                aRes = true;
                timerRefresh = 0;
            }
            btnUpDownFlowPercent.Value = dEditFlow_percent.Value;

           Logger.AddError(aErr);

            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda ustawia dany gaz dla przeplywki
          */
        private void cBoxGasType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mfc != null)
            {
                GasType gasType = (GasType)cBoxGasType.SelectedItem;
                mfc.SetGasType(channelId, gasType.ID);
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda ustawia przplyw gazu wyrazony w sccm na zdarzenie pochodzce z przycisku strzalek
          */
        private void btnUpDownFlowSccm_ValueChanged(object sender, EventArgs e)
        {
            dEditFlow_sccm.Value = btnUpDownFlowSccm.Value;

            dEditFlow_sccm.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda ustawia przplyw gazu wyrazony w procentach na zdarzenie pochodzce z przycisku strzalek
          */
        private void btnUpDownFlowPercent_ValueChanged(object sender, EventArgs e)
        {
            dEditFlow_percent.Value = btnUpDownFlowPercent.Value;

            dEditFlow_percent.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------------
        private void cBoxControlGas_Click(object sender, EventArgs e)
        {
            if (mfc != null)
                mfc.SetState(channelId, cBoxControlGas.Checked);
        }
        //-----------------------------------------------------------------------------------------

    }
}
