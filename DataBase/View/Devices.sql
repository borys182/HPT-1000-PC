-- View: public."Devices"

-- DROP VIEW public."Devices";

CREATE OR REPLACE VIEW public."Devices" AS 
 SELECT devices.id AS device_id,
    devices.name AS device_name,
    parameters.id AS para_id,
    parameters.name AS para_name
   FROM devices
     LEFT JOIN device_parameters ON devices.id = device_parameters.id_device
     LEFT JOIN parameters ON parameters.id = device_parameters.id_parameter;

ALTER TABLE public."Devices"
  OWNER TO postgres;
