-- Function: public."NewProgram"(character varying, text)

-- DROP FUNCTION public."NewProgram"(character varying, text);

CREATE OR REPLACE FUNCTION public."NewProgram"(
    name character varying,
    description text)
  RETURNS integer AS
$BODY$DECLARE
	id_res 		integer := 0;
BEGIN

	insert into programs(name,description)values($1,$2) returning id into id_res;

	RETURN id_res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."NewProgram"(character varying, text)
  OWNER TO postgres;
