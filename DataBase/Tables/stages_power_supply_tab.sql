-- Table: public.stages_power_supply

-- DROP TABLE public.stages_power_supply;

CREATE TABLE public.stages_power_supply
(
  id integer NOT NULL DEFAULT nextval('stages_power_supply_id_seq'::regclass),
  setpoint real,
  mode integer,
  duration_time time without time zone,
  max_devation real,
  CONSTRAINT stages_power_supply_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.stages_power_supply
  OWNER TO postgres;
