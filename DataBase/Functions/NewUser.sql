-- Function: public."NewUser"(character varying, character varying, character varying, character varying, boolean, integer, integer, date, date)

-- DROP FUNCTION public."NewUser"(character varying, character varying, character varying, character varying, boolean, integer, integer, date, date);

CREATE OR REPLACE FUNCTION public."NewUser"(
    name character varying,
    surname character varying,
    login character varying,
    password character varying,
    allow_change_psw boolean,
    type_block integer,
    privilige integer,
    start_date_block date,
    end_date_block date)
  RETURNS integer AS
$BODY$
  
  DECLARE
	id_res 		integer := 0;
	id_privilige 	integer := null;
	id_type_block 	integer := null;
BEGIN

	select id from types_block_account where value = type_block into id_type_block;	
	select id from types_privilige where value = privilige into id_privilige;	

	insert into users(name,surname,login,password,allow_change_psw,id_type_block_account,id_privilige,start_date_block_account,end_date_block_account) 
	values($1,$2,$3,$4,$5,id_type_block,id_privilige,$8,$9) returning id into id_res;

	RETURN id_res;

	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Login alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."NewUser"(character varying, character varying, character varying, character varying, boolean, integer, integer, date, date)
  OWNER TO postgres;
