using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa jest odpowiedzialna za reprezentowanie sterowania pojedyncza przeplywaka MFC w komorze badawczej. Sterowanie polega na ustawieniu przeplywu gazu.
    /// </summary>

    public class MFC_Channel : ChamberObject
    {
        private int      id              = 0;                ///< Powiazanie obiketu po stronie PC z obiektem po stronie PLC
        private bool     enabled         = false;            ///< Flaga okresla czy przplywka wchodzi w sklad danej konfiguracji komory 
        private int      actualFlow      = 0;                ///< Wartosc przeplywu wyrazona w sccm
        private int      gasTypeID       = 0;                ///< ID gazu z ktory jest podlaczony do danej przeplywki
        private int      totalFlow       = 0;                ///< Zmienna przechowuje sume przeplywu ze wszystkich przeplwywek 

        private int      setpoint        = 0;                ///< Wasrtc nastawy przeplywu jaka trzeba ustawic
        private int      percent_PID     = 0;                ///< Zmienna przechowuje wartosc procentowego udzialu danej przeplywki w rulowaniu cisniniena w komrze za pomoca regulatora PID
        //zmienne konfigurujace przeplywke
        private int      rangeVoltage = 10000;              ///< Okreslenie zakresu napieciowego pracy przeplywki
        private int      maxFlowCalibGas_sccm  = 10000;     ///< Okreslenie max przeplywu przeplywki wyrazonego w jednostkach sccm dla gazu skalibrowanego - podawanego przez producenta
        private int      maxFlowActualGas_sccm = 10000;     ///< Okreslenie max przeplywu przeplywki wyrazonego w jednostkach sccm dla aktualnego gazu - max zakres jest przeliczany bazujac na podanych faktora (obliczenia sa wykonywane w PLC)

        private Value     valueFlow     = new Value();   ///< Zmiena jest wykorzystywana do przenoszenia aktualnych wartosci odczytywanych z PLC do obiektu zapisujacego dane w bazie danych jako parametr urzadzenia MFC

        private bool     state = false;// Zmienna okresla czy przeplywka jest wlaczona - samo ustawineiu gazu nie powoduje przplywu trzeba jeszcze wlaczyc przeplywek

        private DB       dataBase       = null;
        string           paraName;                      ///< Zmienna jest wykorzystywana do zbiorczego zapisu/odczytu parametrow MFC so/z bazy danych

        double          factor              = 1;        ///< Wartosc okresla jaki jest aktualnie ustawiny faktor dla przplywki w PLC
        double          calibaratedFactor   = 1;        ///< Wartosc okresla jaki jest ustawiony faktor na ktorym zostala skalibrowana dan przeplywka

        public static int MAX_FLOW_MFC_1 = 1000;
        public static int MAX_FLOW_MFC_2 = 1000;
        public static int MAX_FLOW_MFC_3 = 1000;

        //-----------------------------------------------------------------------------------------
        /*
         * Metoda zwraca referencje na obiekt przechiwujacy aktualna wartosc przeplywu. Wykorzystywane do zapisu danych w bazie danych
         */
        public Value GetValueFlowPtr()
        {
            return valueFlow;
        }
        //-----------------------------------------------------------------------------------------
        /*
         * Konstrktor klasy
         */ 
        public MFC_Channel(int aID)
        {
            id = aID;
            paraName = "MFC" + id.ToString() + "_Parameter";
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca ID danej przeplwyki
         */ 
        public int GetID()
        {
            return id;
        }
        //-----------------------------------------------------------------------------------------
        public bool State
        {
            get { return state; }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda przypisuje dany gaz do przeplywki
         */
        public int GasTypeID
        {
            set
            {
                //gasTypeID = value;
                if (Factory.Hpt1000 != null && Factory.Hpt1000.GetGasTypes() != null)
                {
                    float factor = Factory.Hpt1000.GetGasTypes().GetFactor(value);
                    //Zapisz typ gazu w urzadzeniu
                    SetGasFactor(factor);
                }
                //SaveData();
            }
            get { return gasTypeID; }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia referencje bazy danych
         */
        public DB DataBase
        {
            set
            {
                dataBase = value;
                //Ustaw parametry przeplywki odczytane z bazy danych
                LoadData();
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja odczytuje z bazy danych zapisane wczesniej parametry
         */
        private void LoadData()
        {
            if (dataBase != null)
            {
                ProgramParameter parameter = new ProgramParameter();
                parameter.Name = paraName;
                dataBase.LoadParameter(parameter.Name,out parameter);
                if (parameter.Data != null)
                {
                    string[] parameters = parameter.Data.Split(';');
                    foreach (string para in parameters)
                    {
                        try
                        {
                    //        if (para.Contains("GasTypeID"))
                    //            gasTypeID = Convert.ToInt32(para.Split('=')[1]);
                        }
                        catch (Exception ex)
                        {
                            Logger.AddException(ex);
                        }
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja zapisuje do bazu danych informacje na temat powiazanego typu gazu oraz czy jest aktywna czy nie
        */
        private void SaveData()
        {
            ProgramParameter parameter = new ProgramParameter();

            parameter.Name = paraName;
            parameter.Data = "GasTypeID = " + gasTypeID.ToString() + ";";
            
            int aRes = dataBase.SaveParameter(parameter);
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda aktualizuj dane przeplywki odczytane z PLC
        */
        public override void UpdateData(int[] aData)
        {
            int aIndex_Flow = 0;
            int aIndex_Setpoint = 0;
            int aIndex_Factor = 0;
            int aIndex_ActualMaxFlow = 0;
            if (aData.Length > Types.OFFSET_ACTUAL_FLOW_1 && aData.Length > Types.OFFSET_ACTUAL_FLOW_2 && aData.Length > Types.OFFSET_ACTUAL_FLOW_3)
            {
                if (id == 1)
                {
                    aIndex_Flow     = Types.OFFSET_ACTUAL_FLOW_1;
                    aIndex_Setpoint = Types.OFFSET_SETPOINT_MFC1;
                    aIndex_Factor   = Types.OFFSET_MFC1_FACTOR;
                    aIndex_ActualMaxFlow = Types.OFFSET_ACTULA_RANGE_MFC1;
                }
                if (id == 2)
                {
                    aIndex_Flow     = Types.OFFSET_ACTUAL_FLOW_2;
                    aIndex_Setpoint = Types.OFFSET_SETPOINT_MFC2;
                    aIndex_Factor   = Types.OFFSET_MFC2_FACTOR;
                    aIndex_ActualMaxFlow = Types.OFFSET_ACTULA_RANGE_MFC2;
                }
                if (id == 3)
                {
                    aIndex_Flow     = Types.OFFSET_ACTUAL_FLOW_3;
                    aIndex_Setpoint = Types.OFFSET_SETPOINT_MFC3;
                    aIndex_Factor   = Types.OFFSET_MFC3_FACTOR;
                    aIndex_ActualMaxFlow = Types.OFFSET_ACTULA_RANGE_MFC3;
                }

                Types.GasProcesMode mode = Types.GasProcesMode.Unknown;
                if (Enum.IsDefined(typeof(Types.GasProcesMode), aData[Types.OFFSET_MODE_PRESSURE]))
                    mode = (Types.GasProcesMode)Enum.Parse(typeof(Types.GasProcesMode), (aData[Types.OFFSET_MODE_PRESSURE]).ToString()); // konwertuj int na Enum

                if (aData.Length > aIndex_Flow && aData.Length > aIndex_Setpoint && aData.Length > aIndex_Factor && aData.Length > Types.OFFSET_ACTUAL_FLOW_1 && aData.Length > Types.OFFSET_ACTUAL_FLOW_2 && aData.Length > Types.OFFSET_ACTUAL_FLOW_3)
                {
                    actualFlow = Types.ConvertWORDToInt(aData[aIndex_Flow]);
                    totalFlow  = aData[Types.OFFSET_ACTUAL_FLOW_1] + aData[Types.OFFSET_ACTUAL_FLOW_2] + aData[Types.OFFSET_ACTUAL_FLOW_3];
                    // W zaleznosci od trybu pracy regulatora gazu to pod tym samym adresem kryje sie aktualny setpoint badz procent udzialu w regulowaniu PID
                    if(mode == Types.GasProcesMode.Presure_MFC)
                        percent_PID = aData[aIndex_Setpoint];
                    else
                        setpoint = aData[aIndex_Setpoint];
                    factor = Types.ConvertDWORDToDouble(aData, aIndex_Factor);

                    if (Factory.Hpt1000 != null && Factory.Hpt1000.GetGasTypes() != null)
                        gasTypeID = Factory.Hpt1000.GetGasTypes().GetID(factor);

                    maxFlowActualGas_sccm = aData[aIndex_ActualMaxFlow];
                    if (id == 1) MAX_FLOW_MFC_1 = maxFlowActualGas_sccm;
                    if (id == 2) MAX_FLOW_MFC_2 = maxFlowActualGas_sccm;
                    if (id == 3) MAX_FLOW_MFC_3 = maxFlowActualGas_sccm;


                }
                //Aktualizuj wartosc przeplywu w obiekcie wykorzystywanym do zapisu aktualnje wartosci przeplywu w bazie danych
                valueFlow.Value_ = actualFlow;
            }
            if(aData.Length > Types.OFFSET_STATE_GAS_CTR)
            {
                int aControlGas = aData[Types.OFFSET_STATE_GAS_CTR];
                if (id > 0)
                    state = Convert.ToBoolean(aControlGas & (1 << (id - 1)));
            }
     
            base.UpdateData(aData);

            if (plc != null && plc.GetDummyMode())
                enabled = true;
        }
        //-----------------------------------------------------------------------------------------
        /**
          *  Funkcja ma za zadanie odczytanie ustawien przeplywek MFC z PLC. W sklad ustawien wchodza: aktywnosc, max wartosc przplywu oraz napiecie dla max przeplywu
          *  Jako parametr jest podawana tablica danych w ktorej pod okresolnymi indeksami sa zawarte okreslone dane zgodnie z OFFSETEM dla danego parametru
          */
        public override void UpdateSettingsData(int[] aData)
        {
            int aAddrRangeFlow = 0;
            int aAddrRangeVoltage = 0;
            int aEnabledMFC = 0;        //zmienna przechowuje zbiorcza informacje na temat aktywnosci wszustkicj przeplywek MFC w systemie. Info na temat aktywnosci zaszyte jest w kolejnych bitach {1 - ON; 0 - OFF}
            int aAddrCalibratedFactor = 0;

            aEnabledMFC = aData[Types.OFFSET_ENABLED_MFC];  //Odczytaj dane jakie sa w sterownika PLC
            //W zsleznosci od przeplywki odczytaj dane z odpowiedniego indeksu
            if (id == 1)
            {
                aAddrRangeFlow      = Types.OFFSET_RANGE_FLOW_MFC1;
                aAddrRangeVoltage   = Types.OFFSET_RANGE_VOLTAGE_MFC1;
                enabled             = Convert.ToBoolean(aEnabledMFC & 0x01) ;//wyluskaj informacje na temat tego czy przeplywka jest aktywna {sprawdz czy bit 0 jest ON czy OFF }
                aAddrCalibratedFactor = Types.OFFSET_MFC1_CALIBRATED_FACTOR;
            }
            if (id == 2)
            {
                aAddrRangeFlow      = Types.OFFSET_RANGE_FLOW_MFC2;
                aAddrRangeVoltage   = Types.OFFSET_RANGE_VOLTAGE_MFC2;
                enabled             = Convert.ToBoolean(aEnabledMFC & 0x02);//wyluskaj informacje na temat tego czy przeplywka jest aktywna {sprawdz czy bit 1 jest ON czy OFF }
                aAddrCalibratedFactor = Types.OFFSET_MFC2_CALIBRATED_FACTOR;
            }
            if (id == 3)
            {
                aAddrRangeFlow      = Types.OFFSET_RANGE_FLOW_MFC3;
                aAddrRangeVoltage   = Types.OFFSET_RANGE_VOLTAGE_MFC3;
                enabled             = Convert.ToBoolean(aEnabledMFC & 0x04);//wyluskaj informacje na temat tego czy przeplywka jest aktywna {sprawdz czy bit 2 jest ON czy OFF }
                aAddrCalibratedFactor = Types.OFFSET_MFC3_CALIBRATED_FACTOR;
            }

            if (aData.Length > aAddrRangeFlow && aData.Length > aAddrRangeVoltage && aData.Length > aAddrCalibratedFactor)
            {
                rangeVoltage = aData[aAddrRangeVoltage];
                maxFlowCalibGas_sccm = aData[aAddrRangeFlow];
                calibaratedFactor = Types.ConvertDWORDToDouble(aData,aAddrCalibratedFactor); 
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja umozliwia ustawianie dango przeplwyu na przeplywce
        */
        public ItemLogger SetFlow( double aValue, Types.UnitFlow aUnit)
        {
            ItemLogger aErr = new ItemLogger();

            int aValueSCCM = 0;
            //przlicz wartosc podana w danych jednostach na napiecie
            switch (aUnit)
            {
                case Types.UnitFlow.percent:
                    aValueSCCM = (int)(aValue / 100 * maxFlowActualGas_sccm) ;
                    break;
                case Types.UnitFlow.sccm:
                    aValueSCCM = (int)aValue;
                    break;
            }
            string aAddr = "";
            if(controlMode == Types.ControlMode.Automatic)
            {
                if (id == 1) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_FLOW_1_FLOW).ToString();
                if (id == 2) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_FLOW_2_FLOW).ToString();
                if (id == 3) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_FLOW_3_FLOW).ToString();
            }
            if (controlMode == Types.ControlMode.Manual)
            {
                if (id == 1) aAddr = Types.ADDR_FLOW_1_CTR;
                if (id == 2) aAddr = Types.ADDR_FLOW_2_CTR;
                if (id == 3) aAddr = Types.ADDR_FLOW_3_CTR;
            }

            if (aAddr.Length > 0)
            {
                int[] aData = new int[1];
                aData[0] = aValueSCCM;
                if (plc != null)
                {
                    int aExtCode = plc.WriteWords(aAddr, 1, aData);
                    aErr.SetErrorMXComponents(Types.EventType.SET_FLOW, aExtCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
            }
            else
                aErr.SetErrorApp(Types.EventType.BAD_FLOW_ID);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda zwraca aktualkny przeplyw wyrazony w sccm/%
        */
        public double GetActualFlow(Types.UnitFlow aUnit)
        {
            double aFlow = 0;
            switch (aUnit)
            {
                case Types.UnitFlow.sccm:
                    aFlow = actualFlow;
                    break;
                case Types.UnitFlow.percent: //Zwroc procentowy udzial danej przeplywki w calosciowym przeplywie gazu
                    if (/*maxFlow_sccm*/totalFlow > 0)
                        aFlow = (double)actualFlow / (double)totalFlow * 100;// maxFlow_sccm * 100.0;
                    break;
            }
            return aFlow;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje info czy dana przeplywka jest dostepna w systemie
         */
        public bool GetActive()
        {
            return enabled;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca max zakres przeplywu jaki moze zostac ustawiony na przeplywce dla skalibrowanego gazu
         */
        public int GetMaxCalibFlow()
        {
            return maxFlowCalibGas_sccm;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca max przeplyw jaki moze zostac ustawiony na przeplywce dla aktualnego gazu
         */
        public int GetMaxActualFlow()
        {
            return maxFlowActualGas_sccm;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca zakres napiecia pracy przeplywki
         */
        public int GeRangeVoltage()
        {
            return rangeVoltage;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda zwraca nastawe przeplywu
          */
        public double GetSetpoint(Types.UnitFlow aUnit)
        {           
            double aSetpoint = 0;

            switch (aUnit)
            {
                case Types.UnitFlow.sccm:
                    aSetpoint = setpoint;
                    break;
                case Types.UnitFlow.percent:
                    if (maxFlowActualGas_sccm > 0)
                        aSetpoint = (double)setpoint / (double)maxFlowActualGas_sccm * 100.0;
                    break;
            }
            return aSetpoint;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda zwraca wartosc procentowego udzialu w regulkowaniu cisneina w komorze
          */
        public double GetPercentPID()
        {           
            return percent_PID;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Ustaw max przeplyw jaki jest mozliwy do ustawienia dla danej przeplywki [sccm]
         */
        public ItemLogger SetMaxFlow(int aValue)
        {
            ItemLogger   aErr = new ItemLogger();
            int[]   aData = new int[1];
            int     aAddr = 0;

            if (id == 1) aAddr = Types.OFFSET_RANGE_FLOW_MFC1;
            if (id == 2) aAddr = Types.OFFSET_RANGE_FLOW_MFC2;
            if (id == 3) aAddr = Types.OFFSET_RANGE_FLOW_MFC3;

            aData[0] = aValue;
            if (plc != null)
            {
                int aExtCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, aAddr), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_MAX_FLOW, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
    
            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
         * Ustaw wartosc max napieica sterujacego dana przeplywka [mV]
         */
        public ItemLogger SetRangeVoltage(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];
            int aAddr = 0;

            if (id == 1) aAddr = Types.OFFSET_RANGE_VOLTAGE_MFC1;
            if (id == 2) aAddr = Types.OFFSET_RANGE_VOLTAGE_MFC2;
            if (id == 3) aAddr = Types.OFFSET_RANGE_VOLTAGE_MFC3;

            aData[0] = aValue;
            if (plc != null)
            {
                int aExtCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, aAddr), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_RANGE_VOLTAGE_MFC, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                rangeVoltage = aValue;

            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
         * Funkcja ma za zadanie ustawienie stanu (aktywna/nieaktywna) danej przeplywki w PLC. Informacja ta przez PLC jest zawarta w jednej zmiennej i kazda przplywka posiada jeden bit do koreslania swojego stanu.
         *  Wazne jest to aby podczas ustawiania zmienac tylko jeden bit a nie wyszstkie. Dlatego przed zapisem nalezy odczytac wartosc calego slowa
         */
        public bool SetActive(bool aState)
        {
            ItemLogger  aErr    = new ItemLogger(); //zmienna wykorzystywana do przechowywania info o wyniku zapisu/doczytu danych z PLC
            int[]       aData   = new int[1];           //zmienna wykorzystywana do przechowyanai danych odczytanych/zapisanych do PLC
            int         aAddr   = Types.OFFSET_ENABLED_MFC;   //zmienna zawiera offset adresu z przestrzeni Settings gdzie zawarta jest informacja o aktywnosci przeplywek w PLC

            //Jezeli posiadam wskaznik na obiekt PLC to odczytaj/zapisz wymagane dane
            if (plc != null && id >= 1 && id <= 3)
            {
                //Odczytaj informacje na temat aktywnosci wszystkich przeplwyek MFC aby moc zmienic tylko jeden bit
                int aRes = plc.ReadWords(Types.GetAddress(Types.AddressSpace.Settings, aAddr), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_ENABLED_MFC, aRes);
                //Jezeli dane zostaly poprawnie odebtrane to zapisz aktywnosc danej przeplwyki
                if (!aErr.IsError())
                {
                    aData[0] = aData[0] & (~(0x01 << (id - 1))); //Wyzeruj dany bit
                    aData[0] = aData[0] | (Convert.ToInt16(aState) << (id - 1));    //Ustaw dany bit na 0 badz 1
                    //Zapisz dane w PLC
                    aRes = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, aAddr), 1, aData);
                    //Na podstawie wyniku operacji z PLC ustaw informacje o wyniku cale=j operacji ustawiania aktywnosci przeplwyki
                    aErr.SetErrorMXComponents(Types.EventType.SET_ENABLED_MFC, aRes);
                }
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                enabled = aState;

            return !aErr.IsError();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie stanu wlaczenia/wylaczenia przpleywu gazu. Pamietaj ze z jednej zmienen sterujemy wszystkimi gazami. Sterpowanie odbywa sie bitowo parami bitow (1 - OFF , 2 - ON) dla kazdej z przeplywki
         */
        public ItemLogger SetState(bool aState)
        {
            ItemLogger aErr = new ItemLogger();
            if (plc != null && id > 0)
            {
                int aCode = 0;
                int[] aData = new int[1];
                aData[0] = 0;
                if (aState)
                    aData[0] = 0x2 << ((id - 1) * 2);   //stan 10 wlacza
                else
                    aData[0] = 0x1 << ((id - 1) * 2);   //Stan 01 - wlacza

                if (controlMode == Types.ControlMode.Manual)
                {
                    aCode = plc.WriteWords(Types.ADDR_GAS_CONTROL, 1, aData);
                    aErr.SetErrorMXComponents(Types.EventType.GAS_CONTROL, aCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.GAS_CONTROL);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            Logger.AddError(aErr);
            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie stanu wlaczenia/wylaczenia przpleywu gazu. Pamietaj ze z jednej zmienen sterujemy wszystkimi gazami. Sterpowanie odbywa sie bitowo parami bitow (1 - OFF , 2 - ON) dla kazdej z przeplywki
         */
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja umozliwia ustawianie dango przeplwyu na przeplywce
        */
        public ItemLogger SetPercentPID(float aValue)
        {
            ItemLogger aErr = new ItemLogger();

            //Wyznacz adres komorki gdzie alezy zapisac wartosc do PLC
            string aAddr = "";
            if (controlMode == Types.ControlMode.Automatic)
            {
                if (id == 1) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_FLOW_1_SHARE).ToString();
                if (id == 2) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_FLOW_2_SHARE).ToString();
                if (id == 3) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_FLOW_3_SHARE).ToString();
            }
            if (controlMode == Types.ControlMode.Manual)
            {
                if (id == 1) aAddr = Types.ADDR_MFC1_PERCENT_CONTROL;
                if (id == 2) aAddr = Types.ADDR_MFC2_PERCENT_CONTROL;
                if (id == 3) aAddr = Types.ADDR_MFC3_PERCENT_CONTROL;
            }

            if (aAddr.Length > 0)
            {
                int[] aData = new int[1];
                aData[0] = (int)aValue;
                if (plc != null)
                {
                    int aExtCode = plc.WriteWords(aAddr, 1, aData);
                    aErr.SetErrorMXComponents(Types.EventType.SET_FLOW, aExtCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
            }
            else
                aErr.SetErrorApp(Types.EventType.BAD_FLOW_ID);

            return aErr;
        }

        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie factora kalibracji dnaej przplywki
         */
        public ItemLogger SetCalibratedFactor(double aFactor)
        {
            ItemLogger aErr = new ItemLogger();
            string aAddr = "";
            if (id == 1) aAddr = Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_MFC1_CALIBRATED_FACTOR);
            if (id == 2) aAddr = Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_MFC2_CALIBRATED_FACTOR);
            if (id == 3) aAddr = Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_MFC3_CALIBRATED_FACTOR);

            if (plc != null && id > 0)
            {
                int aCode = plc.WriteRealData(aAddr,(float)aFactor);
                aErr.SetErrorMXComponents(Types.EventType.SET_MFC_CALIBRSTED_FACTOR, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            Logger.AddError(aErr);
            return aErr;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zworcenie wartosci factora kalibracji jaki posiada dana przplywka oraz id gazu ktory odpoaiada danej wartosci faktroa
         */ 
        public double GetCalibratedFactor()
        {
            return calibaratedFactor;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie factora dnaej przplywki w PLC
         */
        public ItemLogger SetGasFactor(double aFactor)
        {
            ItemLogger aErr = new ItemLogger();
            string aAddr = "";
            if (id == 1) aAddr = Types.ADDR_MFC1_FACTOR_CONTROL;
            if (id == 2) aAddr = Types.ADDR_MFC2_FACTOR_CONTROL;
            if (id == 3) aAddr = Types.ADDR_MFC3_FACTOR_CONTROL;

            if (plc != null && id > 0)
            {
                int aCode = plc.WriteRealData(aAddr, (float)aFactor);
                aErr.SetErrorMXComponents(Types.EventType.SET_MFC_FACTOR, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            Logger.AddError(aErr);
            return aErr;
        }
        //-------------------------------------------------------------------------------------
    }
    //-----------------------------------------------------------------------------------------
    //------------------------------------MFC-------------------------------------------------
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// Klasa jest odpowiedzialna za reprezentowanie sterowania przeplywakami MFC w komorze badawczej. Sterowanie polega na ustawieniu przeplywu gazu.
    /// </summary>

    public class MFC : ChamberObject
    {
        private List<MFC_Channel>   flowMeters          = new List<MFC_Channel>();

        private int                 timeFlowStability       = 30; ///< Czas oczekiwania na stablizicacje przeplywu po ustawineiu setpointa
        private int                 timePressureStability   = 10; ///< Czas oczekiwania na stabilizacje sie cisnienia w komorzez zanim zaczne sprawdzac warunki podczas regulacji cisnia za pomoca regulatora PID
        
        //Parametry regulatora PID
        private int                 pid_Kp      = 0;
        private int                 pid_Ti      = 0;
        private int                 pid_Td      = 0;
        private int                 pid_Ts      = 0;
        private int                 pid_Filtr   = 0;

        private double              pressureLimiGAS = 0;
        private double              pressureLimiHV  = 0;

        //-----------------------------------------------------------------------------------------
        public int TimeFlowStability
        {
            get { return timeFlowStability; }
        }
        //-----------------------------------------------------------------------------------------
        public int TimePressureStability
        {
            get { return timePressureStability; }
        }
        //-----------------------------------------------------------------------------------------
        public int PID_Kp
        {
            get { return pid_Kp; }
        }
        //-----------------------------------------------------------------------------------------
        public int PID_Ti
        {
            get { return pid_Ti; }
        }
        //-----------------------------------------------------------------------------------------
        public int PID_Td
        {
            get { return pid_Td; }
        }
        //-----------------------------------------------------------------------------------------
        public int PID_Ts
        {
            get { return pid_Ts; }
        }
        //-----------------------------------------------------------------------------------------
        public int PID_Filtr
        {
            get { return pid_Filtr; }
        }
        //-----------------------------------------------------------------------------------------
        public double PressureLimitGas
        {
            get { return pressureLimiGAS; }
        }
        //-----------------------------------------------------------------------------------------
        public double PressureLimitHV
        {
            get { return pressureLimiHV; }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy
         */
        public MFC()
        {
            flowMeters.Add(new MFC_Channel(1));
            flowMeters.Add(new MFC_Channel(2));
            flowMeters.Add(new MFC_Channel(3));

            //Uzupelnij liste parametrow ktore powinny byc zapisywane w bazi danych
            AddParameter("MFC1 Flow", flowMeters[0].GetValueFlowPtr(),"sccm");
            AddParameter("MFC2 Flow", flowMeters[1].GetValueFlowPtr(),"sccm");
            AddParameter("MFC3 Flow", flowMeters[2].GetValueFlowPtr(),"sccm");
            //Ustaw nazwe urzadzenia - pamietaj ze musi ona byc unikalna dla calego systemu
            name = "MFC";

            acqData = true; //Ustawiam flage ze urzadzenie jest przenzaczone do arachiwzowania danych w nbazie danych
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referecne przeplywki o podanym ID
         */ 
        private MFC_Channel GetMFC_Channel(int aId)
        {
            MFC_Channel mfc = null;
            foreach(MFC_Channel mfc_Channel in flowMeters)
            {
                if (mfc_Channel.GetID() == aId)
                    mfc = mfc_Channel;
            }
            return mfc;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda odczytuje aktualny przeplyw dla wszystkich przeplywek
         */
        override public void UpdateData(int[] aData)
        {
            //z PLC dostaje DWORD ktorego nalezy przekonwertowac na double
            foreach(MFC_Channel mfc in flowMeters)
            {
                if (mfc != null)
                    mfc.UpdateData(aData);
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda aktualizuje informacje na temat ustawien przeplywek oraz regulatroa PID
       */
        public override void UpdateSettingsData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_TIME_FLOW_STABILITY)
                timeFlowStability       = aData[Types.OFFSET_TIME_FLOW_STABILITY];
            if (aData.Length > Types.OFFSET_TIME_STABILITY_PRE)
                timePressureStability = aData[Types.OFFSET_TIME_STABILITY_PRE];
            if (aData.Length > Types.OFFSET_PID_Kp)
                pid_Kp = aData[Types.OFFSET_PID_Kp];
            if (aData.Length > Types.OFFSET_PID_Ti)
                pid_Ti = aData[Types.OFFSET_PID_Ti];
            if (aData.Length > Types.OFFSET_PID_Td)
                pid_Td = aData[Types.OFFSET_PID_Td];
            if (aData.Length > Types.OFFSET_PID_Ts)
                pid_Ts = aData[Types.OFFSET_PID_Ts];
            if (aData.Length > Types.OFFSET_PID_FILTR)
                pid_Filtr = aData[Types.OFFSET_PID_FILTR];
            if (aData.Length > Types.OFFSET_LIMIT_PRESSURE_GAS)
                pressureLimiGAS = Types.ConvertDWORDToDouble(aData, Types.OFFSET_LIMIT_PRESSURE_GAS);
            if (aData.Length > Types.OFFSET_LIMIT_PRESSURE_HV)
                pressureLimiHV = Types.ConvertDWORDToDouble(aData, Types.OFFSET_LIMIT_PRESSURE_HV);


            foreach (MFC_Channel mfc in flowMeters)
            {
                if (mfc != null)
                    mfc.UpdateSettingsData(aData);
            }
            base.UpdateSettingsData(aData);
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja umozliwia ustawianie dango przeplwyu na przeplywce
        */
        public ItemLogger SetFlow(int aId , float aValue , Types.UnitFlow aUnit)
        {
            ItemLogger aErr = new ItemLogger();

            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aErr = mfc_Channel.SetFlow(aValue, aUnit);
               
            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja umozliwia ustawianie wartosci procentowego udzialu danej przpelywki w regulowaniu cisnienia
        */
        public ItemLogger SetPercentPID(int aId, float aValue)
        {
            ItemLogger aErr = new ItemLogger();
            //Pobierz obiekt przeplywki
            MFC_Channel mfc_channel = GetMFC_Channel(aId);
            int countActiveMFC = 0;
            //Zmien aktywnym przeplywka wartosc procentowego PID o roznice zmiany obiektu docelowego
            double[] calculateValuePercent = GetCalculatePercent(mfc_channel, aValue,out countActiveMFC);

            //Ustaw obliczone wartosci dla pozostalych kanałów
            int index = 0;
            foreach (MFC_Channel mfc_Ch in flowMeters)
            {
                if (mfc_Ch.GetActive() && mfc_Ch != mfc_channel)
                    mfc_Ch.SetPercentPID((float)calculateValuePercent[index]);
             
                   index++;
            }

            //Jest tylko jedna aktywna przeplywka wiec nie moze byc innej wartosci niz 100
            if (countActiveMFC == 1)
                aValue = 100;

            if (mfc_channel != null)
                aErr = mfc_channel.SetPercentPID(aValue);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie wyznaczenie wartosci procent jakie nalezy ustawic w pozostalych przeplwywakch tak aby suma dala 100%
         */
        private double[] GetCalculatePercent(MFC_Channel mfc_channel, double targetValue, out int countActiveMFC)
        {
            double[]    calculateValuePercent   = new double[5];
            bool[]      tabSetValue             = new bool[5];  //tablica przechowuje info ze wartosc w danej przplywce zostala juz zapisana i nie nalezy jejz zmieniac
            int         index = 0;
            //Oblicz ile jest aktywnych przpelywek oraz przypsiz aktualne wartosci procent do zmm lokalnej
            countActiveMFC = 0;
            foreach (MFC_Channel mfc_Ch in flowMeters)
            {
                if (mfc_Ch.GetActive())
                    countActiveMFC++;
                calculateValuePercent[index] = mfc_Ch.GetPercentPID();
                tabSetValue[index] = false;
                index++;
            }
            //Oblicz o ile sie zmieni wartosc nastawy dla danej przeplwyki
            double differValue = mfc_channel.GetPercentPID() - targetValue;
            double tmp = 1; //Okreslam czy mam zmniejszyc wartosc przy obliczaniu difera. Jezeli zmianiam wartosc na rzecz nieaktywenj przpelywki to nie powinineime zmniejszycz tej wartosci
            if (!mfc_channel.GetActive())
                tmp = 0;
            //Wyznaczam wartosc o jaka nalezy zwiekszyc/zmniejszyc pozostale procenty przeplwywek aby suma byla 100 poprzez rownomierne podzielenie wartosci zmiany nastawy
            if (countActiveMFC > 1)
                differValue = differValue / (countActiveMFC - tmp);
            //Sprawdz czy dla kazdej przeplywki jest mozliwa zminana jej wartosci o wartosc differValue (wartosc przpelywek nie moze byc mniejsza niz 0)
            int     counterMFC          = countActiveMFC - 1; //zmienna pomocnicza pozwalajaca ustalic na ile kanalow nalezy rozdzielic nadwyzke
            double  notAllocatedDiffer  = 0; //wartosc nadwyzki
            while (true)
            {
                if(counterMFC > 0)
                    differValue += notAllocatedDiffer / counterMFC;
                notAllocatedDiffer = 0;
                index = 0;
                foreach (MFC_Channel mfc_Ch in flowMeters)
                {
                    if (mfc_Ch.GetActive() && mfc_Ch != mfc_channel && !tabSetValue[index])
                    {
                        tmp = calculateValuePercent[index] + differValue;
                        calculateValuePercent[index] += differValue;
                        //Sprawdz czy wartosc sie miesci w widelkach 0-100%. Jezeli nie to wpisz tylko te wartosci do 0 - 100 a reszta zwroc w diferze
                        if (tmp < 0)
                        {
                            calculateValuePercent[index] = 0;
                            notAllocatedDiffer += tmp;//zwieksz differ o nie przyspiana wartosc
                            counterMFC--;
                            tabSetValue[index] = true;
                        }
                        if (tmp > 100)
                        {
                            calculateValuePercent[index] = 100;
                            notAllocatedDiffer += 100 - tmp;//zwieksz differ o nie przyspiana wartosc
                            counterMFC--;
                            tabSetValue[index] = true;
                        }
                    }
                    if (mfc_Ch == mfc_channel)
                        calculateValuePercent[index] = targetValue;

                    index++;
                }
                differValue = 0;
                if (counterMFC <= 0 || notAllocatedDiffer == 0)
                    break;
            }
            //Sprawdz na koniec czy suma wszystkich jest 100 % jezeli nie to zwieksz tak aby byla 100
            double sum = 0;
            for (int i = 0; i < flowMeters.Count; i++)
                if(flowMeters[i].GetActive())
                    sum += calculateValuePercent[i];

            differValue = 0;
            if (sum > 0 && sum < 100)
                differValue = (100 - sum) / (countActiveMFC - 1);

            for (int i = 0; i < calculateValuePercent.Length; i++)
                calculateValuePercent[i] += differValue;              
            
            return calculateValuePercent;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda zwraca wartosc nastawy przeplywu danej przeplywki
       */
        public double GetSetpoint(int aId, Types.UnitFlow aUnit)
        {
            double aSetpoint = 0;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aSetpoint = mfc_Channel.GetSetpoint(aUnit);

            return aSetpoint;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda zwraca aktualny przeplyw danej przeplywki
       */
        public double GetActualFlow(int aId , Types.UnitFlow aUnit)
        {
            double      actualFlow      = 0;
            MFC_Channel mfc_Channel     = GetMFC_Channel(aId);

            if(mfc_Channel != null)
                actualFlow = mfc_Channel.GetActualFlow(aUnit);

            return actualFlow;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda zwraca wartosc procentowego udzialu przpelywki  w rugolowaiu cisniea w komorze
        */
        public double GetPercentPID(int aId)
        {
            double percentPID = 0;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                percentPID = mfc_Channel.GetPercentPID();

            return percentPID;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda ustawia aktywnosc przeplywki
       */
        public bool SetActive(int aId, bool aState)
        {
            bool aRes = false;

            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aRes = mfc_Channel.SetActive(aState);

            //Wywolaj funkcje ktora przeliczy procentowe nastawy dostosowujac je do aktualnej konfigruacji przeplwywek MFC
            SetPercentPID(aId, 0);
            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda zwraca info czy dana przeplywka jest aktywna w systemie
       */
        public bool GetActive(int aId)
        {
            bool aEnabled = false;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aEnabled = mfc_Channel.GetActive();

            return aEnabled;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda zwraca max przeplyw jaki mozna ustawic na danej przplywce dla aktul;anego gazu
        */
        public double GetMaxActualFlow(int aId)
        {
            double aMaxFlow = 0;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aMaxFlow = mfc_Channel.GetMaxActualFlow();

            return aMaxFlow;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda zwraca max zakres jaki mozna ustawic na danej przplywce dla skalibrowanego gazu
        */
        public int GetMaxCalibFlow(int aId)
        {
            int aMaxFlow = 0;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aMaxFlow = mfc_Channel.GetMaxCalibFlow();

            return aMaxFlow;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda zwraca zakres napiecia pracy przepkywki
       */
        public double GetRangeVoltage(int aId)
        {
            double aRangeVoltage = 0;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aRangeVoltage = mfc_Channel.GeRangeVoltage();

            return aRangeVoltage;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda ustawia max przeplyw jaki moze byc na danej przpleywce
       */
        public ItemLogger SetMaxFlow(int aId,int aMaxFlow)
        {
            ItemLogger aErr = new ItemLogger();
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aErr = mfc_Channel.SetMaxFlow(aMaxFlow);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda ustawia zakres pracy napiecia danej przeplywki
       */
        public ItemLogger SetRangeVoltage(int aId, int aRangeVoltage)
        {
            ItemLogger aErr = new ItemLogger();

            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aErr = mfc_Channel.SetRangeVoltage(aRangeVoltage);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda ustawia referencje obiektu do komunikownai sie z PLC
        */
        public override void SetPonterPLC(PLC ptrPLC)
        {
            plc = ptrPLC;
            foreach (MFC_Channel mfc in flowMeters)
            {
                if (mfc != null)
                    mfc.SetPonterPLC(ptrPLC);
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Ustaw czas oczekiwania na stabilizacje sie wartosc przeplywu poiedzy zadanymi widelkami programu
         */
        public ItemLogger SetTimeFlowStability(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aExtCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_TIME_FLOW_STABILITY), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_TIME_FLOW_STABILITY, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                timeFlowStability = aValue;

            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
        * Metoda przypisuje gaz do przeplywki
        */
        public void SetGasType(int aId, int gasTypeID)
        {
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                mfc_Channel.GasTypeID = gasTypeID;
        }
        //------------------------------------------------------------------------------------------- 
        /**
       * Metoda zwraca info jaki gaz jest powiazany z dana przeplwyka
       */
        public int GetGasType(int aId)
        {
            int gasTypeID = 0;

            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                gasTypeID = mfc_Channel.GasTypeID;

            return gasTypeID;
        }
        //------------------------------------------------------------------------------------------- 
        /**
        * Metoda ustawia referencje na obiekt bazy danych
        */
        public void SetDataBase(DB dataBase)
        {
            if (GetMFC_Channel(1) != null) GetMFC_Channel(1).DataBase = dataBase;
            if (GetMFC_Channel(2) != null) GetMFC_Channel(2).DataBase = dataBase;
            if (GetMFC_Channel(3) != null) GetMFC_Channel(3).DataBase = dataBase;
        }
        //------------------------------------------------------------------------------------------- 
        /**
        * Ustaw czas oczekiwania na stabilizacje sie wartosc cisnienia w procedurze kontroli cisnia za pomoca regulatora PID
        */
        public ItemLogger SetTimePressureStability(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aExtCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_TIME_STABILITY_PRE), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_TIME_STABILITY_PRESSURE, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                timePressureStability = aValue;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda ustawia parametru regulatora PID w sterowniku
       */
        //
        public ItemLogger SetPID(Types.PID aKindPara, int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aOffset = 0;
                switch(aKindPara)
                {
                    case Types.PID.Kp:
                        aOffset = Types.OFFSET_PID_Kp;
                        break;
                    case Types.PID.Ti:
                        aOffset = Types.OFFSET_PID_Ti;
                        break;
                    case Types.PID.Td:
                        aOffset = Types.OFFSET_PID_Td;
                        break;
                    case Types.PID.Ts:
                        aOffset = Types.OFFSET_PID_Ts;
                        break;
                    case Types.PID.Filtr:
                        aOffset = Types.OFFSET_PID_FILTR;
                        break;
                }
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, aOffset),1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_PID, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
            {
                switch (aKindPara)
                {
                    case Types.PID.Kp:
                        pid_Kp = aValue;
                        break;
                    case Types.PID.Ti:
                        pid_Ti = aValue;
                        break;
                    case Types.PID.Td:
                        pid_Td = aValue;
                        break;
                    case Types.PID.Ts:
                        pid_Ts = aValue;
                        break;
                    case Types.PID.Filtr:
                        pid_Filtr = aValue;
                        break;
                }
            }
   
            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        public bool GetState(int aId)
        {
            bool aState = false;

            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aState = mfc_Channel.State;

            return aState;
        }
        //-----------------------------------------------------------------------------------------
        /**
       * Metoda ustawia wlaczenie/wylaczenie przeplywu gazu na danej przeplywce
       */
        public ItemLogger SetState(int aId, bool aState)
        {
            ItemLogger  aErr        = new ItemLogger();
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aErr = mfc_Channel.SetState(aState);

            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
        * Ustaw limit gazu do poprawnej prazy zasilacza HV
        */
        public ItemLogger SetPressureLimitHV(float aPressureLimit)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[2];

            aData[0] = Types.ConvertDOUBLEToWORD(aPressureLimit, Types.Word.LOW);
            aData[1] = Types.ConvertDOUBLEToWORD(aPressureLimit, Types.Word.HIGH);

            if (plc != null)
            {
                int aExtCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_LIMIT_PRESSURE_HV), 2, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_PRESSURE_LIMIT_HV, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                pressureLimiHV = aPressureLimit;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Ustaw limit gazu do poprawnej prazy gazow
        */
        public ItemLogger SetPressureLimitGAS(float aPressureLimit)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[2];

            aData[0] = Types.ConvertDOUBLEToWORD(aPressureLimit, Types.Word.LOW);
            aData[1] = Types.ConvertDOUBLEToWORD(aPressureLimit, Types.Word.HIGH);

            if (plc != null)
            {
                int aExtCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_LIMIT_PRESSURE_GAS), 2, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_PRESSURE_LIMIT_GAS, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                    pressureLimiGAS = aPressureLimit;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Ustaw faktor gazu dla jakiego zostala skalibrowana dana przeplywka
        */
        public ItemLogger SetCalibratedFactor(int aId, float aCalibratedFactor)
        {
            ItemLogger aErr = new ItemLogger();
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aErr = mfc_Channel.SetCalibratedFactor(aCalibratedFactor);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Zwroc faktor kalibracyjny przeplywki
        */
        public float GetCalibratedFactor(int aId)
        {
            float aFactor = 0; ;
            MFC_Channel mfc_Channel = GetMFC_Channel(aId);

            if (mfc_Channel != null)
                aFactor = (float)mfc_Channel.GetCalibratedFactor();

            return aFactor;
        }
        //-----------------------------------------------------------------------------------------
    }
}


  
