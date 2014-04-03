// SQL - Extract digits
CREATE FUNCTION GetDigitsFromStr(@thestring VARCHAR(MAX))
RETURNS VARCHAR(MAX)
BEGIN

declare @final varchar(MAX) 
set @final = '' 

select @final = @final + x.thenum 
from 
( 
    select substring(@thestring, number, 1) as thenum, number 
    from master..spt_values 
    where (substring(@thestring, number, 1) like '[0-9]' 
		or substring(@thestring, number, 1) like '/'
		or substring(@thestring, number, 1) like ' ') and type='P'
) x 
order by x.number 

if (@final = '')
begin
	set @final = null
end
return LTRIM(RTRIM(@final))

END
GO
