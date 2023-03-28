using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Chamber;
using HPT1000.Source.Driver;
using HPT1000.Source;

namespace HPT1000.GUI
{
    /// <summary>
    /// Klasa wizualizacyjna zasilacza. Ma na celu ustawienie mocy zasilacza z glownego okna aplikacji
    /// </summary>
    public partial class GeneratorPanel : UserControl
    {
        private PowerSupplay generator          = null;     ///< Refernecja na obiekt fizyczny zasilacza pozwalajacy ustawic jego parametry w s terowniku PLC

        private const int    setpointResolution = 1000;    //zmienna okresla z jaka dokladnosci mozna podawac setopinta
        private int          timerRefreshMode   = 0;
        private int          timeWaitOnRefresh  = 6;       ///< Czekaj 3s na odsiwezenie trybu pracy po jego zminie - czas zwloki na odczyanie danych z PLC
        private bool         initFlag           = false; // Flaga okresla ze nastepuje zmiana wartosci w komponentach grafinczych w wyniuku inicjalizacji wartoaci a nie ingerencji usera

        Bitmap generator_ON     = new Bitmap(Properties.Resources.Bistable_ON);
        Bitmap generator_OFF    = new Bitmap(Properties.Resources.Bistable_OFF);
        Bitmap generator_ERR    = new Bitmap(Properties.Resources.Bistable_ERR);

        //------------------------------------------------------------------------------------------
        /**
         * Konstruktor kalsy
         */
        public GeneratorPanel()
        {
            InitializeComponent();
            scrollSetpoint.Minimum = 1; //Ustaw min setpointa dla scrolbara

            generator_ON.MakeTransparent(Color.White);
            generator_OFF.MakeTransparent(Color.White);
            generator_ERR.MakeTransparent(Color.White);

        }
        //------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia referencje fizycznego obiektu zasilacza
         */
        public void SetGeneratorPtr(PowerSupplay aGeneraotrPtr)
        {
            generator = aGeneraotrPtr;
        }
        //------------------------------------------------------------------------------------------
        /**
         * Odswie dane panelu takie jak: setpoint zasilacza oraz status
         */ 
        public void RefreshData()
        {     
            initFlag = true;

            SetLimit();
            if(generator != null)
            {
                //Po ustawieniu wartosci daj czas aby mozna bylo odczytac aktualne wartosci z PLC
                if (timerRefreshMode > timeWaitOnRefresh)
                {
                    //Przlicz wartoscv z setpointa na procenty
                    double aSetpointPercent = 0;
                    if(generator.MaxPower != 0)
                        aSetpointPercent = generator.Setpoint / generator.MaxPower * 100;

                    dEditSetpoint.Value = aSetpointPercent;
                    buttonUpDown.Value  = aSetpointPercent;

                    if (generator.State == Types.StateHV.ON)
                    {
                        cBoxOperate.Checked = true;
                        cBoxOperate.Image = generator_ON;
                    }
                    if (generator.State == Types.StateHV.OFF)
                    {
                        cBoxOperate.Checked = false;
                        cBoxOperate.Image = generator_OFF;
                    }
                    if (generator.State == Types.StateHV.Error)
                    {
                        cBoxOperate.Checked = false;
                        cBoxOperate.Image = generator_ERR;

                    }
                }
                labCurent.Text  = "Current:  "  + generator.Curent.ToString("F3") + " A";
                labVoltage.Text = "Voltage:  " + generator.Voltage.ToString("F3") + " V";
                labPower.Text   = "Power:    "   + (generator.Curent * generator.Voltage).ToString("F3") + " W";

                if (timerRefreshMode <= timeWaitOnRefresh)
                    timerRefreshMode++;
            }
            //Dostosuj panel do uprawnien usera
            AdjustPanelToPriviligesOfUser();
            initFlag = false;
        }
        //------------------------------------------------------------------------------------------
        /**
         * Dostosuj dostepnsco komponentow do trybu pracy maszyny 
         */
        private void ShowCorrespondingComponent()
        {
            //Zablokuj mozliwosc zmiany gazu w trybie automatic
            if (Factory.Hpt1000 != null && Factory.Hpt1000.GetMode() == Types.Mode.Automatic)
            {
                dEditSetpoint.Enabled = false;
                buttonUpDown.Enabled  = false;
            }
            else
            {
                dEditSetpoint.Enabled = true;
                buttonUpDown.Enabled  = true;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dostosowanie panelu do uprawnien usera
         */
        private void AdjustPanelToPriviligesOfUser()
        {
            //W zaleznosci od trybu pracy maszyny pokaz odpowiednie komponnety
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator)
                ShowCorrespondingComponent();

            if (ApplicationData.UserChanged)
            {
                bool state = true;
                if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator)
                    state = false;

                buttonUpDown.Enabled    = state;
                dEditSetpoint.Enabled   = state;
                cBoxOperate.Enabled     = state;
            }
        }
        //------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia limity dla scroll bara
         */
        private void SetLimit()
        {
            double aLimitValue = 0;

            if(generator != null)
            {
                switch (generator.Mode)
                {
                    case Types.ModeHV.Power:
                        aLimitValue = generator.LimitPower;
                        break;
                    case Types.ModeHV.Curent:
                        aLimitValue = generator.LimitCurent;
                        break;
                    case Types.ModeHV.Voltage:
                        aLimitValue = generator.LimitVoltage;
                        break;
                }
            }
      //      dEditSetpoint.MaximumValue  = aLimitValue;
            scrollSetpoint.Maximum      = (int)(aLimitValue * setpointResolution);
        }
        //------------------------------------------------------------------------------------------
        /**
         * Metoda obsluguje zdarzenia przycksiku/checkboxa wlaczajacego/wulaczajacego urzadzenie
         */ 
        private void cBoxOperate_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();

            if (generator != null)
            {
                if (generator.State == Types.StateHV.Error)
                    cBoxOperate.Checked = false;

                aErr = generator.SetOperate(cBoxOperate.Checked);
            }

            if(!aErr.IsError())
                timerRefreshMode = 0;

            if (cBoxOperate.Checked)
                cBoxOperate.Image = generator_ON;
            else
                cBoxOperate.Image = generator_OFF;

            Logger.AddError(aErr);
        }
        //------------------------------------------------------------------------------------------
        /**
         * Zmiana wartosc setpointa wymuszona poprzez scrol
         */ 
        private void SetScrollValue(double aValue)
        {
            int aValueScroll = (int)(aValue * setpointResolution);

            if (scrollSetpoint.Maximum >= aValueScroll && scrollSetpoint.Minimum <= aValueScroll)
                scrollSetpoint.Value = aValueScroll;
        }
        //------------------------------------------------------------------------------------------
        /**
         * Oblsuga zdarzenia ustawienia wartosc setpointu w komponentcie doubeEdit
         */ 
        private bool dEditSetpoint_EnterOn()
        {
            bool aRes = false;
            ItemLogger aErr = new ItemLogger();

            if (generator != null && !initFlag)
            {
                //Przelicz procenta na wartosc setpointa
                double aSetpoint = dEditSetpoint.Value / 100.0 * generator.MaxPower;

                aErr = generator.SetSetpoint(aSetpoint);
                SetScrollValue(dEditSetpoint.Value);
                //Loguj tylko w razie bledu
                if(aErr.IsError())
                    Logger.AddError(aErr);
            }
            if (!aErr.IsError())
            {
                aRes = true;
                timerRefreshMode = 0;
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------
        /**
         * Oblusdga zdarzenia zmiany wartosci setpointu z komponetu scrol
         */ 
        private void scrollSetpoint_ValueChanged(object sender, EventArgs e)
        {
            double aValue = 0;

            if (setpointResolution > 0)
                aValue = (double)(scrollSetpoint.Value) / (double)(setpointResolution);

            dEditSetpoint.Value = aValue;

            dEditSetpoint.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //------------------------------------------------------------------------------------------
        /**
         * Metoda wywolywana na zdarzenia zaminy wartosci wywoalenej poprzez przyciks Up/Down. Ustawia wartosc mocy zasilacza wyrazonej w procentach
         */ 
        private void buttonUpDown_ValueChanged(object sender, EventArgs e)
        {          
            dEditSetpoint.Value = buttonUpDown.Value;

            dEditSetpoint.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //------------------------------------------------------------------------------------------
        /**
         *  Zadaniem metody jest wyswietlenie pradu i napiecia zasilacza HV tylko dla serwisanta - 
         */
        public void ShowCurentVoltageHV()
        {
            //Na żadanie usera wracam do wyswietlania parametrow tylko dla Serwisanta
            //Zmieniono zasilacz - teraz pokazuj prad , napiecie i moc dla wszystkich uzytkownikow
            //We wczesniejeszej wersji zasilacza user nie byl informowany o parametrach zasilacza z uwagi na lekkie przeklamanie wartosci

            if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Service)
            {
                labVoltage.Visible = true;
                labCurent.Visible  = true;
                labPower.Visible   = true;
            }
            else
            {
                labVoltage.Visible = false;
                labCurent.Visible  = false;
                labPower.Visible   = false;
            }
        }
        //----------------------------------------------------------------------------------
    }
}
