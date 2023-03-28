-- Function: public."RemovePrivilige"(integer)

-- DROP FUNCTION public."RemovePrivilige"(integer);

CREATE OR REPLACE FUNCTION public."RemovePrivilige"(id integer)
  RETURNS integer AS
$BODY$DECLARE
	res integer := 0;
BEGIN

delete from types_privilige where types_privilige.id = $1;
RETURN res;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RemovePrivilige"(integer)
  OWNER TO postgres;
