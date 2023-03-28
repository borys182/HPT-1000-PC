-- Table: public.connections_program_subprograms

-- DROP TABLE public.connections_program_subprograms;

CREATE TABLE public.connections_program_subprograms
(
  id_program integer NOT NULL,
  id_subprogram integer NOT NULL,
  CONSTRAINT connections_program_subprograms_pkey PRIMARY KEY (id_program, id_subprogram)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.connections_program_subprograms
  OWNER TO postgres;
