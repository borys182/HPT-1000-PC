using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa prezentuje tablice Device z bazy danych i jest odpowiedzialan za powiazanie fizycznych urzadzen komory z obiektami bazy danych. Klasa jest wykorzystywan do precesu akwizycji danych parametrow urzadzemn w bazie danychg
    /// </summary>
    public class Device
    {
        private int             id_DB;      ///< ID urzadzenia zapisanego w bazie danych
        protected string        name;       ///< Nazwa urzadzenia ktora musi byc unikalna dla calego systemu. Jest ona nadawana w konstruktorze urzadzenia a poznij dopiero zapisywana w bazie danych
                                            ///< Jezeli urzadzenie nie ma byc rejestrowane w bazie danych to nie ustawiam pola name
        private List<Parameter> parameters = new List<Parameter>(); ///< Lista parametrow urzadzenia ktore sa zapisywane w bazie danych
        protected bool          acqData = false; ///< Flaga okresla czy dane urzadzenie jest przenzaczone do archwizaji danych w bazie danych
       
        //GRUPA SETEROW
        //---------------------------------------------------------------------------------------------------------------------------
        public int ID_DB
        {
            set { id_DB = value; }
            get { return id_DB;  }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public string Name
        {
            get { return name; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        public bool AcqData
        {
            get { return acqData; }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda dodaje parametr urzadzenia okreslajac nazwe, jednostke oraz referencje na wartosc ktroa jest odswiezana w watku caly czas
         */ 
        public Parameter AddParameter(string name, Value value, string unit)
        {
            Parameter para  = new Parameter();
            para.Name       = name;
            para.ValuePtr   = value;
            para.Unit       = unit;
            parameters.Add(para);

            return para;       
        }
        //---------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca liste parametrow urzadzenia
         */
        public List<Parameter> GetParameters()
        {
            return parameters;
        }
        //---------------------------------------------------------------------------------------------------------------------------

    }
}
