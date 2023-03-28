using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
namespace HPT1000.Source
{
    /*
     * Klasa służy do tworzenia obiektow oraz inicjalizaci ich jak i ustawianie wymaganych referencji. Upraszcza to zarzadzanie referencjami które musza posiadac niektore obiketu podczas ich tworzenia
     */ 
    public class Factory
    {
        private static PLC  plc                             = null;
        private static DB   dataBase                        = null;
        private static Chamber.PowerSupplay powerSupply     = null;
        private static GUI.MainForm mainForm                = null;
        private static Source.Driver.HPT1000 hpt1000        = null;
        private static Types.GasProcesMode gasProcessMode = Types.GasProcesMode.Unknown;
      
        //---------------------------------------------------------------------------------
        public static Types.GasProcesMode GasProcessMode
        {
            set { gasProcessMode = value; }
            get { return gasProcessMode; }
        }
        //---------------------------------------------------------------------------------
        public static Source.Driver.HPT1000 Hpt1000
        {
            set { hpt1000 = value; }
            get { return hpt1000; }
        }
        //---------------------------------------------------------------------------------
        //Ustaw referencje na obiekt bazey danych
        public static DB DataBase
        {
            set { dataBase = value; }
            get { return dataBase; }
        }
        //---------------------------------------------------------------------------------
        //Ustaw referecje na obiekt PLC
        public static PLC PLC_
        {
            set { plc = value; }
        }
        //---------------------------------------------------------------------------------
        public static Chamber.PowerSupplay PowerSupply_
        {
            set { powerSupply = value; }
        }
        public static GUI.MainForm MainForm_
        {
            set { mainForm = value; }
        }
        //---------------------------------------------------------------------------------
        //Utworz prgoram i ustaw odpowienie wymagane dla niego referencje
        public static Program.Program CreateProgram(uint programID)
        {
            Program.Program program = new Program.Program(mainForm);

            program.SetID(programID);
            program.SetPtrPLC(plc);
            program.DataBase = dataBase;
            
            return program;
        }
        //---------------------------------------------------------------------------------
        //Utworz obiekt programu plazmy i przypisz do niego zasilacz
        public static Program.PlasmaProces CreatePlasmaProces()
        {
            Program.PlasmaProces plasmaProcess = new Program.PlasmaProces(powerSupply);

            return plasmaProcess;
        }
        //---------------------------------------------------------------------------------

    }
}
