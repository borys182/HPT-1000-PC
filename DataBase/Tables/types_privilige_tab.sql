-- Table: public.types_privilige

-- DROP TABLE public.types_privilige;

CREATE TABLE public.types_privilige
(
  id integer NOT NULL DEFAULT nextval('priviliges_id_seq'::regclass),
  name character varying(30),
  value integer,
  CONSTRAINT "Types_Privilige_pkey" PRIMARY KEY (id),
  CONSTRAINT constraint_value_name UNIQUE (name),
  CONSTRAINT constraint_value_pr UNIQUE (value)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.types_privilige
  OWNER TO postgres;
