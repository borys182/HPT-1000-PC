-- Table: public.devices

-- DROP TABLE public.devices;

CREATE TABLE public.devices
(
  id integer NOT NULL DEFAULT nextval('devices_id_seq'::regclass),
  name character varying NOT NULL,
  CONSTRAINT devices_pkey PRIMARY KEY (id),
  CONSTRAINT devices_name_key UNIQUE (name)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.devices
  OWNER TO postgres;
