using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Program
{
    /// <summary>
    /// Klasa abstrakcyjna bedaca rodzicem klas mogacych byc suprogramami
    /// </summary>

    [Serializable]
    abstract public class ProcesObject
    {
        protected bool      active = true;          ///< Flaga okresla czy w danym segmenie dany obiekt procesu bierze udzial. Domyslnie ustawiam na true
        protected DateTime  timeWorking;            ///< Wartosc czasu pracy danego subpropgrmu
        [NonSerialized]
        private bool        changes = false;        ///< Flaga okresla ze sa niezapiseamne zminay w subprogramie
        [NonSerialized]
        private bool        changesNotSave = false; ///<  Flaga okresl informacje czy sa jakies zmiany ktore niz sotaly zapisane w bazie danych
        //-------------------------------------------------------------------------------------
        public bool Changes
        {
            set{ changes = value;  }
             get { return changes; }
        }
        //-------------------------------------------------------------------------------------
        public bool ChangesNotSave
        {
            set { changesNotSave = value; }
            get { return changesNotSave; }
        }
        //-------------------------------------------------------------------------------------
        public bool Active
        {
            set { active = value; }
            get { return active; }
        }
        //------------------------------------------------------------------------------------
        public DateTime TimeWorking
        {
            get { return timeWorking; }
        }
        //------------------------------------------------------------------------------------
        /**
         * Funkcja wymusza zaimplementowania  funkcji przygotowanieadanych  dla PLC zgodnie z przygotowana rozpiska pamieci przez poszczegolne subprogamy
         */
        abstract public void PrepareDataPLC(int[] aData);
        //------------------------------------------------------------------------------------
        /**
         * Funkcja wymusza zaimplementowania aktualizacji wlasnych parametrow przez poszczegolne subprogramy
        */
        abstract public void UpdateData(SubprogramData aSubprogramData);
        //------------------------------------------------------------------------------------
        /**
        * Funkcja ma za zadanie sprawdzenie czy w slowie komand jest ustawiony bit danego procesu. Jezeli tak to aktuwuj proces
        */
        protected void ReadActiveWithCMD(int aCMD, int aBitNr)
        {
            if ((aCMD & (int)System.Math.Pow(2, aBitNr)) != 0)
                active = true;
            else
                active = false;
        }
        //------------------------------------------------------------------------------------
        /**
         * Metoda sptawdza czy komenda zawiera ustawiony dany bit
         */ 
        protected bool IsBitActive(int aCMD, int aBitNr)
        {
            bool aRes = false;

            if ((aCMD & (int)System.Math.Pow(2, aBitNr)) != 0)
                aRes = true;

            return aRes;
        }
        //------------------------------------------------------------------------------------
        /**
         * Metoda pomocnicza konwetujac sekundy na typ DateTime
         */
        protected DateTime ConvertDate(int aSeconds)
        {
            DateTime aDateTime = DateTime.Now;

            aDateTime = aDateTime.AddHours(-DateTime.Now.Hour);
            aDateTime = aDateTime.AddMinutes(-DateTime.Now.Minute);
            aDateTime = aDateTime.AddSeconds(-DateTime.Now.Second);

            int aHour = aSeconds / (3600);
            int aMinute = (aSeconds - aHour * 3600) / 60;
            int aSecond = aSeconds - aHour * 3600 - aMinute * 60;

            aDateTime = aDateTime.AddHours(aHour);
            aDateTime = aDateTime.AddMinutes(aMinute);
            aDateTime = aDateTime.AddSeconds(aSecond);

            return aDateTime;
        }
        //------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest przypisanie tymczasowych wartosci parametrow procesu jako wartosci rzeczywistych/aktualnych
         */
        public virtual void SetEditableParameters(bool changesStore)
        {}
        //------------------------------------------------------------------------------------

    }
}