-- View: public."Errors_Txt"

-- DROP VIEW public."Errors_Txt";

CREATE OR REPLACE VIEW public."Errors_Txt" AS 
 SELECT errors_txt.id,
    errors_txt.error_code,
    errors_txt.event_type,
    errors_txt.event_category,
    errors_txt.text,
    languages.id AS language_id,
    languages.name AS language_name,
    languages.value AS language_value
   FROM errors_txt
     LEFT JOIN languages ON errors_txt.id_language = languages.id;

ALTER TABLE public."Errors_Txt"
  OWNER TO postgres;
