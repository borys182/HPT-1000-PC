using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Program
{
    //-------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Klasa reprezentuje podstawowe parametry subprogramu ktore powinny zostac zapisane w bazie danych w formie byte 
    /// </summary>
    [Serializable]
    public class SubprogramParameter
    {
        private string          name            = "";           ///< Nazwa subprogramu prezentowana na panelu
        private string          description     = "";                           ///< Opis pozwala dokladnie opsac co dany subpram moze robic
        private FlushProces     flushProces     = null;
        private PumpProces      pumpProces      = null;
        private VentProces      ventProces      = null;
        private MotorProces     motorProces     = null;
        private PlasmaProces    plasmaProces    = null;
        private GasProces       gasProces       = null;

        //-------------------------------------------------------------------------------------------------------------------------
        public string Name
        {
            get { return name; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public string Description
        {
            get { return description; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public FlushProces Flush
        {
            get { return flushProces; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public PumpProces Pump
        {
            get { return pumpProces; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public VentProces Vent
        {
            get { return ventProces; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public GasProces Gas
        {
            get { return gasProces; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public PlasmaProces Plasma
        {
            get { return plasmaProces; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public MotorProces Motor
        {
            get { return motorProces; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy kotry inicalizuje wymagane pola do zapisu w bazi ktore sa uznawane za podstawowe bazujac na zrodlowym programie
         */
        public SubprogramParameter(Subprogram subPr)
        {
            if (subPr != null)
            {
                //Ustaw wartosci pola nazwa i opis
                name        = subPr.GetName();
                description = subPr.GetDescription();
                //Odczytaj obiekty
                pumpProces = subPr.GetPumpProces();
                flushProces = subPr.GetPurgeProces();
                ventProces = subPr.GetVentProces();
                motorProces = subPr.GetMotorProces();
                plasmaProces = subPr.GetPlasmaProces();
                gasProces = subPr.GetGasProces();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
    };
    /// <summary>
    /// Klasa reprezentuje subprogram. Posiada listę subprogramow oraz wlasny name, status opis oraz id odczytywane/zapisywne do PLC
    /// </summary>
    public class Subprogram : Object
    {
        private string                  name        = "Subprogram name";           ///< Nazwa subprogramu prezentowana na panelu
        private Types.StatusProgram     status      = Types.StatusProgram.Stop;     ///< Status odyczytwany z PLC (moze byc Wait/Run/Error/Stop)
        private string                  description = "";                           ///< Opis pozwala dokladnie opsac co dany subpram moze robic
        private uint                    id          = 0;                            ///< Identyfikacja subprogramu nadawan przez baze danych zapisywana takze do PLC aby mozna bylo wiedziec ktory subprogram jest wykomywany przez PLC
        private int                     ordinalNr   = 0;                            ///< Numer porzadkowy na liscie subprogram programu do ktorego naleze
        private static RefreshProgram   refreshProgram = null;

        private bool                    changes     = false;
        private bool                    changesNotSave = false;
        private string                  nameTmp     = "Subprogram name";
        //Utworz tablice segmentow obiektu subpramu z ktorych moze sie on skladac
        public ProcesObject[] stepObjects = new ProcesObject[6]; //Każdy segment może się składać max z 6 procesow (tyle mamy obiektow procesow)

        public ProcesObject PumpProces      { get { return stepObjects[0]; } }
        public ProcesObject GasProces       { get { return stepObjects[1]; } }
        public ProcesObject PlasmaProces    { get { return stepObjects[2]; } }
        public ProcesObject PurgeProces     { get { return stepObjects[3]; } }
        public ProcesObject VentProces      { get { return stepObjects[4]; } }
        public ProcesObject MotorProces     { get { return stepObjects[5]; } }

        //------------------------------------------------------------------------------------------------------------------------------------------------------
        public int OrdinalNumber
        {
            set { ordinalNr = value; }
            get { return ordinalNr; }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        public bool Changes
        {
            get
            {
                bool changes = this.changes;
                foreach (ProcesObject proces in stepObjects)
                    changes |= proces.Changes;
                
                return changes;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        public uint ID
        {   
            set { id = value; }
            get { return id; }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy odpowiedzialny za utworzenie obiektow oraz ich inicjalizacje 
         */ 
        public Subprogram()
        {
            stepObjects[0] = new PumpProces();
            stepObjects[1] = new GasProces();
            stepObjects[2] = Factory.CreatePlasmaProces(); //Poniewaz proces plazmy do poprawngo dzialania wymaga referencji obiektu PowerSupply dlatego tworze PlasmaProcess z wykorzystniem fabryki aby zainijcliziowac poprawnie wymagane referencje
            stepObjects[3] = new FlushProces();
            stepObjects[4] = new VentProces();
            stepObjects[5] = new MotorProces();

            name = "Subprogram " + id.ToString();
            nameTmp = name;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda porownuje referencje subprogramow - wymagane podczas wizualizacji
         */ 
        public override bool Equals(object other)
        {
            bool aRes = false;

            //Porownuje tylko po referencji bo w kilku miejscach sie odnosze do tej samej referencji i cos zmieniam.
            if (ReferenceEquals(this, other))// || (this.GetType() == other.GetType() && ((Subprogram)this).id == ((Subprogram)other).ID))
                aRes = true;

            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja aktualizaje dane na temat subprogramu wykonywanego aktulnie w sterowniku PLC
         */
        public void UpdateData(SubprogramData aSubprogramData)
        {
            stepObjects[0].UpdateData(aSubprogramData);
            stepObjects[1].UpdateData(aSubprogramData);
            stepObjects[2].UpdateData(aSubprogramData);
            stepObjects[3].UpdateData(aSubprogramData);
            stepObjects[4].UpdateData(aSubprogramData);
            stepObjects[5].UpdateData(aSubprogramData);
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Status jest odczytywany ze zbiorczej tablicy w celu optyamlizacji odczytu danych z PLC
         */
        public void UpdateStatus(Types.StatusProgram aStatus)
        {
            status = aStatus;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zapisanie danych sybprogramu w bazie danych oraz ustawienie wartosci tymczasowych jako aktualne
         */ 
        public int SaveDataInDB()
        {
            int res = 1;
            if (Factory.DataBase != null)
            {
                SetEditableParameters(true);
                res = Factory.DataBase.ModifySubprogram(this);
                for (int i = 0; i < stepObjects.Length; i++)
                {
                    stepObjects[i].Changes = false;
                    stepObjects[i].ChangesNotSave = false;
                }
                changes         = false;
                changesNotSave  = false;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zapisanie tymczasowych wartosci parametrow jako rzeczywiste/aktualne 
         */
        public void SetEditableParameters(bool changesStore)
        {            
            if (changesStore)
            {
                name = nameTmp;
                changesNotSave = true;
            }
            else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
            {
                nameTmp = name;
            }
            //Wywolaj metode dla procesow
            foreach (ProcesObject proces in stepObjects)
                proces.SetEditableParameters(changesStore);
            changes = false;

        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja zwraca tablice danych segmentu przygotowana zgodnie z segmentem po stronie PLC
         */
        public int[] GetPLCSegmentData()
        {
            int[] aData = new int[Types.SEGMENT_SIZE];

            for (int i = 0; i < stepObjects.Length; i++)
                stepObjects[i].PrepareDataPLC(aData);
            //Ustaw takze ID subprogramu
            if (aData.Length > Types.OFFSET_SEQ_ID)
                aData[Types.OFFSET_SEQ_ID] = (int)id;
            return aData;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referencje obiektu wykorzystywanego do ustawiania parametrow subprogramu odpowiedzialnego za sterowania pompowanie
         */ 
        public PumpProces GetPumpProces()
        {
            return (PumpProces)stepObjects[0];
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referencje obiektu wykorzystywanego do ustawiania parametrow subprogramu odpowiedzialnego za sterowania gazami
         */
        public GasProces GetGasProces()
        {
            return (GasProces)stepObjects[1];
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referencje obiektu wykorzystywanego do ustawiania parametrow subprogramu odpowiedzialnego za sterowania plazma
         */
        public PlasmaProces GetPlasmaProces()
        {
            return (PlasmaProces)stepObjects[2];
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referencje obiektu wykorzystywanego do ustawiania parametrow subprogramu odpowiedzialnego za sterowania przedmuch
         */
        public FlushProces GetPurgeProces()
        {
            return (FlushProces)stepObjects[3];
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referencje obiektu wykorzystywanego do ustawiania parametrow subprogramu odpowiedzialnego za sterowania wentowanie
         */
        public VentProces GetVentProces()
        {
            return (VentProces)stepObjects[4];
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca referencje obiektu wykorzystywanego do ustawiania parametrow subprogramu odpowiedzialnego za sterowania motorami bebna
         */
        public MotorProces GetMotorProces()
        {
            return (MotorProces)stepObjects[5];
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia nazwe subprogramu
         */
        public void SetName(string aName, bool tmpValue = false)
        {
            if (tmpValue && name != aName)
                changes = true;

            nameTmp = aName;
            if(!tmpValue)
                name = aName;

            if (refreshProgram != null)
                refreshProgram();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia nazwe subprogramu
         */
        public string GetName()
        {
            return name;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda
         */
        public override string ToString()
        {
            return GetName();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia opis subprogramu
         */
        public void SetDescription(string aDesc)
        {
            description = aDesc;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca opis subprogramu
         */
        public string GetDescription()
        {
            return description;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca staus subprogramu
         */
        public Types.StatusProgram GetStatus()
        {
            return status;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda dodaje delegata do listy kotra powinna sie odsiweyc w momencie zmian subprogramu
         */
        public static void AddToRefreshList(RefreshProgram aRefresh)
        {
            refreshProgram = aRefresh;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca indfomracje mowiaca o tym ze sa jakies nie zapisane zmiany w subprogramie
         */ 
        public bool IsAnyChangesNotSave()
        {
            bool aRes =  changesNotSave;

            foreach (ProcesObject proces in stepObjects)
                aRes |= proces.ChangesNotSave;

            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie podstawowych parametrow danego subprogramu ktroe zostana zapisane w bazie danych
         */ 
         public SubprogramParameter GetParameters()
        {
            SubprogramParameter subprogramPara = new SubprogramParameter(this);
            return subprogramPara;
        }
        //-------------------------------------------------------------------------------------------------------------------------

    }
}
