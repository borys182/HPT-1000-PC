--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- Name: AddData(integer, real, character varying, timestamp without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "AddData"(para_id integer, value_ real, unit_ character varying, date_ timestamp without time zone) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	id_data		integer := 0;
	id_sesion 	integer := 0;
	
BEGIN
	--Dodaj rekord danych
	insert into data(id_parameter,value,unit,date)values(para_id,value_,unit_,date_)returning id into id_data;
	
	--Aktualizuj czas trwania sesji. Odczytaj rekord aktualnej sesji i aktualizuj czas end
	select current_sesion_id from current_sesion into id_sesion;

	update sesions set date_end = date_ where id = id_sesion;

	RETURN id_data;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."AddData"(para_id integer, value_ real, unit_ character varying, date_ timestamp without time zone) OWNER TO postgres;

--
-- Name: AddGasType(character varying, text, real); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "AddGasType"(name_ character varying, description_ text, factor_ real) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	id_res		integer := 0;
	
BEGIN
	-- Dodaj do tablicy gas_types nowy typ gazu
	insert into gas_types(name,description,factor)
		values(name_,description_,factor_ )returning id into id_res;

	RETURN id_res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."AddGasType"(name_ character varying, description_ text, factor_ real) OWNER TO postgres;

--
-- Name: AddParameterConfig(integer, real, real, boolean, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "AddParameterConfig"(parameter_id integer, freq real, differ_value real, enabled boolean, mode integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	id_para_config		integer := 0;
	temp			integer := 0;
	
BEGIN
	--Sprawdz czy czasem nie jest juz zarejestroany dany parametr
	select count(*) from acquisition_configurations where id_parameter = parameter_id into temp;
	
	IF temp = 0 THEN
	
		--Dodaj rekord rejestrujacy konfiguracje parametru
		insert into acquisition_configurations(id_parameter , frequency , difference_value , enabled_acq , mode_acq)
		values(parameter_id , freq , differ_value , enabled , mode )returning id into id_para_config;
	END IF;

	RETURN id_para_config;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."AddParameterConfig"(parameter_id integer, freq real, differ_value real, enabled boolean, mode integer) OWNER TO postgres;

--
-- Name: AddSubprogram(integer, character varying, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "AddSubprogram"(program_id integer, name_subprogram character varying, description text) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	subprogram_id	integer := 0;
	id_program 	integer := null;
	id_vent 	integer := null;
	id_pump 	integer := null;
	id_purge 	integer := null;
	id_gas	 	integer := null;
	id_power_supply	integer := null;
	
BEGIN
	--Utworzenie rekordow w tabelach stepow danego subprmagru
	insert into stages_pump(setpoint_pressure)values(NULL)returning id into id_pump;
	insert into stages_purge(time)values(NULL)returning id into id_purge;
	insert into stages_vent(vent_time)values(NULL)returning id into id_vent;
	insert into stages_power_supply(setpoint)values(NULL)returning id into id_power_supply;
	insert into stages_gas(mode_gas)values(NULL)returning id into id_gas;

	insert into subprograms(name,description,id_pump,id_vent,id_purge,id_power_supplay,id_gas)
	values(name_subprogram,description,id_pump,id_vent,id_purge,id_power_supply,id_gas)
	returning id into subprogram_id;

	insert into connections_program_subprograms values(program_id,subprogram_id);
	
	RETURN subprogram_id;

	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Name of subprogram alredy exist';
	--	WHEN NOT_NULL_VIOLATION THEN
	--		RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."AddSubprogram"(program_id integer, name_subprogram character varying, description text) OWNER TO postgres;

--
-- Name: GetData(timestamp without time zone, timestamp without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "GetData"("StartDate" timestamp without time zone, "EndDate" timestamp without time zone) RETURNS TABLE(id integer, id_parameter integer, value real, unit character varying, date timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
DECLARE 
	res integer := 0;
	
BEGIN

	RETURN QUERY Select * from data where data.date >= "StartDate" and data.date <= "EndDate" ;

END;
$$;


ALTER FUNCTION public."GetData"("StartDate" timestamp without time zone, "EndDate" timestamp without time zone) OWNER TO postgres;

--
-- Name: GetSesions(timestamp without time zone, timestamp without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "GetSesions"("StartDate" timestamp without time zone, "EndDate" timestamp without time zone) RETURNS TABLE(id integer, date_start timestamp without time zone, date_end timestamp without time zone, process_name character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE 
	res integer := 0;
	
BEGIN

	RETURN QUERY Select * from sesions where sesions.date_start >= "StartDate" and sesions.date_start <= "EndDate" or sesions.date_start < "StartDate" and sesions.date_end >= "StartDate" ;

END;
$$;


ALTER FUNCTION public."GetSesions"("StartDate" timestamp without time zone, "EndDate" timestamp without time zone) OWNER TO postgres;

--
-- Name: ModifyGasType(integer, character varying, text, real); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifyGasType"(id_gas_type integer, name_ character varying, description_ text, factor_ real) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	res 	integer := 0;
  BEGIN

	update gas_types set name = name_, description = description_, factor = factor_	where gas_types.id = id_gas_type;
	
	RETURN res;
	
	EXCEPTION
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END$$;


ALTER FUNCTION public."ModifyGasType"(id_gas_type integer, name_ character varying, description_ text, factor_ real) OWNER TO postgres;

--
-- Name: ModifyParaConfig(integer, real, real, boolean, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifyParaConfig"(id_ integer, freq real, differ_value real, enabled boolean, mode integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	res 		integer := 0;
  BEGIN

	update acquisition_configurations set frequency = freq, difference_value = differ_value, enabled_acq = enabled,mode_acq = mode
	where  acquisition_configurations.id = id_;

	RETURN res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
  END;$$;


ALTER FUNCTION public."ModifyParaConfig"(id_ integer, freq real, differ_value real, enabled boolean, mode integer) OWNER TO postgres;

--
-- Name: ModifyPrivilige(integer, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifyPrivilige"(id integer, name character varying, value integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$
DECLARE 
	res integer := 0;
BEGIN
	update types_privilige set name = $2, value = $3 where types_privilige.id = $1;
	return res;
END;

$_$;


ALTER FUNCTION public."ModifyPrivilige"(id integer, name character varying, value integer) OWNER TO postgres;

--
-- Name: ModifyProgram(integer, character varying, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifyProgram"(id integer, name character varying, description text) RETURNS integer
    LANGUAGE plpgsql
    AS $_$ DECLARE
	res 		integer := 0;
  BEGIN

	update programs set name = $2, description = $3	where programs.id = $1;
	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Program name alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;$_$;


ALTER FUNCTION public."ModifyProgram"(id integer, name character varying, description text) OWNER TO postgres;

--
-- Name: ModifySubprogram(integer, character varying, text, boolean, boolean, boolean, boolean, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifySubprogram"(id integer, name character varying, description text, active_pump boolean, active_power_supply boolean, active_gas boolean, active_purge boolean, active_vent boolean) RETURNS integer
    LANGUAGE plpgsql
    AS $_$  DECLARE
	res	integer := 0;
  BEGIN

	update subprograms set name = $2, description = $3,pump_active = active_pump,power_supply_active = active_power_supply,gas_active = active_gas,purge_active = active_purge,vent_active = active_vent
	where subprograms.id = $1;
	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Program name alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;$_$;


ALTER FUNCTION public."ModifySubprogram"(id integer, name character varying, description text, active_pump boolean, active_power_supply boolean, active_gas boolean, active_purge boolean, active_vent boolean) OWNER TO postgres;

--
-- Name: ModifySubprogramStages_1(integer, time without time zone, time without time zone, time without time zone, real, real, integer, real, time without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifySubprogramStages_1"(id_subprogram integer, time_vent time without time zone, time_purge time without time zone, time_pump time without time zone, setpoint_pressure_pump real, setpoint_power_supply real, mode_power_supply integer, max_devation_power_supply real, time_duration_power_supply time without time zone) RETURNS integer
    LANGUAGE plpgsql
    AS $$
  DECLARE
	res 		integer := 0;
	pump_id 	integer := 0;
	vent_id 	integer := 0;
	purge_id 	integer := 0;
	power_supply_id	integer := 0;
	
  BEGIN

	select id_pump 		into pump_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_vent 		into vent_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_purge 	into purge_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_power_supplay into power_supply_id from subprograms where subprograms.id = id_subprogram; 

	update stages_pump set max_time_pump = time_pump, setpoint_pressure = setpoint_pressure_pump where stages_pump.id = pump_id;
	update stages_vent set vent_time = time_vent where stages_vent.id = vent_id;
	update stages_purge set time = time_purge where stages_purge.id = purge_id;
	update stages_power_supply set setpoint = setpoint_power_supply ,mode = mode_power_supply, duration_time = time_duration_power_supply, max_devation = max_devation_power_supply where stages_power_supply.id = power_supply_id;

	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Program name alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;$$;


ALTER FUNCTION public."ModifySubprogramStages_1"(id_subprogram integer, time_vent time without time zone, time_purge time without time zone, time_pump time without time zone, setpoint_pressure_pump real, setpoint_power_supply real, mode_power_supply integer, max_devation_power_supply real, time_duration_power_supply time without time zone) OWNER TO postgres;

--
-- Name: ModifySubprogramStages_2(integer, boolean, boolean, boolean, boolean, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, time without time zone, integer, integer, integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifySubprogramStages_2"(id_subprogram integer, active_mfc1 boolean, active_mfc2 boolean, active_mfc3 boolean, active_vaporaiser boolean, flow_mfc1 real, flow_mfc2 real, flow_mfc3 real, min_flow_mfc1 real, min_flow_mfc2 real, min_flow_mfc3 real, max_flow_mfc1 real, max_flow_mfc2 real, max_flow_mfc3 real, vaporaiser_cycle real, vaporaiser_time_on real, setpoint_press real, max_dev_up real, max_dev_down real, mfc1_max_dev real, mfc2_max_dev real, mfc3_max_dev real, gas_share_mfc1 real, gas_share_mfc2 real, gas_share_mfc3 real, "time" time without time zone, mode_process integer, mfc1_id_type_gas integer, mfc2_id_type_gas integer, mfc3_id_type_gas integer, vaporaiser_dosing_ integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	res 		integer := 0;
	gas_id 		integer := 0;
  BEGIN

	select id_gas into gas_id from subprograms where id = id_subprogram;

	update stages_gas set mfc1_flow = flow_mfc1, mfc2_flow = flow_mfc2, mfc3_flow = flow_mfc3,
	mfc1_min_flow = min_flow_mfc1 , mfc2_min_flow = min_flow_mfc2 , mfc3_min_flow = min_flow_mfc3,
	mfc1_max_flow = max_flow_mfc1 , mfc2_max_flow = max_flow_mfc2 , mfc3_max_flow = max_flow_mfc3, 
	vaporaiser_cycle_time = vaporaiser_cycle , vaporaiser_on_time = vaporaiser_time_on,
	setpoint_pressure = setpoint_press, max_devation_up = max_dev_up, max_devation_down = max_dev_down,
	mfc1_max_devation = mfc1_max_dev, mfc2_max_devation = mfc2_max_dev, mfc3_max_devation = mfc3_max_dev,
	mfc1_gas_share = gas_share_mfc1, mfc2_gas_share = gas_share_mfc2, mfc3_gas_share = gas_share_mfc3,
	time_duration = time , mode_gas = mode_process,
	mfc1_active = active_mfc1,mfc2_active = active_mfc2,mfc3_active = active_mfc3, vaporaiser_active = active_vaporaiser,
	mfc1_id_gas_type = mfc1_id_type_gas , mfc2_id_gas_type = mfc2_id_type_gas , mfc3_id_gas_type = mfc3_id_type_gas,vaporaiser_dosing = vaporaiser_dosing_
	where stages_gas.id = gas_id;
	
	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Program name alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;$$;


ALTER FUNCTION public."ModifySubprogramStages_2"(id_subprogram integer, active_mfc1 boolean, active_mfc2 boolean, active_mfc3 boolean, active_vaporaiser boolean, flow_mfc1 real, flow_mfc2 real, flow_mfc3 real, min_flow_mfc1 real, min_flow_mfc2 real, min_flow_mfc3 real, max_flow_mfc1 real, max_flow_mfc2 real, max_flow_mfc3 real, vaporaiser_cycle real, vaporaiser_time_on real, setpoint_press real, max_dev_up real, max_dev_down real, mfc1_max_dev real, mfc2_max_dev real, mfc3_max_dev real, gas_share_mfc1 real, gas_share_mfc2 real, gas_share_mfc3 real, "time" time without time zone, mode_process integer, mfc1_id_type_gas integer, mfc2_id_type_gas integer, mfc3_id_type_gas integer, vaporaiser_dosing_ integer) OWNER TO postgres;

--
-- Name: ModifyTypeBlockAccount(integer, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifyTypeBlockAccount"(id integer, name character varying, value integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$DECLARE
	res integer := 0;
BEGIN

update types_block_account set name = $2, value = $3 where types_block_account.id = $1;
	return res;

END;$_$;


ALTER FUNCTION public."ModifyTypeBlockAccount"(id integer, name character varying, value integer) OWNER TO postgres;

--
-- Name: ModifyUser(integer, character varying, character varying, character varying, character varying, boolean, integer, integer, date, date); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "ModifyUser"(id integer, name character varying, surname character varying, login character varying, password character varying, allow_change_psw boolean, type_block integer, privilige integer, start_date date, end_date date) RETURNS integer
    LANGUAGE plpgsql
    AS $_$  DECLARE
	res 		integer := 0;
	a_id_privilige 	integer := null;
	a_id_type_block integer := null;
  BEGIN

	select types_block_account.id from types_block_account where value = type_block into a_id_type_block;	
	select types_privilige.id from types_privilige where value = privilige into a_id_privilige;	

	update users set name = $2, surname = $3, login = $4, password = $5, allow_change_psw = $6,  
	id_type_block_account = a_id_type_block, id_privilige = a_id_privilige, start_date_block_account = $9, end_date_block_account = $10
	where users.id = $1;
	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Login alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;
$_$;


ALTER FUNCTION public."ModifyUser"(id integer, name character varying, surname character varying, login character varying, password character varying, allow_change_psw boolean, type_block integer, privilige integer, start_date date, end_date date) OWNER TO postgres;

--
-- Name: NewPrivilige(character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "NewPrivilige"(name character varying, value integer) RETURNS integer
    LANGUAGE sql
    AS $_$
insert into types_privilige(name,value)values($1,$2)
returning id
$_$;


ALTER FUNCTION public."NewPrivilige"(name character varying, value integer) OWNER TO postgres;

--
-- Name: NewProgram(character varying, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "NewProgram"(name character varying, description text) RETURNS integer
    LANGUAGE plpgsql
    AS $_$DECLARE
	id_res 		integer := 0;
BEGIN

	insert into programs(name,description)values($1,$2) returning id into id_res;

	RETURN id_res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$_$;


ALTER FUNCTION public."NewProgram"(name character varying, description text) OWNER TO postgres;

--
-- Name: NewTypeBlockAccount(character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "NewTypeBlockAccount"(name character varying, value integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$
DECLARE
	res integer := 0;
BEGIN

insert into types_block_account(name,value)values($1,$2) returning id into res;
RETURN res;

END;$_$;


ALTER FUNCTION public."NewTypeBlockAccount"(name character varying, value integer) OWNER TO postgres;

--
-- Name: NewUser(character varying, character varying, character varying, character varying, boolean, integer, integer, date, date); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "NewUser"(name character varying, surname character varying, login character varying, password character varying, allow_change_psw boolean, type_block integer, privilige integer, start_date_block date, end_date_block date) RETURNS integer
    LANGUAGE plpgsql
    AS $_$
  
  DECLARE
	id_res 		integer := 0;
	id_privilige 	integer := null;
	id_type_block 	integer := null;
BEGIN

	select id from types_block_account where value = type_block into id_type_block;	
	select id from types_privilige where value = privilige into id_privilige;	

	insert into users(name,surname,login,password,allow_change_psw,id_type_block_account,id_privilige,start_date_block_account,end_date_block_account) 
	values($1,$2,$3,$4,$5,id_type_block,id_privilige,$8,$9) returning id into id_res;

	RETURN id_res;

	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Login alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
END;
$_$;


ALTER FUNCTION public."NewUser"(name character varying, surname character varying, login character varying, password character varying, allow_change_psw boolean, type_block integer, privilige integer, start_date_block date, end_date_block date) OWNER TO postgres;

--
-- Name: RegisterDevice(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RegisterDevice"(dev_name character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	res	integer := 0;
	
BEGIN
	--Utworzenie rekordow w tabelach stepow danego subprmagru
	insert into devices(name)values(dev_name)returning id into res;
		
	RETURN res;

	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Name of device alredy exist';
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."RegisterDevice"(dev_name character varying) OWNER TO postgres;

--
-- Name: RegisterParameter(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RegisterParameter"(dev_id integer, para_name character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	para_id	integer := 0;
	
BEGIN
	--dodaj parametr do tablicy
	insert into parameters(name) values(para_name)returning id into para_id;
	--powiaz dodany parametr z urzadzeniem
	insert into device_parameters values(dev_id,para_id);
	
	RETURN para_id;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."RegisterParameter"(dev_id integer, para_name character varying) OWNER TO postgres;

--
-- Name: RemoveGasType(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RemoveGasType"(id_gas_type integer) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	res integer := 0;
BEGIN

	delete from gas_types where gas_types.id = id_gas_type;	

	RETURN res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END$$;


ALTER FUNCTION public."RemoveGasType"(id_gas_type integer) OWNER TO postgres;

--
-- Name: RemovePrivilige(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RemovePrivilige"(id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$DECLARE
	res integer := 0;
BEGIN

delete from types_privilige where types_privilige.id = $1;
RETURN res;

END;$_$;


ALTER FUNCTION public."RemovePrivilige"(id integer) OWNER TO postgres;

--
-- Name: RemoveProgram(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RemoveProgram"(id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$DECLARE
	res integer := 0;
	connection connections_program_subprograms%rowtype;
BEGIN
	--Usun subprogramy nalezace do danego programu
	FOR connection IN select * from connections_program_subprograms where connections_program_subprograms.id_program = $1
	LOOP
		select "RemoveSubprogram"(connection.id_subprogram) into res;
	
	END LOOP;
	--Usun powiazanie z tabela subprograms
	delete from connections_program_subprograms where id_program = $1;
	--Usun program
	delete from programs where programs.id = $1;
	RETURN res;
	
	EXCEPTION
		WHEN OTHERS THEN
			RAISE;

END;$_$;


ALTER FUNCTION public."RemoveProgram"(id integer) OWNER TO postgres;

--
-- Name: RemoveSubprogram(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RemoveSubprogram"(id_subprogram integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$DECLARE
	res integer := 0;
	pump_id 	integer := 0;
	vent_id 	integer := 0;
	purge_id 	integer := 0;
	power_supply_id	integer := 0;
	gas_id 		integer := 0;
BEGIN
	--odczytaj id parametrow procesu subprogramu
	select id_pump 		into pump_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_vent 		into vent_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_purge 	into purge_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_power_supplay into power_supply_id from subprograms where subprograms.id = id_subprogram; 
	select id_gas 		into gas_id 	     from subprograms where subprograms.id = id_subprogram;
	
	--Usun powiazanie z tabela programs
	delete from connections_program_subprograms where connections_program_subprograms.id_subprogram = $1;

	--usun parametry procesow subprogramu 
	delete from stages_gas 		where stages_gas.id          = gas_id;
	delete from stages_vent 	where stages_vent.id         = vent_id;
	delete from stages_purge 	where stages_purge.id 	     = purge_id;
	delete from stages_pump 	where stages_pump.id 	     = pump_id;
	delete from stages_power_supply where stages_power_supply.id = power_supply_id;
	
	--Usun rekord subprogramu
	delete from subprograms where subprograms.id = $1;
	
	RETURN res;
	EXCEPTION
		WHEN OTHERS THEN
			RAISE;

END$_$;


ALTER FUNCTION public."RemoveSubprogram"(id_subprogram integer) OWNER TO postgres;

--
-- Name: RemoveTypeBlockAccount(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RemoveTypeBlockAccount"(id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$
DECLARE
	res integer := 0;
BEGIN

	delete from types_block_account where types_block_account.id = $1;
	RETURN res;

END;
$_$;


ALTER FUNCTION public."RemoveTypeBlockAccount"(id integer) OWNER TO postgres;

--
-- Name: RemoveUser(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "RemoveUser"(id integer) RETURNS integer
    LANGUAGE plpgsql
    AS $_$  DECLARE
	res integer := 0;
	tmp integer := 0;
   BEGIN
	select COUNT(*) from users where users.id = $1 into tmp;
	IF tmp > 0 then
		delete from users where users.id = $1;
	ELSE
		RAISE 'User does not exist in database';
	END IF;
	RETURN res;
	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
   END;$_$;


ALTER FUNCTION public."RemoveUser"(id integer) OWNER TO postgres;

--
-- Name: SaveParameter(integer, character varying, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "SaveParameter"(id_ integer, name character varying, parameter text) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	id_res 		integer := 0;
	id_para		integer := 0;
BEGIN
	select id from program_configuration where parameter_name= name into id_para;

	IF id_para > 0 THEN
		update program_configuration set data = parameter where id = id_para;
		id_res = id_para;
	ELSE
		insert into program_configuration(parameter_name,data)values(name,parameter) returning id into id_res;
	END IF;

	RETURN id_res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END$$;


ALTER FUNCTION public."SaveParameter"(id_ integer, name character varying, parameter text) OWNER TO postgres;

--
-- Name: StartSesion(timestamp without time zone, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION "StartSesion"(start_date timestamp without time zone, name_process character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$DECLARE
	id_sesion	 integer := 0;
	id_current_sesion integer := 0;
	

BEGIN
	--Utworz rekord sesji danych i zapisz jej id do tabeli current_sesion
	insert into sesions(date_start,date_end,process_name)values(start_date,start_date,name_process)returning id into id_sesion;
	
	--zapisz id_aktualnie wykonywanej sesji do tablicy current_sesion aby mozna bylo dopisywac autoamtycznie date_end podczas zapisu danych doa bazy
	--Odczytaj id pierwszego wpisu z tablicy currnet_sesion. Jezeli nie istnieje to go utwprze w przeciwnym wypadku aktualuizje
	select id from current_sesion into id_current_sesion;
	-- sprawdzam czy zostal juz utworzony wpis do tabeli current_sesion jezeli nie to go tworze w przeciwnym wypadku aktualizuje
	
	if  id_current_sesion > 0 then
		update current_sesion set current_sesion_id = id_sesion where id = id_current_sesion;
	else
		insert into current_sesion(current_sesion_id)values(id_sesion);
	end if;
	
	RETURN id_sesion;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$$;


ALTER FUNCTION public."StartSesion"(start_date timestamp without time zone, name_process character varying) OWNER TO postgres;

--
-- Name: acquisition_configurations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE acquisition_configurations_id_seq
    START WITH 7
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE acquisition_configurations_id_seq OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: acquisition_configurations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE acquisition_configurations (
    id integer DEFAULT nextval('acquisition_configurations_id_seq'::regclass) NOT NULL,
    id_parameter integer NOT NULL,
    frequency real,
    difference_value real,
    enabled_acq boolean NOT NULL,
    mode_acq integer NOT NULL
);


ALTER TABLE acquisition_configurations OWNER TO postgres;

--
-- Name: ConfigParametersAcq; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "ConfigParametersAcq" AS
 SELECT acquisition_configurations.id,
    acquisition_configurations.id_parameter,
    acquisition_configurations.frequency,
    acquisition_configurations.difference_value,
    acquisition_configurations.enabled_acq,
    acquisition_configurations.mode_acq
   FROM acquisition_configurations;


ALTER TABLE "ConfigParametersAcq" OWNER TO postgres;

--
-- Name: device_parameters; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE device_parameters (
    id_device integer NOT NULL,
    id_parameter integer NOT NULL
);


ALTER TABLE device_parameters OWNER TO postgres;

--
-- Name: devices_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE devices_id_seq
    START WITH 4
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE devices_id_seq OWNER TO postgres;

--
-- Name: devices; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE devices (
    id integer DEFAULT nextval('devices_id_seq'::regclass) NOT NULL,
    name character varying NOT NULL
);


ALTER TABLE devices OWNER TO postgres;

--
-- Name: parameters_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE parameters_id_seq
    START WITH 11
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE parameters_id_seq OWNER TO postgres;

--
-- Name: parameters; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE parameters (
    id integer DEFAULT nextval('parameters_id_seq'::regclass) NOT NULL,
    name character varying NOT NULL
);


ALTER TABLE parameters OWNER TO postgres;

--
-- Name: Devices; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Devices" AS
 SELECT devices.id AS device_id,
    devices.name AS device_name,
    parameters.id AS para_id,
    parameters.name AS para_name
   FROM ((devices
     LEFT JOIN device_parameters ON ((devices.id = device_parameters.id_device)))
     LEFT JOIN parameters ON ((parameters.id = device_parameters.id_parameter)));


ALTER TABLE "Devices" OWNER TO postgres;

--
-- Name: Errors_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "Errors_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "Errors_id_seq" OWNER TO postgres;

--
-- Name: errors_txt; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE errors_txt (
    id integer DEFAULT nextval('"Errors_id_seq"'::regclass) NOT NULL,
    error_code integer NOT NULL,
    event_type integer,
    event_category integer,
    text text NOT NULL,
    id_language integer NOT NULL
);


ALTER TABLE errors_txt OWNER TO postgres;

--
-- Name: languages_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE languages_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE languages_id_seq OWNER TO postgres;

--
-- Name: languages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE languages (
    id integer DEFAULT nextval('languages_id_seq'::regclass) NOT NULL,
    name character varying(30) NOT NULL,
    value integer NOT NULL
);


ALTER TABLE languages OWNER TO postgres;

--
-- Name: Errors_Txt; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Errors_Txt" AS
 SELECT errors_txt.id,
    errors_txt.error_code,
    errors_txt.event_type,
    errors_txt.event_category,
    errors_txt.text,
    languages.id AS language_id,
    languages.name AS language_name,
    languages.value AS language_value
   FROM (errors_txt
     LEFT JOIN languages ON ((errors_txt.id_language = languages.id)));


ALTER TABLE "Errors_Txt" OWNER TO postgres;

--
-- Name: events_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE events_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE events_id_seq OWNER TO postgres;

--
-- Name: events_txt; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE events_txt (
    id integer DEFAULT nextval('events_id_seq'::regclass) NOT NULL,
    code integer NOT NULL,
    text text NOT NULL,
    id_language integer NOT NULL
);


ALTER TABLE events_txt OWNER TO postgres;

--
-- Name: Events_Txt; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Events_Txt" AS
 SELECT events_txt.id,
    events_txt.code,
    events_txt.text,
    languages.id AS language_id,
    languages.name AS language_name,
    languages.value AS language_value
   FROM (events_txt
     LEFT JOIN languages ON ((events_txt.id_language = languages.id)));


ALTER TABLE "Events_Txt" OWNER TO postgres;

--
-- Name: gas_types_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE gas_types_id_seq
    START WITH 10
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gas_types_id_seq OWNER TO postgres;

--
-- Name: gas_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE gas_types (
    id integer DEFAULT nextval('gas_types_id_seq'::regclass) NOT NULL,
    name character varying NOT NULL,
    description text,
    factor real NOT NULL
);


ALTER TABLE gas_types OWNER TO postgres;

--
-- Name: GasTypes; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "GasTypes" AS
 SELECT gas_types.id,
    gas_types.name,
    gas_types.description,
    gas_types.factor
   FROM gas_types;


ALTER TABLE "GasTypes" OWNER TO postgres;

--
-- Name: program_configuration_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE program_configuration_id_seq
    START WITH 21
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE program_configuration_id_seq OWNER TO postgres;

--
-- Name: program_configuration; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE program_configuration (
    id integer DEFAULT nextval('program_configuration_id_seq'::regclass) NOT NULL,
    parameter_name character varying(30) NOT NULL,
    data text NOT NULL
);


ALTER TABLE program_configuration OWNER TO postgres;

--
-- Name: ProgramParameters; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "ProgramParameters" AS
 SELECT program_configuration.id,
    program_configuration.parameter_name AS name,
    program_configuration.data AS parameter
   FROM program_configuration;


ALTER TABLE "ProgramParameters" OWNER TO postgres;

--
-- Name: connections_program_subprograms; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE connections_program_subprograms (
    id_program integer NOT NULL,
    id_subprogram integer NOT NULL
);


ALTER TABLE connections_program_subprograms OWNER TO postgres;

--
-- Name: programs_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE programs_id_seq
    START WITH 4
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE programs_id_seq OWNER TO postgres;

--
-- Name: programs; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE programs (
    id integer DEFAULT nextval('programs_id_seq'::regclass) NOT NULL,
    name character varying(30) NOT NULL,
    description text
);


ALTER TABLE programs OWNER TO postgres;

--
-- Name: subprograms_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE subprograms_id_seq
    START WITH 19
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE subprograms_id_seq OWNER TO postgres;

--
-- Name: subprograms; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE subprograms (
    id integer DEFAULT nextval('subprograms_id_seq'::regclass) NOT NULL,
    name character varying(30) NOT NULL,
    description text,
    id_pump integer,
    id_vent integer,
    id_purge integer,
    id_power_supplay integer,
    id_gas integer,
    pump_active boolean,
    gas_active boolean,
    power_supply_active boolean,
    purge_active boolean,
    vent_active boolean
);


ALTER TABLE subprograms OWNER TO postgres;

--
-- Name: Programs; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Programs" AS
 SELECT programs.id AS program_id,
    programs.name AS program_name,
    programs.description AS program_description,
    subprograms.id AS subprogram_id,
    subprograms.name AS subprogram_name,
    subprograms.description AS subprogram_description,
    subprograms.pump_active,
    subprograms.power_supply_active,
    subprograms.gas_active,
    subprograms.purge_active,
    subprograms.vent_active
   FROM ((programs
     LEFT JOIN connections_program_subprograms ON ((connections_program_subprograms.id_program = programs.id)))
     LEFT JOIN subprograms ON ((connections_program_subprograms.id_subprogram = subprograms.id)));


ALTER TABLE "Programs" OWNER TO postgres;

--
-- Name: sesions_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE sesions_id_seq
    START WITH 10
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE sesions_id_seq OWNER TO postgres;

--
-- Name: sesions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE sesions (
    id integer DEFAULT nextval('sesions_id_seq'::regclass) NOT NULL,
    date_start timestamp without time zone NOT NULL,
    date_end timestamp without time zone NOT NULL,
    process_name character varying(30)
);


ALTER TABLE sesions OWNER TO postgres;

--
-- Name: Sesions; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Sesions" AS
 SELECT sesions.id,
    sesions.date_start,
    sesions.date_end,
    sesions.process_name
   FROM sesions;


ALTER TABLE "Sesions" OWNER TO postgres;

--
-- Name: stages_gas_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE stages_gas_id_seq
    START WITH 17
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE stages_gas_id_seq OWNER TO postgres;

--
-- Name: stages_gas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE stages_gas (
    id integer DEFAULT nextval('stages_gas_id_seq'::regclass) NOT NULL,
    mfc1_flow real,
    mfc1_min_flow real,
    mfc1_max_flow real,
    mfc2_flow real,
    mfc2_min_flow real,
    mfc2_max_flow real,
    mfc3_flow real,
    mfc3_min_flow real,
    mfc3_max_flow real,
    vaporaiser_cycle_time real,
    vaporaiser_on_time real,
    setpoint_pressure real,
    max_devation_up real,
    max_devation_down real,
    mfc1_max_devation real,
    mfc1_gas_share real,
    mfc2_max_devation real,
    mfc2_gas_share real,
    mfc3_max_devation real,
    mfc3_gas_share real,
    time_duration time without time zone,
    mode_gas integer,
    mfc1_active boolean,
    mfc2_active boolean,
    mfc3_active boolean,
    vaporaiser_active boolean,
    mfc1_id_gas_type integer,
    mfc2_id_gas_type integer,
    mfc3_id_gas_type integer,
    vaporaiser_dosing integer
);


ALTER TABLE stages_gas OWNER TO postgres;

--
-- Name: stages_power_supply_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE stages_power_supply_id_seq
    START WITH 17
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE stages_power_supply_id_seq OWNER TO postgres;

--
-- Name: stages_power_supply; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE stages_power_supply (
    id integer DEFAULT nextval('stages_power_supply_id_seq'::regclass) NOT NULL,
    setpoint real,
    mode integer,
    duration_time time without time zone,
    max_devation real
);


ALTER TABLE stages_power_supply OWNER TO postgres;

--
-- Name: stages_pump_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE stages_pump_id_seq
    START WITH 20
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE stages_pump_id_seq OWNER TO postgres;

--
-- Name: stages_pump; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE stages_pump (
    id integer DEFAULT nextval('stages_pump_id_seq'::regclass) NOT NULL,
    max_time_pump time without time zone,
    setpoint_pressure real
);


ALTER TABLE stages_pump OWNER TO postgres;

--
-- Name: stages_purge_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE stages_purge_id_seq
    START WITH 18
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE stages_purge_id_seq OWNER TO postgres;

--
-- Name: stages_purge; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE stages_purge (
    id integer DEFAULT nextval('stages_purge_id_seq'::regclass) NOT NULL,
    "time" time without time zone
);


ALTER TABLE stages_purge OWNER TO postgres;

--
-- Name: stages_vent_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE stages_vent_id_seq
    START WITH 19
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE stages_vent_id_seq OWNER TO postgres;

--
-- Name: stages_vent; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE stages_vent (
    id integer DEFAULT nextval('stages_vent_id_seq'::regclass) NOT NULL,
    vent_time time without time zone
);


ALTER TABLE stages_vent OWNER TO postgres;

--
-- Name: Subprograms; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Subprograms" AS
 SELECT subprograms.id AS subprogram_id,
    subprograms.name AS subprogram_name,
    subprograms.description AS subprogram_description,
    stages_vent.vent_time,
    stages_purge."time" AS purge_time,
    stages_pump.max_time_pump,
    stages_pump.setpoint_pressure AS setpoint_pump_pressure,
    stages_power_supply.setpoint AS power_supply_setpoint,
    stages_power_supply.mode AS power_supply_mode,
    stages_power_supply.duration_time AS power_supply_time_process,
    stages_power_supply.max_devation AS power_supply_max_devation,
    stages_gas.mfc1_flow,
    stages_gas.mfc2_flow,
    stages_gas.mfc3_flow,
    stages_gas.mfc1_min_flow,
    stages_gas.mfc2_min_flow,
    stages_gas.mfc3_min_flow,
    stages_gas.mfc1_max_flow,
    stages_gas.mfc2_max_flow,
    stages_gas.mfc3_max_flow,
    stages_gas.mfc1_gas_share,
    stages_gas.mfc2_gas_share,
    stages_gas.mfc3_gas_share,
    stages_gas.mfc1_max_devation,
    stages_gas.mfc2_max_devation,
    stages_gas.mfc3_max_devation,
    stages_gas.vaporaiser_cycle_time,
    stages_gas.vaporaiser_on_time,
    stages_gas.setpoint_pressure AS gas_setpoint_pressure,
    stages_gas.max_devation_up AS gas_max_dev_up,
    stages_gas.max_devation_down AS gas_max_dev_down,
    stages_gas.time_duration AS gas_process_duration,
    stages_gas.mode_gas,
    stages_gas.mfc1_active,
    stages_gas.mfc2_active,
    stages_gas.mfc3_active,
    stages_gas.vaporaiser_active,
    stages_gas.mfc1_id_gas_type,
    stages_gas.mfc2_id_gas_type,
    stages_gas.mfc3_id_gas_type,
    stages_gas.vaporaiser_dosing,
    subprograms.pump_active,
    subprograms.power_supply_active,
    subprograms.gas_active,
    subprograms.purge_active,
    subprograms.vent_active
   FROM (((((subprograms
     LEFT JOIN stages_vent ON ((stages_vent.id = subprograms.id_vent)))
     LEFT JOIN stages_purge ON ((stages_purge.id = subprograms.id_purge)))
     LEFT JOIN stages_pump ON ((stages_pump.id = subprograms.id_pump)))
     LEFT JOIN stages_power_supply ON ((stages_power_supply.id = subprograms.id_power_supplay)))
     LEFT JOIN stages_gas ON ((stages_gas.id = subprograms.id_gas)));


ALTER TABLE "Subprograms" OWNER TO postgres;

--
-- Name: priviliges_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE priviliges_id_seq
    START WITH 3
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE priviliges_id_seq OWNER TO postgres;

--
-- Name: types_block_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE types_block_id_seq
    START WITH 10
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE types_block_id_seq OWNER TO postgres;

--
-- Name: types_block_account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE types_block_account (
    id integer DEFAULT nextval('types_block_id_seq'::regclass) NOT NULL,
    name text NOT NULL,
    value integer NOT NULL
);


ALTER TABLE types_block_account OWNER TO postgres;

--
-- Name: types_privilige; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE types_privilige (
    id integer DEFAULT nextval('priviliges_id_seq'::regclass) NOT NULL,
    name character varying(30),
    value integer
);


ALTER TABLE types_privilige OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE users_id_seq
    START WITH 75
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE users_id_seq OWNER TO postgres;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE users (
    id integer DEFAULT nextval('users_id_seq'::regclass) NOT NULL,
    name character varying(30),
    surname character varying(30),
    login character varying(30) NOT NULL,
    password character varying(30),
    allow_change_psw boolean,
    id_type_block_account integer,
    id_privilige integer,
    start_date_block_account date,
    end_date_block_account date
);


ALTER TABLE users OWNER TO postgres;

--
-- Name: COLUMN users.id; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON COLUMN users.id IS '';


--
-- Name: Users; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW "Users" AS
 SELECT users.id,
    users.name,
    users.surname,
    users.login,
    users.password,
    users.allow_change_psw,
    types_block_account.value AS type_block_account,
    types_privilige.value AS privilige,
    users.start_date_block_account,
    users.end_date_block_account
   FROM ((users
     LEFT JOIN types_privilige ON ((types_privilige.id = users.id_privilige)))
     LEFT JOIN types_block_account ON ((users.id_type_block_account = types_block_account.id)));


ALTER TABLE "Users" OWNER TO postgres;

--
-- Name: curent_sesion_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE curent_sesion_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE curent_sesion_id_seq OWNER TO postgres;

--
-- Name: current_sesion; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE current_sesion (
    id integer DEFAULT nextval('curent_sesion_id_seq'::regclass) NOT NULL,
    current_sesion_id integer NOT NULL
);


ALTER TABLE current_sesion OWNER TO postgres;

--
-- Name: data_id_seq1; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE data_id_seq1
    START WITH 41
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE data_id_seq1 OWNER TO postgres;

--
-- Name: data; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE data (
    id integer DEFAULT nextval('data_id_seq1'::regclass) NOT NULL,
    id_parameter integer NOT NULL,
    value real NOT NULL,
    unit character varying(30) NOT NULL,
    date timestamp without time zone NOT NULL
);


ALTER TABLE data OWNER TO postgres;

--
-- Name: Errors_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('"Errors_id_seq"', 1, false);


--
-- Data for Name: acquisition_configurations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY acquisition_configurations (id, id_parameter, frequency, difference_value, enabled_acq, mode_acq) FROM stdin;
7	11	1.10000002	2.0999999	f	1
8	12	2	2.20000005	t	2
9	13	3	3.0999999	f	3
10	14	4	4.0999999	t	1
11	15	5	5.0999999	f	2
12	16	6	6.0999999	t	3
13	17	7	7.0999999	f	1
\.


--
-- Name: acquisition_configurations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('acquisition_configurations_id_seq', 13, true);


--
-- Data for Name: connections_program_subprograms; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY connections_program_subprograms (id_program, id_subprogram) FROM stdin;
4	19
4	23
4	24
4	25
6	28
7	29
8	33
9	34
\.


--
-- Name: curent_sesion_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('curent_sesion_id_seq', 1, true);


--
-- Data for Name: current_sesion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY current_sesion (id, current_sesion_id) FROM stdin;
1	1028
\.


--
-- Data for Name: data; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY data (id, id_parameter, value, unit, date) FROM stdin;
41	11	497	sccm	2016-12-05 08:20:10.381771
42	12	488	sccm	2016-12-05 08:20:10.381771
43	13	290	sccm	2016-12-05 08:20:10.381771
44	14	75	W	2016-12-05 08:20:10.381771
45	15	748	V	2016-12-05 08:20:10.381771
46	16	44	A	2016-12-05 08:20:10.381771
47	17	0	mBar	2016-12-05 08:20:10.381771
48	14	155	W	2016-12-05 10:45:24.03836
49	14	346	W	2016-12-05 10:45:31.989688
50	14	462	W	2016-12-05 10:45:32.551286
51	14	483	W	2016-12-05 10:45:33.103212
52	14	126	W	2016-12-05 10:45:33.651575
53	14	340	W	2016-12-05 10:45:34.181629
54	14	244	W	2016-12-05 10:45:34.746434
55	14	346	W	2016-12-05 10:45:35.29594
56	14	208	W	2016-12-05 10:45:35.828613
57	14	328	W	2016-12-05 10:45:36.386105
58	14	474	W	2016-12-05 10:45:36.953199
59	14	145	W	2016-12-05 10:45:37.488444
60	14	87	W	2016-12-05 10:45:38.026523
61	14	408	W	2016-12-05 10:45:38.593156
62	14	9	W	2016-12-05 10:45:39.149171
63	14	37	W	2016-12-05 10:45:39.684527
64	14	44	W	2016-12-05 10:45:40.239395
65	14	14	W	2016-12-05 10:45:40.770331
66	14	30	W	2016-12-05 10:45:41.311441
67	14	488	W	2016-12-05 10:45:42.39776
68	14	76	W	2016-12-05 10:45:42.951028
69	14	472	W	2016-12-05 10:45:43.518146
70	14	278	W	2016-12-05 10:45:44.068998
71	14	473	W	2016-12-05 10:45:44.598487
72	14	381	W	2016-12-05 10:45:45.138683
73	14	29	W	2016-12-05 10:45:45.675107
74	14	78	W	2016-12-05 10:45:46.245926
75	14	259	W	2016-12-05 10:45:46.801896
76	14	170	W	2016-12-05 10:45:47.374052
77	14	245	W	2016-12-05 10:45:47.93784
78	14	493	W	2016-12-05 10:45:48.47828
79	14	34	W	2016-12-05 10:45:49.030786
80	14	80	W	2016-12-05 10:45:49.577801
81	14	386	W	2016-12-05 10:45:50.140563
82	14	245	W	2016-12-05 10:45:50.702075
83	14	480	W	2016-12-05 10:45:51.25724
84	14	39	W	2016-12-05 10:45:51.798765
85	14	488	W	2016-12-05 10:45:52.358551
86	14	404	W	2016-12-05 10:45:52.917111
87	14	475	W	2016-12-05 10:45:53.436191
88	14	43	W	2016-12-05 10:45:54.020204
89	14	347	W	2016-12-05 10:45:54.558348
90	14	261	W	2016-12-05 10:45:55.089314
91	14	381	W	2016-12-05 10:45:55.613551
92	14	192	W	2016-12-05 10:45:56.173078
93	14	31	W	2016-12-05 10:45:56.771484
94	14	432	W	2016-12-05 10:45:57.326537
95	14	51	W	2016-12-05 10:45:57.857099
96	14	21	W	2016-12-05 10:45:58.413663
97	14	370	W	2016-12-05 10:45:58.943067
98	14	490	W	2016-12-05 10:45:59.497442
99	14	475	W	2016-12-05 10:46:00.083893
100	14	20	W	2016-12-05 10:46:00.645175
101	14	423	W	2016-12-05 10:46:01.197028
102	14	301	W	2016-12-05 10:46:01.781795
103	14	158	W	2016-12-05 10:46:02.32881
104	14	36	W	2016-12-05 10:46:02.88539
105	14	359	W	2016-12-05 10:46:03.414034
106	14	440	W	2016-12-05 10:46:03.948066
107	14	257	W	2016-12-05 10:46:04.511748
108	14	153	W	2016-12-05 10:46:05.055774
109	14	226	W	2016-12-05 10:46:05.620032
110	14	190	W	2016-12-05 10:46:06.149552
111	14	194	W	2016-12-05 10:46:07.240122
112	14	56	W	2016-12-05 10:46:07.825191
113	14	335	W	2016-12-05 10:46:08.382363
114	14	464	W	2016-12-05 10:46:08.932229
115	14	368	W	2016-12-05 10:46:09.455031
116	14	48	W	2016-12-05 10:46:09.984382
117	14	351	W	2016-12-05 10:46:10.501183
118	14	395	W	2016-12-05 10:46:11.024177
119	14	42	W	2016-12-05 10:46:11.578447
120	14	232	W	2016-12-05 10:46:12.137795
121	14	99	W	2016-12-05 10:46:12.701225
122	14	30	W	2016-12-05 10:46:13.257425
123	14	263	W	2016-12-05 10:46:13.834211
124	14	226	W	2016-12-05 10:46:14.363433
125	14	325	W	2016-12-05 10:46:14.9245
126	14	81	W	2016-12-05 10:46:15.479598
127	14	365	W	2016-12-05 10:46:16.016364
128	14	389	W	2016-12-05 10:46:16.551582
129	14	391	W	2016-12-05 10:46:17.079267
130	14	175	W	2016-12-05 10:46:17.628868
131	14	110	W	2016-12-05 10:46:18.205587
132	14	149	W	2016-12-05 10:46:18.734899
133	14	376	W	2016-12-05 10:46:19.298177
134	14	200	W	2016-12-05 10:46:19.859307
135	14	113	W	2016-12-05 10:46:20.410313
136	14	246	W	2016-12-05 10:46:20.955675
137	14	155	W	2016-12-05 10:46:21.488661
138	14	112	W	2016-12-05 10:46:22.036939
139	14	252	W	2016-12-05 10:46:22.610974
140	14	470	W	2016-12-05 10:46:23.14467
141	14	246	W	2016-12-05 10:46:23.676851
142	14	232	W	2016-12-05 10:46:24.22567
143	14	93	W	2016-12-05 10:46:24.799988
144	14	421	W	2016-12-05 10:46:25.367266
145	14	115	W	2016-12-05 10:46:25.917979
146	14	441	W	2016-12-05 10:46:26.474327
147	14	248	W	2016-12-05 10:46:27.0326
148	14	272	W	2016-12-05 10:46:27.586841
149	14	296	W	2016-12-05 10:46:28.150637
150	14	419	W	2016-12-05 10:46:28.716722
151	14	377	W	2016-12-05 10:46:29.242918
152	14	412	W	2016-12-05 10:46:29.772338
153	12	475	sccm	2016-12-05 11:52:27.502926
154	12	161	sccm	2016-12-05 12:40:03.218713
155	14	429	W	2016-12-05 12:40:03.219728
156	16	68	A	2016-12-05 12:40:03.219728
157	12	43	sccm	2016-12-05 12:40:31.694274
158	14	268	W	2016-12-05 12:40:31.694274
159	12	99	sccm	2016-12-05 12:40:32.247896
160	12	297	sccm	2016-12-05 12:40:32.800965
161	12	342	sccm	2016-12-05 12:40:33.343944
162	16	28	A	2016-12-05 12:40:33.343944
163	12	278	sccm	2016-12-05 12:41:04.970498
164	14	92	W	2016-12-05 12:41:04.970498
165	16	36	A	2016-12-05 12:41:04.970498
166	12	61	sccm	2016-12-05 12:41:35.555487
167	12	17	sccm	2016-12-05 12:42:24.875743
168	14	62	W	2016-12-05 12:42:24.875743
169	16	88	A	2016-12-05 12:42:24.875743
170	12	409	sccm	2016-12-05 12:43:16.58823
171	14	174	W	2016-12-05 12:43:16.58823
172	16	57	A	2016-12-05 12:43:16.58823
173	12	10	sccm	2016-12-05 12:43:59.638606
174	14	470	W	2016-12-05 12:43:59.638606
175	16	19	A	2016-12-05 12:43:59.638606
176	12	371	sccm	2016-12-05 12:44:21.581839
177	14	361	W	2016-12-05 12:44:21.581839
178	12	431	sccm	2016-12-05 12:44:22.450463
179	12	254	sccm	2016-12-05 12:44:23.522049
180	12	71	sccm	2016-12-05 12:44:24.592345
181	16	9	A	2016-12-05 12:44:24.592345
182	12	74	sccm	2016-12-05 12:44:32.932756
183	14	165	W	2016-12-05 12:44:32.932756
184	16	12	A	2016-12-05 12:44:32.932756
185	12	211	sccm	2016-12-05 12:45:19.141476
186	14	479	W	2016-12-05 12:45:19.141476
187	16	5	A	2016-12-05 12:45:19.141476
188	12	322	sccm	2016-12-05 12:46:14.32166
189	14	299	W	2016-12-05 12:46:14.32166
190	16	22	A	2016-12-05 12:46:14.32166
191	12	435	sccm	2016-12-05 12:47:08.601022
192	16	84	A	2016-12-05 12:47:08.601022
193	12	45	sccm	2016-12-05 12:47:37.851542
194	16	15	A	2016-12-05 12:47:37.851542
195	12	163	sccm	2016-12-05 12:47:38.997374
196	12	323	sccm	2016-12-05 12:48:18.911635
197	14	70	W	2016-12-05 12:48:18.911635
198	16	92	A	2016-12-05 12:48:18.911635
199	12	334	sccm	2016-12-05 12:48:50.131487
200	14	202	W	2016-12-05 12:48:50.131487
201	12	24	sccm	2016-12-05 12:49:20.414343
202	12	323	sccm	2016-12-05 12:50:28.506585
203	14	37	W	2016-12-05 12:50:28.507585
204	16	73	A	2016-12-05 12:50:28.507585
205	12	251	sccm	2016-12-05 12:50:59.181579
206	12	281	sccm	2016-12-05 12:51:05.20879
207	12	466	sccm	2016-12-05 12:57:49.356572
208	14	340	W	2016-12-05 12:57:49.356572
209	12	352	sccm	2016-12-05 12:58:19.140635
210	12	380	sccm	2016-12-05 12:58:19.67467
211	12	396	sccm	2016-12-05 12:59:22.136266
212	14	369	W	2016-12-05 12:59:22.136266
213	16	24	A	2016-12-05 12:59:22.136266
214	12	171	sccm	2016-12-05 13:01:09.85588
215	14	255	W	2016-12-05 13:01:09.85588
216	16	88	A	2016-12-05 13:01:09.85588
217	12	49	sccm	2016-12-05 13:01:39.440253
218	14	173	W	2016-12-05 13:01:39.440253
219	16	48	A	2016-12-05 13:01:39.440253
220	12	488	sccm	2016-12-05 13:01:52.917668
221	14	16	W	2016-12-05 13:01:52.917668
222	16	24	A	2016-12-05 13:01:52.917668
223	12	280	sccm	2016-12-05 13:02:17.248095
224	12	171	sccm	2016-12-05 13:02:17.903071
225	14	96	W	2016-12-05 13:02:17.903071
226	12	136	sccm	2016-12-05 13:02:18.449019
227	12	421	sccm	2016-12-05 13:02:19.104849
228	12	322	sccm	2016-12-05 13:02:19.759151
229	16	87	A	2016-12-05 13:02:19.759151
230	12	151	sccm	2016-12-05 13:02:20.334948
231	12	449	sccm	2016-12-05 13:02:20.851504
232	12	385	sccm	2016-12-05 13:02:21.431866
233	12	111	sccm	2016-12-05 13:02:21.964933
234	14	465	W	2016-12-05 13:02:21.964933
235	12	462	sccm	2016-12-05 13:02:22.50839
236	12	240	sccm	2016-12-05 13:02:23.057092
237	12	378	sccm	2016-12-05 13:02:55.87998
238	12	250	sccm	2016-12-05 13:35:50.892354
239	12	422	sccm	2016-12-05 13:36:22.26926
240	14	328	W	2016-12-05 13:36:22.26926
241	16	57	A	2016-12-05 13:36:22.26926
242	12	325	sccm	2016-12-05 13:37:33.399402
243	14	286	W	2016-12-05 13:37:33.399402
244	12	181	sccm	2016-12-05 13:38:02.915264
245	12	360	sccm	2016-12-05 13:38:03.478108
246	12	87	sccm	2016-12-05 13:39:04.878127
247	12	115	sccm	2016-12-05 13:39:34.38194
248	12	485	sccm	2016-12-05 13:39:34.949259
249	16	31	A	2016-12-05 13:39:34.949259
250	12	259	sccm	2016-12-05 13:50:30.191123
251	14	202	W	2016-12-05 13:50:30.191123
252	16	70	A	2016-12-05 13:50:30.191123
253	12	344	sccm	2016-12-05 13:51:01.481318
254	12	282	sccm	2016-12-05 13:51:30.592602
255	14	482	W	2016-12-05 13:51:30.592602
256	12	110	sccm	2016-12-05 13:51:31.120919
257	12	192	sccm	2016-12-05 13:51:31.667963
258	12	322	sccm	2016-12-05 13:51:59.535318
259	14	363	W	2016-12-05 13:51:59.535318
260	12	391	sccm	2016-12-05 13:52:00.073354
261	12	142	sccm	2016-12-05 13:52:00.656265
262	16	55	A	2016-12-05 13:52:00.656766
263	12	388	sccm	2016-12-05 13:52:01.172672
264	12	327	sccm	2016-12-05 13:52:01.719638
265	12	128	sccm	2016-12-05 13:53:11.659877
266	14	105	W	2016-12-05 13:53:11.659877
267	12	319	sccm	2016-12-05 13:53:42.289152
268	14	140	W	2016-12-05 13:53:42.289152
269	12	445	sccm	2016-12-05 13:54:12.897251
270	16	99	A	2016-12-05 13:54:12.897251
271	12	305	sccm	2016-12-05 13:54:44.638149
272	14	232	W	2016-12-05 13:54:44.638149
273	16	40	A	2016-12-05 13:54:44.638149
274	12	170	sccm	2016-12-05 13:55:15.255628
275	12	96	sccm	2016-12-05 13:55:45.360355
276	14	421	W	2016-12-05 13:55:45.360355
277	12	486	sccm	2016-12-05 13:56:16.007721
278	12	150	sccm	2016-12-05 13:56:46.588722
279	12	219	sccm	2016-12-05 13:57:16.460233
280	14	373	W	2016-12-05 13:57:16.460233
281	12	205	sccm	2016-12-05 13:57:17.00405
282	12	396	sccm	2016-12-05 13:57:42.064099
283	12	119	sccm	2016-12-05 13:57:42.637799
284	14	15	W	2016-12-05 13:57:42.637799
285	12	468	sccm	2016-12-05 13:57:43.279926
286	12	469	sccm	2016-12-05 13:57:43.831648
287	12	427	sccm	2016-12-05 13:57:44.358294
288	12	463	sccm	2016-12-05 13:57:44.899038
289	12	195	sccm	2016-12-05 13:57:45.455614
290	16	89	A	2016-12-05 13:57:45.455614
291	12	291	sccm	2016-12-05 13:57:46.100844
292	12	320	sccm	2016-12-05 13:57:46.765462
293	14	61	W	2016-12-05 13:57:46.765462
294	12	117	sccm	2016-12-05 13:57:47.304558
295	12	284	sccm	2016-12-05 13:58:18.39446
296	12	347	sccm	2016-12-05 13:59:03.670044
297	14	295	W	2016-12-05 13:59:03.670044
298	12	113	sccm	2016-12-05 14:00:03.79065
299	14	92	W	2016-12-05 14:00:03.79065
300	16	37	A	2016-12-05 14:00:03.79065
301	12	127	sccm	2016-12-05 14:00:04.371457
302	12	300	sccm	2016-12-05 14:00:35.369719
303	14	263	W	2016-12-05 14:00:35.369719
304	12	225	sccm	2016-12-05 14:01:28.207287
305	14	449	W	2016-12-05 14:01:28.207287
306	16	87	A	2016-12-05 14:01:28.207287
307	12	152	sccm	2016-12-05 14:30:04.12257
308	16	48	A	2016-12-05 14:30:04.12257
309	12	465	sccm	2016-12-05 14:30:04.693182
310	12	421	sccm	2016-12-05 14:30:51.798317
311	14	386	W	2016-12-05 14:30:51.798317
312	16	7	A	2016-12-05 14:30:51.798317
313	12	120	sccm	2016-12-05 14:31:23.147435
314	14	482	W	2016-12-05 14:32:19.45819
315	16	44	A	2016-12-05 14:32:19.45819
316	12	435	sccm	2016-12-05 14:32:52.11652
317	14	42	W	2016-12-05 14:32:52.11652
318	16	97	A	2016-12-05 14:32:52.11652
319	12	136	sccm	2016-12-05 14:33:22.536223
320	14	175	W	2016-12-05 14:33:22.536223
321	12	228	sccm	2016-12-05 14:33:53.802689
322	14	301	W	2016-12-05 14:33:53.802689
323	16	75	A	2016-12-05 14:33:53.802689
324	12	268	sccm	2016-12-05 14:34:23.337445
325	14	291	W	2016-12-05 14:34:23.337445
326	12	43	sccm	2016-12-05 14:34:23.92848
327	12	60	sccm	2016-12-05 14:34:52.455223
328	14	428	W	2016-12-05 14:34:52.455223
329	12	157	sccm	2016-12-05 14:34:52.979896
330	12	426	sccm	2016-12-05 14:34:53.539659
331	16	93	A	2016-12-05 14:34:53.539659
332	12	132	sccm	2016-12-05 14:34:54.08575
333	12	69	sccm	2016-12-05 14:35:24.410645
334	12	242	sccm	2016-12-05 14:35:56.761907
335	14	229	W	2016-12-05 14:35:56.761907
336	12	389	sccm	2016-12-05 14:36:27.381084
337	16	38	A	2016-12-05 14:36:27.381084
338	12	88	sccm	2016-12-05 14:36:57.453183
339	12	333	sccm	2016-12-05 14:37:28.769627
340	12	209	sccm	2016-12-05 14:38:00.271065
341	16	99	A	2016-12-05 14:38:00.271065
342	12	264	sccm	2016-12-05 14:38:30.896255
343	12	184	sccm	2016-12-05 14:39:19.949964
344	14	172	W	2016-12-05 14:39:19.949964
345	16	79	A	2016-12-05 14:39:19.949964
346	12	140	sccm	2016-12-05 14:40:58.987635
347	14	439	W	2016-12-05 14:40:58.987635
348	16	98	A	2016-12-05 14:40:58.987635
349	12	245	sccm	2016-12-05 14:41:31.682235
350	14	159	W	2016-12-05 14:41:31.682235
351	16	11	A	2016-12-05 14:41:31.682235
352	12	469	sccm	2016-12-05 14:42:01.799109
353	16	74	A	2016-12-05 14:42:01.799109
354	12	264	sccm	2016-12-05 14:42:32.508536
355	12	279	sccm	2016-12-05 14:43:01.524477
356	12	401	sccm	2016-12-05 14:43:02.067598
357	12	233	sccm	2016-12-05 14:43:02.585044
358	12	405	sccm	2016-12-05 14:43:32.298755
359	12	415	sccm	2016-12-05 14:43:32.814129
360	14	312	W	2016-12-05 14:43:32.814129
361	12	242	sccm	2016-12-05 14:44:01.775161
362	12	76	sccm	2016-12-05 14:44:02.317516
363	14	124	W	2016-12-05 14:44:02.317516
364	12	397	sccm	2016-12-05 14:44:02.897362
365	12	29	sccm	2016-12-05 14:44:27.5141
366	12	462	sccm	2016-12-05 14:44:28.40502
367	12	296	sccm	2016-12-05 14:44:29.278073
368	12	18	sccm	2016-12-05 14:44:30.164615
369	14	40	W	2016-12-05 14:44:30.164615
370	12	34	sccm	2016-12-05 14:44:30.942981
371	12	393	sccm	2016-12-05 14:44:31.864969
372	16	92	A	2016-12-05 14:44:31.864969
373	12	1	sccm	2016-12-05 14:44:32.758019
374	12	136	sccm	2016-12-05 14:44:33.533054
375	12	270	sccm	2016-12-05 14:45:05.831514
376	14	136	W	2016-12-05 14:45:05.831514
377	12	93	sccm	2016-12-05 14:45:38.667324
378	14	357	W	2016-12-05 14:45:38.667324
379	16	67	A	2016-12-05 14:45:38.667324
380	12	376	sccm	2016-12-05 14:46:04.99726
381	14	246	W	2016-12-05 14:46:04.99726
382	12	238	sccm	2016-12-05 14:46:05.897811
383	12	413	sccm	2016-12-05 14:46:06.947245
384	12	304	sccm	2016-12-05 14:46:07.854393
385	12	262	sccm	2016-12-05 14:46:08.747427
386	16	78	A	2016-12-05 14:46:08.747427
387	12	283	sccm	2016-12-05 14:46:42.967617
388	14	86	W	2016-12-05 14:46:42.967617
389	12	185	sccm	2016-12-05 14:47:15.063974
390	16	23	A	2016-12-05 14:47:15.063974
391	12	231	sccm	2016-12-05 14:47:46.86955
392	16	7	A	2016-12-05 14:47:46.86955
393	12	272	sccm	2016-12-05 20:22:30.77656
394	12	192	sccm	2016-12-05 20:23:01.605169
395	12	284	sccm	2016-12-05 20:23:31.679384
396	12	120	sccm	2016-12-05 20:24:02.284833
397	12	54	sccm	2016-12-05 20:24:32.399573
398	12	442	sccm	2016-12-05 20:25:02.1632
399	12	194	sccm	2016-12-05 20:25:02.716464
400	12	245	sccm	2016-12-05 20:25:34.238353
401	16	9	A	2016-12-05 20:25:34.238353
402	12	368	sccm	2016-12-05 20:26:05.07162
403	16	46	A	2016-12-05 20:26:05.07162
404	12	175	sccm	2016-12-05 20:26:34.935547
405	12	487	sccm	2016-12-05 20:26:35.453021
406	12	75	sccm	2016-12-05 20:27:06.873462
407	14	323	W	2016-12-05 20:27:06.873462
408	12	46	sccm	2016-12-05 20:27:38.395922
409	14	13	W	2016-12-05 20:27:38.395922
410	12	274	sccm	2016-12-05 20:28:10.686478
411	14	252	W	2016-12-05 20:28:10.686478
412	15	20	V	2016-12-08 06:01:10.686478
413	15	104	V	2016-12-08 06:30:41.537279
542	15	477	V	2016-12-08 09:20:11.367946
544	14	72	W	2016-12-08 10:25:11.926217
545	14	158	W	2016-12-08 10:55:29.438481
546	16	221	A	2016-12-08 11:30:29.439982
547	16	69	A	2016-12-08 11:58:29.439982
543	15	182	V	2016-12-08 09:50:11.926217
538	14	324	W	2016-12-08 07:05:09.704325
539	14	34	W	2016-12-08 07:35:09.704325
540	16	61	A	2016-12-08 08:15:10.263668
541	16	164	A	2016-12-08 08:45:10.812276
\.


--
-- Name: data_id_seq1; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('data_id_seq1', 547, true);


--
-- Data for Name: device_parameters; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY device_parameters (id_device, id_parameter) FROM stdin;
4	11
4	12
4	13
5	14
5	15
5	16
6	17
\.


--
-- Data for Name: devices; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY devices (id, name) FROM stdin;
4	MFC
5	PowerSupply
6	PressureControl
\.


--
-- Name: devices_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('devices_id_seq', 6, true);


--
-- Data for Name: errors_txt; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY errors_txt (id, error_code, event_type, event_category, text, id_language) FROM stdin;
15	16	16	2	Can't set setpoint of chamber pressure.	2
18	19	19	2	Can't set operate mode of power supply.	2
20	21	21	2	Can't set limit current of power supply.	2
34	35	35	1	Can't run program because program wasn't selected.	2
22	23	23	2	Can't set max voltage of power supply.	2
16	18	18	2	Can't set mode of power supply.	2
24	25	25	2	Can't set max current of power supply.	2
46	120	7	3	Program error. Incorect mode work of power supply.	2
49	123	7	3	Program error. Flow for MFC1 can't stabilization beetwen setting range.	2
58	8	7	3	Range flow work for MFC3 is 0	2
23	24	24	2	Can't set max power of power supply.	2
19	20	20	2	Can't set limit power of power supply.	2
25	26	26	2	Can't set wait time on operate of power supply.	2
29	30	30	2	Can't set time pump of section to SV valve.	2
28	29	29	2	Can't set wait time on confiramtion change state of pump.	2
27	28	28	2	Can't change state of pump.	2
26	27	27	2	Can't set wait time on achive of setpoint by power supply.	2
8	11	11	2	Can't set flow.	2
9	8	8	2	Can't set max flow.	2
30	31	31	2	Can't set cycle time of vaporaizer.	2
17	17	17	2	Can't set setpoint of power supply.	2
36	1	7	3	Can't execute of program because marker of end doesn't exist.	2
37	2	7	3	Can't start program because door is open.	2
39	109	7	3	Program error. Limit time for pumping chamber was exceeded.	2
21	22	22	2	Can't set limit voltage of power supply	2
38	3	7	3	Can't execute program beacues main power supply is turn off.	2
41	115	7	3	Program error. Can't make vent process because fore pump still working.	2
40	110	7	3	Program error. Fore pump reports error.	2
42	116	7	3	Program error. Time for confirmation correct state of power supply was exceeded.	2
44	118	7	3	Program error. Power supply report problem with hardware.	2
43	117	7	3	Program error. Setpoint of power supply is out of range.	2
45	119	7	3	Program error. Wait time for stabilization setpoint of power supply was exceeded.	2
48	122	7	3	Program error. Setpoint flow for MFC1 is out of range.	2
47	121	7	3	Program error. Setpoint limit of power supply can't be 0.	2
51	125	7	3	Program error. Flow for MFC2 can't stabilization beetwen setting range.	2
50	124	7	3	Program error. Setpoint flow for MFC2 is out of range.	2
53	127	7	3	Program error. Flow for MFC3 can't stabilization beetwen setting range.	2
52	126	7	3	Program error. Setpoint flow for MFC3 is out of range.	2
4	2	2	1	Call incorect operation.	2
5	6	6	1	In  PLC doesn't exist any program.	2
7	37	37	1	MX Components not installed. Communication with PLC is impossible.	2
6	1	1	1	Pointer of object is null.	2
10	9	9	2	Can't set range of voltage for mass flow controler.	2
14	13	13	2	Can't load program to PLC.	2
13	12	12	2	Can't make update setings of PLC.	2
11	10	10	2	Can't set stabilization time for mass flow controler.	2
32	33	33	2	Can't set  state of valve.	2
31	32	32	2	Can't set on time of vaporazier.	2
33	34	34	2	Can't set mode as pressure for chamber.	2
54	4	7	3	HV Setpoint is out of range.	2
56	6	7	3	Range flow work for MFC1 is 0.	2
55	5	7	3	Range null.	2
57	7	7	3	Range flow work for MFC2 is 0.	2
60	10	7	3	HV Error power supply.	2
59	9	7	3	Hardware error of HV.	2
62	12	7	3	Error of fore pump.	2
61	11	7	3	HV error confirm.	2
35	36	36	2	Can't set operating mode auto/manual of device.	2
2	3	3	1	Wrong ID parameters of MFC channel. Can't return correct data.	2
64	28	28	1	Can't set manully state of fore pump in automatic mode	2
63	19	19	1	Can't set manually operate mode of power supply in automatic mode	2
65	33	33	1	Can't set manullystate of valve in automatic mode	2
1	4	4	1	Can't set Cycle Time of vaporaizer as less than Time ON.	2
3	5	5	1	Can't set Time ON of vaporazier as more than Cycle Time.	2
12	14	14	2	Can't start a program.	2
66	15	15	2	Can't stop program.	2
67	38	38	2	Can't set dosing for vaporazier	2
68	39	39	2	Can't set type of vaporaizer 	2
69	131	7	3	Program error. Pressure can't stabilization beetwen setting range in mode control pressure.	2
70	132	7	3	Program error. Flow for MFC1 can't stabilization beetwen setting range in mode control pressure.	2
71	133	7	3	Program error. Flow for MFC2 can't stabilization beetwen setting range in mode control pressure.	2
72	134	7	3	Program error. Flow for MFC3 can't stabilization beetwen setting range in mode control pressure.	2
73	40	40	2	Can't set activity for coresponding  MFC.	2
74	41	41	2	Can't set pressure stabilization for PID proceess.	2
75	13	7	3	Regulator PID reports error. Please check settings PID	2
76	14	7	3	Unexpected stop pumping. Emergency stop program, turn off HV, cut off gas	2
78	16	7	3	Pressure over limit. Emergency cut off gas	2
77	15	7	3	Pressure over limit. Emergency turn off HV	2
79	17	7	3	Door of chamber isn't closed. Can't turn on HV, pumping chamber and gas control	2
81	19	7	3	Emergency stop machine due to active Vacuum Switch signal	2
80	18	7	3	Unexpected turns off main power supply. Program has been stopped.	2
82	20	7	3	Emergency switch off power suply due to active Cover Protection signal	2
84	22	7	3	Emergency stop machine due to active Emergency Stop button	2
83	21	7	3	Can't start process due to active Emergency Stop button	2
85	23	7	3	Can't start leak test process because process is still running	2
86	24	7	3	Can't start process because leak test is still running	2
87	25	7	3	Leak test error - fore pump reports problem	2
88	42	42	2	Can't set PID parameters	2
89	43	43	2	Can't set date time of PLC	2
90	44	44	2	Can't start leak test	2
91	45	45	2	Can't clear whole work time of pump	2
92	46	46	2	Can't clear whole work time of machine	2
93	47	47	2	Can't set leak test parameters	2
94	48	48	2	Can't stop leak test	2
\.


--
-- Name: events_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('events_id_seq', 1, false);


--
-- Data for Name: events_txt; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY events_txt (id, code, text, id_language) FROM stdin;
25	36	Operating mode has been set successfully.	2
24	34	Mode pressure for device has been changed successfully.	2
23	33	State of valve has been changed successfully.	2
21	31	Cycle Time of vaporaizer has been set successfully.	2
20	30	Time pump of section to SV has been set successfully.	2
22	32	Time On of vaporaizer has been set successfully.	2
19	29	Wait time for confirmation change state of fore pump has been set successfully.	2
18	28	State of fore pump has been changed successfully.	2
15	25	Max current for power supply has been set successfully.	2
13	23	Max voltage for power supply has been set successfully.	2
12	22	Voltage limit for power supply has been set successfully.	2
10	20	Power limit for power supply has been set successfully.	2
9	19	Operate state for power supply has been set successfully.	2
8	18	Working mode for power supply has been set successfully.	2
7	17	Setpoint for power supply has been set successfully.	2
6	16	Pressure setpoint has been set successfully.	2
4	11	Flow for MFC has been set successfully.	2
3	10	Time flow stability has been set successfully.	2
2	9	Range voltage for MFC has been set successfully.	2
1	8	Max flow for MFC has been set successfully.	2
11	21	Current limit for power supply has been set successfully.	2
16	26	Wait time on confirmation set operate for power supply has been set successfully.	2
17	27	Wait time on stabilization setpoint for power supply has been set successfully.	2
26	38	Dosing value for vaporaizer has been set successfully.	2
27	39	Type of vaporazier valve has been set successfully.	2
28	40	Activity corressponding MFC has been set successfully.	2
29	41	Pressure stabilization time for process PID has been set successfully.	2
30	42	One of PID parameters has been set successfully.	2
14	24	Max power for power supply has been set successfully.	2
5	12	Settings has been read successfully.	2
31	43	Date time of PLC has been set successfully	2
35	47	Leak test parameters has been updated successfully	2
36	48	Leak test has been stopped successfully	2
34	46	Number of process and whole work time of machine has been cleared successfully	2
33	45	Whole work time of pump has been cleared successfully	2
32	44	Leak test has been started successfully	2
\.


--
-- Data for Name: gas_types; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY gas_types (id, name, description, factor) FROM stdin;
10	H	Wodor	1.5
12	Ar	Argon	3.5
\.


--
-- Name: gas_types_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('gas_types_id_seq', 12, true);


--
-- Data for Name: languages; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY languages (id, name, value) FROM stdin;
2	English	1
\.


--
-- Name: languages_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('languages_id_seq', 1, false);


--
-- Data for Name: parameters; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY parameters (id, name) FROM stdin;
11	MFC1 Flow
12	MFC2 Flow
13	MFC3 Flow
14	Power
15	Voltage
16	Curent
17	Pressure
\.


--
-- Name: parameters_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('parameters_id_seq', 17, true);


--
-- Name: priviliges_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('priviliges_id_seq', 3, false);


--
-- Data for Name: program_configuration; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY program_configuration (id, parameter_name, data) FROM stdin;
22	MFC1_Parameter	GasTypeID = 10;
24	MFC2_Parameter	GasTypeID = 12;
25	MFC3_Parameter	GasTypeID = 10;
26	Maintenance_Parameter	DateLastMaintenance = 02.03.2017 13:04:11;DateNextMaintenance = 01.04.2017 13:04:11;IntervalMonth = 10;HourOilChange = 678;TypeTimeMaintenance = Time;ChamberVolume = 34,56;Setpoint = 1,234;TimeDuration = 18429;
23	ParameterAcq	EnabledAcq = False;Pressure = 567,123;DuringProces = False;AllTime = True;
21	Parameter_PLC	Type_PLC = L;Type_Communication = USB;
\.


--
-- Name: program_configuration_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('program_configuration_id_seq', 26, true);


--
-- Data for Name: programs; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY programs (id, name, description) FROM stdin;
4	Program proces	Moj testowy program
6	Program vaporaiser	
7	Program test 	
8	Program Vent	
9	Program Pressure	
\.


--
-- Name: programs_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('programs_id_seq', 9, true);


--
-- Data for Name: sesions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY sesions (id, date_start, date_end, process_name) FROM stdin;
13	2016-12-05 11:52:17.087568	2016-12-05 11:52:17.087568	\N
14	2016-12-05 11:52:21.492391	2016-12-05 11:52:21.492391	\N
15	2016-12-05 11:52:24.219596	2016-12-05 11:52:24.219596	\N
16	2016-12-05 11:52:25.327548	2016-12-05 11:52:25.327548	\N
17	2016-12-05 11:52:27.501908	2016-12-05 11:52:27.502926	\N
10	2016-12-05 08:20:10.381771	2016-12-05 08:20:10.381771	\N
11	2016-12-05 08:22:10.265441	2016-12-05 08:22:10.265441	\N
18	2016-12-05 11:52:29.702931	2016-12-05 11:52:29.702931	\N
19	2016-12-05 11:52:31.349882	2016-12-05 11:52:31.349882	\N
20	2016-12-05 11:52:32.43463	2016-12-05 11:52:32.43463	\N
21	2016-12-05 11:52:34.08478	2016-12-05 11:52:34.08478	\N
22	2016-12-05 11:52:38.443548	2016-12-05 11:52:38.443548	\N
23	2016-12-05 11:52:46.742518	2016-12-05 11:52:46.742518	\N
105	2016-12-05 12:48:29.90942	2016-12-05 12:48:29.90942	Test 1
115	2016-12-05 12:48:50.131487	2016-12-05 12:48:50.131487	\N
24	2016-12-05 12:40:03.211722	2016-12-05 12:40:03.219728	\N
25	2016-12-05 12:40:04.90716	2016-12-05 12:40:04.90716	\N
26	2016-12-05 12:40:07.093531	2016-12-05 12:40:07.093531	\N
27	2016-12-05 12:40:09.829722	2016-12-05 12:40:09.829722	\N
28	2016-12-05 12:40:15.251976	2016-12-05 12:40:15.251976	\N
29	2016-12-05 12:40:16.407689	2016-12-05 12:40:16.407689	\N
30	2016-12-05 12:40:18.035289	2016-12-05 12:40:18.035289	\N
31	2016-12-05 12:40:20.210315	2016-12-05 12:40:20.210315	\N
32	2016-12-05 12:40:22.378059	2016-12-05 12:40:22.378059	\N
33	2016-12-05 12:40:24.04583	2016-12-05 12:40:24.04583	\N
34	2016-12-05 12:40:26.241045	2016-12-05 12:40:26.241045	\N
116	2016-12-05 12:48:51.228727	2016-12-05 12:48:51.228727	\N
117	2016-12-05 12:48:52.86311	2016-12-05 12:48:52.86311	\N
118	2016-12-05 12:48:53.960542	2016-12-05 12:48:53.960542	\N
119	2016-12-05 12:48:55.046603	2016-12-05 12:48:55.046603	\N
120	2016-12-05 12:48:57.242731	2016-12-05 12:48:57.242731	\N
35	2016-12-05 12:40:31.692281	2016-12-05 12:40:33.343944	\N
36	2016-12-05 12:40:35.531301	2016-12-05 12:40:35.531301	\N
37	2016-12-05 12:40:38.230427	2016-12-05 12:40:38.230427	\N
38	2016-12-05 12:40:39.325877	2016-12-05 12:40:39.325877	\N
39	2016-12-05 12:40:43.81995	2016-12-05 12:40:43.81995	\N
40	2016-12-05 12:40:45.493308	2016-12-05 12:40:45.493308	\N
41	2016-12-05 12:40:46.594539	2016-12-05 12:40:46.594539	\N
42	2016-12-05 12:40:47.686649	2016-12-05 12:40:47.686649	\N
43	2016-12-05 12:40:50.201941	2016-12-05 12:40:50.201941	\N
44	2016-12-05 12:40:55.103064	2016-12-05 12:40:55.103064	\N
45	2016-12-05 12:40:56.209917	2016-12-05 12:40:56.209917	\N
46	2016-12-05 12:40:57.839024	2016-12-05 12:40:57.839024	\N
47	2016-12-05 12:40:58.921009	2016-12-05 12:40:58.921009	\N
48	2016-12-05 12:41:00.030093	2016-12-05 12:41:00.030093	\N
49	2016-12-05 12:41:01.672729	2016-12-05 12:41:01.672729	\N
50	2016-12-05 12:41:02.777025	2016-12-05 12:41:02.777025	\N
121	2016-12-05 12:49:00.529851	2016-12-05 12:49:00.529851	\N
122	2016-12-05 12:49:02.158381	2016-12-05 12:49:02.158381	\N
51	2016-12-05 12:41:04.968348	2016-12-05 12:41:04.970498	\N
52	2016-12-05 12:41:07.701816	2016-12-05 12:41:07.701816	\N
53	2016-12-05 12:41:12.038407	2016-12-05 12:41:12.038407	\N
54	2016-12-05 12:41:14.222728	2016-12-05 12:41:14.222728	\N
55	2016-12-05 12:41:15.328802	2016-12-05 12:41:15.328802	\N
56	2016-12-05 12:41:17.499816	2016-12-05 12:41:17.499816	\N
57	2016-12-05 12:41:20.256298	2016-12-05 12:41:20.256298	\N
58	2016-12-05 12:41:21.339313	2016-12-05 12:41:21.339313	\N
59	2016-12-05 12:41:23.52042	2016-12-05 12:41:23.52042	\N
60	2016-12-05 12:41:26.302483	2016-12-05 12:41:26.302483	\N
61	2016-12-05 12:41:29.570722	2016-12-05 12:41:29.570722	\N
62	2016-12-05 12:41:31.726354	2016-12-05 12:41:31.726354	\N
63	2016-12-05 12:41:35.553499	2016-12-05 12:41:35.555487	\N
64	2016-12-05 12:41:37.229831	2016-12-05 12:41:37.229831	\N
123	2016-12-05 12:49:03.929428	2016-12-05 12:49:03.929428	\N
124	2016-12-05 12:49:06.762351	2016-12-05 12:49:06.762351	\N
65	2016-12-05 12:42:24.872742	2016-12-05 12:42:24.875743	\N
125	2016-12-05 12:49:08.961127	2016-12-05 12:49:08.961127	\N
126	2016-12-05 12:49:10.030747	2016-12-05 12:49:10.030747	\N
66	2016-12-05 12:43:16.587228	2016-12-05 12:43:16.58823	\N
67	2016-12-05 12:43:17.654718	2016-12-05 12:43:17.654718	\N
68	2016-12-05 12:43:19.975554	2016-12-05 12:43:19.975554	\N
69	2016-12-05 12:43:21.618035	2016-12-05 12:43:21.618035	\N
70	2016-12-05 12:43:23.896277	2016-12-05 12:43:23.896277	\N
71	2016-12-05 12:43:25.560149	2016-12-05 12:43:25.560149	\N
72	2016-12-05 12:43:26.640902	2016-12-05 12:43:26.640902	\N
73	2016-12-05 12:43:30.464322	2016-12-05 12:43:30.464322	\N
74	2016-12-05 12:43:31.572757	2016-12-05 12:43:31.572757	\N
127	2016-12-05 12:49:11.152633	2016-12-05 12:49:11.152633	\N
128	2016-12-05 12:49:12.209119	2016-12-05 12:49:12.209119	\N
75	2016-12-05 12:43:59.637605	2016-12-05 12:43:59.638606	\N
76	2016-12-05 12:44:08.477922	2016-12-05 12:44:08.477922	\N
77	2016-12-05 12:44:12.742671	2016-12-05 12:44:12.742671	\N
78	2016-12-05 12:44:16.541898	2016-12-05 12:44:16.541898	\N
79	2016-12-05 12:44:19.647146	2016-12-05 12:44:19.647146	\N
129	2016-12-05 12:49:13.296522	2016-12-05 12:49:13.296522	\N
130	2016-12-05 12:49:17.134503	2016-12-05 12:49:17.134503	\N
131	2016-12-05 12:49:20.412342	2016-12-05 12:49:20.414343	\N
132	2016-12-05 12:49:21.520236	2016-12-05 12:49:21.520236	\N
133	2016-12-05 12:49:24.788711	2016-12-05 12:49:24.788711	\N
134	2016-12-05 12:49:26.43686	2016-12-05 12:49:26.43686	\N
135	2016-12-05 12:49:28.616346	2016-12-05 12:49:28.616346	\N
80	2016-12-05 12:44:21.580839	2016-12-05 12:44:32.932756	\N
81	2016-12-05 12:44:38.338656	2016-12-05 12:44:38.338656	\N
82	2016-12-05 12:44:45.802593	2016-12-05 12:44:45.802593	\N
83	2016-12-05 12:44:57.304804	2016-12-05 12:44:57.304804	\N
136	2016-12-05 12:49:29.758092	2016-12-05 12:49:29.758092	\N
137	2016-12-05 12:49:30.845453	2016-12-05 12:49:30.845453	\N
84	2016-12-05 12:45:19.139475	2016-12-05 12:45:19.141476	\N
85	2016-12-05 12:45:24.936605	2016-12-05 12:45:24.936605	\N
86	2016-12-05 12:45:28.24401	2016-12-05 12:45:28.24401	\N
87	2016-12-05 12:45:32.025111	2016-12-05 12:45:32.025111	\N
12	2016-12-05 10:36:07.125728	2016-12-05 10:46:29.772338	\N
138	2016-12-05 12:49:33.539962	2016-12-05 12:49:33.539962	\N
139	2016-12-05 12:49:36.824517	2016-12-05 12:49:36.824517	\N
88	2016-12-05 12:46:14.320659	2016-12-05 12:46:14.32166	\N
89	2016-12-05 12:46:19.343542	2016-12-05 12:46:19.343542	\N
90	2016-12-05 12:47:00.62978	2016-12-05 12:47:00.62978	\N
91	2016-12-05 12:47:03.087255	2016-12-05 12:47:03.087255	\N
140	2016-12-05 12:49:37.953426	2016-12-05 12:49:37.953426	\N
92	2016-12-05 12:47:08.600021	2016-12-05 12:47:08.601022	\N
93	2016-12-05 12:47:17.989326	2016-12-05 12:47:17.989326	\N
94	2016-12-05 12:47:24.766715	2016-12-05 12:47:24.766715	\N
95	2016-12-05 12:47:30.400176	2016-12-05 12:47:30.400176	\N
96	2016-12-05 12:47:34.008098	2016-12-05 12:47:34.008098	\N
141	2016-12-05 12:49:39.043614	2016-12-05 12:49:39.043614	\N
142	2016-12-05 12:49:40.14116	2016-12-05 12:49:40.14116	\N
97	2016-12-05 12:47:37.85056	2016-12-05 12:47:38.997374	\N
98	2016-12-05 12:47:48.60705	2016-12-05 12:47:48.60705	\N
99	2016-12-05 12:47:54.592752	2016-12-05 12:47:54.592752	\N
143	2016-12-05 12:49:42.288421	2016-12-05 12:49:42.288421	\N
100	2016-12-05 12:48:18.909632	2016-12-05 12:48:18.911635	\N
101	2016-12-05 12:48:21.118817	2016-12-05 12:48:21.118817	\N
102	2016-12-05 12:48:22.222886	2016-12-05 12:48:22.222886	\N
110	2016-12-05 12:48:37.016178	2016-12-05 12:48:37.016178	\N
111	2016-12-05 12:48:39.196377	2016-12-05 12:48:39.196377	\N
112	2016-12-05 12:48:40.852501	2016-12-05 12:48:40.852501	\N
113	2016-12-05 12:48:44.12552	2016-12-05 12:48:44.12552	\N
114	2016-12-05 12:48:46.875307	2016-12-05 12:48:46.875307	\N
106	2016-12-05 12:48:31.010423	2016-12-05 12:48:31.010423	Test 2
107	2016-12-05 12:48:32.0945	2016-12-05 12:48:32.0945	test 3
144	2016-12-05 12:50:28.488583	2016-12-05 12:50:28.507585	\N
145	2016-12-05 12:50:30.74112	2016-12-05 12:50:30.74112	\N
146	2016-12-05 12:50:31.833302	2016-12-05 12:50:31.833302	\N
147	2016-12-05 12:50:33.460565	2016-12-05 12:50:33.460565	\N
148	2016-12-05 12:50:34.569736	2016-12-05 12:50:34.569736	\N
149	2016-12-05 12:50:37.301289	2016-12-05 12:50:37.301289	\N
150	2016-12-05 12:50:38.960968	2016-12-05 12:50:38.960968	\N
151	2016-12-05 12:50:42.74961	2016-12-05 12:50:42.74961	\N
152	2016-12-05 12:50:44.41648	2016-12-05 12:50:44.41648	\N
153	2016-12-05 12:50:45.530374	2016-12-05 12:50:45.530374	\N
154	2016-12-05 12:50:48.792397	2016-12-05 12:50:48.792397	\N
155	2016-12-05 12:50:50.423136	2016-12-05 12:50:50.423136	\N
156	2016-12-05 12:50:52.070903	2016-12-05 12:50:52.070903	\N
157	2016-12-05 12:50:53.710371	2016-12-05 12:50:53.710371	\N
158	2016-12-05 12:50:55.866093	2016-12-05 12:50:55.866093	\N
108	2016-12-05 12:48:33.713236	2016-12-05 12:48:33.713236	Test 4
109	2016-12-05 12:48:35.913207	2016-12-05 12:48:35.913207	Test 5
103	2016-12-05 12:48:23.351753	2016-12-05 12:48:23.351753	Test 1
104	2016-12-05 12:48:27.167329	2016-12-05 12:48:27.167329	Test 2
159	2016-12-05 12:50:59.17978	2016-12-05 12:50:59.181579	\N
160	2016-12-05 12:51:01.374019	2016-12-05 12:51:01.374019	\N
265	2016-12-05 13:39:04.876675	2016-12-05 13:39:04.878127	\N
266	2016-12-05 13:39:08.702424	2016-12-05 13:39:08.702424	\N
161	2016-12-05 12:51:05.207771	2016-12-05 12:57:49.356572	\N
162	2016-12-05 12:57:53.940802	2016-12-05 12:57:53.940802	\N
163	2016-12-05 12:57:56.125267	2016-12-05 12:57:56.125267	\N
164	2016-12-05 12:57:58.328182	2016-12-05 12:57:58.328182	\N
165	2016-12-05 12:58:00.537412	2016-12-05 12:58:00.537412	\N
166	2016-12-05 12:58:02.713753	2016-12-05 12:58:02.713753	\N
167	2016-12-05 12:58:05.45918	2016-12-05 12:58:05.45918	\N
168	2016-12-05 12:58:07.075621	2016-12-05 12:58:07.075621	\N
169	2016-12-05 12:58:08.195596	2016-12-05 12:58:08.195596	\N
170	2016-12-05 12:58:10.904047	2016-12-05 12:58:10.904047	\N
171	2016-12-05 12:58:14.182344	2016-12-05 12:58:14.182344	\N
172	2016-12-05 12:58:16.925066	2016-12-05 12:58:16.925066	\N
267	2016-12-05 13:39:09.787546	2016-12-05 13:39:09.787546	\N
173	2016-12-05 12:58:19.139634	2016-12-05 12:58:19.67467	\N
174	2016-12-05 12:58:21.329701	2016-12-05 12:58:21.329701	\N
268	2016-12-05 13:39:14.695164	2016-12-05 13:39:14.695164	\N
269	2016-12-05 13:39:16.370689	2016-12-05 13:39:16.370689	\N
175	2016-12-05 12:59:22.135264	2016-12-05 12:59:22.136266	\N
176	2016-12-05 12:59:24.320104	2016-12-05 12:59:24.320104	\N
177	2016-12-05 12:59:25.400591	2016-12-05 12:59:25.400591	\N
178	2016-12-05 12:59:28.140447	2016-12-05 12:59:28.140447	\N
179	2016-12-05 12:59:32.51335	2016-12-05 12:59:32.51335	\N
180	2016-12-05 12:59:34.722398	2016-12-05 12:59:34.722398	\N
181	2016-12-05 12:59:37.426554	2016-12-05 12:59:37.426554	\N
182	2016-12-05 12:59:40.844004	2016-12-05 12:59:40.844004	\N
183	2016-12-05 12:59:42.492278	2016-12-05 12:59:42.492278	\N
270	2016-12-05 13:39:19.603582	2016-12-05 13:39:19.603582	\N
271	2016-12-05 13:39:22.371553	2016-12-05 13:39:22.371553	\N
184	2016-12-05 13:01:09.85488	2016-12-05 13:01:09.85588	\N
185	2016-12-05 13:01:11.514697	2016-12-05 13:01:11.514697	\N
186	2016-12-05 13:01:13.788931	2016-12-05 13:01:13.788931	\N
187	2016-12-05 13:01:15.654198	2016-12-05 13:01:15.654198	\N
188	2016-12-05 13:01:17.296114	2016-12-05 13:01:17.296114	\N
189	2016-12-05 13:01:22.873724	2016-12-05 13:01:22.873724	\N
190	2016-12-05 13:01:23.999249	2016-12-05 13:01:23.999249	\N
191	2016-12-05 13:01:25.610471	2016-12-05 13:01:25.610471	\N
272	2016-12-05 13:39:28.39689	2016-12-05 13:39:28.39689	\N
273	2016-12-05 13:39:30.590572	2016-12-05 13:39:30.590572	\N
274	2016-12-05 13:39:32.218934	2016-12-05 13:39:32.218934	\N
275	2016-12-05 13:39:33.296828	2016-12-05 13:39:33.296828	\N
192	2016-12-05 13:01:39.438255	2016-12-05 13:01:52.917668	\N
193	2016-12-05 13:01:56.276244	2016-12-05 13:01:56.276244	\N
194	2016-12-05 13:01:57.338653	2016-12-05 13:01:57.338653	\N
195	2016-12-05 13:01:59.567572	2016-12-05 13:01:59.567572	\N
196	2016-12-05 13:02:00.62168	2016-12-05 13:02:00.62168	\N
197	2016-12-05 13:02:02.302461	2016-12-05 13:02:02.302461	\N
198	2016-12-05 13:02:13.418445	2016-12-05 13:02:13.418445	\N
199	2016-12-05 13:02:15.604511	2016-12-05 13:02:15.604511	\N
308	2016-12-05 13:51:01.480818	2016-12-05 13:51:01.481318	\N
309	2016-12-05 13:51:04.80496	2016-12-05 13:51:04.80496	\N
276	2016-12-05 13:39:34.380418	2016-12-05 13:39:34.949259	\N
277	2016-12-05 13:39:37.676065	2016-12-05 13:39:37.676065	\N
278	2016-12-05 13:39:41.093342	2016-12-05 13:39:41.093342	\N
279	2016-12-05 13:39:48.191346	2016-12-05 13:39:48.191346	\N
280	2016-12-05 13:39:49.250116	2016-12-05 13:39:49.250116	\N
281	2016-12-05 13:39:50.380749	2016-12-05 13:39:50.380749	\N
282	2016-12-05 13:39:54.787653	2016-12-05 13:39:54.787653	\N
283	2016-12-05 13:39:56.399255	2016-12-05 13:39:56.399255	\N
284	2016-12-05 13:39:57.492509	2016-12-05 13:39:57.492509	\N
285	2016-12-05 13:39:59.655345	2016-12-05 13:39:59.655345	\N
286	2016-12-05 13:40:01.330631	2016-12-05 13:40:01.330631	\N
200	2016-12-05 13:02:17.246208	2016-12-05 13:02:23.057092	\N
201	2016-12-05 13:02:24.139838	2016-12-05 13:02:24.139838	\N
202	2016-12-05 13:02:25.808441	2016-12-05 13:02:25.808441	\N
203	2016-12-05 13:02:27.564319	2016-12-05 13:02:27.564319	\N
204	2016-12-05 13:02:29.942876	2016-12-05 13:02:29.942876	\N
205	2016-12-05 13:02:32.791456	2016-12-05 13:02:32.791456	\N
206	2016-12-05 13:02:34.420617	2016-12-05 13:02:34.420617	\N
207	2016-12-05 13:02:50.83793	2016-12-05 13:02:50.83793	\N
208	2016-12-05 13:02:53.038379	2016-12-05 13:02:53.038379	\N
209	2016-12-05 13:02:55.879098	2016-12-05 13:02:55.87998	\N
210	2016-12-05 13:02:56.996266	2016-12-05 13:02:56.996266	\N
211	2016-12-05 13:35:40.496664	2016-12-05 13:35:40.496664	\N
212	2016-12-05 13:35:42.647126	2016-12-05 13:35:42.647126	\N
213	2016-12-05 13:35:44.857462	2016-12-05 13:35:44.857462	\N
214	2016-12-05 13:35:49.243739	2016-12-05 13:35:49.243739	\N
215	2016-12-05 13:35:50.890644	2016-12-05 13:35:50.892354	\N
216	2016-12-05 13:35:56.340304	2016-12-05 13:35:56.340304	\N
217	2016-12-05 13:35:57.980895	2016-12-05 13:35:57.980895	\N
218	2016-12-05 13:35:59.086487	2016-12-05 13:35:59.086487	\N
219	2016-12-05 13:36:04.574391	2016-12-05 13:36:04.574391	\N
220	2016-12-05 13:36:06.731162	2016-12-05 13:36:06.731162	\N
221	2016-12-05 13:36:08.910826	2016-12-05 13:36:08.910826	\N
222	2016-12-05 13:36:11.106394	2016-12-05 13:36:11.106394	\N
223	2016-12-05 13:36:13.438862	2016-12-05 13:36:13.438862	\N
224	2016-12-05 13:36:17.283889	2016-12-05 13:36:17.283889	\N
225	2016-12-05 13:36:18.364027	2016-12-05 13:36:18.364027	\N
287	2016-12-05 13:40:02.409697	2016-12-05 13:40:02.409697	\N
288	2016-12-05 13:40:03.517601	2016-12-05 13:40:03.517601	\N
226	2016-12-05 13:36:22.26776	2016-12-05 13:36:22.26926	\N
227	2016-12-05 13:36:25.088574	2016-12-05 13:36:25.088574	\N
228	2016-12-05 13:36:26.741305	2016-12-05 13:36:26.741305	\N
229	2016-12-05 13:36:28.347612	2016-12-05 13:36:28.347612	\N
230	2016-12-05 13:36:30.736158	2016-12-05 13:36:30.736158	\N
231	2016-12-05 13:36:32.97332	2016-12-05 13:36:32.97332	\N
232	2016-12-05 13:36:35.328491	2016-12-05 13:36:35.328491	\N
233	2016-12-05 13:36:36.432087	2016-12-05 13:36:36.432087	\N
234	2016-12-05 13:36:37.615306	2016-12-05 13:36:37.615306	\N
289	2016-12-05 13:50:16.974714	2016-12-05 13:50:16.974714	\N
235	2016-12-05 13:37:33.397819	2016-12-05 13:37:33.399402	\N
236	2016-12-05 13:37:35.03086	2016-12-05 13:37:35.03086	\N
237	2016-12-05 13:37:36.142526	2016-12-05 13:37:36.142526	\N
238	2016-12-05 13:37:37.228908	2016-12-05 13:37:37.228908	\N
239	2016-12-05 13:37:42.150641	2016-12-05 13:37:42.150641	\N
240	2016-12-05 13:37:43.205376	2016-12-05 13:37:43.205376	\N
241	2016-12-05 13:37:48.172933	2016-12-05 13:37:48.172933	\N
242	2016-12-05 13:37:49.794493	2016-12-05 13:37:49.794493	\N
243	2016-12-05 13:37:51.987296	2016-12-05 13:37:51.987296	\N
244	2016-12-05 13:37:54.728986	2016-12-05 13:37:54.728986	\N
245	2016-12-05 13:37:55.826377	2016-12-05 13:37:55.826377	\N
246	2016-12-05 13:37:58.016364	2016-12-05 13:37:58.016364	\N
247	2016-12-05 13:38:00.747285	2016-12-05 13:38:00.747285	\N
248	2016-12-05 13:38:01.816372	2016-12-05 13:38:01.816372	\N
290	2016-12-05 13:50:19.248269	2016-12-05 13:50:19.248269	\N
249	2016-12-05 13:38:02.913755	2016-12-05 13:38:03.478108	\N
250	2016-12-05 13:38:06.212394	2016-12-05 13:38:06.212394	\N
251	2016-12-05 13:38:15.304495	2016-12-05 13:38:15.304495	\N
252	2016-12-05 13:38:16.404266	2016-12-05 13:38:16.404266	\N
253	2016-12-05 13:38:18.812264	2016-12-05 13:38:18.812264	\N
254	2016-12-05 13:38:22.175827	2016-12-05 13:38:22.175827	\N
255	2016-12-05 13:38:25.041544	2016-12-05 13:38:25.041544	\N
256	2016-12-05 13:38:47.377423	2016-12-05 13:38:47.377423	\N
257	2016-12-05 13:38:49.00211	2016-12-05 13:38:49.00211	\N
258	2016-12-05 13:38:50.585541	2016-12-05 13:38:50.585541	\N
259	2016-12-05 13:38:52.255934	2016-12-05 13:38:52.255934	\N
260	2016-12-05 13:38:54.468754	2016-12-05 13:38:54.468754	\N
261	2016-12-05 13:38:56.109756	2016-12-05 13:38:56.109756	\N
262	2016-12-05 13:38:57.215689	2016-12-05 13:38:57.215689	\N
263	2016-12-05 13:38:59.946972	2016-12-05 13:38:59.946972	\N
264	2016-12-05 13:39:02.139819	2016-12-05 13:39:02.139819	\N
291	2016-12-05 13:50:23.08211	2016-12-05 13:50:23.08211	\N
292	2016-12-05 13:50:24.183678	2016-12-05 13:50:24.183678	\N
293	2016-12-05 13:50:25.787071	2016-12-05 13:50:25.787071	\N
294	2016-12-05 13:50:26.905755	2016-12-05 13:50:26.905755	\N
295	2016-12-05 13:50:30.189627	2016-12-05 13:50:30.191123	\N
296	2016-12-05 13:50:31.296026	2016-12-05 13:50:31.296026	\N
297	2016-12-05 13:50:35.113346	2016-12-05 13:50:35.113346	\N
298	2016-12-05 13:50:37.28868	2016-12-05 13:50:37.28868	\N
299	2016-12-05 13:50:41.655758	2016-12-05 13:50:41.655758	\N
300	2016-12-05 13:50:43.324441	2016-12-05 13:50:43.324441	\N
301	2016-12-05 13:50:44.397302	2016-12-05 13:50:44.397302	\N
302	2016-12-05 13:50:46.728726	2016-12-05 13:50:46.728726	\N
303	2016-12-05 13:50:47.805359	2016-12-05 13:50:47.805359	\N
304	2016-12-05 13:50:48.919239	2016-12-05 13:50:48.919239	\N
305	2016-12-05 13:50:52.930286	2016-12-05 13:50:52.930286	\N
306	2016-12-05 13:50:57.728614	2016-12-05 13:50:57.728614	\N
307	2016-12-05 13:50:58.859718	2016-12-05 13:50:58.859718	\N
310	2016-12-05 13:51:12.516602	2016-12-05 13:51:12.516602	\N
311	2016-12-05 13:51:14.708928	2016-12-05 13:51:14.708928	\N
312	2016-12-05 13:51:15.797213	2016-12-05 13:51:15.797213	\N
313	2016-12-05 13:51:17.435922	2016-12-05 13:51:17.435922	\N
314	2016-12-05 13:51:20.176711	2016-12-05 13:51:20.176711	\N
315	2016-12-05 13:51:25.634192	2016-12-05 13:51:25.634192	\N
316	2016-12-05 13:51:28.913936	2016-12-05 13:51:28.913936	\N
436	2016-12-05 13:56:46.587369	2016-12-05 13:56:46.588722	\N
437	2016-12-05 13:56:49.999135	2016-12-05 13:56:49.999135	\N
438	2016-12-05 13:56:52.723031	2016-12-05 13:56:52.723031	\N
317	2016-12-05 13:51:30.581082	2016-12-05 13:51:31.667963	\N
318	2016-12-05 13:51:32.769404	2016-12-05 13:51:32.769404	\N
319	2016-12-05 13:51:33.851188	2016-12-05 13:51:33.851188	\N
320	2016-12-05 13:51:37.699686	2016-12-05 13:51:37.699686	\N
321	2016-12-05 13:51:38.776444	2016-12-05 13:51:38.776444	\N
322	2016-12-05 13:51:40.422096	2016-12-05 13:51:40.422096	\N
323	2016-12-05 13:51:42.075541	2016-12-05 13:51:42.075541	\N
324	2016-12-05 13:51:44.809426	2016-12-05 13:51:44.809426	\N
325	2016-12-05 13:51:47.515686	2016-12-05 13:51:47.515686	\N
326	2016-12-05 13:51:49.191631	2016-12-05 13:51:49.191631	\N
327	2016-12-05 13:51:50.246852	2016-12-05 13:51:50.246852	\N
328	2016-12-05 13:51:53.542401	2016-12-05 13:51:53.542401	\N
329	2016-12-05 13:51:56.801761	2016-12-05 13:51:56.801761	\N
439	2016-12-05 13:56:54.899882	2016-12-05 13:56:54.899882	\N
440	2016-12-05 13:56:58.184433	2016-12-05 13:56:58.184433	\N
441	2016-12-05 13:56:59.388796	2016-12-05 13:56:59.388796	\N
442	2016-12-05 13:57:04.315308	2016-12-05 13:57:04.315308	\N
443	2016-12-05 13:57:05.40768	2016-12-05 13:57:05.40768	\N
444	2016-12-05 13:57:07.632932	2016-12-05 13:57:07.632932	\N
330	2016-12-05 13:51:59.533684	2016-12-05 13:52:01.719638	\N
331	2016-12-05 13:52:08.831531	2016-12-05 13:52:08.831531	\N
332	2016-12-05 13:52:09.942612	2016-12-05 13:52:09.942612	\N
333	2016-12-05 13:52:11.06058	2016-12-05 13:52:11.06058	\N
334	2016-12-05 13:52:14.342472	2016-12-05 13:52:14.342472	\N
335	2016-12-05 13:52:49.638285	2016-12-05 13:52:49.638285	\N
336	2016-12-05 13:52:51.852741	2016-12-05 13:52:51.852741	\N
337	2016-12-05 13:52:55.141884	2016-12-05 13:52:55.141884	\N
338	2016-12-05 13:52:56.263144	2016-12-05 13:52:56.263144	\N
339	2016-12-05 13:52:57.30987	2016-12-05 13:52:57.30987	\N
340	2016-12-05 13:52:58.424185	2016-12-05 13:52:58.424185	\N
341	2016-12-05 13:53:01.711892	2016-12-05 13:53:01.711892	\N
342	2016-12-05 13:53:04.442632	2016-12-05 13:53:04.442632	\N
343	2016-12-05 13:53:06.090952	2016-12-05 13:53:06.090952	\N
344	2016-12-05 13:53:07.175465	2016-12-05 13:53:07.175465	\N
345	2016-12-05 13:53:09.374333	2016-12-05 13:53:09.374333	\N
445	2016-12-05 13:57:10.332719	2016-12-05 13:57:10.332719	\N
346	2016-12-05 13:53:11.658238	2016-12-05 13:53:11.659877	\N
347	2016-12-05 13:53:12.752061	2016-12-05 13:53:12.752061	\N
348	2016-12-05 13:53:13.856974	2016-12-05 13:53:13.856974	\N
349	2016-12-05 13:53:14.941765	2016-12-05 13:53:14.941765	\N
350	2016-12-05 13:53:19.303561	2016-12-05 13:53:19.303561	\N
351	2016-12-05 13:53:33.545177	2016-12-05 13:53:33.545177	\N
352	2016-12-05 13:53:36.259832	2016-12-05 13:53:36.259832	\N
353	2016-12-05 13:53:37.905165	2016-12-05 13:53:37.905165	\N
354	2016-12-05 13:53:41.195163	2016-12-05 13:53:41.195163	\N
446	2016-12-05 13:57:13.085655	2016-12-05 13:57:13.085655	\N
355	2016-12-05 13:53:42.288458	2016-12-05 13:53:42.289152	\N
356	2016-12-05 13:53:43.924904	2016-12-05 13:53:43.924904	\N
357	2016-12-05 13:53:46.670264	2016-12-05 13:53:46.670264	\N
358	2016-12-05 13:53:48.282319	2016-12-05 13:53:48.282319	\N
359	2016-12-05 13:53:52.120064	2016-12-05 13:53:52.120064	\N
360	2016-12-05 13:53:54.854238	2016-12-05 13:53:54.854238	\N
361	2016-12-05 13:53:55.940804	2016-12-05 13:53:55.940804	\N
362	2016-12-05 13:53:58.14267	2016-12-05 13:53:58.14267	\N
363	2016-12-05 13:54:00.337204	2016-12-05 13:54:00.337204	\N
364	2016-12-05 13:54:02.510677	2016-12-05 13:54:02.510677	\N
365	2016-12-05 13:54:04.164634	2016-12-05 13:54:04.164634	\N
366	2016-12-05 13:54:05.800279	2016-12-05 13:54:05.800279	\N
367	2016-12-05 13:54:09.081626	2016-12-05 13:54:09.081626	\N
368	2016-12-05 13:54:11.826805	2016-12-05 13:54:11.826805	\N
447	2016-12-05 13:57:14.193221	2016-12-05 13:57:14.193221	\N
369	2016-12-05 13:54:12.895524	2016-12-05 13:54:12.897251	\N
370	2016-12-05 13:54:14.527726	2016-12-05 13:54:14.527726	\N
371	2016-12-05 13:54:16.71528	2016-12-05 13:54:16.71528	\N
372	2016-12-05 13:54:18.404134	2016-12-05 13:54:18.404134	\N
373	2016-12-05 13:54:19.47883	2016-12-05 13:54:19.47883	\N
374	2016-12-05 13:54:22.757918	2016-12-05 13:54:22.757918	\N
375	2016-12-05 13:54:24.392431	2016-12-05 13:54:24.392431	\N
376	2016-12-05 13:54:28.251489	2016-12-05 13:54:28.251489	\N
377	2016-12-05 13:54:30.974943	2016-12-05 13:54:30.974943	\N
378	2016-12-05 13:54:32.054629	2016-12-05 13:54:32.054629	\N
379	2016-12-05 13:54:33.161371	2016-12-05 13:54:33.161371	\N
380	2016-12-05 13:54:34.808382	2016-12-05 13:54:34.808382	\N
381	2016-12-05 13:54:36.416966	2016-12-05 13:54:36.416966	\N
382	2016-12-05 13:54:39.71561	2016-12-05 13:54:39.71561	\N
458	2016-12-05 13:57:42.062594	2016-12-05 13:57:47.304558	\N
383	2016-12-05 13:54:44.636538	2016-12-05 13:54:44.638149	\N
384	2016-12-05 13:54:49.007304	2016-12-05 13:54:49.007304	\N
385	2016-12-05 13:54:51.236233	2016-12-05 13:54:51.236233	\N
386	2016-12-05 13:54:52.810383	2016-12-05 13:54:52.810383	\N
387	2016-12-05 13:54:56.085885	2016-12-05 13:54:56.085885	\N
388	2016-12-05 13:54:58.311574	2016-12-05 13:54:58.311574	\N
389	2016-12-05 13:55:00.490977	2016-12-05 13:55:00.490977	\N
390	2016-12-05 13:55:02.662819	2016-12-05 13:55:02.662819	\N
391	2016-12-05 13:55:04.340131	2016-12-05 13:55:04.340131	\N
392	2016-12-05 13:55:05.425781	2016-12-05 13:55:05.425781	\N
393	2016-12-05 13:55:08.691792	2016-12-05 13:55:08.691792	\N
394	2016-12-05 13:55:10.865201	2016-12-05 13:55:10.865201	\N
395	2016-12-05 13:55:13.63105	2016-12-05 13:55:13.63105	\N
396	2016-12-05 13:55:15.254179	2016-12-05 13:55:15.255628	\N
397	2016-12-05 13:55:16.904069	2016-12-05 13:55:16.904069	\N
398	2016-12-05 13:55:19.646198	2016-12-05 13:55:19.646198	\N
399	2016-12-05 13:55:24.551389	2016-12-05 13:55:24.551389	\N
400	2016-12-05 13:55:26.223493	2016-12-05 13:55:26.223493	\N
401	2016-12-05 13:55:27.830217	2016-12-05 13:55:27.830217	\N
402	2016-12-05 13:55:28.951526	2016-12-05 13:55:28.951526	\N
403	2016-12-05 13:55:30.560903	2016-12-05 13:55:30.560903	\N
404	2016-12-05 13:55:33.290781	2016-12-05 13:55:33.290781	\N
405	2016-12-05 13:55:36.033978	2016-12-05 13:55:36.033978	\N
406	2016-12-05 13:55:39.332558	2016-12-05 13:55:39.332558	\N
407	2016-12-05 13:55:40.939905	2016-12-05 13:55:40.939905	\N
408	2016-12-05 13:55:43.685356	2016-12-05 13:55:43.685356	\N
409	2016-12-05 13:55:45.359353	2016-12-05 13:55:45.360355	\N
410	2016-12-05 13:55:48.091353	2016-12-05 13:55:48.091353	\N
411	2016-12-05 13:55:49.150238	2016-12-05 13:55:49.150238	\N
412	2016-12-05 13:55:50.822602	2016-12-05 13:55:50.822602	\N
413	2016-12-05 13:55:54.07234	2016-12-05 13:55:54.07234	\N
414	2016-12-05 13:55:55.754117	2016-12-05 13:55:55.754117	\N
415	2016-12-05 13:55:57.393213	2016-12-05 13:55:57.393213	\N
416	2016-12-05 13:55:58.999133	2016-12-05 13:55:58.999133	\N
417	2016-12-05 13:56:00.103491	2016-12-05 13:56:00.103491	\N
418	2016-12-05 13:56:01.212484	2016-12-05 13:56:01.212484	\N
419	2016-12-05 13:56:08.351824	2016-12-05 13:56:08.351824	\N
420	2016-12-05 13:56:09.421857	2016-12-05 13:56:09.421857	\N
421	2016-12-05 13:56:11.104837	2016-12-05 13:56:11.104837	\N
422	2016-12-05 13:56:12.175358	2016-12-05 13:56:12.175358	\N
423	2016-12-05 13:56:14.86631	2016-12-05 13:56:14.86631	\N
424	2016-12-05 13:56:16.006216	2016-12-05 13:56:16.007721	\N
425	2016-12-05 13:56:19.241816	2016-12-05 13:56:19.241816	\N
426	2016-12-05 13:56:20.436507	2016-12-05 13:56:20.436507	\N
427	2016-12-05 13:56:24.288822	2016-12-05 13:56:24.288822	\N
428	2016-12-05 13:56:28.113853	2016-12-05 13:56:28.113853	\N
429	2016-12-05 13:56:29.298431	2016-12-05 13:56:29.298431	\N
430	2016-12-05 13:56:30.975136	2016-12-05 13:56:30.975136	\N
431	2016-12-05 13:56:33.72398	2016-12-05 13:56:33.72398	\N
432	2016-12-05 13:56:36.409283	2016-12-05 13:56:36.409283	\N
433	2016-12-05 13:56:38.59745	2016-12-05 13:56:38.59745	\N
434	2016-12-05 13:56:44.185772	2016-12-05 13:56:44.185772	\N
435	2016-12-05 13:56:45.425484	2016-12-05 13:56:45.425484	\N
448	2016-12-05 13:57:16.45858	2016-12-05 13:57:17.00405	\N
449	2016-12-05 13:57:18.132751	2016-12-05 13:57:18.132751	\N
450	2016-12-05 13:57:21.936248	2016-12-05 13:57:21.936248	\N
451	2016-12-05 13:57:24.700545	2016-12-05 13:57:24.700545	\N
452	2016-12-05 13:57:26.304755	2016-12-05 13:57:26.304755	\N
453	2016-12-05 13:57:27.937526	2016-12-05 13:57:27.937526	\N
454	2016-12-05 13:57:29.035362	2016-12-05 13:57:29.035362	\N
455	2016-12-05 13:57:32.444436	2016-12-05 13:57:32.444436	\N
456	2016-12-05 13:57:37.036723	2016-12-05 13:57:37.036723	\N
457	2016-12-05 13:57:38.330337	2016-12-05 13:57:38.330337	\N
459	2016-12-05 13:57:48.505154	2016-12-05 13:57:48.505154	\N
460	2016-12-05 13:57:52.587472	2016-12-05 13:57:52.587472	\N
461	2016-12-05 13:57:53.655488	2016-12-05 13:57:53.655488	\N
462	2016-12-05 13:57:57.044294	2016-12-05 13:57:57.044294	\N
463	2016-12-05 13:58:00.435883	2016-12-05 13:58:00.435883	\N
464	2016-12-05 13:58:02.107163	2016-12-05 13:58:02.107163	\N
465	2016-12-05 13:58:03.719352	2016-12-05 13:58:03.719352	\N
466	2016-12-05 13:58:04.80713	2016-12-05 13:58:04.80713	\N
467	2016-12-05 13:58:08.638187	2016-12-05 13:58:08.638187	\N
468	2016-12-05 13:58:10.413966	2016-12-05 13:58:10.413966	\N
469	2016-12-05 13:58:12.152399	2016-12-05 13:58:12.152399	\N
470	2016-12-05 13:58:14.006033	2016-12-05 13:58:14.006033	\N
471	2016-12-05 13:58:16.72314	2016-12-05 13:58:16.72314	\N
472	2016-12-05 13:58:18.393459	2016-12-05 13:58:18.39446	\N
473	2016-12-05 13:58:20.136362	2016-12-05 13:58:20.136362	\N
474	2016-12-05 13:58:22.308187	2016-12-05 13:58:22.308187	\N
475	2016-12-05 13:58:23.410334	2016-12-05 13:58:23.410334	\N
476	2016-12-05 13:58:45.812142	2016-12-05 13:58:45.812142	\N
477	2016-12-05 13:58:48.516445	2016-12-05 13:58:48.516445	\N
478	2016-12-05 13:58:53.149804	2016-12-05 13:58:53.149804	\N
479	2016-12-05 13:58:54.783718	2016-12-05 13:58:54.783718	\N
480	2016-12-05 13:58:56.434129	2016-12-05 13:58:56.434129	\N
481	2016-12-05 13:58:58.067368	2016-12-05 13:58:58.067368	\N
482	2016-12-05 13:58:59.26984	2016-12-05 13:58:59.26984	\N
483	2016-12-05 13:59:00.904487	2016-12-05 13:59:00.904487	\N
613	2016-12-05 14:34:23.335942	2016-12-05 14:34:23.92848	\N
484	2016-12-05 13:59:03.668372	2016-12-05 13:59:03.670044	\N
485	2016-12-05 13:59:04.757296	2016-12-05 13:59:04.757296	\N
486	2016-12-05 13:59:05.832294	2016-12-05 13:59:05.832294	\N
487	2016-12-05 13:59:07.479723	2016-12-05 13:59:07.479723	\N
488	2016-12-05 13:59:09.110624	2016-12-05 13:59:09.110624	\N
489	2016-12-05 13:59:11.819584	2016-12-05 13:59:11.819584	\N
490	2016-12-05 13:59:14.036669	2016-12-05 13:59:14.036669	\N
491	2016-12-05 13:59:17.326998	2016-12-05 13:59:17.326998	\N
492	2016-12-05 13:59:19.517757	2016-12-05 13:59:19.517757	\N
493	2016-12-05 13:59:22.240407	2016-12-05 13:59:22.240407	\N
494	2016-12-05 13:59:24.432907	2016-12-05 13:59:24.432907	\N
495	2016-12-05 13:59:26.629483	2016-12-05 13:59:26.629483	\N
496	2016-12-05 13:59:27.710888	2016-12-05 13:59:27.710888	\N
497	2016-12-05 13:59:28.814868	2016-12-05 13:59:28.814868	\N
498	2016-12-05 13:59:29.904265	2016-12-05 13:59:29.904265	\N
499	2016-12-05 13:59:30.994614	2016-12-05 13:59:30.994614	\N
500	2016-12-05 13:59:34.261973	2016-12-05 13:59:34.261973	\N
501	2016-12-05 13:59:35.368088	2016-12-05 13:59:35.368088	\N
502	2016-12-05 13:59:38.650243	2016-12-05 13:59:38.650243	\N
503	2016-12-05 13:59:39.750417	2016-12-05 13:59:39.750417	\N
504	2016-12-05 13:59:41.391283	2016-12-05 13:59:41.391283	\N
505	2016-12-05 13:59:44.125188	2016-12-05 13:59:44.125188	\N
506	2016-12-05 13:59:46.316209	2016-12-05 13:59:46.316209	\N
507	2016-12-05 13:59:49.611889	2016-12-05 13:59:49.611889	\N
508	2016-12-05 13:59:51.780534	2016-12-05 13:59:51.780534	\N
509	2016-12-05 13:59:54.532963	2016-12-05 13:59:54.532963	\N
510	2016-12-05 13:59:56.700882	2016-12-05 13:59:56.700882	\N
511	2016-12-05 13:59:58.890267	2016-12-05 13:59:58.890267	\N
512	2016-12-05 13:59:59.992976	2016-12-05 13:59:59.992976	\N
590	2016-12-05 14:33:22.534722	2016-12-05 14:33:22.536223	\N
591	2016-12-05 14:33:24.714296	2016-12-05 14:33:24.714296	\N
592	2016-12-05 14:33:26.895808	2016-12-05 14:33:26.895808	\N
513	2016-12-05 14:00:03.78915	2016-12-05 14:00:04.371457	\N
514	2016-12-05 14:00:07.100142	2016-12-05 14:00:07.100142	\N
515	2016-12-05 14:00:08.740891	2016-12-05 14:00:08.740891	\N
516	2016-12-05 14:00:25.508557	2016-12-05 14:00:25.508557	\N
517	2016-12-05 14:00:30.949367	2016-12-05 14:00:30.949367	\N
518	2016-12-05 14:00:33.130512	2016-12-05 14:00:33.130512	\N
593	2016-12-05 14:33:33.438517	2016-12-05 14:33:33.438517	\N
519	2016-12-05 14:00:35.369207	2016-12-05 14:00:35.369719	\N
520	2016-12-05 14:00:37.512979	2016-12-05 14:00:37.512979	\N
521	2016-12-05 14:00:39.702173	2016-12-05 14:00:39.702173	\N
522	2016-12-05 14:00:40.831216	2016-12-05 14:00:40.831216	\N
523	2016-12-05 14:00:42.977122	2016-12-05 14:00:42.977122	\N
524	2016-12-05 14:00:47.406127	2016-12-05 14:00:47.406127	\N
525	2016-12-05 14:00:53.380283	2016-12-05 14:00:53.380283	\N
526	2016-12-05 14:01:10.126107	2016-12-05 14:01:10.126107	\N
527	2016-12-05 14:01:11.209989	2016-12-05 14:01:11.209989	\N
528	2016-12-05 14:01:13.39561	2016-12-05 14:01:13.39561	\N
529	2016-12-05 14:01:16.15085	2016-12-05 14:01:16.15085	\N
530	2016-12-05 14:01:18.871761	2016-12-05 14:01:18.871761	\N
531	2016-12-05 14:01:20.525256	2016-12-05 14:01:20.525256	\N
532	2016-12-05 14:01:21.617096	2016-12-05 14:01:21.617096	\N
533	2016-12-05 14:01:24.367769	2016-12-05 14:01:24.367769	\N
534	2016-12-05 14:01:25.458596	2016-12-05 14:01:25.458596	\N
594	2016-12-05 14:33:35.629963	2016-12-05 14:33:35.629963	\N
595	2016-12-05 14:33:37.278486	2016-12-05 14:33:37.278486	\N
535	2016-12-05 14:01:28.205774	2016-12-05 14:01:28.207287	\N
536	2016-12-05 14:01:30.370777	2016-12-05 14:01:30.370777	\N
537	2016-12-05 14:01:31.47297	2016-12-05 14:01:31.47297	\N
538	2016-12-05 14:01:33.655271	2016-12-05 14:01:33.655271	\N
539	2016-12-05 14:01:34.752009	2016-12-05 14:01:34.752009	\N
540	2016-12-05 14:01:38.028712	2016-12-05 14:01:38.028712	\N
541	2016-12-05 14:01:40.760275	2016-12-05 14:01:40.760275	\N
542	2016-12-05 14:01:42.956837	2016-12-05 14:01:42.956837	\N
543	2016-12-05 14:01:44.630127	2016-12-05 14:01:44.630127	\N
544	2016-12-05 14:01:45.682916	2016-12-05 14:01:45.682916	\N
545	2016-12-05 14:01:46.782092	2016-12-05 14:01:46.782092	\N
546	2016-12-05 14:01:47.876979	2016-12-05 14:01:47.876979	\N
547	2016-12-05 14:01:49.527382	2016-12-05 14:01:49.527382	\N
548	2016-12-05 14:29:57.578426	2016-12-05 14:29:57.578426	\N
549	2016-12-05 14:30:00.865488	2016-12-05 14:30:00.865488	\N
550	2016-12-05 14:30:02.504115	2016-12-05 14:30:02.504115	\N
596	2016-12-05 14:33:39.473508	2016-12-05 14:33:39.473508	\N
597	2016-12-05 14:33:43.280739	2016-12-05 14:33:43.280739	\N
598	2016-12-05 14:33:44.507713	2016-12-05 14:33:44.507713	\N
599	2016-12-05 14:33:46.128534	2016-12-05 14:33:46.128534	\N
600	2016-12-05 14:33:47.773365	2016-12-05 14:33:47.773365	\N
551	2016-12-05 14:30:04.121255	2016-12-05 14:30:51.798317	\N
552	2016-12-05 14:30:53.501007	2016-12-05 14:30:53.501007	\N
553	2016-12-05 14:30:54.603767	2016-12-05 14:30:54.603767	\N
554	2016-12-05 14:30:56.765995	2016-12-05 14:30:56.765995	\N
555	2016-12-05 14:31:00.168381	2016-12-05 14:31:00.168381	\N
556	2016-12-05 14:31:02.359634	2016-12-05 14:31:02.359634	\N
557	2016-12-05 14:31:04.533366	2016-12-05 14:31:04.533366	\N
558	2016-12-05 14:31:06.186607	2016-12-05 14:31:06.186607	\N
559	2016-12-05 14:31:10.52705	2016-12-05 14:31:10.52705	\N
560	2016-12-05 14:31:11.634601	2016-12-05 14:31:11.634601	\N
561	2016-12-05 14:31:13.814395	2016-12-05 14:31:13.814395	\N
562	2016-12-05 14:31:17.137284	2016-12-05 14:31:17.137284	\N
563	2016-12-05 14:31:18.761876	2016-12-05 14:31:18.761876	\N
564	2016-12-05 14:31:21.499364	2016-12-05 14:31:21.499364	\N
565	2016-12-05 14:31:23.145847	2016-12-05 14:31:23.147435	\N
566	2016-12-05 14:31:24.226834	2016-12-05 14:31:24.226834	\N
567	2016-12-05 14:31:26.408504	2016-12-05 14:31:26.408504	\N
568	2016-12-05 14:31:29.794831	2016-12-05 14:31:29.794831	\N
569	2016-12-05 14:31:30.899281	2016-12-05 14:31:30.899281	\N
570	2016-12-05 14:31:33.739415	2016-12-05 14:31:33.739415	\N
571	2016-12-05 14:31:36.579678	2016-12-05 14:31:36.579678	\N
572	2016-12-05 14:31:37.655225	2016-12-05 14:31:37.655225	\N
573	2016-12-05 14:31:43.710893	2016-12-05 14:31:43.710893	\N
574	2016-12-05 14:31:45.860781	2016-12-05 14:31:45.860781	\N
601	2016-12-05 14:33:49.950289	2016-12-05 14:33:49.950289	\N
575	2016-12-05 14:32:19.456188	2016-12-05 14:32:19.45819	\N
576	2016-12-05 14:32:21.745649	2016-12-05 14:32:21.745649	\N
577	2016-12-05 14:32:24.019059	2016-12-05 14:32:24.019059	\N
578	2016-12-05 14:32:26.276995	2016-12-05 14:32:26.276995	\N
579	2016-12-05 14:32:44.459778	2016-12-05 14:32:44.459778	\N
580	2016-12-05 14:32:46.627939	2016-12-05 14:32:46.627939	\N
602	2016-12-05 14:33:51.63991	2016-12-05 14:33:51.63991	\N
581	2016-12-05 14:32:52.115017	2016-12-05 14:32:52.11652	\N
582	2016-12-05 14:32:54.27234	2016-12-05 14:32:54.27234	\N
583	2016-12-05 14:32:56.486436	2016-12-05 14:32:56.486436	\N
584	2016-12-05 14:33:01.409806	2016-12-05 14:33:01.409806	\N
585	2016-12-05 14:33:05.222377	2016-12-05 14:33:05.222377	\N
586	2016-12-05 14:33:09.586763	2016-12-05 14:33:09.586763	\N
587	2016-12-05 14:33:13.461886	2016-12-05 14:33:13.461886	\N
588	2016-12-05 14:33:15.097185	2016-12-05 14:33:15.097185	\N
589	2016-12-05 14:33:18.128921	2016-12-05 14:33:18.128921	\N
614	2016-12-05 14:34:25.522499	2016-12-05 14:34:25.522499	\N
603	2016-12-05 14:33:53.80119	2016-12-05 14:33:53.802689	\N
604	2016-12-05 14:34:01.689854	2016-12-05 14:34:01.689854	\N
605	2016-12-05 14:34:02.802367	2016-12-05 14:34:02.802367	\N
606	2016-12-05 14:34:04.419533	2016-12-05 14:34:04.419533	\N
607	2016-12-05 14:34:05.649261	2016-12-05 14:34:05.649261	\N
608	2016-12-05 14:34:07.810284	2016-12-05 14:34:07.810284	\N
609	2016-12-05 14:34:12.747888	2016-12-05 14:34:12.747888	\N
610	2016-12-05 14:34:16.476435	2016-12-05 14:34:16.476435	\N
611	2016-12-05 14:34:17.683165	2016-12-05 14:34:17.683165	\N
612	2016-12-05 14:34:21.152343	2016-12-05 14:34:21.152343	\N
615	2016-12-05 14:34:28.375896	2016-12-05 14:34:28.375896	\N
616	2016-12-05 14:34:29.463427	2016-12-05 14:34:29.463427	\N
617	2016-12-05 14:34:31.766135	2016-12-05 14:34:31.766135	\N
618	2016-12-05 14:34:32.859777	2016-12-05 14:34:32.859777	\N
619	2016-12-05 14:34:35.036249	2016-12-05 14:34:35.036249	\N
620	2016-12-05 14:34:36.135934	2016-12-05 14:34:36.135934	\N
621	2016-12-05 14:34:38.874721	2016-12-05 14:34:38.874721	\N
622	2016-12-05 14:34:43.578606	2016-12-05 14:34:43.578606	\N
623	2016-12-05 14:34:47.433313	2016-12-05 14:34:47.433313	\N
624	2016-12-05 14:34:49.588796	2016-12-05 14:34:49.588796	\N
761	2016-12-05 14:42:32.507035	2016-12-05 14:42:32.508536	\N
749	2016-12-05 14:42:01.798098	2016-12-05 14:42:01.799109	\N
750	2016-12-05 14:42:02.929976	2016-12-05 14:42:02.929976	\N
751	2016-12-05 14:42:05.631549	2016-12-05 14:42:05.631549	\N
752	2016-12-05 14:42:10.667062	2016-12-05 14:42:10.667062	\N
625	2016-12-05 14:34:52.453664	2016-12-05 14:34:54.08575	\N
626	2016-12-05 14:34:56.818278	2016-12-05 14:34:56.818278	\N
627	2016-12-05 14:34:57.910879	2016-12-05 14:34:57.910879	\N
628	2016-12-05 14:34:59.57472	2016-12-05 14:34:59.57472	\N
629	2016-12-05 14:35:01.739004	2016-12-05 14:35:01.739004	\N
630	2016-12-05 14:35:04.496521	2016-12-05 14:35:04.496521	\N
631	2016-12-05 14:35:07.974654	2016-12-05 14:35:07.974654	\N
632	2016-12-05 14:35:11.802738	2016-12-05 14:35:11.802738	\N
633	2016-12-05 14:35:14.112725	2016-12-05 14:35:14.112725	\N
634	2016-12-05 14:35:16.402024	2016-12-05 14:35:16.402024	\N
635	2016-12-05 14:35:18.698635	2016-12-05 14:35:18.698635	\N
636	2016-12-05 14:35:20.897339	2016-12-05 14:35:20.897339	\N
637	2016-12-05 14:35:24.409144	2016-12-05 14:35:24.410645	\N
638	2016-12-05 14:35:26.577995	2016-12-05 14:35:26.577995	\N
639	2016-12-05 14:35:29.638467	2016-12-05 14:35:29.638467	\N
640	2016-12-05 14:35:31.409886	2016-12-05 14:35:31.409886	\N
641	2016-12-05 14:35:33.689918	2016-12-05 14:35:33.689918	\N
642	2016-12-05 14:35:35.982356	2016-12-05 14:35:35.982356	\N
643	2016-12-05 14:35:37.07722	2016-12-05 14:35:37.07722	\N
644	2016-12-05 14:35:40.576267	2016-12-05 14:35:40.576267	\N
645	2016-12-05 14:35:42.984827	2016-12-05 14:35:42.984827	\N
646	2016-12-05 14:35:44.190347	2016-12-05 14:35:44.190347	\N
647	2016-12-05 14:35:46.933574	2016-12-05 14:35:46.933574	\N
648	2016-12-05 14:35:48.586264	2016-12-05 14:35:48.586264	\N
649	2016-12-05 14:35:50.757156	2016-12-05 14:35:50.757156	\N
650	2016-12-05 14:35:54.373915	2016-12-05 14:35:54.373915	\N
753	2016-12-05 14:42:13.367423	2016-12-05 14:42:13.367423	\N
651	2016-12-05 14:35:56.760405	2016-12-05 14:35:56.761907	\N
652	2016-12-05 14:36:03.304643	2016-12-05 14:36:03.304643	\N
653	2016-12-05 14:36:06.577674	2016-12-05 14:36:06.577674	\N
654	2016-12-05 14:36:07.684149	2016-12-05 14:36:07.684149	\N
655	2016-12-05 14:36:09.341855	2016-12-05 14:36:09.341855	\N
656	2016-12-05 14:36:12.035659	2016-12-05 14:36:12.035659	\N
657	2016-12-05 14:36:14.241385	2016-12-05 14:36:14.241385	\N
658	2016-12-05 14:36:18.06488	2016-12-05 14:36:18.06488	\N
659	2016-12-05 14:36:21.354878	2016-12-05 14:36:21.354878	\N
660	2016-12-05 14:36:23.539641	2016-12-05 14:36:23.539641	\N
754	2016-12-05 14:42:15.573102	2016-12-05 14:42:15.573102	\N
661	2016-12-05 14:36:27.379518	2016-12-05 14:36:27.381084	\N
662	2016-12-05 14:36:29.003145	2016-12-05 14:36:29.003145	\N
663	2016-12-05 14:36:31.170655	2016-12-05 14:36:31.170655	\N
664	2016-12-05 14:36:32.288676	2016-12-05 14:36:32.288676	\N
665	2016-12-05 14:36:34.473147	2016-12-05 14:36:34.473147	\N
666	2016-12-05 14:36:38.861961	2016-12-05 14:36:38.861961	\N
667	2016-12-05 14:36:41.009794	2016-12-05 14:36:41.009794	\N
668	2016-12-05 14:36:42.681089	2016-12-05 14:36:42.681089	\N
669	2016-12-05 14:36:44.321215	2016-12-05 14:36:44.321215	\N
670	2016-12-05 14:36:46.519898	2016-12-05 14:36:46.519898	\N
671	2016-12-05 14:36:48.167547	2016-12-05 14:36:48.167547	\N
672	2016-12-05 14:36:49.25079	2016-12-05 14:36:49.25079	\N
673	2016-12-05 14:36:50.898501	2016-12-05 14:36:50.898501	\N
674	2016-12-05 14:36:54.154556	2016-12-05 14:36:54.154556	\N
675	2016-12-05 14:36:55.828326	2016-12-05 14:36:55.828326	\N
676	2016-12-05 14:36:57.451682	2016-12-05 14:36:57.453183	\N
677	2016-12-05 14:36:59.083594	2016-12-05 14:36:59.083594	\N
678	2016-12-05 14:37:00.76077	2016-12-05 14:37:00.76077	\N
679	2016-12-05 14:37:02.383588	2016-12-05 14:37:02.383588	\N
680	2016-12-05 14:37:04.679841	2016-12-05 14:37:04.679841	\N
681	2016-12-05 14:37:07.975651	2016-12-05 14:37:07.975651	\N
682	2016-12-05 14:37:10.155357	2016-12-05 14:37:10.155357	\N
683	2016-12-05 14:37:14.529521	2016-12-05 14:37:14.529521	\N
684	2016-12-05 14:37:16.738151	2016-12-05 14:37:16.738151	\N
685	2016-12-05 14:37:17.790274	2016-12-05 14:37:17.790274	\N
686	2016-12-05 14:37:20.580832	2016-12-05 14:37:20.580832	\N
687	2016-12-05 14:37:22.158101	2016-12-05 14:37:22.158101	\N
688	2016-12-05 14:37:24.351236	2016-12-05 14:37:24.351236	\N
689	2016-12-05 14:37:25.994365	2016-12-05 14:37:25.994365	\N
690	2016-12-05 14:37:27.121049	2016-12-05 14:37:27.121049	\N
691	2016-12-05 14:37:28.768125	2016-12-05 14:37:28.769627	\N
692	2016-12-05 14:37:32.55792	2016-12-05 14:37:32.55792	\N
693	2016-12-05 14:37:36.980892	2016-12-05 14:37:36.980892	\N
694	2016-12-05 14:37:38.580515	2016-12-05 14:37:38.580515	\N
695	2016-12-05 14:37:40.215758	2016-12-05 14:37:40.215758	\N
696	2016-12-05 14:37:41.893547	2016-12-05 14:37:41.893547	\N
697	2016-12-05 14:37:45.686422	2016-12-05 14:37:45.686422	\N
698	2016-12-05 14:37:48.130101	2016-12-05 14:37:48.130101	\N
699	2016-12-05 14:37:49.728098	2016-12-05 14:37:49.728098	\N
700	2016-12-05 14:37:51.379226	2016-12-05 14:37:51.379226	\N
701	2016-12-05 14:37:53.042832	2016-12-05 14:37:53.042832	\N
702	2016-12-05 14:37:54.683229	2016-12-05 14:37:54.683229	\N
703	2016-12-05 14:37:55.752912	2016-12-05 14:37:55.752912	\N
704	2016-12-05 14:37:58.073609	2016-12-05 14:37:58.073609	\N
755	2016-12-05 14:42:16.672393	2016-12-05 14:42:16.672393	\N
705	2016-12-05 14:38:00.269881	2016-12-05 14:38:00.271065	\N
706	2016-12-05 14:38:02.454473	2016-12-05 14:38:02.454473	\N
707	2016-12-05 14:38:07.03646	2016-12-05 14:38:07.03646	\N
708	2016-12-05 14:38:08.147155	2016-12-05 14:38:08.147155	\N
709	2016-12-05 14:38:09.203301	2016-12-05 14:38:09.203301	\N
710	2016-12-05 14:38:13.58229	2016-12-05 14:38:13.58229	\N
711	2016-12-05 14:38:16.330213	2016-12-05 14:38:16.330213	\N
712	2016-12-05 14:38:17.407579	2016-12-05 14:38:17.407579	\N
713	2016-12-05 14:38:21.240906	2016-12-05 14:38:21.240906	\N
714	2016-12-05 14:38:23.466011	2016-12-05 14:38:23.466011	\N
715	2016-12-05 14:38:24.557386	2016-12-05 14:38:24.557386	\N
716	2016-12-05 14:38:27.807781	2016-12-05 14:38:27.807781	\N
717	2016-12-05 14:38:29.113268	2016-12-05 14:38:29.113268	\N
718	2016-12-05 14:38:30.894724	2016-12-05 14:38:30.896255	\N
719	2016-12-05 14:38:34.910233	2016-12-05 14:38:34.910233	\N
720	2016-12-05 14:38:38.314496	2016-12-05 14:38:38.314496	\N
721	2016-12-05 14:38:39.94586	2016-12-05 14:38:39.94586	\N
722	2016-12-05 14:38:43.279754	2016-12-05 14:38:43.279754	\N
723	2016-12-05 14:38:44.358748	2016-12-05 14:38:44.358748	\N
724	2016-12-05 14:38:46.509706	2016-12-05 14:38:46.509706	\N
725	2016-12-05 14:38:47.634302	2016-12-05 14:38:47.634302	\N
726	2016-12-05 14:38:48.699519	2016-12-05 14:38:48.699519	\N
756	2016-12-05 14:42:17.780257	2016-12-05 14:42:17.780257	\N
757	2016-12-05 14:42:20.494163	2016-12-05 14:42:20.494163	\N
727	2016-12-05 14:39:19.948463	2016-12-05 14:39:19.949964	\N
758	2016-12-05 14:42:26.521684	2016-12-05 14:42:26.521684	\N
759	2016-12-05 14:42:28.710655	2016-12-05 14:42:28.710655	\N
728	2016-12-05 14:40:58.985981	2016-12-05 14:40:58.987635	\N
729	2016-12-05 14:41:01.718161	2016-12-05 14:41:01.718161	\N
730	2016-12-05 14:41:03.384788	2016-12-05 14:41:03.384788	\N
731	2016-12-05 14:41:04.463505	2016-12-05 14:41:04.463505	\N
732	2016-12-05 14:41:06.125106	2016-12-05 14:41:06.125106	\N
733	2016-12-05 14:41:11.139331	2016-12-05 14:41:11.139331	\N
734	2016-12-05 14:41:13.878509	2016-12-05 14:41:13.878509	\N
735	2016-12-05 14:41:15.513636	2016-12-05 14:41:15.513636	\N
760	2016-12-05 14:42:30.870362	2016-12-05 14:42:30.870362	\N
736	2016-12-05 14:41:31.68072	2016-12-05 14:41:31.682235	\N
737	2016-12-05 14:41:32.775541	2016-12-05 14:41:32.775541	\N
738	2016-12-05 14:41:35.533689	2016-12-05 14:41:35.533689	\N
739	2016-12-05 14:41:38.847762	2016-12-05 14:41:38.847762	\N
740	2016-12-05 14:41:40.236717	2016-12-05 14:41:40.236717	\N
741	2016-12-05 14:41:42.658059	2016-12-05 14:41:42.658059	\N
742	2016-12-05 14:41:44.268473	2016-12-05 14:41:44.268473	\N
743	2016-12-05 14:41:47.583042	2016-12-05 14:41:47.583042	\N
744	2016-12-05 14:41:51.397281	2016-12-05 14:41:51.397281	\N
745	2016-12-05 14:41:53.581136	2016-12-05 14:41:53.581136	\N
746	2016-12-05 14:41:56.344889	2016-12-05 14:41:56.344889	\N
747	2016-12-05 14:41:57.981089	2016-12-05 14:41:57.981089	\N
748	2016-12-05 14:42:00.127658	2016-12-05 14:42:00.127658	\N
762	2016-12-05 14:42:33.597467	2016-12-05 14:42:33.597467	\N
763	2016-12-05 14:42:35.236659	2016-12-05 14:42:35.236659	\N
764	2016-12-05 14:42:37.448865	2016-12-05 14:42:37.448865	\N
765	2016-12-05 14:42:39.116711	2016-12-05 14:42:39.116711	\N
766	2016-12-05 14:42:40.163259	2016-12-05 14:42:40.163259	\N
767	2016-12-05 14:42:45.109328	2016-12-05 14:42:45.109328	\N
768	2016-12-05 14:42:46.766678	2016-12-05 14:42:46.766678	\N
769	2016-12-05 14:42:50.020753	2016-12-05 14:42:50.020753	\N
770	2016-12-05 14:42:52.192536	2016-12-05 14:42:52.192536	\N
771	2016-12-05 14:42:55.500919	2016-12-05 14:42:55.500919	\N
772	2016-12-05 14:42:57.123427	2016-12-05 14:42:57.123427	\N
773	2016-12-05 14:43:00.400261	2016-12-05 14:43:00.400261	\N
774	2016-12-05 14:43:01.523476	2016-12-05 14:43:02.585044	\N
775	2016-12-05 14:43:03.69308	2016-12-05 14:43:03.69308	\N
776	2016-12-05 14:43:05.366447	2016-12-05 14:43:05.366447	\N
777	2016-12-05 14:43:07.536269	2016-12-05 14:43:07.536269	\N
778	2016-12-05 14:43:09.146287	2016-12-05 14:43:09.146287	\N
779	2016-12-05 14:43:13.518938	2016-12-05 14:43:13.518938	\N
780	2016-12-05 14:43:15.182246	2016-12-05 14:43:15.182246	\N
781	2016-12-05 14:43:17.911751	2016-12-05 14:43:17.911751	\N
782	2016-12-05 14:43:19.539251	2016-12-05 14:43:19.539251	\N
783	2016-12-05 14:43:23.943058	2016-12-05 14:43:23.943058	\N
784	2016-12-05 14:43:26.138367	2016-12-05 14:43:26.138367	\N
785	2016-12-05 14:43:28.299966	2016-12-05 14:43:28.299966	\N
786	2016-12-05 14:43:29.976116	2016-12-05 14:43:29.976116	\N
890	2016-12-05 20:23:31.678375	2016-12-05 20:23:31.679384	\N
891	2016-12-05 20:23:33.862636	2016-12-05 20:23:33.862636	\N
787	2016-12-05 14:43:32.297253	2016-12-05 14:43:32.814129	\N
788	2016-12-05 14:43:35.017281	2016-12-05 14:43:35.017281	\N
789	2016-12-05 14:43:36.642136	2016-12-05 14:43:36.642136	\N
790	2016-12-05 14:43:37.696346	2016-12-05 14:43:37.696346	\N
791	2016-12-05 14:43:38.793895	2016-12-05 14:43:38.793895	\N
792	2016-12-05 14:43:48.967728	2016-12-05 14:43:48.967728	\N
793	2016-12-05 14:43:50.64289	2016-12-05 14:43:50.64289	\N
794	2016-12-05 14:43:53.461335	2016-12-05 14:43:53.461335	\N
795	2016-12-05 14:43:55.657015	2016-12-05 14:43:55.657015	\N
796	2016-12-05 14:43:57.834957	2016-12-05 14:43:57.834957	\N
892	2016-12-05 20:23:36.582683	2016-12-05 20:23:36.582683	\N
893	2016-12-05 20:23:39.330323	2016-12-05 20:23:39.330323	\N
894	2016-12-05 20:23:40.433197	2016-12-05 20:23:40.433197	\N
797	2016-12-05 14:44:01.773593	2016-12-05 14:44:02.897362	\N
798	2016-12-05 14:44:05.199303	2016-12-05 14:44:05.199303	\N
799	2016-12-05 14:44:06.817039	2016-12-05 14:44:06.817039	\N
800	2016-12-05 14:44:11.552474	2016-12-05 14:44:11.552474	\N
801	2016-12-05 14:44:15.913572	2016-12-05 14:44:15.913572	\N
802	2016-12-05 14:44:19.562034	2016-12-05 14:44:19.562034	\N
803	2016-12-05 14:44:25.852854	2016-12-05 14:44:25.852854	\N
895	2016-12-05 20:23:43.674432	2016-12-05 20:23:43.674432	\N
896	2016-12-05 20:23:45.345645	2016-12-05 20:23:45.345645	\N
897	2016-12-05 20:23:47.553	2016-12-05 20:23:47.553	\N
898	2016-12-05 20:23:48.62933	2016-12-05 20:23:48.62933	\N
899	2016-12-05 20:23:50.259833	2016-12-05 20:23:50.259833	\N
900	2016-12-05 20:23:53.022162	2016-12-05 20:23:53.022162	\N
901	2016-12-05 20:23:54.646122	2016-12-05 20:23:54.646122	\N
902	2016-12-05 20:23:57.932934	2016-12-05 20:23:57.932934	\N
903	2016-12-05 20:24:00.085188	2016-12-05 20:24:00.085188	\N
804	2016-12-05 14:44:27.512599	2016-12-05 14:44:33.533054	\N
805	2016-12-05 14:44:35.803908	2016-12-05 14:44:35.803908	\N
806	2016-12-05 14:44:38.345213	2016-12-05 14:44:38.345213	\N
807	2016-12-05 14:44:40.860858	2016-12-05 14:44:40.860858	\N
808	2016-12-05 14:44:44.45945	2016-12-05 14:44:44.45945	\N
809	2016-12-05 14:44:48.835132	2016-12-05 14:44:48.835132	\N
810	2016-12-05 14:44:53.987433	2016-12-05 14:44:53.987433	\N
811	2016-12-05 14:44:59.514419	2016-12-05 14:44:59.514419	\N
812	2016-12-05 14:45:05.830003	2016-12-05 14:45:05.831514	\N
813	2016-12-05 14:45:10.567389	2016-12-05 14:45:10.567389	\N
814	2016-12-05 14:45:13.191175	2016-12-05 14:45:13.191175	\N
815	2016-12-05 14:45:15.027511	2016-12-05 14:45:15.027511	\N
816	2016-12-05 14:45:18.745769	2016-12-05 14:45:18.745769	\N
817	2016-12-05 14:45:24.232873	2016-12-05 14:45:24.232873	\N
818	2016-12-05 14:45:27.726482	2016-12-05 14:45:27.726482	\N
819	2016-12-05 14:45:29.476681	2016-12-05 14:45:29.476681	\N
820	2016-12-05 14:45:32.129206	2016-12-05 14:45:32.129206	\N
821	2016-12-05 14:45:34.013098	2016-12-05 14:45:34.013098	\N
904	2016-12-05 20:24:02.282837	2016-12-05 20:24:02.284833	\N
905	2016-12-05 20:24:03.928544	2016-12-05 20:24:03.928544	\N
822	2016-12-05 14:45:38.665839	2016-12-05 14:45:38.667324	\N
823	2016-12-05 14:45:54.915791	2016-12-05 14:45:54.915791	\N
824	2016-12-05 14:46:01.075021	2016-12-05 14:46:01.075021	\N
825	2016-12-05 14:46:03.048254	2016-12-05 14:46:03.048254	\N
906	2016-12-05 20:24:08.327426	2016-12-05 20:24:08.327426	\N
907	2016-12-05 20:24:12.117257	2016-12-05 20:24:12.117257	\N
908	2016-12-05 20:24:14.332612	2016-12-05 20:24:14.332612	\N
909	2016-12-05 20:24:17.093719	2016-12-05 20:24:17.093719	\N
910	2016-12-05 20:24:18.683303	2016-12-05 20:24:18.683303	\N
911	2016-12-05 20:24:19.799295	2016-12-05 20:24:19.799295	\N
826	2016-12-05 14:46:04.995771	2016-12-05 14:46:08.747427	\N
827	2016-12-05 14:46:14.737169	2016-12-05 14:46:14.737169	\N
828	2016-12-05 14:46:20.556804	2016-12-05 14:46:20.556804	\N
829	2016-12-05 14:46:23.294024	2016-12-05 14:46:23.294024	\N
830	2016-12-05 14:46:26.117035	2016-12-05 14:46:26.117035	\N
831	2016-12-05 14:46:29.133616	2016-12-05 14:46:29.133616	\N
832	2016-12-05 14:46:32.794017	2016-12-05 14:46:32.794017	\N
833	2016-12-05 14:46:34.695142	2016-12-05 14:46:34.695142	\N
912	2016-12-05 20:24:20.898759	2016-12-05 20:24:20.898759	\N
834	2016-12-05 14:46:42.966048	2016-12-05 14:46:42.967617	\N
835	2016-12-05 14:46:47.025441	2016-12-05 14:46:47.025441	\N
836	2016-12-05 14:46:54.809775	2016-12-05 14:46:54.809775	\N
837	2016-12-05 14:46:57.014014	2016-12-05 14:46:57.014014	\N
838	2016-12-05 14:47:01.56922	2016-12-05 14:47:01.56922	\N
839	2016-12-05 14:47:03.738645	2016-12-05 14:47:03.738645	\N
840	2016-12-05 14:47:11.934026	2016-12-05 14:47:11.934026	\N
913	2016-12-05 20:24:21.988459	2016-12-05 20:24:21.988459	\N
841	2016-12-05 14:47:15.062481	2016-12-05 14:47:15.063974	\N
842	2016-12-05 14:47:17.176243	2016-12-05 14:47:17.176243	\N
843	2016-12-05 14:47:21.097029	2016-12-05 14:47:21.097029	\N
844	2016-12-05 14:47:28.495357	2016-12-05 14:47:28.495357	\N
845	2016-12-05 14:47:31.523874	2016-12-05 14:47:31.523874	\N
846	2016-12-05 14:47:39.944882	2016-12-05 14:47:39.944882	\N
847	2016-12-05 14:47:42.940727	2016-12-05 14:47:42.940727	\N
914	2016-12-05 20:24:24.72005	2016-12-05 20:24:24.72005	\N
848	2016-12-05 14:47:46.868041	2016-12-05 14:47:46.86955	\N
849	2016-12-05 14:47:52.577535	2016-12-05 14:47:52.577535	\N
850	2016-12-05 14:47:55.641435	2016-12-05 14:47:55.641435	\N
851	2016-12-05 14:47:57.362479	2016-12-05 14:47:57.362479	\N
852	2016-12-05 14:47:59.277078	2016-12-05 14:47:59.277078	\N
853	2016-12-05 20:22:11.036328	2016-12-05 20:22:11.036328	\N
854	2016-12-05 20:22:13.2484	2016-12-05 20:22:13.2484	\N
855	2016-12-05 20:22:15.982328	2016-12-05 20:22:15.982328	\N
856	2016-12-05 20:22:17.628215	2016-12-05 20:22:17.628215	\N
857	2016-12-05 20:22:19.276414	2016-12-05 20:22:19.276414	\N
858	2016-12-05 20:22:20.365011	2016-12-05 20:22:20.365011	\N
859	2016-12-05 20:22:23.10201	2016-12-05 20:22:23.10201	\N
860	2016-12-05 20:22:24.749302	2016-12-05 20:22:24.749302	\N
861	2016-12-05 20:22:29.144476	2016-12-05 20:22:29.144476	\N
862	2016-12-05 20:22:30.77558	2016-12-05 20:22:30.77656	\N
863	2016-12-05 20:22:34.14398	2016-12-05 20:22:34.14398	\N
864	2016-12-05 20:22:35.254186	2016-12-05 20:22:35.254186	\N
865	2016-12-05 20:22:37.971809	2016-12-05 20:22:37.971809	\N
866	2016-12-05 20:22:39.078981	2016-12-05 20:22:39.078981	\N
867	2016-12-05 20:22:41.243444	2016-12-05 20:22:41.243444	\N
868	2016-12-05 20:22:43.451941	2016-12-05 20:22:43.451941	\N
869	2016-12-05 20:22:47.274416	2016-12-05 20:22:47.274416	\N
870	2016-12-05 20:22:48.379985	2016-12-05 20:22:48.379985	\N
871	2016-12-05 20:22:51.745751	2016-12-05 20:22:51.745751	\N
872	2016-12-05 20:22:53.391791	2016-12-05 20:22:53.391791	\N
873	2016-12-05 20:22:56.145877	2016-12-05 20:22:56.145877	\N
874	2016-12-05 20:22:57.230447	2016-12-05 20:22:57.230447	\N
875	2016-12-05 20:22:59.416314	2016-12-05 20:22:59.416314	\N
876	2016-12-05 20:23:01.604158	2016-12-05 20:23:01.605169	\N
877	2016-12-05 20:23:02.695934	2016-12-05 20:23:02.695934	\N
878	2016-12-05 20:23:04.333549	2016-12-05 20:23:04.333549	\N
879	2016-12-05 20:23:06.524069	2016-12-05 20:23:06.524069	\N
880	2016-12-05 20:23:08.14943	2016-12-05 20:23:08.14943	\N
881	2016-12-05 20:23:10.358081	2016-12-05 20:23:10.358081	\N
882	2016-12-05 20:23:11.454965	2016-12-05 20:23:11.454965	\N
883	2016-12-05 20:23:13.628955	2016-12-05 20:23:13.628955	\N
884	2016-12-05 20:23:15.270573	2016-12-05 20:23:15.270573	\N
885	2016-12-05 20:23:18.011598	2016-12-05 20:23:18.011598	\N
886	2016-12-05 20:23:20.189266	2016-12-05 20:23:20.189266	\N
887	2016-12-05 20:23:24.001194	2016-12-05 20:23:24.001194	\N
888	2016-12-05 20:23:25.113753	2016-12-05 20:23:25.113753	\N
889	2016-12-05 20:23:27.853299	2016-12-05 20:23:27.853299	\N
915	2016-12-05 20:24:26.949272	2016-12-05 20:24:26.949272	\N
916	2016-12-05 20:24:28.594414	2016-12-05 20:24:28.594414	\N
917	2016-12-05 20:24:30.758712	2016-12-05 20:24:30.758712	\N
918	2016-12-05 20:24:32.398573	2016-12-05 20:24:32.399573	\N
919	2016-12-05 20:24:37.53934	2016-12-05 20:24:37.53934	\N
920	2016-12-05 20:24:38.629535	2016-12-05 20:24:38.629535	\N
921	2016-12-05 20:24:39.723069	2016-12-05 20:24:39.723069	\N
922	2016-12-05 20:24:41.615267	2016-12-05 20:24:41.615267	\N
923	2016-12-05 20:24:42.675028	2016-12-05 20:24:42.675028	\N
924	2016-12-05 20:24:47.826304	2016-12-05 20:24:47.826304	\N
925	2016-12-05 20:24:50.893883	2016-12-05 20:24:50.893883	\N
926	2016-12-05 20:24:53.209405	2016-12-05 20:24:53.209405	\N
927	2016-12-05 20:24:54.40617	2016-12-05 20:24:54.40617	\N
928	2016-12-05 20:24:56.591242	2016-12-05 20:24:56.591242	\N
929	2016-12-05 20:24:58.880169	2016-12-05 20:24:58.880169	\N
930	2016-12-05 20:25:02.1617	2016-12-05 20:25:02.716464	\N
931	2016-12-05 20:25:03.801523	2016-12-05 20:25:03.801523	\N
932	2016-12-05 20:25:05.550396	2016-12-05 20:25:05.550396	\N
933	2016-12-05 20:25:08.28519	2016-12-05 20:25:08.28519	\N
934	2016-12-05 20:25:10.481659	2016-12-05 20:25:10.481659	\N
935	2016-12-05 20:25:12.122556	2016-12-05 20:25:12.122556	\N
936	2016-12-05 20:25:17.149408	2016-12-05 20:25:17.149408	\N
937	2016-12-05 20:25:19.336608	2016-12-05 20:25:19.336608	\N
938	2016-12-05 20:25:20.428405	2016-12-05 20:25:20.428405	\N
939	2016-12-05 20:25:21.533714	2016-12-05 20:25:21.533714	\N
940	2016-12-05 20:25:24.482386	2016-12-05 20:25:24.482386	\N
941	2016-12-05 20:25:26.675937	2016-12-05 20:25:26.675937	\N
942	2016-12-05 20:25:27.880898	2016-12-05 20:25:27.880898	\N
943	2016-12-05 20:25:29.515333	2016-12-05 20:25:29.515333	\N
944	2016-12-05 20:25:30.829356	2016-12-05 20:25:30.829356	\N
945	2016-12-05 20:25:34.236351	2016-12-05 20:25:34.238353	\N
946	2016-12-05 20:25:41.022288	2016-12-05 20:25:41.022288	\N
947	2016-12-05 20:25:45.045724	2016-12-05 20:25:45.045724	\N
948	2016-12-05 20:25:49.131534	2016-12-05 20:25:49.131534	\N
949	2016-12-05 20:25:51.581321	2016-12-05 20:25:51.581321	\N
950	2016-12-05 20:25:53.248717	2016-12-05 20:25:53.248717	\N
951	2016-12-05 20:25:57.966936	2016-12-05 20:25:57.966936	\N
952	2016-12-05 20:26:01.834771	2016-12-05 20:26:01.834771	\N
953	2016-12-05 20:26:05.069619	2016-12-05 20:26:05.07162	\N
954	2016-12-05 20:26:07.344022	2016-12-05 20:26:07.344022	\N
955	2016-12-05 20:26:11.166985	2016-12-05 20:26:11.166985	\N
956	2016-12-05 20:26:17.369695	2016-12-05 20:26:17.369695	\N
957	2016-12-05 20:26:19.816816	2016-12-05 20:26:19.816816	\N
958	2016-12-05 20:26:22.111227	2016-12-05 20:26:22.111227	\N
959	2016-12-05 20:26:24.841759	2016-12-05 20:26:24.841759	\N
960	2016-12-05 20:26:28.23182	2016-12-05 20:26:28.23182	\N
961	2016-12-05 20:26:29.864273	2016-12-05 20:26:29.864273	\N
962	2016-12-05 20:26:31.535525	2016-12-05 20:26:31.535525	\N
1024	2016-12-08 09:00:01.430487	2016-12-08 10:00:00.439982	Proces 3
963	2016-12-05 20:26:34.934544	2016-12-05 20:26:35.453021	\N
964	2016-12-05 20:26:37.662031	2016-12-05 20:26:37.662031	\N
965	2016-12-05 20:26:39.940771	2016-12-05 20:26:39.940771	\N
966	2016-12-05 20:26:42.955897	2016-12-05 20:26:42.955897	\N
967	2016-12-05 20:26:53.298289	2016-12-05 20:26:53.298289	\N
968	2016-12-05 20:26:55.93119	2016-12-05 20:26:55.93119	\N
969	2016-12-05 20:26:59.938264	2016-12-05 20:26:59.938264	\N
970	2016-12-05 20:27:06.87196	2016-12-05 20:27:06.873462	\N
971	2016-12-05 20:27:08.487232	2016-12-05 20:27:08.487232	\N
972	2016-12-05 20:27:11.840943	2016-12-05 20:27:11.840943	\N
973	2016-12-05 20:27:13.498841	2016-12-05 20:27:13.498841	\N
974	2016-12-05 20:27:19.702871	2016-12-05 20:27:19.702871	\N
975	2016-12-05 20:27:25.093982	2016-12-05 20:27:25.093982	\N
976	2016-12-05 20:27:26.754451	2016-12-05 20:27:26.754451	\N
977	2016-12-05 20:27:29.253834	2016-12-05 20:27:29.253834	\N
978	2016-12-05 20:27:30.912006	2016-12-05 20:27:30.912006	\N
979	2016-12-05 20:27:32.62722	2016-12-05 20:27:32.62722	\N
980	2016-12-05 20:27:35.435833	2016-12-05 20:27:35.435833	\N
981	2016-12-05 20:27:38.39492	2016-12-05 20:27:38.395922	\N
982	2016-12-05 20:27:41.991255	2016-12-05 20:27:41.991255	\N
983	2016-12-05 20:27:47.315138	2016-12-05 20:27:47.315138	\N
984	2016-12-05 20:27:50.291498	2016-12-05 20:27:50.291498	\N
985	2016-12-05 20:27:53.226561	2016-12-05 20:27:53.226561	\N
986	2016-12-05 20:27:55.025856	2016-12-05 20:27:55.025856	\N
987	2016-12-05 20:28:01.291794	2016-12-05 20:28:01.291794	\N
988	2016-12-05 20:28:04.128195	2016-12-05 20:28:04.128195	\N
1026	2016-12-08 11:00:01.67262	2016-12-08 12:00:00.67262	Proces 1
1025	2016-12-08 10:00:01.457171	2016-12-08 11:00:00.457171	Proces 2
989	2016-12-05 20:28:10.684477	2016-12-05 20:28:10.686478	\N
990	2016-12-05 20:28:12.262394	2016-12-05 20:28:12.262394	\N
991	2016-12-05 20:28:13.991002	2016-12-05 20:28:13.991002	\N
992	2016-12-05 20:28:15.626521	2016-12-05 20:28:15.626521	\N
993	2016-12-05 20:28:17.256177	2016-12-05 20:28:17.256177	\N
994	2016-12-05 20:28:20.442271	2016-12-05 20:28:20.442271	\N
995	2016-12-05 20:28:23.161634	2016-12-05 20:28:23.161634	\N
996	2016-12-05 20:28:24.951294	2016-12-05 20:28:24.951294	\N
997	2016-12-05 20:28:29.526013	2016-12-05 20:28:29.526013	\N
998	2016-12-05 20:28:35.607774	2016-12-05 20:28:35.607774	\N
999	2016-12-05 20:28:39.424512	2016-12-05 20:28:39.424512	\N
1000	2016-12-05 20:28:40.474313	2016-12-05 20:28:40.474313	\N
1001	2016-12-05 20:28:41.536283	2016-12-05 20:28:41.537279	\N
1002	2016-12-05 20:28:42.626758	2016-12-05 20:28:42.626758	\N
1003	2016-12-05 20:28:43.723285	2016-12-05 20:28:43.723285	\N
1004	2016-12-05 20:28:44.811084	2016-12-05 20:28:44.811084	\N
1005	2016-12-05 20:28:45.916507	2016-12-05 20:28:45.916507	\N
1006	2016-12-05 20:28:46.989102	2016-12-05 20:28:46.989102	\N
1007	2016-12-05 20:28:49.931235	2016-12-05 20:28:49.931235	\N
1008	2016-12-05 20:28:51.811514	2016-12-05 20:28:51.811514	\N
1009	2016-12-05 20:28:55.659624	2016-12-05 20:28:55.659624	\N
1010	2016-12-05 20:28:57.81513	2016-12-05 20:28:57.81513	\N
1011	2016-12-05 20:28:59.512577	2016-12-05 20:28:59.512577	\N
1012	2016-12-05 20:29:01.689013	2016-12-05 20:29:01.689013	\N
1023	2016-12-08 08:00:01.473095	2016-12-08 09:00:00.926217	Proces 1
1022	2016-12-08 07:00:01.33845	2016-12-08 08:00:00.33845	Proces 2
1013	2016-12-05 06:00:00.331264	2016-12-08 07:00:00.383224	Proces 3
1027	2016-12-09 07:25:50.064185	2016-12-09 07:25:50.064185	None
1028	2016-12-09 07:25:51.721484	2016-12-09 07:25:51.721484	None
\.


--
-- Name: sesions_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('sesions_id_seq', 1028, true);


--
-- Data for Name: stages_gas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY stages_gas (id, mfc1_flow, mfc1_min_flow, mfc1_max_flow, mfc2_flow, mfc2_min_flow, mfc2_max_flow, mfc3_flow, mfc3_min_flow, mfc3_max_flow, vaporaiser_cycle_time, vaporaiser_on_time, setpoint_pressure, max_devation_up, max_devation_down, mfc1_max_devation, mfc1_gas_share, mfc2_max_devation, mfc2_gas_share, mfc3_max_devation, mfc3_gas_share, time_duration, mode_gas, mfc1_active, mfc2_active, mfc3_active, vaporaiser_active, mfc1_id_gas_type, mfc2_id_gas_type, mfc3_id_gas_type, vaporaiser_dosing) FROM stdin;
17	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	00:00:00.73217	0	f	f	f	f	0	0	0	0
21	100	10	300	0	0	0	0	0	0	0	0	0	0	0	0	100	0	0	0	0	00:01:00.785665	1	t	f	f	f	10	0	0	0
22	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	00:00:00.655769	0	f	f	f	f	0	0	0	0
23	0	10	300	0	10	300	0	10	300	0	0	0	0	0	0	0	0	0	0	0	00:00:00.197661	1	f	f	f	f	0	0	0	0
24	0	10	300	0	10	300	0	10	300	0	0	0	0	0	0	0	0	0	0	0	00:02:30	1	f	f	f	t	0	0	0	10
25	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	0	00:00:45.373573	0	f	f	f	t	0	0	0	0
29	0	10	300	0	10	300	0	10	300	0	0	0	0	0	0	0	0	0	0	0	00:00:00.684421	1	f	f	f	f	0	0	0	0
30	0	0	0	0	0	0	0	0	0	0	0	150	20	10	10	20	20	30	45	50	00:05:00.176627	2	t	t	t	f	0	0	0	0
\.


--
-- Name: stages_gas_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('stages_gas_id_seq', 31, true);


--
-- Data for Name: stages_power_supply; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY stages_power_supply (id, setpoint, mode, duration_time, max_devation) FROM stdin;
17	0	1	00:00:00.73217	0
21	650	1	00:01:10.785665	10
22	0	1	00:00:00.655769	0
23	0	1	00:00:00.197661	0
24	0	1	00:00:00.805768	0
25	200	1	00:01:00.373573	10
29	0	1	00:00:00.684421	0
30	0	1	00:00:00.176627	0
\.


--
-- Name: stages_power_supply_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('stages_power_supply_id_seq', 31, true);


--
-- Data for Name: stages_pump; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY stages_pump (id, max_time_pump, setpoint_pressure) FROM stdin;
20	00:10:00.73217	0.00800000038
24	00:00:00.785665	0
25	00:00:00.655769	0
26	00:00:00.197661	0.5
27	00:00:00.805768	0.5
28	00:01:30.373573	0.5
32	00:00:00.684421	0.5
33	00:00:00.176627	0
\.


--
-- Name: stages_pump_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('stages_pump_id_seq', 34, true);


--
-- Data for Name: stages_purge; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY stages_purge (id, "time") FROM stdin;
18	00:00:00.73217
22	00:00:00.785665
23	00:00:45.655769
24	00:00:00.197661
25	00:00:00.805768
26	00:00:30.373573
30	00:00:00.684421
31	00:00:00.176627
\.


--
-- Name: stages_purge_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('stages_purge_id_seq', 32, true);


--
-- Data for Name: stages_vent; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY stages_vent (id, vent_time) FROM stdin;
19	00:00:00.73217
23	00:00:00.785665
24	00:00:00.655769
25	00:00:30
26	00:00:00.805768
27	00:00:00.373573
31	00:00:35
32	00:00:00.176627
\.


--
-- Name: stages_vent_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('stages_vent_id_seq', 33, true);


--
-- Data for Name: subprograms; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY subprograms (id, name, description, id_pump, id_vent, id_purge, id_power_supplay, id_gas, pump_active, gas_active, power_supply_active, purge_active, vent_active) FROM stdin;
19	Subprogram Pump	Opis subprogramu	20	19	18	17	17	t	t	f	f	f
23	Subprogram Gas		24	23	22	21	21	f	t	t	f	f
24	Subprogram Purge		25	24	23	22	22	f	f	f	t	f
25	Subprogram Vent		26	25	24	23	23	f	f	f	f	t
28	Subprogram vaporsier		27	26	25	24	24	f	t	f	f	f
29	Subprogram plasma		28	27	26	25	25	f	f	t	f	f
33	Subprogram 0		32	31	30	29	29	f	f	f	f	t
34	Subprogram Pressure		33	32	31	30	30	f	t	f	f	f
\.


--
-- Name: subprograms_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('subprograms_id_seq', 35, true);


--
-- Data for Name: types_block_account; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY types_block_account (id, name, value) FROM stdin;
1	Temporaily	1
2	Immediately	2
3	Access	3
\.


--
-- Name: types_block_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('types_block_id_seq', 10, false);


--
-- Data for Name: types_privilige; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY types_privilige (id, name, value) FROM stdin;
1	None	0
2	Administrator	1
3	Operator	2
4	Service	3
5	Technican	4
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY users (id, name, surname, login, password, allow_change_psw, id_type_block_account, id_privilige, start_date_block_account, end_date_block_account) FROM stdin;
77	Operator	Operator	operator	operator	f	3	3	2015-02-03	2016-01-01
75	Administrator 1	Administrator 1	Admin 1	admin	f	1	2	2016-11-24	2016-11-30
\.


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('users_id_seq', 77, true);


--
-- Name: errors_txt Errors_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY errors_txt
    ADD CONSTRAINT "Errors_pkey" PRIMARY KEY (id);


--
-- Name: types_block_account Types_Block_Account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY types_block_account
    ADD CONSTRAINT "Types_Block_Account_pkey" PRIMARY KEY (id);


--
-- Name: types_privilige Types_Privilige_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY types_privilige
    ADD CONSTRAINT "Types_Privilige_pkey" PRIMARY KEY (id);


--
-- Name: acquisition_configurations acquisition_configurations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY acquisition_configurations
    ADD CONSTRAINT acquisition_configurations_pkey PRIMARY KEY (id);


--
-- Name: connections_program_subprograms connections_program_subprograms_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY connections_program_subprograms
    ADD CONSTRAINT connections_program_subprograms_pkey PRIMARY KEY (id_program, id_subprogram);


--
-- Name: types_block_account constraint_name; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY types_block_account
    ADD CONSTRAINT constraint_name UNIQUE (name);


--
-- Name: types_block_account constraint_value; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY types_block_account
    ADD CONSTRAINT constraint_value UNIQUE (value);


--
-- Name: types_privilige constraint_value_name; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY types_privilige
    ADD CONSTRAINT constraint_value_name UNIQUE (name);


--
-- Name: types_privilige constraint_value_pr; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY types_privilige
    ADD CONSTRAINT constraint_value_pr UNIQUE (value);


--
-- Name: current_sesion curent_sesion_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY current_sesion
    ADD CONSTRAINT curent_sesion_pkey PRIMARY KEY (id);


--
-- Name: data data_pkey1; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY data
    ADD CONSTRAINT data_pkey1 PRIMARY KEY (id);


--
-- Name: device_parameters device_parameters_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY device_parameters
    ADD CONSTRAINT device_parameters_pkey PRIMARY KEY (id_device, id_parameter);


--
-- Name: devices devices_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY devices
    ADD CONSTRAINT devices_name_key UNIQUE (name);


--
-- Name: devices devices_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY devices
    ADD CONSTRAINT devices_pkey PRIMARY KEY (id);


--
-- Name: events_txt events_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY events_txt
    ADD CONSTRAINT events_pkey PRIMARY KEY (id);


--
-- Name: gas_types gas_types_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY gas_types
    ADD CONSTRAINT gas_types_name_key UNIQUE (name);


--
-- Name: gas_types gas_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY gas_types
    ADD CONSTRAINT gas_types_pkey PRIMARY KEY (id);


--
-- Name: users id; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY users
    ADD CONSTRAINT id PRIMARY KEY (id);


--
-- Name: programs id_program; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY programs
    ADD CONSTRAINT id_program PRIMARY KEY (id);


--
-- Name: subprograms id_subprogram; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY subprograms
    ADD CONSTRAINT id_subprogram PRIMARY KEY (id);


--
-- Name: languages languages_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY languages
    ADD CONSTRAINT languages_pkey PRIMARY KEY (id);


--
-- Name: users login; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY users
    ADD CONSTRAINT login UNIQUE (login);


--
-- Name: programs name; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY programs
    ADD CONSTRAINT name UNIQUE (name);


--
-- Name: parameters parameters_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY parameters
    ADD CONSTRAINT parameters_pkey PRIMARY KEY (id);


--
-- Name: program_configuration program_configuration_parameter_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY program_configuration
    ADD CONSTRAINT program_configuration_parameter_name_key UNIQUE (parameter_name);


--
-- Name: program_configuration program_configuration_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY program_configuration
    ADD CONSTRAINT program_configuration_pkey PRIMARY KEY (id);


--
-- Name: sesions sesions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY sesions
    ADD CONSTRAINT sesions_pkey PRIMARY KEY (id);


--
-- Name: stages_gas stages_gas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY stages_gas
    ADD CONSTRAINT stages_gas_pkey PRIMARY KEY (id);


--
-- Name: stages_power_supply stages_power_supply_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY stages_power_supply
    ADD CONSTRAINT stages_power_supply_pkey PRIMARY KEY (id);


--
-- Name: stages_pump stages_pump_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY stages_pump
    ADD CONSTRAINT stages_pump_pkey PRIMARY KEY (id);


--
-- Name: errors_txt Errors_id_language_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY errors_txt
    ADD CONSTRAINT "Errors_id_language_fkey" FOREIGN KEY (id_language) REFERENCES languages(id);


--
-- Name: acquisition_configurations acquisition_configurations_id_parameter_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY acquisition_configurations
    ADD CONSTRAINT acquisition_configurations_id_parameter_fkey FOREIGN KEY (id_parameter) REFERENCES parameters(id);


--
-- Name: events_txt events_txt_id_language_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY events_txt
    ADD CONSTRAINT events_txt_id_language_fkey FOREIGN KEY (id_language) REFERENCES languages(id);


--
-- Name: users id_privilige; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY users
    ADD CONSTRAINT id_privilige FOREIGN KEY (id_privilige) REFERENCES types_privilige(id);


--
-- Name: users id_type_block_account; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY users
    ADD CONSTRAINT id_type_block_account FOREIGN KEY (id_type_block_account) REFERENCES types_block_account(id);


--
-- PostgreSQL database dump complete
--

