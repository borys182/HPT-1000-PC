-- Table: public.stages_vent

-- DROP TABLE public.stages_vent;

CREATE TABLE public.stages_vent
(
  id integer NOT NULL DEFAULT nextval('stages_vent_id_seq'::regclass),
  vent_time time without time zone
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.stages_vent
  OWNER TO postgres;
