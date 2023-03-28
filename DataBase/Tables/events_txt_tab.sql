-- Table: public.events_txt

-- DROP TABLE public.events_txt;

CREATE TABLE public.events_txt
(
  id integer NOT NULL DEFAULT nextval('events_id_seq'::regclass),
  code integer NOT NULL,
  text text NOT NULL,
  id_language integer NOT NULL,
  CONSTRAINT events_pkey PRIMARY KEY (id),
  CONSTRAINT events_txt_id_language_fkey FOREIGN KEY (id_language)
      REFERENCES public.languages (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.events_txt
  OWNER TO postgres;
