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
    /// Klasa jest odpowiedzialna za reprezentowanie obiektu kontroli cisnia w komorze
    /// </summary>
    
    public partial class PressurePanel : UserControl
    {
        PressureControl      presureControl     = null;
        private const double pressureResolution = 1000;    //zmienna okresla ile miejsc po przecinku mozna wprowadzac do zmiennych presure
        private const double maxPressure        = 1000;     //max wartosc mozliwa do wpisania

        private       int    waitTimeOnRefresh  = 1; //[s]
        private       int    timerRefresh       = 0;

        Bitmap automaticMode_ON     = new Bitmap(Properties.Resources.Bistable_ON);
        Bitmap automaticMode_OFF    = new Bitmap(Properties.Resources.Bistable_OFF);
     
        //-----------------------------------------------------------------------------------------
        /**
         * Konstruktor
         */ 
        public PressurePanel()
        {
            InitializeComponent();

            scrollPressure.Minimum = 1;
            scrollPressure.Maximum = (int)(maxPressure * pressureResolution);

            dEditSetpoint.MinimumValue = 1 / pressureResolution;
            dEditSetpoint.MaximumValue = maxPressure;

            automaticMode_ON.MakeTransparent(Color.White);
            automaticMode_OFF.MakeTransparent(Color.White);

        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja odswieaz dane odczytane z fizycznego urzadzenia
         */ 
        public void RefreshData()
        {
            if(presureControl != null)
            {          
                //poczekaj az przeladuje dane po ich zmianie
                if (timerRefresh > waitTimeOnRefresh)
                {
                    if (presureControl.GetMode() == Types.GasProcesMode.Pressure_Vap)
                    {
                        rBtnGases.Checked       = false;
                        rBtnVaporaizer.Checked  = true;
                    }
                    if (presureControl.GetMode() == Types.GasProcesMode.Presure_MFC)
                    {
                        rBtnGases.Checked       = true;
                        rBtnVaporaizer.Checked  = false;
                    }

                    if (presureControl.GetMode() == Types.GasProcesMode.Unknown || presureControl.GetMode() == Types.GasProcesMode.FlowSP)
                    {
                        rBtnGases.Checked           = false;
                        rBtnVaporaizer.Checked      = false;
                        rBtnNone.Checked            = true;
                        cBoxAutomaticMode.Checked   = false;
                        cBoxAutomaticMode.Image     = automaticMode_OFF;
                    }
                    else
                    {
                        cBoxAutomaticMode.Checked   = true;
                        cBoxAutomaticMode.Image     = automaticMode_ON;

                    }
               
                    dEditSetpoint.Value = presureControl.GetSetpoint();
                    buttonUpDown.Value  = presureControl.GetSetpoint();
                }
            }
            if (timerRefresh <= waitTimeOnRefresh)
                timerRefresh++;

            AdjustPanelToPriviligesOfUser();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie dostosowanie panelu do uprawnien usera
         */
        private void AdjustPanelToPriviligesOfUser()
        {
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator)
            {
                dEditSetpoint.Enabled   = cBoxAutomaticMode.Checked;
                rBtnGases.Enabled       = cBoxAutomaticMode.Checked;
                rBtnVaporaizer.Enabled  = cBoxAutomaticMode.Checked;
            }

            if (ApplicationData.UserChanged)
            {
                bool state = true;
                if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator)
                    state = false;

                dEditSetpoint.Enabled     = state;
                rBtnGases.Enabled         = state;
                rBtnVaporaizer.Enabled    = state;
                cBoxAutomaticMode.Enabled = state;
                buttonUpDown.Enabled      = state;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia referenejce fizycznego obiektu pozawalajacego kontorolowac nastawy ciosniena w PLC
         */
        public void SetPresureControlPtr(PressureControl presurePtr)
        {
            presureControl = presurePtr;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Ustaw wartosc setpointa ze scroll bara
         */ 
        private void SetScrollValue(double aValue)
        {
            int aValueScroll = (int)(aValue * pressureResolution);

            if (scrollPressure.Maximum >= aValueScroll && scrollPressure.Minimum <= aValueScroll)
                scrollPressure.Value = aValueScroll;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Ustaw wartosc setpointa ze scroll bara
        */
        private void scrollPressure_ValueChanged(object sender, EventArgs e)
        {
            double aValue = 0;

            if (pressureResolution > 0)
                aValue = scrollPressure.Value / pressureResolution;

            dEditSetpoint.Value = aValue;
            
            dEditSetpoint.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zdarzenia zmiany setpointa pochodzaca bezposrednio ze zmiany wartosc w edicie
         */ 
        private bool dEditSetpoint_EnterOn()
        {
            bool  aRes = false;
            ItemLogger aErr = new ItemLogger();

            if (presureControl != null)
                aErr = presureControl.SetSetpoint(dEditSetpoint.Value);

            if (!aErr.IsError())
            {
                aRes = true;
                timerRefresh = 0;
            }
            SetScrollValue(dEditSetpoint.Value);

            Logger.AddError(aErr);

            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia tyb sterowania cisnieniem w komorze
         */ 
        private void cBoxGases_Click(object sender, EventArgs e)
        {
            ItemLogger aErr = new ItemLogger();
            
            if (presureControl != null)
            {
                if (rBtnGases.Checked)
                    aErr = presureControl.SetMode(Types.GasProcesMode.Presure_MFC);

                if (rBtnVaporaizer.Checked)
                    aErr = presureControl.SetMode(Types.GasProcesMode.Pressure_Vap);

                //Sprawdz czy kacja nie pochodz czasaem od wlaczenia/wylaczenia trybu autoamtycznj konotrli
                if (sender == cBoxAutomaticMode)
                {
                    //Wlacz automatyczna kontorle - ustaw domyslnie MFC
                    if (cBoxAutomaticMode.Checked)
                    {
                        aErr = presureControl.SetMode(Types.GasProcesMode.Presure_MFC);
                        if (!aErr.IsError())
                            cBoxAutomaticMode.Image = automaticMode_ON;
                    }
                    else
                    {
                        aErr = presureControl.SetMode(Types.GasProcesMode.FlowSP);
                        if (!aErr.IsError())
                            cBoxAutomaticMode.Image = automaticMode_OFF;
                    }
                }
            }
       
            timerRefresh = 0;
            Logger.AddError(aErr);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Ustwianie nastawy setpointa korzystajac z przycisku strzalek
         */ 
        private void buttonUpDown1_ValueChanged(object sender, EventArgs e)
        {         
            dEditSetpoint.Value = buttonUpDown.Value;

            dEditSetpoint.tBox_KeyUp(sender, new KeyEventArgs(Keys.Enter));
        }
        //-----------------------------------------------------------------------------------------

    }
}
