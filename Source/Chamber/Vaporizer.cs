using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
using HPT1000.Source;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje zawor vaporatora ktorgo zadaniem jest szybkie wstrzykiwanie gazu do komory
    /// </summary>

    public class Vaporizer : ChamberObject
    {
        ///Parametry vaporaitora
        private double cycleTime        = 0;                    ///<Okrelsenie dlugosc trwania cyklu "PWM" [ms]
        private double onTime           = 0;                    ///<Czas wlaczenia przeplywki w danym cyklu (czas nie moze byc wiekszy niz czas cyklu) [ms]
        private double dosing           = 0;                    ///<Wartosc w uL/m okreslajaca dozowanie gazu do komory
        bool           enabled          = false;                ///<Flaga okresla czy w danej konfiguracji sprzetowej dostepny jest vaporizer
        private bool   controlOn        = false;                ///<Zmienna okresla czy przeplywka jest wlaczona - samo ustawineiu gazu nie powoduje przplywu trzeba jeszcze wlaczyc przeplywek
        private Types.StateValve state = Types.StateValve.Close;///< Flaga okresla stab zaowuru dozujacego {False - Close ; True - Open}

        private Value vaporaizerValue   = new Value();

        static Types.VaporaizerType type       = Types.VaporaizerType.None; ///<Określenie typu vaporaziera. Albo podajemy jako parametr wartosc dozowania albo podajemy cykl pracy
        //-------------------------------------------------------------------------------------
        public bool ControlOn
        {
            get { return controlOn; }
        }
        //-------------------------------------------------------------------------------------
        public Types.StateValve State
        {
            get { return state; }
        }
        //-------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy
         */ 
        public Vaporizer()
        {
            //Uzupelnij liste parametrow ktore powinny byc zapisywane w bazi danych
            AddParameter("Vaporaizer state", vaporaizerValue, "");

            //Ustaw nazwe urzadzenia - pamietaj ze musi ona byc unikalna dla calego systemu
            name = "Vaporaizer";

            acqData = true; //Ustawiam flage ze urzadzenie jest przenzaczone do arachiwzowania danych w nbazie danych

        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie aktualizowanie parametró vaporatora odczytane ze sterownika PLC
         */
        override public void UpdateData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_CYCLE_TIME && aData.Length > Types.OFFSET_ON_TIME)
            {
                cycleTime   = Types.ConvertDWORDToDouble(aData, Types.OFFSET_CYCLE_TIME);
                onTime      = Types.ConvertDWORDToDouble(aData, Types.OFFSET_ON_TIME);
                dosing      = aData[Types.OFFSET_DOSING];
                type        = (Types.VaporaizerType)aData[Types.OFFSET_MODE_VAPOR];
                if(type != Types.VaporaizerType.None)
                    enabled = true;
                else
                    enabled = false;

            }
            if (aData.Length > Types.OFFSET_STATE_GAS_CTR)
            {
                int aControlGas = aData[Types.OFFSET_STATE_GAS_CTR];
                controlOn       = Convert.ToBoolean(aControlGas & (1 << 3));
            }
            //Odczytaj stan zaworu
            if (aData.Length > Types.OFFSET_STATE_VALVES + 1)
            {
                int aValvesState = (aData[Types.OFFSET_STATE_VALVES + 1] << 16) | aData[Types.OFFSET_STATE_VALVES];

                int aShiftBits = 8; // o ile musze przesunac bity slowa aby uzyskac dane interesujacego mnie zaworu
                int aMask = 0x03 << aShiftBits;                          //maska bitowa wyodrebniajace stan danego zaworu
                int aState = (aValvesState & aMask) >> (int)aShiftBits;

                if (Enum.IsDefined(typeof(Types.StateValve), aState))
                {
                    state = (Types.StateValve)Enum.Parse(typeof(Types.StateValve), (aState).ToString()); // konwertuj int na Enum
                   //Aktualizuj dane zapisywane do bazy danych
                    vaporaizerValue.Value_ = (int)state - 1;//Poniewaz 0 trkatuje jako Close dlatego odejmuje od odycznej wartosci 1 poniewaz tam close jest traktowane jako 1
                }
                else
                    state = Types.StateValve.Error;

            }

            if (plc != null && plc.GetDummyMode())
            {
                enabled = true;
                type = Types.VaporaizerType.Cycle;
            }
            base.UpdateData(aData);
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia ustawienie w przestrzeni PLC czasu cyklu dla sterowania zaworem szybkim w przestrzeni sterownika PLC
         * @param CycleTime - czas cyklu pracy zaworu podawany w ms
        */
        public ItemLogger SetCycleTime(float aCycleTime)
        {
            ItemLogger aErr = new ItemLogger();

         //   if (aCycleTime < onTime)    aErr.SetErrorApp(Types.EventType.BAD_CYCLE_TIME);
            if (plc == null)            aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
            if (aCycleTime > 30000) aCycleTime = 30000;
            if (!aErr.IsError())
            {
                int aCode = 0;
                if (controlMode == Types.ControlMode.Manual)
                {
                    //Ustaw cycle on na wartosc procentowa zanim ustawie cycle time
                    if (cycleTime != 0)
                    {
                        float aOnTime = (float)(onTime / cycleTime * aCycleTime);
                        SetOnTime(aOnTime, Types.UnitFlow.ms,true);
                    }
                    aCode = plc.WriteRealData(Types.ADDR_CYCLE_TIME, aCycleTime);
                }
                if (controlMode == Types.ControlMode.Automatic)
                    aCode = plc.WriteRealData("D" + (Types.OFFSET_SEQ_FLOW_4_CYCLE_TIME + Types.ADDR_START_CRT_PROGRAM).ToString(), aCycleTime);
                aErr.SetErrorMXComponents(Types.EventType.SET_CYCLE_TIME, aCode);
            }
                        
            //Aktualizuj czas cyklu pracy. W watku moze byc za wolno dla szybkiego ustawienia czasu wlaczenia zaworu 
            if (!aErr.IsError())
                cycleTime = aCycleTime;

            Logger.AddError(aErr);
            return aErr;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia ustawienie w przestrzeni PLC czasu jak dlugo ma byc wlaczany zawor szybki w danym cyklu
         * @param OnTime - procentowe okreslenie czasu wlaczenia zawotu w danym cyklu
        */
        public ItemLogger SetOnTime(float aOnTimeValue, Types.UnitFlow aUnit,bool aIgnoreCheck = false)
        {
            ItemLogger aErr = new ItemLogger();
            double aOnTime = 0;

            if (aUnit == Types.UnitFlow.percent)
            {
                if (aOnTimeValue < 0)                           aOnTime = 0;
                if (aOnTimeValue > 100)                         aOnTime = cycleTime;
                if (aOnTimeValue >= 0 && aOnTimeValue <= 100)   aOnTime = cycleTime * aOnTimeValue / 100;
            }
            else
                aOnTime = aOnTimeValue;

            if (!aIgnoreCheck && aOnTime > cycleTime)   aErr.SetErrorApp(Types.EventType.BAD_ON_TIME);
            if (plc == null)                            aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            if (!aErr.IsError())
            {
                int aCode = 0;
                if (controlMode == Types.ControlMode.Manual)
                    aCode = plc.WriteRealData(Types.ADDR_ON_TIME, (float)aOnTime);
                if (controlMode == Types.ControlMode.Automatic)
                    aCode = plc.WriteRealData("D" + (Types.OFFSET_SEQ_FLOW_4_ON_TIME + Types.ADDR_START_CRT_PROGRAM).ToString(), (float)aOnTime);
                aErr.SetErrorMXComponents(Types.EventType.SET_ON_TIME, aCode);
            }
            Logger.AddError(aErr);
            return aErr;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja zwraca w ms czas cyklu pracy zaworu
         * @return Czas cyklu pracy zaworu wyrazony w ms 
         */ 
        public double GetCycleTime()
        {
            return cycleTime;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja zwraca w % czas wlaczenia zaworu szybkiego w danym cyklu pracy
         * @return Czas wlaczenia zaworu szybkiego w danym cyklu pracy wyrazony w % odnosnie do czasu cyklu pracy zaworu
         */
        public double GetOnTime(Types.UnitFlow aUnit)
        {
            double aValue = 0;

            switch (aUnit)
            {
                case Types.UnitFlow.ms:
                    aValue = onTime;
                    break;
                case Types.UnitFlow.percent:
                    if (cycleTime != 0)
                        aValue = onTime / cycleTime * 100;
                    break;
            }
            return aValue;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ustawia falge okreslajac czy w danej konfiguracji jest dostepny zawor szybki
         */ 
        public void SetActive(bool aState)
        {
            enabled = aState;
        }
        //-------------------------------------------------------------------------------------
        /**
        * Funkcja zwraca falge okreslajac czy w danej konfiguracji jest dostepny zawor szybki
        */
        public bool GetActive()
        {
            return enabled;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie wartosc dozowania gazu
         */ 
         public ItemLogger SetDosing(double aValue)
        {
            ItemLogger aErr = new ItemLogger();
            if (plc != null) 
            {
                int aCode = 0;
                int[] aData = new int[1];
                aData[0] = (int)aValue;
                if (controlMode == Types.ControlMode.Manual)
                    aCode = plc.WriteWords(Types.ADDR_DOSING,1,aData);
            //    if (controlMode == Types.ControlMode.Automatic)
            //        aCode = plc.WriteRealData("D" + (Types.OFFSET_SEQ_FLOW_4_ON_TIME + Types.ADDR_START_CRT_PROGRAM).ToString(), (float)aOnTime);
                aErr.SetErrorMXComponents(Types.EventType.SET_DOSING, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            Logger.AddError(aErr);
            return aErr;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie zwrocenie wartosci dozowania gazu
         */
        public double GetDosing()
        {
            return dosing;
        }
        //-------------------------------------------------------------------------------------
         /**
          * Funkcja ma za zadanie ustawienie typu vaporaziera
          */
        public ItemLogger SetTypeVaporaizer(Types.VaporaizerType aType)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int aCode = 0;
                int[] aData = new int[1];
                aData[0] = (int)aType;
                aCode = plc.WriteWords(Types.GetAddress(Types.AddressSpace.Settings, Types.OFFSET_TYPE_VAPORAIZER), 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_DOSING, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            Logger.AddError(aErr);
            return aErr;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie zwrocenie typu vaporaziera
         */
        public static Types.VaporaizerType GetTypeVaporaizer()
        {
            return type;
        }
        //-------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie stanu wlaczenia/wylaczenia przpleywu gazu przez vaporizer
         */
        public ItemLogger SetState(bool aState)
        {
            ItemLogger aErr = new ItemLogger();
            if (plc != null)
            {
                int aCode = 0;
                int[] aData = new int[1];
                aData[0] = 0x40;
                if (aState)
                    aData[0] = 0x80;
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
        //-------------------------------------------------------------------------------------
    }
}

