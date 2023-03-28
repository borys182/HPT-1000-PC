-- Table: public.stages_motor

-- DROP TABLE public.stages_motor;

CREATE TABLE public.stages_motor
(
  id integer NOT NULL DEFAULT nextval('stages_motor_id_seq'::regclass),
  state_motor_1 integer,
  state_motor_2 integer,
  time_motor_1 time without time zone,
  time_motor_2 time without time zone,
  active_motor_1 boolean,
  active_motor_2 boolean,
  CONSTRAINT stages_motor_pkey PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.stages_motor
  OWNER TO postgres;
