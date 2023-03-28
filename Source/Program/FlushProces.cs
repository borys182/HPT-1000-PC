using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Program
{
    /// <summary>
    /// Klasa jest odpowiedzialna za repreznetowanie mozliwosci wykonania przedmuchu komory jako jednego z elementow programu automatycznego. Zadaniem przedmuch jest wlaczenia na zdanyc czas odpowiedniego zaworu.
    /// Parametry konieczne do ustawienia to: czas przedmuchu
    /// </summary>
    [Serializable]
    public class FlushProces : ProcesObject
    {
        private DateTime timeFlush ;   //czas przedmuchu podawany [s]
        [NonSerialized]
        private DateTime timeFlushTmp;   //czas przedmuchu podawany [s]
        //----------------------------------------------------------------------------------------------------
        public FlushProces()
        {
            //zeruja godziny/minuty/sekundy
            timeFlush = DateTime.Now;
            timeFlush = timeFlush.AddHours(-DateTime.Now.Hour);
            timeFlush = timeFlush.AddMinutes(-DateTime.Now.Minute);
            timeFlush = timeFlush.AddSeconds(-DateTime.Now.Second);

            timeFlushTmp = timeFlush;
        }
        //----------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zdanie aktualizjace parametrow subprogramup rzedmuchu odczytanych ze sterownika PLC
         */ 
        public override void UpdateData(SubprogramData aSubprogramData)
        {            
            timeFlush   = ConvertDate(aSubprogramData.Flush_TargetTime);    //Odczytaj czas przedmuchu jaki jest ustawiony dla progrmau w PLC
            timeWorking = ConvertDate(aSubprogramData.WorkingTimeFlush);    //Odczytah czas pracy programu w PLC

            ReadActiveWithCMD(aSubprogramData.Command, Types.BIT_CMD_FLUSH);    //Sprawdz czy w danym programie jest wykorzystywany subprogram przedmuchu
        }
        //----------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu przedmuchu 
         */
        override public void PrepareDataPLC(int[] aData)
        {
            if (active)
            {
                aData[Types.OFFSET_SEQ_CMD]          |= (int)System.Math.Pow(2, Types.BIT_CMD_FLUSH);   //Ustaw dany bit w slowie sterujacym programem a ktory wskazuje czy subprgroma przedmuchu jest aktywny
                aData[Types.OFFSET_SEQ_FLUSH_TIME]   = timeFlush.Hour * 3600 + timeFlush.Minute * 60 + timeFlush.Second;  //Ustaw czas przedmuchu w [s]
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
                timeFlush = timeFlushTmp;
                ChangesNotSave = true;
            }
            else
                timeFlushTmp = timeFlush;

            Changes = false;
        }
        //----------------------------------------------------------------------------------------------------
        /**
          * Ustaw czas przedmuchu
          */
        public void SetTimePurge(DateTime aTime,bool tmpValue = false)
        {
            //Dodaj rok aby mozna bylo te date wyswietlic. Nas interesuje tylko czas
            if (aTime.Year < 2000)
                aTime = aTime.AddYears(2000);
      
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (timeFlush != aTime && tmpValue)
                Changes = true;

            timeFlushTmp = aTime;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                timeFlush = aTime;
           
        }
        //----------------------------------------------------------------------------------------------------
        /**
         * Podaj czas przedmuchu 
         */
        public DateTime GetTimePurge()
        {
            return timeFlush;
        }
        //----------------------------------------------------------------------------------------------------
    }
}
