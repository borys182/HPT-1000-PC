-- Function: public."ModifyParaConfig"(integer, real, real, boolean, integer)

-- DROP FUNCTION public."ModifyParaConfig"(integer, real, real, boolean, integer);

CREATE OR REPLACE FUNCTION public."ModifyParaConfig"(
    id_ integer,
    freq real,
    differ_value real,
    enabled boolean,
    mode integer)
  RETURNS integer AS
$BODY$DECLARE
	res 		integer := 0;
  BEGIN

	update acquisition_configurations set frequency = freq, difference_value = differ_value, enabled_acq = enabled,mode_acq = mode
	where  acquisition_configurations.id = id_;

	RETURN res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
  END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."ModifyParaConfig"(integer, real, real, boolean, integer)
  OWNER TO postgres;
