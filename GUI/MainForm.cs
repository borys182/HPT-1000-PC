using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;
using HPT1000.Source.Program;
using HPT1000.Source;
using HPT1000;

/**TO DO
   PC
 *  
   PLC
 * 
 */
namespace HPT1000.GUI
{
    ///<summary>
    ///Klasa jest odpowiedzialna za reprezentowanie glownego okna programu. Głowna formatka aplikacja która powołuje do życia całą aplikację ze wszystkimi obiektami wizualnymi i nie wizualnymi
    ///</summary>
    public partial class MainForm : Form
    {
        string version = "1.0.38";
        SplashScrean splash = new SplashScrean();
        ManageKeybordScreen manageKeybordScreen = new ManageKeybordScreen();

        // ScaleForm                       scaleForm = new ScaleForm();                ///< Obiekt jest wykorzystywany do skalowania formatki
        //Tworzenie głownych obiektow aplikacji
        private PLC plc                           = new PLC_Mitsubishi();        ///< Utworzenie obiektu wykorzystywanego do komunikacji sie ze sterownikiem PLC
        private Source.Driver.HPT1000   hpt1000   = null;           ///< Powołanie do zycia obiektu reprezentujacego mozliwosci systemu (wszelkie operacje na systemie PLC są wykonywane za pośrednictwem tego obiektu).
        private DB                      dataBase  = null;           ///< Powołniae do życia obiektu wykorzystywanego do komunkacji z bazą danych (wszelkie operacje na bazie danych sa wykonywane za pośrednictwem tego obiektu)
        private ApplicationData         appData   = null;           ///< Utworzenie obiektu ktorego zadaniem jest przechowywanie infomracji na temat aplikacji (np. lista userow, zalogowany user itp)
        private Source.Message          message   = null;           ///< Utworzenie obiektu wykorzystywanego do wyswietlania roznego rodzai komunikatow podczas pracy z aplikacja (teksty komunikatow sa pobierane z bazy danych)

        private User                    lastUser      = null;                       ///< Zmienna przechowuje dane na temat poprzednio zalogowanego usera
        private Login                   loginForm     = null;                       ///< Zmienna jest wykorzystywana do przechowyania wskaznika na formtke Loginu usera do aplikacji
   
        private bool                    liveModeData_Graphical = false;             ///< Flaga przechowuje info na temat widocznosci wykresu na glownym oknie aplikacji


        private ItemLogger              curentErrorLog  = null;                     ///< Aktualnie wyswietlany wpis z loggera bledow traktowny jako ostatni przychodzacy i nie potwierdzony
        private ItemLogger              curentMsgLog    = null;                     ///< Aktualnie wyswietlany wpis z loggera zdarzen traktowny jako ostatni przychodzacy i nie potwierdzony

        private int                     timerLastEventShow = 0;                     ///< Zmienna wykorzystywana do czasowego pokazywania ostatniej wiadomisc zdarzenia dla usera

        ///< Grupa zmiennych przechowujaca dane na temat rozmieszczenia na formatce wybranych przeplywek MFC (mozna konfoigurowac ktroe przeplywki maja byc widoczne)
        Point pointCornerMFC1   = new Point(290, 200);
        Point pointCornerMFC2   = new Point(290, 293);
        Point pointCornerMFC3   = new Point(290, 405);
        Point pointCornerVapor  = new Point(290, 515);

        Point pointLine         = new Point(290,192);
        Size  sizeLineMFC       = new Size(3,0);
   
        private Color _Color1 = Color.Gainsboro;        ///< Ustawienie poczatkowego koloru dla gradeintu
        private Color _Color2 = Color.White;            ///< Ustawinei koncowego koloru dla gradientu 
        private float _ColorAngle = 30f;                ///< Ustawienie kata gradioentu

        ReleaseNote releaseNote = new ReleaseNote();
        //Delegat umozliwiajacy odczytywanie danych z plc z watku innego niz kolejka komunkatow poprzez wywolanie z watku funkcji delegata poprzez Invoke co powoduje ze ta funkcja uruchomi sie z contextu watku formy
        public delegate int ReadPLC(string addr, int size, int[]data);
        public delegate void UpdateChartData();
        public ReadPLC DelegateReadPLC = null;
        public UpdateChartData DelegateUpdateChartData = null;

        bool appCompatibilityDB = false;
        bool initPLCError = false;      //Flaga wymusz na PLC wykonanie procedury inicjalizujacej sprawdzenie czy sa jakies bledy
        int lastIndexPage = 0;

        bool lastStateProcess = false;  ///<flaga okresla ostatni stan proceseu {TRUE = ProcesObject wlaczony}
        //-----GRUPA SETEROW/GETEROW----- 
        //---------------------------------------------------------------------------------------
        public Color Color1
        {
            get { return _Color1; }
            set
            {
                _Color1 = value;
                this.Invalidate(); // Tell the Form to repaint itself
            }            
        }
        //---------------------------------------------------------------------------------------
        public Color Color2
        {
            get { return _Color2; }
            set
            {
                _Color2 = value;
                this.Invalidate(); // Tell the Form to repaint itself
            }
        }
        //---------------------------------------------------------------------------------------
        public float ColorAngle
        {
            get { return _ColorAngle; }
            set
            {
                _ColorAngle = value;
                this.Invalidate(); // Tell the Form to repaint itself
            }
        }
        //------------------------------------------------------------------------------------------
        /**
         * Konstruktor formatki. Jest wykorzystyewany do tworzenia obiektow i przypisywania odpowoednijh relacji pomiedzy obietkami 
         */
        public MainForm()
        {
            //Uruchom splasha i utworz wymagane obiekty w tle
            System.Threading.ThreadStart funThread = new System.Threading.ThreadStart(Initialize);
            splash.Start(funThread);
            InitializeComponent();
            labStatusMsgAction.Text = "";
            //  Initialize();
                       

            /*     scaleForm.SetForm(this);
                 scaleForm.AddPageTab(programsConfigPanel.TabPagePump);
                 scaleForm.AddPageTab(programsConfigPanel.TabPagePlasma);
                 scaleForm.AddPageTab(programsConfigPanel.TabPageGas);
                 scaleForm.AddPageTab(programsConfigPanel.TabPagePurge);
                 scaleForm.AddPageTab(programsConfigPanel.TabPageVent);
           */
            //Sprawdz kompatybilnosc aplikacji z baza danych
            appCompatibilityDB = CheckcompatibilityAppWithDB();
            if (appCompatibilityDB)
            {
                //Utworz obiekt delegata
                DelegateReadPLC = new ReadPLC(ReadPlcMethod);
                DelegateUpdateChartData = new UpdateChartData(UpdateChartDataMethod);
                //Inicjalizacja wymaganych referencji w fabryce komponenotw. Podczas tworzenie komponeotwe przez fabryke ponizsze referencje będą przypisywane tworzonym obiektom
                Factory.DataBase = dataBase;
                Factory.PLC_ = hpt1000.GetPLC();
                Factory.PowerSupply_ = hpt1000.GetPowerSupply();
                Factory.MainForm_ = this;
                Factory.Hpt1000 = hpt1000;

                //Inicjalizacja refenecji obiektu sluzacego zarzadzeniem systemem PLC w obiektach wizualnych
                programsConfigPanel.HPT1000 = hpt1000;
                programPanel.HPT1000 = hpt1000;
                alertsPanel.HPT1000 = hpt1000;
                settingsPanel.HPT1000 = hpt1000;
                servicePanel.HPT1000 = hpt1000;
                //Inicjalizacja referencji bazy danych w obiektach ktore zapisuja w bazie dane natemat programow i ustawien akwizyji urzadzen
                programsConfigPanel.DataBase = dataBase;
                settingsPanel.DataBase = dataBase;

                if (hpt1000 != null)
                {
                    //Inicjalizacja referencji konkretnych obiektow w obiektach wizualnych utworzonych bezposrednio na formatce
                    generatorPanel.SetGeneratorPtr(hpt1000.GetPowerSupply());
                    pressurePanel.SetPresureControlPtr(hpt1000.GetPressureControl());
                    motorPanel1.SetMotorPtr(hpt1000.GetMotor(1));
                    motorPanel2.SetMotorPtr(hpt1000.GetMotor(2));
                    vaporiserPanel.SetVaporizerPtr(hpt1000.GetVaporizer());
                    mfcPanel1.SetMFC(hpt1000.GetMFC(), hpt1000.GetGasTypes(), 1);
                    mfcPanel2.SetMFC(hpt1000.GetMFC(), hpt1000.GetGasTypes(), 2);
                    mfcPanel3.SetMFC(hpt1000.GetMFC(), hpt1000.GetGasTypes(), 3);
                    flowMFC1.SetMFC(hpt1000.GetMFC(), 1);
                    flowMFC2.SetMFC(hpt1000.GetMFC(), 2);
                    flowMFC3.SetMFC(hpt1000.GetMFC(), 3);

                    //Przypisanie referencji obiektu rzeczywistego powzalajacego pobrac/zapisac dane w PLC obiektowi wieuzlnemu
                    valve_Gas.SetValvePtr(hpt1000.GetValve(), Types.TypeValve.Gas);
                    valve_Purge.SetValvePtr(hpt1000.GetValve(), Types.TypeValve.Purge);
                    valve_SV.SetValvePtr(hpt1000.GetValve(), Types.TypeValve.SV);
                    valve_Vent.SetValvePtr(hpt1000.GetValve(), Types.TypeValve.VV);
                    //Przypisanie referencji obiektu rzeczywistego powzalajacego pobrac/zapisac dane w PLC obiektowi wieuzlnemu
                    interlockPanel_Door.SetInterlockPtr(hpt1000.GetInterlock(), Types.TypeInterlock.Door);
                    interlockPanel_HV.SetInterlockPtr(hpt1000.GetInterlock(), Types.TypeInterlock.InterlockHV);
                    interlockPanel_Thermal.SetInterlockPtr(hpt1000.GetInterlock(), Types.TypeInterlock.ThermalSwitch);
                    interlockPanel_Vacuum.SetInterlockPtr(hpt1000.GetInterlock(), Types.TypeInterlock.VacuumSwitch);

                    liveGraphicalPanel.SetPresureObjPtr(hpt1000.GetPressureControl());
                    liveGraphicalPanel.SetPowerSupplayObjPtr(hpt1000.GetPowerSupply());
                    liveGraphicalPanel.SetMFCObjPtr(hpt1000.GetMFC());
                    liveGraphicalPanel.SetVaporizerObjPtr(hpt1000.GetVaporizer());
                    liveGraphicalPanel.SetForePumpObjPtr(hpt1000.GetForePump());
                    liveGraphicalPanel.SetMotor1ObjPtr(hpt1000.GetMotor(1));
                    liveGraphicalPanel.SetMotor2ObjPtr(hpt1000.GetMotor(2));
                    liveGraphicalPanel.SetVaporizerObjPtr(hpt1000.GetVaporizer());
                    liveGraphicalPanel.MainForm = this;

                    archivePanel.Hpt1000 = hpt1000;
                    archivePanel.MainForm = this;
                    archivePanel.DataBase = dataBase;

                    maintancePanel.MaintancePtr = hpt1000.GetMaintanence();

                    userManagerPanel.AppData = appData;

                    pumpComponent.SetPumpPtr(hpt1000.GetForePump());

                    hpt1000.DataBase = dataBase;

                    message.DataBase = dataBase;
                    message.LoadErrorMessages();// pobierz dane z bazy danych

                    programsConfigPanel.TypesGas = hpt1000.GetGasTypes();

                    chamberPanel.SetPressure(hpt1000.GetPressureControl());
                    chamberPanel.SetGenerator(hpt1000.GetPowerSupply());
                    chamberPanel.SetMainanteceObject(hpt1000.GetMaintanence());
                }
                //Dodaj obserwatorow
                //Odswiezaj nazwy programow/subprogramow w glownym oknie aplikacji gdy zostanie zmieniona one zmoienone w oknie ConfigsProgram
                Program.AddToRefreshList(new RefreshProgram(programPanel.RefreshProgram));
                Program.AddToRefreshList(new RefreshProgram(programsConfigPanel.RefreshProgram));
                Source.Driver.HPT1000.AddToRefreshList(new RefreshProgram(programPanel.RefreshProgram));
                Source.Driver.HPT1000.AddToRefreshList(new RefreshProgram(programsConfigPanel.RefreshProgram));
                //Odswiezaj nazwy gazow na panelu przeplywki gdy zostana one zmienione w oknie Settings
                GasTypes.AddToRefreshList(new RefreshGasType(mfcPanel1.RefreshGasType));
                GasTypes.AddToRefreshList(new RefreshGasType(mfcPanel2.RefreshGasType));
                GasTypes.AddToRefreshList(new RefreshGasType(mfcPanel3.RefreshGasType));
                GasTypes.AddToRefreshList(new RefreshGasType(programsConfigPanel.RefreshGasType));
                //Odswiezanie userow gdy cos sie z nimi zmienilo
                ApplicationData.AddToRefreshUsersList(new RefreshUsers(userManagerPanel.RefreshUsers));
                //Ustaw odpowidnie obrazki dla SystemWindow
                LoadBitmap();

                liveGraphicalPanel.Location = grBoxSystem.Location;
                liveGraphicalPanel.Size = grBoxSystem.Size;

                appData.SetDataBase(dataBase);

                if (hpt1000 != null)
                    hpt1000.LoadDataFromDB();

                //Daj mozliwosc ustawiania wlasnego koloru dla headera page control
                tabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed;

            }
            //Dodaj wszystkie moduly graficnze wsrod ktorych moga przebywac kontrolki edycyjne ktre wymagaja pokazania klawiatury podczas proby ich edycji
            AddControlToManageKeyboard();
            //Uruchom aplikacje na calym ekranie
            this.WindowState = FormWindowState.Maximized;
            //Uruchom timer
            timer.Start();
        }
        //------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworznei obiektow aplikacji ale w oddzielnym watku. Na czas tworzenia obiektow wyswietlamy ekran splasha
        void Initialize()
        {
            hpt1000     = new Source.Driver.HPT1000(plc,this);    ///< Powołanie do zycia obiektu reprezentujacego mozliwosci systemu (wszelkie operacje na systemie PLC są wykonywane za pośrednictwem tego obiektu).
            dataBase    = new DB();                       ///< Powołniae do życia obiektu wykorzystywanego do komunkacji z bazą danych (wszelkie operacje na bazie danych sa wykonywane za pośrednictwem tego obiektu)
            appData     = new ApplicationData();          ///< Utworzenie obiektu ktorego zadaniem jest przechowywanie infomracji na temat aplikacji (np. lista userow, zalogowany user itp)
            message     = new Source.Message();           ///< Utworzenie obiektu wykorzystywanego do wyswietlania roznego rodzai komunikatow podczas pracy z aplikacja (teksty komunikatow sa pobierane z bazy danych)
        }
        //------------------------------------------------------------------------------------------
        //Dodaj wszystkie moduly graficnze wsrod ktorych moga przebywac kontrolki edycyjne ktre wymagaja pokazania klawiatury podczas proby ich edycji
        private void AddControlToManageKeyboard()
        {
            manageKeybordScreen.AddControl(programPanel);
            manageKeybordScreen.AddControl(programPanel.GetProcessInfoForm());
            manageKeybordScreen.AddControl(programsConfigPanel);
            manageKeybordScreen.AddControl(settingsPanel);
            manageKeybordScreen.AddControl(maintancePanel);
            manageKeybordScreen.AddControl(userManagerPanel.GetUserForm());
            manageKeybordScreen.AddControl(this);
        }
        //------------------------------------------------------------------------------------------
        //Zadaniem funkcji jest wymuszenie na PLC uruchomienia procedury sprawdzenia czy nie ma czasem jakis bledow. Wymuszenie powinno sie odbyc
        private void InitPLCError()
        {
            if(!initPLCError && hpt1000 != null && hpt1000.GetPLC() != null && hpt1000.CoonectionPLC)
            {
                if (hpt1000.GetPLC().SetDevice(Types.ADDR_FLAG_INIT_ERROR, 1) == 0)
                    initPLCError = true;
            }
        }
        //------------------------------------------------------------------------------------------
        //Sprawdz kompatybilnosc aplikacji z baza danych
        private bool CheckcompatibilityAppWithDB()
            {
           /*Kompatybilne wersje bazy danych z aplikacja
             * DB ver 0.0.0 do 1.0.2    kompatybilna z App ver 0.0.0 do 1.0.6
             * DB ver 1.0.3 do 1.0.9 kompatybilna z App ver 1.0.7 do 1.0.11
             * DB ver 1.0.10 do nieznane kompatybilna z App ver 1.0.12 do nieznane
            */
           
            string ver_DB = "0.0.0.";
            if (dataBase != null)
                ver_DB = dataBase.Version;
            int[] verDB_Tab  = Types.ConvertToInt(ver_DB.Split('.'));
            int[] verApp_Tab = Types.ConvertToInt(version.Split('.'));

            bool verOK = false;
            //sprawdz kompatybilnosc
            //Werjsa aplikacji mniejsza niz 1.0.6 jest kompatybilna z wersja bazy do wersji 1.0.2
            if (verApp_Tab.Length >= 3 && verApp_Tab[0] <= 1 && verApp_Tab[1] <= 0 && verApp_Tab[2] <= 6)
            {
                //Sprawdz czy wersja bazy jest mniejsz niz 1.0.2
                if (verDB_Tab.Length >= 3 && verDB_Tab[0] <= 1 && verDB_Tab[1] <= 0 && verDB_Tab[2] <= 2)
                    verOK = true;
            }
            //Werjsa aplikacji od 1.0.7 do 1.0.11 jest kompatybilna z wersja bazy do wersji 1.0.3 - 1.0.9
            if (verApp_Tab.Length >= 3 && verApp_Tab[0] <= 1 && verApp_Tab[1] <= 0 && verApp_Tab[2] > 6 && verApp_Tab[2] <= 11)
            {
                //Sprawdz czy wersja bazy jest 1.0.3 - 1.0.9
                if (verDB_Tab.Length >= 3 && verDB_Tab[0] <= 1 && verDB_Tab[1] <= 0 && verDB_Tab[2] > 2 && verDB_Tab[2] <= 9)
                    verOK = true;
            }
            //Werjsa aplikacji od 1.0.12 - nieznane jest kompatybilna z wersja bazy w wersji 1.0.10 - nieznane
            if (verApp_Tab.Length >= 3 && verApp_Tab[0] <= 1 && verApp_Tab[1] <= 0 && verApp_Tab[2] >= 12 )
            {
                //Sprawdz czy wersja bazy jest 1.0.3 - 1.0.9
                if (verDB_Tab.Length >= 3 && verDB_Tab[0] <= 1 && verDB_Tab[1] <= 0 && verDB_Tab[2] >= 10 )
                    verOK = true;
            }

            if (!verOK)
                MessageBox.Show("Application version " + version + " is not compatible with database version " + ver_DB + " . Correctly work of application is not possible. Application will be closed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return verOK;
        }
        //------------------------------------------------------------------------------------------
        /*
         * Metoda ma za zadanie zaladowanie grafik na formatke
         */
        private void LoadBitmap()
        {
            Bitmap chamber      = new Bitmap(Properties.Resources.Plasma);
            Bitmap arrowUp      = new Bitmap(Properties.Resources.Arrow_Up);
            Bitmap arrowDown    = new Bitmap(Properties.Resources.Arrow_Down);
            Bitmap cornerUp     = new Bitmap(Properties.Resources.Corner_Right_Top);
            Bitmap cornerDown   = new Bitmap(Properties.Resources.Corner_Right_Bottom);

            chamber.MakeTransparent(Color.White);
            arrowUp.MakeTransparent(Color.White);
            arrowDown.MakeTransparent(Color.White);
            cornerUp.MakeTransparent(Color.White);
            cornerDown.MakeTransparent(Color.White);

            pictureArrowUp1.SizeMode    = PictureBoxSizeMode.StretchImage;
            pictureArrowUp2.SizeMode    = PictureBoxSizeMode.StretchImage;
            pictureCornerUp.SizeMode   = PictureBoxSizeMode.StretchImage;
            pictureArrowDown.SizeMode   = PictureBoxSizeMode.StretchImage;
            pictureCornerUp1.SizeMode   = PictureBoxSizeMode.StretchImage;
            pictureCornerUp2.SizeMode   = PictureBoxSizeMode.StretchImage;
            pictureCornerUp.SizeMode   = PictureBoxSizeMode.StretchImage;
            pictureCornerDown.SizeMode = PictureBoxSizeMode.StretchImage;

         
            pictureArrowUp1.Image   = arrowUp;
            pictureArrowUp2.Image   = arrowUp;

            pictureArrowDown.Image  = arrowDown;

            pictureCornerUp1.Image  = cornerUp;
            pictureCornerUp2.Image  = cornerUp;
            pictureCornerUp.Image  = cornerUp;

            pictureCornerDown.Image = cornerDown;
        }
        //----------------------------------------------------------------------------------
        /**
         * Metoda wykozystywana do odswiezania danych na formatce
         */ 
        private void timer_Tick(object sender, EventArgs e)
        {
           if (appCompatibilityDB)
            {
                bool clearUserChanged = false;
                if (ApplicationData.UserChanged)
                    clearUserChanged = true;
                //Odswiez panel programu
                programPanel.RefreshPanel();
                //Odswiezaj dane elementow komory na panelach systemu
                generatorPanel.RefreshData();

                vaporiserPanel.RefreshData();
                pressurePanel.RefreshData();
                mfcPanel1.RefreshData();
                mfcPanel2.RefreshData();
                mfcPanel3.RefreshData();
                valve_Gas.RefreshData();
                valve_Purge.RefreshData();
                valve_SV.RefreshData();
                valve_Vent.RefreshData();
                pumpComponent.RefreshData();
                alertsPanel.RefreshPanel();
                interlockPanel_Door.RefreshPanel();
                interlockPanel_HV.RefreshPanel();
                interlockPanel_Thermal.RefreshPanel();
                interlockPanel_Vacuum.RefreshPanel();
                flowMFC1.RefreshPanel();
                flowMFC2.RefreshPanel();
                flowMFC3.RefreshPanel();
                chamberPanel.RefreshPanel();
                maintancePanel.RefreshData();
                motorPanel1.RefreshData();
                motorPanel2.RefreshData();
                motorPanel1.Visible = MotorDriver.Motor_1_Enable;
                motorPanel2.Visible = MotorDriver.Motor_2_Enable;

                //Jezeli proces sie akurat rozpoczal to zeruj wykres
          /*      if (!lastStateProcess && hpt1000.IsProcessPLCRun())
                    liveGraphicalPanel.ClearChart();
                //Aktualizuj dane na wykresie tylko w sytuacji gdy jest uruchomiony process
                if (hpt1000.IsProcessPLCRun())
                    liveGraphicalPanel.UpdateData();
           */     //Podaj status systemu
                switch (hpt1000.GetStatus())
                {
                    case Types.DriverStatus.OK:
                        statusLabel.Text = "CONNECTED";
                        statusLabel.ForeColor = Color.Green;
                        break;
                    case Types.DriverStatus.NoComm:
                        statusLabel.Text = "DISCONNECTED";
                        statusLabel.ForeColor = Color.Red;
                        break;
                    case Types.DriverStatus.DummyMode:
                        statusLabel.Text = "DUMMY MODE";
                        statusLabel.ForeColor = Color.Blue;
                        break;
                    case Types.DriverStatus.Error:
                        statusLabel.Text = "PLC ERROR ";
                        statusLabel.ForeColor = Color.Red;
                        break;
                    case Types.DriverStatus.Warning:
                        statusLabel.Text = "PLC STOP";
                        statusLabel.ForeColor = Color.Orange;
                        break;
                    default:
                        statusLabel.Text = "Status None";
                        statusLabel.ForeColor = Color.Black;
                        break;

                }
                //pokaz wynik ostatniej akcji jaka wystapila w systemie na dolnym pasku statusu
                ShowLastActionStatus();
                //pokaz ustawienia dla aktywnych MFC
                ShowOnlyEnableMFC();
                //pokaz zalogowanego usera oraz pokaz dostepne funkcje programu w zaleznosi od zalogowanego usera
                ShowUser();
                //W zaleznosci od wybranego sposonu prezentowania danych pokaz dane systemowe albo na wykresie albo na panelu
                ShowLivePanelData();         
                //Pokaz info o koniecnych operacja ch konserwatoreskich maszyny
                ShowMaintanceStatus();
                //Daj mozliwosc edytownai wartosci w trybie autoamtycznym tylko modula ktore sa zawarte w wykonytwanym subprogramie
                ShowOnlyActiveModule();
                //Okreslenie wersji programu
                ShowProgramVersion();
                //Inicjalizuj procedure wyznaczanie bledow przez PLC
                InitPLCError();
                //Aktualizuj tryb pracy procesu regulacji gazow
                RefreshGasProcessMode();
                //Ustaw widoczonsc pradu i napiecia na panelu generatora w zaleznosci od zalogowanego usera
                ShowCurentVoltageHV();
                //Zapamietaj ostatni stan procesu
         //       lastStateProcess = hpt1000.IsProcessPLCRun();

                if (clearUserChanged)
                    ApplicationData.ClearInfoUserChanged();

                //Jezeli mam pokazac fomratke loginu to zrob to - pokaz ja wtedy gdy user nie jest zalogowany
                if (ApplicationData.LoggedUser != null && ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.None)
                    ShowLoginForm();

            }
            else
                Close();
        }
        //----------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie odswiezaniae danych na wykresie. Nalezy pamietac ze jest ona wywolywana z watku urzadzenia odrazu po odczytaniu danych z urzadzenia aby byla szybsza aktualizacja danych
         */ 
        public void UpdateChartDataMethod()
        {
            //Jezeli proces sie akurat rozpoczal lub user zostal na nowo zalogowany to zeruj wykres
            if (!lastStateProcess && hpt1000.IsProcessPLCRun())
                liveGraphicalPanel.ClearChart();
      
            //Aktualizuj dane na wykresie ale tylko wtedy gdy user jest zalogowany
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.None)
                 liveGraphicalPanel.UpdateData();
            
            //Zapamietaj ostatni stan procesu
            lastStateProcess = hpt1000.IsProcessPLCRun();
        }
        //----------------------------------------------------------------------------------
        //Okreslenie wersji programu
        void ShowProgramVersion()
        {
            string aVersionProgrma = "Program version: 0.0.0 Firmware 0.0";

            if(hpt1000 != null)
                aVersionProgrma = "Program version: " + version + " Firmware: " + hpt1000.VersionProgramPLC.ToString() + "." + hpt1000.SubversionProgramPLC.ToString();

            labVersion.Text = aVersionProgrma;
        }
        //----------------------------------------------------------------------------------
        /**
         *  Zadaniem metody jest wyswietlenie pradu i napiecia zasilacza HV tylko dla serwisanta
         */
        private void ShowCurentVoltageHV()
        {
            if (generatorPanel != null)
            {
               /* if (ApplicationData.LoggedUser.Privilige == Types.UserPrivilige.Service)
                    generatorPanel.Height = 195;
                else
                    generatorPanel.Height = 195;
                */
                generatorPanel.ShowCurentVoltageHV();
            }
        }
        //----------------------------------------------------------------------------------
        //Daj mozliwosc edytownai wartosci w trybie autoamtycznym tylko modula ktore sa zawarte w wykonytwanym subprogramie
        void ShowOnlyActiveModule()
        {
            if (hpt1000.GetMode() == Types.Mode.Manual)
            {
                if(!generatorPanel.Enabled)     generatorPanel.Enabled  = true;
                if(!motorPanel1.Enabled)        motorPanel1.Enabled     = true;
                if(!motorPanel2.Enabled)        motorPanel2.Enabled     = true;
                if(!pressurePanel.Enabled)      pressurePanel.Enabled   = true;

                bool aEnabledMfc = true;
                bool aEnableVapo = true;
                if (hpt1000.GetPressureControl().GetMode() == Types.GasProcesMode.Presure_MFC)
                    aEnableVapo  = false;
                if (hpt1000.GetPressureControl().GetMode() == Types.GasProcesMode.Pressure_Vap)
                    aEnabledMfc  = false;

                if(mfcPanel1.Enabled        != aEnabledMfc)     mfcPanel1.Enabled       = aEnabledMfc;
                if(mfcPanel2.Enabled        != aEnabledMfc)     mfcPanel2.Enabled       = aEnabledMfc;
                if(mfcPanel3.Enabled        != aEnabledMfc)     mfcPanel3.Enabled       = aEnabledMfc;
                if(vaporiserPanel.Enabled   != aEnableVapo)     vaporiserPanel.Enabled  = aEnableVapo;              
            }
            else
            {
                Subprogram aCurentSubProgram = programPanel.CurentSubProgram;
                if (aCurentSubProgram != null && hpt1000.IsProcessPLCRun())
                {
                    if (aCurentSubProgram.GetPlasmaProces().Active)     generatorPanel.Enabled = true;
                    else                                                generatorPanel.Enabled = false ;

                    if (aCurentSubProgram.GetMotorProces().Active)
                    {
                        if(((MotorProces)aCurentSubProgram.GetMotorProces()).GetActive(1))    motorPanel1.Enabled = true;
                        else                                                                  motorPanel1.Enabled = false;
                        if (((MotorProces)aCurentSubProgram.GetMotorProces()).GetActive(2))   motorPanel2.Enabled = true;
                        else                                                                  motorPanel2.Enabled = false;
                    }
                    else
                    {
                        motorPanel1.Enabled = false;
                        motorPanel2.Enabled = false;
                    }
                    if (aCurentSubProgram.GetGasProces().Active)
                    {
                        GasProces aGasProces = aCurentSubProgram.GetGasProces();

                        bool aEnabledMfc = true;
                        bool aEnableVapo = true;
                        if (aGasProces.GetModeProces() == Types.GasProcesMode.Presure_MFC)
                            aEnableVapo = false;
                        if (aGasProces.GetModeProces() == Types.GasProcesMode.Pressure_Vap)
                            aEnabledMfc = false;

                        if (aGasProces.GetActiveFlow(1)) mfcPanel1.Enabled = aEnabledMfc;
                        else mfcPanel1.Enabled = false;

                        if (aGasProces.GetActiveFlow(2)) mfcPanel2.Enabled = aEnabledMfc;
                        else mfcPanel2.Enabled = false;

                        if (aGasProces.GetActiveFlow(3)) mfcPanel3.Enabled = aEnabledMfc;
                        else mfcPanel3.Enabled = false;

                        if (aGasProces.GetActiveFlow(4)) vaporiserPanel.Enabled = aEnableVapo;
                        else vaporiserPanel.Enabled = false;
                         
                        if(aGasProces.GetModeProces() != Types.GasProcesMode.FlowSP) pressurePanel.Enabled = true;
                        else pressurePanel.Enabled = false;

                    }
                    else
                    {
                        pressurePanel.Enabled   = false;
                        mfcPanel1.Enabled       = false;
                        mfcPanel2.Enabled       = false;
                        mfcPanel3.Enabled       = false;
                        vaporiserPanel.Enabled  = false;
                    }
                }
                else
                {
                    generatorPanel.Enabled  = false;
                    motorPanel1.Enabled     = false;
                    motorPanel2.Enabled     = false;
                    pressurePanel.Enabled   = false;
                    mfcPanel1.Enabled       = false;
                    mfcPanel2.Enabled       = false;
                    mfcPanel3.Enabled       = false;
                    vaporiserPanel.Enabled  = false;
                }
            }
        }
        //----------------------------------------------------------------------------------
        /**
         * Aktualizuj tryb pracy procesu regulacji gazow
         */
        private void RefreshGasProcessMode()
        {
            if (hpt1000 != null && hpt1000.GetPressureControl() != null)
            {
                //Jezeli jest tryb reczny to tryb kontroli gazow jest brany na podstawie obiektu pressureContorl
                if (hpt1000.GetMode() == Types.Mode.Manual)
                    Factory.GasProcessMode = hpt1000.GetPressureControl().GetMode();
                //Tryb autoamtyczny to tryb pracy regulatora cisneinai jest brany na podstawie aktuyalnie wykonywanego subprogramu
                else
                {
                    Subprogram aCurentSubProgram = programPanel.CurentSubProgram;
                    if (aCurentSubProgram != null && hpt1000.IsProcessPLCRun())
                    {
                        GasProces gasProces = aCurentSubProgram.GetGasProces();
                        if (gasProces != null)
                            Factory.GasProcessMode = gasProces.GetModeProces();
                    }
                    else
                        Factory.GasProcessMode = Types.GasProcesMode.Unknown;
                }
            }
        }
        //----------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie pokazanie danych systemu na glownym oknie w postacji wykresu badz wartości
         */
        private void ShowLivePanelData()
        {
             //Pokaz dane jako wartosci. Wazne aby Visable bylo ustawione tylko raz (komponent nie miga)
            if(grBoxSystem.Visible == liveModeData_Graphical)
                grBoxSystem.Visible = !liveModeData_Graphical;

            //Pokaz dane jako wartosci. Wazne aby Visable bylo ustawione tylko raz (komponent nie miga)
            if (liveGraphicalPanel.Visible != liveModeData_Graphical)
                liveGraphicalPanel.Visible = liveModeData_Graphical;            
        }
        //----------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie pokazac nazwe zalgowanego usera oraz ustaw dostpne wlasciwosci aplikacji dostosowane do jego uprawnien
        */    
        public void ShowUser()
        {
            //Dostosuj formatki do uprawnien usera
            ShowComponentsPersmission();    
            //Pokaz info o zalogowanym Userze
            if (dataBase != null)
            {
                //Odswiez dane na temat zalogowanego usera jezeli jest on inny niz poprzednio zalogowany
                if(lastUser == null || !lastUser.Equals(ApplicationData.LoggedUser))
                    labStatusUser.Text = "USER:  " + ApplicationData.LoggedUser.ToString() + "    ";
                //Aktualizuj usera ostatnio zalogowanego jako ten ktroy jest aktualnie zalogowany
                lastUser = ApplicationData.LoggedUser.Copy();
            }
            //Jezeli user posiada uprawienia inne niz None to znaczy ze zostal zalogowany.
            bool aUserLoged = false;
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.None)
                aUserLoged = true;
            //Ustaw w zaleznosci od stanu zalogowania usera widocznosc przuciskow Login/Logout
            btnLogin.Enabled    = !aUserLoged;
            btnLogout.Enabled   =  aUserLoged;
        }
        //----------------------------------------------------------------------------------
        //Metoda ma za zadamoie pokazanie informacji czy jest wymagana wymiana oleju badz konserwacja
        public void ShowMaintanceStatus()
        {
            if(hpt1000.GetMaintanence() != null)
            {
                if (hpt1000.GetMaintanence().IsMaintanceRequired())
                    labStatusMaintance.Text = "MAINTENANCE INTERVAL REACHED";
                if (hpt1000.GetMaintanence().IsOilChange())
                    labStatusMaintance.Text = "CHANGE PUMP OIL";
                if (hpt1000.GetMaintanence().IsMaintanceRequired() && hpt1000.GetMaintanence().IsOilChange())
                    labStatusMaintance.Text = "MAINTENANCE AND PUMP OIL CHANGE DUE";

                labStatusMaintance.Visible = hpt1000.GetMaintanence().IsMaintanceRequired() || hpt1000.GetMaintanence().IsOilChange();

                toolStripSeparator.Visible = false;
                if (labStatusMaintance.Visible && labStatusMsgAction.Text != "")
                    toolStripSeparator.Visible = true;
            }
        }
        //----------------------------------------------------------------------------------
        /**
        *  Funkcja ma za zadanie dostosowanie formatek do uprawnien danego usera 
        */
        private void ShowComponentsPersmission()
        {
            //Dostosuj okreslone panele do uprawnien danego usera
            if (ApplicationData.UserChanged)
            {
                settingsPanel.ShowComponetsPersmission(ApplicationData.LoggedUser);
                programPanel.ShowComponetsPersmission(ApplicationData.LoggedUser);
                programsConfigPanel.ShowComponetsPersmission(ApplicationData.LoggedUser);
     //           maintancePanel.ShowComponetsPersmission(ApplicationData.LoggedUser);
                //Utworz zakaldki zgodnie z uprawnieniami danego usera
                CreateTabControl(ApplicationData.LoggedUser);
            }
        }
        //----------------------------------------------------------------------------------
        /**
         * Funkcja tworzy zakladki w glownym oknie aplikacji zgodnie z uprawnieniami posiadanymi przez usera
         */
        private void CreateTabControl(User user)
        {
            if (user != null)
            {
                if (user.Privilige == Types.UserPrivilige.None)
                {
                    tabControlMain.TabPages.Remove(tabPagePrograms);
                    tabControlMain.TabPages.Remove(tabPageAlerts);
                    tabControlMain.TabPages.Remove(tabPageArchive);
                    tabControlMain.TabPages.Remove(tabPageSettings);
                    tabControlMain.TabPages.Remove(tabPageMaintenance);
                    tabControlMain.TabPages.Remove(tabPageService);
                    tabControlMain.TabPages.Remove(tabPageUser);
                }
                else //Dodaj wspolne zakladki dla Operatora,Technik, Admina i Service
                {
                    if (!tabControlMain.TabPages.Contains(tabPagePrograms))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPagePrograms);
                    if (!tabControlMain.TabPages.Contains(tabPageAlerts))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageAlerts);
                    if (!tabControlMain.TabPages.Contains(tabPageArchive))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageArchive);
                    if (!tabControlMain.TabPages.Contains(tabPageSettings))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageSettings);
                    if (!tabControlMain.TabPages.Contains(tabPageMaintenance))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageMaintenance);
                }
                //Dodaj zakladki dla Admina
                if (user.Privilige == Types.UserPrivilige.Administrator)
                {
                    if (!tabControlMain.TabPages.Contains(tabPageUser))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageUser);

                }
                //Dodaj zakladki dla Service
                if (user.Privilige == Types.UserPrivilige.Service)
                {
                    if (!tabControlMain.TabPages.Contains(tabPageService))
                        tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageService);
                }
            }
        }
        //----------------------------------------------------------------------------------
        /**
         * Funkcja dostosowuje zakladki aplikacje do uprawnien usera jako Admin
         */
        public void SetUserPriviligeToAppAsAdmin()
        {
            programPanel.Enabled        = true;
            grBoxSystem.Enabled         = true;
            programsConfigPanel.Enabled = true;
            alertsPanel.Enabled         = true;
            settingsPanel.Enabled       = true;

            if (!tabControlMain.TabPages.Contains(tabPageUser))
                tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageUser);
            if (!tabControlMain.TabPages.Contains(tabPageService))
                tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageService);
        }
        //----------------------------------------------------------------------------------
        /**
         * Funkcja dostosowuje zakladki aplikacje do uprawnien usera jako operator
         */
        public void SetUserPriviligeToAppAsOperator()
        {
            tabControlMain.Visible      = true;
            programPanel.Enabled        = true;
            grBoxSystem.Enabled         = true;
            programsConfigPanel.Enabled = false;
            alertsPanel.Enabled         = true;
            settingsPanel.Enabled       = false;
            tabControlMain.TabPages.Remove(tabPageService);
            tabControlMain.TabPages.Remove(tabPageUser);
        }
        //----------------------------------------------------------------------------------
        /**
        * Funkcja dostosowuje zakladki aplikacje do uprawnien usera jako service
        */
        public void SetUserPriviligeToAppAsService()
        {
            tabControlMain.Visible      = true;
            programPanel.Enabled        = true;
            grBoxSystem.Enabled         = true;
            programsConfigPanel.Enabled = true;
            alertsPanel.Enabled         = true;
            settingsPanel.Enabled       = true;
            if (!tabControlMain.TabPages.Contains(tabPageService))
                tabControlMain.TabPages.Insert(tabControlMain.TabPages.Count, tabPageService);
        }
        //----------------------------------------------------------------------------------
        /**
        * Funkcja dostosowuje zakladki aplikacje do braku uprawnien usera
        */
        public void SetUserPriviligeToAppAsNone()
        {
            programPanel.Enabled        = false;
            grBoxSystem.Enabled         = false;
            programsConfigPanel.Enabled = false;
            alertsPanel.Enabled         = false;
            settingsPanel.Enabled       = false;


            if (hpt1000 != null && hpt1000.GetPLC() != null)
                hpt1000.GetPLC().SetDummyMode(false);
        }
        //----------------------------------------------------------------------------------
        /**
        * Funkcja ma za zadanie pokazanie fomratki logowania usera jezeli sytuacja tego wymaga (nie jestesmy zalogowani )
        */
        private void ShowLoginForm()
        {
            if (loginForm == null || loginForm.IsDisposed)
            {
                loginForm = new Login();
                manageKeybordScreen.AddControl(loginForm);
                loginForm.FormClosed += new FormClosedEventHandler(loginForm_Closed); //Dodaj funkcje do zdarzenia zamkniecia formatki logowania
                loginForm.SetApp(appData);
            }
            if(loginForm.Visible == false)
                loginForm.ShowDialog();
        }
        //----------------------------------------------------------------------------------
        /**
        * Funkcja zdarzenia zamknieca formatki Login czyszczaca pamiec
        */
        private void loginForm_Closed(object sender, FormClosedEventArgs e)
        {
            loginForm.Dispose();
            loginForm = null;
            if (ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.None)
                liveGraphicalPanel.ClearChart();
        }
        //----------------------------------------------------------------------------------
        /**
        * Funkcja ma za zadanie pokazanie dostepnych przeplywek MFC
        */
        public void ShowOnlyEnableMFC()
        {
            if (hpt1000 != null)
            {
                bool mfc1Enable = hpt1000.GetMFC().GetActive(1);
                bool mfc2Enable = hpt1000.GetMFC().GetActive(2);
                bool mfc3Enable = hpt1000.GetMFC().GetActive(3);

                bool vapoEnable = hpt1000.GetVaporizer().GetActive();


                bool aVisibleCornerUp   = false;
                bool aVisibleCornerDown = false;

                Point pointCornerUp     = pictureCornerUp.Location;
                Point pointCornerDown   = pictureCornerDown.Location;
              
                mfcPanel1.Visible       = mfc1Enable;
                flowMFC1.Visible        = mfc1Enable;
                pictureLineMFC1.Visible = mfc1Enable;

                mfcPanel2.Visible       = mfc2Enable;
                flowMFC2.Visible        = mfc2Enable;
                pictureLineMFC2.Visible = mfc2Enable;

                mfcPanel3.Visible       = mfc3Enable;
                flowMFC3.Visible        = mfc3Enable;
                pictureLineMFC3.Visible = mfc3Enable;

                vaporiserPanel.Visible       = vapoEnable;
                pictureLineVaporizer.Visible = vapoEnable;

                bool AExistAnyMFC = mfc1Enable | mfc2Enable | mfc3Enable | vapoEnable;

                picturelineMFC.Visible      = AExistAnyMFC;
                pictureBoxLineGV.Visible    = AExistAnyMFC;
                valve_Gas.Visible           = AExistAnyMFC;

                aVisibleCornerUp    = mfc1Enable;
                aVisibleCornerDown  = mfc2Enable | mfc3Enable | vapoEnable;

                pointCornerUp = pointCornerMFC1;
                pointCornerDown.X = pictureBoxLineGV.Location.X + pictureBoxLineGV.Size.Width;
                pointCornerDown.Y = pictureBoxLineGV.Location.Y + 3;

                if (mfc2Enable)
                    pointCornerDown = pointCornerMFC2;
                if (mfc3Enable)
                    pointCornerDown = pointCornerMFC3;
                if (vapoEnable)
                    pointCornerDown = pointCornerVapor;


                if (mfc1Enable)
                {
                    sizeLineMFC.Height  = pointCornerDown.Y - pictureCornerUp.Top;
                    pointLine           = pictureCornerUp.Location;
                }
                else
                {
                    sizeLineMFC.Height  =  pointCornerDown.Y - pictureBoxLineGV.Top;
                    pointLine.X         = pictureBoxLineGV.Location.X + pictureBoxLineGV.Size.Width;
                    pointLine.Y         = pictureBoxLineGV.Location.Y;
                }


                pictureCornerUp.Visible    = aVisibleCornerUp;
                pictureCornerDown.Visible  = aVisibleCornerDown;

                pictureCornerUp.Location   = pointCornerUp;
                pictureCornerDown.Location = pointCornerDown;

                picturelineMFC.Size         = sizeLineMFC;
                picturelineMFC.Location     = pointLine;
            }
        }
        //----------------------------------------------------------------------------------
        /**
            Funkcja ma za zadanie pokazanie ostatniego logu. Jezeli jest to blad to info sie wyswietla tak dlugo dopuki nie zostanie potwierdzony
        */
        private void ShowLastActionStatus()
        {
            //Podaj mi ostatni niepotwierdzony blad lub ostatnia akcje. Przy akcjach komunikat jest prezetnowany przez 5s zas przy bledach dopoki nie zostana potwierdzone
            ItemLogger aItemErrorLog = Logger.GetLastError();
            ItemLogger aItemMsgLog   = Logger.GetLastInformation();

            if (aItemErrorLog != null && !aItemErrorLog.Equals(curentErrorLog))
            {
                labStatusAction.Text      = "Error : " + aItemErrorLog.GetText();
                labStatusAction.ForeColor = Color.Red;
                btnConfirm.Visible        = true;
                borderLab1.Visible        = true;
                borderLab2.Visible        = true;

                curentErrorLog = aItemErrorLog;
            }

            if (aItemMsgLog != null && !aItemMsgLog.Equals(curentMsgLog))
            {
                labStatusMsgAction.Text = aItemMsgLog.GetText();
                timerLastEventShow = 0;
              
                curentMsgLog = aItemMsgLog;
            }
            
            //wyswietlaj komunikat dopuki blad nie zostanie potwierdzony - wtedy metoda nie zwroci zadnego bledu
            if (aItemErrorLog == null)
            {
                labStatusAction.Text = "";
                btnConfirm.Visible = false;
                borderLab1.Visible = false;
                borderLab2.Visible = false;
            }
            //wyswietlaj komunikat dopuki nie minie 5 s
            if (timerLastEventShow > 50)
                labStatusMsgAction.Text = "";
     
            if (timerLastEventShow <= 50)
                timerLastEventShow++;
        }
        //----------------------------------------------------------------------------------
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            hpt1000.Dispose();
            manageKeybordScreen.CloseKeyboard();          
        }
        //----------------------------------------------------------------------------------
        private void btnLogin_Click(object sender, EventArgs e)
        {
            ShowLoginForm();
        }
        //----------------------------------------------------------------------------------
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (appData != null)
                appData.LogoutUser();
        }
        //----------------------------------------------------------------------------------
        private void btnLiveModeData_Click(object sender, EventArgs e)
        {
            liveModeData_Graphical = !liveModeData_Graphical;
            if (liveModeData_Graphical)
                btnLiveModeData.Text = "SWITCH TO MIMC   VIEW";
            else
                btnLiveModeData.Text = "SWITCH TO GRAPHICAL VIEW";
        }
        //----------------------------------------------------------------------------------
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (curentErrorLog != null)
                curentErrorLog.SetConfirmError();
            //Jezeli nie ma juz niepotwierdzonych bledow to kasuj bledy po stronie PLC
            if(Logger.GetLastError() == null)
            {
                if (hpt1000 != null && hpt1000.GetPLC() != null)
                {
                    if (hpt1000.GetPLC().SetDevice(Types.ADDR_FLAGE_CLEAR_ERROR, 1) != 0)
                        MessageBox.Show("Occurred problem during deleting errors from driver PLC", "Clear PLC Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //----------------------------------------------------------------------------------
        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            //e.DrawBackground();
            using (LinearGradientBrush br = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Gray, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(br, e.Bounds); //Wrysuj odpowidni kolor zaklaski
                //Wyrysuj tekst
                SizeF sz = e.Graphics.MeasureString(tabControlMain.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(tabControlMain.TabPages[e.Index].Text, e.Font, Brushes.White, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();

                //Ustaw obrazek
         //       e.Graphics.DrawImage(imageList1.Images[e.Index], e.Bounds.Left,e.Bounds.Top, 30, 30);

            }
        }
        //----------------------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Getting the graphics object
            Graphics g = pevent.Graphics;

            // Creating the rectangle for the gradient
            Rectangle rBackground = new Rectangle(0, 0, this.Width, this.Height);

            // Creating the lineargradient
            LinearGradientBrush bBackground = new LinearGradientBrush(rBackground, _Color1, _Color2, _ColorAngle);

            // Draw the gradient onto the form
            g.FillRectangle(bBackground, rBackground);

            // Disposing of the resources held by the brush
            bBackground.Dispose();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
      //      scaleForm.Scale();
        }
        //--------------------------------------------------------------------------------------
        private void labVersion_Click(object sender, EventArgs e)
        {
            releaseNote.ShowDialog();
        }
        //--------------------------------------------------------------------------------------
        /**
         * Funkcaj delegata umozliwiajaca odczytanie pamieci plc z watku innego niz watek aplikacji (formatki). Bibliktoeka mx componets poprawnie odczytuje dane tylko dla kolejki komunikatow
         */ 
        private int ReadPlcMethod(string addr, int size, int[] data )
        {
            int Res = -1;
            if (hpt1000 != null && hpt1000.GetPLC() != null)
            {
                PLC_Mitsubishi plc = (PLC_Mitsubishi)hpt1000.GetPLC();
                Res = plc.ReadWords(addr,size,data);
            }
            return Res;
        }

        //--------------------------------------------------------------------------------------
        private void timerKeyboard_Tick(object sender, EventArgs e)
        {
            //Zarzadzaj klawiatura ktora powinna sie pokazac gdy focusa posiada kontrolka edycjyjna
            manageKeybordScreen.ManageKeyboard();
        }
        //--------------------------------------------------------------------------------------
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Sprawdz czy nie ma zmian czasem w subprogram. Jezeli tak to zapytaj usera czy nie chce ich zapisac pod warunkiem ze userem nie jest operator
            if (hpt1000.IsProgramChanged() && ApplicationData.LoggedUser.Privilige != Types.UserPrivilige.Operator)
            {
                if (MessageBox.Show("Unsaved changes of program parameters exist. Do you want to close application despite and discard changes ?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    e.Cancel = true;
                        //hpt1000.SaveProgramInDB();
            }
        }
        //--------------------------------------------------------------------------------------
        private void tabControlMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //Po opuszczeniu zakladki Programs  sprawdz czy nie zmienily sie czasem dane i nie sa potwierdzone
            if (lastIndexPage == 1 && tabControlMain.SelectedIndex != 1)
                programsConfigPanel.CheckSubprogramAnyChanges();
            lastIndexPage = tabControlMain.SelectedIndex;
        }
        //--------------------------------------------------------------------------------------
    }
}
