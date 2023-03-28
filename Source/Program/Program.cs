using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace HPT1000.Source.Program
{
    public delegate void RefreshProgram();
    //-------------------------------------------------------------------------------------------------------------------------
    //Struktura zawiera dane regultaroa przeplywu. Poniewaz jest ich kilka to lepiej to zgromadzic w tablicy tych samych struktur
    //-------------------------------------------------------------------------------------------------------------------------
    [Serializable]
    public struct MFCData
    {
        public bool Active;
        public int  Flow;
        public int  MinFlow;
        public int  MaxFlow;
        public int  ShareGas;
        public int  Devaition;

        public MFCData(int aDiffer)
        {
            Active    = false;
            Flow      = 0;
            MinFlow   = 0;
            MaxFlow   = 0;
            ShareGas  = 0;
            Devaition = 0;
        }
    };
    //-------------------------------------------------------------------------------------------------------------------------
    //Struktrua zwiera dane aktualnie wykonywanego subprogramu w PLC
    //-------------------------------------------------------------------------------------------------------------------------
    [Serializable]
    public struct SubprogramData
    {
        public int      WorkingTimePump;
        public int      WorkingTimeGas;
        public int      WorkingTimeHV;
        public int      WorkingTimeFlush;
        public int      WorkingTimeVent;
        public int      WorkingTimeMotor;

        public int      Command;
        public int      Status;
        public int      Pump_TargetTime;
        public double   Pump_SetpointPressure;
        public int      Vent_TargetTime;
        public int      Flush_TargetTime;
        public int      HV_Operate_Mode;
        public double   HV_Setpoint;
        public double   HV_Deviation;
        public int      HV_TargetTime;
        public double   Vaporaitor_CycleTime;
        public int      Vaporaitor_Open;
        public int      Vaporaitor_Dosing;
        public int      GasProces_Mode;
        public int      GasProces_TimeTarget;
        public double   GasProces_Setpoint;
        public double   GasProces_MaxDiffer;
        public double   GasProces_MinDiffer;
        public int      Motor1_Command;
        public int      Motor1_OperateTime;
        public int      Motor2_Command;
        public int      Motor2_OperateTime;


        public MFCData[] tabMFC;

        public SubprogramData(int A)
        {
            WorkingTimePump     = 0;
            WorkingTimeGas      = 0;
            WorkingTimeHV       = 0;
            WorkingTimeFlush    = 0;
            WorkingTimeVent     = 0;
            WorkingTimeMotor   = 0;

            Command             = 0;
            Status              = 0;
            Pump_TargetTime     = 0;
            Pump_SetpointPressure = 0;
            Vent_TargetTime     = 0;
            Flush_TargetTime    = 0;
            HV_Operate_Mode     = 0;
            HV_Setpoint         = 0;
            HV_TargetTime       = 0;
            HV_Deviation        = 0;
            Vaporaitor_CycleTime = 0;
            Vaporaitor_Open     = 0;
            Vaporaitor_Dosing   = 0;
            GasProces_Mode      = 0;
            GasProces_TimeTarget = 0;
            GasProces_Setpoint  = 0;
            GasProces_MaxDiffer = 0;
            GasProces_MinDiffer = 0;
            Motor1_Command      = 0;
            Motor1_OperateTime  = 0;
            Motor2_Command      = 0;
            Motor2_OperateTime  = 0;

            tabMFC = new MFCData[3];
            for (int i = 0; i < tabMFC.Length; i++)
            {
                tabMFC[i].Flow      = 0;
                tabMFC[i].MinFlow   = 0;
                tabMFC[i].MaxFlow   = 0;
                tabMFC[i].ShareGas  = 0;
                tabMFC[i].Devaition = 0;
            }
        }
    };
    //-------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Klasa reprezentuje podstawowe parametry programu ktore powinny zostac zapisane w bazie danych w formie byte 
    /// </summary>
    [Serializable]
    public class ProcesParameter 
    {
        private string                      name         = "Program name";
        private string                      description  = "";
        private List<SubprogramParameter>   subPrograms  = new List<SubprogramParameter>();

        [OptionalField]
        private string                      user = "";

        [OptionalField]
        private string                      processInformation = "";    // Extra informacja wprowadona po rozpoczeciu programu

        //-------------------------------------------------------------------------------------------------------------------------
        public string ProcessInformation
        {
            get { return processInformation; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public string User
        {
            set { user = value;}
            get { return user; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public string Name
        {
            get { return name; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public string Description
        {
            get { return description; }
        }//-------------------------------------------------------------------------------------------------------------------------
        public List<SubprogramParameter> SubprogramsPara
        {
            get { return subPrograms; }
        }//-------------------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy kotry inicalizuje wymagane pola do zapisu w bazi ktore sa uznawane za podstawowe bazujac na zrodlowym programie
         */
        public ProcesParameter(Program pr)
        {
            if(pr != null)
            {
                //Ustaw wartosci pola nazwa i opis
                name = pr.GetName();
                description = pr.GetDescription();
                processInformation = pr.GetProcessInformation();

                //Utworz liste subprgroamow
                foreach (Subprogram subPr in pr.GetSubprograms())
                    subPrograms.Add(subPr.GetParameters());
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
    };
    //-------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Klasa reprezentuje program kotry zawiera wszelkie informacje do sterowania procesm w sterowniku PLC. 
    /// Progrma sklada sie z subprogramow a subprogmray ze segmentow.
    /// </summary>

    public class Program : Object
    {
        private DB                      dataBase            = null;    ///< Referencja na obiekt bazy danych wymagany do odczytania/zapisania konfigruacji programow w bazie danych
        //Dane odczytane z PLC
        private Types.StatusProgram     status              = Types.StatusProgram.Stop;
        private int                     countSubprogramsPLC = 0;        //liczba subprogramow z ktorych sie skalada program wczytny do PLC
        private int                     actualSubprogramId  = 0;        //numer aktualnie wykonywanego subprogramu

        //Dane potrzebne do konfigruacji na PC
        private PLC                     plc                 = null;
        private string                  name                = "Program name";
        private uint                    id                  = 0;         //unikalny identyfikator programu po ktorym rozrozniamy programy nadawane przez baze danych
        private string                  description         = "";
        private string                  processInformation  = "";
        private List<Subprogram>        subPrograms         = new List<Subprogram>();
       // private List<Subprogram>        subProgramsActual   = new List<Subprogram>(); //Lista subprogramow ktora sluzy do akutalizacji parametrow uruchomionego procesu. 
                                                                                      //Aby nie nadpisywac parametrow zapisanych w programie tworze alternatywna liste
        private bool                    existInPLC          = false; //Flaga okresl czy dany program aktualnie istnieje w PLC
        private static object           Sync                = new object(); ///< Obiekt synchronizacji watkow

        private static RefreshProgram   refreshProgram      = null;

        private GUI.MainForm            mainForm            = null;

        private bool                    changes             = false;
        private bool                    changesNotSave      = false;
        private string                  nameTmp             = "Program name";

        private bool subprogramChangedPlace = false;

        //-------------------------------------------------------------------------------------------------------------------------
        public bool Changes
        {
            get { return changes; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public bool ExistInPLC
        {
            get { return existInPLC; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public DB DataBase
        {
            set { dataBase = value; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public Types.StatusProgram Status
        {
            get { return status; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public int CountSubprograms
        {
            get { return countSubprogramsPLC; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public int ActualSubprogramId
        {
            get { return actualSubprogramId; }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Konstruktor
         */ 
        public Program(GUI.MainForm mainFormPtr)
        {
            mainForm = mainFormPtr;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda porownuje referencje dwoch programow
         */ 
        public override bool Equals(object other)
        {
            bool aRes = false;
            //Porownuje tylko po referencji bo w kilku miejscach sie odnosze do tej samej referencji i cos zmieniam.
            if (ReferenceEquals(this, other))// || (this.GetType() == other.GetType() && ((Program)this).id == ((Program)other).GetID()))
                aRes = true;

            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca hashCode
         */
        public override int GetHashCode()
        {
           return base.GetHashCode();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        /* Funkcja odczytuje dane z PLC na temat aktualnie wykonywanego subprogramu
        */
        public void UpdateActualSubprogramData(int[] aData,int aIntegralSeqData)
        {
            //Odczytaj dane z PLC ale tylko do programu ktroego ID pokrywa sie z tym wgranym do PLC
            int aProgramId = aData[Types.OFFSET_PRG_ACTUAL_PRG_ID];
            if (aProgramId == id)
            {
                existInPLC          = true;
                status              = (Types.StatusProgram)aData[Types.OFFSET_PRG_STATUS];
                actualSubprogramId  = aData[Types.OFFSET_PRG_ACTUAL_SEQ_ID];
                //Poniewaz funkcja GetSegmentData pracuje na ofsetach pamieci Segmentu dlatego dane segmentu powinny sie zaczynac od krotki 0 co sie wiaze z koniecznoscia przekopiowania danych do nowej tablicy
                int[] aDataSeq = new int[Types.SEGMENT_SIZE];
                for (int i = 0; i < aData.Length; i++)
                    if (aData.Length > (i + Types.OFFSET_PRG_SEQ_DATA) && aDataSeq.Length > i)
                        aDataSeq[i] = aData[i + Types.OFFSET_PRG_SEQ_DATA];

                SubprogramData aSubprogramData      = GetSegmentData(aDataSeq, 0);

               // aSubprogramData.Status              = aData[Types.OFFSET_PRG_SUBPR_STATUS];
                aSubprogramData.WorkingTimeFlush    = aData[Types.OFFSET_PRG_TIME_FLUSH];
                aSubprogramData.WorkingTimeGas      = aData[Types.OFFSET_PRG_TIME_GAS];
                aSubprogramData.WorkingTimeHV       = aData[Types.OFFSET_PRG_TIME_HV];
                aSubprogramData.WorkingTimePump     = aData[Types.OFFSET_PRG_TIME_PUMP];
                aSubprogramData.WorkingTimeVent     = aData[Types.OFFSET_PRG_TIME_VENT];
                aSubprogramData.WorkingTimeMotor   = aData[Types.OFFSET_PRG_TIME_MOTOR];
           
                //aktualizuj status subprogramu oraz dane w aktualnie wykonywanym subprogramie pod warunkiem ze program jest wykonywny 
                int subprogramIndex = 0;
                foreach (Subprogram subProgram in subPrograms )//subProgramsActual)
                {
                    //Aktulizuj dane procesow danego subprogramu ktory jest aktualnie wykonywany w PLC
                    if (subProgram.ID == actualSubprogramId && IsRun() && aIntegralSeqData == 1)
                        subProgram.UpdateData(aSubprogramData);
                    //Aktualizuj status danego subprogramu. Statusy znajduja sie w zbiorczej przstrzeni pamieci zaraz po danych aktualnego subpropgramu
                    if (aData.Length > (subprogramIndex + Types.OFFSET_STATUS_DATA))
                        subProgram.UpdateStatus((Types.StatusProgram)aData[subprogramIndex + Types.OFFSET_STATUS_DATA]);
                    subprogramIndex++;
                }
            }
            else
            {
                //Ustawiam statu ze program nie jest zaladowany do PLC.
                status = Types.StatusProgram.Stop;// Types.StatusProgram.NoLoad;
                existInPLC  = false;

            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        /* Funkcja odczytaje z tablicy danych pobranych z PLC dane subprogramu
        */
        private SubprogramData GetSegmentData(int[] aData, uint aSubprogramNo)
        {
            SubprogramData aSubprogramData = new SubprogramData(0);
            uint aOffset = aSubprogramNo * Types.SEGMENT_SIZE; //przesuniecie danych segmentu wzgledem aktualnie odczytanej przestrzeni pamieci

            if (CheckCorectTabSize((int)(aData.Length - aOffset)))
            {        
                aSubprogramData.Command                 = aData[Types.OFFSET_SEQ_CMD              + aOffset];
                aSubprogramData.Status                  = aData[Types.OFFSET_SEQ_STATUS           + aOffset];

                aSubprogramData.Pump_SetpointPressure   = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_PUMP_SP + aOffset));
                aSubprogramData.Pump_TargetTime         = aData[Types.OFFSET_SEQ_PUMP_MAX_TIME    + aOffset];

                aSubprogramData.Vent_TargetTime         = aData[Types.OFFSET_SEQ_VENT_TIME        + aOffset];

                aSubprogramData.Flush_TargetTime        = aData[Types.OFFSET_SEQ_FLUSH_TIME       + aOffset];

                aSubprogramData.HV_Operate_Mode         = aData[Types.OFFSET_SEQ_HV_OPERATE       + aOffset];
                aSubprogramData.HV_Setpoint             = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_HV_SETPOINT       + aOffset));
                aSubprogramData.HV_Deviation            = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_HV_DRIFT_SETPOINT + aOffset));
                aSubprogramData.HV_TargetTime           = aData[Types.OFFSET_SEQ_HV_TIME          + aOffset];

                aSubprogramData.tabMFC[0].Active        = IsBitActive(aData[Types.OFFSET_SEQ_CMD  + aOffset],Types.BIT_CMD_FLOW_1);
                aSubprogramData.tabMFC[0].Flow          = aData[Types.OFFSET_SEQ_FLOW_1_FLOW      + aOffset];
                aSubprogramData.tabMFC[0].MinFlow       = aData[Types.OFFSET_SEQ_FLOW_1_MIN_FLOW  + aOffset];
                aSubprogramData.tabMFC[0].MaxFlow       = aData[Types.OFFSET_SEQ_FLOW_1_MAX_FLOW  + aOffset];
                aSubprogramData.tabMFC[0].ShareGas      = aData[Types.OFFSET_SEQ_FLOW_1_SHARE     + aOffset];
                aSubprogramData.tabMFC[0].Devaition     = aData[Types.OFFSET_SEQ_FLOW_1_DEVIATION + aOffset];

                aSubprogramData.tabMFC[1].Active        = IsBitActive(aData[Types.OFFSET_SEQ_CMD  + aOffset], Types.BIT_CMD_FLOW_2);
                aSubprogramData.tabMFC[1].Flow          = aData[Types.OFFSET_SEQ_FLOW_2_FLOW + aOffset];
                aSubprogramData.tabMFC[1].MinFlow       = aData[Types.OFFSET_SEQ_FLOW_2_MIN_FLOW + aOffset];
                aSubprogramData.tabMFC[1].MaxFlow       = aData[Types.OFFSET_SEQ_FLOW_2_MAX_FLOW + aOffset];
                aSubprogramData.tabMFC[1].ShareGas      = aData[Types.OFFSET_SEQ_FLOW_2_SHARE + aOffset];
                aSubprogramData.tabMFC[1].Devaition     = aData[Types.OFFSET_SEQ_FLOW_2_DEVIATION + aOffset];

                aSubprogramData.tabMFC[2].Active        = IsBitActive(aData[Types.OFFSET_SEQ_CMD  + aOffset], Types.BIT_CMD_FLOW_3);
                aSubprogramData.tabMFC[2].Flow          = aData[Types.OFFSET_SEQ_FLOW_3_FLOW + aOffset];
                aSubprogramData.tabMFC[2].MinFlow       = aData[Types.OFFSET_SEQ_FLOW_3_MIN_FLOW + aOffset];
                aSubprogramData.tabMFC[2].MaxFlow       = aData[Types.OFFSET_SEQ_FLOW_3_MAX_FLOW + aOffset];
                aSubprogramData.tabMFC[2].ShareGas      = aData[Types.OFFSET_SEQ_FLOW_3_SHARE + aOffset];
                aSubprogramData.tabMFC[2].Devaition     = aData[Types.OFFSET_SEQ_FLOW_3_DEVIATION + aOffset];

                aSubprogramData.GasProces_Mode          = aData[Types.OFFSET_SEQ_GAS_MODE         + aOffset];
                aSubprogramData.GasProces_Setpoint      = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_GAS_SETPOINT      + aOffset));
                aSubprogramData.GasProces_MinDiffer     = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_GAS_DOWN_DIFFER   + aOffset));
                aSubprogramData.GasProces_MaxDiffer     = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_GAS_UP_DIFFER     + aOffset));
                aSubprogramData.GasProces_TimeTarget    = aData[Types.OFFSET_SEQ_GAS_TIME         + aOffset];

                aSubprogramData.Vaporaitor_CycleTime    = Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_FLOW_4_CYCLE_TIME + aOffset));
                aSubprogramData.Vaporaitor_Open         = (int)Types.ConvertDWORDToDouble(aData, (int)(Types.OFFSET_SEQ_FLOW_4_ON_TIME + aOffset));
                aSubprogramData.Vaporaitor_Dosing       = aData[Types.OFFSET_SEQ_DOSING + aOffset];

                aSubprogramData.Motor1_Command          = aData[Types.OFFSET_SEQ_MOTOR_1_CMD  + aOffset];
                aSubprogramData.Motor1_OperateTime      = aData[Types.OFFSET_SEQ_MOTOR_1_TIME + aOffset];
                aSubprogramData.Motor2_Command          = aData[Types.OFFSET_SEQ_MOTOR_2_CMD  + aOffset];
                aSubprogramData.Motor2_OperateTime      = aData[Types.OFFSET_SEQ_MOTOR_2_TIME + aOffset];

            }
            return aSubprogramData;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda sprawdza czy tablica z danymi jest wystaraczjacu duza do odczytu/zapisu danych pod konkretny indeks
         */ 
        private bool CheckCorectTabSize(int aDataLen)
        {
            bool aRes = false;
            if (aDataLen > Types.OFFSET_SEQ_PUMP_SP &&          aDataLen > Types.OFFSET_SEQ_PUMP_MAX_TIME &&    aDataLen > Types.OFFSET_SEQ_VENT_TIME &&        aDataLen > Types.OFFSET_SEQ_FLUSH_TIME &&
                aDataLen > Types.OFFSET_SEQ_HV_OPERATE &&       aDataLen > Types.OFFSET_SEQ_HV_SETPOINT &&      aDataLen > Types.OFFSET_SEQ_HV_TIME &&          aDataLen > Types.OFFSET_SEQ_FLOW_1_FLOW &&
                aDataLen > Types.OFFSET_SEQ_FLOW_1_MIN_FLOW &&  aDataLen > Types.OFFSET_SEQ_FLOW_1_MAX_FLOW &&  aDataLen > Types.OFFSET_SEQ_FLOW_1_SHARE &&     aDataLen > Types.OFFSET_SEQ_FLOW_1_DEVIATION &&
                aDataLen > Types.OFFSET_SEQ_FLOW_2_FLOW &&      aDataLen > Types.OFFSET_SEQ_FLOW_2_MIN_FLOW &&  aDataLen > Types.OFFSET_SEQ_FLOW_2_SHARE &&     aDataLen > Types.OFFSET_SEQ_FLOW_2_DEVIATION &&
                aDataLen > Types.OFFSET_SEQ_FLOW_2_MAX_FLOW &&  aDataLen > Types.OFFSET_SEQ_FLOW_3_FLOW &&      aDataLen > Types.OFFSET_SEQ_FLOW_3_MIN_FLOW &&  aDataLen > Types.OFFSET_SEQ_FLOW_3_MAX_FLOW &&
                aDataLen > Types.OFFSET_SEQ_FLOW_3_SHARE &&     aDataLen > Types.OFFSET_SEQ_FLOW_3_DEVIATION && aDataLen > Types.OFFSET_SEQ_FLOW_3_MIN_FLOW &&  aDataLen > Types.OFFSET_SEQ_FLOW_3_MAX_FLOW &&
                aDataLen > Types.OFFSET_SEQ_FLOW_3_SHARE &&     aDataLen > Types.OFFSET_SEQ_FLOW_3_DEVIATION && aDataLen > Types.OFFSET_SEQ_GAS_MODE &&         aDataLen > Types.OFFSET_SEQ_GAS_SETPOINT &&
                aDataLen > Types.OFFSET_SEQ_GAS_DOWN_DIFFER &&  aDataLen > Types.OFFSET_SEQ_GAS_UP_DIFFER &&    aDataLen > Types.OFFSET_SEQ_GAS_TIME &&         aDataLen > Types.OFFSET_SEQ_FLOW_4_CYCLE_TIME &&
                aDataLen > Types.OFFSET_SEQ_FLOW_4_ON_TIME)
                aRes = true;

            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda wykonuje procedure startu programu - wgrywa dane prgoamu do PLC i uruchamia program
         */ 
        public ItemLogger StartProgram()
        {
            ItemLogger aErr = new ItemLogger();
            //przygotuj dane do wgrania do PLC
            int[] aDataControl  = new int[1];

            if (plc != null && Factory.Hpt1000 != null)
            {
                //Sprawdz czy jest wlaczona akiwzyja danych jezeli nie i jest wlaoczna opcia informowania o tym usera to wyswietl komunikat
                DialogResult res = DialogResult.Yes;
                if (!Factory.Hpt1000.EnabledAcq && Factory.Hpt1000.AskAcq)
                    res = MessageBox.Show("Acquisition of data is disabled. Are you sure you want to continue without acquisition of data process ?", "Process start", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    //Wgraj parametry segmentow do PLC   
                    aErr = WriteProgramToPLC();
                    //Utworz struktury danych do przechowyania aktualnych parametrow programu wczytanychy do PLC
                    //CreateActualSubprogram();
                    //uruchom program
                    aDataControl[0] = (int)Types.ControlProgram.Start;
                    int aCode = 0;
                    if (!aErr.IsError())
                        aCode = plc.WriteWords(Types.ADDR_CONTROL_PROGRAM, 1, aDataControl);

                    if (aCode != 0)
                        aErr.SetErrorMXComponents(Types.EventType.START_PROGRAM, aCode);
                }
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zatrzumuje wykonywanie pgroamu przez PLC
        */
        public ItemLogger StopProgram()
        {
            ItemLogger aErr = new ItemLogger();

            //przygotuj dane do wgrania do PLC
            int[] aDataControl = new int[1];

            if (plc != null)
            {
                aDataControl[0] = (int)Types.ControlProgram.Stop;
                int aCode       = plc.WriteWords(Types.ADDR_CONTROL_PROGRAM, 1, aDataControl);
                aErr.SetErrorMXComponents(Types.EventType.STOP_PROGRAM, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda przesyla do PLC parametry subprogramow z kotrych sklada sie dany program
         */
        private ItemLogger WriteProgramToPLC()
        {
            ItemLogger aErr = new ItemLogger();

            int[] aDataID    = new int[1];
            int[] aData      = new int[Types.MAX_SEGMENTS * Types.SEGMENT_SIZE];
            int aSizeData = 0;
            
            aDataID[0] = (int)id;
            //Pobierz z kolejnych subprogramow parametry procesow
            int[] aSeqData = new int[Types.SEGMENT_SIZE];
            for (int i = 0; i < subPrograms.Count; i++)
            {
                aSeqData = subPrograms[i].GetPLCSegmentData();
                for (int j = 0; j < Types.SEGMENT_SIZE; j++)
                {
                    if (aData.Length > i * Types.SEGMENT_SIZE + j)
                        aData[i * Types.SEGMENT_SIZE + j] = aSeqData[j];
                }
                int aStatusIndex = i * Types.SEGMENT_SIZE + Types.OFFSET_SEQ_STATUS;
                if (aData.Length > aStatusIndex)
                    aData[aStatusIndex] = (int)Types.StatusProgram.Stop;
            }
            //uzupelnij subprogramy o ostatni segment END ktory dawany jest z automatu
            if (aData.Length > (subPrograms.Count * Types.SEGMENT_SIZE + Types.OFFSET_SEQ_CMD))
                aData[subPrograms.Count * Types.SEGMENT_SIZE + Types.OFFSET_SEQ_CMD] = (int)Math.Pow(2,Types.BIT_CMD_END); //ustaw komende END
            //wgraj dane programu do PLC 
            aSizeData = subPrograms.Count * Types.SEGMENT_SIZE + Types.OFFSET_SEQ_CMD + 1;
            //Zapisz ID programu
            int aCode = plc.WriteWords(Types.ADDR_PRG_ID, 1, aDataID);
            if (aCode == 0)
                aCode = plc.WriteWords(Types.ADDR_START_BUFFER_PROGRAM, aSizeData, aData);

            aErr.SetErrorMXComponents(Types.EventType.WRITE_PROGRAM, aCode);

            return aErr;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Funkcja odczytaje z plc dane na temat wszystkich subprogramow oraz tworzy ich struktrure oraz zaktualizuje dane
        */
        public void ReadProgramsData()
        {
            int aFinishCount = 0;
            int aLoop = 0;

            if (plc != null)
            {
                int[] aData = new int[1];
                //Wymus na PLC przeliczenie na nowo liczby segmentow i czekaj az to zrobi. Robie tak aby miec pewnosc ze dane w PLC zostaly poprawnie zaktualizowane na na podstawie przed chwila wgranego programu   
                plc.SetDevice(Types.ADDR_REQ_COUNT_SEGMENT, 1);

                while (aFinishCount == 0)
                {
                    plc.GetDevice(Types.ADDR_FINISH_COUNT_SEGMENT, out aFinishCount);
                    //czekaj max 1 + czas odczytu danych z plc dla jednej wartosci * 10
                    if (aLoop > 10)
                        break;
                    System.Threading.Thread.Sleep(100);
                    aLoop++;
                }
                //Odczytaj liczbe segmentow z ktorych sklada sie program wgrany do PLC ale najpierw wymus na PLC przeliczenie na nowow liczby segmentow i czekaj az to zrobi
                if (mainForm != null && mainForm.Created)
                    mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_PRG_SEQ_COUNTS, 1, aData);

                countSubprogramsPLC = aData[0];
                //Odczytaj parametry subprogramow
                int[] aDataSeq = new int[countSubprogramsPLC * Types.SEGMENT_SIZE];
                if (mainForm != null && mainForm.Created)
                    mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_START_BUFFER_PROGRAM, aDataSeq.Length, aDataSeq);
                //Utworz strukture subprogramow taka sama jaka jest po stronie PLC
                for (int i = 0; i < countSubprogramsPLC; i++)
                {
                    uint aIdSubprogramInPLC = (uint)aDataSeq[i * Types.SEGMENT_SIZE + Types.OFFSET_SEQ_ID];
                    //Sprawdz czy ID subprogramu w tej kolejnosci jest takie samo jak w PLC
                    if (subPrograms.Count > i)
                    {
                        //Subprogram po stronei PC jest rozny od PLC wiec ustawiam mu ID pobrane z PLC i zmioaniem nazwe
                        if (subPrograms[i] != null && subPrograms[i].ID != aIdSubprogramInPLC)
                        {
                            subPrograms[i].ID = aIdSubprogramInPLC;
                            subPrograms[i].SetName("Subprogram in PLC");
                        }

                    }
                    else//Subprogram nie istniekje w PC to tworze nowy
                    {
                        Subprogram subProgram = new Subprogram();
                        subProgram.ID = aIdSubprogramInPLC;
                        subProgram.OrdinalNumber = i + 1;
                        subProgram.SetName("Subprogram in PLC");
                        AddSubprogram(subProgram);

                    }
                }
                // Pobierz dane na temat subprogramow z PLC
                SubprogramData subProgramData;
                uint index = 0;
                foreach (Subprogram subPr in subPrograms)
                {
                    //Pobierz dane z PLC
                    subProgramData = GetSegmentData(aDataSeq, index++);
                    subPr.UpdateData(subProgramData);
                }
                //CreateActualSubprogram();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //Funkcja tworzy kliste akutalnych subprogramowa na bazie tych co zostalyu wgrane do PLC aby umozliwic prezentacje danych odczytanych z PLC. W innym przypadku parametry subprogramow moglyby zostac nadpisane
        /*   public void CreateActualSubprogram()
           {
               lock (subProgramsActual)
               {
                   subProgramsActual.Clear();
                   foreach (Subprogram subProgram in subPrograms)
                   {
                       Subprogram aSubProgram = new Subprogram(subProgram.ID);//(subProgram);
                       subProgramsActual.Add(aSubProgram);
                   }
               }
           }
           */
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zwraca informacje czy dany program jest wykonywany przez PLC
        */
        public bool IsRun()
        {
            bool aProgramRun = false;

            if (status == Types.StatusProgram.Suspended || status == Types.StatusProgram.Run)
                aProgramRun = true;

            return aProgramRun;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Funkcja zwraca aktualnie wykonywany subprogram
        */
        public Subprogram GetActualSubprogram()
        {
            Subprogram aActualSubprogram = null;

            foreach (Subprogram aSubprogram in subPrograms)
            {
                if (aSubprogram.ID == actualSubprogramId)
                    aActualSubprogram = aSubprogram;
            }
            return aActualSubprogram;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Utworz subprogram w bazie danych a nastepnie jako obiekt programu i  przypisz subprogramowi unikalne ID otrzymane z bazy danych
        */
        public void NewSubprogram()
        {
            if (dataBase != null)
            {
                //Utworz obiekt subprogramu
                Subprogram aSubprogram = new Subprogram();
                aSubprogram.OrdinalNumber = GetOrdinalSubprogramNumber();
                aSubprogram.SetName(GetUniqueSubprogramName());
                //Zapisz subprogram w bazie danych
                int aId = dataBase.AddSubProgram(aSubprogram, (Int32)id);
                //Jezeli poprawnie dodano obiekt do bazy danych to dodaj go do lsity subprogramow
                if (aId > 0)
                {
                    aSubprogram.ID = (uint)aId;
                    AddSubprogram(aSubprogram);
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca unikalna nazwe subprogramu
         */
        private string GetUniqueSubprogramName()
        {
            string aName;
            int aNo = 1;

            aName = "Subprogram " + aNo.ToString();
            for (int i = 0; i < subPrograms.Count; i++)
            {
                Subprogram subPr = subPrograms[i];
                if (subPr.GetName() == aName)
                {
                    aName = "Subprogram " + (++aNo).ToString();
                    i = 0;
                }
            }

            return aName;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda dodaje do listy subprogramow nowy subprma oraz infomrauje obserwator ze lista zostala zmieniona
        */
        public void AddSubprogram(Subprogram subProgram)
        {
            subPrograms.Add(subProgram);        
            //Poinformuj moich obserwatorow aby odswiezyly sobie informacje na temat programow
            if (refreshProgram != null)
                refreshProgram();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda usuwa dany subprogram
         */
        public bool RemoveSubprogram(Subprogram aSubprogram)
        {
            bool aRes = false;
            if (dataBase != null)
            {
                //Usub subprogram z  bazy danych. Jezeli to sie uda to usun takzez z lokalnej lsity
                if(dataBase.RemoveSubprogram(aSubprogram) == 0)
                    aRes = subPrograms.Remove(aSubprogram);

                //Poinformuj moich obserwatorow aby odswiezyly sobie informacje na temat programow
                if (refreshProgram != null)
                    refreshProgram();
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Ustaw kolejnosc subprogramow na liscie zgdonie z ich numerami porzadkowymi - posortuj rosnaco
         */
        public void SetOrderSubprogrms()
        {
            //sortowanie babelkowe
            for (int j = 0; j < subPrograms.Count; j++)
            {
                for (int i = 0; i < subPrograms.Count - 1; i++)
                {
                    //Zamien miejscami jezeli jestem wiekszy od nastepnika
                    if (subPrograms[i].OrdinalNumber > subPrograms[i + 1].OrdinalNumber)
                    {
                        Subprogram tmp = subPrograms[i];
                        subPrograms[i] = subPrograms[i + 1];
                        subPrograms[i + 1] = tmp;
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //Metoda ma za zadanie podniesie do gory danego subprogramu na liscie subproamow z punktu widzenia procesu
        public void UpSubprogram(Subprogram subprogram)
        {
            for(int i = 1; i < subPrograms.Count; i++)
            {
                if(subprogram == subPrograms[i] )
                {
                    //zamien elementy na liscie
                    Subprogram tmp      = subPrograms[i - 1];
                    subPrograms[i - 1]  = subprogram;
                    subPrograms[i]      = tmp;
                    //zamien takze numery porzadkowe
                    int ordNrTmp = subPrograms[i - 1].OrdinalNumber;
                    subPrograms[i - 1].OrdinalNumber = subPrograms[i].OrdinalNumber;
                    subPrograms[i].OrdinalNumber = ordNrTmp;
                    //ustaw flage ze pozychje zostaly zmienione                    
                    subprogramChangedPlace = true;
                    break;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //Metoda ma za zadanie ustawinie danego subprogramu w dol na liscie subprogramow z punktu widzenia procesu
        public void DownSubprogram(Subprogram subprogram)
        {
            for (int i = 0; i < subPrograms.Count - 1; i++)
            {
                if (subprogram == subPrograms[i])
                {
                    //zamien elementy na liscie
                    Subprogram tmp      = subPrograms[i + 1];
                    subPrograms[i + 1]  = subprogram;
                    subPrograms[i]      = tmp;
                    //zamien takze numery porzadkowe
                    int ordNrTmp = subPrograms[i + 1].OrdinalNumber;
                    subPrograms[i + 1].OrdinalNumber = subPrograms[i].OrdinalNumber;
                    subPrograms[i].OrdinalNumber = ordNrTmp;
                    //ustaw flage ze pozychje zostaly zmienione                    
                    subprogramChangedPlace = true;
                    break;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zwrocenie kolejnego unikatowego numeru porzadkowego dla danego subprgroamy. Nie moze on byc mniejszy niz najwiekszy z dotychczasowych 
         */
        public int GetOrdinalSubprogramNumber()
        {
            int ordinalNr = 0;
            bool nrExist  = true;

            while(nrExist)
            {
                ordinalNr++;
                nrExist = false;
                //Sprawdz czy taki juz istnieje
                foreach (Subprogram subPr in subPrograms)
                {
                    if(subPr.OrdinalNumber == ordinalNr)
                    {
                        nrExist = true;
                        break;
                    }
                }
                //Sprawdz czy jest on najwiekszy
                foreach (Subprogram subPr in subPrograms)
                {
                    if (subPr.OrdinalNumber > ordinalNr)
                    {
                        nrExist = true;
                        break;
                    }
                }
            }
            return ordinalNr;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /*
        private uint GetUnigueSubprogramID()
        {
            uint aId = 0;
            bool aExist = true;

            while (aExist)
            {
                aId++;
                aExist = false;
                foreach (Subprogram subProgram in subPrograms)
                {
                    if (subProgram.ID == aId)
                        aExist = true;
                }
            }
            return aId;
        }
        */
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda pomocnicza sprawdza czy dany subprogram jest aktywny
        */
        private bool IsBitActive(int aData, int aBitNo)
        {
            bool aRes = false;

            if ((aData & (int)Math.Pow(2, aBitNo)) != 0)
                aRes = true;

            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Zwroc subprogramy konfigurowane przez uzytkownika
        */
        public List<Subprogram> GetSubprograms()
        {
            return subPrograms;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //Zwroc subprogramy ktrorych parametry zostaly odczytane na podstawie pamieci PLC
        /* public List<Subprogram> GetSubprogramsPLC()
         {
             return subProgramsActual;
         }
         */
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia ID progamu 
         */
        public void SetID(uint aId)
        {
            id = aId;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zwraca ID prgroamu
        */
        public uint GetID()
        {
            return id;
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
                res = Factory.DataBase.ModifyProgram(this);

                changes = false;
                changesNotSave = false;
                subprogramChangedPlace = false;
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
            changes = false;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda ustawian nazwe prgoramu
        */
        public void SetName(string aName,bool tmpValue = false)
        {
            if (tmpValue && name != aName)
                changes = true;

            nameTmp = aName;
            if (!tmpValue)
                name = aName;

            if (refreshProgram != null)
                refreshProgram();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zwraca nazwe programu
        */
        public string GetName()
        {
            return name;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda ustawia opis programu
        */
        public void SetDescription(string aDescription)
        {
            description = aDescription;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zwraca opis programu
        */
        public string GetDescription()
        {
            return description;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zwraca dodatkowe dane na temat procesu
        */
        public void SetProcessInformation(string processInfo)
        {
           this.processInformation = processInfo;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda zwraca dodatkowe dane na temat procesu
        */
        public string GetProcessInformation()
        {
            return this.processInformation;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda ustawia referencje obiektu dajacego mozliwosci komunikacji z pLC
        */
        public void SetPtrPLC(PLC aPLC)
        {
            plc = aPLC;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda przeciarza funkcej ToString zwracajac nazwe programu
        */
        public override string ToString()
        {
            return name;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Metoda aktualizuje liste obserwatorow
        */
        public static void AddToRefreshList(RefreshProgram aRefresh)
        {
            refreshProgram += aRefresh;
            Subprogram.AddToRefreshList(refreshProgram);
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy nie wystapily jakies zmiany w programach ktore nie zostaly zapisane
         */ 
         public bool IsAnyChangesNotSave()
        {
            bool res = changesNotSave || subprogramChangedPlace;

            foreach (Subprogram subpr in subPrograms)
                res |= subpr.IsAnyChangesNotSave();

            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy prgoram zawiera subprogram o podanej nazwie
         */ 
         public bool ContainsSubprgoram(string name)
        {
            bool res = false;

            foreach (Subprogram subPr in subPrograms)
                if (subPr.GetName() == name)
                    res = true;

            return res;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest zwrocenie w wersji tablicy byte podstawowych parametrow programu ktore sa wymagane do zapisu w bazie danych jako dokumentacja wykonywanego procesu
         */ 
        public byte[] GetByte()
        {
            BinaryFormatter binary      = new BinaryFormatter();
            MemoryStream    memory      = new MemoryStream();
            ProcesParameter programPara = new ProcesParameter(this);
            //Dodaj info o zalogowanym userze do parametrow
            if(ApplicationData.LoggedUser != null)
                programPara.User = ApplicationData.LoggedUser.Name + " " + ApplicationData.LoggedUser.Surname;

            //WYkonaj serializacje czyli zamiane dnaych na caig bajtow
            binary.Serialize(memory, programPara);
                                        
            return memory.ToArray();
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy w aktualnie wykonytwanym programie znajduje sie proces plasmy
         */ 
         public bool IsPlasmaProcessExist()
        {
            bool ARes = false;

            foreach(Subprogram subProgram in subPrograms )
            {
                if(subProgram.PlasmaProces.Active)
                {
                    ARes = true;
                    break;
                }
            }
            return ARes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
    }
}
