-- View: public."Users"

-- DROP VIEW public."Users";

CREATE OR REPLACE VIEW public."Users" AS 
 SELECT users.id,
    users.name,
    users.surname,
    users.login,
    users.password,
    users.allow_change_psw,
    types_block_account.value AS type_block_account,
    types_privilige.value AS privilige,
    users.start_date_block_account,
    users.end_date_block_account
   FROM users
     LEFT JOIN types_privilige ON types_privilige.id = users.id_privilige
     LEFT JOIN types_block_account ON users.id_type_block_account = types_block_account.id;

ALTER TABLE public."Users"
  OWNER TO postgres;
