--select * from tDevTask order by cdate asc

insert into tDevTask (OrderID, Name, Type, Status, EstimationDate, Priority, DeveloperUserID, CUser, CDate)
SELECT
 F1.OrderNumber,
 fod.OrderName as TaskName,
 100094 as Type, 100098 as Status,
 fod.DevEstimation,
 fod.DevPriority,
 su.ID as DeveloperID,
 'FA1888C8-6DBB-43D2-A01E-66AF8BC12315' as CUser,
 '' as CDate
FROM
 (
	SELECT f.OrderNumber,
	cast('<X>'+replace(F.DevelopedBy,',','</X><X>')+'</X>' as XML) as xmlfilter
	from tFOD_OrderAdditional F 
	join tFOD_Order ord on f.OrderNumber = ord.OrderNumber
	where ord.OrderStatus in (100063, 100064, 100065, 100067)
 )as F1
 CROSS APPLY
 ( 
	 SELECT fdata.D.value('.','varchar(50)') as splitdata 
	 FROM f1.xmlfilter.nodes('X') as fdata(D)
 ) O
 left join tSecurityUsers su on LTRIM(RTRIM(O.splitdata)) = su.FullName
 join tFOD_Order fod on fod.OrderNumber = f1.OrderNumber
order by o.splitdata
