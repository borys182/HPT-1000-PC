using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Program
{
    /// <summary>
    /// Klasa jest odpowiedzialna za repreznetowanie mozliwosci wykonania wentowania komory jako jednego z elementow programu automatycznego. Zadaniem wentowania jest wlaczenia na zdanyc czas odpowiedniego zaworu.
    /// Parametry konieczne do ustawienia to: czas ventowania
    /// </summary>
    [Serializable]
    public class VentProces : ProcesObject
    {
        private DateTime timeVent ;       ///< Okreslenie czasu ventowania [s]
        [NonSerialized]
        private DateTime timeVentTmp;     ///< Okreslenie czasu ventowania [s]
        //---------------------------------------------------------------------------------------------------------------------
        public VentProces()
        {
            //Zeruj czas ventowania
            timeVent = DateTime.Now;
            timeVent = timeVent.AddHours(-DateTime.Now.Hour);
            timeVent = timeVent.AddMinutes(-DateTime.Now.Minute);
            timeVent = timeVent.AddSeconds(-DateTime.Now.Second);

            timeVentTmp = timeVent;
        }
        //---------------------------------------------------------------------------------------------------------------------
       /**
       * Metoda ma za zdanie aktualizjace parametrow subprogramup wentowania odczytanych ze sterownika PLC
       */
        public override void UpdateData(SubprogramData aSubprogramData)
        {
            timeVent    = ConvertDate(aSubprogramData.Vent_TargetTime);
            timeWorking = ConvertDate(aSubprogramData.WorkingTimeVent);

            ReadActiveWithCMD(aSubprogramData.Command, Types.BIT_CMD_VENT);//Sprawdz czy w danym programie jest wykorzystywany subprogram wentowania
        }
        //---------------------------------------------------------------------------------------------------------------------
        /**
       * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu wentowania 
       */
        override public void PrepareDataPLC(int[] aData)
        {
            if (active)
            {
                aData[Types.OFFSET_SEQ_CMD]           |= (int)System.Math.Pow(2, Types.BIT_CMD_VENT);
                aData[Types.OFFSET_SEQ_VENT_TIME]    = timeVent.Hour * 3600 + timeVent.Minute * 60 + timeVent.Second; 
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
                timeVent = timeVentTmp;
                ChangesNotSave = true;
            }
            else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
                timeVentTmp = timeVent;

            Changes = false;
        }
        //---------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie czasu wentowania. Parametr okresla czy wpisujemy parametr jako wartosc rzeczywista czy tymczasowa
         */
        public void SetTimeVent(DateTime aTime, bool tmpValue = false)
        {
            //Dodaj rok aby mozna bylo te date wyswietlic. Nas interesuje tylko czas
            if (aTime.Year < 2000)
                aTime = aTime.AddYears(2000);

            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (timeVent != aTime && tmpValue)
                Changes = true;

            timeVentTmp = aTime;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                timeVent = aTime;            
        }
        //---------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie podanie czasu ventowania
         */
        public DateTime GetTimeVent()
        {
            return timeVent;
        }
        //---------------------------------------------------------------------------------------------------------------------
    }
}
