--
-- DROP CONSTRAINTS 
--
SELECT 'ALTER TABLE ' + '[' + OBJECT_NAME(f.parent_object_id)+ ']'+
' DROP  CONSTRAINT ' + '[' + f.name  + ']'
FROM .sys.foreign_keys AS f
INNER JOIN .sys.foreign_key_columns AS fc
ON f.OBJECT_ID = fc.constraint_object_id
where f.referenced_object_id = object_id('tsoftproductlicencelog')
or f.referenced_object_id = object_id('tsoftproductorder')
or f.referenced_object_id = object_id('tsoftproductorderlog')

-- 
-- RECREATE CONSTRAINTS
--
SELECT 'ALTER TABLE [' + OBJECT_NAME(f.parent_object_id)+ ']' +
' ADD CONSTRAINT ' + '[' +  f.name  +']'+ ' FOREIGN KEY'+'('+COL_NAME(fc.parent_object_id,fc.parent_column_id)+')'
+'REFERENCES ['+OBJECT_NAME (f.referenced_object_id)+']('+COL_NAME(fc.referenced_object_id,
fc.referenced_column_id)+')' as Scripts
FROM .sys.foreign_keys AS f
INNER JOIN .sys.foreign_key_columns AS fc
ON f.OBJECT_ID = fc.constraint_object_id
where f.referenced_object_id = object_id('tsoftproductlicencelog')
or f.referenced_object_id = object_id('tsoftproductorder')
or f.referenced_object_id = object_id('tsoftproductorderlog')
