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

namespace HPT1000.GUI
{
    /*Klasa jest odpowiedzialna za ustawienie konfigruacji urzadzenia oraz dodanie typow gazow z ktorych mozemy korzystac
     */
    public partial class ServicePanel : UserControl
    {
        private Source.Driver.HPT1000 hpt1000 = null;
        private GasTypes gasTypes = null;
        private bool lastStateCommunication = false;

        private bool initValue = false; // flaga okresla ze aktualnei odbywa sie inicjalizacja wartosci komponentow i nie powinno sie ustawiac ich w PLC bo nie powstaly ich zmiany z ingerencji usera
        private GasType selectedGas = null;
        private bool blockEvent = false;
        //----------------------------------------------------------------------------------------------------------------------------
        public Source.Driver.HPT1000 HPT1000
        {
            set
            {
                hpt1000 = value;                       //Ustaw referencvje na urzadzenie
                if (hpt1000 != null)
                    gasTypes = hpt1000.GetGasTypes();   //Ustaw referencje na typ gazu
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Konstruktor klasy sluzacy do zainicjalizowania poczatkowych wartosci formatki
        public ServicePanel()
        {
            initValue = true;

            InitializeComponent();
            //Pokaz zapisane typy gazow
            FillComboBoxGases();
            //Pokaz dostepny typy vaporaziera
            FillComboBoxVaporaizerType();

            initValue = false;

            //Daj mozliwosc ustawienia backgourndcolor dla tab of pagecontrol
            tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;

            //Ustaw poprawny format datetime picker
            timeSetpointStabilization.Format = DateTimePickerFormat.Custom;
            timeSetpointStabilization.CustomFormat = "HH:mm:ss";

            timeWaitOnOperate.Format = DateTimePickerFormat.Custom;
            timeWaitOnOperate.CustomFormat = "HH:mm:ss";

            timeWaitPF.Format = DateTimePickerFormat.Custom;
            timeWaitPF.CustomFormat = "HH:mm:ss";

            timePumpToSV.Format = DateTimePickerFormat.Custom;
            timePumpToSV.CustomFormat = "HH:mm:ss";

            timePressureStability.Format = DateTimePickerFormat.Custom;
            timePressureStability.CustomFormat = "HH:mm:ss";

            timeFlowStabilization.Format = DateTimePickerFormat.Custom;
            timeFlowStabilization.CustomFormat = "HH:mm:ss";

            timeFlowStabilityMixGas.Format = DateTimePickerFormat.Custom;
            timeFlowStabilityMixGas.CustomFormat = "HH:mm:ss";

            timeMaxPumpDown.Format = DateTimePickerFormat.Custom;
            timeMaxPumpDown.CustomFormat = "HH:mm:ss";
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja wypelnia ComboBoxa zapisanymi typami gazow. Jezeli jest jakis gaz to wyswietl jego infomracje 
        private void FillComboBoxGases()
        {
            FillListGases();

            cBoxGasCalibrated_MFC1.Items.Clear();
            cBoxGasCalibrated_MFC2.Items.Clear();
            cBoxGasCalibrated_MFC3.Items.Clear();

            if (gasTypes != null)
            {
                //Dodaj do lsity dostepne typy gazow
                foreach (GasType gasType in gasTypes.Items)
                {
                    cBoxGasCalibrated_MFC1.Items.Add(gasType);
                    cBoxGasCalibrated_MFC2.Items.Add(gasType);
                    cBoxGasCalibrated_MFC3.Items.Add(gasType);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja wypelnia liste zapisanymi typami gazow. Jezeli jest jakis gaz to wyswietl jego infomracje 
        private void FillListGases()
        {
            listViewGases.Items.Clear();

            if (gasTypes != null)
            {
                //Dodaj do lsity dostepne typy gazow
                foreach (GasType gasType in gasTypes.Items)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = gasType.Name;
                    item.SubItems.Add(gasType.Factor.ToString("F3"));
                    item.Tag = gasType;

                    listViewGases.Items.Add(item);
                }
            }
            FillGasTypeInChamberCBox();
        }
        //----------------------------------------------------------------------------------------------------------------------------
        /**
         * Wypelnij liste dostpnymi gazami oraz ustaw na aktulnym gazie kotry jest ustawiony w komorze
         */
        private void FillGasTypeInChamberCBox()
        {
            blockEvent = true;
            cBoxChamberGas.Items.Clear();

            if (gasTypes != null)
            {
                //Dodaj do lsity dostepne typy gazow wraz z podaniem jej factora
                foreach (GasType gasType in gasTypes.Items)
                    cBoxChamberGas.Items.Add(gasType);
            }
            blockEvent = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie wyswietlenie gazu o raz fkatora ktroy jest ustawiony w PLC
         */
        private void ShowGasFromChamber()
        {
            blockEvent = true;

            cBoxChamberGas.Text = "Unrecognized";
            string gasName = "Unrecognized";
            int i = 0;
            //Ustaw itema na gazie ktory jest w komorze
            foreach (GasType gasType in cBoxChamberGas.Items)
            {
                if (Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000.Chamber.Gas != null && Math.Abs(gasType.Factor - Source.Factory.Hpt1000.Chamber.Gas.Factor) < 0.001)
                {
                    cBoxChamberGas.SelectedIndex = i;
                    gasName = gasType.Name;
                }
                i++;
            }
            if (Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000.Chamber.Gas != null)
                labFactorGauge.Text = "Factor of gas '" + gasName + "' :" + Source.Factory.Hpt1000.Chamber.Gas.Factor.ToString("F3");

            blockEvent = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja wypelnia ComboBoxa zapisanymi typami gazow. Jezeli jest jakis gaz to wyswietl jego infomracje 
        private void FillComboBoxVaporaizerType()
        {
            cBoxVaporaizerType.Items.Clear();
            //Dodaj do lsity dostepne typy vaporaizera
            foreach (string type in Enum.GetNames(typeof(Types.VaporaizerType)))
            {
                cBoxVaporaizerType.Items.Add(type);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odswiezanie informacji na temat ustawien danych obiektow
        public void RefreshSettingsPanel()
        {
            initValue = true;

            if (hpt1000 != null && (hpt1000.GetPLC() != null))
            {
                PowerSupplay aPowerSupply = hpt1000.GetPowerSupply();
                MFC aMFC = hpt1000.GetMFC();
                ForePump aForePump = hpt1000.GetForePump();
                Vaporizer vaporaizer = hpt1000.GetVaporizer();
                Gauge gauge = this.hpt1000.GetGauge();

                RefreshPowerSupply(aPowerSupply);   //Odswiez panel zasilacza to znaczy odczytaj jego ustawienia i je zaprezentuj
                RefreshMFC(aMFC);                   //Odswiez panel MFC to znaczy odczytaj jego ustawienia i je zaprezentuj
                RefreshForePump(aForePump);         //Odswiez panel pompy to znaczy odczytaj jego ustawienia i je zaprezentuj
                RefreshVaporazier(vaporaizer);      //Odswiez panel vaporaziera to znaczy odczytaj jego ustawianie i je zaprezentuj (chodzi o typ )
                RefreshPID(aMFC, aPowerSupply);
                RefreshTimePLC(hpt1000.Chamber);
                RefreshGauge(gauge);
                RefreshMotor();
                //Odswiez liste zapisanych typow gazow
                FillComboBoxGases();
                //Pokaz gaz jaki jest w komorze
                ShowGasFromChamber();
            }

            initValue = false;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odswiezenie panelu zasilacza to znaczy odczytaj jego ustawienia i je zaprezentuj
        private void RefreshPowerSupply(PowerSupplay powerSupply)
        {
            if (powerSupply != null && hpt1000 != null)
            {
                //Przedstaw parametry urzadzenia
                if (hpt1000.CoonectionPLC)
                {
                    dEditCurentLimit.Value = powerSupply.LimitCurent;
                    dEditPowerLimit.Value = powerSupply.LimitPower;
                    dEditVoltageLimit.Value = powerSupply.LimitVoltage;
                    dEditRangePower.Value = powerSupply.MaxPower;
                    dEditRangeCurent.Value = powerSupply.MaxCurent;
                    dEditRangeVoltage.Value = powerSupply.MaxVoltage;
                    timeSetpointStabilization.Value = Types.ConvertDate((int)powerSupply.TimeWaitSetpoint);
                    timeWaitOnOperate.Value = Types.ConvertDate((int)powerSupply.TimeWaitOperate);
                    dEditMinRangeVoltageHV.Value = powerSupply.MinRangeVolatgeHV;
                    dEditMaxRangeVoltageHV.Value = powerSupply.MaxRangeVolatgeHV;
                    ShowInfoAboutLimitPower();
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odswiezenie panelu MFC to znaczy odczytaj jego ustawienia i je zaprezentuj
        private void RefreshMFC(MFC mfc)
        {
            if (mfc != null && hpt1000 != null && hpt1000.Chamber != null)
            {
                //Przedstaw parametry urzadzenia
                if (hpt1000.CoonectionPLC)
                {
                    dEditMaxFlow_MFC1.Value = mfc.GetMaxCalibFlow(1);
                    dEditMaxFlow_MFC2.Value = mfc.GetMaxCalibFlow(2);
                    dEditMaxFlow_MFC3.Value = mfc.GetMaxCalibFlow(3);
                    dEditRangeVoltageMFC1.Value = mfc.GetRangeVoltage(1) / 1000; //Konwersja z mV na V
                    dEditRangeVoltageMFC2.Value = mfc.GetRangeVoltage(2) / 1000; //Konwersja z mV na V
                    dEditRangeVoltageMFC3.Value = mfc.GetRangeVoltage(3) / 1000; //Konwersja z mV na V
                    timeFlowStabilization.Value = Types.ConvertDate((int)mfc.TimeFlowStability);
                    timePressureStability.Value = Types.ConvertDate((int)mfc.TimePressureStability);
                    timeFlowStabilityMixGas.Value = Types.ConvertDate((int)hpt1000.Chamber.GetTimeFlowStabilityMixGas());

                    cBoxMFC1.Checked = mfc.GetActive(1);
                    cBoxMFC2.Checked = mfc.GetActive(2);
                    cBoxMFC3.Checked = mfc.GetActive(3);

                    dEditMaxFlow_MFC1.Enabled = cBoxMFC1.Checked;
                    dEditRangeVoltageMFC1.Enabled = cBoxMFC1.Checked;
                    cBoxGasCalibrated_MFC1.Enabled = cBoxMFC1.Checked;

                    dEditMaxFlow_MFC2.Enabled = cBoxMFC2.Checked;
                    dEditRangeVoltageMFC2.Enabled = cBoxMFC2.Checked;
                    cBoxGasCalibrated_MFC2.Enabled = cBoxMFC2.Checked;

                    dEditMaxFlow_MFC3.Enabled = cBoxMFC3.Checked;
                    dEditRangeVoltageMFC3.Enabled = cBoxMFC3.Checked;
                    cBoxGasCalibrated_MFC3.Enabled = cBoxMFC3.Checked;

                    dEditPressureLimitGas.Value = mfc.PressureLimitGas;
                    dEditPressureLimitHV.Value = mfc.PressureLimitHV;

                    SetComboBoxCalibratedFactor(cBoxGasCalibrated_MFC1, mfc.GetCalibratedFactor(1));
                    SetComboBoxCalibratedFactor(cBoxGasCalibrated_MFC2, mfc.GetCalibratedFactor(2));
                    SetComboBoxCalibratedFactor(cBoxGasCalibrated_MFC3, mfc.GetCalibratedFactor(3));

                    dEditThresholdMixGas.Value = hpt1000.Chamber.GetThresholdMixGasProc();
                    int para = hpt1000.Chamber.GetActiveOptionMixaGasPorc();
                    if (para == 1)
                        cBoxActiveProcMixGas.Checked = true;
                    else
                        cBoxActiveProcMixGas.Checked = false;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odswiezenie panelu pompy to znaczy odczytaj jego ustawienia i je zaprezentuj
        private void RefreshForePump(ForePump forePump)
        {
            if (forePump != null && hpt1000 != null)
            {
                //Przedstaw parametry urzadzenia
                if (hpt1000.CoonectionPLC)
                {
                    timePumpToSV.Value = Types.ConvertDate((int)forePump.TimePumpToSV);
                    timeWaitPF.Value = Types.ConvertDate((int)forePump.TimeWaitPF);
                    timeMaxPumpDown.Value = Types.ConvertDate((int)forePump.MaxTimetPumpDown);
                    dEditSetpointPumpDown.Value = forePump.SetpointPumpDown;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Odswiez panel vaporaziera to znaczy odczytaj jego ustawianie i je zaprezentuj (chodzi o typ )
        private void RefreshVaporazier(Vaporizer vaporaizer)
        {
            if (vaporaizer != null)
            {
                for (int i = 0; i < cBoxVaporaizerType.Items.Count; i++)
                {
                    if (cBoxVaporaizerType.Items[i].ToString() == Enum.GetName(typeof(Types.VaporaizerType), Vaporizer.GetTypeVaporaizer()))
                        cBoxVaporaizerType.SelectedIndex = i;
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Odswiez panel vaporaziera to znaczy odczytaj jego ustawianie i je zaprezentuj (chodzi o typ )
        private void RefreshGauge(Gauge gauge)
        {
            if (gauge != null && hpt1000 != null)
            {
                //Przedstaw parametry urzadzenia
                if (hpt1000.CoonectionPLC)
                {
                    switch(gauge.Type)
                    {
                        case Types.GaugeType.Pirani:
                            rBtnPirani.Checked = true;
                            break;
                        case Types.GaugeType.Barotron:
                            rBtnBarotron.Checked = true;
                            break;
                        default:
                            rBtnNoneGauge.Checked = true;
                            break;
                    }
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie przedstawinie na formatce parametrow reuglatora PID odczytene ze sterownika PLC
         */
        private void RefreshPID(MFC mfc, PowerSupplay powerSupply)
        {
            if (mfc != null)
            {
                //Przedstaw parametry urzadzenia
                if (hpt1000.CoonectionPLC)
                {
                    dEditKp.Value = mfc.PID_Kp;
                    dEditTi.Value = mfc.PID_Ti;
                    dEditTd.Value = mfc.PID_Td;
                    dEditTs.Value = mfc.PID_Ts;
                    dEditFiltr.Value = mfc.PID_Filtr;

                    dEditPID_HV_Kp.Value = powerSupply.PID_Kp;
                    dEditPID_HV_Ti.Value = powerSupply.PID_Ti;
                    dEditPID_HV_Td.Value = powerSupply.PID_Td;
                    dEditPID_HV_Ts.Value = powerSupply.PID_Ts;
                    dEditPID_HV_Filtr.Value = powerSupply.PID_Filtr;

                    cBoxPID_HV.Checked = powerSupply.PID_ON;
                    SetEnableOptionPIDHV(cBoxPID_HV.Checked);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Ograniczenie max wartosci do jakiej mozna ustawic wyj analogowe ogranicza nam max moc zasilacza. Powiadom o tym usera aby byl swiadomy
        private void ShowInfoAboutLimitPower()     
        {
       /*     if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (hpt1000.GetPowerSupply().RangeVolategHV >= 10.00)
                    labMsgLimiPower.Visible = false;
                else
                {
                     double aLimitValue = (hpt1000.GetPowerSupply().RangeVolategHV / 10.0 ) * hpt1000.GetPowerSupply().MaxPower;
                    labMsgLimiPower.Visible = true;
                    labMsgLimiPower.Text = "Possible power to reach is " + aLimitValue.ToString("F3") + " W";
                }
            }
      */  }
        //----------------------------------------------------------------------------------------------------------------------------
        private void SetEnableOptionPIDHV(bool aEnable)
        {
            dEditPID_HV_Kp.Enabled = aEnable;
            dEditPID_HV_Ti.Enabled = aEnable;
            dEditPID_HV_Td.Enabled = aEnable;
            dEditPID_HV_Ts.Enabled = aEnable;
            dEditPID_HV_Filtr.Enabled = aEnable;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        private void RefreshTimePLC(Chamber aChamber)
        {
            if (aChamber != null)
            {
                if (timePLC.MinDate < aChamber.TimePLC && timePLC.MaxDate > aChamber.TimePLC)
                    timePLC.Value = aChamber.TimePLC;
                if (datePLC.MinDate < aChamber.DatePLC && datePLC.MaxDate > aChamber.DatePLC)
                    datePLC.Value = aChamber.DatePLC;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odswiezenie panelu Motor
        private void RefreshMotor()
        {
            cBoxEnableMotor1.Checked = MotorDriver.Motor_1_Enable;
            cBoxEnableMotor2.Checked = MotorDriver.Motor_2_Enable;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia MFC na 0
        private void ClearMFComponent()
        {
            dEditMaxFlow_MFC1.Value = 0;
            dEditMaxFlow_MFC2.Value = 0;
            dEditMaxFlow_MFC3.Value = 0;
            dEditRangeVoltageMFC1.Value = 0;
            dEditRangeVoltageMFC2.Value = 0;
            dEditRangeVoltageMFC3.Value = 0;
            timeFlowStabilization.Value = Types.ConvertDate(0);
            timeFlowStabilityMixGas.Value = Types.ConvertDate(0);
            cBoxMFC1.Checked = false;
            cBoxMFC2.Checked = false;
            cBoxMFC3.Checked = false;
            timePressureStability.Value = Types.ConvertDate(0);
            cBoxVaporaizerType.SelectedIndex = 0;

            blockEvent = true;
            cBoxGasCalibrated_MFC1.SelectedIndex = -1;
            cBoxGasCalibrated_MFC2.SelectedIndex = -1;
            cBoxGasCalibrated_MFC3.SelectedIndex = -1;
            cBoxGasCalibrated_MFC1.Text = "";
            cBoxGasCalibrated_MFC2.Text = "";
            cBoxGasCalibrated_MFC3.Text = "";

            blockEvent = false;

            SetEnableComponentMFC(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia PowerSupply na 0
        private void ClearPowerSupplyComponent()
        {
            dEditCurentLimit.Value = 0;
            dEditPowerLimit.Value = 0;
            dEditVoltageLimit.Value = 0;
            dEditRangePower.Value = 0;
            dEditRangeCurent.Value = 0;
            dEditRangeVoltage.Value = 0;
            timeSetpointStabilization.Value = Types.ConvertDate(0);
            timeWaitOnOperate.Value = Types.ConvertDate(0);
            dEditMinRangeVoltageHV.Value = 0;
            dEditMaxRangeVoltageHV.Value = 0;

            SetEnableComponentPowerSupply(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearPumpComponent()
        {
            timePumpToSV.Value = Types.ConvertDate(0);
            timeWaitPF.Value = Types.ConvertDate(0);
            timeMaxPumpDown.Value = Types.ConvertDate(0);
            dEditSetpointPumpDown.Value = 0;

            SetEnableComponentPump(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearVaporiserComponent()
        {
            cBoxVaporaizerType.SelectedIndex = -1;

            SetEnableComponentVaporiser(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearGasesComponent()
        {
            tBoxNameGas.Text = "";
            tBoxGasDescription.Text = "";
            dEditFactorGas.Value = 0;
            cBoxChamberGas.SelectedIndex = -1;
            dEditPressureLimitGas.Value = 0;
            dEditPressureLimitHV.Value = 0;
            dEditThresholdMixGas.Value = 0;
            cBoxActiveProcMixGas.Checked = false;

            SetEnableComponentGases(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearPidComponent()
        {
            dEditKp.Value = 0;
            dEditTi.Value = 0;
            dEditTs.Value = 0;
            dEditTd.Value = 0;
            dEditFiltr.Value = 0;

            dEditPID_HV_Kp.Value = 0;
            dEditPID_HV_Ti.Value = 0;
            dEditPID_HV_Ts.Value = 0;
            dEditPID_HV_Td.Value = 0;
            dEditPID_HV_Filtr.Value = 0;

            cBoxPID_HV.Checked = false;
            timePressureStability.Value = Types.ConvertDate(0);

            SetEnableComponentPid(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearTimeComponent()
        {
            timePLC.Value = Types.ConvertDate(0);
            datePLC.Value = Types.ConvertDate(0);

            SetEnableComponentTime(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearMotorComponent()
        {
            cBoxEnableMotor1.Checked = false;
            cBoxEnableMotor2.Checked = false;

            SetEnableComponentMotor(false);
        }
                //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie ustawienie komponentow odpowiedizalnych za wizualicjace parametrow urzadzenia ForePump na 0
        private void ClearGaugeComponent()
        {
            rBtnNoneGauge.Checked = true;

            SetEnableComponentGauge(false);
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow MFC
        private void SetEnableComponentMFC(bool enabled)
        {
            //Jezeli jest brak komunikacji to nie moge ustawiac parametrow urzadzenia
            dEditMaxFlow_MFC1.Enabled = enabled;
            dEditMaxFlow_MFC2.Enabled = enabled;
            dEditMaxFlow_MFC3.Enabled = enabled;
            dEditRangeVoltageMFC1.Enabled = enabled;
            dEditRangeVoltageMFC2.Enabled = enabled;
            dEditRangeVoltageMFC3.Enabled = enabled;
            timeFlowStabilization.Enabled = enabled;
            timeFlowStabilityMixGas.Enabled = enabled;
            timePressureStability.Enabled = enabled;
            cBoxMFC1.Enabled = enabled;
            cBoxMFC2.Enabled = enabled;
            cBoxMFC3.Enabled = enabled;
            cBoxVaporaizerType.Enabled = enabled;
            cBoxGasCalibrated_MFC1.Enabled = enabled;
            cBoxGasCalibrated_MFC2.Enabled = enabled;
            cBoxGasCalibrated_MFC3.Enabled = enabled;
            btnReadSetingsPLC_1.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow zasilacza
        private void SetEnableComponentPowerSupply(bool enabled)
        {
            dEditCurentLimit.Enabled = enabled;
            dEditPowerLimit.Enabled = enabled;
            dEditVoltageLimit.Enabled = enabled;
            dEditRangePower.Enabled = enabled;
            dEditRangeCurent.Enabled = enabled;
            dEditRangeVoltage.Enabled = enabled;
            timeSetpointStabilization.Enabled = enabled;
            timeWaitOnOperate.Enabled = enabled;
            dEditMinRangeVoltageHV.Enabled = enabled;
            dEditMaxRangeVoltageHV.Enabled = enabled;
            btnReadSetingsPLC_2.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentPump(bool enabled)
        {
            timePumpToSV.Enabled = enabled;
            timeWaitPF.Enabled = enabled;
            timeMaxPumpDown.Enabled = enabled;
            dEditSetpointPumpDown.Enabled = enabled;
            btnReadSetingsPLC_3.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentVaporiser(bool enabled)
        {
            cBoxVaporaizerType.Enabled = enabled;
            btnReadSetingsPLC_4.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentGases(bool enabled)
        {
            tBoxNameGas.Enabled = enabled;
            tBoxGasDescription.Enabled = enabled;
            dEditFactorGas.Enabled = enabled;
            cBoxChamberGas.Enabled = enabled;
            dEditPressureLimitGas.Enabled = enabled;
            dEditPressureLimitHV.Enabled = enabled;
            dEditThresholdMixGas.Enabled = enabled;
            listViewGases.Enabled = enabled;
            btnSaveSettings.Enabled = enabled;
            btnRemoveGas.Enabled = enabled;
            btnNewGas.Enabled = enabled;
            cBoxActiveProcMixGas.Enabled = enabled;
            btnReadSetingsPLC_5.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentPid(bool enabled)
        {
            dEditKp.Enabled = enabled;
            dEditTi.Enabled = enabled;
            dEditTs.Enabled = enabled;
            dEditTd.Enabled = enabled;
            dEditFiltr.Enabled = enabled;

            dEditPID_HV_Kp.Enabled = enabled;
            dEditPID_HV_Ti.Enabled = enabled;
            dEditPID_HV_Ts.Enabled = enabled;
            dEditPID_HV_Td.Enabled = enabled;
            dEditPID_HV_Filtr.Enabled = enabled;

            cBoxPID_HV.Enabled = enabled;
            timePressureStability.Enabled = enabled;
            btnReadSetingsPLC_6.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentTime(bool enabled)
        {
            timePLC.Enabled = enabled;
            datePLC.Enabled = enabled;

            btnSetDateTime.Enabled = enabled;
            btnReadSetingsPLC_7.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentMotor(bool enabled)
        {
            cBoxEnableMotor1.Enabled = enabled;
            cBoxEnableMotor2.Enabled = enabled;
            btnReadSetingsPLC_8.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Uutaw dostepnosc komponentow odpowiedzialnych za prezentacje/ustawianie parametrow fore pump
        private void SetEnableComponentGauge(bool enabled)
        {
            rBtnPirani.Enabled = enabled;
            rBtnBarotron.Enabled = enabled;
            btnReadSetingsPLC_9.Enabled = enabled;
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie konwetowanie obiektu czasu na sekundy poniewaz parametry zapisywane w PLC sa w sekundach
        private int GetSecond(DateTime aDateTime)
        {
            int aSec = 0;
            //Konwersja czasu na sekundy
            aSec = aDateTime.Hour * 3600 + aDateTime.Minute * 60 + aDateTime.Second;

            return aSec;
        }
        //------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private void timePumpToSV_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetForePump() != null && !initValue)
                hpt1000.GetForePump().SetTimePumpToSV(GetSecond(timePumpToSV.Value));
        }
        //----------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private void timeWaitPF_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetForePump() != null && !initValue)
                hpt1000.GetForePump().SetTimeWaitPF((int)GetSecond(timeWaitPF.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private void timeFlowStabilization_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetMFC() != null && !initValue)
                hpt1000.GetMFC().SetTimeFlowStability(GetSecond(timeFlowStabilization.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditRangeVoltageMFC1_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                //Ustaw zakres napiec w [mv]
                if (!hpt1000.GetMFC().SetRangeVoltage(1, ((int)(dEditRangeVoltageMFC1.Value * 1000))).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditRangeVoltageMFC2_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                //Ustaw zakres napiec w [mv]
                if (!hpt1000.GetMFC().SetRangeVoltage(2, ((int)(dEditRangeVoltageMFC2.Value * 1000))).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditRangeVoltageMFC3_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                //Ustaw zakres napiec w [mv]
                if (!hpt1000.GetMFC().SetRangeVoltage(3, ((int)(dEditRangeVoltageMFC3.Value * 1000))).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private void timeSetpointStabilization_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null && !initValue)
                hpt1000.GetPowerSupply().SetWaitTimeSetpoint(GetSecond(timeSetpointStabilization.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private void timeWaitOnOperate_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null && !initValue)
                hpt1000.GetPowerSupply().SetWaitTimeOperate(GetSecond(timeWaitOnOperate.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditRangeCurent_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetMaxCurent(dEditRangeCurent.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditRangeVoltage_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetMaxVoltage(dEditRangeVoltage.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditRangePower_EnterOn()
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetMaxPower(dEditRangePower.Value).IsError())
                    aRes = true;
            }
            ShowInfoAboutLimitPower();
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditMaxFlow_MFC1_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetMaxFlow(1, (int)dEditMaxFlow_MFC1.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditMaxFlow_MFC2_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetMaxFlow(2, (int)dEditMaxFlow_MFC2.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditMaxFlow_MFC3_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetMaxFlow(3, (int)dEditMaxFlow_MFC3.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditCurentLimit_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetLimitCurent(dEditCurentLimit.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditVoltageLimit_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetLimitVoltage(dEditVoltageLimit.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu ustawienie parametru
        private bool dEditPowerLimit_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetLimitPower(dEditPowerLimit.Value).IsError())
                    aRes = true;
            }

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja zdarzenia majaca na celu odczytanie parmaetrow z PLC
        private void btnReadSettings_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null)
                hpt1000.UpdateSettings();

            RefreshSettingsPanel();
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Zdarzenie zmiany wybranego typu gazu. Przedtsw ustawienia dla wybranego gazu
        private void cBoxGasType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            GasType gasType = (GasType)cBoxGasType.SelectedItem; //Pobierz typ aktualnie wybranego gazu 

            if (gasType != null)
            {
                //Ustawiony zostal wezel gazu przedstaw jego parametry
                tBoxGasDescription.Text = gasType.Description;
                dEditFactorGas.Value    = gasType.Factor;
                tBoxNameGas.Text        = gasType.Name;
            }
            */
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
        * Zadaniem metody jest ustawienie w liscie itemow combox itemu ktory odpowiada podanemu faktorowi
        */
        private void SetComboBoxCalibratedFactor(ComboBox cBox, float aFactor)
        {
            if (cBox != null)
            {
                for (int i = 0; i < cBox.Items.Count; i++)
                {
                    GasType gas = (GasType)cBox.Items[i];
                    //Wykonaj porównanie z dokładnościa do 0.001
                    if (gas != null && Math.Abs(gas.Factor - aFactor) <= 0.001)
                    {
                        cBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja sprawdza czy nazwa gazu juz czasem nie istnieje
         */
        private bool IsGasNameUniq(string gasName, int gasID)
        {
            bool res = false;
            int countDifferName = 0;
            //Przejdz po wszystkich gazach i zlicz liczbe gazow ktre posiadaja inna nazwe - pamietaj aby mnie nie brac pod uwage
            foreach (GasType gasType in gasTypes.Items)
            {
                if (gasType.Name != gasName || (gasType.Name == gasName && gasType.ID == gasID))
                    countDifferName++;
            }
            //jezlei liczba gazow posiadajacych rozna nazwe jest rowna liczbie gazow to znaczy ze jest ona unikalana
            if (countDifferName == gasTypes.Items.Count)
                res = true;

            return res;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zapisanie parametrow dla wybranego typu gazu. Dane sa zapisywane w bazie danych
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            bool aError = true;
            //Pobierz aktualnie wybrany typ gazu
            if (selectedGas != null)
            {
                //Pole nazwy nie moze byc puste
                if (tBoxNameGas.Text != "")
                {
                    //Nazwa musi byc unikalna
                    if (IsGasNameUniq(tBoxNameGas.Text, selectedGas.ID))
                    {
                        //Uzupelnij obiekt danymi
                        selectedGas.Factor = dEditFactorGas.Value;
                        selectedGas.Description = tBoxGasDescription.Text;
                        selectedGas.Name = tBoxNameGas.Text;
                        //Zapisz dane w bazie danych. Jezeli sie to powiedzie to poinformuj o tym usera
                        if (selectedGas.Save() == 0)
                        {
                            //Gaz zostal zmodyfikowany to odswiez comboboxa na wypadek gdyby sie zmienila nazwa pamietajac prz tym aktualnie wybrany typ gazu
                            FillComboBoxGases();

                            aError = false;
                            Source.Logger.AddMsg(Source.Translate.GetText("Gas name '" + selectedGas.Name + "' has been saved successfully"), Types.MessageType.Message);
                            MessageBox.Show(Source.Translate.GetText("Gas name '" + selectedGas.Name + "' has been saved successfully"), "Save gas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Source.Logger.AddMsg(Source.Translate.GetText("Failed save gas. Gas name '" + tBoxNameGas.Text + "' already exist"), Types.MessageType.Error);
                        MessageBox.Show("Failed save gas. Gas name '" + tBoxNameGas.Text + "' already exist", "Save gas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Source.Logger.AddMsg(Source.Translate.GetText("Failed save gas. Field Name cannot be empty"), Types.MessageType.Error);
                    MessageBox.Show(Source.Translate.GetText("Failed save gas.Field Name cannot be empty"), "Save gas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Source.Logger.AddMsg(Source.Translate.GetText("Failed save gas. Gas has not been selected"), Types.MessageType.Error);
                MessageBox.Show(Source.Translate.GetText("Failed save gas. Gas has not been selected"), "Save gas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Nie udalo sie zapisac zmian ustaw elementy wizulane na aktualnych wartosciach typu gazu
            /*    if (aError && selectedGas != null)
                {
                    tBoxGasDescription.Text = selectedGas.Description;
                    dEditFactorGas.Value    = selectedGas.Factor;
                    tBoxNameGas.Text        = selectedGas.Name;
                }
            */
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcaj ma za zadanie dodawanie nowego typu gazu
        private void btnNewGas_Click(object sender, EventArgs e)
        {
            if (gasTypes != null)
            {
                DialogResult resMsg = DialogResult.Yes;
                if (selectedGas != null && selectedGas.Changes)
                    resMsg = MessageBox.Show(" Do you want discard changes for gas '" + selectedGas.Name + "' ? Gas has been changed but not saved. ", "Gas changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //
                if (resMsg == DialogResult.Yes)
                {
                    if (selectedGas != null)
                        selectedGas.ClearChanges();

                    GasType gasType = new GasType(false);
                    //Uzupelnij dane nowo utworzonego obiektu
                    gasType.Factor = 1;
                    gasType.Description = "Description ...";// tBoxGasDescription.Text;
                    gasType.Name = GetUniqGasName();// tBoxNameGas.Text;
                                                    //Jezeli dany gaz nie jest jeszcze dodany to go dodaj
                    if (!gasTypes.Contains(gasType))
                    {
                        //Dodaj nowy gaz do bazy danych. Jezeli operacja sie powiedzie to dodaj go takze do listu lokalnej oraz poinformuj o tym usera
                        gasTypes.Add(gasType);
                        //Dodaj gaz do lokalnej listy combobox
                        FillComboBoxGases();          //Odswiez Comboboxa
                                                      //Zapisz nowy gaz w bazie
                        int res = gasType.Save();
                        //Poinfomruj usera ze udalo sie daodac gaz
                        if (res == 0)
                        {
                            Source.Logger.AddMsg(Source.Translate.GetText("Gas name '" + gasType.Name + "' has been added to gases list"), Types.MessageType.Message);
                            //Ustaw selecta na nowym gazie
                            selectedGas = gasType;
                            //Wyswietl infomracje o nowym itemie
                            dEditFactorGas.Value = selectedGas.Factor;
                            tBoxGasDescription.Text = selectedGas.Description;
                            tBoxNameGas.Text = selectedGas.Name;
                        }
                        else
                        {
                            Source.Logger.AddMsg("Failed saved new gas '" + gasType.Name + "' in database", Types.MessageType.Error);
                            MessageBox.Show("Failed saved new gas '" + gasType.Name + "' in database", "New Gas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Source.Logger.AddMsg(Source.Translate.GetText("Failed add new gas. Gas name '" + gasType.Name + "' already exists"), Types.MessageType.Error);
                        MessageBox.Show(Source.Translate.GetText("Failed add new gas. Gas name '" + gasType.Name + "' already exists"), "New Gas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Operation added new gas has been canceled", "New Gas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie unikalnej nazwy dla gazu. Nazwa to Gas + kolejny index
         */
        private string GetUniqGasName()
        {
            string uniq_gas_name = "Gas";
            bool exist = true;
            int index = 1;
            while (exist)
            {
                exist = false;
                uniq_gas_name = "Gas " + index.ToString();
                foreach (GasType gas in gasTypes.Items)
                    if (gas.Name == uniq_gas_name)
                        exist = true;
                index++;
            }
            return uniq_gas_name;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkja ustawia konfiguracje dostepnosci przeplywke w systemie
        private void cBoxMFC_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null)
            {
                //Ustasw aktywnosc dla danej przeplywki
                if (sender == cBoxMFC1)
                    hpt1000.GetMFC().SetActive(1, cBoxMFC1.Checked);

                if (sender == cBoxMFC2)
                    hpt1000.GetMFC().SetActive(2, cBoxMFC2.Checked);

                if (sender == cBoxMFC3)
                    hpt1000.GetMFC().SetActive(3, cBoxMFC3.Checked);
                //Odswiez dane na formatce po ustawieniu aktywnosci danej przeplywki
                if (hpt1000.GetPLC() != null)
                {
                    MFC aMFC = hpt1000.GetMFC();
                    RefreshMFC(aMFC);
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie usuniecie dango typu gazu
        private void btnRemoveGas_Click(object sender, EventArgs e)
        {
            // GasType gasType = (GasType)cBoxGasType.SelectedItem; //Pobierz aktulnie wybrany typ gazu

            if (selectedGas != null && gasTypes != null)
            {
                string gasName = selectedGas.Name;
                //Usun typ gazu z bazy danych
                if (gasTypes.Remove(selectedGas) == 0)
                {
                    //Ustaw selectedGas na poprzedzajacym Itemie
                    GasType newSelectedGas = null;
                    foreach (ListViewItem item in listViewGases.Items)
                    {
                        if (item.Tag == selectedGas)
                            break;
                        newSelectedGas = (GasType)item.Tag;
                    }
                    selectedGas = newSelectedGas;
                    //Usuniecie gazu z bazy danych sie powiodlo to odswiez comboboxa
                    FillComboBoxGases();
                    //W przypadku gdy nie ma juz zadnego typu gazu zeruj komponenty wizulane
                    if (selectedGas != null)
                    {
                        tBoxNameGas.Text = selectedGas.Name;
                        tBoxGasDescription.Text = selectedGas.Description;
                        dEditFactorGas.Value = selectedGas.Factor;
                    }
                    else
                    {
                        tBoxNameGas.Text = "";
                        tBoxGasDescription.Text = "";
                        dEditFactorGas.Value = 0;
                    }
                    Source.Logger.AddMsg(Source.Translate.GetText("Gas '" + gasName + "' has been removed from gases list"), Types.MessageType.Message);
                    MessageBox.Show(Source.Translate.GetText("Gas '" + gasName + "' has been removed from gases list"), "Remove gas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Source.Logger.AddMsg(Source.Translate.GetText("Can't remove gas. Gas hasn't been selected"), Types.MessageType.Error);
                MessageBox.Show(Source.Translate.GetText("Can't remove gas. Gas hasn't been selected"), "Remove gas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Funkcja timera majaca na celu ustawianie widocznosci przycoskow w zaleznosci od sytuacji
        private void timer_Tick(object sender, EventArgs e)
        {
            //Pokaz odpowiednie przyciski do zarzadzania gazami dostosowane do aktualnie wybranych itemow
            ShowCorrespondingButtonGases();

            if (hpt1000 != null)
            {
                //Brak komunikacji z PLC kasuj widzocznosc komponetow oraz ustaw wartosci na 0
                if (!hpt1000.CoonectionPLC)
                {
                    initValue = true;
                    ClearMFComponent();
                    ClearPowerSupplyComponent();
                    ClearPumpComponent();
                    ClearVaporiserComponent();
                    ClearGasesComponent();
                    ClearPidComponent();
                    ClearTimeComponent();
                    ClearMotorComponent();
                    ClearGaugeComponent();
                    initValue = false;
                }
                //Komunikacja sie pojawila pokaz aktualnie odczytane wartosci parametrow z PLC ale tylko raz. Musi to dzialc na zbocze narastajace
                if (!lastStateCommunication && hpt1000.CoonectionPLC)
                {
                    RefreshSettingsPanel();
                    SetEnableComponentMFC(true);
                    SetEnableComponentPowerSupply(true);
                    SetEnableComponentPump(true);
                    SetEnableComponentVaporiser(true);
                    SetEnableComponentGases(true);
                    SetEnableComponentPid(true);
                    SetEnableComponentTime(true);
                    SetEnableComponentMotor(true);
                    SetEnableComponentGauge(true);

                }
                lastStateCommunication = hpt1000.CoonectionPLC;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie pokazanie odpwiednich przysiskow do zarzadzania gzami w zaleznosci od tego co jest wybrane
         */
        private void ShowCorrespondingButtonGases()
        {
            bool btnSaveEnabled = false;
            bool btnRemoveEnabled = false;

            if (selectedGas != null && selectedGas.Changes)
                btnSaveEnabled = true;

            if (selectedGas != null)
                btnRemoveEnabled = true;

            if (btnSaveSettings.Enabled != btnSaveEnabled)
                btnSaveSettings.Enabled = btnSaveEnabled;
            if (btnRemoveGas.Enabled != btnRemoveEnabled)
                btnRemoveGas.Enabled = btnRemoveEnabled;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //Zdarzenie wywolywane w momencie ladowania panelu. W tym czasie odczytaj parametry urzadzenia z PLC i uzupelnij je na formatce
        private void ServicePanel_Load(object sender, EventArgs e)
        {
            //Odczytaj paramtry PLC
            if (hpt1000 != null)
                hpt1000.UpdateSettings();
            //Odseiez formtke
            RefreshSettingsPanel();
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxVaporaizerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetVaporizer() != null && !initValue)
            {
                Types.VaporaizerType type = (Types.VaporaizerType)Enum.Parse(typeof(Types.VaporaizerType), cBoxVaporaizerType.SelectedItem.ToString());
                hpt1000.GetVaporizer().SetTypeVaporaizer(type);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void timePressureStability_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetMFC() != null && !initValue)
                hpt1000.GetMFC().SetTimePressureStability((int)GetSecond(timePressureStability.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditKp_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetPID(Types.PID.Kp, (int)dEditKp.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditTi_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetPID(Types.PID.Ti, (int)dEditTi.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditTd_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetPID(Types.PID.Td, (int)dEditTd.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditTs_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetPID(Types.PID.Ts, (int)dEditTs.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditFiltr_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!hpt1000.GetMFC().SetPID(Types.PID.Filtr, (int)dEditFiltr.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void btnSetDate_Click(object sender, EventArgs e)
        {
            Source.ItemLogger err = new Source.ItemLogger();

            if (hpt1000 != null && hpt1000.Chamber != null)
                err = hpt1000.Chamber.SetDateTimePLC(datePLC.Value, timePLC.Value);

            if (!err.IsError())
                MessageBox.Show(err.GetText(), "PLC Date time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(err.GetText(), "PLC Date time", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            using (LinearGradientBrush br = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Gray, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(br, e.Bounds); //Wrysuj odpowidni kolor zakladki

                //Wyrysuj tekst
                SizeF sz = e.Graphics.MeasureString(tabControl2.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControl2.TabPages[e.Index].Text, e.Font, Brushes.White, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxEnableMotor1_Click(object sender, EventArgs e)
        {
            MotorDriver.Motor_1_Enable = cBoxEnableMotor1.Checked;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxEnableMotor2_Click(object sender, EventArgs e)
        {
            MotorDriver.Motor_2_Enable = cBoxEnableMotor2.Checked;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenia zmiany zaznaczenia Itema
         */
        private void listViewGases_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            /*    Console.WriteLine("Item - " + e.Item + e.Item.Selected);
                if (e.Item.Tag != selectedGas && e.Item.Selected && e.IsSelected && DateTime.Now.Ticks - 10000 > lastTime)
                {
                    if (selectedGas != null && selectedGas.Changes)
                    {
                        DialogResult res = DialogResult.No;
               //         res = MessageBox.Show(" Do you want discard gas changes ? Gas has been changed but not saved. ", "Gas changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                 //       lastTime = DateTime.Now.Ticks;
                        //Wyswietl dane gazu lub cofnij zaznaczenie
                        if (res == DialogResult.Yes)
                            selectedGas.ClearChanges();
                        else
                            e.Item.Selected = false;
                    }
                }
                if (listViewGases.SelectedItems.Count > 0 && listViewGases.SelectedItems[0].Tag != selectedGas && listViewGases.SelectedItems[0].Selected)
                {
                    if (listViewGases.SelectedItems.Count > 0)
                        selectedGas = (GasType)listViewGases.SelectedItems[0].Tag;
                    else
                        selectedGas = null;

                    if (selectedGas != null)
                    {
                        //Ustawiony zostal wezel gazu przedstaw jego parametry
                        tBoxGasDescription.Text = selectedGas.Description;
                        dEditFactorGas.Value = selectedGas.Factor;
                        tBoxNameGas.Text = selectedGas.Name;
                    }
                }
                listViewGases.Refresh();// RedrawItems(0, 0, false);
                */
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Obsluga zdarzenie zmiany gazu. SPrawdz czy czasem nie niezapisanych zmian akualnego gazu jezeli nie to pokaz parametry nowqgo gazu
         */
        private void listViewGases_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*   if (listViewGases.SelectedItems.Count > 0 && listViewGases.SelectedItems[0].Tag != selectedGas && listViewGases.SelectedItems[0].Selected)
               {
                   if (listViewGases.SelectedItems.Count > 0)
                       selectedGas = (GasType)listViewGases.SelectedItems[0].Tag;
                   else
                       selectedGas = null;

                   if (selectedGas != null)
                   {   
                       //Ustawiony zostal wezel gazu przedstaw jego parametry
                       tBoxGasDescription.Text = selectedGas.Description;
                       dEditFactorGas.Value = selectedGas.Factor;
                       tBoxNameGas.Text = selectedGas.Name;
                   }
               }
               */
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPressureLimitHV_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!(hpt1000.GetMFC().SetPressureLimitHV((float)dEditPressureLimitHV.Value)).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPressureLimitGas_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetMFC() != null)
            {
                if (!(hpt1000.GetMFC().SetPressureLimitGAS((float)dEditPressureLimitGas.Value)).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //------GRUPA FUNKCJI DO ZARZADZANIA WYGLADEM LIST VIEW DLA GAZOW--------------------------
        private void listViewGases_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            //Podswietl aktualnie wybrany gas
            if (e.Item.Tag == selectedGas)
            {
                // Draw the background and focus rectangle for a selected item.
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                //      e.DrawFocusRectangle();
            }
            else
            {
                //e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                //           e.DrawFocusRectangle();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void listViewGases_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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
                e.Graphics.DrawString(e.SubItem.Text, listViewGases.Font, Brushes.Green, e.Bounds, sf);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void listViewGases_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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
                using (Font headerFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }

                // Draw the background for an unselected item.
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.LightBlue, Color.SkyBlue, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                e.DrawText();
            }
            return;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void tBoxNameGas_TextChanged(object sender, EventArgs e)
        {
            if (selectedGas != null && !blockEvent)
                selectedGas.NameTmp = tBoxNameGas.Text;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void tBoxGasDescription_TextChanged(object sender, EventArgs e)
        {
            if (selectedGas != null && !blockEvent)
                selectedGas.DescriptionTmp = tBoxGasDescription.Text;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja zdarzenia majaca na celu ustawienie factora gazu
         */
        private bool dEditFactorGas_EnterOn()
        {
            bool aRes = false;

            if (selectedGas != null)
            {
                selectedGas.FactorTmp = dEditFactorGas.Value;
                aRes = true;
            }
            return aRes;
        }
        /**
         * Zadaniem metody jest ustawienie nowego SelectedGas i anaulowanie niezapisanych zmian na poprzednio aktuwlnym gazie
         */
        private void listViewGases_Click(object sender, EventArgs e)
        {
            if (listViewGases.SelectedItems.Count > 0 && listViewGases.SelectedItems[0].Tag != selectedGas)
            {
                if (selectedGas != null && selectedGas.Changes)
                {
                    DialogResult res = DialogResult.No;
                    res = MessageBox.Show(" Do you want discard changes for gas '" + selectedGas.Name + "' ? Gas has been changed but not saved. ", "Gas changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //Wyswietl dane gazu lub cofnij zaznaczenie
                    if (res == DialogResult.Yes)
                        selectedGas.ClearChanges();
                    else
                        listViewGases.SelectedItems[0].Selected = false;
                }
                //Jezeli mam przejsc do nowego Itemu to ustaw aktualny gaz
                if (listViewGases.SelectedItems.Count > 0)
                {
                    selectedGas = (GasType)listViewGases.SelectedItems[0].Tag;
                    if (selectedGas != null)
                    {
                        //Ustawiony zostal wezel gazu przedstaw jego parametry
                        tBoxGasDescription.Text = selectedGas.Description;
                        dEditFactorGas.Value = selectedGas.Factor;
                        tBoxNameGas.Text = selectedGas.Name;
                    }
                }
            }
            listViewGases.Refresh();// RedrawItems(0, 0, false);

        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxChamberGas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ustaw gas w komorze
            if (!blockEvent && cBoxChamberGas.SelectedItem != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000.Chamber != null)
            {
                GasType gasType = (GasType)cBoxChamberGas.SelectedItem;
                Source.Factory.Hpt1000.Chamber.SetGuageFactor(gasType);
                ShowGasFromChamber();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxGasCalibrated_MFC1_Click(object sender, EventArgs e)
        {
            //Ustaw gas w komorze
            if (!initValue && !blockEvent && cBoxGasCalibrated_MFC1.SelectedItem != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000.GetMFC() != null)
            {
                GasType gasType = (GasType)cBoxGasCalibrated_MFC1.SelectedItem;
                Source.ItemLogger aErr = Source.Factory.Hpt1000.GetMFC().SetCalibratedFactor(1, (float)gasType.Factor);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxGasCalibrated_MFC2_Click(object sender, EventArgs e)
        {
            //Ustaw gas w komorze
            if (!initValue && !blockEvent && cBoxGasCalibrated_MFC2.SelectedItem != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000.GetMFC() != null)
            {
                GasType gasType = (GasType)cBoxGasCalibrated_MFC2.SelectedItem;
                Source.ItemLogger aErr = Source.Factory.Hpt1000.GetMFC().SetCalibratedFactor(2, (float)gasType.Factor);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxGasCalibrated_MFC3_Click(object sender, EventArgs e)
        {
            //Ustaw gas w komorze
            if (!initValue && !blockEvent && cBoxGasCalibrated_MFC3.SelectedItem != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000 != null && Source.Factory.Hpt1000.GetMFC() != null)
            {
                GasType gasType = (GasType)cBoxGasCalibrated_MFC3.SelectedItem;
                Source.ItemLogger aErr = Source.Factory.Hpt1000.GetMFC().SetCalibratedFactor(3, (float)gasType.Factor);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditThresholdMixGas_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.Chamber != null)
            {
                if (!(hpt1000.Chamber.SetThresholdMixaGasPorc((int)(dEditThresholdMixGas.Value * 1000)).IsError()))
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxActiveProcMixGas_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.Chamber != null)
            {
                int value = 0;
                if (cBoxActiveProcMixGas.Checked)
                    value = 1;
                hpt1000.Chamber.SetActiveOptionMixaGasPorc(value);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void timeFlowStabilityMixGas_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.Chamber != null && !initValue)
                hpt1000.Chamber.SetTimeFlowStabilityMixGas(GetSecond(timeFlowStabilityMixGas.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPID_HV_Kp_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetPID(Types.PID.Kp, (int)dEditPID_HV_Kp.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPID_HV_Ti_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetPID(Types.PID.Ti, (int)dEditPID_HV_Ti.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPID_HV_Td_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetPID(Types.PID.Td, (int)dEditPID_HV_Td.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPID_HV_Ts_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetPID(Types.PID.Ts, (int)dEditPID_HV_Ts.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditPID_HV_Filtr_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                if (!hpt1000.GetPowerSupply().SetPID(Types.PID.Filtr, (int)dEditPID_HV_Filtr.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditRangeVoltageHV_EnterOn()
        {
            bool aRes = false;

            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                double value = dEditMinRangeVoltageHV.Value;
                if (value > hpt1000.GetPowerSupply().MaxRangeVolatgeHV)
                    value = dEditMaxRangeVoltageHV.Value - 0.001;

                if (!hpt1000.GetPowerSupply().SetRangeVoltageHV(value, Types.TypeRangeHV.Min).IsError())
                    aRes = true;

                if (aRes)
                {
                    ShowInfoAboutLimitPower(); //Ograniczenie max wartosci do jakiej mozna ustawic wyj analogowe ogranicza nam max moc zasilacza. Powiadom o tym usera aby byl swiadomy
                    RefreshPowerSupply(hpt1000.GetPowerSupply());
                }
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditMaxRangeVoltageHV_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
            {
                double value = dEditMaxRangeVoltageHV.Value;
                if (value < hpt1000.GetPowerSupply().MinRangeVolatgeHV)
                    value = dEditMinRangeVoltageHV.Value + 0.001;

                if (!hpt1000.GetPowerSupply().SetRangeVoltageHV(value, Types.TypeRangeHV.Max).IsError())
                    aRes = true;

                if (aRes)
                {
                    ShowInfoAboutLimitPower(); //Ograniczenie max wartosci do jakiej mozna ustawic wyj analogowe ogranicza nam max moc zasilacza. Powiadom o tym usera aby byl swiadomy
                    RefreshPowerSupply(hpt1000.GetPowerSupply());
                }
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void cBoxPID_HV_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetPowerSupply() != null)
                hpt1000.GetPowerSupply().SetPIDMode(cBoxPID_HV.Checked);

            SetEnableOptionPIDHV(cBoxPID_HV.Checked);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void dEditRangePower_Load(object sender, EventArgs e)
        {

        }
        //---------------------------------------------------------------------------------------------------------------------------
        private bool dEditSetpointPumpDown_EnterOn()
        {
            bool aRes = false;
            if (hpt1000 != null && hpt1000.GetForePump() != null && !initValue)
            {
                if (!hpt1000.GetForePump().SetPumpdownSetpoint(dEditSetpointPumpDown.Value).IsError())
                    aRes = true;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void timeMaxPumpDown_ValueChanged(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetForePump() != null && !initValue)
                hpt1000.GetForePump().SetMaxTimePumpdown(GetSecond(timeMaxPumpDown.Value));
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void rBtnPirani_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetGauge() != null && !initValue)
                hpt1000.GetGauge().SetGaugeType(Types.GaugeType.Pirani);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        private void rBtnBarotron_Click(object sender, EventArgs e)
        {
            if (hpt1000 != null && hpt1000.GetGauge() != null && !initValue)
                hpt1000.GetGauge().SetGaugeType(Types.GaugeType.Barotron);
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
