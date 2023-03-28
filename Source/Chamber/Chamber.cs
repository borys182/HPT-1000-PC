using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa opisuje komorę i wszystkie urzadzenia/zawory jakie ona posiada. Daje nam możwlisość sterowania recznego nastawami kolenych urzadzen
    /// </summary>
    /// 
    public class Chamber
    {
        private PLC                 plc     = null;                     ///< Referencja na obiekt sluzacy do komunikwoania sie z PLC

        private List<ChamberObject> objects = new List<ChamberObject>(); ///< Lista urzadzen zawartych prze komore ktroymi mozemy sterowac badz odczytac dane

        private int plcOperationStatus = 0; /*zmienna okresla stan pracy sterownika PLC. Pierwsze 4 bity okreslaja stan programu { 0 -RUN, 2 - STOP, 3-PAUSE} kolejne 4 okreslja powod dlaczego program stoi
                                             {0- Switch, 1- Remote Contact, 2-Remote operation from programing tool, 3-Interlna program instrucvtion, 4-ERROR}*/

        private DateTime timePLC = new DateTime();      ///< Czas sterownika PLC
        private DateTime datePLC = new DateTime();      ///< Data sterownika PLC
        private double   factor  = 0;                   ///< Zmienna przechowuje info na temat gazu jakim jest wypelniona komora. W zaleznosci od typu gazu sa brane pod uwage korekty odczytu glowicy

        private int      activeMixGasProtect    = 0;    ///< Zmienna okrels czy jest aktywna procedura chronioca komore przed mieszanka niebezpiecznych gazow
        private double   thresholdMixGasProtect = 4.75; ///< Prog zadzialania procedury chroniocej komore przed miesznka niebezpiecznych gazow. Jest to wartosc wyrazona w woltach i ktora ma byc odczytana z przeplywki
        private int      timeFlowStabilityMixGas = 5;   ///< Okreslenie czasu oczekiwania na stailizacje przeplywu dla procdury zabezpieczajcej komore przed miesznka wyvbuchowa gazow [s]
        //------------------------------------------------------------------------------------------------
        public GasType Gas
        {
            get
            {
                GasType gas = null;

                if (Factory.Hpt1000 != null && Factory.Hpt1000.GetGasTypes() != null)
                {
                    foreach (GasType gasType in Factory.Hpt1000.GetGasTypes().Items)
                    {
                        if (Math.Abs(gasType.Factor - factor) < 0.001)
                            gas = gasType;
                    }
                }
                return gas;
            }
        }
        //------------------------------------------------------------------------------------------------
        public DateTime TimePLC
        {
            get { return timePLC; }
        }
        //------------------------------------------------------------------------------------------------
        public DateTime DatePLC
        {
            get { return datePLC; }
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Konstruktor klasy. Tworzy koljene obiekty urzadzen i dodaje do listy
         */ 
        public Chamber()
        {
            objects.Add(new Valve());
            objects.Add(new ForePump());
            objects.Add(new MFC());
            objects.Add(new PowerSupplay());
            objects.Add(new Vaporizer());
            objects.Add(new PressureControl());
            objects.Add(new Interlock());
            objects.Add(new Maintenance());
            objects.Add(new MotorDriver(1));
            objects.Add(new MotorDriver(2));
            objects.Add(new Gauge());

            //Poniewz jedyny sposob na ustawienie daty i czasu to dodanie danej wartsci do aktualnej to na poczatku zeruje obie zmienn
            ClearDate();
            ClearTime();          
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda sluzy do odswiezenia aktualnych wartosc urzadzen komory odczytanyuch z PLC
         */ 
        public void UpdateData(int []aData)
        {
            //Aktualizuj status wykonywania programy przez PLC
            plcOperationStatus = aData[Types.OFFSET_STATUS_PLC];
            //aktualizuj dane obiektow
            foreach (ChamberObject aObject in objects)
                aObject.UpdateData(aData);          
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda sluzy do odswiezenia ustawien urzadzen komory odczutena z PLC
         */
        public void UpdateSettings(int[] aData, int[] aExtraData)
        {         
            UpdateDateTime(aData);   //AKTUALIZUJ date i czas odycztany z PLC
            ReadGaugeFactor(aData);//Odczytaj jaki jest rodzaj gazu w plc
            ReadThresholdMixGas(aData);//Odczytaj prog zadzialania procedury chronicej komore przed miesznka wybuchowa gazow
            ReadActiveProcMixGas(aData);//ODczytaj czy procedur chronica komore przed miesznka wychicjowa jest aktywna
            ReadTimeFlowStabilityMixGas(aData); ; //ODczytsj czas oczekiwania na przeplyw
            //aktualizuj dane obiektow
            foreach (ChamberObject aObject in objects)
            {
                aObject.UpdateSettingsData(aData);
                aObject.UpdateExtraSettingsData(aExtraData);
            }
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Metoda odczytuje z PLC jaki jest ustawiony rodzaj gazu w komorze bazujac na factorze
         */
        private void ReadGaugeFactor(int[] aData)
        {
            if (aData.Length > Types.OFFSET_GAUGE_FACTOR + 1)
                factor = Types.ConvertDWORDToDouble(aData, Types.OFFSET_GAUGE_FACTOR);
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Metoda odczytuje prog zadzialania procedury chronicej komore przed miesznka wybuchowa gazow
         */
        private void ReadThresholdMixGas(int[] aData)
        {
            if (aData.Length > Types.OFFSET_THRESHOLD_MIX_GAS)
                thresholdMixGasProtect = aData[Types.OFFSET_THRESHOLD_MIX_GAS] / 1000.0; ;// Types.ConvertDWORDToDouble(aData, Types.OFFSET_THRESHOLD_MIX_GAS);
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Metoda odczytaj czy procedur chronica komore przed miesznka wychicjowa jest aktywna
         */
        private void ReadActiveProcMixGas(int[] aData)
        {
            if (aData.Length > Types.OFFSET_ACTIVE_PROC_MIX_GAS )
                activeMixGasProtect = aData[Types.OFFSET_ACTIVE_PROC_MIX_GAS];
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Metoda odczytuje wartosc opuznienia czaswoego podczas sprawdzania czy jest przeplyw na przeplywce zabezpieczajacej komore przed miesznka wybuchowa gazow
         */
        private void ReadTimeFlowStabilityMixGas(int[] aData)
        {
            if (aData.Length > Types.OFFSET_TIME_FLOW_STABILITY_MIX_GAS)
                timeFlowStabilityMixGas = aData[Types.OFFSET_TIME_FLOW_STABILITY_MIX_GAS];
        }
        //------------------------------------------------------------------------------------------------
        /**
        * Meotda ma za zadanie ustawienie faktora gazu w komorze
        */
        public ItemLogger SetGuageFactor(GasType gasType)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_GAUGE_FACTOR), (float)gasType.Factor);
                aErr.SetErrorMXComponents(Types.EventType.SET_GAUGE_FACTOR, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                factor = gasType.Factor;

            return aErr;
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda ustawia referencje obiektu sluzacego do komunikowani sie ze sterownikiem PLC
         */
        public void SetPtrPLC(PLC aPLC)
        {
            plc = aPLC;
            foreach (ChamberObject aObject in objects)
                aObject.SetPonterPLC(aPLC);
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanie zwrocenie danego urzadzenia komory
         */
        public ChamberObject GetObject(Types.TypeObject typeObj)
        {
            ChamberObject aObject = null;
            foreach (ChamberObject obj in objects)
            {
                if (typeObj == Types.TypeObject.FM && obj is MFC)
                    aObject = obj;
                if (typeObj == Types.TypeObject.FP && obj is ForePump)
                    aObject = obj;
                if (typeObj == Types.TypeObject.HV && obj is PowerSupplay)
                    aObject = obj;
                if (typeObj == Types.TypeObject.VL && obj is Valve)
                    aObject = obj;
                if (typeObj == Types.TypeObject.VP && obj is Vaporizer)
                    aObject = obj;
                if (typeObj == Types.TypeObject.PC && obj is PressureControl)
                    aObject = obj;
                if (typeObj == Types.TypeObject.INT && obj is Interlock)
                    aObject = obj;
                if (typeObj == Types.TypeObject.MNT && obj is Maintenance)
                    aObject = obj;
                if (typeObj == Types.TypeObject.MTR1 && obj is MotorDriver && ((MotorDriver)obj).ID == 1)
                    aObject = obj;
                if (typeObj == Types.TypeObject.MTR2 && obj is MotorDriver && ((MotorDriver)obj).ID == 2)
                    aObject = obj;
                if (typeObj == Types.TypeObject.GAUGE && obj is Gauge)
                    aObject = obj;
            }
            return aObject;
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda zwraca liste wszystkoch urzadzen ktore wchodza w sklad komory
         */
        public List<ChamberObject> GetObjects()
        {
            return objects;
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda jest wykorzystywana do aktualizacji czasu i daty sterownika PLC
         */
        private void UpdateDateTime(int[] aData)
        {
            //Wyczysc aktulne wartosci
            ClearDate();
            ClearTime();
            //Odczytaj rok pamietajac o konwerko DCB na INT
            int aYear = ConvertBCDToINT((aData[Types.OFFSET_DATE_TIME_YER_MON] & 0xFF00) >> 8);
            if (aYear < 37)
                aYear += 2000;
            else
                aYear += 1990;
            //Odczytaj date i czas z PLC pamietajac o konwerji z DCB na INT. Date/time sa zapisane w trzech wordach jako wartosci kodu BCD
            datePLC = datePLC.AddYears (aYear - 1);
            datePLC = datePLC.AddMonths (ConvertBCDToINT(aData[Types.OFFSET_DATE_TIME_YER_MON] & 0x00FF) - 1);
            datePLC = datePLC.AddDays  (ConvertBCDToINT((aData[Types.OFFSET_DATE_TIME_DAY_HUR] & 0xFF00) >> 8) - 1);

            timePLC = timePLC.AddHours   (ConvertBCDToINT(aData[Types.OFFSET_DATE_TIME_DAY_HUR] & 0x00FF));
            timePLC = timePLC.AddMinutes(ConvertBCDToINT((aData[Types.OFFSET_DATE_TIME_MIN_SEC] & 0xFF00) >> 8));
            timePLC = timePLC.AddSeconds (ConvertBCDToINT(aData[Types.OFFSET_DATE_TIME_MIN_SEC] & 0x00FF));
        }
        //-------------------------------------------------------------------------------
        /*
         * Metoda pomocnicza zeruje wartosc daty 
         */
        private void ClearDate()
        {
            if(datePLC.Year > 1)
                datePLC = datePLC.AddYears(-datePLC.Year + 1);
            if (datePLC.Month > 1)
                datePLC = datePLC.AddMonths(-datePLC.Month + 1);
            if (datePLC.Day > 1)
                datePLC = datePLC.AddDays(-datePLC.Day + 1);
        }
        //-------------------------------------------------------------------------------
        /*
         * Metoda pomocnicza zeruje wartosc czasu
         */
        private void ClearTime()
        {
            timePLC = timePLC.AddHours(-timePLC.Hour);
            timePLC = timePLC.AddMinutes(-timePLC.Minute);
            timePLC = timePLC.AddSeconds(-timePLC.Second);
            //Aby mozna bylo wyswietlic date w komponencie data nie moze byc wczesniejsza niz 1789 wiec sztucznie dodade rok ktory i tak nie bedzie wyswietlany
            if (timePLC.Year < 2000)
                timePLC = timePLC.AddYears(2000);
        }
        //------------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanioe ustawieni czasu w sterowniku PLC
         */
        public ItemLogger SetDateTimePLC(DateTime aDatePLC, DateTime aTimePLC)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[3];

            if (plc != null)
            {          
                //Przygotuj dane do ustawienia w rejestrach po stronie PLC. Wszystkie dane sa przedstawiane w postacji kodu BCD
                int aYear = aDatePLC.Year - 2000;
                if (aYear < 0)
                    aYear = aDatePLC.Year - 1900;

                aData[0] = (GetBCD(aYear) << 8)            | GetBCD(aDatePLC.Month);
                aData[1] = (GetBCD(aDatePLC.Day) << 8)     | GetBCD(aTimePLC.Hour);
                aData[2] = (GetBCD(aTimePLC.Minute) << 8)  | GetBCD(aTimePLC.Second);

 
                int aExtCode = plc.WriteWords(Types.ADDR_DATE_TIME, 3, aData);
                //Ustaw flage w PLC ktora pozwoli ustawic odpowiednie rejestry w PLC
                plc.SetDevice(Types.ADDR_SET_DATE_TIME_PLC, 1);

                aErr.SetErrorMXComponents(Types.EventType.SET_DATE_TIME, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
            {
                    datePLC = aDatePLC;
                    timePLC = aTimePLC;
            }
            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia ustawianie setpointa prozni dla regulatora PID
         */
        public ItemLogger SetThresholdMixaGasPorc(int aThreshold)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aThreshold;

            if (plc != null)
            {
                int aCode = 0;
                aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_THRESHOLD_MIX_GAS), 1, aData);// plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings,Types.OFFSET_THRESHOLD_MIX_GAS), (float)aThreshold);
                aErr.SetErrorMXComponents(Types.EventType.SET_THERSHOLD_MIX_GAS, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Zadaniem metody jest zwrocenie wartosci progu zadzialania procedury chroniacej gazy
        */
        public double GetThresholdMixGasProc()
        {
            return thresholdMixGasProtect;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia ustawianie setpointa prozni dla regulatora PID
         */
        public ItemLogger SetActiveOptionMixaGasPorc(int aOption)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aOption;
            if (plc != null)
            {
                int aCode = 0;
                aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_ACTIVE_PROC_MIX_GAS), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_ACTIVE_PROC_MIX_GAS, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;           
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie wartosci czy procedura chronica gazy jest aktywowana
         */ 
        public int GetActiveOptionMixaGasPorc()
        {
            return activeMixGasProtect;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia ustawianie czasu oczekiwania na stabiliacje sie przeplywu pod zas procedury zabezpieczajaej komore przed miesznka wybuchwa gazow
         */
        public ItemLogger SetTimeFlowStabilityMixGas(int aTimeFlowStability)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];
            if (aTimeFlowStability > 3200)
                aTimeFlowStability = 3200;
            aData[0] = aTimeFlowStability;
            if (plc != null)
            {
                int aCode = 0;
                aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_TIME_FLOW_STABILITY_MIX_GAS), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_TIME_FLOW_STABILITY_MIX_GAS, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie wartosci czy procedura chronica gazy jest aktywowana
         */
        public int GetTimeFlowStabilityMixGas()
        {
            return timeFlowStabilityMixGas;
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie konwersje int na wartosc BCD np 10 = H10. Kod BCD to jest kod gdzie jednostkie i dziesatki sa podawane na oddzilenych tetradach
         */
        private int GetBCD(int aValue)
        {
            int aBCD = 0;

            int aUnit = aValue % 10;
            int aDec = aValue / 10;

            aBCD = aDec << 4 | aUnit;

            return aBCD;
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Metodea ma za zadanie konwersje liczby z kodu BCD na INT
         */
        int ConvertBCDToINT(int aBCD)
        {
            int aInt = 0;

            aInt = ((aBCD >> 4) & 0xF) * 10 + (aBCD & 0xF);

            return aInt;
        }
        //------------------------------------------------------------------------------------------------
    }
}
