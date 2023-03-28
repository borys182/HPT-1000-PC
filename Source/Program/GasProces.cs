using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;

namespace HPT1000.Source.Program
{
    /// <summary>
    ///
    ///    Klasa zawiera dane potrzebne do sterowania procesem gazow w komorze. Gazami mozemy sterowac na 3 sposoby
    ///    1 - utrzymywahie zadanej prozni przez przeplywki
    ///    2 - utrzymywanie zadanej prozni przez vaporatior
    ///    3 - sterowanie przeplywem zgodnie z ustalonymi przeplywami
    /// </summary>
    [Serializable]
    public class GasProces : ProcesObject
    {
        //Struktura zawiera informacje na temat jednej przeplywki
        [Serializable]
        public struct FlowMeter
        {
            enum Index { CMD = 0, FLOW = 1, MIN_FLOW = 2, MAX_FLOW = 3, SHARE = 4, DEVIATION = 5, FACTOR = 6};

            [NonSerialized]
            private int     id;             //ID jednoznaczie okresla przeplywek oraz przypisuje jej odpowiednie adresy po stronie PLC (id moze byc 1,2 lub 3)
            public bool     Active;
            public int      GasFlow;        //wartosc w procenatch na jaka nalezy otworzyc dany zawor
            public int      MinGasFlow;     //min przeplyw gazu - po jego przekroczeniu zglaszaj blad
            public int      MaxGasFlow;     //max przeplyw gazu - po jego przekroczeniu zglaszaj blad
            public int      ShareGas;       //zmienna okresla udzial daneg gazu w kontrli prozni podczas uzywania gazów do kontrli zadanej prozni z udzialem regulatora PID
            public int      MaxDeviation;   //max odchylenie od zadanej wartosci udzialu danego gazu w procesie utrzymywania zadanej prozni w komorze
            public int      GasLimitDown;   //min przeplyw jaki moze byc uzyskany przez przeplywke
            public int      GasLimitUp;     //max przeplyw jaki moze byc uzyskany przez przeplywke
            public Types.GasProcesMode Mode ;
            public int      IDTypeGas;
            [NonSerialized]
            private int[,]  addresses;

            //wartosci tymczasowe
            [NonSerialized]
            public bool ActiveTmp;
            [NonSerialized]
            public int GasFlowTmp;        //wartosc w procenatch na jaka nalezy otworzyc dany zawor
            [NonSerialized]
            public int MinGasFlowTmp;     //min przeplyw gazu - po jego przekroczeniu zglaszaj blad
            [NonSerialized]
            public int MaxGasFlowTmp;     //max przeplyw gazu - po jego przekroczeniu zglaszaj blad
            [NonSerialized]
            public int ShareGasTmp;       //zmienna okresla udzial daneg gazu w kontrli prozni podczas uzywania gazów do kontrli zadanej prozni z udzialem regulatora PID
            [NonSerialized]
            public int MaxDeviationTmp;   //max odchylenie od zadanej wartosci udzialu danego gazu w procesie utrzymywania zadanej prozni w komorze
            [NonSerialized]
            public int IDTypeGasTmp;
            [NonSerialized]
            public Types.GasProcesMode ModeTmp;

            //---------------------------------------------------------------------------------------------------------------
            //Konstrukto inicjalizujcy wartosci
            public FlowMeter(int aId)
            {
                id              = aId;
                Active          = false;
                GasFlow         = 0;
                MinGasFlow      = 10;
                MaxGasFlow      = 300;
                ShareGas        = 0;
                MaxDeviation    = 0;
                GasLimitDown    = 0;
                GasLimitUp      = 2000;
                Mode = Types.GasProcesMode.FlowSP;
                IDTypeGas       = 0;

                ActiveTmp       = false;
                GasFlowTmp      = 0;
                MinGasFlowTmp   = 10;
                MaxGasFlowTmp   = 300;
                ShareGasTmp     = 0;
                MaxDeviationTmp = 0;
                IDTypeGasTmp    = 0;
                ModeTmp         = Types.GasProcesMode.Unknown;

                addresses = new int[3,7];
                InitAddresses(); 
            }
            //---------------------------------------------------------------------------------------------------------------
            /**
            * Metoda ma za zdanie aktualizjace parametrow subprogramup sterowania gazami odczytanych ze sterownika PLC
            */
            public void UpdateData(SubprogramData aSubprogramData)
            {
                if (aSubprogramData.tabMFC.Length > id)
                {
                    Active       = aSubprogramData.tabMFC[id].Active;
                    GasFlow      = aSubprogramData.tabMFC[id].Flow;
                    MinGasFlow   = aSubprogramData.tabMFC[id].MinFlow;
                    MaxGasFlow   = aSubprogramData.tabMFC[id].MaxFlow;
                    ShareGas     = aSubprogramData.tabMFC[id].ShareGas;
                    MaxDeviation = aSubprogramData.tabMFC[id].Devaition;
                }
            }
            //---------------------------------------------------------------------------------------------------------------
            /**
            * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu sterowania gazami 
            */
            public void PrepareData(int[] aData)
            {
                int aPow = 0;
                if (id == 0) aPow = Types.BIT_CMD_FLOW_1;
                if (id == 1) aPow = Types.BIT_CMD_FLOW_2;
                if (id == 2) aPow = Types.BIT_CMD_FLOW_3;
              
                //Jezeli dany obiekt jest niedostepny fizycznie to nie mozna dla niego tworzyc programu dlatego Active ustaw wtedu na false
                if (Factory.Hpt1000  != null && Factory.Hpt1000.GetMFC() != null)
                    Active &= Factory.Hpt1000.GetMFC().GetActive(id + 1);

                if (id < (addresses.Length / 6) && Active)
                {
                    if (Mode == Types.GasProcesMode.FlowSP)
                    {
                        aData[addresses[id, (int)Index.CMD]]     |= (int)System.Math.Pow(2, aPow);
                        aData[addresses[id, (int)Index.FLOW]]     = GasFlow;
                        aData[addresses[id, (int)Index.MIN_FLOW]] = MinGasFlow;
                        aData[addresses[id, (int)Index.MAX_FLOW]] = MaxGasFlow;
                    }
                    else
                    {
                        aData[addresses[id, (int)Index.CMD]]       |= (int)System.Math.Pow(2, aPow);                  //Ustaw czy nalezy sterowac vaporasorem w trybie sterowania cisnieniem
                        aData[addresses[id, (int)Index.SHARE]]      = ShareGas;
                        aData[addresses[id, (int)Index.DEVIATION]]  = MaxDeviation;
                    }
                    double aFactor = 1;
                    if(Factory.Hpt1000 != null && Factory.Hpt1000.GetGasTypes() != null)
                        aFactor = Factory.Hpt1000.GetGasTypes().GetFactor(IDTypeGas);
                    aData[addresses[id, (int)Index.FACTOR]]     = Types.ConvertDOUBLEToWORD(aFactor, Types.Word.LOW);
                    aData[addresses[id, (int)Index.FACTOR] + 1] = Types.ConvertDOUBLEToWORD(aFactor, Types.Word.HIGH);
    
                }
            }
            //---------------------------------------------------------------------------------------------------------------------
            /**
             * Metoda ma za zadanie ustawienie wartoscu tymczasowych parametrow jako wartosci rzeczywiste/akualne
            */
            public void SetEditableParameters(bool changesStore)
            {
                if (changesStore)
                {
                    Active          = ActiveTmp;
                    GasFlow         = GasFlowTmp;        //wartosc w procenatch na jaka nalezy otworzyc dany zawor
                    MinGasFlow      = MinGasFlowTmp;     //min przeplyw gazu - po jego przekroczeniu zglaszaj blad
                    MaxGasFlow      = MaxGasFlowTmp;     //max przeplyw gazu - po jego przekroczeniu zglaszaj blad
                    ShareGas        = ShareGasTmp;       //zmienna okresla udzial daneg gazu w kontrli prozni podczas uzywania gazów do kontrli zadanej prozni z udzialem regulatora PID
                    MaxDeviation    = MaxDeviationTmp;   //max odchylenie od zadanej wartosci udzialu danego gazu w procesie utrzymywania zadanej prozni w komorze
                    IDTypeGas       = IDTypeGasTmp;
                    Mode            = ModeTmp;                 
                }
                else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
                {
                    ActiveTmp       = Active;
                    GasFlowTmp      = GasFlow;        //wartosc w procenatch na jaka nalezy otworzyc dany zawor
                    MinGasFlowTmp   = MinGasFlow;     //min przeplyw gazu - po jego przekroczeniu zglaszaj blad
                    MaxGasFlowTmp   = MaxGasFlow;     //max przeplyw gazu - po jego przekroczeniu zglaszaj blad
                    ShareGasTmp     = ShareGas;       //zmienna okresla udzial daneg gazu w kontrli prozni podczas uzywania gazów do kontrli zadanej prozni z udzialem regulatora PID
                    MaxDeviationTmp = MaxDeviation;   //max odchylenie od zadanej wartosci udzialu danego gazu w procesie utrzymywania zadanej prozni w komorze
                    IDTypeGasTmp    = IDTypeGas;
                    ModeTmp         = Mode;
                }
            }
            //---------------------------------------------------------------------------------------------------------------
            /**
             * Metoda ustawia adresy parametrow kolejnych przeplywek jakie zostaly im przyporzadkowen po stronie PLC 
             */
            private void InitAddresses()
            {
                addresses[0,(int)Index.CMD]        = Types.OFFSET_SEQ_CMD;
                addresses[0,(int)Index.FLOW]       = Types.OFFSET_SEQ_FLOW_1_FLOW;
                addresses[0,(int)Index.MIN_FLOW]   = Types.OFFSET_SEQ_FLOW_1_MIN_FLOW;
                addresses[0,(int)Index.MAX_FLOW]   = Types.OFFSET_SEQ_FLOW_1_MAX_FLOW;
                addresses[0,(int)Index.SHARE]      = Types.OFFSET_SEQ_FLOW_1_SHARE;
                addresses[0,(int)Index.DEVIATION]  = Types.OFFSET_SEQ_FLOW_1_DEVIATION;
                addresses[0, (int)Index.FACTOR]    = Types.OFFSET_SEQ_FLOW_1_FACTOR;

                addresses[1,(int)Index.CMD]        = Types.OFFSET_SEQ_CMD;
                addresses[1,(int)Index.FLOW]       = Types.OFFSET_SEQ_FLOW_2_FLOW;
                addresses[1,(int)Index.MIN_FLOW]   = Types.OFFSET_SEQ_FLOW_2_MIN_FLOW;
                addresses[1,(int)Index.MAX_FLOW]   = Types.OFFSET_SEQ_FLOW_2_MAX_FLOW;
                addresses[1,(int)Index.SHARE]      = Types.OFFSET_SEQ_FLOW_2_SHARE;
                addresses[1,(int)Index.DEVIATION]  = Types.OFFSET_SEQ_FLOW_2_DEVIATION;
                addresses[1,(int)Index.FACTOR]     = Types.OFFSET_SEQ_FLOW_2_FACTOR;

                addresses[2,(int)Index.CMD]        = Types.OFFSET_SEQ_CMD;
                addresses[2,(int)Index.FLOW]       = Types.OFFSET_SEQ_FLOW_3_FLOW;
                addresses[2,(int)Index.MIN_FLOW]   = Types.OFFSET_SEQ_FLOW_3_MIN_FLOW;
                addresses[2,(int)Index.MAX_FLOW]   = Types.OFFSET_SEQ_FLOW_3_MAX_FLOW;
                addresses[2,(int)Index.SHARE]      = Types.OFFSET_SEQ_FLOW_3_SHARE;
                addresses[2,(int)Index.DEVIATION]  = Types.OFFSET_SEQ_FLOW_3_DEVIATION;
                addresses[2,(int)Index.FACTOR]     = Types.OFFSET_SEQ_FLOW_3_FACTOR;

            }
        };
        //---------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------
        private Types.GasProcesMode modeGasProces       = Types.GasProcesMode.FlowSP;   //Zmienna przechowuje dane na temat trybu pracy sterowania gazami {przeplyw, PID_MFC lub PID_Vaporaiser}
        private DateTime            timeDurationProces  ; //[s]
        //sterowanie przeplywakami
        private FlowMeter[]         tabFlow             = new FlowMeter[3];     //Tablica przeplwyek
        //sterowanie vaporatorem
        private bool                activeVaporaitor    = false;
        private double              cycleTimeVaporaito  = 0;
        private int                 onTimeVaporaitor    = 0;    //procentowe okreslenie jak dlugo ma byc otwarty zawor vaporatiora podczas cyklu
        private int                 dosing              = 0;

        //utrzymywanie zadanej prozni
        private double setpointPressure = 0;
        private double minDeviationSP   = 0;
        private double maxDeviationSP   = 0;

        [NonSerialized]
        int[]           tabOrderEditGasShareChannel    = new int[3];
        [NonSerialized]
        int curentIndex = 0;   //indeks aktulnie wykonywanej akcji edycji ShareGas (indeks tablicy)

        //Parametry przechowaywane jako tymczasowe
        [NonSerialized]
        private DateTime timeDurationProcesTmp; //[s]
        [NonSerialized]
        private Types.GasProcesMode modeGasProcesTmp = Types.GasProcesMode.FlowSP;   //Zmienna przechowuje dane na temat trybu pracy sterowania gazami {przeplyw, PID_MFC lub PID_Vaporaiser}
        [NonSerialized]
        private bool    activeVaporaitorTmp         = false;
        [NonSerialized]
        private double  cycleTimeVaporaitoTmp        = 0;
        [NonSerialized]
        private int     onTimeVaporaitorTmp          = 0;    //procentowe okreslenie jak dlugo ma byc otwarty zawor vaporatiora podczas cyklu
        [NonSerialized]
        private int     dosingTmp                    = 0;
        [NonSerialized]
        private double  setpointPressureTmp          = 0;
        [NonSerialized]
        private double  minDeviationSPTmp            = 0;
        [NonSerialized]
        private double  maxDeviationSPTmp            = 0;

        //---------------------------------------------------------------------------------------------------------------
        /**
         * Konstrukyot klasy tworzycy przeplywki i nadajacy im poprawne ID 
         */
        public GasProces()
        {
            timeDurationProces = DateTime.Now;
            timeDurationProces = timeDurationProces.AddHours(-DateTime.Now.Hour);
            timeDurationProces = timeDurationProces.AddMinutes(-DateTime.Now.Minute);
            timeDurationProces = timeDurationProces.AddSeconds(-DateTime.Now.Second);

            timeDurationProcesTmp = timeDurationProces;

            tabFlow[0] = new FlowMeter(0);
            tabFlow[1] = new FlowMeter(1);
            tabFlow[2] = new FlowMeter(2);

            tabOrderEditGasShareChannel[0] = -1;
            tabOrderEditGasShareChannel[1] = -1;
            tabOrderEditGasShareChannel[2] = -1;
        }
        //---------------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zdanie aktualizjace parametrow subprogramup kontorli gazow odczytanych ze sterownika PLC
        */
        public override void UpdateData(SubprogramData aSubprogramData)
        {
            timeWorking         = ConvertDate(aSubprogramData.WorkingTimeGas);
            timeDurationProces  = ConvertDate(aSubprogramData.GasProces_TimeTarget);

            modeGasProces = (Types.GasProcesMode)aSubprogramData.GasProces_Mode;

            cycleTimeVaporaito = aSubprogramData.Vaporaitor_CycleTime;
            onTimeVaporaitor   = aSubprogramData.Vaporaitor_Open;
            dosing             = aSubprogramData.Vaporaitor_Dosing;
            setpointPressure   = aSubprogramData.GasProces_Setpoint;
            minDeviationSP     = aSubprogramData.GasProces_MinDiffer;
            maxDeviationSP     = aSubprogramData.GasProces_MaxDiffer;

            activeVaporaitor   = IsBitActive(aSubprogramData.Command, Types.BIT_CMD_FLOW_4);
            
            for (int i = 0; i < tabFlow.Length; i++)
                tabFlow[i].UpdateData(aSubprogramData);

            if (tabFlow[0].Active || tabFlow[1].Active || tabFlow[2].Active || activeVaporaitor || IsBitActive(aSubprogramData.Command, Types.BIT_CMD_PRESSURE))
                active = true;
            else
                active = false;
        }
        //---------------------------------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie uzupelnienie danych w programie na temat parametrow subprogramu kontorli gazow 
        */
        override public void PrepareDataPLC(int[] aData)
        {
            if (active)
            {
                aData[Types.OFFSET_SEQ_GAS_MODE] = (int)modeGasProces;
                aData[Types.OFFSET_SEQ_GAS_TIME] = timeDurationProces.Hour * 3600 + timeDurationProces.Minute * 60 + timeDurationProces.Second;

                for (int i = 0; i < tabFlow.Length; i++)
                    tabFlow[i].PrepareData(aData);

                //Jezeli dany obiekt jest niedostepny fizycznie to nie mozna dla niego tworzyc programu dlatego Active ustaw wtedu na false
                if (Factory.Hpt1000 != null && Factory.Hpt1000.GetVaporizer() != null)
                    activeVaporaitor &= Factory.Hpt1000.GetVaporizer().GetActive();
                if (activeVaporaitor)
                {
                    aData[Types.OFFSET_SEQ_CMD] |= (int)System.Math.Pow(2, Types.BIT_CMD_FLOW_4);
                    aData[Types.OFFSET_SEQ_FLOW_4_CYCLE_TIME] = Types.ConvertDOUBLEToWORD(cycleTimeVaporaito, Types.Word.LOW);
                    aData[Types.OFFSET_SEQ_FLOW_4_CYCLE_TIME + 1] = Types.ConvertDOUBLEToWORD(cycleTimeVaporaito, Types.Word.HIGH);
                    aData[Types.OFFSET_SEQ_FLOW_4_ON_TIME] = Types.ConvertDOUBLEToWORD(onTimeVaporaitor, Types.Word.LOW);
                    aData[Types.OFFSET_SEQ_FLOW_4_ON_TIME + 1] = Types.ConvertDOUBLEToWORD(onTimeVaporaitor, Types.Word.HIGH);
                    aData[Types.OFFSET_SEQ_DOSING] = dosing;

                }

                if (modeGasProces == Types.GasProcesMode.Pressure_Vap || modeGasProces == Types.GasProcesMode.Presure_MFC)
                {
                    //Ustaw komende jako rozkaz
                    aData[Types.OFFSET_SEQ_CMD] |= (int)System.Math.Pow(2, Types.BIT_CMD_PRESSURE);
   
                    aData[Types.OFFSET_SEQ_GAS_SETPOINT] = Types.ConvertDOUBLEToWORD(setpointPressure, Types.Word.LOW);
                    aData[Types.OFFSET_SEQ_GAS_SETPOINT + 1] = Types.ConvertDOUBLEToWORD(setpointPressure, Types.Word.HIGH);

                    aData[Types.OFFSET_SEQ_GAS_DOWN_DIFFER] = Types.ConvertDOUBLEToWORD(minDeviationSP, Types.Word.LOW);
                    aData[Types.OFFSET_SEQ_GAS_DOWN_DIFFER + 1] = Types.ConvertDOUBLEToWORD(minDeviationSP, Types.Word.HIGH);

                    aData[Types.OFFSET_SEQ_GAS_UP_DIFFER] = Types.ConvertDOUBLEToWORD(maxDeviationSP, Types.Word.LOW);
                    aData[Types.OFFSET_SEQ_GAS_UP_DIFFER + 1] = Types.ConvertDOUBLEToWORD(maxDeviationSP, Types.Word.HIGH);
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie wartoscu tymczasowych parametrow jako wartosci rzeczywiste/akualne
        */
        public override void SetEditableParameters(bool changesStore)
        {
            if (changesStore)
            {
                activeVaporaitor    = activeVaporaitorTmp;
                timeDurationProces  = timeDurationProcesTmp;
                modeGasProces       = modeGasProcesTmp;
                cycleTimeVaporaito  = cycleTimeVaporaitoTmp;
                onTimeVaporaitor    = onTimeVaporaitorTmp;
                dosing              = dosingTmp;
                setpointPressure    = setpointPressureTmp;
                minDeviationSP      = minDeviationSPTmp;
                maxDeviationSP      = maxDeviationSPTmp;
                ChangesNotSave = true;
            }
            else // W celu unikniecia sytuacji w ktoerej nie zmieniona wartosc zostanie nadpisana nalezy inicjalizowanc wartosci tymczasowe wartosciami aktualnymi
            {
                activeVaporaitorTmp     = activeVaporaitor;
                timeDurationProcesTmp   = timeDurationProces;
                modeGasProcesTmp        = modeGasProces;
                cycleTimeVaporaitoTmp   = cycleTimeVaporaito;
                onTimeVaporaitorTmp     = onTimeVaporaitor;
                dosingTmp               = dosing;
                setpointPressureTmp     = setpointPressure;
                minDeviationSPTmp       = minDeviationSP;
                maxDeviationSPTmp       = maxDeviationSP;
            }
            //Wykonja funkcje dla flowmeterow
            for (int i = 0; i < tabFlow.Length; i++)
                tabFlow[i].SetEditableParameters(changesStore);
                          
            Changes = false;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanie ustawienie aktywnosci danych przeplywek w procesie kontroli gazow danego subprogramu
         */
        public void SetActiveFlow(bool aActive, int AFlowNo,bool aOnlyActive, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (((AFlowNo == 1 && tabFlow[0].Active != aActive) ||
                 (AFlowNo == 2 && tabFlow[1].Active != aActive) ||
                 (AFlowNo == 3 && tabFlow[2].Active != aActive) ||
                 (AFlowNo == 4 && activeVaporaitor  != aActive)) &&
                  tmpValue)
                Changes = true;

            if (AFlowNo == 1) tabFlow[0].ActiveTmp = aActive;
            if (AFlowNo == 2) tabFlow[1].ActiveTmp = aActive;
            if (AFlowNo == 3) tabFlow[2].ActiveTmp = aActive;
            if (AFlowNo == 4) activeVaporaitorTmp  = aActive;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
            {
                if (AFlowNo == 1) tabFlow[0].Active = aActive;
                if (AFlowNo == 2) tabFlow[1].Active = aActive;
                if (AFlowNo == 3) tabFlow[2].Active = aActive;
                if (AFlowNo == 4) activeVaporaitor = aActive;
            }
            //ustaw z automatu SahreGas dla danej liczby aktywnych kanalow. Jezeli jest 1 to 100 jezeli sa dwa to suma daje 100
            int aCountActiveFlow = 0;
            for(int i = 0; i < tabFlow.Length; i++)
            {
                if (tabFlow[i].ActiveTmp)
                    aCountActiveFlow++;
            }
            //Ustaw ShareGas aby suma aktywnych kanalow byla 100
            int aValue = 0;
            if (aCountActiveFlow > 0)
                aValue = 100 / aCountActiveFlow;

            if (!aOnlyActive)
            {
                for (int i = 0; i < tabFlow.Length; i++)
                {
                    if (tabFlow[i].ActiveTmp)
                        tabFlow[i].ShareGasTmp = aValue;
                    else
                        tabFlow[i].ShareGasTmp = 0;
                }
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
          * Metoda zwraca info ktore przeplywki biora udzial w sterowaniu gazami danego subprogramu
          */
        public bool GetActiveFlow(int AFlowNo)
        {
            bool aActive = false;

            if (AFlowNo == 1) aActive = tabFlow[0].Active;
            if (AFlowNo == 2) aActive = tabFlow[1].Active;
            if (AFlowNo == 3) aActive = tabFlow[2].Active;
            if (AFlowNo == 4) aActive = activeVaporaitor;

            return aActive;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
          * Metoda ustawia przplyw danej przeplywki
          */
        public void SetGasFlow(double aFlow, int aFlowNo,bool tmpValue = false)
        {
            aFlow = Math.Round(aFlow, 3);

            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (((aFlowNo == 1 && tabFlow[0].GasFlow != aFlow) ||
                 (aFlowNo == 2 && tabFlow[1].GasFlow != aFlow) ||
                 (aFlowNo == 3 && tabFlow[2].GasFlow != aFlow)) &&
                  tmpValue)
                Changes = true;

            if (aFlowNo == 1) tabFlow[0].GasFlowTmp = (int)aFlow;
            if (aFlowNo == 2) tabFlow[1].GasFlowTmp = (int)aFlow;
            if (aFlowNo == 3) tabFlow[2].GasFlowTmp = (int)aFlow;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
            {
                if (aFlowNo == 1) tabFlow[0].GasFlow = (int)aFlow;
                if (aFlowNo == 2) tabFlow[1].GasFlow = (int)aFlow;
                if (aFlowNo == 3) tabFlow[2].GasFlow = (int)aFlow;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
          * Metoda zwraca przeplyw danej plrzywki
          */
        public double GetGasFlow(int aFlowNo)
        {
            int     aFlow     = 0;
            
            if (aFlowNo == 1) aFlow = tabFlow[0].GasFlow;
            if (aFlowNo == 2) aFlow = tabFlow[1].GasFlow;
            if (aFlowNo == 3) aFlow = tabFlow[2].GasFlow;
            
            return aFlow;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanie przypisanie do danej przplywki rodzaju gazu ktory jest fizycznie podpiety
         */
        public void SetGasType(int idTypeGas, int aFlowNo, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (((aFlowNo == 1 && tabFlow[0].IDTypeGas != idTypeGas) ||
                 (aFlowNo == 2 && tabFlow[1].IDTypeGas != idTypeGas) ||
                 (aFlowNo == 3 && tabFlow[2].IDTypeGas != idTypeGas)) &&
                  tmpValue)
                Changes = true;

            if (aFlowNo == 1) tabFlow[0].IDTypeGasTmp = idTypeGas;
            if (aFlowNo == 2) tabFlow[1].IDTypeGasTmp = idTypeGas;
            if (aFlowNo == 3) tabFlow[2].IDTypeGasTmp = idTypeGas;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
            {
                if (aFlowNo == 1) tabFlow[0].IDTypeGas = idTypeGas;
                if (aFlowNo == 2) tabFlow[1].IDTypeGas = idTypeGas;
                if (aFlowNo == 3) tabFlow[2].IDTypeGas = idTypeGas;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda zwraca ID gazu ktory jest powiazany z dana przeplywka
        */
        public int GetGasType(int aFlowNo)
        {
            int idTypeGas = 0;

            if (aFlowNo == 1) idTypeGas = tabFlow[0].IDTypeGas;
            if (aFlowNo == 2) idTypeGas = tabFlow[1].IDTypeGas;
            if (aFlowNo == 3) idTypeGas = tabFlow[2].IDTypeGas;

            return idTypeGas;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
          * Metoda ustawia jak długo powinna sie odbywac kontorla gazow w komorze przez dany subprogram
          */
        public void SetTimeProcesDuration(DateTime aTimeProces,bool tmpValue = false)
        {
            //Dodaj rok aby mozna bylo te date wyswietlic. Nas interesuje tylko czas
            if (aTimeProces.Year < 2000)
                aTimeProces = aTimeProces.AddYears(2000);
            
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (timeDurationProces != aTimeProces && tmpValue)
                Changes = true;

            timeDurationProcesTmp = aTimeProces;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                timeDurationProces = aTimeProces;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda zwraca czas jak dlugo trwac bedzie subprogram
        */
        public DateTime GetTimeProcesDuration()
        {
            return timeDurationProces;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda ustawia czas cyklu pracy vaporaizer
         */
        public void SetCycleTime(double aTime, bool tmpValue = false)
        {
            aTime = Math.Round(aTime, 3);

            //zabezpieczenie przed zbyt duza wartoscia
            if (aTime > 30000)
                aTime = 30000;
        
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (cycleTimeVaporaito != aTime && tmpValue)
                Changes = true;

            cycleTimeVaporaitoTmp = aTime;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                cycleTimeVaporaito = aTime;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda  zwraca czas cyklu pracy vaporaizer
        */
        public double GetCycleTime()
        {
            return cycleTimeVaporaito;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda ustawia jak dlugo w danym cyklu ma byc vaporaizer wlaczony
         */
        public void SetOnTime(int aTime, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (onTimeVaporaitor != aTime && tmpValue)
                Changes = true;

            onTimeVaporaitorTmp = aTime;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                onTimeVaporaitor = aTime;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda zwraca info jak dlugo w danym cyklu ma byc vaporaizer wlaczony
         */
        public int GetOnTime()
        {
            return onTimeVaporaitor;
        }
        /*
        * Metoda ma za zadanie ustawienie wartosci dozowania gazu do komory przez vaporaizer
        */
        //---------------------------------------------------------------------------------------------------------------
        public void SetDosing(int aDosing,bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (dosing != aDosing && tmpValue)
                Changes = true;

            dosingTmp = aDosing;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                dosing = aDosing;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie zwrocenie wartosci dozowania gazu do komory przez vaporaizer
        */
        public int GetDosing()
        {
            return dosing;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ustawia zakre przeplywu jaki powinien byc w czasie dzialania subprogramu
        */
        public void SetMinGasFlow(int aMinFlow, int aFlowNo,bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (((aFlowNo == 1 && tabFlow[0].MinGasFlow != aMinFlow) ||
                 (aFlowNo == 2 && tabFlow[1].MinGasFlow != aMinFlow) ||
                 (aFlowNo == 3 && tabFlow[2].MinGasFlow != aMinFlow)) &&
                  tmpValue)
                Changes = true;

            if (aFlowNo == 1) tabFlow[0].MinGasFlowTmp = aMinFlow;
            if (aFlowNo == 2) tabFlow[1].MinGasFlowTmp = aMinFlow;
            if (aFlowNo == 3) tabFlow[2].MinGasFlowTmp = aMinFlow;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
            {
                if (aFlowNo == 1) tabFlow[0].MinGasFlow = aMinFlow;
                if (aFlowNo == 2) tabFlow[1].MinGasFlow = aMinFlow;
                if (aFlowNo == 3) tabFlow[2].MinGasFlow = aMinFlow;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda zwraca zakre przeplywu jaki powinien byc w czasie dzialania subprogramu
        */
        public double GetMinGasFlow(int aFlowNo)
        {
            int aMinFlow = 0;

            if (aFlowNo == 1) aMinFlow = tabFlow[0].MinGasFlow;
            if (aFlowNo == 2) aMinFlow = tabFlow[1].MinGasFlow;
            if (aFlowNo == 3) aMinFlow = tabFlow[2].MinGasFlow;

            return aMinFlow;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ustawia zakre przeplywu jaki powinien byc w czasie dzialania subprogramu
        */
        public void SetMaxGasFlow(int aMaxFlow, int aFlowNo, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (((aFlowNo == 1 && tabFlow[0].MaxGasFlow != aMaxFlow) ||
                 (aFlowNo == 2 && tabFlow[1].MaxGasFlow != aMaxFlow) ||
                 (aFlowNo == 3 && tabFlow[2].MaxGasFlow != aMaxFlow)) &&
                  tmpValue)
                Changes = true;

            if (aFlowNo == 1) tabFlow[0].MaxGasFlowTmp = aMaxFlow;
            if (aFlowNo == 2) tabFlow[1].MaxGasFlowTmp = aMaxFlow;
            if (aFlowNo == 3) tabFlow[2].MaxGasFlowTmp = aMaxFlow;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
            {
                if (aFlowNo == 1) tabFlow[0].MaxGasFlow = aMaxFlow;
                if (aFlowNo == 2) tabFlow[1].MaxGasFlow = aMaxFlow;
                if (aFlowNo == 3) tabFlow[2].MaxGasFlow = aMaxFlow;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda zwraca zakre przeplywu jaki powinien byc w czasie dzialania subprogramu
        */
        public double GetMaxGasFlow(int aFlowNo)
        {
            int aMaxFlow = 0;

            if (aFlowNo == 1) aMaxFlow = tabFlow[0].MaxGasFlow;
            if (aFlowNo == 2) aMaxFlow = tabFlow[1].MaxGasFlow;
            if (aFlowNo == 3) aMaxFlow = tabFlow[2].MaxGasFlow;

            return aMaxFlow;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ustawia dzielenie gazow ale pameitaj ze suma aktywnych kanalow mus byc 100 dlatego z autoamtu uzupelniam pozostale aktywne kanly.
        */
        public void SetShareGas(int aShareGas, int aFlowNo, bool aOryginalValue = false, bool tmpValue = false)
        {
            if (aOryginalValue)
            {
                if (((aFlowNo == 1 && tabFlow[0].ShareGas != aShareGas) ||
                     (aFlowNo == 2 && tabFlow[1].ShareGas != aShareGas) ||
                     (aFlowNo == 3 && tabFlow[2].ShareGas != aShareGas)) &&
                      tmpValue)
                    Changes = true;

                if (aFlowNo == 1) tabFlow[0].ShareGasTmp = aShareGas;
                if (aFlowNo == 2) tabFlow[1].ShareGasTmp = aShareGas;
                if (aFlowNo == 3) tabFlow[2].ShareGasTmp = aShareGas;
                //Jezeli mam ustawic wartosci rzeczywiste to je ustaw
                if (!tmpValue)
                {
                    //Ustwa wartosc taka jak jest podana bez obliczen i ustawiania pozostalych (jest to wymagane do odczytu z bazy danych)
                    if (aFlowNo == 1) tabFlow[0].ShareGas = aShareGas;
                    if (aFlowNo == 2) tabFlow[1].ShareGas = aShareGas;
                    if (aFlowNo == 3) tabFlow[2].ShareGas = aShareGas;
                }
            }
            else
            {
                //Ustaw/nadpisz wartosc wynikajacam ze suma aktywnych kanalow musi byc 100
                //Oblicz ile jest aktynych kanalow
                int aCountActiveChannels = 0;
                for (int i = 0; i < tabFlow.Length; i++)
                {
                    if (tabFlow[i].ActiveTmp)
                        aCountActiveChannels++;
                }
                //aktywny tylko jeden kanal to ustaw z automatu 100
                if (aCountActiveChannels == 1)
                    aShareGas = 100;

                //aktywne sa dwa kanaly to dostosuj ten drugi do aktulnej wartosci aby suma byla 100
                if (aCountActiveChannels == 2)
                {
                    for (int i = 0; i < tabFlow.Length; i++)
                    {
                        if (tabFlow[i].ActiveTmp && i != aFlowNo - 1)
                            tabFlow[i].ShareGasTmp = 100 - aShareGas;
                    }
                }
                //Ustaw wartosc wynikajacam z funkcji - musi to byc za 1 i 2 a przed 3
                if (aFlowNo == 1) tabFlow[0].ShareGasTmp = aShareGas;
                if (aFlowNo == 2) tabFlow[1].ShareGasTmp = aShareGas;
                if (aFlowNo == 3) tabFlow[2].ShareGasTmp = aShareGas;

                aFlowNo--;//dostosowabie zmiennej do indeksu tablicy

                //aktywne sa trzy kanaly to dopelnij do 100 przedostatnio edytowany kanal biorac pod uwage sume dwoch wczesniej edytowanych kanalow
                int aTmp = 0;
                int aLastShareGasEditChannel = GetLastEditGasShareChannel();
                if (aCountActiveChannels == 3)
                {
                    for (int i = 0; i < tabFlow.Length; i++)
                    {
                        if (i != aLastShareGasEditChannel)
                            aTmp += tabFlow[i].ShareGasTmp;
                    }
                    if (aLastShareGasEditChannel >= 0 && aLastShareGasEditChannel < tabFlow.Length)
                    {
                        int aValue = 100 - aTmp;
                        if (aValue >= 0)
                            tabFlow[aLastShareGasEditChannel].ShareGasTmp = aValue;
                        else
                        {
                            //Suma kanalow jest wieksza od 100 dlatego jednemu ustawiam na 0 a drugi dopelniam do 100
                            for (int i = 0; i < tabFlow.Length; i++)
                                if (i != aFlowNo && i != aLastShareGasEditChannel)
                                    tabFlow[i].ShareGasTmp += aValue; //defacto odejmuje poniewaz wartosc aValue jest ujemna
                            tabFlow[aLastShareGasEditChannel].ShareGasTmp = 0;
                        }
                    }
                }
                //Zapamietaj kolejnosc edycji ktora byla wykonana aby pozniej wiedziec dla ktorego kanalu mam manewrowac wartoscia dopelninia do 100 - ostatni ktroy byl edytowany
                if (curentIndex >= 0 && curentIndex < tabOrderEditGasShareChannel.Length)
                {
                    if (IfNewAction(aFlowNo))
                    {
                        tabOrderEditGasShareChannel[curentIndex] = aFlowNo;
                        curentIndex++;
                    }
                }
                //Przekrecenie sie licznka ustaw od 0
                if (curentIndex >= tabOrderEditGasShareChannel.Length)
                    curentIndex = 0;
               
                //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
                if (((aFlowNo == 0 && tabFlow[0].ShareGas != tabFlow[0].ShareGasTmp) ||
                     (aFlowNo == 1 && tabFlow[1].ShareGas != tabFlow[1].ShareGasTmp) ||
                     (aFlowNo == 2 && tabFlow[2].ShareGas != tabFlow[2].ShareGasTmp)) &&
                      tmpValue)
                    Changes = true;

                //Jezeli wartosc nie jest tymczasowa to zapisz ja jako rzczywista
                if (!tmpValue)
                {
                    if (aFlowNo == 0) tabFlow[0].ShareGas = tabFlow[0].ShareGasTmp;
                    if (aFlowNo == 1) tabFlow[1].ShareGas = tabFlow[1].ShareGasTmp;
                    if (aFlowNo == 2) tabFlow[2].ShareGas = tabFlow[2].ShareGasTmp;
                }
            }            
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda wykorzystywana podczas ustalania w jaki sposob mam uzupelniac przeplyw do 100 dla pozostalych kanalow mFC
        */
        private bool IfNewAction(int aFlowNo)
        {
            bool aRes = false;
            int aIndex = curentIndex - 1;

            if (aIndex < 0)
                aIndex = tabOrderEditGasShareChannel.Length - 1;

            if (aIndex >= 0 && aIndex < tabOrderEditGasShareChannel.Length && tabOrderEditGasShareChannel[aIndex] != aFlowNo)
                aRes = true;

            return aRes;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie zwrocenie wartosci przeplywu ostiono edytowanego kanalu MFC
        */
        private int GetLastEditGasShareChannel()
        {
            int aLastChannel = 0;

            if (curentIndex == 0) aLastChannel = tabOrderEditGasShareChannel[1];
            if (curentIndex == 1) aLastChannel = tabOrderEditGasShareChannel[2];
            if (curentIndex == 2) aLastChannel = tabOrderEditGasShareChannel[0];

            if (aLastChannel == -1)
            {
                aLastChannel = curentIndex + 1;
                if (aLastChannel >= tabOrderEditGasShareChannel.Length)
                    aLastChannel = 0;
            }

            return aLastChannel;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda zwraca info na temat
        */
        public double GetShareGas(int aFlowNo)
        {
            int aShareGas = 0;

            if (aFlowNo == 1) aShareGas = tabFlow[0].ShareGas;
            if (aFlowNo == 2) aShareGas = tabFlow[1].ShareGas;
            if (aFlowNo == 3) aShareGas = tabFlow[2].ShareGas;

            return aShareGas;
        }
        /*
     * Metoda zwraca info na temat
     */
        public double GetShareGasTmp(int aFlowNo)
        {
            int aShareGas = 0;

            if (aFlowNo == 1) aShareGas = tabFlow[0].ShareGasTmp;
            if (aFlowNo == 2) aShareGas = tabFlow[1].ShareGasTmp;
            if (aFlowNo == 3) aShareGas = tabFlow[2].ShareGasTmp;

            return aShareGas;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
           * Metoda ma za zadanie
           */
        public void SetShareDevaition(int aDevaition, int aFlowNo, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (((aFlowNo == 1 && tabFlow[0].MaxDeviation != aDevaition) ||
                 (aFlowNo == 2 && tabFlow[1].MaxDeviation != aDevaition) ||
                 (aFlowNo == 3 && tabFlow[2].MaxDeviation != aDevaition)) &&
                  tmpValue)
                Changes = true;

            if (aFlowNo == 1) tabFlow[0].MaxDeviationTmp = aDevaition;
            if (aFlowNo == 2) tabFlow[1].MaxDeviationTmp = aDevaition;
            if (aFlowNo == 3) tabFlow[2].MaxDeviationTmp = aDevaition;
            //Jezeli wartosc nie jest tymczasowa to zapisz ja jako rzczywista
            if (!tmpValue)
            {
                if (aFlowNo == 1) tabFlow[0].MaxDeviation = aDevaition;
                if (aFlowNo == 2) tabFlow[1].MaxDeviation = aDevaition;
                if (aFlowNo == 3) tabFlow[2].MaxDeviation = aDevaition;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
           * Metoda ma za zadanie
           */
        public double GetShareDevaition(int aFlowNo)
        {
            int aDevaition = 0;

            if (aFlowNo == 1) aDevaition = tabFlow[0].MaxDeviation;
            if (aFlowNo == 2) aDevaition = tabFlow[1].MaxDeviation;
            if (aFlowNo == 3) aDevaition = tabFlow[2].MaxDeviation;

            return aDevaition;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
           * Metoda ma za zadanie
           */
        public void SetModeProces(Types.GasProcesMode aModeProces, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (modeGasProces != aModeProces && tmpValue)
                Changes = true;

            modeGasProcesTmp = aModeProces;
            for (int i = 0; i < tabFlow.Length; i++)
                tabFlow[i].ModeTmp = aModeProces;
            //Jezeli wartosc nie jest tymczasowa to zapisz ja jako rzczywista
            if (!tmpValue)
            {
                modeGasProces = aModeProces;
                for (int i = 0; i < tabFlow.Length; i++)
                    tabFlow[i].Mode = aModeProces;
            }
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
           * Metoda ma za zadanie
           */
        public Types.GasProcesMode GetModeProces()
        {
            return modeGasProces;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
           * Metoda ma za zadanie
           */
        public void SetMinDeviationPresure(double aDev, bool tmpValue = false)
        {
            aDev = Math.Round(aDev, 3);

            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (minDeviationSP != aDev && tmpValue)
                Changes = true;

            minDeviationSPTmp = aDev;
            //Jezeli wartosc nie jest tymczasowa to zapisz ja jako rzczywista
            if (!tmpValue)
                minDeviationSP = aDev;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanie ustawienie minimalnej 
         */
        public double GetMinDeviationPresure()
        {
            return minDeviationSP;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie
        */
        public void SetMaxDeviationPresure(double aDev, bool tmpValue = false)
        {
            aDev = Math.Round(aDev, 3);

            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (maxDeviationSP != aDev && tmpValue)
                Changes = true;

            maxDeviationSPTmp = aDev;
            //Jezeli wartosc nie jest tymczasowa to zapisz ja jako rzczywista
            if (!tmpValue)
                maxDeviationSP = aDev;
        }
        /*
        * Metoda ma za zadanie
        */
        //---------------------------------------------------------------------------------------------------------------
        public double GetMaxDeviationPresure()
        {
            return maxDeviationSP;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie ustawienie setpointa cisniena utrzymywanego w komorze podaczas automatycznego (PID) sterowania cisnieniem
        */
        public void SetSetpointPressure(double aSetpoint, bool tmpValue = false)
        {
            //zaokraglij do 3 miejsc po przecinku
            aSetpoint = Math.Round(aSetpoint, 3);
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (setpointPressure != aSetpoint && tmpValue)
                Changes = true;

            setpointPressureTmp = aSetpoint;
            //Jezeli wartosc nie jest tymczasowa to zapisz ja jako rzczywista
            if (!tmpValue)
                setpointPressure = aSetpoint;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie zwrocenie info na temat setpointa cisniena utrzymywanego w komorze podaczas automatycznego (PID) sterowania cisnieniem
        */
        public double GetSetpointPressure()
        {
            return setpointPressure;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie ustawienie dolnego pulapu cisnienia w komorze po przekroczeniu ktorego podczas automatycznego sterowania gazami (PID) pojawia sie blad
        */
        public void SetLimitDown(int aLimitDown, int aFlowNo)
        {
            if (aFlowNo == 1) tabFlow[0].GasLimitDown = aLimitDown;
            if (aFlowNo == 2) tabFlow[1].GasLimitDown = aLimitDown;
            if (aFlowNo == 3) tabFlow[2].GasLimitDown = aLimitDown;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanie zwrocenie dolnego pulapu cisnienia w komorze po przekroczeniu ktorego podczas automatycznego sterowania gazami (PID) pojawia sie blad
         */
        public int GetLimitDown(int aFlowNo)
        {
            int aLimitDown = 0;

            if (aFlowNo == 1) aLimitDown = tabFlow[0].GasLimitDown;
            if (aFlowNo == 2) aLimitDown = tabFlow[1].GasLimitDown;
            if (aFlowNo == 3) aLimitDown = tabFlow[2].GasLimitDown;

            return aLimitDown;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie ustawienie gornego pulapu cisnienia w komorze po przekroczeniu ktorego podczas automatycznego sterowania gazami (PID) pojawia sie blad
        */
        public void SetLimitUp(int aLimitUp, int aFlowNo)
        {
            if (aFlowNo == 1) tabFlow[0].GasLimitUp = aLimitUp;
            if (aFlowNo == 2) tabFlow[1].GasLimitUp = aLimitUp;
            if (aFlowNo == 3) tabFlow[2].GasLimitUp = aLimitUp;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie zwrocenie gornego pulapu cisnienia w komorze po przekroczeniu ktorego podczas automatycznego sterowania gazami (PID) pojawia sie blad
        */
        public int GetLimitUp(int aFlowNo,int aIDGasType = -1)
        {
            int aLimitUp = 0;
            //Zwracana wartosc zalezy od aktualnie wybranego gazu oraz faktora kalibracji
            if (aFlowNo > 0 && aFlowNo <= 3 && Factory.Hpt1000 != null && Factory.Hpt1000.GetMFC() != null)
            {
                if (aIDGasType < 0)
                    aIDGasType = tabFlow[aFlowNo - 1].IDTypeGas;
                double aFactorCalib = Factory.Hpt1000.GetMFC().GetCalibratedFactor(aFlowNo);
                double aFactor      = Factory.Hpt1000.GetGasTypes().GetFactor(aIDGasType);
                int    aRangeFlow   = Factory.Hpt1000.GetMFC().GetMaxCalibFlow(aFlowNo);
                if (aFactorCalib > 0)
                    aLimitUp = (int)(aFactor / aFactorCalib * aRangeFlow);
            }
            /*
            if (aFlowNo == 1) aLimitUp = MFC_Channel.MAX_FLOW_MFC_1;// tabFlow[0].GasLimitUp;
            if (aFlowNo == 2) aLimitUp = MFC_Channel.MAX_FLOW_MFC_2;//tabFlow[1].GasLimitUp;
            if (aFlowNo == 3) aLimitUp = MFC_Channel.MAX_FLOW_MFC_3;//tabFlow[2].GasLimitUp;
            */
            return aLimitUp;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie ustawienie aktywnosci vaporaziera jako elementu subprogramu
        */
        public void SetVaporaiserActive(bool aValue, bool tmpValue = false)
        {
            //Sprawdz czy nie zmieniam tymczasowych parametrow procesu
            if (activeVaporaitor != aValue && tmpValue)
                Changes = true;

            activeVaporaitorTmp = aValue;
            //Ustaw wartosc jako rzeczywista jezeli nie jest tymczasowa
            if (!tmpValue)
                activeVaporaitor = aValue;
        }
        //---------------------------------------------------------------------------------------------------------------
        /*
        * Metoda ma za zadanie zwrocenie informajic czy vaporaizer bierze udzial w steroeniau gazami
        */
        public bool GetVaporiserActive()
        {
            return activeVaporaitor;
        }
        //---------------------------------------------------------------------------------------------------------------
    }
}
