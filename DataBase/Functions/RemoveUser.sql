-- Function: public."RemoveUser"(integer)

-- DROP FUNCTION public."RemoveUser"(integer);

CREATE OR REPLACE FUNCTION public."RemoveUser"(id integer)
  RETURNS integer AS
$BODY$  DECLARE
	res integer := 0;
	tmp integer := 0;
   BEGIN
	select COUNT(*) from users where users.id = $1 into tmp;
	IF tmp > 0 then
		delete from users where users.id = $1;
	ELSE
		RAISE 'User does not exist in database';
	END IF;
	RETURN res;
	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
   END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RemoveUser"(integer)
  OWNER TO postgres;
