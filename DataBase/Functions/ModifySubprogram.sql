-- Function: public."ModifySubprogram"(integer, integer, character varying, text, boolean, boolean, boolean, boolean, boolean, boolean)

-- DROP FUNCTION public."ModifySubprogram"(integer, integer, character varying, text, boolean, boolean, boolean, boolean, boolean, boolean);

CREATE OR REPLACE FUNCTION public."ModifySubprogram"(
    id integer,
    ordinal_number integer,
    name character varying,
    description text,
    active_pump boolean,
    active_power_supply boolean,
    active_gas boolean,
    active_purge boolean,
    active_vent boolean,
    active_motor boolean)
  RETURNS integer AS
$BODY$  DECLARE
	res	integer := 0;
  BEGIN

	update subprograms set name = $3, description = $4,pump_active = active_pump,power_supply_active = active_power_supply,gas_active = active_gas,purge_active = active_purge,vent_active = active_vent,motor_active = active_motor ,subprogram_ordinal_number = ordinal_number
	where subprograms.id = $1;
	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Program name alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."ModifySubprogram"(integer, integer, character varying, text, boolean, boolean, boolean, boolean, boolean, boolean)
  OWNER TO postgres;
