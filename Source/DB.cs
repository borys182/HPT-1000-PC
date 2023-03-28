using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using Npgsql;

using System.Text;
using System.Threading.Tasks;
using HPT1000.Source.Driver;
using HPT1000.Source.Chamber;
using System.Diagnostics;

/* Poprawki w instalatorze bazy danych
 * zmienić ustawienia lokalne w skrypcie tworzacym baze danych z polskich (LC_COLLATE = 'Polish_Poland.1250'
      LC_CTYPE = 'Polish_Poland.1250') na angielksie
 */
namespace HPT1000.Source
{
    enum TypeFillParameters { NewUser = 1 , ModifyUser = 2 , RemoveUser = 3};
   
    /*
     * Klasa jest odpowiedzilana za komunikacje z baza danych oraz udostpenianie danych z niej pobranych. W bazie danych sa przechowywane informacje na temat:
     * : translacji tekstow , użytkowników i uprawnień, pomiarów, programów 
     */
    public class DB
    {
        //korzystanie ze standardowego sterownika ODBC dostarczonego przez framwork visula
        private OdbcConnection connection = new OdbcConnection("Dsn=PostgreSQL;" + "database=HE-005;server=localhost;" + "port=5432;uid=postgres;");

        //korzystanie z providera bazy Postgres dla C# (framework Npsql)
        NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1 ; User ID = postgres ;" + "Password = " + "admin_friends" + "; Database=HE-005;");
        NpgsqlCommand    cmd  = new NpgsqlCommand();

        private string paraName = "Data_Base";           ///<Nazwa parametra pod ktora wszystkie infomacje na teamt obiektu PLC zostana zapisane w bazie danych
        private string versionDB = "0.0.0";
        //------------------------------------------------------------------------------------------------------------------------------
        public string Version
        {
            get { return versionDB; }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        public DB()
        {
            Open();
            cmd.Connection = conn;
            LoadVersionDB();
        }
        //-----------------------------------------------------------------------------------------
        /**
        * Funkcja odczytuje wersje bazy danych
        */
        private void LoadVersionDB()
        {
            ProgramParameter parameter = new ProgramParameter();
            parameter.Name = paraName;
            LoadParameter(parameter.Name, out parameter);
            if (parameter.Data != null)
            {
                string[] parameters = parameter.Data.Split(';');
                foreach (string para in parameters)
                {
                    try
                    {
                        if (para.Contains("Version"))
                            versionDB = para.Split('=')[1].Trim();
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
            }            
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie otwarcie polaczenia z baza danych. Jezeli sie to udalo to zwraca 0 w przeciwnym wypadku wartosc rozna od 0 
        private int Open()
        {
            int aRes = 1;
            try
            {
                //czekaj aż server postgres zostanie uruchomiony ale nie dluzej niz 30 sekund
                long time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                bool wait = true;
                while (wait)
                {
                    Process[] tabProcess = Process.GetProcesses();
                    for (int i = 0; i < tabProcess.Length; i++)
                    {
                        string name = tabProcess[i].ProcessName;
                        //Sprawdz czy serwer zostal juz uruchomiony
                        if (tabProcess[i].ProcessName.Contains("postgres"))
                        {
                            wait = false;
                            break;
                        }
                    }
                    //Przekroczono czas oczekiwania 30 s na uruchomienie sie serwera bazy to opusc petle
                    if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - time > 30000)
                        break;
                    System.Threading.Thread.Sleep(10); // czekaj na serwer baz danych
                }
                    conn.Open();
                //    connection.Open();
                aRes = 0;
            }
            catch (Exception ex) {
                Logger.AddException(ex);
                Logger.AddMsg("For unknown reasons database connection can't be opened", Types.MessageType.Error);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zamkniecia polaczenia z baza danych. Jezeli sie to udalo to zwraca 0 w przeciwnym wypadku wartosc rozna od 0 
        public int Close()
        {
            int aRes = 1;
            try
            {
                // connection.Close();
                conn.Close();
                aRes = 0;
            }
            catch (Exception ex) {
                Logger.AddException(ex);
                Logger.AddMsg("For unknown reasons database connection can't be closed.", Types.MessageType.Error);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie podanie listy userow wpisanych do tabeli Users bazy danych
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            //Programowanie komunikcji z baza z urzyciem frameworka Npgswl sprecyzowanego na komuniakcje z baza danych srodowiska Visual

            //utworz zapytanie
            string query = " SELECT * FROM \"Users\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkich userow zwroconych przez zapytanie i przypisz je do obietku User oraz do lsity users
                while (data.Read())
                {
                    User user = new User();
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("id")))
                            user.ID = data.GetInt32(data.GetOrdinal("id"));

                        if (!data.IsDBNull(data.GetOrdinal("name")))
                            user.Name = data.GetString(data.GetOrdinal("name"));

                        if (!data.IsDBNull(data.GetOrdinal("surname")))
                            user.Surname = data.GetString(data.GetOrdinal("surname"));

                        if (!data.IsDBNull(data.GetOrdinal("login")))
                            user.Login = data.GetString(data.GetOrdinal("login"));

                        if (!data.IsDBNull(data.GetOrdinal("password")))
                            user.Password = data.GetString(data.GetOrdinal("password"));

                        if (!data.IsDBNull(data.GetOrdinal("allow_change_psw")))
                            user.AllowChangePsw = data.GetBoolean(data.GetOrdinal("allow_change_psw"));

                        if (!data.IsDBNull(data.GetOrdinal("type_block_account")))
                            user.DisableAccount = (Types.TypeDisableAccount)data.GetInt32(data.GetOrdinal("type_block_account"));

                        if (!data.IsDBNull(data.GetOrdinal("privilige")))
                            user.Privilige = (Types.UserPrivilige)data.GetInt32(data.GetOrdinal("privilige"));

                        if (!data.IsDBNull(data.GetOrdinal("start_date_block_account")))
                            user.DateStartDisableAccount = (DateTime)data.GetDate(data.GetOrdinal("start_date_block_account"));

                        if (!data.IsDBNull(data.GetOrdinal("end_date_block_account")))
                            user.DateEndDisableAccount = (DateTime)data.GetDate(data.GetOrdinal("end_date_block_account"));

                        users.Add(user);

                    }
                    catch (Exception ex) {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return users;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie podanie listy userow wpisanych do tabeli Users bazy danych
        public List<Program.Program> GetPrograms()
        {
            List<Program.Program> programs = new List<Program.Program>();

            //utworz zapytanie
            string query = " SELECT * FROM \"Programs\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkich userow zwroconych przez zapytanie i przypisz je do obietku User oraz do lsity users
                int aProgramId = 0;
                while (data.Read())
                {
                    Program.Program program = null;
                    Program.Subprogram subprogram = null;
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("program_id")))
                            aProgramId = data.GetInt32(data.GetOrdinal("program_id"));

                        //Podaj mi wskaznik na program z danym ID ktory zostal juz wczesniej utworzony
                        program = GetProgram(programs, aProgramId);
                        if (program == null)
                        {
                            program = Factory.CreateProgram((uint)aProgramId);
                            programs.Add(program);
                        }

                        if (!data.IsDBNull(data.GetOrdinal("program_name")))
                            program.SetName(data.GetString(data.GetOrdinal("program_name")));

                        if (!data.IsDBNull(data.GetOrdinal("program_description")))
                            program.SetDescription(data.GetString(data.GetOrdinal("program_description")));

                        //Jezeli program posiada jakis subprogram to zczytaj jego podstawoe info i dodaj do listy
                        if (!data.IsDBNull(data.GetOrdinal("subprogram_id")))
                        {
                            subprogram = new Program.Subprogram();
                            subprogram.ID = (uint)data.GetInt32(data.GetOrdinal("subprogram_id"));

                            if (!data.IsDBNull(data.GetOrdinal("subprogram_ordinal_number")))
                                subprogram.OrdinalNumber = data.GetInt32(data.GetOrdinal("subprogram_ordinal_number"));
                            else //Jezeli nie bylo wczesniej nadego numeru porzadkowego to nadaj mu kolejny automatycznie
                                subprogram.OrdinalNumber = program.GetOrdinalSubprogramNumber();

                            if (!data.IsDBNull(data.GetOrdinal("subprogram_name")))
                                subprogram.SetName(data.GetString(data.GetOrdinal("subprogram_name")));
                            if (!data.IsDBNull(data.GetOrdinal("subprogram_description")))
                                subprogram.SetDescription(data.GetString(data.GetOrdinal("subprogram_description")));
                            if (!data.IsDBNull(data.GetOrdinal("pump_active")) && subprogram.PumpProces != null)
                                subprogram.PumpProces.Active = (data.GetBoolean(data.GetOrdinal("pump_active")));
                            if (!data.IsDBNull(data.GetOrdinal("power_supply_active")) && subprogram.PlasmaProces != null)
                                subprogram.PlasmaProces.Active = (data.GetBoolean(data.GetOrdinal("power_supply_active")));
                            if (!data.IsDBNull(data.GetOrdinal("gas_active")) && subprogram.GasProces != null)
                                subprogram.GasProces.Active = (data.GetBoolean(data.GetOrdinal("gas_active")));
                            if (!data.IsDBNull(data.GetOrdinal("purge_active")) && subprogram.PurgeProces != null)
                                subprogram.PurgeProces.Active = (data.GetBoolean(data.GetOrdinal("purge_active")));
                            if (!data.IsDBNull(data.GetOrdinal("vent_active")) && subprogram.VentProces != null)
                                subprogram.VentProces.Active = (data.GetBoolean(data.GetOrdinal("vent_active")));
                            if (!data.IsDBNull(data.GetOrdinal("motor_active")) && subprogram.MotorProces != null)
                                subprogram.MotorProces.Active = (data.GetBoolean(data.GetOrdinal("motor_active")));

                            program.AddSubprogram(subprogram);
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
                //Ustaw w poprawnej kolejnosci subprgoramy
                foreach (Program.Program pr in programs)
                    pr.SetOrderSubprogrms();
            }
            return programs;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie danego prgramu z lisy ale jezeli nie istnieje to go musi utworzyc
        private Program.Program GetProgram(List<Program.Program> programs, int aIdProgram)
        {
            Program.Program program = null;
            foreach (Program.Program pr in programs)
            {
                if (pr.GetID() == aIdProgram)
                    program = pr;
            }
            return program;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie podanie listy userow wpisanych do tabeli Users bazy danychwypelnienie danymi procesowymi podanengo subprogramu
        public void FillProcessParametersOfSubprograms(List<Program.Subprogram> subprograms)
        {
            //utworz zapytanie
            string query = " SELECT * FROM \"Subprograms\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkich userow zwroconych przez zapytanie i przypisz je do obietku User oraz do lsity users
                int subprogramID = 0;
                while (data.Read())
                {
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("subprogram_id")))
                            subprogramID = data.GetInt32(data.GetOrdinal("subprogram_id"));

                        if (subprogramID > 0)
                        {
                            Program.Subprogram subprogram = GetSubprogram(subprograms, subprogramID);
                            if (subprogram != null)
                            {
                                FillVentProcessData(subprogram.GetVentProces(), data);
                                FillPumpProcessData(subprogram.GetPumpProces(), data);
                                FillPurgeProcessData(subprogram.GetPurgeProces(), data);
                                FillPowerProcessData(subprogram.GetPlasmaProces(), data);
                                FillGasProcessData(subprogram.GetGasProces(), data);
                                FillMotorProcessData(subprogram.GetMotorProces(), data);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie liczby danych zawartych w bazie danych pomiedzy podanymi widelkami czasowymi
        public int GetCountData(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            int counData = 0;
            //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(GetParameter("StartDate", DbType.DateTime, dateTimeStart));
            parameters.Add(GetParameter("EndDate", DbType.DateTime, dateTimeEnd));
            //Wykonaj procedurerozpoczecia sesji zapisu danych do bazy danych
            DataSet dataSet = null;
            PerformFunctionDB("\"GetSesions\"", parameters, out dataSet); //procedura zwraca id utworzonego 
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                    counData = dataSet.Tables[0].Rows.Count;                
            }
            return counData;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie listy sesji zapisu danych zawartych w bazie danych
        public List<Sesion> GetSesions(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            List<Sesion> sesions = new List<Sesion>();

            //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(GetParameter("StartDate", DbType.DateTime, dateTimeStart));
            parameters.Add(GetParameter("EndDate", DbType.DateTime, dateTimeEnd));
            //Wykonaj procedurerozpoczecia sesji zapisu danych do bazy danych
            DataSet dataSet = null;
            PerformFunctionDB("\"GetSesions\"", parameters, out dataSet); //procedura zwraca id utworzonego 
            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        //if(dataSet.Tables[0].Rows[i].ItemArray. == 5)
                        {
                            Sesion sesion = new Sesion();
                            
                            sesion.ID           = (int)dataSet.Tables[0].Rows[i].ItemArray[0];
                            sesion.StartDate    = (DateTime)dataSet.Tables[0].Rows[i].ItemArray[1];
                            sesion.EndDate      = (DateTime)dataSet.Tables[0].Rows[i].ItemArray[2];
                            if(!dataSet.Tables[0].Rows[i].IsNull(3))
                                sesion.ProcesParameter = Types.ConvertByteToProcesParameter((byte[])dataSet.Tables[0].Rows[i].ItemArray[3]);
                            if (sesion.ProcesParameter != null)
                                sesion.ProcesName = sesion.ProcesParameter.Name;
                            /*if (!dataSet.Tables[0].Rows[i].IsNull(3))
                                sesion.ProcesName   = (String)dataSet.Tables[0].Rows[i].ItemArray[3];
                            */
                            sesions.Add(sesion);
                        }
                    }
                }
            }
	return sesions;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie listy typow gazow zapisanych w bazie danych
        public List<GasType> GetGasTypes()
        {
            List<GasType> gasTypes = new List<GasType>();

            //utworz zapytanie
            string query = " SELECT * FROM \"GasTypes\"";
            lock (this)
            {
                cmd.CommandText = query;
                //wykonaj zapytanie
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkie sesje zwrocone przez zapytanie
                while (data.Read())
                {
                    GasType gasType = new GasType(true);
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("id")))
                            gasType.ID = data.GetInt32(data.GetOrdinal("id"));
                        if (!data.IsDBNull(data.GetOrdinal("name")))
                            gasType.Name = data.GetString(data.GetOrdinal("name"));
                        if (!data.IsDBNull(data.GetOrdinal("description")))
                            gasType.Description = data.GetString(data.GetOrdinal("description"));
                        if (!data.IsDBNull(data.GetOrdinal("factor")))
                            gasType.Factor = data.GetFloat(data.GetOrdinal("factor"));

                        gasTypes.Add(gasType);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return gasTypes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie listy urzadzen i jej parametrow
        public List<DataBaseDevice> GetDevices()
        {
            List<DataBaseDevice> devices = new List<DataBaseDevice>();

            //utworz zapytanie
            string query = " SELECT * FROM \"Devices\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkie sesje zwrocone przez zapytanie
                while (data.Read())
                {
                    DataBaseDevice device = new DataBaseDevice();
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("device_id")))
                            device.DeviceID = data.GetInt32(data.GetOrdinal("device_id"));
                        if (!data.IsDBNull(data.GetOrdinal("para_id")))
                            device.ParaID = data.GetInt32(data.GetOrdinal("para_id"));
                        if (!data.IsDBNull(data.GetOrdinal("device_name")))
                            device.DeviceName = data.GetString(data.GetOrdinal("device_name"));
                        if (!data.IsDBNull(data.GetOrdinal("para_name")))
                            device.ParaName = data.GetString(data.GetOrdinal("para_name"));

                        devices.Add(device);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return devices;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie listy urzadzen i jej parametrow
        public List<ConfigPara> GetConfigParameters()
        {
            List<ConfigPara> parameters = new List<ConfigPara>();

            //utworz zapytanie
            string query = " SELECT * FROM \"ConfigParametersAcq\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {

                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkie sesje zwrocone przez zapytanie
                while (data.Read())
                {
                    ConfigPara configPara = new ConfigPara();
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("id")))
                            configPara.ID = data.GetInt32(data.GetOrdinal("id"));
                        if (!data.IsDBNull(data.GetOrdinal("id_parameter")))
                            configPara.ParameterID = data.GetInt32(data.GetOrdinal("id_parameter"));
                        if (!data.IsDBNull(data.GetOrdinal("frequency")))
                            configPara.Frequency = data.GetFloat(data.GetOrdinal("frequency"));
                        if (!data.IsDBNull(data.GetOrdinal("difference_value")))
                            configPara.Difference = data.GetFloat(data.GetOrdinal("difference_value"));
                        if (!data.IsDBNull(data.GetOrdinal("enabled_acq")))
                            configPara.Enabled = data.GetBoolean(data.GetOrdinal("enabled_acq"));
                        if (!data.IsDBNull(data.GetOrdinal("mode_acq")))
                            configPara.Mode = data.GetInt32(data.GetOrdinal("mode_acq"));

                        parameters.Add(configPara);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return parameters;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie listy bledow
        public List<MessageError> GetMessageErrors()
        {
            List<MessageError> errors = new List<MessageError>();

            //utworz zapytanie
            string query = " SELECT * FROM \"Errors_Txt\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkie sesje zwrocone przez zapytanie
                while (data.Read())
                {
                    MessageError error = new MessageError();
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("id")))
                            error.ID = data.GetInt32(data.GetOrdinal("id"));
                        if (!data.IsDBNull(data.GetOrdinal("error_code")))
                            error.ErrCode = data.GetInt32(data.GetOrdinal("error_code"));
                        if (!data.IsDBNull(data.GetOrdinal("event_type")))
                            error.EventType = (Types.EventType)data.GetInt32(data.GetOrdinal("event_type"));
                        if (!data.IsDBNull(data.GetOrdinal("event_category")))
                            error.EventCategory = (Types.EventCategory)data.GetInt32(data.GetOrdinal("event_category"));
                        if (!data.IsDBNull(data.GetOrdinal("text")))
                            error.Text = data.GetString(data.GetOrdinal("text"));
                        if (!data.IsDBNull(data.GetOrdinal("language_value")))
                            error.Language = (Types.Language)data.GetInt32(data.GetOrdinal("language_value"));

                        errors.Add(error);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return errors;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie odczytanie listy bledow
        public List<MessageEvent> GetMessageEvents()
        {
            List<MessageEvent> events = new List<MessageEvent>();

            //utworz zapytanie
            string query = " SELECT * FROM \"Events_Txt\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkie sesje zwrocone przez zapytanie
                while (data.Read())
                {
                    MessageEvent eventTxt = new MessageEvent();
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("id")))
                            eventTxt.ID = data.GetInt32(data.GetOrdinal("id"));
                        if (!data.IsDBNull(data.GetOrdinal("code")))
                            eventTxt.Code = data.GetInt32(data.GetOrdinal("code"));
                        if (!data.IsDBNull(data.GetOrdinal("text")))
                            eventTxt.Text = data.GetString(data.GetOrdinal("text"));
                        if (!data.IsDBNull(data.GetOrdinal("language_value")))
                            eventTxt.Language = (Types.Language)data.GetInt32(data.GetOrdinal("language_value"));

                        events.Add(eventTxt);
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return events;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private Program.Subprogram GetSubprogram(List<Program.Subprogram> subprograms, int subprogramID)
        {
            Program.Subprogram subprogram = null;
            foreach (Program.Subprogram subpr in subprograms)
            {
                if (subpr.ID == subprogramID)
                    subprogram = subpr;
            }
            return subprogram;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private void FillVentProcessData(Program.VentProces ventProcess, NpgsqlDataReader data)
        {
            if (ventProcess != null && data != null)
            {
                if (!data.IsDBNull(data.GetOrdinal("vent_time")))
                    ventProcess.SetTimeVent((DateTime)data.GetTime(data.GetOrdinal("vent_time")));
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private void FillPurgeProcessData(Program.FlushProces purgeProcess, NpgsqlDataReader data)
        {
            if (purgeProcess != null && data != null)
            {
                if (!data.IsDBNull(data.GetOrdinal("purge_time")))
                    purgeProcess.SetTimePurge((DateTime)data.GetTime(data.GetOrdinal("purge_time")));
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private void FillPumpProcessData(Program.PumpProces pumpProcess, NpgsqlDataReader data)
        {
            if (pumpProcess != null && data != null)
            {
                if (!data.IsDBNull(data.GetOrdinal("setpoint_pump_pressure")))
                    pumpProcess.SetSetpoint(data.GetFloat(data.GetOrdinal("setpoint_pump_pressure")));
                if (!data.IsDBNull(data.GetOrdinal("max_time_pump")))
                    pumpProcess.SetTimeWaitForPumpDown((DateTime)data.GetTime(data.GetOrdinal("max_time_pump")));
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private void FillPowerProcessData(Program.PlasmaProces powerProcess, NpgsqlDataReader data)
        {
            if (powerProcess != null && data != null)
            {
                if (!data.IsDBNull(data.GetOrdinal("power_supply_time_process")))
                    powerProcess.SetTimeOperate((DateTime)data.GetTime(data.GetOrdinal("power_supply_time_process")));
                if (!data.IsDBNull(data.GetOrdinal("power_supply_setpoint")))
                    powerProcess.SetSetpointValue(data.GetFloat(data.GetOrdinal("power_supply_setpoint")));
                if (!data.IsDBNull(data.GetOrdinal("power_supply_max_devation")))
                    powerProcess.SetDeviation(data.GetFloat(data.GetOrdinal("power_supply_max_devation")));
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private void FillGasProcessData(Program.GasProces gasProcess, NpgsqlDataReader data)
        {
            if (gasProcess != null && data != null)
            {
                if (!data.IsDBNull(data.GetOrdinal("gas_process_duration")))
                    gasProcess.SetTimeProcesDuration((DateTime)data.GetTime(data.GetOrdinal("gas_process_duration")));
                if (!data.IsDBNull(data.GetOrdinal("mode_gas")))
                    gasProcess.SetModeProces((Types.GasProcesMode)data.GetInt32(data.GetOrdinal("mode_gas")));
                if (!data.IsDBNull(data.GetOrdinal("gas_setpoint_pressure")))
                    gasProcess.SetSetpointPressure(data.GetFloat(data.GetOrdinal("gas_setpoint_pressure")));
                if (!data.IsDBNull(data.GetOrdinal("gas_max_dev_up")))
                    gasProcess.SetMaxDeviationPresure(data.GetFloat(data.GetOrdinal("gas_max_dev_up")));
                if (!data.IsDBNull(data.GetOrdinal("gas_max_dev_down")))
                    gasProcess.SetMinDeviationPresure(data.GetFloat(data.GetOrdinal("gas_max_dev_down")));
                if (!data.IsDBNull(data.GetOrdinal("vaporaiser_cycle_time")))
                    gasProcess.SetCycleTime(data.GetFloat(data.GetOrdinal("vaporaiser_cycle_time")));
                if (!data.IsDBNull(data.GetOrdinal("vaporaiser_on_time")))
                    gasProcess.SetOnTime((int)data.GetFloat(data.GetOrdinal("vaporaiser_on_time")));
                if (!data.IsDBNull(data.GetOrdinal("mfc1_flow")))
                    gasProcess.SetGasFlow(data.GetFloat(data.GetOrdinal("mfc1_flow")), 1);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_flow")))
                    gasProcess.SetGasFlow(data.GetFloat(data.GetOrdinal("mfc2_flow")), 2);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_flow")))
                    gasProcess.SetGasFlow(data.GetFloat(data.GetOrdinal("mfc3_flow")), 3);
                if (!data.IsDBNull(data.GetOrdinal("mfc1_min_flow")))
                    gasProcess.SetMinGasFlow((int)data.GetFloat(data.GetOrdinal("mfc1_min_flow")), 1);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_min_flow")))
                    gasProcess.SetMinGasFlow((int)data.GetFloat(data.GetOrdinal("mfc2_min_flow")), 2);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_min_flow")))
                    gasProcess.SetMinGasFlow((int)data.GetFloat(data.GetOrdinal("mfc3_min_flow")), 3);
                if (!data.IsDBNull(data.GetOrdinal("mfc1_max_flow")))
                    gasProcess.SetMaxGasFlow((int)data.GetFloat(data.GetOrdinal("mfc1_max_flow")), 1);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_max_flow")))
                    gasProcess.SetMaxGasFlow((int)data.GetFloat(data.GetOrdinal("mfc2_max_flow")), 2);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_max_flow")))
                    gasProcess.SetMaxGasFlow((int)data.GetFloat(data.GetOrdinal("mfc3_max_flow")), 3);
                if (!data.IsDBNull(data.GetOrdinal("mfc1_gas_share")))
                    gasProcess.SetShareGas((int)data.GetFloat(data.GetOrdinal("mfc1_gas_share")), 1,true);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_gas_share")))
                    gasProcess.SetShareGas((int)data.GetFloat(data.GetOrdinal("mfc2_gas_share")), 2,true);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_gas_share")))
                    gasProcess.SetShareGas((int)data.GetFloat(data.GetOrdinal("mfc3_gas_share")), 3,true);
                if (!data.IsDBNull(data.GetOrdinal("mfc1_max_devation")))
                    gasProcess.SetShareDevaition((int)data.GetFloat(data.GetOrdinal("mfc1_max_devation")), 1);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_max_devation")))
                    gasProcess.SetShareDevaition((int)data.GetFloat(data.GetOrdinal("mfc2_max_devation")), 2);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_max_devation")))
                    gasProcess.SetShareDevaition((int)data.GetFloat(data.GetOrdinal("mfc3_max_devation")), 3);
                if (!data.IsDBNull(data.GetOrdinal("mfc1_active")))
                    gasProcess.SetActiveFlow(data.GetBoolean(data.GetOrdinal("mfc1_active")), 1,true);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_active")))
                    gasProcess.SetActiveFlow(data.GetBoolean(data.GetOrdinal("mfc2_active")), 2, true);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_active")))
                    gasProcess.SetActiveFlow(data.GetBoolean(data.GetOrdinal("mfc3_active")), 3, true);
                if (!data.IsDBNull(data.GetOrdinal("vaporaiser_active")))
                    gasProcess.SetVaporaiserActive(data.GetBoolean(data.GetOrdinal("vaporaiser_active")));
                if (!data.IsDBNull(data.GetOrdinal("mfc1_id_gas_type")))
                    gasProcess.SetGasType(data.GetInt32(data.GetOrdinal("mfc1_id_gas_type")),1);
                if (!data.IsDBNull(data.GetOrdinal("mfc2_id_gas_type")))
                    gasProcess.SetGasType(data.GetInt32(data.GetOrdinal("mfc2_id_gas_type")),2);
                if (!data.IsDBNull(data.GetOrdinal("mfc3_id_gas_type")))
                    gasProcess.SetGasType(data.GetInt32(data.GetOrdinal("mfc3_id_gas_type")),3);
                if (!data.IsDBNull(data.GetOrdinal("vaporaiser_dosing")))
                    gasProcess.SetDosing(data.GetInt32(data.GetOrdinal("vaporaiser_dosing")));
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        private void FillMotorProcessData(Program.MotorProces motorProcess, NpgsqlDataReader data)
        {
            if (motorProcess != null && data != null)
            {
                if (!data.IsDBNull(data.GetOrdinal("state_motor_1")))
                    motorProcess.SetState(1,(Types.StateFP)data.GetInt32(data.GetOrdinal("state_motor_1")));
                if (!data.IsDBNull(data.GetOrdinal("state_motor_2")))
                    motorProcess.SetState(2, (Types.StateFP)data.GetInt32(data.GetOrdinal("state_motor_2")));
                if (!data.IsDBNull(data.GetOrdinal("time_motor_1")))
                    motorProcess.SetTimeMotor(1, data.GetDateTime(data.GetOrdinal("time_motor_1")));
                if (!data.IsDBNull(data.GetOrdinal("time_motor_2")))
                    motorProcess.SetTimeMotor(2, data.GetDateTime(data.GetOrdinal("time_motor_2")));
                if (!data.IsDBNull(data.GetOrdinal("active_motor_1")))
                    motorProcess.SetActive(1, data.GetBoolean(data.GetOrdinal("active_motor_1")));
                if (!data.IsDBNull(data.GetOrdinal("active_motor_2")))
                    motorProcess.SetActive(2, data.GetBoolean(data.GetOrdinal("active_motor_2")));
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja dodaje usera do bazy danych. Jezeli operacja sie powiedzia xwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int AddUser(User user)
        {
            int aRes = 0;
            List<NpgsqlParameter> parameters = GetUserParameters(user, TypeFillParameters.NewUser);
            aRes = PerformFunctionDB("\"NewUser\"", parameters);
            //Funkcja zwraca ID nowo utworzonego usera
            if (aRes > 0 && user != null)
            {
                user.ID = aRes; //wynik funkicji bazodanowej zwraca ID nowo utworzonego usera
                aRes = 0; //Ustaw reuslt jako ze obulo sie bez problemow
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja dodaje usera do bazy danych. Jezeli operacja sie powiedzia xwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int ModifyUser(User user)
        {
            int aRes = 0;
            //Przygotuj parametry dla procedury modife na bazie obiektu usera
            List<NpgsqlParameter> parameters = GetUserParameters(user, TypeFillParameters.ModifyUser);
            //Wykonaj procedure modyfikowania usera
            aRes = PerformFunctionDB("\"ModifyUser\"", parameters);
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja usuwa usera z bazy danych. Jezeli operacja sie powiedzia xwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int RemoveUser(User user)
        {
            int aRes = 0;
            //Przygotuj parametry dla procedury usuwania na bazie obiektu usera
            List<NpgsqlParameter> parameters = GetUserParameters(user, TypeFillParameters.RemoveUser);
            //Wykonaj procedure usuwania usera
            aRes = PerformFunctionDB("\"RemoveUser\"", parameters);
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja usuwa dane powiązane z podaną sesją z bazy danych. Jezeli operacja sie powiedzia zwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int RemoveData(Sesion sesion)
        {
            int res = -1;
            //Usun sesje
            string query = String.Format(" DELETE FROM sesions where sesions.id = {0}",sesion.ID);
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                if (data.RecordsAffected > 0)
                    res = 0;
                data.Close();
            }
            //Usun dane
            query = String.Format(" DELETE FROM data where data.id_sesion = {0}", sesion.ID);
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                if (data.RecordsAffected > 0)
                    res = 0;
                data.Close();
            }
            return res;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funckja dodaje do bazy danych program
        public int AddProgram(Program.Program program)
        {
            int aRes = -1;
            if (program != null)
            {
                //Przygotuj parametry dla procedury dodawania programuw bazie danych
                List<NpgsqlParameter> parameters = GetProgramParameters(program, TypeFillParameters.NewUser);
                //Wykonaj procedure dodawania programu
                aRes = PerformFunctionDB("\"NewProgram\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja dodaje usera do bazy danych. Jezeli operacja sie powiedzia xwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int ModifyProgram(Program.Program program)
        {
            int aRes = -1;
            if (program != null)
            {
                //Przygotuj parametry dla procedury modife na bazie obiektu usera
                List<NpgsqlParameter> parameters = GetProgramParameters(program, TypeFillParameters.ModifyUser);
                //Wykonaj procedure modyfikowania usera
                aRes = PerformFunctionDB("\"ModifyProgram\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja usuwa program z bazy danych. Jezeli operacja sie powiedzia xwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int RemoveProgram(Program.Program program)
        {
            int aRes = -1;
            if (program != null)
            {
                //Przygotuj parametry dla procedury usuwania standardowo rekordu z bazy danych to naczy funkcja posiada tylko jeden parametr o nazwie ID
                List<NpgsqlParameter> parameters = GetRemoveStandardParameters((int)program.GetID());
                //Wykonaj procedure usuwania programu
                aRes = PerformFunctionDB("\"RemoveProgram\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja dodaje do bazy danych subprogram. Powiazanie subprogramu z programem odbywa sie juz po stronie bazy danych w funkcji AddSubprpgram
        public int AddSubProgram(Program.Subprogram subprogram, Int32 idProgram)
        {
            int aRes = -1;
            if (subprogram != null)
            {
                //Przygotuj parametry dla procedury dodawania subprogramu powiazanego z programem
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                //utworz parametr program name                    
                parameters.Add(GetParameter("id_program", DbType.Int32, idProgram));
                //utworz parametr ordinal number                    
                parameters.Add(GetParameter("ordinal_number", DbType.Int32, subprogram.OrdinalNumber));
                //utworz parametr subprogram name                    
                parameters.Add(GetParameter("name_subprogram", DbType.AnsiString, subprogram.GetName()));
                //utworz parametr description                    
                parameters.Add(GetParameter("description", DbType.AnsiString, subprogram.GetDescription()));

                //Wykonaj procedure dodawania programu
                aRes = PerformFunctionDB("\"AddSubprogram\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja modyfikuje dane w bazie danych dla tabeli subprogram. 
        public int ModifySubprogram(Program.Subprogram subprogram)
        {
            int aRes = -1;
            if (subprogram != null && subprogram.PumpProces != null && subprogram.PlasmaProces != null && subprogram.GasProces != null && subprogram.PurgeProces != null && subprogram.VentProces != null && subprogram.MotorProces != null )
            {
                //Przygotuj parametry dla procedury dodawania subprogramu powiazanego z programem
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                                
                parameters.Add(GetParameter("id", DbType.Int32, subprogram.ID));//utworz parametr program id     
                parameters.Add(GetParameter("ordinal_number", DbType.Int32, subprogram.OrdinalNumber));
                parameters.Add(GetParameter("name", DbType.AnsiString, subprogram.GetName()));  //utworz parametr subprogram name                             
                parameters.Add(GetParameter("description", DbType.AnsiString, subprogram.GetDescription()));    //utworz parametr description  
                parameters.Add(GetParameter("active_pump", DbType.Boolean, subprogram.PumpProces.Active));
                parameters.Add(GetParameter("active_power_supply", DbType.Boolean, subprogram.PlasmaProces.Active));
                parameters.Add(GetParameter("active_gas", DbType.Boolean, subprogram.GasProces.Active));
                parameters.Add(GetParameter("active_purge", DbType.Boolean, subprogram.PurgeProces.Active));
                parameters.Add(GetParameter("active_vent", DbType.Boolean, subprogram.VentProces.Active));
                parameters.Add(GetParameter("active_motor", DbType.Boolean, subprogram.MotorProces.Active));

                //Wykonaj procedure dodawania programu
                aRes = PerformFunctionDB("\"ModifySubprogram\"", parameters);

                //Modyfikuj tabele zwiazane z prcesami
                ModifySubprogramStages_1(subprogram);
                ModifySubprogramStages_2(subprogram);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja modyfikuje dane w bazie danych dla tabel powiazanych z danym subprogramem i przechowujacych info na temat jego parametrow odnosnie:
        //vent, purgu, pump oraz zasilacza. 
        public int ModifySubprogramStages_1(Program.Subprogram subprogram)
        {
            int aRes = -1;
            if (subprogram != null && subprogram.GetVentProces() != null && subprogram.GetPlasmaProces() != null && subprogram.GetPumpProces() != null && subprogram.GetPurgeProces() != null && subprogram.GetMotorProces() != null)
            {
                //Przygotuj parametry dla procedury dodawania subprogramu powiazanego z programem
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

                //utworz liste parametrow dla funkcji ModifySubprogramStages_1
                parameters.Add(GetParameter("id_subprogram", DbType.Int32, subprogram.ID));
                parameters.Add(GetParameter("time_vent", DbType.Time, subprogram.GetVentProces().GetTimeVent()));
                parameters.Add(GetParameter("time_purge", DbType.Time, subprogram.GetPurgeProces().GetTimePurge()));
                parameters.Add(GetParameter("time_pump", DbType.Time, subprogram.GetPumpProces().GetTimeWaitForPumpDown()));
                parameters.Add(GetParameter("setpoint_pressure_pump", DbType.Single, (float)subprogram.GetPumpProces().GetSetpoint()));
                parameters.Add(GetParameter("setpoint_power_supply", DbType.Single, (float)subprogram.GetPlasmaProces().GetSetpointValue()));
                parameters.Add(GetParameter("mode", DbType.Int32, (int)subprogram.GetPlasmaProces().GetWorkMode()));
                parameters.Add(GetParameter("max_devation_power_supply", DbType.Single, (float)subprogram.GetPlasmaProces().GetDeviation()));
                parameters.Add(GetParameter("time_duration_power_supply", DbType.Time, subprogram.GetPlasmaProces().GetTimeOperate()));
                parameters.Add(GetParameter("motor_1_state", DbType.Int32, subprogram.GetMotorProces().GetState(1)));
                parameters.Add(GetParameter("motor_2_state", DbType.Int32, subprogram.GetMotorProces().GetState(2)));
                parameters.Add(GetParameter("motor_1_time", DbType.Time, subprogram.GetMotorProces().GetTimeMotor(1)));
                parameters.Add(GetParameter("motor_2_time", DbType.Time, subprogram.GetMotorProces().GetTimeMotor(2)));
                parameters.Add(GetParameter("motor_1_state", DbType.Boolean, subprogram.GetMotorProces().GetActive(1)));
                parameters.Add(GetParameter("motor_2_state", DbType.Boolean, subprogram.GetMotorProces().GetActive(2)));

                //Wykonaj procedure dodawania programu
                aRes = PerformFunctionDB("\"ModifySubprogramStages_1\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja modyfikuje dane w bazie danych dla tabel powiazanych z danym subprogramem i przechowujacych info na temat jego parametrow odnosnie sterowania gazami:
        public int ModifySubprogramStages_2(Program.Subprogram subprogram)
        {
            int aRes = -1;
            if (subprogram != null && subprogram.GetGasProces() != null)
            {
                Program.GasProces gas = subprogram.GetGasProces();
                //Przygotuj parametry dla procedury dodawania subprogramu powiazanego z programem
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
               
                //utworz liste parametrow dla funkcji ModifySubprogramStages_2 - edycja tabeli przechowujacej info na temat sterowania gazami
                parameters.Add(GetParameter("id_subprogram", DbType.Int32, subprogram.ID));
                parameters.Add(GetParameter("active_mfc1", DbType.Boolean, gas.GetActiveFlow(1)));
                parameters.Add(GetParameter("active_mfc2", DbType.Boolean, gas.GetActiveFlow(2)));
                parameters.Add(GetParameter("active_mfc3", DbType.Boolean, gas.GetActiveFlow(3)));
                parameters.Add(GetParameter("active_vaporaiser", DbType.Boolean, gas.GetVaporiserActive()));
                parameters.Add(GetParameter("flow_mfc1", DbType.Single, (float)gas.GetGasFlow(1)));
                parameters.Add(GetParameter("flow_mfc2", DbType.Single, (float)gas.GetGasFlow(2)));
                parameters.Add(GetParameter("flow_mfc3", DbType.Single, (float)gas.GetGasFlow(3)));
                parameters.Add(GetParameter("min_flow_mfc1", DbType.Single, (float)gas.GetMinGasFlow(1)));
                parameters.Add(GetParameter("min_flow_mfc2", DbType.Single, (float)gas.GetMinGasFlow(2)));
                parameters.Add(GetParameter("min_flow_mfc3", DbType.Single, (float)gas.GetMinGasFlow(3)));
                parameters.Add(GetParameter("max_flow_mfc1", DbType.Single, (float)gas.GetMaxGasFlow(1)));
                parameters.Add(GetParameter("max_flow_mfc2", DbType.Single, (float)gas.GetMaxGasFlow(2)));
                parameters.Add(GetParameter("max_flow_mfc3", DbType.Single, (float)gas.GetMaxGasFlow(3)));
                parameters.Add(GetParameter("vaporaiser_cycle", DbType.Single, (float)gas.GetCycleTime()));
                parameters.Add(GetParameter("vaporaiser_time_on", DbType.Single, (float)gas.GetOnTime()));
                parameters.Add(GetParameter("setpoint_press", DbType.Single, (float)gas.GetSetpointPressure()));
                parameters.Add(GetParameter("max_dev_up", DbType.Single, (float)gas.GetMaxDeviationPresure()));
                parameters.Add(GetParameter("max_dev_down", DbType.Single, (float)gas.GetMinDeviationPresure()));
                parameters.Add(GetParameter("mfc1_max_dev", DbType.Single, (float)gas.GetShareDevaition(1)));
                parameters.Add(GetParameter("mfc2_max_dev", DbType.Single, (float)gas.GetShareDevaition(2)));
                parameters.Add(GetParameter("mfc3_max_dev", DbType.Single, (float)gas.GetShareDevaition(3)));
                parameters.Add(GetParameter("mfc1_gas_share", DbType.Single, (float)gas.GetShareGas(1)));
                parameters.Add(GetParameter("mfc2_gas_share", DbType.Single, (float)gas.GetShareGas(2)));
                parameters.Add(GetParameter("mfc3_gas_share", DbType.Single, (float)gas.GetShareGas(3)));
                parameters.Add(GetParameter("time", DbType.Time, gas.GetTimeProcesDuration()));
                parameters.Add(GetParameter("mode_process", DbType.Int32, gas.GetModeProces()));
                parameters.Add(GetParameter("mfc1_id_type_gas", DbType.Int32, gas.GetGasType(1)));
                parameters.Add(GetParameter("mfc2_id_type_gas", DbType.Int32, gas.GetGasType(2)));
                parameters.Add(GetParameter("mfc3_id_type_gas", DbType.Int32, gas.GetGasType(3)));
                parameters.Add(GetParameter("vaporaiser_dosing_", DbType.Int32, gas.GetDosing()));

                //Wykonaj procedure dodawania programu
                aRes = PerformFunctionDB("\"ModifySubprogramStages_2\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja usuwa subprogram z bazy danych. Jezeli operacja sie powiedzia xwraca 0 w przeciwnym wypadku wartosc rozna od 0
        public int RemoveSubprogram(Program.Subprogram subprogram)
        {
            int aRes = -1;
            if (subprogram != null)
            {
                //Przygotuj parametry dla procedury usuwania standardowo rekordu z bazy danych to naczy funkcja posiada tylko jeden parametr o nazwie ID
                List<NpgsqlParameter> parameters = GetRemoveStandardParameters((int)subprogram.ID);
                //Wykonaj procedure usuwania subprogramu
                aRes = PerformFunctionDB("\"RemoveSubprogram\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zarejestrowanie urzadzenia w bazie danych
        public int RegisterDevice(Device device)
        {
            int aRes = -1;
            if (device != null)
            {
                //Sprawdz czy dane urzadzenie jest przenzaczone do zbierania danych z bazy danych
                if (device.AcqData)
                {
                    //jezeli urzadzenie nie zostal jeszcze zarejestrowany to go zarejestruj. Zarejestruj tylko te urzadzenia ktore sa przeznacozne do zbierania danych
                    int aID = GetRegisterDeviceID(device.Name, true);
                    if (aID == 0)
                    {
                        //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
                        List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
                        parameters.Add(GetParameter("dev_name", DbType.AnsiString, device.Name));
                        //Wykonaj procedure rejestracji urzadzenia w bazie danych subprogramu
                        int id = PerformFunctionDB("\"RegisterDevice\"", parameters); //procedura zwraca id utworzonego 
                        if (id > 0)
                        {
                            device.ID_DB = id;
                            aRes = 0;
                        }
                    }
                    else //Urzadzenie jest zarejestrowane to ustaw ID
                    {
                        device.ID_DB = aID;
                    }
                }
                else
                    aRes = 0;
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zarejestrowanie listy parametrow w bazie danych
        public int RegisterParameters(int idDevice, List<Parameter> parameters)
        {
            int aRes = -1;
            int aCountOK = 0; // liczba poprawnie zapisanych parametrow
            if (parameters != null)
            {
                foreach (Parameter para in parameters)
                {
                    //jezeli parametr nie zostal jeszcze zarejestrowany to go zarejestruj
                    int aID = GetRegisterDeviceID(para.Name, false);
                    if (aID == 0)
                    {
                        //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
                        List<NpgsqlParameter> parametersDB = new List<NpgsqlParameter>();
                        parametersDB.Add(GetParameter("dev_id", DbType.Int32, idDevice));
                        parametersDB.Add(GetParameter("para_name", DbType.AnsiString, para.Name));
                        //Wykonaj procedure rejestracji urzadzenia w bazie danych subprogramu
                        int id = PerformFunctionDB("\"RegisterParameter\"", parametersDB); //procedura zwraca id utworzonego 
                        if (id > 0)
                        {
                            para.ID = id;
                            aCountOK++;
                        }
                    }
                    else //Parametr jest zarejestrowany to ustaw ID
                    {
                        para.ID = aID;
                    }
                        aCountOK++;
                    //Dodaj wpis do tabeli konfiguracje akwizycji danego parametru w bazie danych
                    AddParaConfiguration(para);
                }
                if (aCountOK == parameters.Count)
                    aRes = 0;
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie dodanie do zarejestrowanego parametru konfiguracji akwizycji danych
        public int AddParaConfiguration(Parameter parameter)
        {
            int aRes = -1;
            if (parameter != null)
            {
                //jezeli konfiguracja parametr nie zostala jeszcze zarejestrowana to ja zarejestruj
                ConfigPara configPara = GetConfigPara(parameter.ID);
                if (configPara.ID == 0)
                {
                    //Przygotuj parametry dla procedury dodajacej konfiguracje parametru w bazie danych
                    List<NpgsqlParameter> parametersDB = new List<NpgsqlParameter>();
                    parametersDB.Add(GetParameter("parameter_id", DbType.Int32, parameter.ID));
                    parametersDB.Add(GetParameter("freq", DbType.Single, (float)parameter.Frequency));
                    parametersDB.Add(GetParameter("differ_value", DbType.Single, (float)parameter.Differance));
                    parametersDB.Add(GetParameter("enabled", DbType.Boolean, parameter.EnabledAcq));
                    parametersDB.Add(GetParameter("mode", DbType.Int32, (int)parameter.Mode));
                    //Wykonaj procedure rejestracji urzadzenia w bazie danych subprogramu
                    int id = PerformFunctionDB("\"AddParameterConfig\"", parametersDB); //procedura zwraca id utworzonego 
                    if (id > 0)
                    {
                        parameter.ID_Configuration = id;
                        aRes = 0;
                    }
                }
                //Parametr jest zarejestrowany to ustaw ID
                else
                {
                    parameter.ID_Configuration = configPara.ID;
                    parameter.Frequency = configPara.Frequency;
                    parameter.Differance = configPara.Difference;
                    parameter.EnabledAcq = configPara.Enabled;
                    parameter.Mode = (Types.ModeAcq)configPara.Mode;
                    aRes = 0;
                }
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja modyfikuje konfiguracje akwizyji danych dla danego parametru
        public int ModifyConfigPara(Parameter para)
        {
            int aRes = 0;
            if (para != null)
            {
                //Przygotuj parametry dla procedury modife na bazie
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
                parameters.Add(GetParameter("id", DbType.Int32, para.ID_Configuration));
                parameters.Add(GetParameter("freq", DbType.Single, (float)para.Frequency));
                parameters.Add(GetParameter("differ_value", DbType.Single, (float) para.Differance));
                parameters.Add(GetParameter("enabled", DbType.Boolean, para.EnabledAcq));
                parameters.Add(GetParameter("mode", DbType.Int32, (int)para.Mode));

                //Wykonaj procedure modyfikowania usera
                aRes = PerformFunctionDB("\"ModifyParaConfig\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zapisanie parametru w bazie danych
        public int SaveParameter(ProgramParameter programParameter)
        {
            int aRes = -1;
            //Przygotuj parametry dla procedury dodajacej konfiguracje parametru w bazie danych
            List<NpgsqlParameter> parametersDB = new List<NpgsqlParameter>();
            parametersDB.Add(GetParameter("id_", DbType.Int32, programParameter.ID));
            parametersDB.Add(GetParameter("name", DbType.AnsiString, programParameter.Name));
            parametersDB.Add(GetParameter("parameter", DbType.AnsiString, programParameter.Data));
            //Wykonaj procedure rejestracji urzadzenia w bazie danych subprogramu
            int id = PerformFunctionDB("\"SaveParameter\"", parametersDB); //procedura zwraca id utworzonego 
            if (id > 0)
            {
                programParameter.ID = id;
                aRes = 0;
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie wartosci parametru zkojarzonego z podana nazwa
        public int LoadParameter(string nameParameter, out ProgramParameter programParameter)
        {
            int aRes = -1;
             List<ProgramParameter> programParameters = new List<ProgramParameter>();

            programParameter = new ProgramParameter();
            //utworz zapytanie
            string query = " SELECT * FROM \"ProgramParameters\"";
            cmd.CommandText = query;
            //wykonaj zapytanie
            lock (this)
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                //odczytaj wszystkie parametry programu i znajdz ten ktorego szukamy
                while (data.Read())
                {
                    try
                    {
                        if (!data.IsDBNull(data.GetOrdinal("name")) && data.GetString(data.GetOrdinal("name")) == nameParameter)
                        {
                            if (!data.IsDBNull(data.GetOrdinal("id")))
                                programParameter.ID = data.GetInt32(data.GetOrdinal("id"));
                            if (!data.IsDBNull(data.GetOrdinal("parameter")))
                                programParameter.Data = data.GetString(data.GetOrdinal("parameter"));

                            aRes = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.AddException(ex);
                    }
                }
                data.Close();
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie ID urzadzenia/parametru z bazy danych . Rozpoznaien nastepuje po nazwie urzadzenia/parametru. Druho parametr okresla czy szukam urzadzenia czy parametru
        private int GetRegisterDeviceID(string name, bool findDev)
        {
            int aRes = 0;
            List<DataBaseDevice> devices = GetDevices();
            if (devices != null)
            {
                foreach (DataBaseDevice device in devices)
                {
                    if (findDev)
                    {
                        if (device.DeviceName == name)
                            aRes = device.DeviceID;
                    }
                    else
                    {
                        if (device.ParaName == name)
                            aRes = device.ParaID;
                    }
                }
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie ID konfiguracji parametru arejestrowanego juz w bazie danych
        private ConfigPara GetConfigPara(int aParaID)
        {
            ConfigPara configParaRes = new ConfigPara() ;
            List<ConfigPara> parameters = GetConfigParameters();

            configParaRes.ID = 0;
            foreach (ConfigPara configPara in parameters)
            {
                if (configPara.ParameterID == aParaID)
                    configParaRes = configPara;
            }            
            return configParaRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie dodanie do bazy danych typu gazu
        public int AddGasType(GasType gasType)
        {
            int aRes = -1;
            if (gasType != null)
            {
                //Przygotuj parametry dla procedury dodajacej konfiguracje parametru w bazie danych
                List<NpgsqlParameter> parametersDB = new List<NpgsqlParameter>();
                parametersDB.Add(GetParameter("name", DbType.AnsiString, gasType.Name));
                parametersDB.Add(GetParameter("description", DbType.AnsiString, gasType.Description));
                parametersDB.Add(GetParameter("factor", DbType.Single, (float)gasType.Factor));
                //Wykonaj procedure rejestracji urzadzenia w bazie danych subprogramu
                int id = PerformFunctionDB("\"AddGasType\"", parametersDB); //procedura zwraca id utworzonego 
                if (id > 0)
                {
                    gasType.ID = id;
                    aRes = 0;
                }
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja modyfikuje konfiguracje akwizyji danych dla danego parametru
        public int ModifyGasType(GasType gasType)
        {
            int aRes = 0;
            if (gasType != null)
            {
                //Przygotuj parametry dla procedury modife na bazie
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
                parameters.Add(GetParameter("id_gas_type", DbType.Int32, gasType.ID));
                parameters.Add(GetParameter("name", DbType.AnsiString, gasType.Name));
                parameters.Add(GetParameter("description", DbType.AnsiString, gasType.Description));
                parameters.Add(GetParameter("factor", DbType.Single, (float)gasType.Factor));
                //Wykonaj procedure modyfikowania usera
                aRes = PerformFunctionDB("\"ModifyGasType\"", parameters);
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworzenie lsity parametrow sla standardowej funkcji usuwania rekordu za bazy danych posiadajacej tylko jedne parametro nazwie ID
        public int RemoveGasType(int aId)
        {
            int aRes = -1;
            //Przygotuj parametry dla procedury modife na bazie
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(GetParameter("id_gas_type", DbType.Int32, aId));
            //Wykonaj procedure modyfikowania usera
            aRes = PerformFunctionDB("\"RemoveGasType\"", parameters);

            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zarejestrowanie urzadzenia w bazie danych
        public int AddData(DataBaseData data)
        {
            int aRes = -1;

            if (data.ValuePtr != null)
            {
                //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
                List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
                parameters.Add(GetParameter("para_id", DbType.Int32, data.ID_Para));
                parameters.Add(GetParameter("value_", DbType.Single, (float)data.Value));
                parameters.Add(GetParameter("unit_", DbType.AnsiString, data.Unit));
                parameters.Add(GetParameter("date_", DbType.DateTime, data.Date));
                parameters.Add(GetParameter("proces_id", DbType.Int32, data.ProcesID));

                //Wykonaj procedure zapisu danych pomiarowytch w bazie danych jako danych historycznych
                aRes = PerformFunctionDB("\"AddData\"", parameters); //procedura zwraca id utworzonego 
            }
            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zapis daty poczatku sesji zapisu danych do bazy danych
        public int StartSesion(string processName,byte[] proces_para)
        {
            int aRes = -1;
            //Program.ProcesParameter para = Types.ConvertByteToProcesParameter(proces_para);

            //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(GetParameter("start_date", DbType.DateTime, DateTime.Now));
            NpgsqlParameter parameter = GetParameter("process_parameter", DbType.Object, proces_para);
            //Poniewaz parametry procesu sa zapisywane jako ciag byte ustaw odpowieni typ danych jako bytea
            parameter.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
            parameters.Add(parameter);// GetParameter("process_parameter", DbType.Object, proces_para));
            //Wykonaj procedurerozpoczecia sesji zapisu danych do bazy danych
            aRes = PerformFunctionDB("\"StartSesion\"", parameters); //procedura zwraca id utworzonego rekordu sesji

            return aRes;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        /**
         * Funkcja ma za zadanie pobranie danych z bazy danych dla danego procesu
         */ 
        public List<DataBaseData> GetHistoryData(int procesID)
        {
            List<DataBaseData> listData = new List<DataBaseData>();

            //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(GetParameter("ProcesID", DbType.Int32, procesID));
            //Wykonaj procedurerozpoczecia sesji zapisu danych do bazy danych
            DataSet dataSet = null;
            PerformFunctionDB("\"GetProcesData\"", parameters, out dataSet); //procedura zwraca id utworzonego 

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        //if(dataSet.Tables[0].Rows[i].ItemArray. == 5)
                        {
                            DataBaseData data = new DataBaseData();

                            data.ID_Para = (int)dataSet.Tables[0].Rows[i].ItemArray[1];
                            data.Value = (float)dataSet.Tables[0].Rows[i].ItemArray[2];
                            data.Date = (DateTime)dataSet.Tables[0].Rows[i].ItemArray[4];

                            listData.Add(data);
                        }
                    }
                }
            }
            return listData;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie pobranie danych z bazy danych z danego okresu
        public List<DataBaseData> GetHistoryData(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            List<DataBaseData> listData = new List<DataBaseData>();

            //Przygotuj parametry dla procedury rejestracji urzadzenia w bazie danych
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            parameters.Add(GetParameter("StartDate", DbType.DateTime, dateTimeStart));
            parameters.Add(GetParameter("EndDate", DbType.DateTime, dateTimeEnd));
            //Wykonaj procedurerozpoczecia sesji zapisu danych do bazy danych
            DataSet dataSet = null;
            PerformFunctionDB("\"GetData\"", parameters, out dataSet); //procedura zwraca id utworzonego 

            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        //if(dataSet.Tables[0].Rows[i].ItemArray. == 5)
                        {
                            DataBaseData data = new DataBaseData();

                            data.ID_Para = (int)dataSet.Tables[0].Rows[i].ItemArray[1];
                            data.Value = (float)dataSet.Tables[0].Rows[i].ItemArray[2];
                            data.Date = (DateTime)dataSet.Tables[0].Rows[i].ItemArray[4];

                            listData.Add(data);
                        }
                    }
                }
            }
            //Wykonaj korekcje danych o zakres sesji to znaczy wprowadz wartosci pomiedzy punkty nalezace do roznych sesji aby nie rysowac miedzy nimi lini na wykresie
            CorectDataAboutSesion(listData, dateTimeStart, dateTimeEnd);
            //Posortuj liste po datach rosnaco aby wykres zostal poprawnie narysowany
          //  SortList(listData);
            return listData;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie wstawianie pkt zero pomiedzy dwa sasiednie punkty nalezacy do roznych sesji. Ma to na celu przerywanie lini na wykresie aby nie sugerowac userowi ze dane byly odczydtywane w sytuacji gdy ich nie bylo
        private void CorectDataAboutSesion(List<DataBaseData> listData, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            List<Sesion> listSesions = GetSesions(dateTimeStart, dateTimeEnd); // Lokalna lista sesji aby za kazdym razem nie pobierac tego z bazy
            if(listData != null)
            {
                DataBaseData dataFirst;
                DataBaseData dataNext;
                //Posortuj liste po datach rosnaco
              //  SortList(listData);
                //Usun z lsity danych dene ktore nie odpowiadaja danemu procesowi
               // listData = RemoveDataNotBelongToProcess(listSesions,listData, processName);
                //Uzupelnij dane brakami w sytaucji gdy w danymn czanie nie bylo akwizycji danych
                for (int i = 0; i < listData.Count - 1; i++)
                {
                    //Pobierz dwa sąsiednie punkty tego samego parametru
                    dataFirst   = listData[i];
                    dataNext    = GetNextPara(listData, dataFirst.ID_Para, i + 1);
                    //Jezeli zostal znaleziony sasiedni punkt i wartosc pierwszego jezt rozna od zera to dodaj dodatkowe punkty 0 aby nie rysowac danych na wykresie dla nieistniejacych sesji
                    if (dataFirst.ID_Para == dataNext.ID_Para && dataFirst.Value != 0)
                    {
                        //Podaj mi id sesji do ktorej nalezy dany punkt
                        int idSesionFirstPkt = GetIdSesion(listSesions,dataFirst.Date);
                        int idSesionNextPkt = GetIdSesion(listSesions,dataNext.Date);
                        //Sprawdzam czy oba punkty naleza do tej samej sesji jezeli nie to dla obydwu dodaje punkt 0 co spowoduje sciagniecie wykresu do wartosci 0
                        if(idSesionFirstPkt != idSesionNextPkt)
                        {
                            DataBaseData newDataFirst = new DataBaseData();
                            newDataFirst.ID_Para  = dataFirst.ID_Para;
                            newDataFirst.Unit     = dataFirst.Unit;
                            newDataFirst.Date     = dataFirst.Date.AddSeconds(1);
                            newDataFirst.ValuePtr = dataFirst.ValuePtr;
                            newDataFirst.Value    = 0;

                            DataBaseData newDataNext = new DataBaseData();
                            newDataNext.ID_Para  = dataNext.ID_Para;
                            newDataNext.Unit     = dataNext.Unit;
                            newDataNext.Date     = dataNext.Date.AddSeconds(-1);
                            newDataNext.ValuePtr = dataNext.ValuePtr;
                            newDataNext.Value    = 0;

                            listData.Add(newDataFirst);
                            listData.Add(newDataNext);
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Usun z lsity danych dene ktore nie odpowiadaja danemu procesowi
        private List<DataBaseData> RemoveDataNotBelongToProcess(List<Sesion>listSesions,List<DataBaseData> listDataIn, string processName)
        {
            bool aRemove = true;

            //Jezeli nazwa procesu jest null lub "all data" to znaczy ze nie chce nic usuwac
            if (processName != null && processName != "All data")
            {
                if (listDataIn != null && listSesions != null)
                {
                    for(int i = 0; i < listDataIn.Count; i++)
                    {
                        DataBaseData data = listDataIn[i];
                        aRemove = true;
                        foreach (Sesion sesion in listSesions)
                        {
                            if(sesion.ProcesName == processName && data.Date >= sesion.StartDate && data.Date <= sesion.EndDate)
                            {
                                aRemove = false;
                                break;
                            }
                        }
                        if (aRemove)
                        {
                            listDataIn.Remove(data);
                            i--;
                        }
                    }
                }
            }
            List<DataBaseData> listDateOut = listDataIn;
            return listDateOut;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie zwrocenie id sesji do ktroej nalezy dany punkt
        private int GetIdSesion(List<Sesion> listSesions ,DateTime date)
        {
            int aId = 0;
            if (listSesions != null)
            {
                foreach (Sesion sesion in listSesions)
                {
                    if (date >= sesion.StartDate && date <= sesion.EndDate)
                        aId = sesion.ID;
                }
            }         
            return aId;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcaj ma za zadanie podanie nastepnego punkty tego samego parametru z listy
        private DataBaseData GetNextPara(List<DataBaseData> listData,int idPara,int index)
        {
            DataBaseData data = new DataBaseData();
            for (int i = index; i < listData.Count ; i++)
            {
                //Pobierz pierwszy z brzegu punkt tego samego parametru ktroy zostal przekazany jako parametr
                if(listData[i].ID_Para == idPara)
                {
                    data = listData[i];
                    break;
                }
            }
            return data;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie sortowanie listy
        private void SortList(List<DataBaseData>listData)
        {
            if (listData != null)
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    for (int j = 0; j < listData.Count - 1; j++)
                    {
                        if (listData[j].Date > listData[j + 1].Date)
                        {
                            DataBaseData data = listData[j];
                            listData[j] = listData[j + 1];
                            listData[j + 1] = data;
                        }
                    }
                }
            }
        } 
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworzenie lsity parametrow sla standardowej funkcji usuwania rekordu za bazy danych posiadajacej tylko jedne parametro nazwie ID
        private List<NpgsqlParameter> GetRemoveStandardParameters(int aId)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();

           //utworz parametr id                    
            parameters.Add(GetParameter("id", DbType.Int32, aId));

            return parameters;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworzenie parametrow procedury Add/Modify user 
        private List<NpgsqlParameter> GetUserParameters(User user, TypeFillParameters typeFill)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
      
            //Wpraowdz parametr ID. Jest on wymagany dla procedur Remove oraz Modify
            if (typeFill == TypeFillParameters.ModifyUser || typeFill == TypeFillParameters.RemoveUser)
            {
                //utworz parametr id 
                parameters.Add(GetParameter("id", DbType.Int32, user.ID));
            }
            //WPrawdz parametry funkcji New/Modify na podstawie ustawien usera
            if (typeFill == TypeFillParameters.ModifyUser || typeFill == TypeFillParameters.NewUser)
            {
                //utworz parametr name 
                parameters.Add(GetParameter("name", DbType.AnsiString, user.Name));
                //utworz parametr surnname                    
                parameters.Add(GetParameter("surname", DbType.AnsiString, user.Surname));
                //utworz parametr login                    
                parameters.Add(GetParameter("login", DbType.AnsiString, user.Login));  
                //utworz parametr password                    
                parameters.Add(GetParameter("password", DbType.AnsiString, user.Password));  
                //utworz parametr allow_change psw                    
                parameters.Add(GetParameter("allow_change_psw", DbType.Boolean, user.AllowChangePsw));
                //utworz parametr typ blokowania usera                    
                parameters.Add(GetParameter("type_block", DbType.Int32, user.DisableAccount)); 
                //utworz parametr uprawnienia                    
                parameters.Add(GetParameter("privilige", DbType.Int32, user.Privilige));
                //utworz parametr data poczaktu ewentualnej blokady                    
                parameters.Add(GetParameter("start_date_block", DbType.Date, user.DateStartDisableAccount));
                //utworz parametr data konca ewentualnej blokady                      
                parameters.Add(GetParameter("end_date_block", DbType.Date, user.DateEndDisableAccount));
            }
            return parameters;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworzenie parametrow procedury Add/Modify dla programu 
        private List<NpgsqlParameter> GetProgramParameters(Program.Program program, TypeFillParameters typeFill)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            NpgsqlParameter para = null;
            if (program != null)
            {
                //Wpraowdz parametr ID. Jest on wymagany dla procedur Remove oraz Modify
                if (typeFill == TypeFillParameters.ModifyUser || typeFill == TypeFillParameters.RemoveUser)
                {
                    //utworz parametr id                    
                    parameters.Add(GetParameter("id", DbType.Int32, program.GetID()));
                }
                //WPrawdz parametry funkcji New/Modify na podstawie ustawien usera
                if (typeFill == TypeFillParameters.ModifyUser || typeFill == TypeFillParameters.NewUser)
                {
                    //utworz parametr name 
                    parameters.Add(GetParameter("name", DbType.AnsiString, program.GetName()));
                    //utworz parametr surnname                    
                    parameters.Add(GetParameter("description", DbType.AnsiString, program.GetDescription()));
                }
            }
            return parameters;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie utworzenie parametrow procedury Add/Modify dla subprogramu 
        private List<NpgsqlParameter> GetSubprogramParameters(Program.Subprogram subprogram, TypeFillParameters typeFill)
        {
            List<NpgsqlParameter> parameters = new List<NpgsqlParameter>();
            NpgsqlParameter para = null;
            if (subprogram != null)
            {
                //Wpraowdz parametr ID. Jest on wymagany dla procedur Remove oraz Modify
                if (typeFill == TypeFillParameters.ModifyUser || typeFill == TypeFillParameters.RemoveUser)
                {
                    //utworz parametr id                    
                    parameters.Add(GetParameter("id", DbType.Int32, subprogram.ID));
                }
                //WPrawdz parametry funkcji New/Modify na podstawie ustawien usera
                if (typeFill == TypeFillParameters.ModifyUser || typeFill == TypeFillParameters.NewUser)
                {
                    //utworz parametr name 
                    parameters.Add(GetParameter("name" , DbType.AnsiString , subprogram.GetName()));

                    //utworz parametr surnname                    
                    parameters.Add(GetParameter("description", DbType.AnsiString, subprogram.GetDescription()));
                }
            }
            return parameters;
        }
        //-------------------------------------------------------------------------------------
        private NpgsqlParameter GetParameter(string name, DbType type, object value)
        {
            NpgsqlParameter para = new NpgsqlParameter();       //utworz parametr description                    

            para.ParameterName  = name;
            para.DbType         = type;
            para.Value          = value;

            return para;
        }
        //------------------------------------------------------------------------------------------------------------------------------
        //Funkcja ma za zadanie przekopiowanie parametrow z lity do parametrow komendy utworzonych na bazie obiektu komendy
        public void PrepareParameters(NpgsqlCommand cmd, List<NpgsqlParameter> parameters)
        {
            if (cmd != null && parameters != null)
            {
                NpgsqlParameter paraCMD = null;
                foreach (NpgsqlParameter para in parameters)
                {
                    paraCMD = cmd.CreateParameter();       //utworz parametr name                    
                    paraCMD.ParameterName = para.ParameterName;
                    paraCMD.DbType = para.DbType;
                    paraCMD.NpgsqlDbType = para.NpgsqlDbType;
                    paraCMD.Value = para.NpgsqlValue;
                    cmd.Parameters.Add(paraCMD);
                }
            }
        }
        //-------------------------------------------------------------------------------------
        private int PerformFunctionDB(string functionName, List<NpgsqlParameter> parameters)
        {
            int aRes = -1;
            NpgsqlTransaction trans = null;
            lock (this)
            {
                try
                {
                    trans = conn.BeginTransaction();   //Rozpoczecie tranzakcji
                                                       //Powolonie pbiektu komendy i nadaniu mu komendy jako nazwy procedury ktora ma wykonac. Nazwa musi byc w cudzyslowiach bo wtedy sa rozpoznawane duze i male litery
                    NpgsqlCommand cmd = new NpgsqlCommand(functionName, conn);
                    //Ustawienie komendy jako komendy dla ProceduryStorowanej
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Uzupelnij liste parametrow
                    PrepareParameters(cmd, parameters);
                    //Przygotuj obiekty wymagane do wykonania procedure
                    NpgsqlDataAdapter data = new NpgsqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    //wypelnij data set danymi odebrannymi z procedury
                    data.Fill(ds);
                    //Odbierz wynik zwracany funkcje przez bazy danych
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        aRes = (int)ds.Tables[0].Rows[0].ItemArray[0];
                }
                //Jezeli zdazyl sie jakis problem to zglos to
                catch (Exception ex)
                {
                    Logger.AddException(ex);
                }
                //Zakonczenie tranzakcji
                finally
                {
                    if (trans != null)
                        trans.Commit();
                }
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------
        private int PerformFunctionDB(string functionName, List<NpgsqlParameter> parameters, out DataSet ds)
        {
            int aRes = -1;
            ds = new DataSet();
            NpgsqlTransaction trans = null;
            lock (this)
            {
                try
                {
                    trans = conn.BeginTransaction();   //Rozpoczecie tranzakcji
                                                       //Powolonie pbiektu komendy i nadaniu mu komendy jako nazwy procedury ktora ma wykonac. Nazwa musi byc w cudzyslowiach bo wtedy sa rozpoznawane duze i male litery
                    NpgsqlCommand cmd = new NpgsqlCommand(functionName, conn);
                    //Ustawienie komendy jako komendy dla ProceduryStorowanej
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Uzupelnij liste parametrow
                    PrepareParameters(cmd, parameters);
                    //Przygotuj obiekty wymagane do wykonania procedure
                    NpgsqlDataAdapter data = new NpgsqlDataAdapter(cmd);
                    //wypelnij data set danymi odebrannymi z procedury
                    data.Fill(ds);
                    //Odbierz wynik zwracany funkcje przez bazy danych
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        aRes = (int)ds.Tables[0].Rows[0].ItemArray[0];
                }
                //Jezeli zdazyl sie jakis problem to zglos to
                catch (Exception ex)
                {
                    Logger.AddException(ex);
                }
                //Zakonczenie tranzakcji
                finally
                {
                    if (trans != null)
                        trans.Commit();
                }
            }
            return aRes;
        }
        //-------------------------------------------------------------------------------------
    }
}
