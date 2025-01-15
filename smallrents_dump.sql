--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2 (Debian 17.2-1.pgdg120+1)
-- Dumped by pg_dump version 17.2 (Debian 17.2-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: inserirusuario(integer, character varying, integer, character varying, integer, character varying, character varying, date, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.inserirusuario(IN p_id integer, IN p_nome character varying, IN p_cep integer, IN p_logradouro character varying, IN p_numero integer, IN p_complemento character varying, IN p_sexo character varying, IN p_data_nascimento date, IN p_cpf character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO Usuario (ID, NOME, CEP, LOGRADOURO, NUMERO, COMPLEMENTO, SEXO, DATA_NASCIMENTO, CPF)
    VALUES (p_id, p_nome, p_cep, p_logradouro, p_numero, p_complemento, p_sexo, p_data_nascimento, p_cpf);
END;
$$;


ALTER PROCEDURE public.inserirusuario(IN p_id integer, IN p_nome character varying, IN p_cep integer, IN p_logradouro character varying, IN p_numero integer, IN p_complemento character varying, IN p_sexo character varying, IN p_data_nascimento date, IN p_cpf character varying) OWNER TO postgres;

--
-- Name: usuario_sequence; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.usuario_sequence
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.usuario_sequence OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: usuario; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuario (
    id integer DEFAULT nextval('public.usuario_sequence'::regclass) NOT NULL,
    nome character varying(50) NOT NULL,
    cep integer NOT NULL,
    logradouro character varying(100) NOT NULL,
    numero integer NOT NULL,
    complemento character varying(50),
    sexo character varying(1) NOT NULL,
    data_nascimento date NOT NULL,
    cpf character varying(11)
);


ALTER TABLE public.usuario OWNER TO postgres;

--
-- Data for Name: usuario; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usuario (id, nome, cep, logradouro, numero, complemento, sexo, data_nascimento, cpf) FROM stdin;
1	Israel Durra	9715160	Rua do Israel	44	apto 62	M	2025-01-12	36868303025
3	Teste 3	9715160	Rua teste teste	65	apto 77	M	2025-01-13	69599444060
\.


--
-- Name: usuario_sequence; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.usuario_sequence', 8, true);


--
-- Name: usuario usuario_cpf_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_cpf_key UNIQUE (cpf);


--
-- Name: usuario usuario_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);


--
-- PostgreSQL database dump complete
--

