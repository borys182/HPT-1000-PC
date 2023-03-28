-- Table: public.types_block_account

-- DROP TABLE public.types_block_account;

CREATE TABLE public.types_block_account
(
  id integer NOT NULL DEFAULT nextval('types_block_id_seq'::regclass),
  name text NOT NULL,
  value integer NOT NULL,
  CONSTRAINT "Types_Block_Account_pkey" PRIMARY KEY (id),
  CONSTRAINT constraint_name UNIQUE (name),
  CONSTRAINT constraint_value UNIQUE (value)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.types_block_account
  OWNER TO postgres;
