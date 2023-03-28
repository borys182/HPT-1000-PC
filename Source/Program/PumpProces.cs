using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
namespace HPT1000.Source.Program
{
    /// <summary>
    /// Klasa jest odpowiedzialna za repreznetowanie mozliwosci wykonania pompowania komory jako jednego z elementow programu automatycznego. Zadaniem pompowania jest wlaczenia pompy wstepnej i pompowanie do momentu uzyskania zadanego trigera
    /// Parametry konieczne do ustawienia to: setpoint cisnienia pompowania
    /// </summary>
    [Serializable]
    public class PumpProces : ProcesObject
    {
        private DateTime    timeWaitForPumpDown     = DateTime.Now; ///< Max zzas oczekiwania na odpompowanie komory do zadanego setpointa
        private double      setpointPressure        = 0.5;          ///< Setpoint cisnienia do ktroego bedizmey pomowac komore [mBar] 

        [NonSerialized]
        private DateTime    timeWaitForPumpDownTmp  = DateTime.Now; ///< Max zzas oczekiwania na odpompowanie komory do zadanego setpointa
        [NonSerialized]
        private double      setpointPressureTmp     = 0.5;          ///< Setpoint cisnienia do ktroego bedizmey pomowac komore [mBar] 

        //--------------------------------------------------------------------------------------------------------
        public PumpProces()
        {
            //zeruja godziny/minuty/sekundy
            timeWaitForPumpDown = DateTime.Now;
            timeWaitForPumpDown = timeWaitForPumpDown.AddHours(-DateTime.Now.Hour);
            timeWaitForPumpDown = timeWaitForPumpDown.AddMinutes(-DateTime.Now.Minute);
            timeWaitForPumpDown = timeWaitForPumpDown.AddSeconds(-DateTime.Now.Second);

            timeWaitForPumpDownTmp = timeWaitForPumpDown;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zdanie aktualizjace parametrow subprogramup pompowania odczytanych ze sterownika PLC
        */
        public override void UpdateData(SubprogramData aSubprogramData)
        {
            setpointPressure    = aSubprogramData.Pump_SetpointPressure;
            timeWaitForPumpDown = ConvertDate(aSubprogramData.Pump_TargetTime);
            timeWorking         = ConvertDate(aSubprogramData.WorkingTimePump);

            ReadActiveWithCMD(aSubprogramData.Command, Types.BIT_CMD_PUMP);//Sprawdz czy w danym programie jest wykorzystywany subprogram przedmuchu
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu przedmuchu 
         */
        override public void PrepareDataPLC(int[] aData)
        {
            if (active)
            {
                aData[Types.OFFSET_SEQ_CMD]          |= (int)System.Math.Pow(2, Types.BIT_CMD_PUMP);
                aData[Types.OFFSET_SEQ_PUMP_MAX_TIME] = timeWaitForPumpDown.Hour * 3600 + timeWaitForPumpDown.Minute * 60 + timeWaitForPumpDown.Second;
                aData[Types.OFFSET_SEQ_PUMP_SP]       = Types.ConvertDOUBLEToWORD(setpointPressure, Types.Word.LOW); 
                aData[Types.OFFSET_SEQ_PUMP_SP + 1]   = Types.ConvertDOUBLEToWORD(setpointPressure, Types.Word.HIGH); 
            }
        }
        //---------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie wartoscu tymczasowych parametrow jako wartosci rzeczywiste/akualne
        */
        public override void SetEditableParameters(bool changesStore)
        {
            if (changesStore)
            {
                timeWaitForPumpDown = timeWaitForPumpDownTmp;
                setpointPressure    = setpointPressureTmp;
                ChangesNotSave      = true;
            }
            else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
            {
                timeWaitForPumpDownTmp  = timeWaitForPumpDown;
                setpointPressureTmp     = setpointPressure;
            }
            Changes = false;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie setpointa pompowania
         */
        public void SetSetpoint(double aPressure, bool tmpValue = false)
        {
            //zaokraglij do 3 miejsc po przecinku
            aPressure = Math.Round(aPressure, 3);

            //Sprawdz czy zmienila sie wartosc parametru setpoint i nie zostanie przypisana jako rzeczywista
            if (setpointPressure != aPressure && tmpValue)
                Changes = true;

            setpointPressureTmp = aPressure;
            //Ustaw aktualna wartosc setpoinyu gdy nie jest przekazana jako tymczasowa
            if (!tmpValue)
                setpointPressure = aPressure;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zwrocenie setpoina do ktorego chcemy pompwac
        */
        public double GetSetpoint()
        {
            return setpointPressure;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie ustawienie max czasu pompowania po przekroczeniu ktorego PLC zglasza blad
        */
        public void SetTimeWaitForPumpDown(DateTime aTime, bool tmpValue = false)
        {
            //Dodaj rok aby mozna bylo te date wyswietlic. Nas interesuje tylko czas
            if (aTime.Year < 2000)
                aTime = aTime.AddYears(2000);
      
            //Sprawdz czy zmienila sie wartosc parametru czas pompowania i nie zostanie przypisana jako rzeczywista
            if (timeWaitForPumpDown != aTime && tmpValue)
                Changes = true;

            timeWaitForPumpDownTmp = aTime;
            //Ustaw aktualna wartosc czau popowania gdy nie jest przekazana jako tymczasowa
            if (!tmpValue)
                timeWaitForPumpDown = aTime;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zwrocenie max czasu oczekiwania na odpomowanie komory do zadanego setpointa
        */
        public DateTime GetTimeWaitForPumpDown()
        {
            return timeWaitForPumpDown;
        }
        //--------------------------------------------------------------------------------------------------------

    }
}
