-- Function: public."SaveParameter"(integer, character varying, text)

-- DROP FUNCTION public."SaveParameter"(integer, character varying, text);

CREATE OR REPLACE FUNCTION public."SaveParameter"(
    id_ integer,
    name character varying,
    parameter text)
  RETURNS integer AS
$BODY$DECLARE
	id_res 		integer := 0;
	id_para		integer := 0;
BEGIN
	select id from program_configuration where parameter_name= name into id_para;

	IF id_para > 0 THEN
		update program_configuration set data = parameter where id = id_para;
		id_res = id_para;
	ELSE
		insert into program_configuration(parameter_name,data)values(name,parameter) returning id into id_res;
	END IF;

	RETURN id_res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."SaveParameter"(integer, character varying, text)
  OWNER TO postgres;
