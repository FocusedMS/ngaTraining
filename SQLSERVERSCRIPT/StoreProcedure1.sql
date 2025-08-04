CREATE PROC prcDivision
    @a INT,
    @b INT
AS
BEGIN
    BEGIN TRY
        SET @a = 5;
        SET @b = @a / 0;
        PRINT @b;
    END TRY
    BEGIN CATCH
        PRINT 'Error is: ' + ERROR_MESSAGE();
    END CATCH
END
GO

CREATE PROC prcEvenCheck
    @n INT
AS
BEGIN
    BEGIN TRY
        DECLARE @Res INT;
        SET @Res = @n % 2;

        IF (@Res = 0)
        BEGIN
            PRINT 'NO ERROR';
            PRINT 'EVEN NUMBER';
        END
        ELSE
        BEGIN
            PRINT 'NO ERROR';
            PRINT 'ODD NUMBER';
        END
    END TRY
    BEGIN CATCH
        PRINT 'ERROR Occurred';
        PRINT ERROR_MESSAGE();
        THROW 70000, 'Error Occurred', 1;
    END CATCH
END
GO

EXEC prcEvenCheck @n = 6;