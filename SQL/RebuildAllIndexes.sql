DECLARE @TableName varchar(255)
SET QUOTED_IDENTIFIER ON

declare @D1 datetime
set @D1 = getdate()

DECLARE TableCursor CURSOR FOR
SELECT table_name FROM information_schema.tables
WHERE table_type = 'base table'

OPEN TableCursor

FETCH NEXT FROM TableCursor INTO @TableName
WHILE @@FETCH_STATUS = 0
BEGIN
DBCC DBREINDEX(@TableName,' ',85)
FETCH NEXT FROM TableCursor INTO @TableName
END

CLOSE TableCursor

DEALLOCATE TableCursor


Insert into tNightLog (ID,CDATE,SERVICE,Rezultcode,rezultdesc,FDATE)
select newid(),@D1,'RebildIndexes',0,'no errors',GETDATE()
