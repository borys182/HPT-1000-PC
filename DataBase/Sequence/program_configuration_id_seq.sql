-- Sequence: public.program_configuration_id_seq

-- DROP SEQUENCE public.program_configuration_id_seq;

CREATE SEQUENCE public.program_configuration_id_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE public.program_configuration_id_seq
  OWNER TO postgres;
