-- AUTHOR
INSERT INTO public."author"
values (DEFAULT, 'test_first_name', 'test_last_name', 'test...biography', 'test_publisher');

-- BOOK
INSERT INTO book 
values ('9785321569852', 1, 'Test_Name01', 'Test01_Description', 20.25, 'Test01_Genre', 'Test01_Publisher',
		 CURRENT_DATE, 5, 'Test01_Seller'),
	   ('9788962364875', 1, 'Test_Name02', 'Test02_Description', 120.25, 'Test02_Genre', 'Test02_Publisher',
		 CURRENT_DATE, 10, 'Test02_Seller');
	   ('9785321569853', 1, 'Test_Name03', 'Test03_Description', 20.25, 'Test01_Genre', 'Test01_Publisher',
	     CURRENT_DATE, 5, 'Test01_Seller');

-- USER
INSERT INTO public."user"
values (DEFAULT, 'Test_UserName', '123456789', 'Test Name', 'test@test.com', '1111 ne 158 street');

-- RATING
INSERT INTO public."rating"
values (DEFAULT, 1, '9785321569852', 5, 'One of the best books i have ever read!');

-- AUTHOR_BOOK
INSERT INTO public.author_book
values (1, 1);

-- CREDIT CARD
INSERT INTO public.credit_card
values (DEFAULT, 1, 'test username', '4500356589684523', CURRENT_DATE, '0253');

-- CART
INSERT INTO public.cart
values (DEFAULT, 1, '9785321569852', 3);

-- WISH LIST
INSERT INTO public.wish_list
values (DEFAULT, 1, 'test wish list 01');

-- WISH LIST BOOK
INSERT INTO public.wish_list_book
values (1, '9788962364875'),
	   (1, '9785321569852');