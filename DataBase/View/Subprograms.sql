-- View: public."Subprograms"

-- DROP VIEW public."Subprograms";

CREATE OR REPLACE VIEW public."Subprograms" AS 
 SELECT subprograms.id AS subprogram_id,
    subprograms.name AS subprogram_name,
    subprograms.description AS subprogram_description,
    stages_vent.vent_time,
    stages_purge."time" AS purge_time,
    stages_pump.max_time_pump,
    stages_pump.setpoint_pressure AS setpoint_pump_pressure,
    stages_power_supply.setpoint AS power_supply_setpoint,
    stages_power_supply.mode AS power_supply_mode,
    stages_power_supply.duration_time AS power_supply_time_process,
    stages_power_supply.max_devation AS power_supply_max_devation,
    stages_gas.mfc1_flow,
    stages_gas.mfc2_flow,
    stages_gas.mfc3_flow,
    stages_gas.mfc1_min_flow,
    stages_gas.mfc2_min_flow,
    stages_gas.mfc3_min_flow,
    stages_gas.mfc1_max_flow,
    stages_gas.mfc2_max_flow,
    stages_gas.mfc3_max_flow,
    stages_gas.mfc1_gas_share,
    stages_gas.mfc2_gas_share,
    stages_gas.mfc3_gas_share,
    stages_gas.mfc1_max_devation,
    stages_gas.mfc2_max_devation,
    stages_gas.mfc3_max_devation,
    stages_gas.vaporaiser_cycle_time,
    stages_gas.vaporaiser_on_time,
    stages_gas.setpoint_pressure AS gas_setpoint_pressure,
    stages_gas.max_devation_up AS gas_max_dev_up,
    stages_gas.max_devation_down AS gas_max_dev_down,
    stages_gas.time_duration AS gas_process_duration,
    stages_gas.mode_gas,
    stages_gas.mfc1_active,
    stages_gas.mfc2_active,
    stages_gas.mfc3_active,
    stages_gas.vaporaiser_active,
    stages_gas.mfc1_id_gas_type,
    stages_gas.mfc2_id_gas_type,
    stages_gas.mfc3_id_gas_type,
    stages_gas.vaporaiser_dosing,
    subprograms.pump_active,
    subprograms.power_supply_active,
    subprograms.gas_active,
    subprograms.purge_active,
    subprograms.vent_active,
    subprograms.motor_active,
    stages_motor.state_motor_1,
    stages_motor.state_motor_2,
    stages_motor.time_motor_1,
    stages_motor.time_motor_2,
    stages_motor.active_motor_1,
    stages_motor.active_motor_2
    
   FROM subprograms
     LEFT JOIN stages_vent ON stages_vent.id = subprograms.id_vent
     LEFT JOIN stages_purge ON stages_purge.id = subprograms.id_purge
     LEFT JOIN stages_pump ON stages_pump.id = subprograms.id_pump
     LEFT JOIN stages_power_supply ON stages_power_supply.id = subprograms.id_power_supplay
     LEFT JOIN stages_gas ON stages_gas.id = subprograms.id_gas
     LEFT JOIN stages_motor ON stages_motor.id = subprograms.id_motor;

ALTER TABLE public."Subprograms"
  OWNER TO postgres;
