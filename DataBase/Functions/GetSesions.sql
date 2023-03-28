-- Function: public."GetSesions"(timestamp without time zone, timestamp without time zone)

-- DROP FUNCTION public."GetSesions"(timestamp without time zone, timestamp without time zone);

CREATE OR REPLACE FUNCTION public."GetSesions"(
    IN "StartDate" timestamp without time zone,
    IN "EndDate" timestamp without time zone)
  RETURNS TABLE(id integer, date_start timestamp without time zone, date_end timestamp without time zone, process_parameter bytea) AS
$BODY$
DECLARE 
	res integer := 0;
	
BEGIN

	RETURN QUERY Select sesions.id, sesions.date_start,sesions.date_end,sesions.process_parameter from sesions where sesions.date_start >= "StartDate" and sesions.date_start <= "EndDate" or sesions.date_start < "StartDate" and sesions.date_end >= "StartDate" order by sesions.date_start ASC;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION public."GetSesions"(timestamp without time zone, timestamp without time zone)
  OWNER TO postgres;
