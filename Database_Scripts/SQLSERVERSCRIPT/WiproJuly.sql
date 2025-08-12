create database Wiprojuly
GO


IF DB_ID('Wiprojuly') IS NULL
    CREATE DATABASE Wiprojuly;
GO
USE Wiprojuly;
GO


IF OBJECT_ID('dbo.Employee', 'U') IS NOT NULL
    DROP TABLE dbo.Employee;
GO

CREATE TABLE dbo.Employee
(
    Empno  INT           NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(50)  NOT NULL,
    Gender NVARCHAR(10)  NOT NULL,
    Dept   NVARCHAR(20)  NOT NULL,
    Desig  NVARCHAR(20)  NOT NULL,
    Basic  DECIMAL(10,2) NOT NULL
);
GO


INSERT INTO dbo.Employee (Empno, [Name], Gender, Dept, Desig, Basic) VALUES
(1, 'Yamini', 'FEMALE', 'Dotnet', 'Expert', 88222.00),
(2, 'Anusha', 'FEMALE', 'Java',   'Manager', 82222.00),
(3, 'Uday',   'MALE',   'Python', 'Expert',  68833.00),
(4, 'Datta',  'MALE',   'Dotnet', 'Manager', 88322.00),
(5, 'Mahi',   'FEMALE', 'Python', 'Expert',  88223.00),
(6, 'Madhu',  'Male',   'Dotnet', 'Expert',  88323.00),
(7, 'Vasim',  'MALE',   'Dotnet', 'Manager', 88233.00);
GO


SELECT * FROM dbo.Employee ORDER BY Empno;
