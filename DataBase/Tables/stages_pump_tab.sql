-- Table: public.stages_pump

-- DROP TABLE public.stages_pump;

CREATE TABLE public.stages_pump
(
  id integer NOT NULL DEFAULT nextval('stages_pump_id_seq'::regclass),
  max_time_pump time without time zone,
  setpoint_pressure real,
  CONSTRAINT stages_pump_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.stages_pump
  OWNER TO postgres;
