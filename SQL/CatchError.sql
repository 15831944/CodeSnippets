begin catch
	rollback
	declare @errorMessage nvarchar(max)
	
	select @errorMessage = 'ERROR_NUMBER: ' + ISNULL(CONVERT(nvarchar(max), ERROR_NUMBER()), '') + '<br />' +
			'ERROR_SEVERITY: ' + ISNULL(CONVERT(nvarchar(max), ERROR_SEVERITY()), '') + '<br />' +
			'ERROR_STATE: ' + ISNULL(CONVERT(nvarchar(max), ERROR_STATE()), '') + '<br />' +
			'ERROR_PROCEDURE: ' + ISNULL(ERROR_PROCEDURE(), '') + '<br />' +
			'ERROR_LINE: ' + ISNULL(CONVERT(nvarchar(max), ERROR_LINE()), '') + '<br />' +
			'ERROR_MESSAGE: ' + ISNULL(ERROR_MESSAGE(), '');
    
    select @errorMessage;
    
	--EXEC msdb.dbo.sp_send_dbmail
	--@profile_name = 'Job Status Notification',
	--@recipients = 'david.veseli@smartit.bg',
	--@from_address = '.......',
	--@subject = 'calculation error',
	--@body_format = 'HTML',
	--@body = @errorMessage;
end catch
