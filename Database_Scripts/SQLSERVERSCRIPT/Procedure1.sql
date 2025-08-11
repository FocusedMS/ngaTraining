if exists(select * from sysobjects where name='PrcSayHello')
Drop Proc PrcSayHello
Go
Create Proc prcSayhello
AS
BEGIN
	PRINT 'Welcome to T-Sql'
END
Go

EXEC prcSayhello
Go