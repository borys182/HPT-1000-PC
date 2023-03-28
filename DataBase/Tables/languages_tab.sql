-- Table: public.languages

-- DROP TABLE public.languages;

CREATE TABLE public.languages
(
  id integer NOT NULL DEFAULT nextval('languages_id_seq'::regclass),
  name character varying(30) NOT NULL,
  value integer NOT NULL,
  CONSTRAINT languages_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.languages
  OWNER TO postgres;
