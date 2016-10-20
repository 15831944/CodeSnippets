DECLARE @Deleted_Rows INT;
SET @Deleted_Rows = 1;


WHILE (@Deleted_Rows > 0)
  BEGIN

   BEGIN TRANSACTION

   -- Delete some small number of rows at a time
     DELETE TOP (10000) LargeTable 
     WHERE IsDeleted = 1
	 
	 SET @Deleted_Rows = @@ROWCOUNT;
   COMMIT TRANSACTION
   CHECKPOINT -- for simple recovery model
END
