-- View: public."Events_Txt"

-- DROP VIEW public."Events_Txt";

CREATE OR REPLACE VIEW public."Events_Txt" AS 
 SELECT events_txt.id,
    events_txt.code,
    events_txt.text,
    languages.id AS language_id,
    languages.name AS language_name,
    languages.value AS language_value
   FROM events_txt
     LEFT JOIN languages ON events_txt.id_language = languages.id;

ALTER TABLE public."Events_Txt"
  OWNER TO postgres;
