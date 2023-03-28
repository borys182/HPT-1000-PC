using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;


namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje pompe westepna komory. Jest odpowiedzialana za przedstawianie jej stanu oraz sterowanie praca poprzez wlacz/wylacz
    /// </summary>
    public class ForePump : ChamberObject
    {
        private Types.StateFP state = Types.StateFP.Error;  ///< Stan pompy wstepnej {ON/OFF/Error}

        //parametry
        private int timeWaitPF      = 5;            ///< Czas [s] oczekiwania na sprawdzenie poprawnosci wlaczenia pompy wstepnej
        private int timePumpToSV    = 30;           ///< Czas [s] pompowania do zaworu SV. Brak jest tam glowcy dlatego pompuje en odcinek na czas
        private double setpointPumpDown = 0.5;      ///< Setpoint jest wykorzystywany do procesu zabezpieczenia pompy wstepnej na wypadek pompowania komory otwartej. Okresla setpoint ktorego nieosiagniecie w zadanym czasie powoduje awaryjne zatrzymanie pompowania
        private int maxTimetPumpDown = 5 * 60;      ///< Czas [s] jest wykorzystywany do procesu zabezpieczenia pompy wstepnej na wypadek pompowania komory otwartej. Okresla max czas do osiagniecia zadanego setpoint. Nieosiagniecie setpoint w zadanym czasie powoduje awaryjne zatrzymanie pompowania

        //Grupa SETTEROW
        //-----------------------------------------------------------------------------------------
        public int TimeWaitPF
        {
            get { return timeWaitPF; }
        }
        //-----------------------------------------------------------------------------------------
        public int TimePumpToSV
        {
            get { return timePumpToSV; }
        }
        //-----------------------------------------------------------------------------------------
        public double SetpointPumpDown
        {
            get { return setpointPumpDown; }
        }
        //-----------------------------------------------------------------------------------------
        public int MaxTimetPumpDown
        {
            get { return maxTimetPumpDown; }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie odczytanie aktualnego stanu pompy wstpenje ze sterownika PLC
         */
        override public void UpdateData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_STATE_FP)
            {
                if (Enum.IsDefined(typeof(Types.StateFP), aData[Types.OFFSET_STATE_FP]))
                    state = (Types.StateFP)Enum.Parse(typeof(Types.StateFP), (aData[Types.OFFSET_STATE_FP]).ToString());
                else
                    state = Types.StateFP.Error;
            }
            base.UpdateData(aData);
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Funkcja akutalizuje parametry pompy odczytane z PLC 
         */
        public override void UpdateSettingsData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_TIME_WAIT_PF && aData.Length > Types.OFFSET_TIME_PUMP_TO_SV )
            {
                timeWaitPF = aData[Types.OFFSET_TIME_WAIT_PF];
                timePumpToSV = aData[Types.OFFSET_TIME_PUMP_TO_SV];
            }
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Funkcja akutalizuje parametry pompy odczytane z PLC 
         */
        public override void UpdateExtraSettingsData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_MAX_TIME_PUMPDOWN && aData.Length > Types.OFFSET_SETPOINT_PUMPDOWN)
            {
                maxTimetPumpDown = aData[Types.OFFSET_MAX_TIME_PUMPDOWN];
                setpointPumpDown = Types.ConvertDWORDToDouble(aData, Types.OFFSET_SETPOINT_PUMPDOWN);
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca stam pompy
         */
        public Types.StateFP GetState()
        {
            return state;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia wlaczenie/wylaczenie pompy
         */
        public ItemLogger ControlPump(Types.StateFP state)
        {
            ItemLogger aErr = new ItemLogger();

            int[] aData = { (int)state };

            if (plc != null)
            {
                if (controlMode == Types.ControlMode.Manual)
                {
                    int aCode = plc.WriteWords(Types.ADDR_FP_CTRL, 1, aData);
                    aErr.SetErrorMXComponents(Types.EventType.CONTROL_PUMP, aCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.CONTROL_PUMP);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
        
            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Funkcja ustawia czas oczekiwania na potwierdzenie sie wlaczenia pompy
          */
        public ItemLogger SetTimeWaitPF(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_TIME_WAIT_PF), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_WIAT_TIME_PF, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                timeWaitPF = aValue;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Funkcja ustawia czas pompowania komory do zaworu SV
          */
        public ItemLogger SetTimePumpToSV(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_TIME_PUMP_TO_SV), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_TIME_PUMP_TO_SV, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                timePumpToSV = aValue;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Funkcja ustawia setpoint w [mBar] do ktorego komora powinna zostac odpompowana w zadanym czasie. Jest to parametr serwisowy ktory zabezpiecza pompe przed pompowaniem komory ktora jest rozszczelniona
          */
        public ItemLogger SetPumpdownSetpoint(double aValue)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = plc.WriteRealData(Types.GetAddress(Types.AddressSpace.ExtraSettings, Types.OFFSET_SETPOINT_PUMPDOWN), (float)aValue);
                aErr.SetErrorMXComponents(Types.EventType.CONTROL_PUMP, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                setpointPumpDown = aValue;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Funkcja ustawia setpoint w [mBar] do ktorego komora powinna zostac odpompowana w zadanym czasie. Jest to parametr serwisowy ktory zabezpiecza pompe przed pompowaniem komory ktora jest rozszczelniona
          */
        public ItemLogger SetMaxTimePumpdown(int aValue)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = aValue;
            if (plc != null)
            {
                int aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.ExtraSettings, Types.OFFSET_MAX_TIME_PUMPDOWN), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.CONTROL_PUMP, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
                maxTimetPumpDown = aValue;

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
    }
}
