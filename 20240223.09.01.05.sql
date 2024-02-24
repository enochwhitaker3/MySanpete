--
-- PostgreSQL database dump
--

-- Dumped from database version 12.17
-- Dumped by pg_dump version 16.1 (Debian 16.1-1.pgdg120+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: mysanpete; Type: SCHEMA; Schema: -; Owner: parkerswenson_25
--

CREATE SCHEMA mysanpete;


ALTER SCHEMA mysanpete OWNER TO parkerswenson_25;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: blog; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.blog (
    id integer NOT NULL,
    title text NOT NULL,
    blog_content text NOT NULL,
    publish_date timestamp with time zone DEFAULT now(),
    author_id integer NOT NULL,
    commentable boolean DEFAULT true NOT NULL,
    photo bytea
);


ALTER TABLE mysanpete.blog OWNER TO parkerswenson_25;

--
-- Name: blog_comment; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.blog_comment (
    id integer NOT NULL,
    comment_id integer NOT NULL,
    blog_id integer NOT NULL
);


ALTER TABLE mysanpete.blog_comment OWNER TO parkerswenson_25;

--
-- Name: blog_comment_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.blog_comment ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.blog_comment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: blog_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.blog ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.blog_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: blog_reaction; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.blog_reaction (
    id integer NOT NULL,
    blog_id integer NOT NULL,
    reaction_id integer NOT NULL
);


ALTER TABLE mysanpete.blog_reaction OWNER TO parkerswenson_25;

--
-- Name: blog_reaction_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.blog_reaction ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.blog_reaction_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: business; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.business (
    id integer NOT NULL,
    business_name text NOT NULL,
    address text NOT NULL,
    logo bytea
);


ALTER TABLE mysanpete.business OWNER TO parkerswenson_25;

--
-- Name: business_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.business ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.business_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: end_user; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.end_user (
    id integer NOT NULL,
    user_name character varying(80),
    user_role_id integer NOT NULL,
    photo bytea,
    user_email text NOT NULL
);


ALTER TABLE mysanpete.end_user OWNER TO parkerswenson_25;

--
-- Name: end_user_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.end_user ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.end_user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: occasion; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.occasion (
    id integer NOT NULL,
    title text,
    x_coordinate numeric,
    y_coordinate numeric,
    start_date timestamp with time zone NOT NULL,
    end_date timestamp with time zone,
    business_id integer NOT NULL,
    description text,
    photo bytea
);


ALTER TABLE mysanpete.occasion OWNER TO parkerswenson_25;

--
-- Name: occasion_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.occasion ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.occasion_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: podcast; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.podcast (
    id integer NOT NULL,
    podcast_name text NOT NULL,
    podcast_url text NOT NULL,
    commentable boolean DEFAULT true NOT NULL
);


ALTER TABLE mysanpete.podcast OWNER TO parkerswenson_25;

--
-- Name: podcast_comment; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.podcast_comment (
    id integer NOT NULL,
    comment_id integer NOT NULL,
    podcast_id integer NOT NULL
);


ALTER TABLE mysanpete.podcast_comment OWNER TO parkerswenson_25;

--
-- Name: podcast_comment_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.podcast_comment ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.podcast_comment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: podcast_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.podcast ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.podcast_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: podcast_reaction; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.podcast_reaction (
    id integer NOT NULL,
    podcast_id integer NOT NULL,
    reaction_id integer NOT NULL
);


ALTER TABLE mysanpete.podcast_reaction OWNER TO parkerswenson_25;

--
-- Name: podcast_reaction_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.podcast_reaction ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.podcast_reaction_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: reaction; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.reaction (
    id integer NOT NULL,
    unicode text NOT NULL
);


ALTER TABLE mysanpete.reaction OWNER TO parkerswenson_25;

--
-- Name: reaction_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.reaction ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.reaction_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: review; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.review (
    id integer NOT NULL,
    business_id integer NOT NULL,
    user_id integer NOT NULL,
    review_text text NOT NULL,
    stars integer NOT NULL,
    CONSTRAINT review_stars_check CHECK (((stars > 0) AND (stars < 6)))
);


ALTER TABLE mysanpete.review OWNER TO parkerswenson_25;

--
-- Name: review_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.review ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.review_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_comment; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.user_comment (
    id integer NOT NULL,
    reply_id integer,
    user_id integer NOT NULL,
    comment_text text NOT NULL,
    post_date timestamp with time zone DEFAULT now()
);


ALTER TABLE mysanpete.user_comment OWNER TO parkerswenson_25;

--
-- Name: user_comment_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.user_comment ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.user_comment_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_occasion; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.user_occasion (
    id integer NOT NULL,
    user_id integer NOT NULL,
    occasion_id integer NOT NULL
);


ALTER TABLE mysanpete.user_occasion OWNER TO parkerswenson_25;

--
-- Name: user_occasion_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.user_occasion ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.user_occasion_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_role; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.user_role (
    id integer NOT NULL,
    role_name text NOT NULL
);


ALTER TABLE mysanpete.user_role OWNER TO parkerswenson_25;

--
-- Name: user_role_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.user_role ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.user_role_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_vourcher; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.user_vourcher (
    id integer NOT NULL,
    user_id integer NOT NULL,
    voucher_id integer NOT NULL,
    final_price money
);


ALTER TABLE mysanpete.user_vourcher OWNER TO parkerswenson_25;

--
-- Name: user_vourcher_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.user_vourcher ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.user_vourcher_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: voucher; Type: TABLE; Schema: mysanpete; Owner: parkerswenson_25
--

CREATE TABLE mysanpete.voucher (
    id integer NOT NULL,
    business_id integer NOT NULL,
    start_date timestamp with time zone,
    end_date timestamp with time zone,
    promo_code text NOT NULL,
    promo_name text NOT NULL,
    promo_description text NOT NULL,
    promo_stock integer,
    retail_price money,
    total_reclaimable integer
);


ALTER TABLE mysanpete.voucher OWNER TO parkerswenson_25;

--
-- Name: voucher_id_seq; Type: SEQUENCE; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE mysanpete.voucher ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME mysanpete.voucher_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: blog; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.blog (id, title, blog_content, publish_date, author_id, commentable, photo) FROM stdin;
\.


--
-- Data for Name: blog_comment; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.blog_comment (id, comment_id, blog_id) FROM stdin;
\.


--
-- Data for Name: blog_reaction; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.blog_reaction (id, blog_id, reaction_id) FROM stdin;
\.


--
-- Data for Name: business; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.business (id, business_name, address, logo) FROM stdin;
\.


--
-- Data for Name: end_user; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.end_user (id, user_name, user_role_id, photo, user_email) FROM stdin;
\.


--
-- Data for Name: occasion; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.occasion (id, title, x_coordinate, y_coordinate, start_date, end_date, business_id, description, photo) FROM stdin;
\.


--
-- Data for Name: podcast; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.podcast (id, podcast_name, podcast_url, commentable) FROM stdin;
\.


--
-- Data for Name: podcast_comment; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.podcast_comment (id, comment_id, podcast_id) FROM stdin;
\.


--
-- Data for Name: podcast_reaction; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.podcast_reaction (id, podcast_id, reaction_id) FROM stdin;
\.


--
-- Data for Name: reaction; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.reaction (id, unicode) FROM stdin;
\.


--
-- Data for Name: review; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.review (id, business_id, user_id, review_text, stars) FROM stdin;
\.


--
-- Data for Name: user_comment; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.user_comment (id, reply_id, user_id, comment_text, post_date) FROM stdin;
\.


--
-- Data for Name: user_occasion; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.user_occasion (id, user_id, occasion_id) FROM stdin;
\.


--
-- Data for Name: user_role; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.user_role (id, role_name) FROM stdin;
\.


--
-- Data for Name: user_vourcher; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.user_vourcher (id, user_id, voucher_id, final_price) FROM stdin;
\.


--
-- Data for Name: voucher; Type: TABLE DATA; Schema: mysanpete; Owner: parkerswenson_25
--

COPY mysanpete.voucher (id, business_id, start_date, end_date, promo_code, promo_name, promo_description, promo_stock, retail_price, total_reclaimable) FROM stdin;
\.


--
-- Name: blog_comment_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.blog_comment_id_seq', 1, false);


--
-- Name: blog_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.blog_id_seq', 1, false);


--
-- Name: blog_reaction_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.blog_reaction_id_seq', 1, false);


--
-- Name: business_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.business_id_seq', 1, false);


--
-- Name: end_user_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.end_user_id_seq', 1, false);


--
-- Name: occasion_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.occasion_id_seq', 1, false);


--
-- Name: podcast_comment_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.podcast_comment_id_seq', 1, false);


--
-- Name: podcast_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.podcast_id_seq', 1, false);


--
-- Name: podcast_reaction_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.podcast_reaction_id_seq', 1, false);


--
-- Name: reaction_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.reaction_id_seq', 1, false);


--
-- Name: review_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.review_id_seq', 1, false);


--
-- Name: user_comment_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.user_comment_id_seq', 1, false);


--
-- Name: user_occasion_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.user_occasion_id_seq', 1, false);


--
-- Name: user_role_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.user_role_id_seq', 1, false);


--
-- Name: user_vourcher_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.user_vourcher_id_seq', 1, false);


--
-- Name: voucher_id_seq; Type: SEQUENCE SET; Schema: mysanpete; Owner: parkerswenson_25
--

SELECT pg_catalog.setval('mysanpete.voucher_id_seq', 1, false);


--
-- Name: blog_comment blog_comment_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog_comment
    ADD CONSTRAINT blog_comment_pkey PRIMARY KEY (id);


--
-- Name: blog blog_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog
    ADD CONSTRAINT blog_pkey PRIMARY KEY (id);


--
-- Name: blog_reaction blog_reaction_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog_reaction
    ADD CONSTRAINT blog_reaction_pkey PRIMARY KEY (id);


--
-- Name: business business_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.business
    ADD CONSTRAINT business_pkey PRIMARY KEY (id);


--
-- Name: end_user end_user_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.end_user
    ADD CONSTRAINT end_user_pkey PRIMARY KEY (id);


--
-- Name: occasion occasion_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.occasion
    ADD CONSTRAINT occasion_pkey PRIMARY KEY (id);


--
-- Name: podcast_comment podcast_comment_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast_comment
    ADD CONSTRAINT podcast_comment_pkey PRIMARY KEY (id);


--
-- Name: podcast podcast_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast
    ADD CONSTRAINT podcast_pkey PRIMARY KEY (id);


--
-- Name: podcast_reaction podcast_reaction_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast_reaction
    ADD CONSTRAINT podcast_reaction_pkey PRIMARY KEY (id);


--
-- Name: reaction reaction_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.reaction
    ADD CONSTRAINT reaction_pkey PRIMARY KEY (id);


--
-- Name: review review_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.review
    ADD CONSTRAINT review_pkey PRIMARY KEY (id);


--
-- Name: user_comment user_comment_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_comment
    ADD CONSTRAINT user_comment_pkey PRIMARY KEY (id);


--
-- Name: user_occasion user_occasion_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_occasion
    ADD CONSTRAINT user_occasion_pkey PRIMARY KEY (id);


--
-- Name: user_role user_role_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_role
    ADD CONSTRAINT user_role_pkey PRIMARY KEY (id);


--
-- Name: user_vourcher user_vourcher_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_vourcher
    ADD CONSTRAINT user_vourcher_pkey PRIMARY KEY (id);


--
-- Name: voucher voucher_pkey; Type: CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.voucher
    ADD CONSTRAINT voucher_pkey PRIMARY KEY (id);


--
-- Name: blog blog_author_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog
    ADD CONSTRAINT blog_author_id_fkey FOREIGN KEY (author_id) REFERENCES mysanpete.end_user(id);


--
-- Name: blog_comment blog_comment_blog_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog_comment
    ADD CONSTRAINT blog_comment_blog_id_fkey FOREIGN KEY (blog_id) REFERENCES mysanpete.blog(id);


--
-- Name: blog_comment blog_comment_comment_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog_comment
    ADD CONSTRAINT blog_comment_comment_id_fkey FOREIGN KEY (comment_id) REFERENCES mysanpete.user_comment(id);


--
-- Name: blog_reaction blog_reaction_blog_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog_reaction
    ADD CONSTRAINT blog_reaction_blog_id_fkey FOREIGN KEY (blog_id) REFERENCES mysanpete.blog(id);


--
-- Name: blog_reaction blog_reaction_reaction_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.blog_reaction
    ADD CONSTRAINT blog_reaction_reaction_id_fkey FOREIGN KEY (reaction_id) REFERENCES mysanpete.reaction(id);


--
-- Name: end_user end_user_user_role_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.end_user
    ADD CONSTRAINT end_user_user_role_id_fkey FOREIGN KEY (user_role_id) REFERENCES mysanpete.user_role(id);


--
-- Name: occasion occasion_business_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.occasion
    ADD CONSTRAINT occasion_business_id_fkey FOREIGN KEY (business_id) REFERENCES mysanpete.business(id);


--
-- Name: podcast_comment podcast_comment_comment_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast_comment
    ADD CONSTRAINT podcast_comment_comment_id_fkey FOREIGN KEY (comment_id) REFERENCES mysanpete.user_comment(id);


--
-- Name: podcast_comment podcast_comment_podcast_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast_comment
    ADD CONSTRAINT podcast_comment_podcast_id_fkey FOREIGN KEY (podcast_id) REFERENCES mysanpete.podcast(id);


--
-- Name: podcast_reaction podcast_reaction_podcast_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast_reaction
    ADD CONSTRAINT podcast_reaction_podcast_id_fkey FOREIGN KEY (podcast_id) REFERENCES mysanpete.podcast(id);


--
-- Name: podcast_reaction podcast_reaction_reaction_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.podcast_reaction
    ADD CONSTRAINT podcast_reaction_reaction_id_fkey FOREIGN KEY (reaction_id) REFERENCES mysanpete.reaction(id);


--
-- Name: review review_business_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.review
    ADD CONSTRAINT review_business_id_fkey FOREIGN KEY (business_id) REFERENCES mysanpete.business(id);


--
-- Name: review review_user_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.review
    ADD CONSTRAINT review_user_id_fkey FOREIGN KEY (user_id) REFERENCES mysanpete.end_user(id);


--
-- Name: user_comment user_comment_reply_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_comment
    ADD CONSTRAINT user_comment_reply_id_fkey FOREIGN KEY (reply_id) REFERENCES mysanpete.user_comment(id);


--
-- Name: user_comment user_comment_user_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_comment
    ADD CONSTRAINT user_comment_user_id_fkey FOREIGN KEY (user_id) REFERENCES mysanpete.end_user(id);


--
-- Name: user_occasion user_occasion_occasion_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_occasion
    ADD CONSTRAINT user_occasion_occasion_id_fkey FOREIGN KEY (occasion_id) REFERENCES mysanpete.occasion(id);


--
-- Name: user_occasion user_occasion_user_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_occasion
    ADD CONSTRAINT user_occasion_user_id_fkey FOREIGN KEY (user_id) REFERENCES mysanpete.end_user(id);


--
-- Name: user_vourcher user_vourcher_user_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_vourcher
    ADD CONSTRAINT user_vourcher_user_id_fkey FOREIGN KEY (user_id) REFERENCES mysanpete.end_user(id);


--
-- Name: user_vourcher user_vourcher_voucher_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.user_vourcher
    ADD CONSTRAINT user_vourcher_voucher_id_fkey FOREIGN KEY (voucher_id) REFERENCES mysanpete.voucher(id);


--
-- Name: voucher voucher_business_id_fkey; Type: FK CONSTRAINT; Schema: mysanpete; Owner: parkerswenson_25
--

ALTER TABLE ONLY mysanpete.voucher
    ADD CONSTRAINT voucher_business_id_fkey FOREIGN KEY (business_id) REFERENCES mysanpete.business(id);


--
-- PostgreSQL database dump complete
--

