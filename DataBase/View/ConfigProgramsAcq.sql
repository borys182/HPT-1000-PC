-- View: public."ConfigParametersAcq"

-- DROP VIEW public."ConfigParametersAcq";

CREATE OR REPLACE VIEW public."ConfigParametersAcq" AS 
 SELECT acquisition_configurations.id,
    acquisition_configurations.id_parameter,
    acquisition_configurations.frequency,
    acquisition_configurations.difference_value,
    acquisition_configurations.enabled_acq,
    acquisition_configurations.mode_acq
   FROM acquisition_configurations;

ALTER TABLE public."ConfigParametersAcq"
  OWNER TO postgres;
