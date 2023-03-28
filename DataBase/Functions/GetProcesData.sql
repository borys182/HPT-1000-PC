-- Function: public."GetProcesData"(integer)

-- DROP FUNCTION public."GetProcesData"(integer);

CREATE OR REPLACE FUNCTION public."GetProcesData"(IN "ProcesID" integer)
  RETURNS TABLE(id integer, id_parameter integer, value real, unit character varying, date timestamp without time zone) AS
$BODY$
DECLARE 
	res integer := 0;
	
BEGIN

	RETURN QUERY Select data.id, data.id_parameter, data.value, data.unit, data.date from data where data.id_sesion = "ProcesID" order by data.date ASC ;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION public."GetProcesData"(integer)
  OWNER TO postgres;
