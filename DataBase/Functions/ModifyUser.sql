-- Function: public."ModifyUser"(integer, character varying, character varying, character varying, character varying, boolean, integer, integer, date, date)

-- DROP FUNCTION public."ModifyUser"(integer, character varying, character varying, character varying, character varying, boolean, integer, integer, date, date);

CREATE OR REPLACE FUNCTION public."ModifyUser"(
    id integer,
    name character varying,
    surname character varying,
    login character varying,
    password character varying,
    allow_change_psw boolean,
    type_block integer,
    privilige integer,
    start_date date,
    end_date date)
  RETURNS integer AS
$BODY$  DECLARE
	res 		integer := 0;
	a_id_privilige 	integer := null;
	a_id_type_block integer := null;
  BEGIN

	select types_block_account.id from types_block_account where value = type_block into a_id_type_block;	
	select types_privilige.id from types_privilige where value = privilige into a_id_privilige;	

	update users set name = $2, surname = $3, login = $4, password = $5, allow_change_psw = $6,  
	id_type_block_account = a_id_type_block, id_privilige = a_id_privilige, start_date_block_account = $9, end_date_block_account = $10
	where users.id = $1;
	RETURN res;
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
ALTER FUNCTION public."ModifyUser"(integer, character varying, character varying, character varying, character varying, boolean, integer, integer, date, date)
  OWNER TO postgres;
