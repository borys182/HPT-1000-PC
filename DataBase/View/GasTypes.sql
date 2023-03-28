-- View: public."GasTypes"

-- DROP VIEW public."GasTypes";

CREATE OR REPLACE VIEW public."GasTypes" AS 
 SELECT gas_types.id,
    gas_types.name,
    gas_types.description,
    gas_types.factor
   FROM gas_types;

ALTER TABLE public."GasTypes"
  OWNER TO postgres;
