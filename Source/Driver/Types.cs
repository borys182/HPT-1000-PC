using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Chamber;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HPT1000.Source.Driver
{
    /// <summary>
    /// PLik zawiera definicje typow,  adresy komorek
    /// </summary>

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    // Struktura opisuje dane parametru urzadzenia zapisywanego w bazie danych
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public struct DataBaseData
    {
        public int      ID_Para;
        public double   Value;
        public Value    ValuePtr;
        public string   Unit;
        public DateTime Date;
        public int      ProcesID;
    };
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Struktura opisuje dane sesji danych pobrane z bazy danych
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public struct Sesion
    {
        public int ID;
        public DateTime StartDate;
        public DateTime EndDate;
        public string   ProcesName;
        public Program.ProcesParameter ProcesParameter;

        public override string ToString()
        {
            return ProcesName;
        }
        public override bool Equals(object other)
        {
            bool aRes = false;

            if (other != null && this.GetType() == other.GetType())
            {
                Sesion sesion = (Sesion)other;
                if (sesion.ProcesName == ProcesName)
                    aRes = true;
            }
            return aRes;
        }
    };
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Struktura opisuje parametr urzadzenia
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public struct DataBaseDevice
    {
        public int      DeviceID;
        public int      ParaID;
        public string   DeviceName;
        public string   ParaName;
    };
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Struktura opisuje konfigruacje zapisu parametru urzadzenia w bazie danych
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public struct ConfigPara
    {
        public int      ID;
        public int      ParameterID;
        public double   Frequency;
        public double   Difference;
        public bool     Enabled;
        public int      Mode;
    };
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Struktrua zawiera informacje na temat danego parametru programu przechowywanego w bazie danych
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public struct ProgramParameter
    {
        public int ID;
        public string Name;
        public string Data;
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Klasa jest odpowiezilana za przechowanie danych dla wszystkoch seri w danym czasie - wykorzystywana do eksportou raportu do csv
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    class ItemChartCSV : IComparable<ItemChartCSV>
    {
        public double Time;
        public double Pressure;
        public double Power;
        public double Flow1;
        public double Flow2;
        public double Flow3;
        public double Motor1;
        public double Motor2;
        public double DosigValve;

        //-------------------------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            bool aRes = false;
            if (obj != null && obj is ItemChartCSV)
            {
                ItemChartCSV itemCSV = (ItemChartCSV)obj;
                if (itemCSV.Time == Time && itemCSV.Pressure == Pressure)
                    aRes = true;
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------
        // Default comparer for Part type.
        public int CompareTo(ItemChartCSV compareObj)
        {
            int res = 0;
            // A null value means that this object is greater.
            if (compareObj == null || !(compareObj is ItemChartCSV))
                res = 1;
            else
            {
                ItemChartCSV obj = (ItemChartCSV)compareObj;
                if (obj.Time > Time)
                    res = -1;
                if (obj.Time < Time)
                    res = 1;
            }
            return res;
        }
        //-------------------------------------------------------------------------------------
    };
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Klasa zawierta wszelkie informacje na temat adresow i mozliwuch typow danych wykorzystywanych przez aplikacje
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class Types
    {
        /// <summary>
        /// TYPY WYLICZENIOWE
        /// </summary>
        public enum TypeValve       { SV = 0, VV = 1, Purge = 2, Gas = 3, None }; //Kolejnosc zaworow odpowiada kolejnosci stanow zaworow przesylanych w zbiorczym DWORD z PLC
        public enum DriverStatus    { Unknown = 0, OK = 1, NoComm = 2, Error = 3, Warning = 4, DummyMode };
        public enum StateValve      { Unknown = 0, Close = 1, Open = 2, Error = 3, HalfOpen = 4 };
        public enum StateFP         { OFF   = 1, ON   = 2, Error = 3 };
        public enum StateHV         { OFF   = 1, ON   = 2, Error = 3 };
        public enum UnitFlow        { sccm, percent, ms };
        public enum ModeHV          { Power = 1 , Voltage = 2 , Curent = 3, Unknown = 4};
        public enum TypeObject      { None, VL, FP, HV, FM, VP, PC, INT, MNT, MTR1, MTR2, GAUGE}; //VL - vavle , FP - fore pump , HV - power suplay , FN - flow meter, VP - Vaporizer, PC-pressure control
        public enum ControlProgram  { Start = 1,Stop = 2,Resume = 3 };
        public enum WorkModeHV      { Power = 1, Voltage = 2, Curent = 3};
        public enum Word            { LOW , HIGH};
        public enum GasProcesMode   { Unknown = 0, FlowSP = 1, Presure_MFC = 2, Pressure_Vap = 3}; //okreslenie sposobu sterowania gazami w komorze {Presure_MFC - proznia jest utrzymywana przez PID z 3 przeplywek, Pressure_Vap proznia jest utrzymywana przez PID z vaporatora, FlowSP - sterujemy zgodnie z ustawionymi setpontami}
        public enum StatusProgram   { Unknown = 0 , Run = 1 , Stop = 2, Error = 3, Done = 4, Suspended = 5, Wait = 6 , Warning = 7};
        public enum ControlMode     { Automatic , Manual , None}
        public enum AddressSpace    { Settings, Program, Status,ExtraSettings}; // ExtraSettings okresla obszar pamieci dodatkow wygospodarowany w PLC poniewaz program sie rozrosl tak ze w pierwotnym miejscu braklo juz miejsca
        public enum Language        { English = 1 };
        public enum Mode            { Automatic = 1, Manual = 2, None = 3 };
        public enum TypeComm        { USB = 0x51, TCP = 0x52 };
        public enum TypePLC         { L = 0xA1 };
        public enum UserPrivilige   { None = 0, Administrator = 1, Operator = 2, Service = 3, Technican = 4};
        public enum TypeInterlock   { Door = 0 , VacuumSwitch = 1 , InterlockHV = 2 , ThermalSwitch = 3 , EmgStop = 4, None = 10 }
        public enum TypeDisableAccount  { Temporarily = 1 , Immediately = 2 , Access = 3  }
        public enum ModeAcq         { Frequency = 1 , Difference = 2 , Mixed = 3}
        public enum VaporaizerType  { None = 0, Cycle = 1 , Dosing = 2 }
        public enum MessageType     { Error = 1, Warrning = 2, Message = 3 };
        public enum PID             { Kp, Ti , Td, Ts, Filtr  };
        public enum KindOperation   { SetDate , SetTime};
        public enum TimeMaintenance  { Interval = 1 , Time = 2 };
        public enum StateLeaktest   {None = 0 , Stop = 1 ,Error = 2 , Run_PumpDown = 3 , Run_MesureLeak = 4 }
        public enum TypeScalible    { Width , Heigh }
        public enum SeriesType      { Pressure , Power , Current , Voltage , Flow1 , Flow2 , Flow3, Motor1 , Motor2, DosingValve  }
        public enum Heading { Feauture , Improvment , Bug}
        public enum TypeRangeHV { Min = 1 , Max = 2}
        public enum GaugeType {None = 0 , Pirani = 1 , Barotron = 2}

        //Okreslenie zdrodla pochodzenia eventu ktory generuje nam wiadomosc do systemu. Jest to wymagane do szukania powiazanego z nim textu informacji - dla mxComponent generujemy dodatkowe info na temat bledu
        public enum EventCategory
        {
            APLICATION      = 0x01,
            MX_COMPONENTS   = 0x02,
            PLC             = 0x03,
            MESSAGE         = 0x04
        }
        public enum EventType
        {
            NONE                        = 0x00,
            PLC_PTR_NULL                = 0x01,         //Brak wskaznika na obiekt protokolu PLC
            CALL_INCORRECT_OPERATION    = 0x02,         //Wywolanie zabronienioej operacji na danym obiekcie
            BAD_FLOW_ID                 = 0x03,         //proba wykonia zapisu do plc info o przeplywkach o id roznym niz 0-2 (innych nie ma w plc)
            BAD_CYCLE_TIME              = 0x04,          //Podana wartosc cyklu szybkiego zaworu jest mniejsz niz czas wlaczenia
            BAD_ON_TIME                 = 0x05,          //Podana wartosc czasu wlaczenia zaworu szybkieg jest wieksza niz czas cyklu
            NO_PRG_IN_PLC               = 0x06,          //Brak programu w PLC   
            PLC_ERROR                   = 0x07,         //Sygnalizacja wystapienia bledu w programie PLC     
            SET_MAX_FLOW                = 0x08,
            SET_RANGE_VOLTAGE_MFC       = 0x09,
            SET_TIME_FLOW_STABILITY     = 0x0A,
            SET_FLOW                    = 0x0B,
            UPDATE_SETINGS              = 0x0C,
            WRITE_PROGRAM               = 0x0D,
            START_PROGRAM               = 0x0E,
            STOP_PROGRAM                = 0x0F,
            SET_PRESSURE_SETPOINT       = 0x10,
            SET_SETPOINT_HV             = 0x11,
            SET_MODE                    = 0x12,
            SET_OPERATE_HV              = 0x13,
            SET_LIMIT_POWER_HV          = 0x14,
            SET_LIMIT_CURRENT_HV        = 0x15,
            SET_LIMIT_VOLTAGE_HV        = 0x16,
            SET_MAX_VOLTAGE_HV          = 0x17,
            SET_MAX_POWER_HV            = 0x18,
            SET_MAX_CURENT_HV           = 0x19,
            SET_WAIT_TIME_OPERATE_HV    = 0x1A,
            SET_WAIT_TIME_SETPOINT_HV   = 0x1B,
            CONTROL_PUMP                = 0x1C,
            SET_WIAT_TIME_PF            = 0x1D,
            SET_TIME_PUMP_TO_SV         = 0x1E,
            SET_CYCLE_TIME              = 0x1F,
            SET_ON_TIME                 = 0x20,
            SET_STATE_VALVE             = 0x21,
            SET_MODE_PRESSURE           = 0x22,
            NO_SELECT_PROGRAM_TO_RUN    = 0x23,
            SET_MODE_CONTROL            = 0x24,
            MX_COMPONENTS_NO_INSTALL    = 0x25,
            SET_DOSING                  = 0x26,
            SET_MODE_VAPOR              = 0x27,
            SET_ENABLED_MFC             = 0x28,
            SET_TIME_STABILITY_PRESSURE = 0x29,
            SET_PID                     = 0x2A,
            SET_DATE_TIME               = 0x2B,
            START_LEAKTEST              = 0x2C,
            CLEAR_FP_TIME               = 0x2D,
            MAINTANANCE                 = 0x2E,
            SET_LEAKTEST_PARA           = 0x2F,
            STOP_LEAKTEST               = 0x30,
            MOTOR_CONTROL               = 0x31,
            GAS_CONTROL                 = 0x32,
            SET_PRESSURE_LIMIT_HV       = 0x33,
            SET_PRESSURE_LIMIT_GAS      = 0x34,
            SET_GAUGE_FACTOR            = 0x35,
            SET_MFC_FACTOR              = 0x36,
            SET_MFC_CALIBRSTED_FACTOR   = 0x37,
            SET_THERSHOLD_MIX_GAS       = 0x38,
            SET_ACTIVE_PROC_MIX_GAS     = 0x39,
            SET_TIME_FLOW_STABILITY_MIX_GAS  = 0x3A,
            SET_RANGE_VOLTAGE_HV        = 0x3B,
            SET_MODE_PID_HV             = 0x3C,
        }

        /// <summary>
        /// ADRESY KOMOREK PLC
        /// </summary>

        public static string ADDR_FLAG_WAS_READ             = "M300";
        public static string ADDR_FLAGE_SERVICE_MODE        = "M301";
        public static string ADDR_FLAGE_CLEAR_ERROR         = "M302";
        public static string ADDR_REQ_COUNT_SEGMENT         = "M303";
        public static string ADDR_FINISH_COUNT_SEGMENT      = "M304";
        public static string ADDR_SET_DATE_TIME_PLC         = "M305";
        public static string ADDR_LEAK_TEST                 = "M306";
        public static string ADDR_CLEAR_TIME_MACHINE        = "M307";
        public static string ADDR_CLEAR_TIME_FP             = "M308";
        public static string ADDR_FLAG_INIT_ERROR           = "M309";


        //Adresy komorek do sterowania recznego
        public static string ADDR_VALVES_CTRL               = "D200";
        public static string ADDR_FP_CTRL                   = "D202";
        public static string ADDR_CYCLE_TIME                = "D203";
        public static string ADDR_ON_TIME                   = "D205";
        public static string ADDR_FLOW_1_CTR                = "D207";
        public static string ADDR_FLOW_2_CTR                = "D209";
        public static string ADDR_FLOW_3_CTR                = "D211";
        public static string ADDR_POWER_SUPPLAY_SETPOINT    = "D213";
        public static string ADDR_POWER_SUPPLAY_MODE        = "D215";
        public static string ADDR_POWER_SUPPLAY_OPERATE     = "D216";
        public static string ADDR_PRESSURE_SETPOINT         = "D217";
        public static string ADDR_PRESSURE_MODE             = "D219";
        public static string ADDR_MODE_CONTROL              = "D220";  //Tryb sterowania komora albo manulany albo automatyczny
        public static string ADDR_DOSING                    = "D222";
        public static string ADDR_DATE_TIME                 = "D223";
        public static string ADDR_SETPOINT_LEAKTEST         = "D226";
        public static string ADDR_TIME_DURATION_LEAKTEST    = "D228";
        public static string ADDR_MOTOR_1_DRIVER            = "D229";
        public static string ADDR_MOTOR_2_DRIVER            = "D230";
        public static string ADDR_GAS_CONTROL               = "D231"; //Sterowanie bitowe wlaczaniem/wylaczaniem gazow (bit 1 - FLOW1 , 2 - FLOW2, 3 - FLOW3, 4 -Vaporiser)
        public static string ADDR_MFC1_PERCENT_CONTROL      = "D232";
        public static string ADDR_MFC2_PERCENT_CONTROL      = "D233";
        public static string ADDR_MFC3_PERCENT_CONTROL      = "D234";
        public static string ADDR_MFC1_FACTOR_CONTROL       = "D60";    //Musza byc w tym miejscu bo sa to wartosci przechowywane w pamieci bateryjnej
        public static string ADDR_MFC2_FACTOR_CONTROL       = "D62";    //Musza byc w tym miejscu bo sa to wartosci przechowywane w pamieci bateryjnej
        public static string ADDR_MFC3_FACTOR_CONTROL       = "D64";    //Musza byc w tym miejscu bo sa to wartosci przechowywane w pamieci bateryjnej

        public static string ADDR_START_STATUS_CHAMBER      = "D1000"; //poczatek bufora z danymi przedstawiajacymi stan systemu 
        public static string ADDR_CONTROL_PROGRAM           = "D1055";
        public static int    ADDR_START_CRT_PROGRAM         = 1070;   //Adres parametrow aktualnie wykonywanego programu i wykorzystuje go do dostrajania parametrow programu. Jest to adres poczatku buforu danych gdzie sa przechowywane parametry aktualnie wykonywanego programu
                                                                      //Pamietaj ze ten adres jest odzwierciedleniem PLC. Kolejne parametry urządzen posiadaja adresy zgodnie z ofsetem danego parametru w programie
   //     public static string ADDR_SUBPROGRAMS_ID            = "D1222";

        public static string ADDR_START_EXTRA_SETTINGS      = "D50";
        public static string ADDR_START_SETTINGS            = "D1400";
        public static string ADDR_BUFER_ERROR               = "D1460";
        public static string ADDR_PRG_ID                    = "D2750";
        public static string ADDR_PRG_SEQ_COUNTS            = "D2751";
        public static string ADDR_START_BUFFER_PROGRAM      = "D2752";


        /// <summary>
        /// OFFSET KONKRETNYCH DANYCH ODCZYTANYCH W ZBIORCZYM BUFORZE Z PLC
        /// </summary>

        //Dane odczytywane ciagle w watku
        public static int OFFSET_DEVICE_STATUS  = 0;
        public static int OFFSET_PRESSURE       = 1;
        public static int OFFSET_STATE_LEAKTEST = 3;
        public static int OFFSET_STATE_FP       = 4;
        public static int OFFSET_STATUS_HV      = 5;
        public static int OFFSET_MODE_HV        = 6;
        public static int OFFSET_VOLTAGE        = 7;
        public static int OFFSET_CURENT         = 9;
        public static int OFFSET_POWER          = 11;
        public static int OFFSET_STATE_VALVES   = 13;
        public static int OFFSET_ACTUAL_FLOW_1  = 15;
        public static int OFFSET_ACTUAL_FLOW_2  = 17;
        public static int OFFSET_ACTUAL_FLOW_3  = 19;
        public static int OFFSET_CYCLE_TIME     = 21;
        public static int OFFSET_ON_TIME        = 23;
        public static int OFFSET_MODE_PRESSURE  = 25;
        public static int OFFSET_OCCURED_ERROR  = 26;
        public static int OFFSET_MODE           = 27;
        public static int OFFSET_SETPOINT_HV    = 28;
        public static int OFFSET_SETPOINT_GAS   = 30;
        public static int OFFSET_SETPOINT_MFC1  = 32;
        public static int OFFSET_SETPOINT_MFC2  = 33;
        public static int OFFSET_SETPOINT_MFC3  = 34;
        public static int OFFSET_INTERLOCKS     = 35;
        public static int OFFSET_DOSING         = 36;
        public static int OFFSET_MODE_VAPOR     = 37;
        public static int OFFSET_STATUS_PLC     = 38;
        public static int OFFSET_ACTUAL_TIME_LEAKTEST = 39;
        public static int OFFSET_FP_TIME_WORK   = 40;
        public static int OFFSET_NUMBER_PROCESS = 41;
        public static int OFFSET_OPERATING_HOUR = 42;
        public static int OFFSET_STATE_MOTOR    = 43;   //< Status motora bebna
        public static int OFFSET_STATE_GAS_CTR  = 44;   //< status wlaczenia gazow MFC1,2,3 i vaporizera
        public static int OFFSET_MFC1_FACTOR    = 45;
        public static int OFFSET_MFC2_FACTOR    = 47;
        public static int OFFSET_MFC3_FACTOR    = 49;
        public static int OFFSET_ACTULA_RANGE_MFC1 = 51;
        public static int OFFSET_ACTULA_RANGE_MFC2 = 52;
        public static int OFFSET_ACTULA_RANGE_MFC3 = 53;
        public static int OFFSET_INTEGRAL_SEQ_DATA = 54;

        //Dane przechowywane w "ExtraSettings"
        public static int OFFSET_MAX_TIME_PUMPDOWN  = 7;
        public static int OFFSET_SETPOINT_PUMPDOWN  = 8;
        public static int OFFSET_GAUGE_TYPE         = 16;

        //Dane odczytywane na zdarzenie
        public static int OFFSET_HV_LIMIT_POWER     = 0;
        public static int OFFSET_HV_LIMIT_CURENT    = 2;
        public static int OFFSET_HV_LIMIT_VOLTAGE   = 4;
        public static int OFFSET_HV_MAX_POWER       = 6;
        public static int OFFSET_HV_MAX_CURENT      = 8;
        public static int OFFSET_HV_MAX_VOLTAGE     = 10;
        public static int OFFSET_HV_WAIT_OPERATE    = 12;
        public static int OFFSET_HV_WAIT_SETPOINT   = 13;
        public static int OFFSET_RANGE_FLOW_MFC1    = 14;
        public static int OFFSET_RANGE_FLOW_MFC2    = 15;
        public static int OFFSET_RANGE_FLOW_MFC3    = 16;
        public static int OFFSET_RANGE_VOLTAGE_MFC1 = 17;
        public static int OFFSET_RANGE_VOLTAGE_MFC2 = 18;
        public static int OFFSET_RANGE_VOLTAGE_MFC3 = 19;
        public static int OFFSET_TIME_FLOW_STABILITY = 20;
        public static int OFFSET_TIME_PUMP_TO_SV    = 21;
        public static int OFFSET_TIME_WAIT_PF       = 22;
        public static int OFFSET_GAUGE_FACTOR       = 23;
        public static int OFFSET_MIN_RANGE_VOLTAGE_HV = 25;
        public static int OFFSET_MAX_RANGE_VOLTAGE_HV = 26;
        public static int OFFSET_TYPE_VAPORAIZER    = 27;  //Tryb sterowania dla vaporaizera albo cyklicznie albo na dozowanie gazu
        public static int OFFSET_ENABLED_MFC        = 28;
        public static int OFFSET_TIME_STABILITY_PRE = 29;  //CZAS STABILIZACJI CISNIENIA/PRZEPLWYU W MODZIE KONTROLUJACYM CISNIENIE Z REGULATROA PID
        public static int OFFSET_LIMIT_PRESSURE_HV  = 30;
        public static int OFFSET_LIMIT_PRESSURE_GAS = 32;
        public static int OFFSET_PID_Kp             = 34;
        public static int OFFSET_PID_Ti             = 35;
        public static int OFFSET_PID_Td             = 36;
        public static int OFFSET_PID_Ts             = 37;
        public static int OFFSET_PID_FILTR          = 38;
        public static int OFFSET_MODE_PID_HV        = 39;
        public static int OFFSET_DATE_TIME          = 40;
        public static int OFFSET_DATE_TIME_YER_MON  = 40;
        public static int OFFSET_DATE_TIME_DAY_HUR  = 41;
        public static int OFFSET_DATE_TIME_MIN_SEC  = 42;
        public static int OFFSET_VERSION_PROGRAM    = 43;
        public static int OFFSET_SUBVERSION_PROGRAM = 44;
        public static int OFFSET_MFC1_CALIBRATED_FACTOR = 45;
        public static int OFFSET_MFC2_CALIBRATED_FACTOR = 47;
        public static int OFFSET_MFC3_CALIBRATED_FACTOR = 49;
        public static int OFFSET_THRESHOLD_MIX_GAS   = 51;
        public static int OFFSET_ACTIVE_PROC_MIX_GAS = 52;
        public static int OFFSET_TIME_FLOW_STABILITY_MIX_GAS = 53;
        public static int OFFSET_PID_HV_Kp          = 54;
        public static int OFFSET_PID_HV_Ti          = 55;
        public static int OFFSET_PID_HV_Td          = 56;
        public static int OFFSET_PID_HV_Ts          = 57;
        public static int OFFSET_PID_HV_FILTR       = 58;
      
        //Dane aktualnie wykonywanego programu i subprogramu odczytywane ciagle
        public const int LENGHT_STATUS_DATA         = 228;  //Rozmiar przestrzenie pamieci ktora przechowuje dane o statusach urzadzeni i stanie programow - dane te sa odczytywane ciagle w watku
        public const int LENGHT_SETTINGS_DATA       = 60;   //Rozmiar danych statusowych odczytywanych ciagle ale nie bedacych zwiazane z procesami
        public const int LENGHT_EXTRA_SETTINGS_DATA = 20;   //Rozmiar danych statusowych odczytywanych ciagle ale nie bedacych zwiazane z procesami
        public const int SEGMENT_SIZE               = 58;   //Rozmiar parametrow subprogramu
        public const int MAX_SEGMENTS               = 90;  //Max liczba segmentow z ktorych moze sie skladac program po stronie PLC

        public static int SIZE_PRG_DATA             = 173;  //Rozmiar danych powiazanych z programami - parametry aktualnego programu oraz statusy wszystkich subprogramow (58 + 15 + 100) ;
        public static int OFFSET_PRG_DATA           = 55;   //Okreslenie poczatku gdzie sie znajduje dane na temat aktualnie wykonywanego progrmau w odczytanym buforze
        public static int OFFSET_STATUS_DATA        = 73;   //Wskazanie poczatku danych ze statusami kolejnych subprogramow w przestrzeni danych posiadajacych dane z programami. Statusy kolejnych subprogramow znajduja sie zaraz za parametrami danego programu

        public const int  COUNT_ERROR_PLC           = 25;
        public const  int SIZE_ERROR_BUFFER_PLC     = COUNT_ERROR_PLC * 6 + 2;  //Rozmiar bufora jest mnozony razy 6 poniewaz jeden wpis bledu zajmuje 4 WORDY a 2 wziela sie z odczytu za jednym razem info p StartIndex i CountError

        public static int OFFSET_PRG_CONTROL        = 0;
        public static int OFFSET_PRG_STATUS         = 1;
        //Grupa offsetow danych z ktorych sklada sie jeden segemnet programu
        public static int OFFSET_PRG_ACTUAL_PRG_ID  = 3;
        public static int OFFSET_PRG_ACTUAL_SEQ_ID  = 4;
        public static int OFFSET_PRG_TIME_PUMP      = 5;
        public static int OFFSET_PRG_TIME_GAS       = 6;
        public static int OFFSET_PRG_TIME_HV        = 7;
        public static int OFFSET_PRG_TIME_VENT      = 8;
        public static int OFFSET_PRG_TIME_FLUSH     = 9;
        public static int OFFSET_PRG_TIME_MOTOR     = 10;
        public static int OFFSET_PRG_SEQ_DATA       = 15;   //Poaczatek parametrow dla programow wchodzacych w sklada danego segmetnu

        public static int OFFSET_ERR_START_INDEX    = 0;
        public static int OFFSET_ERR_COUNTS_INDEX   = 1;
        public static int OFFSET_ERR_BUFFER_INDEX   = 2;

        //Offset od bazowego adresu dla kolejnych parametrow subprogramu
        public static int OFFSET_SEQ_CMD                = 0;
        public static int OFFSET_SEQ_STATUS             = 2;
        public static int OFFSET_SEQ_PUMP_MAX_TIME      = 3;
        public static int OFFSET_SEQ_PUMP_SP            = 4;
        public static int OFFSET_SEQ_VENT_TIME          = 6;
        public static int OFFSET_SEQ_FLUSH_TIME         = 7;
        public static int OFFSET_SEQ_DELAY_TIME         = 8;
        public static int OFFSET_SEQ_CHECK_VACUUM       = 9;
        public static int OFFSET_SEQ_MOTOR_1_CMD        = 11;
        public static int OFFSET_SEQ_MOTOR_1_TIME       = 12;
        public static int OFFSET_SEQ_MOTOR_2_CMD        = 13;
        public static int OFFSET_SEQ_MOTOR_2_TIME       = 14;
        public static int OFFSET_SEQ_HV_OPERATE         = 17;
        public static int OFFSET_SEQ_HV_SETPOINT        = 18;
        public static int OFFSET_SEQ_HV_DRIFT_SETPOINT  = 20;
        public static int OFFSET_SEQ_HV_TIME            = 22;
        public static int OFFSET_SEQ_FLOW_1_FLOW        = 23;
        public static int OFFSET_SEQ_FLOW_1_MIN_FLOW    = 24;
        public static int OFFSET_SEQ_FLOW_1_MAX_FLOW    = 25;
        public static int OFFSET_SEQ_FLOW_1_SHARE       = 26;
        public static int OFFSET_SEQ_FLOW_1_DEVIATION   = 27;
        public static int OFFSET_SEQ_FLOW_2_FLOW        = 28;
        public static int OFFSET_SEQ_FLOW_2_MIN_FLOW    = 29;
        public static int OFFSET_SEQ_FLOW_2_MAX_FLOW    = 30;
        public static int OFFSET_SEQ_FLOW_2_SHARE       = 31;
        public static int OFFSET_SEQ_FLOW_2_DEVIATION   = 32;
        public static int OFFSET_SEQ_FLOW_3_FLOW        = 33;
        public static int OFFSET_SEQ_FLOW_3_MIN_FLOW    = 34;
        public static int OFFSET_SEQ_FLOW_3_MAX_FLOW    = 35;
        public static int OFFSET_SEQ_FLOW_3_SHARE       = 36;
        public static int OFFSET_SEQ_FLOW_3_DEVIATION   = 37;
        public static int OFFSET_SEQ_FLOW_4_ON_TIME     = 38;
        public static int OFFSET_SEQ_FLOW_4_CYCLE_TIME  = 40;
        public static int OFFSET_SEQ_GAS_MODE           = 42;
        public static int OFFSET_SEQ_GAS_TIME           = 43;
        public static int OFFSET_SEQ_GAS_SETPOINT       = 44;
        public static int OFFSET_SEQ_GAS_UP_DIFFER      = 46;
        public static int OFFSET_SEQ_GAS_DOWN_DIFFER    = 48;
        public static int OFFSET_SEQ_DOSING             = 50; 
        public static int OFFSET_SEQ_ID                 = 51; // ID Subprogrmu pod jakim dany subprogram jest widziany w bazie danych
        public static int OFFSET_SEQ_FLOW_1_FACTOR      = 52; // Faktor gazu podpietego pod przeplywke 1
        public static int OFFSET_SEQ_FLOW_2_FACTOR      = 54; // Faktor gazu podpietego pod przeplywke 1
        public static int OFFSET_SEQ_FLOW_3_FACTOR      = 56; // Faktor gazu podpietego pod przeplywke 1
        

        //Bity w slowie komendy programu dla kolejnych funkcji
        public static int BIT_CMD_PUMP              = 0;
        public static int BIT_CMD_PUMP_BUTTERFLY    = 1;
        public static int BIT_CMD_STOP              = 2;
        public static int BIT_CMD_VENT              = 3;
        public static int BIT_CMD_FLUSH             = 4;
        public static int BIT_CMD_HV                = 5;
        public static int BIT_CMD_FLOW_1            = 6;
        public static int BIT_CMD_FLOW_2            = 7;
        public static int BIT_CMD_FLOW_3            = 8;
        public static int BIT_CMD_FLOW_4            = 9;
        public static int BIT_CMD_MOTOR1            = 10;
        public static int BIT_CMD_MOTOR2            = 11;
        public static int BIT_CMD_DELAY             = 12;
        public static int BIT_CMD_CHECK_VACUM       = 13;
        public static int BIT_CMD_PRESSURE          = 14;
        public static int BIT_CMD_END               = 15;

        /// <summary>
        /// Pomocniecze funkcje
        /// </summary>
        //--------------------------------------------------------------------------------------------
        public static int ConvertWORDToInt(int aWordData)
        {
            int aValue = 0;
            try
            {
                aValue = Int16.Parse(aWordData.ToString());
            }
            catch(Exception e)
            {
                aWordData =(~aWordData) & 0xFFFF;
                aValue = Int16.Parse(aWordData.ToString()) * -1;
            }
                return aValue;
        }
        //--------------------------------------------------------------------------------------------
        public static double ConvertDWORDToDouble(int[] aData, int aIndex)
        {
            double aValue = 0;
            byte[] aBytes = new byte[4];

            aBytes[0] = (byte)( aData[aIndex]    & 0xFF);
            aBytes[1] = (byte)((aData[aIndex]    & 0xFF00) >> 8);
            aBytes[2] = (byte)( aData[aIndex +1] & 0xFF);
            aBytes[3] = (byte)((aData[aIndex +1] & 0xFF00) >> 8);

            aValue = BitConverter.ToSingle(aBytes, 0);

            return aValue;
        }
        //--------------------------------------------------------------------------------------------
        //Funkcaj ma za zadanie przekonwertowanie float na dwa wordy i zwrocenie mlodszego badz starszego
        public static int ConvertDOUBLEToWORD(double aValue,Word whichWord )
        {
            int aWord = 0;
            byte[] aBytes = new byte[4];
            aBytes = BitConverter.GetBytes((float)aValue);         // przkonwertuj float na tablice bajtow

            if (whichWord == Word.HIGH)
                aWord = (int)(aBytes[3] << 8 | aBytes[2]);
            else
                aWord = (int)(aBytes[1] << 8 | aBytes[0]);

            return aWord;
        }
        //--------------------------------------------------------------------------------------------
        public static DateTime ConvertDate(int aSeconds)
        {
            DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            
            int aHour   = aSeconds / (3600);
            int aMinute = (aSeconds - aHour * 3600) / 60;
            int aSecond = aSeconds - aHour * 3600 - aMinute * 60;

            try
            {
                aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,aHour,aMinute,aSecond);
            }
            catch(Exception ex)
            {
                Logger.AddException(ex);
            }

            return aDateTime;
        }
        //--------------------------------------------------------------------------------------------
        public static string GetAddress(AddressSpace aTypeSpace , int aOffsetAddr)
        {
            string  aAddrRes = "D0";
            int     aSpaceAddr = 0;

            switch(aTypeSpace)
            {
                case AddressSpace.Settings:
                    Int32.TryParse(ADDR_START_SETTINGS.Remove(0,1),out aSpaceAddr);
                    aAddrRes = "D" + (aSpaceAddr + aOffsetAddr).ToString();
                    break;

                case AddressSpace.Program:
                    Int32.TryParse(ADDR_START_BUFFER_PROGRAM.Remove(0, 1), out aSpaceAddr);
                    aAddrRes = "D" + (aSpaceAddr + aOffsetAddr).ToString();
                    break;
                case AddressSpace.Status:
                    Int32.TryParse(ADDR_START_STATUS_CHAMBER.Remove(0, 1), out aSpaceAddr);
                    aAddrRes = "D" + (aSpaceAddr + aOffsetAddr).ToString();
                    break;
                case AddressSpace.ExtraSettings:
                    Int32.TryParse(ADDR_START_EXTRA_SETTINGS.Remove(0, 1), out aSpaceAddr);
                    aAddrRes = "D" + (aSpaceAddr + aOffsetAddr).ToString();
                    break;
            }
            return aAddrRes;
        }
        //--------------------------------------------------------------------------------------------
        /**
         * Zadaniem metody jest konwersja ciagu bajtow odczytanego za bazy na obiekt reprezentyjacy podstawowe parametry programu kotre zostaly w bazie zapisane
         */ 
        public static Program.ProcesParameter ConvertByteToProcesParameter(byte[] bytes)
        {
            Program.ProcesParameter procesPara  = new Program.ProcesParameter(null);
            MemoryStream            memory      = new MemoryStream();
            BinaryFormatter         binary      = new BinaryFormatter();
            memory.Write(bytes, 0, bytes.Length);
            memory.Seek(0,SeekOrigin.Begin);
            try
            {
                procesPara = (Program.ProcesParameter)binary.Deserialize(memory);
            }
            catch(Exception ex)
            {
                procesPara = null;
            }

            return procesPara;
        }
        //--------------------------------------------------------------------------------------------
        public static int[] ConvertToInt(string[] chars)
        {
            int[] tab = new int[10];
            int index = 0;

            foreach (string c_nr in chars)
            {
                if (index < 10)
                {
                    try { tab[index] = Convert.ToInt32(c_nr); }
                    catch (Exception ex) { tab[index] = -1; }
                }
                index++;
            }
            return tab;
        }
        //--------------------------------------------------------------------------------------------
        public static string ConvertDataToCorrectFileName(DateTime date)
        {
            string res = date.ToString();

            //Zamien / na myślniki
            res = res.Replace('/', '-');
            //Zamien : na _
            res = res.Replace(':', '_');

            return res;
        }
        //--------------------------------------------------------------------------------------------

    }
}
