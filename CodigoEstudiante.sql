USE CodigoEstudiante_db;

SELECT * FROM CATEGORY;
SELECT * FROM PRODUCT;
SELECT * FROM [User];
SELECT * FROM [Order];
SELECT * FROM OrderItem;


INSERT INTO Product (CategoryId, Name, Description, Price, Stock, ImageName)
VALUES (1, 'asdad', 'asdad', 12, 2, 'asdasd');

insert into [User] (Fullname, Email,password,type)
values ('Jazzgod', 'jazz@gmail.com', '123','Cleinte');