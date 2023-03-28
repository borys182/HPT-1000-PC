-- Function: public."AddSubprogram"(integer, integer, character varying, text)

DROP FUNCTION public."AddSubprogram"(integer, character varying, text);

CREATE OR REPLACE FUNCTION public."AddSubprogram"(
    program_id integer,
    ordinal_number integer,
    name_subprogram character varying,
    description text)
  RETURNS integer AS
$BODY$DECLARE
	subprogram_id	integer := 0;
	id_program 	integer := null;
	id_vent 	integer := null;
	id_pump 	integer := null;
	id_purge 	integer := null;
	id_gas	 	integer := null;
	id_power_supply	integer := null;
	id_motor	integer := null;
	
BEGIN
	--Utworzenie rekordow w tabelach stepow danego subprmagru
	insert into stages_pump(setpoint_pressure)values(NULL)returning id into id_pump;
	insert into stages_purge(time)values(NULL)returning id into id_purge;
	insert into stages_vent(vent_time)values(NULL)returning id into id_vent;
	insert into stages_power_supply(setpoint)values(NULL)returning id into id_power_supply;
	insert into stages_gas(mode_gas)values(NULL)returning id into id_gas;
	insert into stages_motor(state_motor_1)values(NULL)returning id into id_motor;

	insert into subprograms(name,description,id_pump,id_vent,id_purge,id_power_supplay,id_gas,id_motor,subprogram_ordinal_number)
	values(name_subprogram,description,id_pump,id_vent,id_purge,id_power_supply,id_gas,id_motor,ordinal_number)
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
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."AddSubprogram"(integer, integer, character varying, text)
  OWNER TO postgres;
