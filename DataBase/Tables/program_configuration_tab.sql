-- Table: public.program_configuration

-- DROP TABLE public.program_configuration;

CREATE TABLE public.program_configuration
(
  id integer NOT NULL DEFAULT nextval('program_configuration_id_seq'::regclass),
  parameter_name character varying(30) NOT NULL,
  data text NOT NULL,
  CONSTRAINT program_configuration_pkey PRIMARY KEY (id),
  CONSTRAINT program_configuration_parameter_name_key UNIQUE (parameter_name)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.program_configuration
  OWNER TO postgres;
