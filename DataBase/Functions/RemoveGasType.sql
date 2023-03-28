-- Function: public."RemoveGasType"(integer)

-- DROP FUNCTION public."RemoveGasType"(integer);

CREATE OR REPLACE FUNCTION public."RemoveGasType"(id_gas_type integer)
  RETURNS integer AS
$BODY$DECLARE
	res integer := 0;
BEGIN

	delete from gas_types where gas_types.id = id_gas_type;	

	RETURN res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."RemoveGasType"(integer)
  OWNER TO postgres;
