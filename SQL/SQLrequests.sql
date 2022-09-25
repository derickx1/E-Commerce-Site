
--List Jewelry
select Item_name FROM Jewelry;

-- Get JewelryDetails
SELECT * from Jewelry where Item_ID = 1;

--List Jewelry Reviews 
select Customer_ID,Review_date,content,rating from Reviews where Item_ID = 1;

--List Customer’s Reviews
select Review_date,content,rating, Item_ID from Reviews where Customer_ID = 1;

-- Make/Add Review
/*
INSERT into Reviews VALUES
(@Reviews_ID,@Customer_ID,@Item_ID,@Review_date,@content,@rating);
*/

--Get User Profile
SELECT CName, Shipping_address FROM Customers WHERE Customer_ID = 1; 

--Modify User Profile

update Customers
set
Shipping_address = '@Shipping_address'
where Customer_ID = 1;


--INSERT into Customers VALUES(@Customer_ID,@CName,@Shipping_address);

--INSERT into Orders VALUES(@Order_ID,@Customer_ID,@Order_Date );

--INSERT into Jewelry VALUES (@Item_ID,@Item_name,@Price,@Material,@item_Type,@Img_url);

--INSERT INTO J_T VALUES(@Jewelry_ID,Customers_ID,@Order_ID,@Item_ID);

--Change a review

update Reviews
set
content = '@newContent',
rating = '@newRating'
where Reviews_ID = 1;


--Orders for customer X
SELECT Orders.Order_ID, Orders.Order_Date, J_T.Jewelry_ID, Jewelry.Item_name, Jewelry.Price 
FROM Orders join J_T ON Orders.Order_ID = J_T.Order_ID join Jewelry on Jewelry.Item_ID = J_T.Item_ID WHERE Customer_ID = 1;


--Reviews for item X
select Customer_ID, Review_date,content,rating from Reviews where Item_ID = 2;


select Customer_ID from cred WHERE userN = 'user' and Pass= 'passw';

SELECT * from Jewelry ORDER BY Item_ID OFFSET 0  ROWS FETCH NEXT 9 rows ONLY;

--test insert
/*

INSERT into Customers VALUES('Cj','buckhead atlanta'),('richard','Tampa Floraida')


INSERT into Orders VALUES(1,'08/08/2022'),(2,'08/08/2022');

INSERT into Jewelry VALUES ('pearls',2000,'seastone','necklace','https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fae01.alicdn.com%2Fkf%2FHTB1uufYbrsTMeJjSszdq6AEupXa6%2F8-9mm-Genuine-White-Cultured-Freshwater-Pearl-Bead-Necklace.jpg&f=1&nofb=1'),('Diamond Ring',5000,'Diamond','ring','Img_url2');

INSERT into Jewelry VALUES ('cufflinks',200,'metal','accessory','https://upload.wikimedia.org/wikipedia/commons/b/bb/Cufflinks-old_hg.jpg'),('Pendant',500,'Amber','necklace','https://upload.wikimedia.org/wikipedia/commons/4/41/Amber.pendants.800pix.050203.jpg');

INSERT into Jewelry VALUES ('Brooch',1500,'Silver','accessory','https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Wing_Brooch_MET_DT108.jpg/330px-Wing_Brooch_MET_DT108.jpg'),('Gold Ring',935,'Gold','ring','https://thejoue.com/wp-content/uploads/2021/04/Chopard-18kt-rose-gold-Ice-Cube-Pure-diamond-ring-min-800x497.jpg');

INSERT into Jewelry VALUES 
('Diamond Necklace',2500,'Diamond','necklace','https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2F4.bp.blogspot.com%2F_Q5JWnk9IbJo%2FTOL6uDouKPI%2FAAAAAAAABRE%2Fwp7hIpV7B80%2Fs1600%2F00525%2BNK%2B103.60%2BCarat%2BDiamond%2BNecklace.jpg&f=1&nofb=1'),
('Emerald earing',1900,'Emerald','accessory','https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fww1.prweb.com%2Fprfiles%2F2019%2F04%2F29%2F16275483%2FERG0070.jpg&f=1&nofb=1');



INSERT INTO J_T VALUES(1,1,1),(1,1,2),(2,2,2);

INSERT into Reviews VALUES(1,1,'08/08/2022','cool stuff',5),(1,2,'08/08/2022','exspensive stuff',4),(2,2,'08/08/2022','thank you',5);

INSERT INTO Cred VALUES('user','passw',1),('user1','passw2',2);



*/

-- 10 jewls per type
--