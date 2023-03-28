-- Function: public."NewPrivilige"(character varying, integer)

-- DROP FUNCTION public."NewPrivilige"(character varying, integer);

CREATE OR REPLACE FUNCTION public."NewPrivilige"(
    name character varying,
    value integer)
  RETURNS integer AS
$BODY$
insert into types_privilige(name,value)values($1,$2)
returning id
$BODY$
  LANGUAGE sql VOLATILE
  COST 100;
ALTER FUNCTION public."NewPrivilige"(character varying, integer)
  OWNER TO postgres;
