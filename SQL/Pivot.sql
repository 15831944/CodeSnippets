;with cte as
	(select DepartmentID, Value, Row_Number() over (partition by DepartmentID order by Value) as rn
	from tRedFlagTemp)
select DepartmentID, [1] as col1, [2] as col2, [3] as col3
from cte
pivot
(MAX(Value) for rn in ([1], [2], [3])) as pvt;

select DepartmentID, [Column1], [Column2], [Column3], [Column4] from
(
	select DepartmentID, Value, ColumnName = case
		when Nomenclature = 'A319C542-507E-4CB2-A428-37A386689919' then 'Column1'
		when Nomenclature = '63EBC80E-6230-457F-B2A3-7812E4E0C33C' then 'Column2'
		when Nomenclature = '79E57FFC-D62C-4D39-A312-FB3672A2F9CB' then 'Column3'
		when Nomenclature = '0FB16451-68A3-4D2F-A331-36E7F8BBB219' then 'Column4'
	end
	from tRedFlagTemp
) src
pivot
(
	MAX(Value) for ColumnName in ([Column1], [Column2], [Column3], [Column4])
) pvt
