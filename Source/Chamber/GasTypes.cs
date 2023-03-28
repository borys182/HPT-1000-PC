using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPT1000.Source.Chamber
{
    public delegate void RefreshGasType();

    /// <summary>
    /// Klasa reprezentuje gas jaki mozna podpiac pod przeplywke MFC. Jest on defioniowany poprzez nazwe, opis, factor oraz ID na podstawie ktorego zachodzi powieaznie go z przeplywka
    /// </summary>
    public class GasType
    {
        private int     id          = 0;                ///< ID gazu pochodzi z bazy danych i jest wykorzysytwane do powiazania gazu z przeplywka
        private string  name        = "New gas type";   ///< Nazwa gazu
        private string  description = "";               ///< Opis gazu
        private double  factor      = 0; ///< Okresleneie factora dla danego gazu podpietego do danej przeplywki. Przeplywki
                                         ///< sa skalibrowane na jeden gaz i podpiecie innego wymusza ustawienie factora dla poprawnych przeliczen przeplywu
        private bool    savedInDB   = false;

        private DB dataBase     = null;                     ///< Referencja obiektu bazy danych

        private static RefreshGasType refreshObject = null; ///<

        ///< Grupa zmiennych tymczasowych wartosci parametrow gazu. Jest wykrozystywan w mechanizmie anulowania wprowadzania zmian do gazu
        private bool   changes          = false;           ///< Flga okresl czy sa jakies zmiany w ustawieniach gazu
        private string nameTmp          = "New gas type";   ///< Nazwa gazu
        private string descriptionTmp   = "";               ///< Opis gazu
        private double factorTmp        = 0;                ///< Okresleneie factora dla danego gazu podpietego do danej przeplywki. Przeplywki

        //--------------------------------------------------------------------------------------------------------------
        public bool Changes
        {
            get { return changes; }
        }
        //--------------------------------------------------------------------------------------------------------------
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        //--------------------------------------------------------------------------------------------------------------
        public string Name
        {
            set
            {
                name = value;
                nameTmp = name;
                if (refreshObject != null)
                    refreshObject();
            }
            get { return name; }
        }
        //--------------------------------------------------------------------------------------------------------------
        public string Description
        {
            set
            {
                description = value;
                descriptionTmp = description;
            }
            get { return description; }
        }
        //--------------------------------------------------------------------------------------------------------------
        public double Factor
        {
            set
            {
                factor = value;
                factorTmp = factor;
            }
            get { return factor; }
        }
        //--------------------------------------------------------------------------------------------------------------
        public string NameTmp
        {
            set
            {
                if (nameTmp != value)
                    changes = true;
                nameTmp = value;
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public string DescriptionTmp
        {
            set
            {
                if (descriptionTmp != value)
                    changes = true;
                descriptionTmp = value;
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public double FactorTmp
        {
            set
            {
                if (factorTmp != value)
                    changes = true;
                factorTmp = value;
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        public DB DataBase
        {
            set { dataBase = value; }
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor
         */ 
         public GasType(bool loadFromDB)
        {
            savedInDB = loadFromDB;
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Metoda sluzy do porowania dowch gazow. Porownujemy ich wartosci a nie referencje.
         */
        public override bool Equals(object obj)
        {
            bool aRes = false;
            if (obj != null && obj is GasType)
            {
                GasType gasType = (GasType)obj;
                if (gasType.ID == id && gasType.Name == name && gasType.Description == description && gasType.Factor == factor)
                    aRes = true;
            }
            return aRes;
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Przeciarzenie metofdy ToString pozwalajace zwraca nazwe gazu
         */ 
        public override string ToString()
        {
            return name;
        }
        //--------------------------------------------------------------------------------------------------------------
        public static void SetDelegateToRefreshInfo(RefreshGasType aRefresh)
        {
            refreshObject = aRefresh;
        }
        //--------------------------------------------------------------------------------------------------------------
        public int Save()
        {
            int aRes = -1;
            if (dataBase != null)
            {
                //Jezeli gaz nie zostal jeszcze zapisany w bazie to go zapisz w przeciwnym razie modyfikuj
                if(savedInDB)
                    aRes = dataBase.ModifyGasType(this);
                else
                {
                    aRes = dataBase.AddGasType(this);
                    if (aRes == 0)
                        savedInDB = true;
                }
            }
            ClearChanges();
            return aRes;
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Metoda kasuje flage okresljaaca ze zaszly zmiany w ustawineach danego gazu
         */
         public void ClearChanges()
        {
            changes         = false;
            nameTmp         = name;
            descriptionTmp  = description;
            factorTmp       = factor;
        } 
    }
    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Klasa reprezentuje liste gazow jakie zostaly zdefioniowane w systemie
    /// </summary>
    public class GasTypes
    {
  
        private List<GasType> items = new List<GasType>();  ///< Lista gazow
        private DB dataBase         = null;                 ///< Referencja na baze danych

        private static RefreshGasType refreshObject = null; ///< Odswiezanie obserwatroew gdy zmianie ulegna wartosci gazow
        //--------------------------------------------------------------------------------------------------------------
        public List<GasType> Items
        {
            get { return items; }
        }
        //--------------------------------------------------------------------------------------------------------------
        public DB DataBase
        {
            set { dataBase = value; }
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie odczytanie zapisanych gazow z bazy danych
         */ 
        public void LoadGasType()
        {
            if(dataBase != null)
            {
                foreach (GasType gasType in dataBase.GetGasTypes())
                {
                    gasType.DataBase = dataBase;
                    items.Add(gasType);
                    if (refreshObject != null)
                        refreshObject();
                }
            }
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Metoda dodaje nowy gaz do listy oraz inforumuje o tym obserwatorow. Dane sa zapisywane w bazie danych
         */
        public void Add(GasType gasType)
        {
            items.Add(gasType);
            if (refreshObject != null)
                refreshObject();
            if (gasType != null)
                gasType.DataBase = dataBase;
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Metoda usuwa dany gaz oraz informuje o tym obserwatorow. Gaz jest usuwany z bazy danych
         */
        public int Remove(GasType gasType)
        {
            int aRes = -1;
            if (dataBase != null)
            {
                aRes = dataBase.RemoveGasType(gasType.ID);
                if(aRes == 0)
                {
                    items.Remove(gasType);
                    if (refreshObject != null)
                        refreshObject();
                }
            }
            return aRes;
        }
        //--------------------------------------------------------------------------------------------------------------
        /**
         * Metoda dodaje obserwatora do listy
         */
        public static void AddToRefreshList(RefreshGasType aRefresh)
        {
            refreshObject += aRefresh;
            GasType.SetDelegateToRefreshInfo(refreshObject);
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
          * Metoda zwraca info czy dany gaz juz istnieje
          */
        public bool Contains(GasType aGasType)
        {
            bool aRes = false;

            foreach(GasType gasType in items)
            {
                if (gasType.Name == aGasType.Name)
                    aRes = true;
            }

            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie podanie nazwy danego gazu
         */ 
         public string GetGasName(int id)
        {
            string aRes = "";

            foreach (GasType gasType in items)
            {
                if (gasType.ID == id)
                    aRes = gasType.Name;
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest podanie factora jaki odpowiada gazu o podanym ID
         */
         public float GetFactor(int aID)
        {
            float aFactor = 0;

            foreach (GasType gasType in items)
            {
                if (gasType.ID == aID)
                    aFactor = (float)gasType.Factor;
            }

            return aFactor;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Zadaniem metody jest podanie ID pierwszego gazu odpowadajacemu danemu faktorowi
        */
        public int GetID(double aFactor)
        {
            int aID = -1;

            foreach (GasType gasType in items)
            {
                //Porównuj z dokładnością do 0.001
                if (Math.Abs(gasType.Factor - aFactor) <= 0.001)
                {
                    aID = gasType.ID;
                    break;
                }
            }
            return aID;
        }
        //-------------------------------------------------------------------------------------------------------------------------

    }
}
