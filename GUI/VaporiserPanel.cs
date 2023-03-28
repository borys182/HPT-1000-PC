using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;
using HPT1000.Source;

namespace HPT1000.GUI
{
    /// <summary>
    /// Klasa reprezentuje wizulna forme zaworu szybkiego dozowania gazu. Mamy mozliwosc sterowania dozowaniem danej ilosci gazu badz ustawaine gazu jako PWM
    /// </summary>

    public partial class VaporiserPanel : UserControl
    {
        Vaporizer vaporizer     = null;     ///<Referencja obiektu pozwalajaca komunikowac sie z urzadzenim
        bool      unitPercent   = true;     ///<Flaga okresla czy mam pokazywac jednostki w procentach czy ms

        int timeWaitOnRefresh   = 5;       ///<Zwloka czasowa pozwalajaca odswiezy wartosci w komponetanch dopiero po powenym czasie od ich wproawdzenia.
        int timerRefresh        = 0;        ///<
      
            //-----------------------------------------------------------------------------------
        /**
         * Konstruktor
         */ 
        public VaporiserPanel()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------------------------------
        /**
         * Funkcja ustawia referencje obiektu zaworu ktorym bedziemy sterowac
         */
        public void SetVaporizerPtr(Vaporizer vapPtr)
        {
            vaporizer = vapPtr;
        }
        //-----------------------------------------------------------------------------------
        /**
         * Funkcja odswieza nastawy urzadzenia odczytane bezposrednio z fizycznego urzadzenia
         */
        public void RefreshData()
        {
            //Jezeli minal czas zwloki na odswiezanie to pokaz aktulane wartosci
            if (vaporizer != null && timerRefresh > timeWaitOnRefresh)
            {
                dEditCycleTImne.Value       = (double)vaporizer.GetCycleTime() / 1000.0; //Pokaz wartosci wyrazone w ms
                btnUpDownCycleTime.Value    = (double)vaporizer.GetCycleTime() / 1000.0;

                dEditDosing.Value           = vaporizer.GetDosing();
                btnUpDownDosing.Value       = vaporizer.GetDosing();

                //Pokaz wartpsc czasu wlaczenia zaworu wcyklu wyrazona w zalneznosci od wybranej jednostki
                if (unitPercent)
                {
                    dEditOnTime.Value       = vaporizer.GetOnTime(Types.UnitFlow.percent);
                    btnUpDownOnTime.Value   = vaporizer.GetOnTime(Types.UnitFlow.percent);
                }
                else
                {
                    dEditOnTime.Value       = (double)vaporizer.GetOnTime(Types.UnitFlow.ms) / 1000.0;
                    btnUpDownOnTime.Value   = (double)vaporizer.GetOnTime(Types.UnitFlow.ms) / 1000.0;
                }
                cBoxGasControl.Checked = vaporizer.ControlOn;
                if (vaporizer.ControlOn)
                    cBoxGasControl.Image = Properties.Resources.Interlock_ON;
                else
                    cBoxGasControl.Image = Properties.Resources.interlock_OFF;
            }

            if (timerRefresh <= timeWaitOnRefresh)
                timerRefresh++;

            RefreshUnit();
            RefreshPanel();//Dostosuj panel do danego typu vaporaizera
            //Dostisuj panel do uprawnien usera
            AdjustPanelToPriviligesOfUser();
        }
        //-----------------------------------------------------------------------------------
        /**
         * Odswiez labael z jednostkamii
         */
        private void RefreshUnit()
        {
            if (unitPercent)
            {
                labUnit.Text             = "[%]";
           //     dEditOnTime.Mask         = "{0:F2}";
                dEditOnTime.MaximumValue = 100;
                btnUpDownOnTime.Maximum  = 100;
            }
            else
            {
                labUnit.Text             = "[sec]";
            //    dEditOnTime.Mask         = "{0:F3}";
                dEditOnTime.MaximumValue = 100000;
                btnUpDownOnTime.Maximum  = 100000;
            }
        }
        //-----------------------------------------------------------------------------------
        /**
          * Pokaz panel zaworu dostosowany graficznie do jego typu
          */
        private void RefreshPanel()
        {
            bool aTypeCycle  = false;
            bool aTypeDosing = false;

            if(vaporizer != null)
            {              
                if ((int)vaporizer.GetDosing() >= scrolDosing.Minimum && (int)vaporizer.GetDosing() <= scrolDosing.Maximum)
                    scrolDosing.Value = (int)vaporizer.GetDosing();

                if(Vaporizer.GetTypeVaporaizer() == Types.VaporaizerType.Cycle)
                    aTypeCycle = true;
                if(Vaporizer.GetTypeVaporaizer() == Types.VaporaizerType.Dosing)
                    aTypeDosing = true;
            }

            labCycle.Visible        = aTypeCycle;
            labOnTime.Visible       = aTypeCycle;
            labUnit.Visible         = aTypeCycle;
            labUnitCycle.Visible    = aTypeCycle;
            dEditCycleTImne.Visible = aTypeCycle;
            dEditOnTime.Visible     = aTypeCycle;
            btnUpDownCycleTime.Visible = aTypeCycle;
            btnUpDownOnTime.Visible = aTypeCycle;

            labDosing.Visible       = aTypeDosing;
            labUnitDosing.Visible   = aTypeDosing;
            scrolDosing.Visible     = aTypeDosing;
            dEditDosing.Visible     = aTypeDosing;
            btnUpDownDosing.Visible = aTypeDosing;

        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dostosowanie panelu do uprawnien usera
         */
        private void AdjustPanelToPriviligesOfUser()
        {
            //W zaleznosci od trybu pracy maszyny pokaz odpowiednie komponnety
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator)
            {
                //Ustaw widocznosc komponentow w zaleznosci o trybu sterowania gazem
                bool enable = true;
                if (Factory.GasProcessMode == Types.GasProcesMode.Pressure_Vap)
                    enable = false;
                foreach (Control ctr in groupBox1.Controls)
                {
                    if (ctr.Enabled != enable && Enabled)
                        ctr.Enabled = enable;
                }
                if (Factory.GasProcessMode == Types.GasProcesMode.Pressure_Vap && !cBoxGasControl.Enabled)
                    cBoxGasControl.Enabled = true;

            }
            //Zablokuj mozliwosc sterowania czymkolkwiek dla Operatora
            if (ApplicationData.UserChanged)
            {
                bool state = true;
                if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator)
                    state = false;

                dEditCycleTImne.Enabled     = state;
                dEditDosing.Enabled         = state;
                dEditOnTime.Enabled         = state;
                cBoxGasControl.Enabled      = state;
                btnUpDownCycleTime.Enabled  = state;
                btnUpDownDosing.Enabled     = state;
                btnUpDownOnTime.Enabled     = state;
            }
        }
        //-----------------------------------------------------------------------------------
        /**
         * Zmiana jednostek czasu wlaczenia zaworu w trybie Cycle
         */
        private void labUnit_Click(object sender, EventArgs e)
        {
            unitPercent = !unitPercent;
            RefreshUnit();
        }
        //-----------------------------------------------------------------------------------
        /**
          * Obsluga zdarzenia ustawiania wartosci czasu wlaczenia cyklu pochodzaca ze zmiany wartosci dEdit
          */
        private bool dEditOnTime_EnterOn()
        {
            bool aRes = false;
            ItemLogger aErr = new ItemLogger();

            if (vaporizer != null)
            {
                float           aValue = (float)dEditOnTime.Value;
                Types.UnitFlow  aUnit  = Types.UnitFlow.ms;

                if (unitPercent) aUnit = Types.UnitFlow.percent;
                else aValue *= 1000; //przlicz s na ms

                aErr = vaporizer.SetOnTime(aValue , aUnit);
            }

            btnUpDownOnTime.Value = (float)dEditOnTime.Value;
            Logger.AddError(aErr);

            if (!aErr.IsError())
            {
                aRes = true;
                timerRefresh = 0;
            }
            return aRes;
        }
        //-----------------------------------------------------------------------------------
        /**
          * Obsluga zdarzenia ustawiania wartosci czasu cyklu pochodzaca ze zmiany wartosci dEdit
          */
        private bool dEditCycleTImne_EnterOn()
        {
            bool aRes = false;
            ItemLogger aErr = new ItemLogger();
            if (vaporizer != null)
            {
                aErr = vaporizer.SetCycleTime((float)dEditCycleTImne.Value * 1000); // poniewa z formatki dane wprowadzam w sec musze je przeliczyc na ms
                Logger.AddError(aErr);
            }

            btnUpDownCycleTime.Value = (float)dEditCycleTImne.Value;

            if (!aErr.IsError())
            {
                aRes = true;
                timerRefresh = 0;
            }
            return aRes;
        }
        //-----------------------------------------------------------------------------------
        /**
          * Zmiana widoku myszki podczas przechodzenia nad labelem pozwalajacym zmienic jednostke
          */
        private void labUnit_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        //-----------------------------------------------------------------------------------
        /**
          * Zmiana widoku myszki podczas przechodzenia nad innymi labelem nie pozwalajacym zmienic jednostki
          */
        private void labUnit_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        //-----------------------------------------------------------------------------------
        /**
          * Obsluga zdarzenia ustawiania wartosci czasu cyklu pochodzaca ze zmiany wartosci dEdit
          */
        private bool dEditDosing_EnterOn()
        {
            bool aRes = false;
            if (vaporizer != null)
            {
                int dosing = (int)dEditDosing.Value;
                if (!vaporizer.SetDosing(dosing).IsError())
                {
                    if(dosing >= scrolDosing.Minimum && dosing <= scrolDosing.Minimum)
                        scrolDosing.Value = dosing;
                    aRes = true;
                }
            }

            btnUpDownDosing.Value = (float)dEditDosing.Value;

            return aRes;
        }
        //-----------------------------------------------------------------------------------
        /**
          *Zmiana wartosci dozowania gazu pochodaca ze zdarzenia zmiany wartosci scrollbar 
          */
        private void scrolDosing_ValueChanged(object sender, EventArgs e)
        {
            if (vaporizer != null)
            {
                if (!vaporizer.SetDosing(scrolDosing.Value).IsError())
                    dEditDosing.Value = scrolDosing.Value;
            }          
        }
        //-----------------------------------------------------------------------------------
        /**
         * Metoda ustawia czas cyklu na zdarzenie pochodzce z przycisku strzalek
         */
        private void btnUpDownCycleTime_ValueChanged(object sender, EventArgs e)
        {
            dEditCycleTImne.Value = btnUpDownCycleTime.Value;

            dEditCycleTImne.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));

        }
        //-----------------------------------------------------------------------------------
        /**
         * Metoda ustawia czas wlaczenia zaworu na zdarzenie pochodzce z przycisku strzalek
         */
        private void btnUpDownOnTime_ValueChanged(object sender, EventArgs e)
        {
            dEditOnTime.Value = btnUpDownOnTime.Value;

            dEditOnTime.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------
        private void btnUpDownDosing_ValueChanged(object sender, EventArgs e)
        /**
          * Metoda ustawia dozowanie gazu na zdarzenie pochodzce z przycisku strzalek
          */
        {
            dEditDosing.Value = btnUpDownDosing.Value;

            dEditDosing.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------
        private void cBoxGasControl_Click(object sender, EventArgs e)
        {
            if (vaporizer != null)
                vaporizer.SetState(cBoxGasControl.Checked);
        }
        //-----------------------------------------------------------------------------------
    }
}
