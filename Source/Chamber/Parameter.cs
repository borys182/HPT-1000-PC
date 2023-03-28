using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa jest odpowiedzialna za reprezentowanie parametru fizycznego urzadzenia w bazie danych. Poza podstawowymi danymi okreslajacymi dany parametr posiada
    /// takze informacje na temat jak czesto zapisywać dane w bazie danych
    /// </summary>
    public class Parameter
    {
        private int     id;                    //Identyfikator parametru w bazie danych
        private int     idConfigPara;          //Identyfikator w tabeli zawierajacej konfiguracje parametru
        private string  name;                  //Okreslenie nazwy parametru
        private Value   valuePtr    = null;    //wskaznik na obietk z aktualna wartoscia danego paramwetru
        private string  unit;

        private double  frequency       = 1;    //Parametr określa częstotliwość zapisu parametru do bazy danych [s]
        private double  diffValue       = 1;    //Parametr okresla roznice pomiedzy aktualna wartosci a ostatnia. Jezeli jest ona wieksza od zadanej to dana powinna zostac zapisana w bazie danych
        private bool    enabledAcq      = true; //Flaga okresl czy dany parametr ma byc zapisywany w bazie danych
        private bool    firstAsk        = true; //Flaga okresla czy pierwszy raz sie pytamy o roznice wartosci. Za pierwszym razem roznicy nie psrawdzam aby umozliwic zapisa pierwszej wartosci do bazy
        private Types.ModeAcq modeAcq   = Types.ModeAcq.Mixed;

        private long    lastDateSaved = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; //Czas ostatnio zapisanenj wartosci w bazie danych 
        //---------------------------------------------------------------------------------------------------------------------------
        public double Frequency
        {
            set { frequency = value; }
            get { return frequency; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public bool EnabledAcq
        {
            set { enabledAcq = value; }
            get { return enabledAcq; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public double Differance
        {
            set { diffValue = value; }
            get { return diffValue; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public string Unit
        {
            set { unit = value; }
            get { return unit; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public int ID_Configuration
        {
            set { idConfigPara = value; }
            get { return idConfigPara; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public Types.ModeAcq Mode
        {
            set { modeAcq = value; }
            get { return modeAcq; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Ustaw wskaznik na wartosc parametru. Umozliwia nam to dostep zawsze do aktaulknej wartosci parametru zmienianej w fizczynym urzadzeniu
         */
        public Value ValuePtr
        {
            set { valuePtr = value; }
            get { return valuePtr; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Podaj wartosc parametru
         */
        double GetValue()
        {
            double aRes = 0;

            if (valuePtr != null)
                aRes = valuePtr.Value_;

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Podaj info czy wartosc jest wieksza od zadeklarowanej roznicy i moze zostac zapisana do bazy. Przy pierwszym odpytywabiu nie sprawdzam warunku aby umozliwic wpisanie pierwszej wartosci do bazy
         */
        public bool IsDifferenceValue(bool skipMode = false)
        {
            bool aRes = false;
            //SPrawdz czy roznica wartosi aktualnej i ostatniej mieci sie w zadanej oraz czy tryb oracy sie zgadza
            if (firstAsk || ((valuePtr != null && Math.Abs(valuePtr.Value_ - valuePtr.LastValueDB) >= diffValue) && (modeAcq == Types.ModeAcq.Difference || skipMode)))
            {
                aRes = true;
                firstAsk = false;  //Umozliwinie zapisania pierwszej wartosci w bazie poprzez nie sprawdzanie roznocy wartosci. Dajemy w ten sposob szanse na poprawne zainicjalizowanie sie wartoscu ostatniej
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja podaje informacje czy dany parametr powinin zostac zapisany w bazie z uwagi na czestotliwosc zapisu
         */
        public bool IsTimeSavePara(bool skipMode = false)
        {
            bool aRes = false;

            if ((Math.Abs(DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond - lastDateSaved) >= frequency * 1000) && (modeAcq == Types.ModeAcq.Frequency || skipMode))
            {
                aRes = true;
                lastDateSaved = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            }
            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja podaje informacje czy dany parametr powinin zostac zapisany w bazie z uwagi na czestotliwosc zapisu
         */
        public bool IsMixed()
        {
            bool res = false;

            if(modeAcq == Types.ModeAcq.Mixed && IsTimeSavePara(true) && IsDifferenceValue(true))
                res = true;
            return res;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
