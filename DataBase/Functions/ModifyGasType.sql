-- Function: public."ModifyGasType"(integer, character varying, text, real)

-- DROP FUNCTION public."ModifyGasType"(integer, character varying, text, real);

CREATE OR REPLACE FUNCTION public."ModifyGasType"(
    id_gas_type integer,
    name_ character varying,
    description_ text,
    factor_ real)
  RETURNS integer AS
$BODY$DECLARE
	res 	integer := 0;
  BEGIN

	update gas_types set name = name_, description = description_, factor = factor_	where gas_types.id = id_gas_type;
	
	RETURN res;
	
	EXCEPTION
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."ModifyGasType"(integer, character varying, text, real)
  OWNER TO postgres;
