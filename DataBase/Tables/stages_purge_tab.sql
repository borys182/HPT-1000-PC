-- Table: public.stages_purge

-- DROP TABLE public.stages_purge;

CREATE TABLE public.stages_purge
(
  id integer NOT NULL DEFAULT nextval('stages_purge_id_seq'::regclass),
  "time" time without time zone
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.stages_purge
  OWNER TO postgres;
