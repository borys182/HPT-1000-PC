-- Function: public."StartSesion"(timestamp without time zone, bytea)

-- DROP FUNCTION public."StartSesion"(timestamp without time zone, bytea);

CREATE OR REPLACE FUNCTION public."StartSesion"(
    start_date timestamp without time zone,
    process_parameter bytea)
  RETURNS integer AS
$BODY$DECLARE
	id_sesion	 integer := 0;
	id_current_sesion integer := 0;
	

BEGIN
	--Utworz rekord sesji danych i zapisz jej id do tabeli current_sesion
	insert into sesions(date_start,date_end,process_parameter)values(start_date,start_date,process_parameter)returning id into id_sesion;
	
	--zapisz id_aktualnie wykonywanej sesji do tablicy current_sesion aby mozna bylo dopisywac autoamtycznie date_end podczas zapisu danych doa bazy
	--Odczytaj id pierwszego wpisu z tablicy currnet_sesion. Jezeli nie istnieje to go utwprze w przeciwnym wypadku aktualuizje
	select id from current_sesion into id_current_sesion;
	-- sprawdzam czy zostal juz utworzony wpis do tabeli current_sesion jezeli nie to go tworze w przeciwnym wypadku aktualizuje
	
	if  id_current_sesion > 0 then
		update current_sesion set current_sesion_id = id_sesion where id = id_current_sesion;
	else
		insert into current_sesion(current_sesion_id)values(id_sesion);
	end if;
	
	RETURN id_sesion;

	EXCEPTION
		WHEN OTHERS THEN
			RAISE;
END;$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100;
ALTER FUNCTION public."StartSesion"(timestamp without time zone, bytea)
  OWNER TO postgres;
