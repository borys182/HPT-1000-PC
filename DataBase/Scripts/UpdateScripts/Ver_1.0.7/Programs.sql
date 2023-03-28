-- View: public."Programs"

-- DROP VIEW public."Programs";

CREATE OR REPLACE VIEW public."Programs" AS 
 SELECT programs.id AS program_id,
    programs.name AS program_name,
    programs.description AS program_description,
    subprograms.id AS subprogram_id,
    subprograms.name AS subprogram_name,
    subprograms.description AS subprogram_description,
    subprograms.pump_active,
    subprograms.power_supply_active,
    subprograms.gas_active,
    subprograms.purge_active,
    subprograms.vent_active,
    subprograms.motor_active

   FROM programs
     LEFT JOIN connections_program_subprograms ON connections_program_subprograms.id_program = programs.id
     LEFT JOIN subprograms ON connections_program_subprograms.id_subprogram = subprograms.id;

ALTER TABLE public."Programs"
  OWNER TO postgres;
