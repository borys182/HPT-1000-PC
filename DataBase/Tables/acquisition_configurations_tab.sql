-- Table: public.acquisition_configurations

-- DROP TABLE public.acquisition_configurations;

CREATE TABLE public.acquisition_configurations
(
  id integer NOT NULL DEFAULT nextval('acquisition_configurations_id_seq'::regclass),
  id_parameter integer NOT NULL,
  frequency real,
  difference_value real,
  enabled_acq boolean NOT NULL,
  mode_acq integer NOT NULL,
  CONSTRAINT acquisition_configurations_pkey PRIMARY KEY (id),
  CONSTRAINT acquisition_configurations_id_parameter_fkey FOREIGN KEY (id_parameter)
      REFERENCES public.parameters (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.acquisition_configurations
  OWNER TO postgres;
