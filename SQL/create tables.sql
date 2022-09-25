




drop table J_T;
drop table Reviews;
Drop table Orders;
Drop table Cred;
drop table Customers;
DROP TABLE Jewelry;

CREATE TABLE Customers   
(Customer_ID INT IDENTITY Not NULL UNIQUE,    
CName NVARCHAR(16) NOT NULL,       
Shipping_address NVARCHAR(200) NOT NULL,  
PRIMARY KEY (Customer_ID));

CREATE TABLE Cred
(Cred_ID  INT IDENTITY Not NULL UNIQUE,
userN VARCHAR(30) Not NULL UNIQUE,
Pass VARCHAR(30) Not NULL UNIQUE,
Customer_ID INT NOT NULL,
PRIMARY KEY (Cred_ID),
FOREIGN KEY (Customer_ID) REFERENCES Customers(Customer_ID));

CREATE TABLE Orders 
(Order_ID INT IDENTITY NOT NULL UNIQUE,    
Customer_ID INT Not NULL,   
Order_Date DATETIME NOT NULL,
PRIMARY KEY (Order_ID),
FOREIGN KEY (Customer_ID) REFERENCES Customers(Customer_ID));


CREATE TABLE Jewelry   
(Item_ID INT IDENTITY Not NULL UNIQUE,    
Item_name NVARCHAR(16) NOT NULL,    
Price Float NOT NULL,    
Material NVARCHAR(30) NOT NULL,    
item_Type NVARCHAR(30) NOT NULL,
Img_url NVARCHAR(200) NULL,
PRIMARY KEY (Item_ID));


CREATE TABLE J_T
(Jewelry_ID INT IDENTITY NoT NULL UNIQUE,
Customers_ID INT Not NULL,
Order_ID INT NOT NULL ,    
Item_ID INT NOT NULL,
PRIMARY KEY (Jewelry_ID),
FOREIGN KEY (Customers_ID) REFERENCES Customers(Customer_ID),
FOREIGN KEY (Order_ID) REFERENCES Orders(Order_ID),
FOREIGN KEY (Item_ID) REFERENCES Jewelry(Item_ID));


CREATE TABLE Reviews  
(Reviews_ID INT IDENTITY Not NULL UNIQUE,    
Customer_ID INT Not NULL,  
Item_ID INT NOT NULL,  
Review_date DATETIME NOT NULL,    
content Text NULL,    
rating TINYINT NULL,
PRIMARY KEY (Reviews_ID),
FOREIGN KEY (Customer_ID) REFERENCES Customers(Customer_ID),
FOREIGN KEY (Item_ID) REFERENCES Jewelry(Item_ID));

/* */