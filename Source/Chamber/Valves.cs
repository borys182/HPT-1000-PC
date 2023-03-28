using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje zawor komory. Umożliwia sterowanie zaworem i prezentuje jego stan 
    /// </summary>
    public class Valve : ChamberObject
    {
        private List<Valve>         valves              = new List<Valve>();    ///< Lista wszystkich zaworow dostepmna w komorze
        private string              name                = "No name";            ///< Okreslenie nazwy zaworu
        private int                 id                  = 0;                    ///< Parametr wiaze dany zawor z zaworem po stronie PLC 
        private Types.StateValve    state               = Types.StateValve.Error; ///< Stan zaworu {Open/Close/Error}
      
        //-----------------------------------------------------------------------------------------
        private Valve(int ID , string Name)
        {
            name = Name;
            id = ID;
        }
        //-----------------------------------------------------------------------------------------
        public Valve()
        {
            CreateValves();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkacja ma za zadanie utworzenie listy zaworow dostepnych w systemie
         */
        private void CreateValves()
        {
            string []ValvesType = Enum.GetNames(typeof(Types.TypeValve));

            for (int i = 0; i < ValvesType.Length; i++)
            {
                int ID = (int)Enum.GetValues(typeof(Types.TypeValve)).GetValue(i);
                string  Name = ValvesType[i];
                valves.Add(new Valve(ID, Name));
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie aktualizacje stanow zaworow odczytane z PLC
         */
        override public void UpdateData(int []aData)
        {
            if (aData.Length > Types.OFFSET_STATE_VALVES + 1)
            {
                foreach (Valve valve in valves)//ustaw stany zaworow dla wszystkich zaworow zawartych w systemie
                {
                    int aValvesState = (aData[Types.OFFSET_STATE_VALVES + 1] << 16) | aData[Types.OFFSET_STATE_VALVES];

                    int aShiftBits = valve.id * 2; // o ile musze przesunac bity slowa aby uzyskac dane interesujacego mnie zaworu
                    if (valve.id == 0) aShiftBits = 0;
                    int aMask = 0x03 << aShiftBits;                          //maska bitowa wyodrebniajace stan danego zaworu
                    int aState = (aValvesState & aMask) >> (int)aShiftBits;

                    if (Enum.IsDefined(typeof(Types.StateValve), aState))
                        valve.state = (Types.StateValve)Enum.Parse(typeof(Types.StateValve), (aState).ToString()); // konwertuj int na Enum
                    else
                        valve.state = Types.StateValve.Error;
                }
            }
            base.UpdateData(aData);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Zwroc stan danego zaworu
         */
        public Types.StateValve GetState(Types.TypeValve aTypeValve)
        {
            Types.StateValve aState = Types.StateValve.Error;
            foreach(Valve valve in valves)
            {
                if (valve.id == (int)aTypeValve)
                {
                    aState = valve.state;
                    break;
                }
            }
            return aState;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie ustawienie stanu zaworu
         */ 
        public ItemLogger SetState(Types.StateValve aState, Types.TypeValve aTypeValve)
        {
            ItemLogger aErr = new ItemLogger();
            int []ctrlValve = {0};

            //ustaw na odpowidnim miejscu bity sterujace zgodnie z ID zaworu powiazanego z PLC
            int aShiftBits = (int)aTypeValve * 2; // o ile musze przesunac bity slowa aby uzyskac dane interesujacego mnie zaworu
            
            if (aState == Types.StateValve.Open)
                ctrlValve[0] = 0x02 << aShiftBits;                          
            if(aState == Types.StateValve.Close)
                ctrlValve[0] = 0x01 << aShiftBits;

            if (aState == Types.StateValve.Close || aState == Types.StateValve.Open)
            {
                if (plc != null)
                {
                    if (controlMode == Types.ControlMode.Manual)
                    {
                        int aCode = plc.WriteWords(Types.ADDR_VALVES_CTRL, 2, ctrlValve);
                        aErr.SetErrorMXComponents(Types.EventType.SET_STATE_VALVE, aCode);
                    }
                    else
                        aErr.SetErrorApp(Types.EventType.SET_STATE_VALVE);
                }
                else
                    aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
            }
            else
                aErr.SetErrorApp(Types.EventType.CALL_INCORRECT_OPERATION);


            return aErr;
        }
        //-----------------------------------------------------------------------------------------
    }
}
