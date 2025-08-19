

-- drop the procedure if it already exists
if exists (select * from sysobjects where name = 'prcEmployInsert' AND xtype = 'P')
	DROP PROC prcEmployInsert
GO

--Create the procedure
Create PROC prcEmployInsert
	@Name VARCHAR(30),
	@Gender VARCHAR(10),
	@Dept VARCHAR(30),
	@Desig VARCHAR(30),
	@Basic NUMERIC(9,2)
AS
BEGIN
	DECLARE @Empno INT;

	Select @Empno =
		CASE
			WHEN MAX(Empno) IS NULL THEN 1
			ELSE MAX(Empno) + 1
		END
	FROM Employ;

	INSERT INTO Employ (Empno, Name, Gender, Dept, Desig, Basic)
	VALUES (@Empno, @Name, @Gender, @Dept, @Desig, @Basic);