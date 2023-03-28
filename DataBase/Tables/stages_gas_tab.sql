-- Table: public.stages_gas

-- DROP TABLE public.stages_gas;

CREATE TABLE public.stages_gas
(
  id integer NOT NULL DEFAULT nextval('stages_gas_id_seq'::regclass),
  mfc1_flow real,
  mfc1_min_flow real,
  mfc1_max_flow real,
  mfc2_flow real,
  mfc2_min_flow real,
  mfc2_max_flow real,
  mfc3_flow real,
  mfc3_min_flow real,
  mfc3_max_flow real,
  vaporaiser_cycle_time real,
  vaporaiser_on_time real,
  setpoint_pressure real,
  max_devation_up real,
  max_devation_down real,
  mfc1_max_devation real,
  mfc1_gas_share real,
  mfc2_max_devation real,
  mfc2_gas_share real,
  mfc3_max_devation real,
  mfc3_gas_share real,
  time_duration time without time zone,
  mode_gas integer,
  mfc1_active boolean,
  mfc2_active boolean,
  mfc3_active boolean,
  vaporaiser_active boolean,
  mfc1_id_gas_type integer,
  mfc2_id_gas_type integer,
  mfc3_id_gas_type integer,
  vaporaiser_dosing integer,
  CONSTRAINT stages_gas_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.stages_gas
  OWNER TO postgres;
