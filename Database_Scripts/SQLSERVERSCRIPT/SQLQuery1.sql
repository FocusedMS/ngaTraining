use sqlpractice
Go

select * from INFORMATION_SCHEMA.TABLES
Go

sp_help Emp
Go

select * from Emp
Go

select Empno 'Employ No', nam 'Employ Name', basic 'Salary'
from Emp
Go

select * from Emp
WHERE basic < 50000
Go

select * from Emp
where Dept='Dotnet'
Go

select * from Emp
where nam = 'Swetha'
Go

select * from Emp
where Basic Between 50000 and 90000
Go

select * from Emp
where Basic NOT Between 50000 and 90000
Go

select * from Emp
where dept in('Dotnet', 'Java', 'sql')
Go

select * from Emp
where nam IN('Manu', 'Satish', 'Swapna', 'Smitha', 'Rushi')
Go

select * from Emp where nam like 'S%'
Go

select * from Emp where nam like '%A'
Go

select Dept from Emp
Go

select distinct Dept from Emp
Go

select * from Emp order by nam
Go

select * from Emp order by Basic DESC
Go

select * from Emp order by Dept, Nam
Go

select * from Emp
order by Dept, Nam desc
Go