CREATE TABLE Employ (
    Empno INT PRIMARY KEY,
    Name VARCHAR(30),
    Gender VARCHAR(10),
    Dept VARCHAR(30),
    Desig VARCHAR(30),
    Basic NUMERIC(9,2)
);

INSERT INTO Employ (Empno, Name, Gender, Dept, Desig, Basic) VALUES
(1, 'Norma Fisher', 'Female', 'Marketing', 'Executive', 42945.84),
(2, 'Kayla Sullivan', 'Female', 'Marketing', 'Content Writer', 78390.0),
(3, 'Elizabeth Woods', 'Female', 'Sales', 'Sales Rep', 55234.34),
(4, 'Susan Wagner', 'Female', 'HR', 'Manager', 60918.45),
(5, 'Nicole Montgomery', 'Female', 'Sales', 'Sales Rep', 45507.38),
(6, 'Theodore Mcgrath', 'Male', 'Operations', 'Executive', 53607.14),
(7, 'Steven Collins', 'Male', 'Finance', 'Accountant', 45809.83),
(8, 'Steven Sutton', 'Male', 'Sales', 'Team Lead', 52134.67),
(9, 'Ashlee Hamilton', 'Female', 'IT', 'SysAdmin', 75799.72),
(10, 'Thomas Levy', 'Male', 'Operations', 'Executive', 65512.67),
(11, 'Sean Green', 'Male', 'Sales', 'Team Lead', 71403.16),
(12, 'Kimberly Smith', 'Female', 'HR', 'Recruiter', 65182.13),
(13, 'James Summers', 'Male', 'HR', 'Manager', 41930.8),
(14, 'Aaron Snyder', 'Male', 'Sales', 'Team Lead', 34560.8),
(15, 'Dana Nguyen', 'Female', 'Sales', 'Team Lead', 35452.89),
(16, 'Cheryl Bradley', 'Female', 'Operations', 'Coordinator', 57372.05),
(17, 'Walter Pratt', 'Male', 'Sales', 'Team Lead', 52249.45),
(18, 'Angela Flores', 'Female', 'Finance', 'Analyst', 44516.48),
(19, 'Timothy Rodriguez', 'Male', 'HR', 'Manager', 60638.66),
(20, 'Michelle Kelley', 'Female', 'Marketing', 'Executive', 34491.22),
(21, 'Joshua Maynard', 'Male', 'HR', 'Manager', 72123.01),
(22, 'Laurie Wallace', 'Female', 'Operations', 'Executive', 56089.51)
GO


IF EXISTS(SELECT * FROM sysobjects WHERE name='prcEmpSearch')
    DROP PROC PrcEmpSearch
GO

CREATE PROC PrcEmpSearch
    @empno INT
AS
BEGIN
    DECLARE 
        @name VARCHAR(30),
        @dept VARCHAR(30),
        @gender VARCHAR(10),
        @desig VARCHAR(30),
        @basic NUMERIC(9,2)

    IF EXISTS(SELECT * FROM Employ WHERE empno = @empno)
    BEGIN
        SELECT 
            @name = Name, 
            @gender = Gender,
            @dept = Dept, 
            @desig = Desig,
            @basic = Basic
        FROM Employ 
        WHERE Empno = @empno

        PRINT 'Employ Name: ' + @name
        PRINT 'Gender: ' + @gender
        PRINT 'Department: ' + @dept
        PRINT 'Designation: ' + @desig
        PRINT 'Basic: ' + CONVERT(VARCHAR, @basic)
    END
    ELSE
    BEGIN
        PRINT 'Record Not Found...'
    END
END
GO

-- Execute the stored procedure
EXEC PrcEmpSearch 1
GO

-- View the stored procedure code
sp_helptext PrcEmpSearch
GO

