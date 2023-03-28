-- Table: public.device_parameters

-- DROP TABLE public.device_parameters;

CREATE TABLE public.device_parameters
(
  id_device integer NOT NULL,
  id_parameter integer NOT NULL,
  CONSTRAINT device_parameters_pkey PRIMARY KEY (id_device, id_parameter)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.device_parameters
  OWNER TO postgres;
