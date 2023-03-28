-- Function: public."RemoveTypeBlockAccount"(integer)

-- DROP FUNCTION public."RemoveTypeBlockAccount"(integer);

CREATE OR REPLACE FUNCTION public."RemoveTypeBlockAccount"(id integer)
  RETURNS integer AS
$BODY$
DECLARE
	res integer := 0;
BEGIN

	delete from types_block_account where types_block_account.id = $1;
	RETURN res;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RemoveTypeBlockAccount"(integer)
  OWNER TO postgres;
