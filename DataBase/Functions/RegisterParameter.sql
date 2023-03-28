-- Function: public."RegisterParameter"(integer, character varying)

-- DROP FUNCTION public."RegisterParameter"(integer, character varying);

CREATE OR REPLACE FUNCTION public."RegisterParameter"(
    dev_id integer,
    para_name character varying)
  RETURNS integer AS
$BODY$DECLARE
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
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RegisterParameter"(integer, character varying)
  OWNER TO postgres;
