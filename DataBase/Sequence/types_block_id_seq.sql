-- Sequence: public.types_block_id_seq

-- DROP SEQUENCE public.types_block_id_seq;

CREATE SEQUENCE public.types_block_id_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 10
  CACHE 1;
ALTER TABLE public.types_block_id_seq
  OWNER TO postgres;
