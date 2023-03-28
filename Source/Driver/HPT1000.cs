using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HPT1000.Source.Chamber;
using HPT1000.Source.Program;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HPT1000.Source.Driver
{
    /// <summary>
    /// Klasa driver opisujaca zachowanie sie komory. Daje mozliwosc do ustawienia programow autoamtycznych badz sterowania recznego
    /// Z punktu widzenia sterowania komorą podstawowa klasa.
    /// </summary>
    public class HPT1000 : IDisposable
    {
        GUI.MainForm mainForm = null;
        private DB dataBase = null;                        ///< Referencja obiektu bazy danych przekazywana do obiektow powiazanych z HPT1000
        private PLC plc = null;// new PLC_Mitsubishi();        ///< Utworzenie obiektu wykorzystywanego do komunikacji sie ze sterownikiem PLC
        private Chamber.Chamber chamber = new Chamber.Chamber();       ///< Utworzenie obiwktu komory wykorzystywanego do sterowania manualnego komponentami komory
        private List<Program.Program> programs = new List<Program.Program>(); ///< Utworzenie listy wszystkich programow zapisanych w aplikacji
        private GasTypes gasTypes = new GasTypes();              ///< Utworzenie listy typow gazow ktore moga zostac podpiete pod dana przeplywke MFC

        private Types.DriverStatus status    = Types.DriverStatus.Unknown;       ///< Zmienna opisuje status komory {OK, Warrning,Error,DummyMode,NoComm}
        private Types.DriverStatus statusPLC = Types.DriverStatus.Unknown;       ///< Zmienna opisuje status komory {OK, Warrning,Error,DummyMode,NoComm}

        private bool flagError = false;                            ///< Flaga okresla ze sterownik PLC zglasza blad i nalezy odczyc liste bledow

        private static Types.Mode mode = Types.Mode.None;                       ///< Zmienna okresla typ sterowania komora {Automatic - programy ; Manual - reczne nastawy}

        private ThreadStart funThr = null;                         ///< Funkcja watku sluzacego do odczytu danych z PLC
        private Thread threadReadData = null;                         ///<Obiekt watku sluzacego do odczytu danych z PLC

        bool connectionPLC = false;                        ///< Flaga okresla stan komuniikacji ze sterownikiem PLC
        bool oldConnectionPLC = false;                        ///< Flaga okresla stan komuniikacji ze sterownikiem PLC w poprzednim przebiegu petli watku

        private static RefreshProgram refreshProgram = null;                    ///< Referencja na delegaty ktore chca byc odsiwezane na wypadek zmian zachodzacych w komorze

        public static Types.Language LanguageApp = Types.Language.English;      ///<zm globalna określająca jezyk aplikacji

        List<DataBaseData> dataBaseData = new List<DataBaseData>();    ///<Lista z wartoscami danych ktore nalezy zapisac do bazy danych
        ProgramParameter parameterAcq = new ProgramParameter();      ///<Zmienna jest wykorzystywana do przechowyania w bazie danych informacji na temat parametorw urzadzenie dotyczace archiwizacji danych

        bool procesRunning = false; // /< Flaga okresla stan wykonywania sie procesu

        int version_programPLC = 0;
        int subVersion_programPLC = 0;

        DateTime chartWindowTime = new DateTime();

        //-----Zestaw parametrow okreslajych akwizycj danych w bazie danych
        long lastTimeSaveDataInDB = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;    //Counter jest wykorzystywany do odmierzania czasu zapidu danych w bazie danych
        long lastTimeCheckConnectPLC = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;    //Counter jest wykorzystywany do odmierzania czasu zapidu danych w bazie danych
        int frqSaveDataInDB = 30;       ///<Czestotliwość okresla jak czesto powinismy zapisywac dane w bazie. Dane sa zbierane a nastepnie zbiorczo zapisywane [s]
        double pressureAcq = 1;        ///<Poziom prozni ponizej ktorej dane sa zapisywane w bazie danych
        bool activeCheckPressureAcq = true;     ///<Flaga okresla czy podczas zapisu danych w bazie danych mam sprawdzac warunek na poziom cisnienia w komorze
        bool acqDuringOnlyProcess = true;//false;    ///<Flaga okresla czy mam zapisywac dane tylko podczas trwania prcesu 
        bool acqAllTime = false;//true;     ///<Flaga okresla czy mam zapisywac dane caly czas
        bool lastConditionsSaveData = false;    ///< flaga okresla czy poprzednio warunki do zapisu danych byly spelnione
        bool firstRunNewSesion = false;    ///<Flaga okresla ze wykonywana jest pierwszy przebieg petli po utworzeniu nowej sesji danych
        bool enabledAcq = true;     ///<Flaga okresla czy jesrt wlaczona akwizycja danych

        string currentNameProgram = "None";      ///<
        bool startNewProcess = false;       ///< Flaga wykorzsytywana do tworzenia nowej sesji podczas zapisu danych w bazie danych podczas uruchomienia procesu
        bool stopProcess = false;       ///< Flaga wykorzsytywana do tworzenia nowej sesji podczas zapisu danych w bazie danych gdy ostatnio byl zatrzymany
        bool createNewSesion = false;

        int  actualSesionID = 0;        ///< ID aktualnej sesji danych (ID rekordu w tabeli sesion ktora jest powiazana z akutalnie wykonywanym procesem)
        bool askAcq         = true;     ///< Flaga okresl czy user chce aby zadawac pytanie a braku ajwizycji danych oidczas startu procesu

        //----------GRUPA SETEROW
        //-----------------------------------------------------------------------------------------
        public DateTime ChartWindowTime
        {
            set
            {
                chartWindowTime = value;
                SaveData();
            }
            get { return chartWindowTime; }
        }
        //-----------------------------------------------------------------------------------------
        public int FrequencySaveDataInDB
        {
            set { frqSaveDataInDB = value; }
            get { return frqSaveDataInDB; }
        }
        //-----------------------------------------------------------------------------------------
        public double PressureAcq
        {
            set { pressureAcq = value; }
            get { return pressureAcq; }
        }
        //-----------------------------------------------------------------------------------------
        public bool AskAcq
        {
            set { askAcq = value; }
            get { return askAcq; }
        }//-----------------------------------------------------------------------------------------
        public bool ActiveCheckPressureAcq
        {
            set { activeCheckPressureAcq = value; }
            get { return activeCheckPressureAcq; }
        }
        //-----------------------------------------------------------------------------------------
        public bool AcqDuringOnlyProcess
        {
           // set { acqDuringOnlyProcess = value; }
            get { return acqDuringOnlyProcess; }
        }
        //-----------------------------------------------------------------------------------------
        public bool AcqAllTime
        {
         //   set { acqAllTime = value; }
            get { return acqAllTime; }
        }
        //-----------------------------------------------------------------------------------------
        public bool CoonectionPLC
        {
            get { return connectionPLC; }
        }
        //-----------------------------------------------------------------------------------------
        public bool EnabledAcq
        {
            set { enabledAcq = value; }
            get { return enabledAcq; }
        }
        //-----------------------------------------------------------------------------------------
        public DB DataBase
        {
            set
            {
                dataBase = value;
                gasTypes.DataBase = value;
                if (chamber != null && chamber.GetObject(Types.TypeObject.FM) != null)
                    ((MFC)chamber.GetObject(Types.TypeObject.FM)).SetDataBase(dataBase);
                if (GetMaintanence() != null)
                    GetMaintanence().DataBase = dataBase;
                if (GetMotor(1) != null)
                    GetMotor(1).DataBase = dataBase;

                if (plc != null)
                    plc.DataBase = dataBase;
            }
        }
        //-----------------------------------------------------------------------------------------
        public static Types.Mode Mode
        {
            get { return mode; }
        }
        //-----------------------------------------------------------------------------------------
        public Chamber.Chamber Chamber
        {
            get { return chamber; }
        }
        //-----------------------------------------------------------------------------------------
        public int VersionProgramPLC
        {
            get { return version_programPLC; }
        }
        //-----------------------------------------------------------------------------------------
        public int SubversionProgramPLC
        {
            get { return subVersion_programPLC; }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Konstruktor kalsy
         */
        public HPT1000(PLC plcPtr, GUI.MainForm mainFormPtr)
        {
            plc = plcPtr;
            mainForm = mainFormPtr;

            chamber.SetPtrPLC(plc);
            foreach (Program.Program pr in programs)
                pr.SetPtrPLC(plc);

            funThr = new ThreadStart(Run);
            threadReadData = new Thread(funThr);
            threadReadData.Name = "Thread_Read_Data_PLC";
            threadReadData.Start();

            parameterAcq.Name = "ParameterAcq";

            //Ustaw domyslna wartosc okna wykresu jako 30 s
            chartWindowTime = chartWindowTime.AddSeconds(30);
        }
        //-----------------------------------------------------------------------------------------
        ~HPT1000()
        {
            Dispose();
        }
        //-----------------------------------------------------------------------------------------
        public void Dispose()
        {
            if (threadReadData.IsAlive)
                threadReadData.Abort();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zaladowanie danych z bazy daych na temat: programoów, zarejestrownych urzadzen, danych oraz typow gazow
         */
        public void LoadDataFromDB()
        {
            //pobierz listę programó zapisanych w bazie danych
            if (dataBase != null)
            {
                ReadProgramsFromDB();
                RegisterDevice();
                LoadData();
                gasTypes.LoadGasType();
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcaj watku - GLOWANA FUNKCJA SLUZACA DO KOMUNIKOWANIA SIE Z PLC
         */
        private void Run()
        {
            int aRes = 0 ;
            int[] aData = new int[Types.LENGHT_STATUS_DATA];
            int[] aStatusPLC = new int[2];
            while (true)
            {
                if (plc != null && mainForm != null && mainForm.Created && mainForm.DelegateReadPLC != null)
                {
                    //Odczytaj status PLC - aby to poprawnie zrobic musze odczytac rejestr specjalny PLC.
                    aRes = (int)mainForm.Invoke(mainForm.DelegateReadPLC, "SD201", 1, aStatusPLC);
                    //Odczytaj dane urzadzen
                    aRes = (int)mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_START_STATUS_CHAMBER, Types.LENGHT_STATUS_DATA,aData);
                    //aktualizuj status urzadzenia HPT1000
                    if (aData.Length > Types.OFFSET_OCCURED_ERROR) flagError = Convert.ToBoolean(aData[Types.OFFSET_OCCURED_ERROR]);
                    if (aData.Length > Types.OFFSET_MODE) mode = (Types.Mode)aData[Types.OFFSET_MODE];

                    //Ustaw status urzadzenia gdy dane zostaly poprawnie odebrane
                    statusPLC = Types.DriverStatus.Warning;
                    if ((aStatusPLC[0] & 0x03) == 1) //Dioda RUN sie swieci
                        statusPLC = Types.DriverStatus.OK;
                    if ((aStatusPLC[0] & 0x0C) != 0)    //Dioda led mruga badz swiecie
                        statusPLC = Types.DriverStatus.Error;
                
                    //aktualizuju dane komponentow komory
                    chamber.UpdateData(aData);
                        
                    //aktualizuj dane na temat aktualnie wykonywanego subprogramu. Poniewaz dane programow sa odczytywane razem z danymi komponenotw musze je przesunac o dany offset
                    int[] aDataProgram = new int[Types.SIZE_PRG_DATA];
                    CopyData(aData, aDataProgram);
                    foreach (Program.Program pr in programs)
                        pr.UpdateActualSubprogramData(aDataProgram,aData[Types.OFFSET_INTEGRAL_SEQ_DATA]);
                    
                    //Wykonaj operacje po nawiazaniu polaczenia z PLC
                    if (connectionPLC && !oldConnectionPLC)
                        FirstRun();
                    oldConnectionPLC = connectionPLC;

                    //Sprawdz czy jest komunikacja. Jezeli nie ma to sprobuj nawiazac
                    CheckConnection(aRes);
                    //Odczytaj bledy z PLC
                    if (flagError)
                        ReadErrorsFromPLC();

                    //Wywolaj funkcje odpowiedzialna za wykonywanie akwizycji danych
                    MakeAcquisitionData();

                    //Ustaw automatycznie tryb autoamtic gdy zalogowany user jest operatorem
                    if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Operator && CoonectionPLC && GetMode() != Types.Mode.Automatic)
                        SetMode(Types.Mode.Automatic);//ustawiam z automaytu tryb automatyczny

                    if (mainForm.DelegateUpdateChartData != null)
                         mainForm.Invoke(mainForm.DelegateUpdateChartData);

                }
                //Odczytuj dane co 0.2 s
                Thread.Sleep(60); 
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja organizuje zapis danych do bazy. Jej zadanie polega na sprawdzeniu warunkow zapisu, zbieraniu danych oraz zapisie danych w bazie
         */
        private void MakeAcquisitionData()
        {
            //Ustaw zmiwnne takie jak: nazwe aktualnie wykonywanego procesu oraz flagi Start/Stop procesu                                                                
            CheckProgramWorking();
            //Sprawdz czy sa warunki do zapisu danych w bazie. prawdz takze czy nie ma czasem nowej sesji danych wywolanej poprzez Start/Stop procesu . Podczas rozpoczecia nowej akwizycji wyczysc liste danych do zapisu
            if (ConditionsSaveData())
            {
                //Sprawdz czy nie ma nowej sesji. Nowa sesja jest zawsze gdy jest: nowa seria danych,start programu , stop programu
                if (/*!lastConditionsSaveData ||*/ createNewSesion /*|| stopProcess*/)
                {
                    if (dataBase != null && ApplicationData.LastLoadProgramToPLC != null)
                    {
                        actualSesionID = dataBase.StartSesion(currentNameProgram, ApplicationData.LastLoadProgramToPLC.GetByte()); //Rozpocznij nowa sesje danych poprzez wpis w bazie i kasowanie aktualnej listy parametrow
                        createNewSesion = false;
                    }
                }
                //Wyczysc liste parametrow do zapisu w bazie poniewaz rozpoczynam nowa serie danych
                if (!lastConditionsSaveData)
                    dataBaseData.Clear();
             
                //Zbieraj parametry ktore zostana zapisane w bazie danych w odpowiednim czasie
                ColetctDataToSaveInDB();
            }
            //Sprawcz czy nadszedl czas na zpisa danych do bazy danych. Po rozpoczciu nowej sesji badz zakonczeniu procesu dane sa zapisywane odrazu ale tylko przez pierwsza iteracje
            if (Math.Abs(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - lastTimeSaveDataInDB) >= frqSaveDataInDB * 1000 || firstRunNewSesion || stopProcess)
                SaveDataInDataBase();

            firstRunNewSesion       = false;
            lastConditionsSaveData  = ConditionsSaveData();
        }   
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie sprawdzenie czy sa spelnione warunki do zapisu danych w bazie danych
         */
        private bool ConditionsSaveData()
        {
            bool aRes = false;
            double aPressure = 0;
            bool aProcessActive = false;

            //Sprawdz czy nie wykonuje sie jakis program.
            foreach (Program.Program program in programs)
            {
                if (program.IsRun())
                    aProcessActive = true;
            }
            //Odczytaj wartosc aktualnej prozni
            if (chamber != null && chamber.GetObject(Types.TypeObject.PC) != null)
                aPressure = ((PressureControl)chamber.GetObject(Types.TypeObject.PC)).GetPressure();

            //Sprawdz czy sa spelnione warunki aby zapisac dane w bazie danych
            //Warunki to proznia jezeli opcja aktywna oraz wykonywany proces jezeli opcja aktywna
            if (connectionPLC && enabledAcq && (!activeCheckPressureAcq || (activeCheckPressureAcq && aPressure <= pressureAcq)) && (acqAllTime || (acqDuringOnlyProcess && aProcessActive)))
                aRes = true;

            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie zapisanie danych w bazie danych 
         */
        private void SaveDataInDataBase()
        {
            //Pobierz wyszstkie elementy do zapisu i zapisany element z listy usun
            while (dataBaseData.Count > 0)
            {
                DataBaseData data = dataBaseData[0]; //pobierz pierwszy element z listy do zapisu do bazy danych
                dataBaseData.RemoveAt(0);            //usun pobrany elemnt z listy
                if (dataBase != null)
                {
                    int aRes = dataBase.AddData(data);

                }
            }
            lastTimeSaveDataInDB = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie zbieranie danych i wprowadzanie ich do listy parametrow ktore zostana zapisane w bazie. Zbieranie odbywa sie zgodnie z indywiduaknymi preferenacjami parametru
         */
        private void ColetctDataToSaveInDB()
        {
            //Odczytaj dane ze wszystkich zarejestrowanych urzadzen i ich parametrow
            foreach (Device device in chamber.GetObjects())
            {
                if (device.ID_DB > 0)
                {
                    foreach (Parameter para in device.GetParameters())
                    {
                        if (para.ID > 0 && para.ValuePtr != null)
                        {
                            //Sprawdz czy sa spelnione warunki aby parametr dodac do listy tych kotre zostana zapisane do bazy danych. 
                            //Pod uwage biore roznice pomiedzy wartoscia aktualna a ostatnia oraz ostatni czas wprowadzenia parametru do lsity.
                            if ((startNewProcess || para.IsDifferenceValue() || para.IsTimeSavePara() || para.IsMixed()) && para.EnabledAcq)
                            {
                                DataBaseData data = new DataBaseData();

                                data.ID_Para = para.ID;
                                data.Value = para.ValuePtr.Value_;
                                data.ValuePtr = para.ValuePtr;
                                data.Unit = para.Unit;
                                data.Date = DateTime.Now;
                                data.ProcesID = actualSesionID;
                                dataBaseData.Add(data);
                                //Aktualizuj wartosc aktulna ktora zostanie zapisana do bazy
                                para.ValuePtr.LastValueDB = data.Value;
                            }
                        }
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Podaj mi nazwe aktualnie wykonywanego programu oraz ustaw flagi start/stop program
         */
        private void CheckProgramWorking()
        {
            bool someProgramRun = false;
            string processName = "None";
            //Sprawdz czy nie wykonuje sie jakis program.
            foreach (Program.Program program in programs)
            {
                if (program.IsRun())
                {
                    processName = program.GetName();
                    someProgramRun = true;
                }
            }
            //Ustaw flage zakonczenia procesu. Jezeli proces byl uruchomiony ale juz nie jest to znaczy ze zostal zakoncozny
            stopProcess = false;
            if (!someProgramRun && procesRunning)
            {
                stopProcess = true;
                createNewSesion = false;
            }
            //Ustaw flage startu nowego procesu
            startNewProcess = false;
            if (processName != currentNameProgram && someProgramRun)
            {
                startNewProcess = true;
                createNewSesion = true;
            }
            //Ustaw nazwe aktualnie wykonywanego procesu
            currentNameProgram = processName;
            //Ustaw stan czy proces sie alkutanie wykonuje
            procesRunning = someProgramRun;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda sluzy do inicjalizacji operaji wymaganych do przeprowadzenia tylko podczas pierwszego uruchomnienia obiektu/nawiazania komunikacji z PLC
          */
        private void FirstRun()
        {
            //Odczytaj z automatu settingsy z PLC po nawiazaniu polaczenia
            UpdateSettings();
            //Odczytaj z PLC parametry programu aktualnie wgranego
            ReadProgramFromPLC();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja odczytuje parametry programow zapisane w bazie danych. Do programow sa takze odczytywane parametry subprogramow
         */
        private void ReadProgramsFromDB()
        {
            if (dataBase != null)
            {
                //Odczytaj liste programow z bazy danych
                foreach (Program.Program pr in dataBase.GetPrograms())
                {
                    //Wypelnij subprogramy parametrmi procesow
                    if (pr != null)
                        dataBase.FillProcessParametersOfSubprograms(pr.GetSubprograms());
                    programs.Add(pr);
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Zarejestruj urzadzenia w bazie danych powolane w komorze
         */
        private void RegisterDevice()
        {
            if (chamber != null && dataBase != null)
            {
                foreach (Device device in chamber.GetObjects())
                {
                    dataBase.RegisterDevice(device);
                    dataBase.RegisterParameters(device.ID_DB, device.GetParameters());
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * W PLC bledy sa przechowywane w buforze cyklicznym, ktory posiada dwa wskazniki START i END. Bledy sa zapisywane pomiedzy tymi indeksami. Oprocz kodow bledow jest takze zapisana data wystapienia
         * Jedne blad jest przechowywany na 6 WORDACH (1 word kod bledu + 3 wordy daty + 1 word ID programu + 1 word ID subprogramu. Data jest przechowayna w kodzie BCD. 
         * Data jest przechowywan w kodzie BCD to znaczy kazdy word jet podzielony na bajty ktore przechowuja info: rok i miesiac, dzien i godzina, minuta i sekunda
         */
        private void ReadErrorsFromPLC()
        {
            int[] aData = new int[Types.SIZE_ERROR_BUFFER_PLC];
            int[] aDataErr = new int[6];
            int aRes = 0;
            int aCountOverflow = 0;
            ItemLogger aErr;

            if (plc != null)
            {
                //Odczytaj bufor bledow z PLC                                                            
                aRes = (int)mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_BUFER_ERROR, Types.SIZE_ERROR_BUFFER_PLC, aData);
               //Ustaw flage ze bledy zostaly odczytane
                plc.SetDevice(Types.ADDR_FLAG_WAS_READ, 1);

                //Wyodrebnij z danych odczytanych z PLC konkrente kody bledow oraz daty ich wystapienia jak i ID programu i subprogramu

                int aStartIndex = aData[Types.OFFSET_ERR_START_INDEX];
                int aCountsError = aData[Types.OFFSET_ERR_COUNTS_INDEX];

                //Odczytaj z PLC nowe bledy ktore sie mieszcza pomiedzy indeksem START i END
                for (int i = 0; i < Types.COUNT_ERROR_PLC; i++)
                {
                    if (aData.Length > (Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 3))
                    {
                        //Sprawdz czy bledy nie sa zapisame na poczatku bufora przed indeksen startu. Wynika z mozliwosci przekrecenia sie bufora
                        aCountOverflow = aStartIndex + aCountsError - Types.COUNT_ERROR_PLC;
                        if (aCountOverflow > 0 && aCountOverflow < i)
                        {
                            aDataErr[0] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6];     //Index mnozemy razy 4 poniewaz jeden wpis bledu zajmuje 6 WORDY
                            aDataErr[1] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 1];
                            aDataErr[2] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 2];
                            aDataErr[3] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 3];
                            aDataErr[4] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 4];
                            aDataErr[5] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 5];

                            aErr = GetError(aDataErr);
                            Logger.AddError(aErr);
                        }
                        if (i >= aStartIndex && i < aStartIndex + aCountsError)
                        {
                            aDataErr[0] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6];     //Index mnozemy razy 4 poniewaz jeden wpis bledu zajmuje 6 WORDY
                            aDataErr[1] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 1];
                            aDataErr[2] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 2];
                            aDataErr[3] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 3];
                            aDataErr[4] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 4];
                            aDataErr[5] = aData[Types.OFFSET_ERR_BUFFER_INDEX + i * 6 + 5];

                            aErr = GetError(aDataErr);
                            Logger.AddError(aErr);
                        }
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie wyodrebnienie z jednego wpisu bledu ktory jest przechowywany na 4 wordach kod bledu oraz data jego wystapienia
         */
        private ItemLogger GetError(int[] aData)
        {
            ItemLogger aErr = new ItemLogger();
            DateTime dateTime = new DateTime();
            int aCode = 0;

            int aProgramID = 0;
            int aSubprogramID = 0;

            int aYear = 0;
            int aMonth = 0;
            int aDay = 0;
            int aHour = 0;
            int aMinute = 0;
            int aSecond = 0;
            //Wydrebnij kod bledu oraz date jego wystapienia z danych odczytanych z PLC (dostajesz jeden wpis)
            if (aData.Length > 3)
            {
                aCode = aData[0];
                aProgramID = aData[4];
                aSubprogramID = aData[5];

                aYear = ConvertBcdToInt(((aData[1] >> 8) & 0xFF)) + 2000;
                aMonth = ConvertBcdToInt(aData[1] & 0xFF);
                aDay = ConvertBcdToInt((aData[2] >> 8) & 0xFF);
                aHour = ConvertBcdToInt(aData[2] & 0xFF);
                aMinute = ConvertBcdToInt((aData[3] >> 8) & 0xFF);
                aSecond = ConvertBcdToInt(aData[3] & 0xFF);
            }
            try
            {
                if (aYear < 9999 && aMonth > 0 && aMonth <= 12 && aDay > 0 && aDay <= 31 && aHour >= 0 && aHour <= 24 && aMinute >= 0 && aMinute < 60 && aSecond >= 0 && aSecond < 60)
                    dateTime = new DateTime(aYear, aMonth, aDay, aHour, aMinute, aSecond);
                else
                    dateTime = DateTime.Now;

                aErr.SetErrorPLC(aCode, dateTime, aProgramID, aSubprogramID);
            }
            catch (Exception ex)
            {
                Logger.AddException(ex);
            }

            return aErr;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda pomocnicza konwertujaca dane z kodu BCD na INT
        */
        private int ConvertBcdToInt(int aBcdValue)
        {
            int aRes = 0;

            aRes = ((aBcdValue >> 4) & 0xF) * 10 + (aBcdValue & 0xF);

            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda wyodrebnia z bufora danych dane programu
        */
        void CopyData(int[] aData, int[] aDataProgram)
        {
            for (int i = 0; i < aDataProgram.Length; i++)
            {
                if (aData.Length > i)
                    aDataProgram[i] = aData[i + Types.OFFSET_PRG_DATA];
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda sprawdza stan komunikacji z PLC i ustawia odpowiedni status
        */
        private void CheckConnection(int aRes)
        {
            if (aRes == 0)
            {
                connectionPLC = true;
                status = statusPLC;
            }
            //Brak komunikacji to sprobuj nawiazac polaczenie ale nie czescie niz co 2 sekundy
            else
            {
                if (Math.Abs(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - lastTimeCheckConnectPLC) >= 3 * 1000)
                {
                    if (plc != null && plc.Connect() == 0)
                        connectionPLC = true;
                    else
                        connectionPLC = false;
                    lastTimeCheckConnectPLC = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }

                status = Types.DriverStatus.NoComm;
            }
            if (plc != null && plc.GetDummyMode())
            {
                connectionPLC = true;
                status = Types.DriverStatus.DummyMode;
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie pobranie parametrow programu z PLC i utworzonie/aktualizacja paramtrow instancji po stronie PC
         */
        private void ReadProgramFromPLC()
        {
            Program.Program programPLC = null;
            //Sprawdz czy w PLC istnieje jakis program oraz czy ma jakies subprogramy
            int[] aData = new int[1];
            int aProgramIdInPLC = 0;
            int aCountSubprogramInPLC = 0;

            if (mainForm != null && mainForm.Created)
                mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_PRG_ID, 2, aData);

            aProgramIdInPLC = aData[0];

            if (mainForm != null && mainForm.Created)
                mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_PRG_SEQ_COUNTS, 1, aData);

            aCountSubprogramInPLC = aData[0];

            //Sprawdz czy istnieje juz instanacja programu z parametrami z PLC
            if (aCountSubprogramInPLC > 0)
            {
                foreach (Program.Program program in programs)
                {
                    //Znajdz program ktory jest zaladowany w PLC
                    if (program.GetID() == aProgramIdInPLC)
                        programPLC = program;
                }
                /*   if (programPLC == null)
                   {
                       programPLC = Factory.CreateProgram((uint)aProgramIdInPLC);
                       programPLC.SetName("Program in PLC");
                       AddProgram(programPLC);
                   }
                */   //Laduj dane tylko z programu ktore jest aktuanie wykonywane w PLC
                if (programPLC != null && programPLC.Status == Types.StatusProgram.Run)
                    programPLC.ReadProgramsData();
            }
            else
            {
                //       ItemLogger aErr = new ItemLogger();
                //       aErr.SetErrorApp(Types.EventType.NO_PRG_IN_PLC);
                //       Logger.AddError(aErr);
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda sluzy do odczytywania z PLC ustawie kolejnych komponentow
         */
        public void UpdateSettings()
        {
            if (plc != null)
            {
                ItemLogger aErr = new ItemLogger();
                int[] aData = new int[Types.LENGHT_SETTINGS_DATA];
                int[] aExtraData = new int[Types.LENGHT_EXTRA_SETTINGS_DATA];

                int aCode = 0;
                aCode = (int)mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_START_EXTRA_SETTINGS, Types.LENGHT_EXTRA_SETTINGS_DATA, aExtraData);
                aCode = (int)mainForm.Invoke(mainForm.DelegateReadPLC, Types.ADDR_START_SETTINGS, Types.LENGHT_SETTINGS_DATA, aData);

                aErr.SetErrorMXComponents(Types.EventType.UPDATE_SETINGS, aCode);

                //aktualizuj dane na temat settingsow
                if (aCode == 0)
                    chamber.UpdateSettings(aData, aExtraData);

                if (aData.Length > Types.OFFSET_VERSION_PROGRAM)
                    version_programPLC = aData[Types.OFFSET_VERSION_PROGRAM];
                if (aData.Length > Types.OFFSET_SUBVERSION_PROGRAM)
                    subVersion_programPLC = aData[Types.OFFSET_SUBVERSION_PROGRAM];

                Logger.AddError(aErr);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public void SaveProgramInDB()
        {
          int aRes = 0;
            //zapisz ustawienia programow i subprogramow w bazie danych ale tylko tych wsrod ktorych zostalo cos zmienione
            if (dataBase != null)
            {
                foreach (Program.Program pr in GetPrograms())
                {
                    aRes += pr.SaveDataInDB();
                    foreach (Subprogram subpr in pr.GetSubprograms())
                        aRes += subpr.SaveDataInDB();// dataBase.ModifySubprogram(subpr);
                }
            }
            if(aRes == 0)
                Source.Logger.AddMsg("Program configuration has been saved successfully", Types.MessageType.Message);
            else
                System.Windows.Forms.MessageBox.Show("Program data save failed ","Save Program Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }
        //-------------------------------------------------------------------------------------------------------------------------
        public bool IsProgramChanged()
        {
            bool aRes = false;

            foreach (Program.Program pr in GetPrograms())
                    aRes |= pr.IsAnyChangesNotSave();
            
            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda dodaje obserwatorow do listy
         */
        public static void AddToRefreshList(RefreshProgram aRefresh)
        {
            refreshProgram += aRefresh;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie zaworami
         */
        public Valve GetValve()
        {
            return (Valve)chamber.GetObject(Types.TypeObject.VL);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie zasilaczem
         */
        public PowerSupplay GetPowerSupply()
        {
            return (PowerSupplay)chamber.GetObject(Types.TypeObject.HV);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie MFC
         */
        public MFC GetMFC()
        {
            return (MFC)chamber.GetObject(Types.TypeObject.FM);
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie pompowaniem
          */
        public ForePump GetForePump()
        {
            return (ForePump)chamber.GetObject(Types.TypeObject.FP);
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda podaje refernejce obiektu glowicy
          */
        public Gauge GetGauge()
        {
            return (Gauge)chamber.GetObject(Types.TypeObject.GAUGE);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie vaporaizerem
         */
        public Vaporizer GetVaporizer()
        {
            return (Vaporizer)chamber.GetObject(Types.TypeObject.VP);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie cisnieniem
         */
        public PressureControl GetPressureControl()
        {
            return (PressureControl)chamber.GetObject(Types.TypeObject.PC);
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie interlokami
          */
        public Interlock GetInterlock()
        {
            return (Interlock)chamber.GetObject(Types.TypeObject.INT);
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda podaje refernejce obiektu maintanve
         */
        public Maintenance GetMaintanence()
        {
            return (Maintenance)chamber.GetObject(Types.TypeObject.MNT);
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie silnikami bebna
          */
        public MotorDriver GetMotor(int aMotorID)
        {
            MotorDriver aMotor = null;

            if (aMotorID == 1) aMotor = (MotorDriver)chamber.GetObject(Types.TypeObject.MTR1);
            if (aMotorID == 2) aMotor = (MotorDriver)chamber.GetObject(Types.TypeObject.MTR2);

            return aMotor;
        }

        //-----------------------------------------------------------------------------------------
        /**
          * Metoda podaje refernejce obiektu odpowiedzialnego za sterowanie PLC
          */
        public PLC GetPLC()
        {
            return plc;
        }
        //-----------------------------------------------------------------------------------------
        /**
          * Metoda zwraca status komory
          */
        public Types.DriverStatus GetStatus()
        {
            return status;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda zwraca liste programow
        */
        public List<Program.Program> GetPrograms()
        {
            return programs;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Funckaj ma za zadanie utworzenie nowego programu. Najpierw program jest zapisany w bazie danych. Jezeli zapis sie powisodl to jest tworzony obiekt i nadawany jest mu id rekordu bazy danych
         */
        public void NewProgram()
        {
            if (dataBase != null)
            {
                //Utworz program aby posiadac obiekt na bazie ktorego utworzymy program w bazie danych.
                Program.Program program = Factory.CreateProgram(0);
                //Nadaj programowi unikalna nazwe
                program.SetName(GetUniqueProgramName());
                //Dodaj program do bazy danych
                int id = dataBase.AddProgram(program);
                //Poprawnie dodano program do bazy danych wiec dodaj program do lsity programow
                if (id > 0)
                {
                    program.SetID((uint)id);
                    AddProgram(program);
                }
            }
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda dodaje nowy program do listy oraz informuje o tym fakcie obserwatorow
         */
        private void AddProgram(Program.Program program)
        {
            programs.Add(program);
            //Poinformuj moich obserwatorow aby odswiezyly sobie informacje na temat programow
            if (refreshProgram != null)
                refreshProgram();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda usuwa dany program nie tylko z listy ale tkaze z bazy danych
         */
        public bool RemoveProgram(Program.Program program)
        {
            bool aRes = false;
            if (dataBase != null)
            {
                //Usun program z bazy danych. Jezeli to sie uda to usun takze z lokalnej listy
                if (dataBase.RemoveProgram(program) == 0)
                    aRes = programs.Remove(program);

                //Poinformuj moich obserwatorow aby odswiezyly sobie informacje na temat programow
                if (refreshProgram != null)
                    refreshProgram();
            }
            return aRes;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda zwraca unikalna nazwe programu
         */
        private string GetUniqueProgramName()
        {
            string aName;
            int aNo = 1;

            aName = "Program " + aNo.ToString();
            for (int i = 0; i < programs.Count; i++)
            {
                Program.Program program = programs[i];
                if (program.GetName() == aName)
                {
                    aName = "Program " + (++aNo).ToString();
                    i = 0;
                }
            }

            return aName;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda ustawia tryb pracy komory (Auto/Manual)
         */
        public ItemLogger SetMode(Types.Mode aMode)
        {
            ItemLogger aErr = new ItemLogger();
            int[] aData = new int[1];

            aData[0] = (int)aMode;
            if (plc != null)
            {
                //Jezeli przechodze w tryb automatyczny to wylaczam zasilacz i motor
                if(aMode == Types.Mode.Automatic)
                {
                    if (chamber.GetObject(Types.TypeObject.HV) != null)
                        ((PowerSupplay)chamber.GetObject(Types.TypeObject.HV)).SetOperate(false);
                    if (chamber.GetObject(Types.TypeObject.MTR1) != null)
                        ((MotorDriver)chamber.GetObject(Types.TypeObject.MTR1)).ControlMotor(Types.StateFP.OFF);
                    if (chamber.GetObject(Types.TypeObject.MTR2) != null)
                        ((MotorDriver)chamber.GetObject(Types.TypeObject.MTR2)).ControlMotor(Types.StateFP.OFF);
                }
                int aCode = plc.WriteWords(Types.ADDR_MODE_CONTROL, 1, aData);
                aErr.SetErrorMXComponents(Types.EventType.SET_MODE_CONTROL, aCode);
            }
            else
                aErr.SetErrorApp(Types.EventType.PLC_PTR_NULL);

            return aErr;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca tryb pracy komory (Auto/Manual)
         */
        public Types.Mode GetMode()
        {
            return mode;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Metoda zwraca liste typow gazow kotre moga byc podpiete pod dany MFC
         */
        public GasTypes GetGasTypes()
        {
            return gasTypes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie zapisanie w jednym stringu parametrow dotyczacych akwizyji danych urzadzenia w baziee danych
         */
        private void ParseParameterToAcqData(string data)
        {
            if (data != null)
            {
                string[] parameters = data.Split(';');
                foreach (string para in parameters)
                {
                    try
                    {
                        if (para.Contains("EnabledAcq"))
                            enabledAcq = Convert.ToBoolean(para.Split('=')[1]);
                        if (para.Contains("Pressure"))
                            pressureAcq = Convert.ToDouble(para.Split('=')[1]);
                        if (para.Contains("DuringProces"))
                            acqDuringOnlyProcess = true;// Convert.ToBoolean(para.Split('=')[1]);
                        if (para.Contains("AllTime"))
                            acqAllTime = false;// Convert.ToBoolean(para.Split('=')[1]);
                        if (para.Contains("ChartWindowSize"))
                            chartWindowTime = Convert.ToDateTime(para.Split('=')[1]);
                        if (para.Contains("ActiveCheck"))
                            activeCheckPressureAcq = Convert.ToBoolean(para.Split('=')[1]);
                        if (para.Contains("AskAcq"))
                            askAcq = Convert.ToBoolean(para.Split('=')[1]);

                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        *Funkcja ma za zadanie wyodrebnienie z danych parametry zapisanych w formie zbiorczego stringa kolejnych wartosci dla parametrow akwizycji danych
        */
        string ParseAcqDataToParameter()
        {
            string parameter;

            parameter  = "EnabledAcq = "      + EnabledAcq.ToString() + ";";
            parameter += "Pressure = "        + pressureAcq.ToString() + ";";
            parameter += "DuringProces = "    + acqDuringOnlyProcess.ToString() + ";";
            parameter += "AllTime = "         + acqAllTime.ToString() + ";";
            parameter += "ChartWindowSize = " + chartWindowTime.ToString() + ";";
            parameter += "ActiveCheck = "     + activeCheckPressureAcq.ToString() + ";";
            parameter += "AskAcq = "          + askAcq.ToString();

            return parameter;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Funckaj jest wykorzystywana do odczytu parametrow urzadzenia z bazie danych
        */
        public int LoadData()
        {
            int aRes = -1;

            if (dataBase != null)
            {
                //Odczytaj z bazy danych wartosc dla tego parametru
                ProgramParameter parameter;
                if (dataBase.LoadParameter(parameterAcq.Name, out parameter) == 0)
                {
                    parameterAcq.ID = parameter.ID;
                    parameterAcq.Data = parameter.Data;
                    ParseParameterToAcqData(parameter.Data); // dane zostaly poprawnie odczytane z bazy danych to przekonwertuj je na parametry dotyczace akwizycji danych
                    aRes = 0;
                }
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
        * Funckja jest wykorzystywana do zapisu danyuch urzadzenia w bazie danych
        */
        public int SaveData()
        {
            int aRes = -1;
            if (dataBase != null)
            {
                parameterAcq.Data = ParseAcqDataToParameter(); //Wykonaj parsowanie parametrow akwizycji danych ktroe powinny zostac zapisane w bazie danych
                                                               //W bazie danych sa przechowaywane jako jeden string rozdzielony ;
                aRes = dataBase.SaveParameter(parameterAcq);
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        //Metoda zwraca informacje czy jest wykonywany jakis proces w PLC
        public bool IsProcessPLCRun()
        {
            bool aProgramRun = false;

            foreach(Program.Program pr in programs)
            {
                if (pr.Status == Types.StatusProgram.Run)
                    aProgramRun = true;
            }

            return aProgramRun;
        }
        //-------------------------------------------------------------------------------------------------------------------------
        /**
         * Podaj mi info czy aktualnie wykonujacy sie program zawietra proces plasmy
         */ 
        public bool IsProgramContainsPlasmaProces()
        {
            bool ARes = false;

            foreach(Program.Program pr in programs)
            {
                if (pr.Status == Types.StatusProgram.Run && pr.IsPlasmaProcessExist())
                    ARes = true;
            }

            return ARes;
        }
        //-------------------------------------------------------------------------------------------------------------------------
    }
}
