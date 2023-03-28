using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa jest wykorzystywana do powiazania wartosci parametru fizycznego urzadzenia z parametrem urzadzenia ktore jest zapisywane do bazy danych
    /// Taki sposob trzymania wartosci jako obiektu w prosty sposob umozliwia aktualizacje wartosci odczytanej z plc i mam pewnosc ze zawsze jest ona aktualna bo taki obiekt jest utworzony jeden dla danego parametru
    /// </summary>

    public class Value
    {
        //reprezentajca wartosci
        private double value_    =  0;  //Aktualna wartosc  
        private double lastValue =  0;  //Zmienna pomocnicza okreslajaca ostatnia wartosc parametru 

        //------------------------------------------------------------------------------------------------
        public double Value_
        {
            set { value_ = value;  }
            get { return value_; }
        }
        //------------------------------------------------------------------------------------------------
        /**
         * Ostatnia wartosc jaka zostala zapisana w bazie danych
         */
        public double LastValueDB
        {
            set { lastValue = value; }
            get { return lastValue; }
        }
        //------------------------------------------------------------------------------------------------

    }
}
