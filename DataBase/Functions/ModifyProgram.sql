-- Function: public."ModifyProgram"(integer, character varying, text)

-- DROP FUNCTION public."ModifyProgram"(integer, character varying, text);

CREATE OR REPLACE FUNCTION public."ModifyProgram"(
    id integer,
    name character varying,
    description text)
  RETURNS integer AS
$BODY$ DECLARE
	res 		integer := 0;
  BEGIN

	update programs set name = $2, description = $3	where programs.id = $1;
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
ALTER FUNCTION public."ModifyProgram"(integer, character varying, text)
  OWNER TO postgres;
