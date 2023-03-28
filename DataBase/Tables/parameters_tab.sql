-- Table: public.parameters

-- DROP TABLE public.parameters;

CREATE TABLE public.parameters
(
  id integer NOT NULL DEFAULT nextval('parameters_id_seq'::regclass),
  name character varying NOT NULL,
  CONSTRAINT parameters_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.parameters
  OWNER TO postgres;
