using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;

namespace HPT1000.Source.Program
{
    /// <summary>
    /// Klasa jest odpowiedzialna za repreznetowanie mozliwosci wykonania sterowania silnikiem bebna jako jednego z elementow programu automatycznego. Zadaniem sterowania bebnem jest wlaczenia silnika na zadany czas badz w nieskonczonosc
    /// </summary>
    [Serializable]
    public class MotorProces : ProcesObject
    {
        private DateTime        timeMotor1  = DateTime.Now;         ///< Czas pracy motora
        private DateTime        timeMotor2  = DateTime.Now;         ///< Czas pracy motora

        private bool activeMotor1 = false;               ///< Flaga aktywuje sterowanie procesem dla motora 1
        private bool activeMotor2 = false;               ///< Flaga aktywuje sterowanie procesem dla motora 2

        [NonSerialized]
        private Types.StateFP   stateMotor1_ToSet = Types.StateFP.ON;
        [NonSerialized]
        private Types.StateFP   stateMotor2_ToSet = Types.StateFP.ON;

        [NonSerialized]
        private Types.StateFP   stateMotor1_Read = Types.StateFP.OFF;
        [NonSerialized]
        private Types.StateFP   stateMotor2_Read = Types.StateFP.OFF;

        
        [NonSerialized]
        private DateTime        timeMotor1Tmp = DateTime.Now;       ///< Czas pracy motora
        [NonSerialized]
        private DateTime        timeMotor2Tmp = DateTime.Now;       ///< Czas pracy motora

        //--------------------------------------------------------------------------------------------------------
        public Types.StateFP StateMotor1_Read
        {
            get { return stateMotor1_Read; }
        }
        //--------------------------------------------------------------------------------------------------------
        public Types.StateFP StateMotor2_Read
        {
            get { return stateMotor2_Read; }
        }
        //--------------------------------------------------------------------------------------------------------
        public MotorProces()
        {
            //Ustaw zerowy czas dla zmiennej
            DateTime timeZero = DateTime.Now;
            timeZero = timeZero.AddHours(-DateTime.Now.Hour);
            timeZero = timeZero.AddMinutes(-DateTime.Now.Minute);
            timeZero = timeZero.AddSeconds(-DateTime.Now.Second);

            timeMotor1      = timeZero;    
            timeMotor2      = timeZero;
            timeMotor1Tmp   = timeZero;
            timeMotor2Tmp   = timeZero;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zdanie aktualizjace parametrow subprogramup pompowania odczytanych ze sterownika PLC
        */
        public override void UpdateData(SubprogramData aSubprogramData)
        {
                timeMotor1        = ConvertDate(aSubprogramData.Motor1_OperateTime);
                stateMotor1_Read  = (Types.StateFP)Enum.Parse(typeof(Types.StateFP), aSubprogramData.Motor1_Command.ToString());
                
                timeMotor2        = ConvertDate(aSubprogramData.Motor2_OperateTime);
                stateMotor2_Read  = (Types.StateFP)Enum.Parse(typeof(Types.StateFP), aSubprogramData.Motor2_Command.ToString());

                timeWorking = ConvertDate(aSubprogramData.WorkingTimeMotor);

                ReadActiveWithCMD(aSubprogramData.Command, Types.BIT_CMD_MOTOR1);//Sprawdz czy w danym programie jest wykorzystywany subprogram motora 1
                //jezeli nadal jestem nieaktywny to sprawdz motor2
                if(!active)
                    ReadActiveWithCMD(aSubprogramData.Command, Types.BIT_CMD_MOTOR2);//Sprawdz czy w danym programie jest wykorzystywany subprogram motora 2
        }
        //--------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu przedmuchu 
         */
        override public void PrepareDataPLC(int[] aData)
        {
            //Jezeli dany obiekt jest niedostepny fizycznie to nie mozna dla niego tworzyc programu dlatego Active ustaw wtedu na false
            activeMotor1 &= MotorDriver.Motor_1_Enable;
            activeMotor2 &= MotorDriver.Motor_2_Enable;

            if (active)
            {
                if(activeMotor1) aData[Types.OFFSET_SEQ_CMD] |= (int)System.Math.Pow(2, Types.BIT_CMD_MOTOR1);
                if(activeMotor2) aData[Types.OFFSET_SEQ_CMD] |= (int)System.Math.Pow(2, Types.BIT_CMD_MOTOR2);

                aData[Types.OFFSET_SEQ_MOTOR_1_TIME] = timeMotor1.Hour * 3600 + timeMotor1.Minute * 60 + timeMotor1.Second;
                aData[Types.OFFSET_SEQ_MOTOR_1_CMD]  = (int)stateMotor1_ToSet;

                aData[Types.OFFSET_SEQ_MOTOR_2_TIME] = timeMotor2.Hour * 3600 + timeMotor2.Minute * 60 + timeMotor2.Second;
                aData[Types.OFFSET_SEQ_MOTOR_2_CMD] = (int)stateMotor2_ToSet;
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
                timeMotor1 = timeMotor1Tmp;
                timeMotor2 = timeMotor2Tmp;
                ChangesNotSave = true;
            }
            else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
            {
                timeMotor1Tmp = timeMotor1;
                timeMotor2Tmp = timeMotor2;
            }
            Changes = false;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie stanu operate
         */
        public void SetState(int aMotorNo,Types.StateFP aState)
        {
            //Poprawak blokuje mozliwosc zmiany stanu motora na inny niz ON - motor zawsze jest wlaczany w programach
            /*
            if(aMotorNo == 1) stateMotor1_ToSet = aState;
            if(aMotorNo == 2) stateMotor2_ToSet = aState;
            */
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zwrocenie stanu motora
        */
        public Types.StateFP GetState(int aMotorNo)
        {
            Types.StateFP aState = Types.StateFP.Error;

            if (aMotorNo == 1) aState = stateMotor1_ToSet;
            if (aMotorNo == 2) aState = stateMotor2_ToSet;

            return aState;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie ustawienie czasu wlaczenia motora
        */
        public void SetTimeMotor(int aMotorNo, DateTime aTime , bool tmpValue = false)
        {
            //Dodaj rok aby mozna bylo te date wyswietlic. Nas interesuje tylko czas
            if (aTime.Year < 2000)
                aTime = aTime.AddYears(2000);

            if (aMotorNo == 1)
            {
                //Sprawdz czy nie zmian parametrow procesu
                if (timeMotor1 != aTime && tmpValue)
                    Changes = true;

                timeMotor1Tmp = aTime;
                //Ustaw wartosc rzeczywista czasu jezeli nie jest tymczasowa
                if (!tmpValue)
                    timeMotor1 = aTime;
            }
            if (aMotorNo == 2)
            {
                //Sprawdz czy nie zmian parametrow procesu
                if (timeMotor2 != aTime && tmpValue)
                    Changes = true;

                timeMotor2Tmp = aTime;
                //Ustaw wartosc rzeczywista czasu jezeli nie jest tymczasowa
                if (!tmpValue)
                    timeMotor2 = aTime;
            }
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zwrocenie max czasu oczekiwania na odpomowanie komory do zadanego setpointa
        */
        public DateTime GetTimeMotor(int aMotorNo)
        {
            DateTime dateTime = DateTime.Now;

            if (aMotorNo == 1) dateTime = timeMotor1;
            if (aMotorNo == 2) dateTime = timeMotor2;

            return dateTime;
        }
        //--------------------------------------------------------------------------------------------------------
        public void SetActive(int aMotorNo, bool aActive)
        {
            if (aMotorNo == 1) activeMotor1 = aActive;
            if (aMotorNo == 2) activeMotor2 = aActive;
        }
        //--------------------------------------------------------------------------------------------------------
        public bool GetActive(int aMotorNo)
        {
            bool aActive = false;

            if (aMotorNo == 1) aActive = activeMotor1;
            if (aMotorNo == 2) aActive = activeMotor2;

            return aActive;
        }     
        //--------------------------------------------------------------------------------------------------------

    }
}
