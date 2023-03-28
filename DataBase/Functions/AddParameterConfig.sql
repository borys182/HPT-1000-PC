-- Function: public."AddParameterConfig"(integer, real, real, boolean, integer)

-- DROP FUNCTION public."AddParameterConfig"(integer, real, real, boolean, integer);

CREATE OR REPLACE FUNCTION public."AddParameterConfig"(
    parameter_id integer,
    freq real,
    differ_value real,
    enabled boolean,
    mode integer)
  RETURNS integer AS
$BODY$DECLARE
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
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."AddParameterConfig"(integer, real, real, boolean, integer)
  OWNER TO postgres;
