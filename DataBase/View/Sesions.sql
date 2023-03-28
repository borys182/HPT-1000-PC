-- View: public."Sesions"

-- DROP VIEW public."Sesions";

CREATE OR REPLACE VIEW public."Sesions" AS 
 SELECT sesions.id,
    sesions.date_start,
    sesions.date_end,
    sesions.process_name
   FROM sesions;

ALTER TABLE public."Sesions"
  OWNER TO postgres;
