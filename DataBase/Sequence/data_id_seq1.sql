-- Sequence: public.data_id_seq1

-- DROP SEQUENCE public.data_id_seq1;

CREATE SEQUENCE public.data_id_seq1
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE public.data_id_seq1
  OWNER TO postgres;
