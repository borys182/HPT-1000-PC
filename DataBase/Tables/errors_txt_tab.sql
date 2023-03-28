-- Table: public.errors_txt

-- DROP TABLE public.errors_txt;

CREATE TABLE public.errors_txt
(
  id integer NOT NULL DEFAULT nextval('"Errors_id_seq"'::regclass),
  error_code integer NOT NULL,
  event_type integer,
  event_category integer,
  text text NOT NULL,
  id_language integer NOT NULL,
  CONSTRAINT "Errors_pkey" PRIMARY KEY (id),
  CONSTRAINT "Errors_id_language_fkey" FOREIGN KEY (id_language)
      REFERENCES public.languages (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.errors_txt
  OWNER TO postgres;
