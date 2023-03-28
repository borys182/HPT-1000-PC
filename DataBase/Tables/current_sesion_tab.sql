-- Table: public.current_sesion

-- DROP TABLE public.current_sesion;

CREATE TABLE public.current_sesion
(
  id integer NOT NULL DEFAULT nextval('curent_sesion_id_seq'::regclass),
  current_sesion_id integer NOT NULL,
  CONSTRAINT curent_sesion_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.current_sesion
  OWNER TO postgres;
