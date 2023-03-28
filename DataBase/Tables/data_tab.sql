-- Table: public.data

-- DROP TABLE public.data;

CREATE TABLE public.data
(
  id integer NOT NULL DEFAULT nextval('data_id_seq1'::regclass),
  id_parameter integer NOT NULL,
  value real NOT NULL,
  unit character varying(30) NOT NULL,
  date timestamp without time zone NOT NULL,
  id_sesion integer NOT NULL,
  CONSTRAINT data_pkey1 PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.data
  OWNER TO postgres;
