using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
using System.Windows.Forms;

namespace HPT1000.Source.Chamber
{
    /// <summary>
    /// Klasa reprezentuje konserwację komory. Przechowuje informacje na temat tego kiedy powinna odbyc się następna kiedy była poprzednia oraz komunikuje sie z PLC po to aby
    /// odczytac liczbe godzin pracy pompy wstepnej, przeprowadzic leak test
    /// </summary>
    public class Maintenance : ChamberObject
    {
        //---------Lista obiektow wymagnacyh do poprawnego dzialania obiektu konserwacji------
        DB      db  = null; ///< Referencja bazy danych. Wymagane do zapisu odczytu parametrow
  
        //---------Lista parametrow obiektu Maintanece przechowywanych w bazie danych------
        private DateTime    dateLastMaintenance     = DateTime.Now ;               ///< Data ostatniej konserwacji
        private DateTime    dateNextMaintenance     = DateTime.Now;                ///< Data następnej konserwacji ale to nie jest data globalna (Data nastepnej globalnej konserwacji zalezy od ustawien)
        private int         intervalMonth           = 12;       ///< Liczba miesiecy co jaki czas powinna odbywac sie konserwacja
        private int         hourOilChange           = 500;      ///< Zmienna okresla liczbe godzin pracy pompy wstepnej po ktorej powinna sie pojawic komunikato wymianie oleju
        private Types.TimeMaintenance typeTimeMaintenance = Types.TimeMaintenance.Interval; ///< Okreslenie rodzaju czasu na podstawie ktorego powinine byc wyznaczony nastpan data konserwacji
        private double      chamberVolume           = 30;       ///< Pojemosc komory. Na jej podstawie jest obvliczny ewentualny naciek
        private int         processNumber           = 0;        ///< Liczba wykonanych procesow przez maszyne - wartosc odczytana z PLC
        private int         timeOperatingMachine    = 0;        ///< Czas pracu maszyny przechowaywany PLC i wyrazony w h
        private int         timeWorkFP              = 0;        ///< Czas pracy pompy wstepnej wyrazony w h

        private int         actualTimeLeakTest      = 0;        ///< Czas pracy procedury leak test od momentu osiagniecia setpointa   
        private Types.StateLeaktest  stateLeakTest  = Types.StateLeaktest.None;    ///< Zmienna informauje czy jest wykonywana procedura leaktestu
        private double      leakValue               = 0;        ///< Zmienna określa poziom nacieku w komorze. Jest obliczana po zakonczeniu procedury leakTestu
        private double      setpoint                = 0;        ///< Wartosc setpoint do ktorego nalezy odpompowac komore podczas procedury
        private int         timeDuration            = 0;        ///< Czas po jakim od odpompowania komory sprawdzam naciek

        string              paraName;                      ///< Zmienna jest wykorzystywana do zbiorczego zapisu/odczytu parametrow MFC so/z bazy danych

        //-----SETERY/GETERY---------------------------------------------------------------------------------------------------------
        public double Setpoint
        {
            get { return setpoint; }
        }//---------------------------------------------------------------------------------------------------------------------------
        public double LeakValue
        {
            get { return leakValue; }
        }//---------------------------------------------------------------------------------------------------------------------------
         public int TimeWorkFP
        {
            get { return timeWorkFP; }
        } //---------------------------------------------------------------------------------------------------------------------------
        public int TimeOperatingMachine
        {
            get { return timeOperatingMachine; }
        }//---------------------------------------------------------------------------------------------------------------------------
        public int ProcessNumber
        {
            get { return processNumber; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public double ChamberVolume
        {
            set { chamberVolume = value; SaveData(); }
            get { return chamberVolume; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public Types.StateLeaktest StateLeakTest
        {
            get { return stateLeakTest; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public int ActualTimeLeakTestDuration
        {
            get { return actualTimeLeakTest; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public DB DataBase
        {
            set
            {
                db = value;
                //Odczytaj wartoscvi zapisanych parametrow w bazie danych dla konserwacji
                LoadData();
            }
        }//--------------------------------------------------------------------------------------------------------------------------
        public DateTime DateLastMaintenance
        {
            get { return dateLastMaintenance; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public DateTime DateNextMaintenance
        {
            set
            {
                dateNextMaintenance = value;
                SaveData();
            }
            get { return dateNextMaintenance; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public DateTime DateNextGlobalMaintenance
        {
            set { dateNextMaintenance = value; }
            get
            {
                DateTime aNextTime = dateNextMaintenance; 
                if (typeTimeMaintenance == Types.TimeMaintenance.Interval)
                    aNextTime = dateLastMaintenance.AddMonths(IntervalMonth);
             
                return aNextTime;
            }
        }//--------------------------------------------------------------------------------------------------------------------------
        public int IntervalMonth
        {
            set { intervalMonth = value; SaveData(); }
            get { return intervalMonth; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public int HourOilChange
        {
            set { hourOilChange = value; SaveData(); }
            get { return hourOilChange; }
        }//--------------------------------------------------------------------------------------------------------------------------
        public Types.TimeMaintenance TypeTimeMaintenance
        {
            set { typeTimeMaintenance = value; SaveData(); }
            get { return typeTimeMaintenance; }
        }//--------------------------------------------------------------------------------------------------------------------------------------------
        public int TimeDuration
        {
            get { return timeDuration; }
        }    
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * KOnstruktor klasy
         */
        public Maintenance()
        {
            //Ustaw mazwe pod jaka parametry beda zapisane w bazie danych
            paraName = "Maintenance_Parameter";
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
                            if (para.Contains("DateLastMaintenance"))
                                dateLastMaintenance = Convert.ToDateTime(para.Split('=')[1]);
                            if (para.Contains("DateNextMaintenance"))
                                dateNextMaintenance = Convert.ToDateTime(para.Split('=')[1]);
                            if (para.Contains("IntervalMonth"))
                                intervalMonth = Convert.ToInt32(para.Split('=')[1]);
                            if (para.Contains("HourOilChange"))
                                hourOilChange = Convert.ToInt32(para.Split('=')[1]);
                            if (para.Contains("TypeTimeMaintenance"))
                                typeTimeMaintenance = (Types.TimeMaintenance)Enum.Parse(typeof(Types.TimeMaintenance),para.Split('=')[1]);
                            if (para.Contains("ChamberVolume"))
                                chamberVolume = Convert.ToDouble(para.Split('=')[1]);
                            if (para.Contains("Setpoint"))
                                setpoint = Convert.ToDouble(para.Split('=')[1]);
                            if (para.Contains("TimeDuration"))
                                timeDuration = Convert.ToInt32(para.Split('=')[1]);
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
            parameter.Data = "DateLastMaintenance = " + dateLastMaintenance.ToString()  + ";";
            parameter.Data += "DateNextMaintenance = " + dateNextMaintenance.ToString()  + ";";
            parameter.Data += "IntervalMonth = "       + intervalMonth.ToString()        + ";";
            parameter.Data += "HourOilChange = "       + hourOilChange.ToString()        + ";";
            parameter.Data += "TypeTimeMaintenance = " + typeTimeMaintenance.ToString()  + ";";
            parameter.Data += "ChamberVolume = "       + chamberVolume.ToString()        + ";";
            parameter.Data += "Setpoint = "            + setpoint.ToString()             + ";";
            parameter.Data += "TimeDuration = "        + timeDuration.ToString()         + ";";

            int aRes = db.SaveParameter(parameter);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda odswieza dane na temat leak testu odczytane z PLC
         */
        override public void UpdateData(int[] aData)
        {
            Types.StateLeaktest    aLeakTestState = Types.StateLeaktest.None;
            double  aCurentPressure = 0;
            if (aData.Length > Types.OFFSET_ACTUAL_TIME_LEAKTEST && aData.Length > Types.OFFSET_STATE_LEAKTEST && aData.Length > Types.OFFSET_FP_TIME_WORK &&
                aData.Length > Types.OFFSET_NUMBER_PROCESS && aData.Length       > Types.OFFSET_OPERATING_HOUR)
            {
                actualTimeLeakTest   = aData[Types.OFFSET_ACTUAL_TIME_LEAKTEST];
                aLeakTestState       = (Types.StateLeaktest)Enum.Parse(typeof(Types.StateLeaktest), aData[Types.OFFSET_STATE_LEAKTEST].ToString());
                timeWorkFP = aData[Types.OFFSET_FP_TIME_WORK];
                processNumber        = aData[Types.OFFSET_NUMBER_PROCESS];
                timeOperatingMachine = aData[Types.OFFSET_OPERATING_HOUR];
                aCurentPressure      = Types.ConvertDWORDToDouble(aData, Types.OFFSET_PRESSURE);

                //Jezeli leak test zostal zakonczony to oblicz wartosc nacieku
                if ((stateLeakTest == Types.StateLeaktest.Run_MesureLeak || stateLeakTest == Types.StateLeaktest.Run_PumpDown) && aLeakTestState == Types.StateLeaktest.Stop  && timeDuration != 0)
                    leakValue = (aCurentPressure - setpoint) * chamberVolume / timeDuration;

                //aktualizuj status procedury leaktest
                stateLeakTest = aLeakTestState;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda potwierdza wykonanie konserwacji
         */
         public void SetMaintenanceMade()
        {
            dateLastMaintenance = DateTime.Now;

            DialogResult result = MessageBox.Show("Are you sure want to set maintenance state as done", "Confirm maintenance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Wyzeruj parametry w PLC
                ItemLogger aErr = new ItemLogger();
                //Wyzetuj licznik pracy pompy wstepnej w PLC
                if (plc != null)
                {
                    int aExtCode = plc.SetDevice(Types.ADDR_CLEAR_TIME_MACHINE, 1);
                    aErr.SetErrorMXComponents(Types.EventType.MAINTANANCE, aExtCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

                SaveData();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda potwierdza wykonanie konserwacji pompy wstepnej
         */
        public void ClearHourWorkFP()
        {
            DialogResult result = MessageBox.Show("Are you sure want to set oil change state as done", "Confirm oil change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                ItemLogger aErr = new ItemLogger();
                //Wyzetuj licznik pracy pompy wstepnej w PLC
                if (plc != null)
                {
                    int[] aData = new int[1];
                    aData[0] = 0;
                    int aExtCode = plc.SetDevice(Types.ADDR_CLEAR_TIME_FP, 1);
                    aErr.SetErrorMXComponents(Types.EventType.CLEAR_FP_TIME, aExtCode);
                }
                else
                    aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia setpoint prozni do ktorej jest pompowana komora podczas leak testu
         */
        public bool SetSetpoint(double aSetpoint)
        {
            bool ARes = false;
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                //Ustaw setpointa do ktorego nalezy odpompowiac komore
                int aExtCode = plc.WriteRealData(Types.ADDR_SETPOINT_LEAKTEST, (float)aSetpoint);
                aErr.SetErrorMXComponents(Types.EventType.SET_LEAKTEST_PARA, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            //Jezeli udalo sie ustawic parametry w PLC to aktualizuj wartosci w obiekicie
            if (!aErr.IsError())
            {
                setpoint = aSetpoint;
                SaveData();
                ARes = true;
            }
            return ARes;
         }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia czas procesu pompowania podczas leaktestu
         */
        public void SetTimeLeakTest(DateTime aTimeLeakTest)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aDate = new int[1];

            if (plc != null)
            {
                aDate[0] = aTimeLeakTest.Hour * 3600 + aTimeLeakTest.Minute * 60 + aTimeLeakTest.Second;
                //Ustaw setpointa do ktorego nalezy odpompowiac komore
                int aExtCode = plc.WriteWords(Types.ADDR_TIME_DURATION_LEAKTEST, 1,aDate);
                aErr.SetErrorMXComponents(Types.EventType.SET_LEAKTEST_PARA, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            //Jezeli udalo sie ustawic parametry w PLC to aktualizuj wartosci w obiekicie
            if (!aErr.IsError())
            {
                timeDuration = aDate[0];
                SaveData();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca informacje czy jest wymagane przeprowadzenie konserwacji
         */
        public bool IsMaintanceRequired()
        {
            bool aMaintanceRequired = false;

            if (((typeTimeMaintenance == Types.TimeMaintenance.Interval && DateTime.Now > dateLastMaintenance.AddMonths(IntervalMonth)) ||
                (typeTimeMaintenance == Types.TimeMaintenance.Time     && DateTime.Now > dateNextMaintenance)) && (processNumber > 0 || timeOperatingMachine > 0))
                aMaintanceRequired = true;

            return aMaintanceRequired;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca informacje czy jest konieczna wymina oleju pompy wstepnej
         */
        public bool IsOilChange()
        {
            bool aOilChange = false;

            if (hourOilChange < timeWorkFP)
                aOilChange = true;

            return aOilChange;
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie przeprowadzenie procesu wykrywania wycieku w komorze
         */ 
         public void StartLeakProcess(double aSetpoint, int aTime)
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                int[] aData = new int[1];
                aData[0] = aTime;
                //Ustaw setpointa do ktorego nalezy odpompowiac komore
                plc.WriteRealData(Types.ADDR_SETPOINT_LEAKTEST, (float)aSetpoint);
                //Ustaw czas jaki nalezy odczekac po wylaczeniu pompowania
                plc.WriteWords(Types.ADDR_TIME_DURATION_LEAKTEST,1,aData);
                //Rozpocznij procedure leka testu
                int aExtCode = plc.SetDevice(Types.ADDR_LEAK_TEST, 1);

                aErr.SetErrorMXComponents(Types.EventType.START_LEAKTEST, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            //Jezeli udalo sie ustawic parametry w PLC to aktualizuj wartosci w obiekicie
            if (!aErr.IsError())
            {
                timeDuration = aTime;
                setpoint = aSetpoint;
                SaveData();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie zatrzymanie leaktestu
        */
        public void StopLeakProcess()
        {
            ItemLogger aErr = new ItemLogger();

            if (plc != null)
            {
                //Zatrzymaj procedure leka testu
                int aExtCode = plc.SetDevice(Types.ADDR_LEAK_TEST, 0);
                aErr.SetErrorMXComponents(Types.EventType.STOP_LEAKTEST, aExtCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
    }
}
