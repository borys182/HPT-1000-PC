-- Function: public."NewTypeBlockAccount"(character varying, integer)

-- DROP FUNCTION public."NewTypeBlockAccount"(character varying, integer);

CREATE OR REPLACE FUNCTION public."NewTypeBlockAccount"(
    name character varying,
    value integer)
  RETURNS integer AS
$BODY$
DECLARE
	res integer := 0;
BEGIN

insert into types_block_account(name,value)values($1,$2) returning id into res;
RETURN res;

END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."NewTypeBlockAccount"(character varying, integer)
  OWNER TO postgres;
