-- Table: public.programs

-- DROP TABLE public.programs;

CREATE TABLE public.programs
(
  id integer NOT NULL DEFAULT nextval('programs_id_seq'::regclass),
  name character varying(30) NOT NULL,
  description text,
  CONSTRAINT id_program PRIMARY KEY (id),
  CONSTRAINT name UNIQUE (name)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.programs
  OWNER TO postgres;
