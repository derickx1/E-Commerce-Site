
/*
create TRIGGER fill_J_T
on Orders after insert
as 
BEGIN
    --SET NOCOUNT ON;
    DECLARE @Order_ID INT
    DECLARE @Customers_ID INT

    SELECT @Order_ID = Inserted.Order_ID 
    SELECT @Customers_ID = Inserted.Customers_ID
    FROM INSERTED

    INSERT into J_T
    VALUES ( @Customers_ID,@Order_ID )

END

*/