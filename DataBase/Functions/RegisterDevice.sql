-- Function: public."RegisterDevice"(character varying)

-- DROP FUNCTION public."RegisterDevice"(character varying);

CREATE OR REPLACE FUNCTION public."RegisterDevice"(dev_name character varying)
  RETURNS integer AS
$BODY$DECLARE
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
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RegisterDevice"(character varying)
  OWNER TO postgres;
