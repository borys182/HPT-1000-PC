using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje silnik sterujacy bebnem komory. Jest odpowiedzialana za przedstawianie jego stanu oraz sterowanie praca poprzez wlacz/wylacz
    /// </summary>
    public class MotorDriver : ChamberObject
    {
        DB                      db      = null;

        private Types.StateFP   state   = Types.StateFP.Error;  ///< Stan motora {ON/OFF/Error}
        private int             id      = 0;                    ///< Okreslenie ID motora ktroy jest bezposrednio powiazany z motorem po stronie PLC. W PLC sterujemy dwoma motrami
        private static bool     motor1Enable        = false; ///< Flaga okresla czy dany motor jest dostepny w danej konfiguracji sprtetowej komory
        private static bool     motor2Enable        = false; ///< Flaga okresla czy dany motor jest dostepny w danej konfiguracji sprtetowej komory

        private bool            oldMotor_1_Enable   = false;
        private bool            oldMotor_2_Enable   = false;

        private string          paraName;                      ///< Zmienna jest wykorzystywana do zbiorczego zapisu/odczytu parametrow MFC so/z bazy danych

        private Value           motorValue          = new Value();
 
        //-------------------------------------------------------------------------------------
        public DB DataBase
        {
            set
            {
                db = value;
                //Odczytaj wartoscvi zapisanych parametrow w bazie danych dla konserwacji
                LoadData();
            }
        }
        //-----------------------------------------------------------------------------------------
        public Types.StateFP State
        {
            get { return state; }
        }
        //-----------------------------------------------------------------------------------------
        public int ID
        {
            get { return id; }
        }
        //-----------------------------------------------------------------------------------------
        public static bool Motor_1_Enable
        {
            set { motor1Enable = value;}
            get { return motor1Enable; }
        }
        //-----------------------------------------------------------------------------------------
        public static bool Motor_2_Enable
        {
            set { motor2Enable = value; }
            get { return motor2Enable; }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Konstruktor - ustaw na etpaie tworzenia obiekty realizacji z PLC poprzez ID
         */
        public MotorDriver(int aID)
        {
            id = aID;
          
            //Ustaw mazwe pod jaka parametry beda zapisane w bazie danych
            paraName = "Motor_Parameter";

            //Uzupelnij liste parametrow ktore powinny byc zapisywane w bazi danych
            AddParameter("Motor " + id.ToString() + " state", motorValue, "");
            //Ustaw nazwe urzadzenia - pamietaj ze musi ona byc unikalna dla calego systemu
            name = "Motor " + id.ToString();

            acqData = true; //Ustawiam flage ze urzadzenie jest przenzaczone do arachiwzowania danych w nbazie danych

        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie odczytanie aktualnego stanu motora ze sterownika PLC
         */
        override public void UpdateData(int[] aData)
        {
            if (aData.Length > Types.OFFSET_STATE_MOTOR)
            {
                int aStateWord = aData[Types.OFFSET_STATE_MOTOR];
                //W slowie mamy 4 bitowe statusy dla 4 motorow. Wyodrebnij status danego motora. Kolejnme statusy sa zapisywane na kolejnych tetradach
                int aState = (aStateWord >> 4 * (id - 1)) & 0xF;
                if (Enum.IsDefined(typeof(Types.StateFP), aState))
                {
                    state = (Types.StateFP)Enum.Parse(typeof(Types.StateFP), aState.ToString());
                    //Aktualizuj dane zapisywane do bazy danych
                    motorValue.Value_ = (int)state - 1;//Poniewaz 0 trkatuje jako OFF dlatego odejmuje od odycznej wartosci 1 poniewaz tam OFF jest traktowane jako 1
                }
                else
                    state = Types.StateFP.Error;
            }
            base.UpdateData(aData);

            //Zapisz parametry do bazy jezeli ulegly zmianie
            if (oldMotor_1_Enable != motor1Enable || oldMotor_2_Enable != motor2Enable)
                SaveData();

            oldMotor_1_Enable = motor1Enable;
            oldMotor_2_Enable = motor2Enable;
        }
        //--------------------------------------------------------------------------------------------------------
        /**
        * Funkcja akutalizuje parametry motora z PLC 
         */
        public override void UpdateSettingsData(int[] aData)
        {
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja odczytuje z bazy danych zapisane wczesniej parametry
         */
        private void LoadData()
        {
            if (db != null)
            {
                ProgramParameter parameter = new ProgramParameter();
                parameter.Name = paraName;
                db.LoadParameter(parameter.Name, out parameter);
                if (parameter.Data != null)
                {
                    //Kolejne parametry sa oddzieolne ; Wyodrebnij je.
                    string[] parameters = parameter.Data.Split(';');
                    foreach (string para in parameters)
                    {
                        try
                        {
                            //Wyszukuj kolejnych nazw i odczytaj wartosc ktora jest zapisana po =
                            if (para.Contains("Motor_1_Enable"))
                                motor1Enable = Convert.ToBoolean(para.Split('=')[1]);
                            if (para.Contains("Motor_2_Enable"))
                                motor2Enable = Convert.ToBoolean(para.Split('=')[1]);
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
            parameter.Data  = "Motor_1_Enable = " + motor1Enable.ToString() + ";";
            parameter.Data += "Motor_2_Enable = " + motor2Enable.ToString() + ";";
       
            if(db != null)
                db.SaveParameter(parameter);
        }

        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja umozliwia wlaczenie/wylaczenie pompy
         */
        public ItemLogger ControlMotor(Types.StateFP state)
        {
            ItemLogger aErr = new ItemLogger();

            int[] aData = { (int)state };

            if (plc != null)
            {
                string aAddr = "";
             
                if (controlMode == Types.ControlMode.Automatic)
                {
                    if (id == 1) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_MOTOR_1_CMD).ToString();
                    if (id == 2) aAddr = "D" + (Types.ADDR_START_CRT_PROGRAM + Types.OFFSET_SEQ_MOTOR_2_CMD).ToString();
                }
                if (controlMode == Types.ControlMode.Manual)
                {
                    if (id == 1) aAddr = Types.ADDR_MOTOR_1_DRIVER;
                    if (id == 2) aAddr = Types.ADDR_MOTOR_2_DRIVER;
                }

                if (aAddr.Length > 0)
                {                    
                    int aCode = plc.WriteWords(aAddr, 1, aData);
                    aErr.SetErrorMXComponents(Types.EventType.MOTOR_CONTROL, aCode);
                }
                else if (controlMode == Types.ControlMode.Automatic || controlMode == Types.ControlMode.Manual)
                    aErr.SetErrorApp(Types.EventType.MOTOR_CONTROL);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
    }
}
