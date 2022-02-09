BEGIN;

CREATE TABLE IF NOT EXISTS public.book
(
    isbn VARCHAR(13) PRIMARY KEY NOT NULL,
    name VARCHAR(20) NOT NULL,
    description VARCHAR(256),
    price DOUBLE PRECISION,
    genre VARCHAR(20),
    publisher VARCHAR(20),
    published_date DATE,
    copies_sold INTEGER DEFAULT 0,
    seller VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS public."user"
(
	id           SERIAL PRIMARY KEY NOT NULL,
    username     VARCHAR(100) NOT NULL,
    password     VARCHAR(100) NOT NULL,
    name         VARCHAR(24),
    email        VARCHAR(64),
    home_address VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS public.rating
(
    id SERIAL PRIMARY KEY NOT NULL,
    user_id SERIAL NOT NULL,
    isbn VARCHAR(13) NOT NULL,
    stars SMALLINT NOT NULL,
    comment VARCHAR(256),
    "timestamp" TIMESTAMP WITH TIME ZONE DEFAULT current_timestamp
);

CREATE TABLE IF NOT EXISTS public.author
(
    id SERIAL PRIMARY KEY NOT NULL,
    first_name VARCHAR(20) NOT NULL,
    last_name VARCHAR(20) NOT NULL,
    biography VARCHAR(256),
    publisher VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS public.author_book
(
    author_id SERIAL NOT NULL,
    book_isbn VARCHAR(13) NOT NULL,
    PRIMARY KEY (author_id, book_isbn)
);

CREATE TABLE IF NOT EXISTS public.credit_card
(
    id SERIAL PRIMARY KEY NOT NULL,
    user_id SERIAL NOT NULL,
    name VARCHAR(20) NOT NULL,
    "number" VARCHAR(16) NOT NULL,
    expire_date DATE NOT NULL,
    pin VARCHAR(4) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.cart
(
    id SERIAL PRIMARY KEY NOT NULL,
    user_id SERIAL NOT NULL,
    book_isbn VARCHAR(13) NOT NULL,
	quantity INT NOT NULL
);

CREATE TABLE IF NOT EXISTS public.wish_list
(
    id SERIAL PRIMARY KEY NOT NULL,
	user_id SERIAL NOT NULL,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.wish_list_book
(
    wishlist_id SERIAL NOT NULL,
    book_id VARCHAR(13) NOT NULL,
    PRIMARY KEY (wishlist_id, book_id)
);


-- ALTER TABLE IF EXISTS public.rating
--     ADD FOREIGN KEY (book_isbn)
--     REFERENCES public.book (isbn) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.rating
--     ADD FOREIGN KEY (user_id)
--     REFERENCES public."user" (id) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.credit_card
--     ADD FOREIGN KEY (user_id)
--     REFERENCES public."user" (id) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.cart
--     ADD FOREIGN KEY (user_id)
--     REFERENCES public."user" (id) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.cart
--     ADD FOREIGN KEY (book_isbn)
--     REFERENCES public.book (isbn) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.wish_list
--     ADD FOREIGN KEY (user_id)
--     REFERENCES public."user" (id) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.author_book
--     ADD FOREIGN KEY (author_id)
--     REFERENCES public.author (id) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.author_book
--     ADD FOREIGN KEY (book_isbn)
--     REFERENCES public.book (isbn) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.book_wish_list
--     ADD FOREIGN KEY (wishlist_id)
--     REFERENCES public.wish_list (id) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;


-- ALTER TABLE IF EXISTS public.book_wish_list
--     ADD FOREIGN KEY (book_isbn)
--     REFERENCES public.book (isbn) MATCH SIMPLE
--     ON UPDATE CASCADE
--     ON DELETE CASCADE
--     NOT VALID;

END;