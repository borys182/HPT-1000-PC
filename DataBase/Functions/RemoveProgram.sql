-- Function: public."RemoveProgram"(integer)

-- DROP FUNCTION public."RemoveProgram"(integer);

CREATE OR REPLACE FUNCTION public."RemoveProgram"(id integer)
  RETURNS integer AS
$BODY$DECLARE
	res integer := 0;
	connection connections_program_subprograms%rowtype;
BEGIN
	--Usun subprogramy nalezace do danego programu
	FOR connection IN select * from connections_program_subprograms where connections_program_subprograms.id_program = $1
	LOOP
		select "RemoveSubprogram"(connection.id_subprogram) into res;
	
	END LOOP;
	--Usun powiazanie z tabela subprograms
	delete from connections_program_subprograms where id_program = $1;
	--Usun program
	delete from programs where programs.id = $1;
	RETURN res;
	
	EXCEPTION
		WHEN OTHERS THEN
			RAISE;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RemoveProgram"(integer)
  OWNER TO postgres;
