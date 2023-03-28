-- Function: public."AddGasType"(character varying, text, real)

-- DROP FUNCTION public."AddGasType"(character varying, text, real);

CREATE OR REPLACE FUNCTION public."AddGasType"(
    name_ character varying,
    description_ text,
    factor_ real)
  RETURNS integer AS
$BODY$DECLARE
	id_res		integer := 0;
	
BEGIN
	-- Dodaj do tablicy gas_types nowy typ gazu
	insert into gas_types(name,description,factor)
		values(name_,description_,factor_ )returning id into id_res;

	RETURN id_res;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."AddGasType"(character varying, text, real)
  OWNER TO postgres;
