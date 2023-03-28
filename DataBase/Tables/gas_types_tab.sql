-- Table: public.gas_types

-- DROP TABLE public.gas_types;

CREATE TABLE public.gas_types
(
  id integer NOT NULL DEFAULT nextval('gas_types_id_seq'::regclass),
  name character varying NOT NULL,
  description text,
  factor real NOT NULL,
  CONSTRAINT gas_types_pkey PRIMARY KEY (id),
  CONSTRAINT gas_types_name_key UNIQUE (name)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.gas_types
  OWNER TO postgres;
