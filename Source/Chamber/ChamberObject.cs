using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa abstrakcyjna definiujaca zachowanie jakie powinny posiadać komponenty komory
    /// </summary>

    public abstract class ChamberObject:  Device
    {
        protected PLC               plc         = null;                     ///< Referencja na obiekt wykorzystywany do komunikacji z PLC
        protected Types.ControlMode controlMode = Types.ControlMode.None; ///< Zmienna przechowuje info na temat rodzaju sterowania komora {Auto/Manual}

        //---------------------------------------------------------------------------------------
        /**
         * Metoda aktualizuje dane komponentow  komory odczytane z PLC zgodnie z przypisana tablica pamiecie
         */
        public virtual void UpdateData(int[] aData)
        {
            switch (Driver.HPT1000.Mode)
            {
                case Types.Mode.Automatic:
                    controlMode = Types.ControlMode.Automatic;
                    break;
                case Types.Mode.Manual:
                    controlMode = Types.ControlMode.Manual;
                    break;
                default:
                    controlMode = Types.ControlMode.None;
                    break;
            }
        }
        //---------------------------------------------------------------------------------------
        /*
         * Metoda odczytuje ustawienia kolejnych urzadzen PLC  zgodnie z przypisana tablica pamiecie
         */
        public virtual void UpdateSettingsData(int[] aData)
        {}
        //---------------------------------------------------------------------------------------
        /*
         * Metoda odczytuje ustawienia kolejnych urzadzen PLC  zgodnie z przypisana tablica pamiecie. Poniewaz program sie rozrosl tak ze braklo miejsca w pierwotnej lokalizacji na dodatkowe parametry daletgo rozbudoano go nowa lokalizacje
         */
        public virtual void UpdateExtraSettingsData(int[] aData)
        { }
        //---------------------------------------------------------------------------------------
        /**
         * Metoda ustawia referenje na obiekt PLC
         */ 
        public virtual void SetPonterPLC(PLC ptrPLC)
        {
            plc = ptrPLC;
        }
        //---------------------------------------------------------------------------------------
        /**
         * Metoda zwraca typ sterowania komora
         */
        public Types.ControlMode GetControlMode()
        {
            return controlMode;
        }
        //---------------------------------------------------------------------------------------
    }
}
