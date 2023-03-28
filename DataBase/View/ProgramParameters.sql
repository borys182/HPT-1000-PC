-- View: public."ProgramParameters"

-- DROP VIEW public."ProgramParameters";

CREATE OR REPLACE VIEW public."ProgramParameters" AS 
 SELECT program_configuration.id,
    program_configuration.parameter_name AS name,
    program_configuration.data AS parameter
   FROM program_configuration;

ALTER TABLE public."ProgramParameters"
  OWNER TO postgres;
