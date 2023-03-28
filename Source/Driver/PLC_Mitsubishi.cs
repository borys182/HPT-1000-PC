using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HPT1000.Source.Driver
{

    /// <summary>
    /// Klasa rzeczywistego urzadzenia PLC firmy Mitsubushhi. Jest ona odpowiedzialna za wymianę danych z PLC
    /// </summary>
    class PLC_Mitsubishi : PLC
    {
        //obiekt umozliwiajacy komunikacje z PLC bez uzycia narzedzia SetupUtility
        private ActProgTypeLib.ActProgTypeClass plc = null;
 
        private static object sync_Object = new object();
        //Obiekt do generowania losowych wartosi dla trybu Dummy
        private Random random = new Random();

        //Lista zmiennych wymagana do odczytywania danych w blokach. Poniewaz nie wiadomo dlaczego funkcja do odczytywabia danych z blokow danych poprawnie zwraca wartoci tlyko gdy jest wywolana z kolejki komunikatow a nie watku innego dlatego dane odczytuje w timerze i synchronizuje odczyt za pomoca flag
   //     Timer timerPLC = new Timer();
        //Grupa zmiennych sluzaca do synchronizacji odczytu blokowych danych z timera i przekazywnie ich do funkcji
        private int     resReadBlock     = -1;
        private string  addrBlock        = "D0";
        private int     sizeBlock        = 0;
        private short[] dataBlock        = new short[500];
        private bool    readBlockData    = false;
        private bool    blockDataWasRead = false;
        //-----------------------------------------------------------------------------------------
        /**
         * Konstruktor klasy
         */
        public PLC_Mitsubishi()
        {
            typePLC = Types.TypePLC.L;
            InitialDummyModeData();
            //Ustawienie funkcji ktora jest wywolywana jako Execute timera
        //    timerPLC.Tick += new System.EventHandler(this.ReadBlockData);
         
            //Utworz obiekt komunikacji z  PLC
            try
            {
                plc = new ActProgTypeLib.ActProgTypeClass();
            }
            catch (Exception e)
            {
                ItemLogger aErr = new ItemLogger();
                aErr.SetErrorApp(Types.EventType.MX_COMPONENTS_NO_INSTALL);
                Logger.AddError(aErr);
            }
            //Ustaw interwal czasu dla timera 5 ms oraz wlacz go
     //       timerPLC.Interval   = 100;
     //       timerPLC.Start();
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metofda ma za zadanie ustawienie wartosci dumy dla trybu dumu
         */
        private void InitialDummyModeData()
        {
            dummyModeStatusChamberData[0] = 1;
            dummyModeStatusChamberData[1] = 0;     //pressure
            dummyModeStatusChamberData[2] = 1;
            dummyModeStatusChamberData[4] = (int)Types.StateFP.ON;
            dummyModeStatusChamberData[5] = (int)Types.StateHV.ON;
            dummyModeStatusChamberData[6] = (int)Types.ModeHV.Power;
            dummyModeStatusChamberData[7] = 0;     //actula HV value
            dummyModeStatusChamberData[8] = 1;
            dummyModeStatusChamberData[9] = 0;
            dummyModeStatusChamberData[10] = 1;
            dummyModeStatusChamberData[11] = 0;
            dummyModeStatusChamberData[12] = 1;
            dummyModeStatusChamberData[13] = 0xAAAA; // state valves
            dummyModeStatusChamberData[14] = 0;
            dummyModeStatusChamberData[15] = 345;  //actual flow
            dummyModeStatusChamberData[17] = 157;
            dummyModeStatusChamberData[19] = 289;
            dummyModeStatusChamberData[27] = (int)Types.Mode.Automatic;
            dummyModeStatusChamberData[35] = 0xD6A; //InterlockState {}
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metofda ma za zadanie ustawienie wartosci dumy dla trybu dumu
         */
        private void GenerateRandomValueForDummyMode()
        {
            double pressure = random.Next(0, 1000);
            double power    = random.Next(0, 500);
            double voltage  = random.Next(0, 1000);
            double curent   = random.Next(0, 100);
            int flow1       = random.Next(0, 500);
            int flow2       = random.Next(0, 500);
            int flow3       = random.Next(0, 500);

            dummyModeStatusChamberData[1] = Types.ConvertDOUBLEToWORD(pressure, Types.Word.LOW);      //pressure
            dummyModeStatusChamberData[2] = Types.ConvertDOUBLEToWORD(pressure, Types.Word.HIGH);     //pressure

            dummyModeStatusChamberData[7] = Types.ConvertDOUBLEToWORD(voltage, Types.Word.LOW);      //voltage
            dummyModeStatusChamberData[8] = Types.ConvertDOUBLEToWORD(voltage, Types.Word.HIGH);     //voltage

            dummyModeStatusChamberData[9] = Types.ConvertDOUBLEToWORD(curent, Types.Word.LOW);      //curent
            dummyModeStatusChamberData[10] = Types.ConvertDOUBLEToWORD(curent, Types.Word.HIGH);     //curent

            dummyModeStatusChamberData[11] = Types.ConvertDOUBLEToWORD(power, Types.Word.LOW);      //power
            dummyModeStatusChamberData[12] = Types.ConvertDOUBLEToWORD(power, Types.Word.HIGH);     //power


            dummyModeStatusChamberData[15] = flow1;   //actual flow 1
            dummyModeStatusChamberData[17] = flow2;   //actual flow 2
            dummyModeStatusChamberData[19] = flow3;   //actual flow 3

        }
        //-----------------------------------------------------------------------------------------
        /**
         *  Metoda ma za zadanie otwarcie połączenia z PLC
         */
        override public int Connect()
        {
            int aResult = -1;                //Return code
            lock (this)
            {
                try
                {
                    plc.Close();// Disconnect();

                    plc.ActCpuType = (int)typePLC;
                    plc.ActUnitType = (int)typeComm;
                    switch (typeComm)
                    {
                        case Types.TypeComm.USB:
                            plc.ActProtocolType = 0x0D;
                            break;
                        case Types.TypeComm.TCP:
                            plc.ActProtocolType = 0x05;
                            plc.ActHostAddress =  addressIP;
                            plc.ActTimeOut = 30;
                            break;
                    }
                    aResult = plc.Open();       //The Open method is executed.
                }
                catch (Exception exception)
                {
                    string aMessage = exception.Message;
                }
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie ustawienie bitu w przestrzeni M sterownika PLC
         */
        override public int SetDevice(string aAddr, int aState)
        {
            int aResult = -1;
            lock (this)//sync_Object)
            {
                try
                {
                    aResult = plc.SetDevice(aAddr, aState);
                }
                catch (Exception exception)
                {
                    string aMsg = exception.Message;
                }
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie pobranie bitu z przestrzeni M sterownika PLC
        */
        override public int GetDevice(string aAddr, out int aState)
        {
            int aResult = -1;
            lock (this)
            {

                aState = 0;
                try
                {
                    aResult = plc.GetDevice(aAddr, out aState);
                }
                catch (Exception exception)
                {
                    string aMsg = exception.Message;
                }
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie ustawienie danych w przestrzeni D sterownika PLC
        */
        override public int WriteWords(string aAddr, int aSize, int[] aData)
        {
            int aResult = -1;
            lock (this)
            {
                try
                {
                    aResult = plc.WriteDeviceBlock(aAddr, aSize, ref aData[0]);
                }
                catch (Exception exception)
                {
                    string aMsg = exception.Message;
                }
                //przypisz wartosc do tablicy
                if (dummyMode && aAddr == Types.ADDR_MODE_CONTROL)
                {
                    dummyModeStatusChamberData[27] = aData[0];
                    aResult = 0;
                }
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie pobranie danych z przestrzeni D sterownika PLC
        */
        override public int ReadWords(string aAddr, int aSize, int[] aData)
        {
            int aResult = -1;
            int aStartAddr = 1000;

            Int32.TryParse(aAddr.Remove(0, 1), out aStartAddr);

            lock (this)
            {
                //Jezeli jest dumy mode to pobierz mi spreparowane dane
                if (dummyMode && aAddr == Types.ADDR_START_STATUS_CHAMBER)
                {
                    GenerateRandomValueForDummyMode();
                    for (int i = 0; i < aData.Length;i++)
                        if(i < dummyModeStatusChamberData.Length)
                            aData[i] = dummyModeStatusChamberData[i];
                }
                //Brak trybu dummy - pobierz dane z PLC
                else
                {
                    //Pobierz  info na temat watku
                    System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
                    
                    //Jezeli jestem wywolywany z kolejki komunikatow to odczytaj dane w sposob noramlny
                    if (thread.Name  != "Thread_Read_Data_PLC")
                    {
                        try
                        {
                            aResult = plc.ReadDeviceBlock(aAddr, aSize, out aData[0]);
                            //Jezeli danych nie odczytalem poprawnie to zeruj bufor
                            if (aResult != 0)
                                for (int i = 0; i < aData.Length; i++)
                                    aData[i] = 0;
                        }
                        catch (Exception exception)
                        {
                            string aMsg = exception.Message;
                        }
                    }
                    else //Nie jestem wywolywany z kolejki komunikatow to odczytaj dame z wykorzystanime timera
                    {
                    /*    
                        //czekaj az timer zakonczy poprzedni przebieg max 100 ms
                        bool aDataFaild = false;
                        while (readBlockData)
                        {
                            if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - time > 2000)
                            {
                                aDataFaild = true;
                                break;
                            }
                            System.Threading.Thread.Sleep(5);
                        }
                        blockDataWasRead = false;
                        addrBlock = aAddr;
                        sizeBlock = aSize;
                        //Ustaw flage ze mozna odczytac dane z PLC (parametry zostaly zainicjalizowane) w timerze
                        readBlockData = true;
                        //Czekaj az dane zostana odczytane ale nie dluzej niz 100 ms
                        long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        while (!blockDataWasRead)
                        {
                            //Przekroczono czas oczekiwania na dane to opusc petle
                            if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - time >100)
                            {
                                aDataFaild = true;
                                break;
                            }
                            System.Threading.Thread.Sleep(5); // czekaj na dane
                        }
                        //kopiuj wynik do tablicy tylko gdy dane poprawnie zostaly odebrane
                        for (int i = 0; i < aSize; i++)
                        {
                            if (dataBlock.Length > i & aData.Length > i)
                                aData[i] = dataBlock[i];
                        }
                        aResult = resReadBlock;
                        if (aDataFaild)
                            aResult = 1; ; //Zglaszam problem ze dane nie zostaly odczytane w calosci
                       */
                    }                    
                }       
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie zapisanie wartosci zmienno przecinkowej pod dany adres
         */ 
        override public int WriteRealData(string aAddr, float aValue)
        {
            int aResult    = 0;
            int []aData    = new int[2];
            byte[]aBytes   = new byte[4];

            lock (this)
            {

                aBytes = BitConverter.GetBytes(aValue);         // przkonwertuj float na tablice bajtow

                aData[0] = (int)(aBytes[1] << 8 | aBytes[0]);   //zluz bajty w odpowiednie slowa
                aData[1] = (int)(aBytes[3] << 8 | aBytes[2]);

                aResult = WriteWords(aAddr, 2, aData);          //wgraj do PLC
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Metoda ma za zadanie odczytanie wartosci zmienno przecinkowej spod danego adresu
        */
        override public int ReadRealData(string aAddr, out float aValue)
        {
            int aResult = 0;

            int[] aData = new int[2];
            byte[] aBytes = new byte[4];

            lock (this)
            {

                aResult = ReadWords(aAddr, 2, aData);

                if (aResult == 0)
                {
                    aBytes[0] = (byte)(aData[0]  & 0xFF);
                    aBytes[1] = (byte)((aData[0] & 0xFF00) >> 8);
                    aBytes[2] = (byte)(aData[1]  & 0xFF);
                    aBytes[3] = (byte)((aData[1] & 0xFF00) >> 8);
                }
                aValue = BitConverter.ToSingle(aBytes, 0);
            }
            return aResult;
        }
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda jest wykorzystywana do odczytywania bloku danych jezeli dane sa pobierane przez watek nie bedacy kolejka komunikatow. Z niewiadomych przyczn funkcja GetDeviceBlock wywolywana z innego watku niz kolejka komunikatow wywala sie
         */
        /* private void ReadBlockData(object sender, EventArgs e)
         {
             //Timer kreci sie caly czas ale dane odczytaj tylko wtedy kiedy ustawie flage
             if (readBlockData)
             {
                 try
                 {
                     resReadBlock = plc.ReadDeviceBlock2(addrBlock, sizeBlock, out dataBlock[0]);
                     //Jezeli danych nie odczytalem poprawnie to zeruj bufor
                     if (resReadBlock != 0)
                         for (int i = 0; i < dataBlock.Length; i++)
                             dataBlock[i] = 0;
                 }
                 catch (Exception ex)
                 {
                     string aMsg = ex.Message;

                 }
                 finally { blockDataWasRead = true; }
            }
            readBlockData    = false;
         }
         */
        //-----------------------------------------------------------------------------------------
        /**
         * Metoda ma za zadanie sprawdzenie czy zalogowany user posiada uprawnienia do zapisu danych do danej przestrzeni
         */ 
        private bool CheckUserPriviliges(string addrMemoryPLC)
        {
            bool res = false;
            int addr = 0;

            if (!Int32.TryParse(addrMemoryPLC.Remove(0, 1), out addr))
                addr = 0;

            //Ustaw dls wszystkich uprawnienie do czytania bledow oraz mozlikwosc sterowania programami
            if (addrMemoryPLC == Types.ADDR_FLAG_WAS_READ   || addrMemoryPLC == Types.ADDR_REQ_COUNT_SEGMENT || addrMemoryPLC == Types.ADDR_FINISH_COUNT_SEGMENT || addrMemoryPLC == Types.ADDR_FLAG_INIT_ERROR ||
                addrMemoryPLC == Types.ADDR_CONTROL_PROGRAM || addrMemoryPLC == Types.ADDR_PRG_ID            || addrMemoryPLC == Types.ADDR_PRG_SEQ_COUNTS       || addrMemoryPLC == Types.ADDR_START_BUFFER_PROGRAM)
                res = true;
            //Jezeli jestem zalogowany jako operator to nie moge nic zmieniac recznie
            switch(ApplicationData.LoggedUser.Privilige)
            {
                //Operator moze wykonac tylko LeakTest
                case Types.UserPrivilige.Operator:
                    if(addrMemoryPLC == Types.ADDR_LEAK_TEST || addrMemoryPLC == Types.ADDR_SETPOINT_LEAKTEST || addrMemoryPLC == Types.ADDR_TIME_DURATION_LEAKTEST)
                        res = true;
                    break;
                //Technik ma dostep do wszystkiego poza ustawieniami serwisowymi
                case Types.UserPrivilige.Technican:
                    if(addr < 1400 || addr > 1445)
                        res = true;
                    break;
                //Administrator ma dostep do wszystkiego poza ustawieniami serwisowymi
                case Types.UserPrivilige.Administrator:
                    if (addr < 1400 || addr > 1445)
                        res = true;
                    break;
                //Serwis ma dostep do wszystkiego
                case Types.UserPrivilige.Service:
                    res = true;
                    break;
            }

            return res;
        }
        //-----------------------------------------------------------------------------------------
    }
}
