using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje zasilacz do wytwarzania plazmy w komorze. Zasilacz jest defioniowany jako aktualna wartosc mocy/napiecia/pradu stan operate/HV
    /// </summary>
    public class PowerSupplay : ChamberObject
    {
        //stan zasilacza
        private Types.StateHV   state       = Types.StateHV.Error;  ///< Stan zasilacza ON/OFF/Error
        private Types.ModeHV    mode        = Types.ModeHV.Power;   ///< Typ pracy zasilacza
        private double          power       = 0;                    ///< Aktaulna wartosc mocy zasilacza
        private double          voltage     = 0;                    ///< Aktualna wartosc napiecia zasilacza
        private double          curent      = 0;                    ///< Aktualna wartosc pradu
        private double          setpoint    = 0;                    ///< Wartosc nastawy jaka powinna zostac ustawiona w zasliaczu

        //Utworz wartosci ktore beda wykorzystywane do aktualizacji danych w bazie danych
        Value powerValue    = new Value();
        Value powerPercent  = new Value();
        Value voltageValue  = new Value();
        Value curentValue   = new Value();

        //parametry zasilacza
        private double          limitVoltage        = 500;
        private double          limitCurent         = 2;
        private double          limitPower          = 1000;
        private double          maxVoltage          = 500; ///< Wartość max napiecia dla trybu Voltage jaką można ustawić na zasilaczu
        private double          maxCurent           = 2;  ///< Wartość max prądu dla trybu Curent jaką można ustawić na zasilaczu
        private double          maxPower            = 1000; ///< Wartość max mocy dla trybu Power jaką można ustawić na zasilaczu
        private int             timeWaitOnOperate   = 5;    ///< Czas oczekiwania na potwierdzenie wlaczenia zasilacza
        private int             timeWaitOnSetpoint  = 30;   ///< Czas oczekiwania na sptrawdzenie czy aktualny setpoint miesci sie w wyznaczonych widelkach programu

        //parametry PID
        private int pid_Kp = 0;
        private int pid_Ti = 0;
        private int pid_Td = 0;
        private int pid_Ts = 0;
        private int pid_Filtr = 0;
        private bool pid_On = false;    //flaga okresla czy jest wlaczony tryb PID do regulacji mocy zasilacza
        //ograniczenie wartosc analogowej sterujacej zasilaczem
        private double minRangeVoltageHV = 0.0;   ///<Zmienna przechowuje informacje na temat max wartosci napieca jaka moze zostac ustawioan na wyjsciu sterujacym zasilaczem HV (Max wartosc to 10V)
        private double maxRangeVoltageHV = 10.0;   ///<Zmienna przechowuje informacje na temat max wartosci napieca jaka moze zostac ustawioan na wyjsciu sterujacym zasilaczem HV (Max wartosc to 10V)

        //-------------------------------------------------------------------------------------------
        /**
         * Ustaw w konstruktorze liste parametrow ktore powinny byc zapisywane w bazie danyc
         */
        public PowerSupplay()
        {
            //Uzupelnij liste parametrow ktore powinny byc zapisywane w bazi danych
            //AddParameter("Power Percent", powerPercent, "%");
            AddParameter("Power", powerValue,"W");
            // AddParameter("Voltage", voltageValue,"V"); // parametry prad i napiecie sa zablokowane poniewaz nas nie interesuja i nie prowadzimy ich akwizycji
            // AddParameter("Curent", curentValue,"A");
            //Ustaw nazwe urzadzenia - pamietaj ze musi ona byc unikalna dla calego systemu
            name = "PowerSupply";

            acqData = true; //Ustawiam flage ze urzadzenie jest przenzaczone do arachiwzowania danych w nbazie danych
        }
        // ------SETERY/GETERY
        //-------------------------------------------------------------------------------------------
        public double SetpointPercent
        {
            get
            {
                double setpointPercent = 0;
                Driver.HPT1000 hpt = Factory.Hpt1000;
                //Ustaw procentowa wartosc setpointa mocy. Dla trybu manual jezeli HV jest wylaczony to ustaw 0. Dla trybu Autoamtic ustaw 0 jezeli aktulny subprgram nie zawiera sterowania plasma
                if (maxPower > 0 && hpt != null && 
                   ((hpt.GetMode() == Types.Mode.Manual && state == Types.StateHV.ON) || (hpt.GetMode() == Types.Mode.Automatic && hpt.IsProgramContainsPlasmaProces())))
                    setpointPercent = setpoint / maxPower * 100;
                return setpointPercent;
            }
        }
        //-------------------------------------------------------------------------------------------
        public bool PID_ON
        {
            get { return pid_On; }
        }
        //-------------------------------------------------------------------------------------------
        public double MinRangeVolatgeHV
        {
            get { return minRangeVoltageHV; }
        }
        //-------------------------------------------------------------------------------------------
        public double MaxRangeVolatgeHV
        {
            get { return maxRangeVoltageHV; }
        }
        //-------------------------------------------------------------------------------------------
        public int PID_Kp
        {
            get { return pid_Kp; }
        }
        //-------------------------------------------------------------------------------------------
        public int PID_Ti
        {
            get { return pid_Ti; }
        }
        //-------------------------------------------------------------------------------------------
        public int PID_Td
        {
            get { return pid_Td; }
        }
        //-------------------------------------------------------------------------------------------
        public int PID_Ts
        {
            get { return pid_Ts; }
        }
        //-------------------------------------------------------------------------------------------
        public int PID_Filtr
        {
            get { return pid_Filtr; }
        }
        //-------------------------------------------------------------------------------------------
        public double LimitVoltage
        {
            get { return limitVoltage; }
        }
        //-------------------------------------------------------------------------------------------
        public double LimitCurent
        {
            get { return limitCurent; }
        }
        //-------------------------------------------------------------------------------------------
        public double LimitPower
        {
            get { return limitPower; }
        }
        //-------------------------------------------------------------------------------------------
        public double Power
        {
            get { return power; }
        }
        //-------------------------------------------------------------------------------------------
        public double Voltage
        {
            get { return voltage; }
        }
        //-------------------------------------------------------------------------------------------
        public double Curent
        {
            get { return curent; }
        }
        //-------------------------------------------------------------------------------------------
        public double Setpoint
        {
            get { return setpoint; }
        }
        //-------------------------------------------------------------------------------------------
        public Types.StateHV State
        {
            get { return state; }
        }
        //-------------------------------------------------------------------------------------------
        public Types.ModeHV Mode
        {
            get { return mode; }
        }
        //-------------------------------------------------------------------------------------------
        public double MaxPower
        {
            get { return maxPower; }
        }
        //-------------------------------------------------------------------------------------------
        public double MaxVoltage
        {
            get { return maxVoltage; }
        }
        //-------------------------------------------------------------------------------------------
        public double MaxCurent
        {
            get { return maxCurent; }
        }
        //-------------------------------------------------------------------------------------------
        public double TimeWaitSetpoint
        {
            get { return timeWaitOnSetpoint; }
        }
        //-------------------------------------------------------------------------------------------
        public double TimeWaitOperate
        {
            get { return timeWaitOnOperate; }
        }
        //-------------------------------------------------------------------------------------------
        /**
         * Metoda aktualizuje dane na temat zasilacza
         */
        override public void UpdateData(int []aData)
        {
            if (aData.Length > Types.OFFSET_POWER && aData.Length > Types.OFFSET_VOLTAGE && aData.Length > Types.OFFSET_CURENT && aData.Length > Types.OFFSET_MODE_HV && aData.Length > Types.OFFSET_STATUS_HV)
            {
                power       = Types.ConvertDWORDToDouble(aData, Types.OFFSET_POWER);
                voltage     = Types.ConvertDWORDToDouble(aData, Types.OFFSET_VOLTAGE);
                curent      = Types.ConvertDWORDToDouble(aData, Types.OFFSET_CURENT);
                setpoint    = Types.ConvertDWORDToDouble(aData, Types.OFFSET_SETPOINT_HV);

                if (Enum.IsDefined(typeof(Types.ModeHV), aData[Types.OFFSET_MODE_HV]))
                    mode = (Types.ModeHV)Enum.Parse(typeof(Types.ModeHV), (aData[Types.OFFSET_MODE_HV]).ToString()); // konwertuj int na Enum
                else
                    mode = Types.ModeHV.Unknown;

                if (Enum.IsDefined(typeof(Types.StateHV), aData[Types.OFFSET_STATUS_HV]))
                    state = (Types.StateHV)Enum.Parse(typeof(Types.StateHV), (aData[Types.OFFSET_STATUS_HV]).ToString()); // konwertuj int na Enum
                else
                    state = Types.StateHV.Error;

                //aktualizuj wartosci w obiektach parametrow ktore sa wykorzystywane do zapisu danych w bazie danych
                powerValue.Value_   = power;
                voltageValue.Value_ = voltage;
                curentValue.Value_  = curent;
                double percentPower = 0;
                if(maxPower > 0)
                    percentPower = setpoint / MaxPower * 100;
                powerPercent.Value_ = percentPower; 
            }
            base.UpdateData(aData);
        }
        //-------------------------------------------------------------------------------------------
        /**
        * Metoda odczytuje ustawienia zasilacza
        */
        override public void UpdateSettingsData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_HV_LIMIT_POWER  && aData.Length > Types.OFFSET_HV_LIMIT_VOLTAGE && aData.Length > Types.OFFSET_HV_LIMIT_CURENT &&
                aData.Length > Types.OFFSET_HV_MAX_POWER    && aData.Length > Types.OFFSET_HV_MAX_VOLTAGE   && aData.Length > Types.OFFSET_HV_MAX_CURENT &&
                aData.Length > Types.OFFSET_HV_WAIT_OPERATE && aData.Length > Types.OFFSET_HV_WAIT_SETPOINT)
            {
                limitPower          = Types.ConvertDWORDToDouble(aData, Types.OFFSET_HV_LIMIT_POWER);
                limitVoltage        = Types.ConvertDWORDToDouble(aData, Types.OFFSET_HV_LIMIT_VOLTAGE);
                limitCurent         = Types.ConvertDWORDToDouble(aData, Types.OFFSET_HV_LIMIT_CURENT);
                maxPower            = Types.ConvertDWORDToDouble(aData, Types.OFFSET_HV_MAX_POWER);
                maxVoltage          = Types.ConvertDWORDToDouble(aData, Types.OFFSET_HV_MAX_VOLTAGE);
                maxCurent           = Types.ConvertDWORDToDouble(aData, Types.OFFSET_HV_MAX_CURENT);
                timeWaitOnOperate   = aData[Types.OFFSET_HV_WAIT_OPERATE];
                timeWaitOnSetpoint  = aData[Types.OFFSET_HV_WAIT_SETPOINT];
                pid_Kp              = aData[Types.OFFSET_PID_HV_Kp];
                pid_Ti              = aData[Types.OFFSET_PID_HV_Ti];
                pid_Td              = aData[Types.OFFSET_PID_HV_Td];
                pid_Ts              = aData[Types.OFFSET_PID_HV_Ts];
                pid_Filtr           = aData[Types.OFFSET_PID_HV_FILTR];
                minRangeVoltageHV   = aData[Types.OFFSET_MIN_RANGE_VOLTAGE_HV] / 1000.0; // Wartosc w PLC jest przechowywana a mV
                maxRangeVoltageHV   = aData[Types.OFFSET_MAX_RANGE_VOLTAGE_HV] / 1000.0; // Wartosc w PLC jest przechowywana a mV
                if (aData[Types.OFFSET_MODE_PID_HV] == 1)
                    pid_On = true;
                else
                    pid_On = false;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia wartosc nastawy zasilacza. Jako parametr podaje wartosc procentowa mocy
        */
        public ItemLogger SetSetpoint(double aSetpoint)
        {
            ItemLogger aErr  = new ItemLogger();
            int   aCode = 0;

            if (plc != null)
            {
                if(controlMode == Types.ControlMode.Automatic)
                    aCode = plc.WriteRealData("D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_HV_SETPOINT).ToString(), (float)aSetpoint);

                if(controlMode == Types.ControlMode.Manual)
                    aCode = plc.WriteRealData(Types.ADDR_POWER_SUPPLAY_SETPOINT, (float)aSetpoint);
                //Zglos tylko gdy jest blad
                if(aCode != 0)
                    aErr.SetErrorMXComponents(Types.EventType.SET_SETPOINT_HV ,aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Mozliwosc ustawienia tylko w trybie recznym
        */
        public ItemLogger SetMode(Types.ModeHV aMode)
        {
            ItemLogger aErr = new ItemLogger();
            int []aData = new int[1] ;

            aData[0] =  (int)aMode;

            if (plc != null)
            {
                int aCode = 0;

                if (controlMode == Types.ControlMode.Automatic)
                    aCode = plc.WriteWords("D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_HV_OPERATE).ToString(), 1, aData);

                if (controlMode == Types.ControlMode.Manual)
                    aCode = plc.WriteWords(Types.ADDR_POWER_SUPPLAY_MODE, 1, aData);

                aErr.SetErrorMXComponents(Types.EventType.SET_MODE, aCode);                
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Mozliwosc ustawienia tylko w trybie recznym
        */
        public ItemLogger SetOperate(bool aOperate)
        {
            ItemLogger aErr = new ItemLogger();

            int[] aData = new int[1];

            if (aOperate)
                aData[0] = 2;
            else
                aData[0] = 1;
            if (plc != null)
            {
                int aCode = 0;
                if (controlMode == Types.ControlMode.Manual)
                {
                    aCode = plc.WriteWords(Types.ADDR_POWER_SUPPLAY_OPERATE, 1, aData);
                    aErr.SetErrorMXComponents(Types.EventType.SET_OPERATE_HV, aCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.SET_OPERATE_HV);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
        /**
         * Metoda ustawia max wartosc mocy jak mozna ustawic w zasilaczu
        */
        public ItemLogger SetLimitPower(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_LIMIT_POWER), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.SET_LIMIT_POWER_HV,aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                limitPower = aValue;

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
        /**
         * Metoda ustaiwa nax wartosc pradu jaka mozna ustawic w zasilaczu
        */
        public ItemLogger SetLimitCurent(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_LIMIT_CURENT), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.SET_LIMIT_CURRENT_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                limitCurent = aValue;

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
        /**
         * Metoda ustawia max wartosc napiecia jaka mozna ustawic w zasilaczu
        */
        public ItemLogger SetLimitVoltage(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_LIMIT_VOLTAGE), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.SET_LIMIT_VOLTAGE_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                limitVoltage = aValue;

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
        /**
         * Ustaw max wartosc napiecia jaka jest jest mozliwa do ustawiania z zasilacza
         */
        public ItemLogger SetMaxVoltage(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_MAX_VOLTAGE), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.SET_MAX_VOLTAGE_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                maxVoltage = aValue;

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
        /**
         * Ustaw max wartosc mocy jaka jest jest mozliwa do ustawiania z zasilacza
        */
        public ItemLogger SetMaxPower(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_MAX_POWER), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.SET_MAX_POWER_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                maxPower = aValue;

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
        /**
         * Ustaw max wartosc pradu jaka jest jest mozliwa do ustawiania z zasilacza
         */
        public ItemLogger SetMaxCurent(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_MAX_CURENT), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.SET_MAX_CURENT_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                maxCurent = aValue;

            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
         * Ustaw czas oczekiwania na sprawdzenie poprawnisci wlaczenia zasilacza
         */
        public ItemLogger SetWaitTimeOperate(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_WAIT_OPERATE), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_WAIT_TIME_OPERATE_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                timeWaitOnOperate = aValue;

            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
         * Ustaw czas oczekiwania na stabilizacje sie wartosc setpoint poiedzy zadanymi widelkami programu
         */
        public ItemLogger SetWaitTimeSetpoint(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_HV_WAIT_SETPOINT), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_WAIT_TIME_SETPOINT_HV,aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                timeWaitOnSetpoint = aValue;

            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
        * Metoda ustawia parametru regulatora PID w sterowniku
        */
        public ItemLogger SetPID(Types.PID aKindPara, int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aOffset = 0;
                switch (aKindPara)
                {
                    case Types.PID.Kp:
                        aOffset = Types.OFFSET_PID_HV_Kp;
                        break;
                    case Types.PID.Ti:
                        aOffset = Types.OFFSET_PID_HV_Ti;
                        break;
                    case Types.PID.Td:
                        aOffset = Types.OFFSET_PID_HV_Td;
                        break;
                    case Types.PID.Ts:
                        aOffset = Types.OFFSET_PID_HV_Ts;
                        break;
                    case Types.PID.Filtr:
                        aOffset = Types.OFFSET_PID_HV_FILTR;
                        break;
                }
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, aOffset), 1, aData);
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
        //------------------------------------------------------------------------------------------- 
        /**
         * Ustaw zakres napiecia sterujacego zasilaczem HV. Max wartosc to 10 V
         */
        public ItemLogger SetRangeVoltageHV(double aValue,Types.TypeRangeHV typeRangeHV)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            if (aValue > 10.0)  aValue = 10.0;
            if (aValue < 0)     aValue = 0;
            //przelicz V na mV
            aData[0] = (int)(aValue * 1000.0);
            //Usaladres komorki PLC
            int aSettingOffset = Types.OFFSET_MIN_RANGE_VOLTAGE_HV;
            if(typeRangeHV == Types.TypeRangeHV.Max)
                aSettingOffset = Types.OFFSET_MAX_RANGE_VOLTAGE_HV;
            //Zapisz wartosc do PLC
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, aSettingOffset), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_RANGE_VOLTAGE_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
            {
                if(typeRangeHV == Types.TypeRangeHV.Min)
                    minRangeVoltageHV = aValue;
                if (typeRangeHV == Types.TypeRangeHV.Max)
                    maxRangeVoltageHV = aValue;
            }
            return aErr;
        }
        //------------------------------------------------------------------------------------------- 
        /**
         * Ustaw tryb sterowania zasilaczem HV - albo zadaj moc recnzie albo ustaw z regulatora PID
         */
        public ItemLogger SetPIDMode(bool aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];
            aData[0] = 0;
            if (aValue)
                aData[0] = 1;

            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_MODE_PID_HV), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_MODE_PID_HV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                pid_On = aValue;

            return aErr;
        }
        //-------------------------------------------------------------------------------------------  
    }
}
