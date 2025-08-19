CREATE TABLE EmployPrc
(
	Eno INT PRIMARY KEY,
	Name VARCHAR(30),
	Gender VARCHAR(10) CONSTRAINT chk_dummy_gen CHECK (Gender IN ('MALE','FEMALE')),
	SALARY NUMERIC(9,2) CONSTRAINT ch_dummy_sal CHECK (Salary BETWEEN 10000 AND 80000)
);
GO

CREATE PROCEDURE PrcEmpInsNew
		@eno INT,
		@nam VARCHAR(30),
		@gen VARCHAR(10),
		@bas NUMERIC(9,2)
AS
BEGIN
	DECLARE @erNo INT;

	BEGIN TRY
		INSERT INTO EmployPrc VALUES(@eno, @nam, @gen, @bas);
		PRINT 'Inserted Successfully';
	END TRY
	BEGIN CATCH
		SET @erNo = ERROR_NUMBER();
		PRINT 'Error Number: ' + CONVERT(VARCHAR, @erNo);
		PRINT 'Error Message: ' + ERROR_MESSAGE();
		PRINT 'Error Severity: ' + Convert(VARCHAR, ERROR_SEVERITY());
		PRINT 'Error Line: ' + CONVERT(VARCHAR, ERROR_LINE());
	END CATCH
END;
GO

EXEC PrcEmpInsNew 3, 'Madhu', 'MALE', 50000;
