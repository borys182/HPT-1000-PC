-- Function: public."RemoveSubprogram"(integer)

-- DROP FUNCTION public."RemoveSubprogram"(integer);

CREATE OR REPLACE FUNCTION public."RemoveSubprogram"(id_subprogram integer)
  RETURNS integer AS
$BODY$DECLARE
	res integer := 0;
	pump_id 	integer := 0;
	vent_id 	integer := 0;
	purge_id 	integer := 0;
	power_supply_id	integer := 0;
	gas_id 		integer := 0;
	motor_id	integer := 0;
BEGIN
	--odczytaj id parametrow procesu subprogramu
	select id_pump 		into pump_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_vent 		into vent_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_purge 	into purge_id 	     from subprograms where subprograms.id = id_subprogram; 
	select id_power_supplay into power_supply_id from subprograms where subprograms.id = id_subprogram; 
	select id_gas 		into gas_id 	     from subprograms where subprograms.id = id_subprogram;
	select id_motor		into motor_id 	     from subprograms where subprograms.id = id_subprogram;
	
	--Usun powiazanie z tabela programs
	delete from connections_program_subprograms where connections_program_subprograms.id_subprogram = $1;

	--usun parametry procesow subprogramu 
	delete from stages_gas 		where stages_gas.id          = gas_id;
	delete from stages_vent 	where stages_vent.id         = vent_id;
	delete from stages_purge 	where stages_purge.id 	     = purge_id;
	delete from stages_pump 	where stages_pump.id 	     = pump_id;
	delete from stages_power_supply where stages_power_supply.id = power_supply_id;
	delete from stages_motor	where stages_motor.id 	     = motor_id;
	
	--Usun rekord subprogramu
	delete from subprograms where subprograms.id = $1;
	
	RETURN res;
	EXCEPTION
		WHEN OTHERS THEN
			RAISE;

END$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RemoveSubprogram"(integer)
  OWNER TO postgres;
