-- Table: public.users

-- DROP TABLE public.users;

CREATE TABLE public.users
(
  id integer NOT NULL DEFAULT nextval('users_id_seq'::regclass), -- 
  name character varying(30),
  surname character varying(30),
  login character varying(30) NOT NULL,
  password character varying(30),
  allow_change_psw boolean,
  id_type_block_account integer,
  id_privilige integer,
  start_date_block_account date,
  end_date_block_account date,
  CONSTRAINT id PRIMARY KEY (id),
  CONSTRAINT id_privilige FOREIGN KEY (id_privilige)
      REFERENCES public.types_privilige (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT id_type_block_account FOREIGN KEY (id_type_block_account)
      REFERENCES public.types_block_account (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT login UNIQUE (login)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.users
  OWNER TO postgres;
COMMENT ON COLUMN public.users.id IS '';

