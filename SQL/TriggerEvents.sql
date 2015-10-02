DECLARE @event_type nvarchar(10)

IF EXISTS(SELECT * FROM inserted)
IF EXISTS(SELECT * FROM deleted)
SELECT @event_type = 'update'
ELSE
SELECT @event_type = 'insert'
ELSE
IF EXISTS(SELECT * FROM deleted)
SELECT @event_type = 'delete'
ELSE
--no rows affected - cannot determine event
SELECT @event_type = 'unknown'
