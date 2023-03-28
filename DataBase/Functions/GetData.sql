-- Function: public."GetData"(timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION public."GetData"(timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public."GetData"(
    IN "StartDate" timestamp without time zone,
    IN "EndDate" timestamp without time zone)
  RETURNS TABLE(id integer, id_parameter integer, value real, unit character varying, date timestamp without time zone) AS
$BODY$
DECLARE 
	res integer := 0;
	
BEGIN

	RETURN QUERY Select * from data where data.date >= "StartDate" and data.date <= "EndDate" order by data.date ASC ;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION public."GetData"(timestamp without time zone, timestamp without time zone)
  OWNER TO postgres;
