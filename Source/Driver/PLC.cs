using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPT1000.Source.Driver
{
     /**
     * Klasa bazowa dla konkretnych typow PLC. Wymusza na nich implementacje funkcji do komunikacji oraz czytania/zapisywania danych
    */
    public abstract class PLC
    {
        private string            paraName  = "Parameter_PLC";           ///<Nazwa parametra pod ktora wszystkie infomacje na teamt obiektu PLC zostana zapisane w bazie danych
        protected Types.TypePLC   typePLC   = Types.TypePLC.L;           ///<Zmienna okrela typ sterownika PLC 
        protected Types.TypeComm  typeComm  = Types.TypeComm.USB;        ///<Zmienna okresla typ komunikacji sie ze sterownikiem PLC
        protected string          addressIP = "192.168.1.1";

        protected bool  dummyMode                   = false;
        protected int[] dummyModeStatusChamberData  = new int[Types.LENGHT_STATUS_DATA];

        protected DB    dataBase            = null;
        //----------------------------------------------------------------------------------------
        public DB DataBase
        {
            set
            {
                dataBase = value;
                //Zaladuj parametry PLC z bazy danych
                LoadData();
            }
        }
        //----------------------------------------------------------------------------------------
        abstract public int Connect();                                          ///<Funkcja ma za zadanie opierajac sie na parmaetrach komunikacyjnych polaczenie sie z PLC. Aktualne polaczenie zostaje przerwane
        //----------------------------------------------------------------------------------------
        abstract public int SetDevice(string aAddr, int aState);                ///<Funkcja ustawia przestrzn dyskrtna w PLC na podana wartosc
        //----------------------------------------------------------------------------------------
        abstract public int GetDevice(string aAddr, out int aState);            ///<Funkcja zwraca stan flagi spod danego adresu w przestrzeni PLC
        //----------------------------------------------------------------------------------------
         abstract public int WriteWords(string aAddr, int aSize, int[] aData);   ///<Zapisz w przestrzeni PLC dana liczbe rejestrow wartosciami 16 bitowymi
        //----------------------------------------------------------------------------------------
        abstract public int ReadWords(string aAddr, int aSize, int[] aData);    ///<Odczytaj z przestrzeni PLC dana liczbe rejestrow 16 bitowych
        //----------------------------------------------------------------------------------------
        abstract public int WriteRealData(string aAddr, float aValue);          ///<Zapisz w przestrzeni PLC liczbe zmienno przecinkowa (dwa rejestry 16 bitiwe)
        //----------------------------------------------------------------------------------------
        abstract public int ReadRealData(string aAddr , out float aValue);      ///<Odczytaj z przestrzeni PLC liczbe zmienno przecinkowa (dwa rejestry 16 bitiwe)
        //----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia adres IP na ktorym mam sie komunikowac z PLC
         */ 
        public string GetAddrIP()
        {
            return addressIP;
        }
        //----------------------------------------------------------------------------------------
        public void SetAddrIP(string aAddrIP)
        {
            addressIP = aAddrIP;
            //Zapisz parametr w bazie danych
            SaveData();
        }
        //----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia typ sterownika z kotrym sie chce komunikowac
         */
        public void SetTypePLC(Types.TypePLC aType)
        {
            typePLC = aType;
            //Zapisz parametr w bazie danych
            SaveData();
        }
        //----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca info na temat typu sterownika pLC
         */ 
        public Types.TypePLC GetTypePLC()
        {
            return typePLC;
        }
        //----------------------------------------------------------------------------------------
        /**
         * Metoda ustawia typ komunikacji
         */
        public void SetTypeComm(Types.TypeComm aType)
        {
            typeComm = aType;
            //Zapisz parametr w bazie danych
            SaveData();
        }
        //----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca typ komunikacji
         */
        public Types.TypeComm GetTypeComm()
        {
            return typeComm;
        }
        //----------------------------------------------------------------------------------------
        /**
         * Metoda wlacza/wylacza tryb dumy mode
         */
        public void SetDummyMode(bool aDummyMode)
        {
            dummyMode = aDummyMode;
        }
        //----------------------------------------------------------------------------------------
        public bool GetDummyMode()
        {
            return dummyMode;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja odczytuje z bazy danych zapisane wczesniej parametry
        */
        private void LoadData()
        {
            if (dataBase != null)
            {
                ProgramParameter parameter = new ProgramParameter();
                parameter.Name = paraName;
                dataBase.LoadParameter(parameter.Name, out parameter);
                if (parameter.Data != null)
                {
                    string[] parameters = parameter.Data.Split(';');
                    foreach (string para in parameters)
                    {
                        try
                        {
                            if (para.Contains("Type_PLC"))
                                typePLC = (Types.TypePLC)Enum.Parse(typeof(Types.TypePLC), para.Split('=')[1]);
                            if (para.Contains("Type_Communication"))
                                typeComm = (Types.TypeComm)Enum.Parse(typeof(Types.TypeComm), para.Split('=')[1]);
                            if (para.Contains("IP_Address"))
                                addressIP =  para.Split('=')[1].TrimStart();
                        }
                        catch (Exception ex)
                        {
                            Logger.AddException(ex);
                        }
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja zapisuje do bazu danych informacje na temat powiazanego typu gazu oraz czy jest aktywna czy nie
        */
        private void SaveData()
        {
            ProgramParameter parameter = new ProgramParameter();

            parameter.Name = paraName;
            parameter.Data =  "Type_PLC = " + typePLC + ";" ;
            parameter.Data += "Type_Communication = " + typeComm + ";";
            parameter.Data += "IP_Address = " + addressIP + ";";

            int aRes = dataBase.SaveParameter(parameter);
        }
        //----------------------------------------------------------------------------------------
    }
}
