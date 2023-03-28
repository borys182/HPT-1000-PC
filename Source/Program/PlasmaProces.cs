using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Program
{
    /// <summary>
    /// Klasa jest odpowiedzialna za repreznetowanie mozliwosci sterpowania zasilaczem plazmy jako jednego z elementow programu automatycznego. 
    /// Parametry mozliwe do ustawienia to: moc zasilacza oraz tryb pracy
    /// </summary>
    [Serializable]
    public class PlasmaProces : ProcesObject
    {
        private Types.WorkModeHV workMode       = Types.WorkModeHV.Power;
        private double           setpoint       = 0;    //Ustaw wartosc ktora jest moca/pradem/napieciem w zaleznosci od wybranego trybu pracy
        private DateTime         timeOperate;           //Czas wlaczenia zasilacza. Ustawienie czasu 0 wlacza zasilacz na czas nieokrelsony i jego wylaczenie nastepuje w momencie wyslania komendy OFF
        private double           deviationSP    = 0;    //procentowe okreslenie max odchylki

        [NonSerialized]
        private double           setpointTmp    = 0;    //Ustaw wartosc ktora jest moca/pradem/napieciem w zaleznosci od wybranego trybu pracy
        [NonSerialized]
        private DateTime         timeOperateTmp;        //Czas wlaczenia zasilacza. Ustawienie czasu 0 wlacza zasilacz na czas nieokrelsony i jego wylaczenie nastepuje w momencie wyslania komendy OFF
        [NonSerialized]
        private double           deviationSPTmp = 0;    //procentowe okreslenie max odchylki

        [NonSerialized]
        Chamber.PowerSupplay     powerSupply    = null;    
        
        //-----------------------------------------------------------------------------------------------------------
        public PlasmaProces(Chamber.PowerSupplay aPowerSupply)
        {
            //zeruja godziny/minuty/sekundy
            timeOperate = DateTime.Now;
            timeOperate = timeOperate.AddHours(-DateTime.Now.Hour);
            timeOperate = timeOperate.AddMinutes(-DateTime.Now.Minute);
            timeOperate = timeOperate.AddSeconds(-DateTime.Now.Second);

            timeOperateTmp = timeOperate;

            powerSupply = aPowerSupply;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zdanie aktualizjace parametrow subprogramup plazmy odczytanych ze sterownika PLC
        */
        public override void UpdateData(SubprogramData aSubprogramData)
        {
           // workMode    = (Types.WorkModeHV)aSubprogramData.HV_Operate_Mode;
            setpoint    = aSubprogramData.HV_Setpoint;
            deviationSP = ConvertToPercent(setpoint,aSubprogramData.HV_Deviation); // Wartosc odczytana z PLC kest wyrazona w jednostakch setpointa. Nalezy to przeliczyc na procenty
            timeOperate = ConvertDate(aSubprogramData.HV_TargetTime);
            timeWorking = ConvertDate(aSubprogramData.WorkingTimeHV);

            ReadActiveWithCMD(aSubprogramData.Command, Types.BIT_CMD_HV);//Sprawdz czy w danym programie jest wykorzystywany subprogram przedmuchu
            //       deviationSP = aSubprogramData.
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu plasmy 
        */
        override public void PrepareDataPLC(int[] aData)
        {
            if (active)
            {
                double aDev = setpoint * deviationSP / 100 ;//Wartsc devation kjest wyrazona w procentach a do PLC nalezy ja przeslac w jednostakach setpointa
                aData[Types.OFFSET_SEQ_CMD] |= (int)System.Math.Pow(2, Types.BIT_CMD_HV);
                aData[Types.OFFSET_SEQ_HV_OPERATE] = (int)workMode;
                aData[Types.OFFSET_SEQ_HV_TIME] = timeOperate.Hour * 3600 + timeOperate.Minute * 60 + timeOperate.Second;
                aData[Types.OFFSET_SEQ_HV_SETPOINT] = Types.ConvertDOUBLEToWORD(setpoint, Types.Word.LOW);
                aData[Types.OFFSET_SEQ_HV_SETPOINT + 1] = Types.ConvertDOUBLEToWORD(setpoint, Types.Word.HIGH);
                aData[Types.OFFSET_SEQ_HV_DRIFT_SETPOINT] = Types.ConvertDOUBLEToWORD(aDev, Types.Word.LOW);
                aData[Types.OFFSET_SEQ_HV_DRIFT_SETPOINT + 1] = Types.ConvertDOUBLEToWORD(aDev, Types.Word.HIGH);
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
                timeOperate = timeOperateTmp;
                setpoint = setpointTmp;
                deviationSP = deviationSPTmp;
                ChangesNotSave = true;
            }
            else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
            {
                timeOperateTmp  = timeOperate;
                setpointTmp     = setpoint;
                deviationSPTmp  = deviationSP;
            }
            Changes = false;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
          * Metoda ma za zadanie ustawienie czasu wlaczenia zasilacza na dana moc
          */
        public void SetTimeOperate(DateTime aTime, bool tmpValue = false)
        {
            //Dodaj rok aby mozna bylo te date wyswietlic. Nas interesuje tylko czas
            if (aTime.Year < 2000)
                aTime = aTime.AddYears(2000);
        
            //Sprawdz czy nie zmienia parametrow procesu ktory nie zostanie ustawiony jako rzeczywisty
            if (timeOperate != aTime && tmpValue)
                Changes = true;

            timeOperateTmp = aTime;
            //Ustaw wartosc rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                timeOperate = aTime;
         }
        //-----------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zworcenie czasu pracy zasilacza
        */
        public DateTime GetTimeOperate()
        {
            return timeOperate;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zwrocenie trybu pracy zasilacza
        */
        public Types.WorkModeHV GetWorkMode()
        {
            return workMode;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia setpoint zasilacza
         */ 
        public void SetSetpointValue(double aValueSP, bool tmpValue = false)
        {
            //zaokraglij do 3 miejsc po przecinku
            aValueSP = Math.Round(aValueSP, 3);

            //Sprawdz czy nie zmienia parametrow procesu ktory nie zostanie ustawiony jako rzeczywisty
            if (setpoint != aValueSP && tmpValue)
                Changes = true;

            setpointTmp = aValueSP;
            //Ustaw wartosc rzeczywista jezeli nie jest tymczasowa
            if(!tmpValue)
                setpoint = aValueSP;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca wartosc nastawy zasilacza
         */
        public double GetSetpointValue()
        {
            return setpoint;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia nastawe zasilacza wyrazona w procentach
         */
        public void SetSetpointPercent(int aValue, bool tmpValue = false)
        {
            if (powerSupply != null)
            {
                double maxPower     = powerSupply.MaxPower;
                double maxVoltage   = powerSupply.MaxVoltage;
                double maxCurrent   = powerSupply.MaxCurent;
                double aSetpoint    = 0;
                switch (workMode)
                {
                    case Types.WorkModeHV.Power:
                        aSetpoint = maxPower * aValue / 100;
                        break;
                    case Types.WorkModeHV.Voltage:
                        aSetpoint = maxVoltage * aValue / 100;
                        break;
                    case Types.WorkModeHV.Curent:
                        aSetpoint = maxCurrent * aValue / 100;
                        break;
                }
                //Sprawdz czy nie zmienia parametrow procesu ktory nie zostanie ustawiony jako rzeczywisty
                if (aSetpoint != setpoint && tmpValue)
                    Changes = true;

                setpointTmp = aSetpoint;
                if (!tmpValue)
                    setpoint = aSetpoint;
            }
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca nastawe zasilacza wyrazona w procentach
         */
        public int GetSetpointPercent()
        {
            int aPercentValue = 0;
            if (powerSupply != null)
            {
                double maxPower     = powerSupply.MaxPower;
                double maxVoltage   = powerSupply.MaxVoltage;
                double maxCurrent   = powerSupply.MaxCurent;

                switch (workMode)
                {
                    case Types.WorkModeHV.Power:
                        if (maxPower > 0)
                            aPercentValue = (int)(setpoint / maxPower * 100);
                        break;
                    case Types.WorkModeHV.Voltage:
                        if (maxVoltage > 0)
                            aPercentValue = (int)(setpoint / maxVoltage * 100);
                        break;
                    case Types.WorkModeHV.Curent:
                        if (maxCurrent > 0)
                            aPercentValue = (int)(setpoint / maxCurrent * 100);
                        break;
                }
            }
            return aPercentValue;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie dozwolonego odchylenie wartosci od ustawionego setpointa [%]
         */ 
        public void SetDeviation(double aValue, bool tmpValue = false)
        {
            //zaokraglij do 3 miejsc po przecinku
            aValue = Math.Round(aValue, 3);

            //Sprawdz czy nie zmienia parametrow procesu ktory nie zostanie ustawiony jako rzeczywisty
            if (deviationSP != aValue && tmpValue)
                Changes = true;

            deviationSPTmp = aValue;
            //Jezlei wartosc nie jest tymczasowan to ustaw jako rzeczywsita
            if (!tmpValue)
                deviationSP = aValue;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
          * Metoda zwraca dozwolne odchylenie od wartoci zasilacza
          */
        public double GetDeviation()
        {
            return deviationSP;
        }
        //-----------------------------------------------------------------------------------------------------------
        /**
        *Funkcja ma za zadanie wyliczenie jaka czesci procentowa parametru 1 jest parametr 2
        */
        private int ConvertToPercent(double aValue1, double aValue2)
        {
            int aRes = 0;

            if (aValue1 != 0)
                aRes = (int)(aValue2 / aValue1 * 100);

            return aRes;
        }
        //-----------------------------------------------------------------------------------------------------------
    }
}
