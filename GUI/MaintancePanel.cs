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

namespace HPT1000.GUI
{
    /// <summary>
    /// Klasa reprezentuje wizulna forme obiektu konserwacji. Pozwala na ustawienie/odczytanie parametrow oraz wykonaine akcji zwiazanych z konserwacja urzadzenia
    /// </summary>
    public partial class MaintancePanel : UserControl
    {
        Maintenance maintance = null;   ///< Referencja obiekt konserwacji
        bool noEvent = true;    //Flaga okresl ze mam pomijac zdarzenia na zmiane wartosci poniewaz nie pochodza one z itereakcji z userem

        //-------------SETERY/GETERY------------------------------------------------------------------------------------------
        public Maintenance MaintancePtr
        {
            set { maintance = value; }
        }
        //--------------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor 
         */
        public MaintancePanel()
        {
            InitializeComponent();
            dateTimeNextMaintance.MinDate = DateTime.Today;

            //Ustaw poprawny format dla DateTimePicker
            timeLeakMesure.Format = DateTimePickerFormat.Custom;
            timeLeakMesure.CustomFormat = "HH:mm:ss";

            actualTimeDurationLeakProces.Format = DateTimePickerFormat.Custom;
            actualTimeDurationLeakProces.CustomFormat = "HH:mm:ss";

        }
        //--------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie odswiezanie informacji na temat stanu konserwacji oraz jej parametrow
         */
        public void RefreshData()
        {
            if (maintance != null)
            {
                //Pokaz status
                ShowState();
                //POkaz aktualny czas trwania leaktestu
                if(maintance.TimeDuration - maintance.ActualTimeLeakTestDuration >= 0)
                    actualTimeDurationLeakProces.Value = Types.ConvertDate(maintance.TimeDuration - maintance.ActualTimeLeakTestDuration);
                //Odczytaj interwal czasu
                dEditInterval.Value = maintance.IntervalMonth;
                btnUpDownInterval.Value = maintance.IntervalMonth;
                //Ustaw czas ostnio wykonanej konserwacji
                labTimeLastMaintance.Text = maintance.DateLastMaintenance.ToShortDateString();
                labTimeNextGlobalMaintanence.Text = maintance.DateNextGlobalMaintenance.ToShortDateString();
                //Ustaw czas daty nastepnej konserwacji
           //     dateTimeNextMaintance.Value = maintance.DateNextMaintenance;
                //Pokaz czas konserwacji pompy wstepnej
                dEditRotatoryVanePump.Value = maintance.HourOilChange;
                btnUpDownTimeFP.Value = maintance.HourOilChange;
                //Pokaz liczbe wykonancyh procesow
                labProcesNumber.Text = maintance.ProcessNumber.ToString();
                labOperetingHour.Text = maintance.TimeOperatingMachine.ToString();
                tBoxTimeWorkFP.Text = maintance.TimeWorkFP.ToString();
                labTimeWorkFP.Text = maintance.TimeWorkFP.ToString();
                //Pokaz parametry dla kotrych jest przeprowadzany leak test
                dEditChamberVolume.Value = maintance.ChamberVolume;
                btnUpDownChamberVolume.Value = maintance.ChamberVolume;
                dEditSetpoint.Value = maintance.Setpoint;
                btnUpDownSetpoint.Value = maintance.Setpoint;
                //Pokaz wartosc nacieku. Jezeli jest uruchomiona procedura to wykreskuj dane
                tBoxLeak.Text = "------";
                if (maintance.StateLeakTest == Types.StateLeaktest.Stop)
                    tBoxLeak.Text = maintance.LeakValue.ToString("F3");
                //Pokaz tryb
                if (maintance.TypeTimeMaintenance == Source.Driver.Types.TimeMaintenance.Interval)
                    rBtnInterval.Checked = true;
                if (maintance.TypeTimeMaintenance == Source.Driver.Types.TimeMaintenance.Time)
                    rBtnTime.Checked = true;

                timeLeakMesure.Value = Types.ConvertDate(maintance.TimeDuration);
                //Dostosuj panel do uprawnien usera
                AdjustPanelToPriviligesOfUser();
                noEvent = false;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        /**
        * Dostosuj panel do uprawnieni posadanych prze usera
        */ 
        private void AdjustPanelToPriviligesOfUser()
        {
            bool state = true;
            if (Source.ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator || Source.ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Administrator)
                state = false;
            foreach (Control ctr in Controls)
            {
                if(ctr.Enabled != state)
                    ctr.Enabled = state;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void ShowState()
        {
            if (maintance != null)
            {
                //Polkaz status konsrewackoi
                labStatusMaintenance.Text = "MAINTENANCE NOT REQUIRED";
                labStatusMaintenance.BackColor = Color.Gray;
                if (maintance.IsMaintanceRequired())
                {
                    labStatusMaintenance.Text = "MAINTENANCE REQUIRED";
                    labStatusMaintenance.BackColor = Color.Red;
                }
                //Pokaz status wymiany oleju
                labStatusOilChange.Text = "OIL CHANGE NOT REQUIRED";
                labStatusOilChange.BackColor = Color.Gray;
                if (maintance.IsOilChange())
                {
                    labStatusOilChange.Text = "OIL CHANGE REQUIRED";
                    labStatusOilChange.BackColor = Color.Red;
                }

                //Pokaz status Leaktestu
                labLeaktestStatus.Visible = false;
                if(maintance.StateLeakTest != Types.StateLeaktest.None)
                    labLeaktestStatus.Visible = true;

                if (maintance.StateLeakTest == Types.StateLeaktest.Run_MesureLeak || maintance.StateLeakTest == Types.StateLeaktest.Run_PumpDown)
                {
                    cBoxLeakTest.Checked = true;
                    cBoxLeakTest.Image = Properties.Resources.off;
                }
                else
                {
                    cBoxLeakTest.Checked = false;
                    cBoxLeakTest.Image = Properties.Resources.on;
                }
                labLeaktestStatus.ForeColor = Color.Black;
                switch (maintance.StateLeakTest)
                {
                    case Types.StateLeaktest.Error:
                        labLeaktestStatus.ForeColor = Color.Red;
                        labLeaktestStatus.Text = "ERROR";
                        break;
                    case Types.StateLeaktest.Run_PumpDown:
                        labLeaktestStatus.ForeColor = Color.Green;
                        labLeaktestStatus.Text = "MEASURING LEAK RATE";
                         break;
                    case Types.StateLeaktest.Run_MesureLeak:
                        labLeaktestStatus.ForeColor = Color.Green;
                        labLeaktestStatus.Text = "WAIT";
                        break;
                    case Types.StateLeaktest.Stop:
                        labLeaktestStatus.Text = "STOP";
                        break;
                }

            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void dateTimeNextMaintance_ValueChanged(object sender, EventArgs e)
        {
            if (maintance != null)
                maintance.DateNextMaintenance = dateTimeNextMaintance.Value;
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void btnMaintanceMade_Click(object sender, EventArgs e)
        {
            if (maintance != null)
                maintance.SetMaintenanceMade();
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void rBtnInterval_Click(object sender, EventArgs e)
        {
            if (maintance != null)
            {
                if (rBtnInterval.Checked)
                    maintance.TypeTimeMaintenance = Source.Driver.Types.TimeMaintenance.Interval;
                if (rBtnTime.Checked)
                    maintance.TypeTimeMaintenance = Source.Driver.Types.TimeMaintenance.Time;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void btnOliChange_Click(object sender, EventArgs e)
        {
            if (maintance != null)
                maintance.ClearHourWorkFP();
        }
        //--------------------------------------------------------------------------------------------------------------------
        private bool dEditInterval_EnterOn()
        {
            bool ARes = false;
            if (maintance != null)
            {
                maintance.IntervalMonth = Convert.ToInt32(dEditInterval.Value);
                btnUpDownInterval.Value = Convert.ToInt32(dEditInterval.Value);
                ARes = true;
            }
            return ARes;
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void btnUpDownInterval_ValueChanged(object sender, EventArgs e)
        {
            dEditInterval.Value = btnUpDownInterval.Value;
            dEditInterval_EnterOn();
        }
        //--------------------------------------------------------------------------------------------------------------------
        private bool dEditRotatoryVanePump_EnterOn()
        {
            bool ARes = false;
            if (maintance != null)
            {
                maintance.HourOilChange = Convert.ToInt32(dEditRotatoryVanePump.Value);
                btnUpDownTimeFP.Value = Convert.ToInt32(dEditRotatoryVanePump.Value);

                ARes = true;
            }
            return ARes;
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void btnUpDownRotatoryFP_ValueChanged(object sender, EventArgs e)
        {
            dEditRotatoryVanePump.Value = btnUpDownTimeFP.Value;
            dEditRotatoryVanePump_EnterOn();
        }
        //--------------------------------------------------------------------------------------------------------------------
        private bool dEditChamberVolume_EnterOn()
        {
            bool ARes = false;
            if (maintance != null)
            {
                maintance.ChamberVolume = dEditChamberVolume.Value;
                btnUpDownChamberVolume.Value = dEditChamberVolume.Value;
                ARes = true;
            }
            return ARes;
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void btnUpDownChamberVolume_ValueChanged(object sender, EventArgs e)
        {
            dEditChamberVolume.Value = btnUpDownChamberVolume.Value;
            dEditChamberVolume_EnterOn();
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void btnUpDownSetpoint_ValueChanged(object sender, EventArgs e)
        {
            dEditSetpoint.Value = btnUpDownSetpoint.Value;
            dEditSetpoint_EnterOn();
        }
        //--------------------------------------------------------------------------------------------------------------------
        private bool dEditSetpoint_EnterOn()
        {
            bool ARes = false;

            if (maintance != null)
                ARes = maintance.SetSetpoint(dEditSetpoint.Value);
            if (ARes)
                btnUpDownSetpoint.Value = dEditSetpoint.Value;

            return ARes;
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void timeLeakMesure_ValueChanged(object sender, EventArgs e)
        {
            if (maintance != null && !noEvent)
                maintance.SetTimeLeakTest(timeLeakMesure.Value);
        }
        //--------------------------------------------------------------------------------------------------------------------
        //Sterowanie lekatestem
        private void cBoxLeakTest_Click(object sender, EventArgs e)
        {
            if (maintance != null)
            {
                //Start leak test
                if (cBoxLeakTest.Checked)
                {
                    double  aPressure = dEditSetpoint.Value;
                    int     aTime     = timeLeakMesure.Value.Hour * 3600 + timeLeakMesure.Value.Minute * 60 + timeLeakMesure.Value.Second;
                    maintance.StartLeakProcess(aPressure, aTime);
                }
                else
                    maintance.StopLeakProcess();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
}
