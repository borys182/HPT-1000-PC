-- Function: public."ModifyTypeBlockAccount"(integer, character varying, integer)

-- DROP FUNCTION public."ModifyTypeBlockAccount"(integer, character varying, integer);

CREATE OR REPLACE FUNCTION public."ModifyTypeBlockAccount"(
    id integer,
    name character varying,
    value integer)
  RETURNS integer AS
$BODY$DECLARE
	res integer := 0;
BEGIN

update types_block_account set name = $2, value = $3 where types_block_account.id = $1;
	return res;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."ModifyTypeBlockAccount"(integer, character varying, integer)
  OWNER TO postgres;
