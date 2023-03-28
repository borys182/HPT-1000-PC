-- Function: public."ModifySubprogramStages_2"(integer, boolean, boolean, boolean, boolean, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, time without time zone, integer, integer, integer, integer, integer)

-- DROP FUNCTION public."ModifySubprogramStages_2"(integer, boolean, boolean, boolean, boolean, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, time without time zone, integer, integer, integer, integer, integer);

CREATE OR REPLACE FUNCTION public."ModifySubprogramStages_2"(
    id_subprogram integer,
    active_mfc1 boolean,
    active_mfc2 boolean,
    active_mfc3 boolean,
    active_vaporaiser boolean,
    flow_mfc1 real,
    flow_mfc2 real,
    flow_mfc3 real,
    min_flow_mfc1 real,
    min_flow_mfc2 real,
    min_flow_mfc3 real,
    max_flow_mfc1 real,
    max_flow_mfc2 real,
    max_flow_mfc3 real,
    vaporaiser_cycle real,
    vaporaiser_time_on real,
    setpoint_press real,
    max_dev_up real,
    max_dev_down real,
    mfc1_max_dev real,
    mfc2_max_dev real,
    mfc3_max_dev real,
    gas_share_mfc1 real,
    gas_share_mfc2 real,
    gas_share_mfc3 real,
    "time" time without time zone,
    mode_process integer,
    mfc1_id_type_gas integer,
    mfc2_id_type_gas integer,
    mfc3_id_type_gas integer,
    vaporaiser_dosing_ integer)
  RETURNS integer AS
$BODY$DECLARE
	res 		integer := 0;
	gas_id 		integer := 0;
  BEGIN

	select id_gas into gas_id from subprograms where id = id_subprogram;

	update stages_gas set mfc1_flow = flow_mfc1, mfc2_flow = flow_mfc2, mfc3_flow = flow_mfc3,
	mfc1_min_flow = min_flow_mfc1 , mfc2_min_flow = min_flow_mfc2 , mfc3_min_flow = min_flow_mfc3,
	mfc1_max_flow = max_flow_mfc1 , mfc2_max_flow = max_flow_mfc2 , mfc3_max_flow = max_flow_mfc3, 
	vaporaiser_cycle_time = vaporaiser_cycle , vaporaiser_on_time = vaporaiser_time_on,
	setpoint_pressure = setpoint_press, max_devation_up = max_dev_up, max_devation_down = max_dev_down,
	mfc1_max_devation = mfc1_max_dev, mfc2_max_devation = mfc2_max_dev, mfc3_max_devation = mfc3_max_dev,
	mfc1_gas_share = gas_share_mfc1, mfc2_gas_share = gas_share_mfc2, mfc3_gas_share = gas_share_mfc3,
	time_duration = time , mode_gas = mode_process,
	mfc1_active = active_mfc1,mfc2_active = active_mfc2,mfc3_active = active_mfc3, vaporaiser_active = active_vaporaiser,
	mfc1_id_gas_type = mfc1_id_type_gas , mfc2_id_gas_type = mfc2_id_type_gas , mfc3_id_gas_type = mfc3_id_type_gas,vaporaiser_dosing = vaporaiser_dosing_
	where stages_gas.id = gas_id;
	
	RETURN res;
	EXCEPTION
		WHEN UNIQUE_VIOLATION THEN
			RAISE 'Program name alredy exist';
		WHEN NOT_NULL_VIOLATION THEN
			RAISE 'Required fields are not filled';
		WHEN OTHERS THEN
			RAISE;
  END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."ModifySubprogramStages_2"(integer, boolean, boolean, boolean, boolean, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, real, time without time zone, integer, integer, integer, integer, integer)
  OWNER TO postgres;
