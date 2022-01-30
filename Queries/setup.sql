-- This script was generated by a beta version of the ERD tool in pgAdmin 4.
-- Please log an issue at https://redmine.postgresql.org/projects/pgadmin4/issues/new if you find any bugs, including reproduction steps.
BEGIN;

CREATE TABLE IF NOT EXISTS public.book
(
    isbn character varying NOT NULL,
    name character varying NOT NULL,
    description character varying,
    price double precision,
    genre character varying,
    publisher character varying,
    published_date date,
    copies_sold integer NOT NULL DEFAULT 0,
    seller character varying,
    author_id bigint,
    PRIMARY KEY (isbn)
);

CREATE TABLE IF NOT EXISTS public.rating
(
    id bigserial NOT NULL,
    user_id bigint NOT NULL,
    stars smallint NOT NULL DEFAULT 0,
    comment character varying,
    "timestamp" timestamp with time zone DEFAULT current_timestamp,
    book_isbn character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.author
(
    id bigserial NOT NULL,
    first_name character varying NOT NULL,
    last_name character varying,
    biography character varying,
    publisher character varying,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public."user"
(
    id bigserial NOT NULL,
    username character varying NOT NULL,
    password character varying NOT NULL,
    name character varying,
    email character varying,
    home_address character varying,
	PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.credit_card
(
    id bigserial NOT NULL,
    user_id bigint NOT NULL,
    name character varying NOT NULL,
    "number" character varying NOT NULL,
    expire_date date NOT NULL,
    pin character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.cart
(
    id bigserial NOT NULL,
    user_id bigint NOT NULL,
    book_isbn character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.wish_list
(
    id bigserial NOT NULL,
    user_id bigint NOT NULL,
    name character varying NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.author_book
(
    author_id bigint NOT NULL,
    book_isbn character varying NOT NULL,
    PRIMARY KEY (author_id, book_isbn)
);

CREATE TABLE IF NOT EXISTS public.book_wish_list
(
    wishlist_id bigint NOT NULL,
    book_isbn character varying NOT NULL,
    PRIMARY KEY (wishlist_id, book_isbn)
);

ALTER TABLE IF EXISTS public.book
    ADD FOREIGN KEY (author_id)
    REFERENCES public.author (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.rating
    ADD FOREIGN KEY (book_isbn)
    REFERENCES public.book (isbn) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.rating
    ADD FOREIGN KEY (user_id)
    REFERENCES public."user" (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.credit_card
    ADD FOREIGN KEY (user_id)
    REFERENCES public."user" (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.cart
    ADD FOREIGN KEY (user_id)
    REFERENCES public."user" (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.cart
    ADD FOREIGN KEY (book_isbn)
    REFERENCES public.book (isbn) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.wish_list
    ADD FOREIGN KEY (user_id)
    REFERENCES public."user" (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.author_book
    ADD FOREIGN KEY (author_id)
    REFERENCES public.author (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.author_book
    ADD FOREIGN KEY (book_isbn)
    REFERENCES public.book (isbn) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.book_wish_list
    ADD FOREIGN KEY (wishlist_id)
    REFERENCES public.wish_list (id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.book_wish_list
    ADD FOREIGN KEY (book_isbn)
    REFERENCES public.book (isbn) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;

END;