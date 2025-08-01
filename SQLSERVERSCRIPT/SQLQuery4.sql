select GETDATE()
Go

select convert(varchar, GETDATE(),1)
Go

select convert(varchar, GETDATE(),2)
Go

select convert(varchar, GETDATE(),101)
Go

select convert(varchar, GETDATE(),103)
Go

select DATEPART(dd,getDate())
select DATEPART(mm,getDate())
select DATEPART(yy,getDate())
select DATEPART(hh,getDate())
select datepart(mi,getdate())
select datepart(ss,getdate())
select datepart(ms,getdate())
select datepart(dw,getdate())
select datepart(qq,getdate())

select DATENAME(dw,getDate())

select convert(varchar,DATEPART(dd,getdate())) + '/' + 
convert(varchar,datepart(mm,getdate())) + '/' + 
convert(varchar,DATEPART(yy,getdate()))
GO

select DATENAME(mm, getdate())
GO


select DATEADD(dd,3,getdate())

select dateadd(mm,3,getdate())

select DATEADD(yy,3,getdate())


select DATEDIFF(dd,'03/09/1980',getdate())
select DATEDIFF(yy,'03/09/1980',getdate())