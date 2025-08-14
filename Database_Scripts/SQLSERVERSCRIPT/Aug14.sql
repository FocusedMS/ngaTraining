SET NOCOUNT ON;

------------------------------------------------------------
-- 0) Create DB only if missing
------------------------------------------------------------
IF DB_ID(N'lms') IS NULL
BEGIN
    PRINT 'Creating database [lms]...';
    CREATE DATABASE [lms];
END
ELSE
BEGIN
    PRINT 'Database [lms] already exists. Skipping CREATE.';
END
GO

USE [lms];
GO

------------------------------------------------------------
-- 1) dbo.Employee
------------------------------------------------------------
IF OBJECT_ID(N'dbo.Employee', N'U') IS NULL
BEGIN
    PRINT 'Creating table dbo.Employee...';
    CREATE TABLE dbo.Employee
    (
        EmpId        INT           NOT NULL,
        EmployName   VARCHAR(30)   NULL,
        MgrId        INT           NULL,
        LeaveAvail   INT           NULL,
        DateOfBirth  DATETIME      NULL,
        Email        VARCHAR(30)   NULL,
        Mobile       VARCHAR(30)   NULL,
        CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED (EmpId)
    );
END
ELSE
BEGIN
    PRINT 'Table dbo.Employee already exists. Skipping CREATE.';
END
GO

-- FK: Employee.MgrId → Employee.EmpId (self reference)
IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_Employee_Manager'
      AND parent_object_id = OBJECT_ID(N'dbo.Employee')
)
BEGIN
    PRINT 'Adding FK_Employee_Manager...';
    ALTER TABLE dbo.Employee
      WITH CHECK
      ADD CONSTRAINT FK_Employee_Manager
      FOREIGN KEY (MgrId) REFERENCES dbo.Employee(EmpId);
END
ELSE
BEGIN
    PRINT 'FK_Employee_Manager already exists. Skipping.';
END
GO

------------------------------------------------------------
-- 2) dbo.LeaveHistory
------------------------------------------------------------
IF OBJECT_ID(N'dbo.LeaveHistory', N'U') IS NULL
BEGIN
    PRINT 'Creating table dbo.LeaveHistory...';
    CREATE TABLE dbo.LeaveHistory
    (
        LeaveId         INT IDENTITY(1,1) NOT NULL,
        EmpId           INT               NULL,
        LeaveStartDate  DATETIME          NULL,
        LeaveEndDate    DATETIME          NULL,
        noOfDays        INT               NULL,
        LeaveStatus     VARCHAR(30)       NULL,
        LeaveReason     VARCHAR(30)       NULL,
        ManagerComments VARCHAR(30)       NULL,
        CONSTRAINT PK_LeaveHistory PRIMARY KEY CLUSTERED (LeaveId)
    );
END
ELSE
BEGIN
    PRINT 'Table dbo.LeaveHistory already exists. Skipping CREATE.';
END
GO

-- Default constraint for LeaveStatus
IF NOT EXISTS (
    SELECT 1
    FROM sys.default_constraints dc
    WHERE dc.parent_object_id = OBJECT_ID(N'dbo.LeaveHistory')
      AND dc.parent_column_id = COLUMNPROPERTY(OBJECT_ID(N'dbo.LeaveHistory'), 'LeaveStatus', 'ColumnId')
)
BEGIN
    PRINT 'Adding DF_LeaveHistory_LeaveStatus default...';
    ALTER TABLE dbo.LeaveHistory
      ADD CONSTRAINT DF_LeaveHistory_LeaveStatus
      DEFAULT ('PENDING') FOR LeaveStatus;
END
ELSE
BEGIN
    PRINT 'Default on LeaveStatus already exists. Skipping.';
END
GO

-- FK: LeaveHistory.EmpId → Employee.EmpId
IF NOT EXISTS (
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_LeaveHistory_Employee'
      AND parent_object_id = OBJECT_ID(N'dbo.LeaveHistory')
)
BEGIN
    PRINT 'Adding FK_LeaveHistory_Employee...';
    ALTER TABLE dbo.LeaveHistory
      WITH CHECK
      ADD CONSTRAINT FK_LeaveHistory_Employee
      FOREIGN KEY (EmpId) REFERENCES dbo.Employee(EmpId);
END
ELSE
BEGIN
    PRINT 'FK_LeaveHistory_Employee already exists. Skipping.';
END
GO

------------------------------------------------------------
-- 3) Seed data (only if missing)
------------------------------------------------------------

-- Employees
IF NOT EXISTS (SELECT 1 FROM dbo.Employee WHERE EmpId = 1000)
    INSERT dbo.Employee (EmpId, EmployName, MgrId, LeaveAvail, DateOfBirth, Email, Mobile)
    VALUES (1000, N'Muskan', NULL, 20, '2002-12-12', N'muskan@gmail.com', N'992445552');

IF NOT EXISTS (SELECT 1 FROM dbo.Employee WHERE EmpId = 2000)
    INSERT dbo.Employee (EmpId, EmployName, MgrId, LeaveAvail, DateOfBirth, Email, Mobile)
    VALUES (2000, N'Aadithian', 1000, 22, '2002-05-12', N'aadi@gmail.com', N'99293444');

IF NOT EXISTS (SELECT 1 FROM dbo.Employee WHERE EmpId = 3000)
    INSERT dbo.Employee (EmpId, EmployName, MgrId, LeaveAvail, DateOfBirth, Email, Mobile)
    VALUES (3000, N'Avinash', 1000, 28, '2001-11-11', N'Avin@gmail.com', N'9922445');

IF NOT EXISTS (SELECT 1 FROM dbo.Employee WHERE EmpId = 4000)
    INSERT dbo.Employee (EmpId, EmployName, MgrId, LeaveAvail, DateOfBirth, Email, Mobile)
    VALUES (4000, N'Prashanth', 2000, 18, '2002-11-12', N'prash@gmail.com', N'99234544');

IF NOT EXISTS (SELECT 1 FROM dbo.Employee WHERE EmpId = 5000)
    INSERT dbo.Employee (EmpId, EmployName, MgrId, LeaveAvail, DateOfBirth, Email, Mobile)
    VALUES (5000, N'Anjali', 3000, 18, '2002-12-12', N'anjali@gmail.com', N'9994222');

-- LeaveHistory (keep your specific LeaveId values if they are not taken)
IF NOT EXISTS (SELECT 1 FROM dbo.LeaveHistory WHERE LeaveId = 1)
BEGIN
    SET IDENTITY_INSERT dbo.LeaveHistory ON;
    INSERT dbo.LeaveHistory (LeaveId, EmpId, LeaveStartDate, LeaveEndDate, noOfDays, LeaveStatus, LeaveReason, ManagerComments)
    VALUES (1, 2000, '2025-10-10', '2025-10-11', NULL, N'PENDING', N'Going Home', NULL);
    SET IDENTITY_INSERT dbo.LeaveHistory OFF;
END

IF NOT EXISTS (SELECT 1 FROM dbo.LeaveHistory WHERE LeaveId = 2)
BEGIN
    SET IDENTITY_INSERT dbo.LeaveHistory ON;
    INSERT dbo.LeaveHistory (LeaveId, EmpId, LeaveStartDate, LeaveEndDate, noOfDays, LeaveStatus, LeaveReason, ManagerComments)
    VALUES (2, 2000, '2025-11-11', '2025-11-14', NULL, N'PENDING', N'Marriage', NULL);
    SET IDENTITY_INSERT dbo.LeaveHistory OFF;
END

IF NOT EXISTS (SELECT 1 FROM dbo.LeaveHistory WHERE LeaveId = 3)
BEGIN
    SET IDENTITY_INSERT dbo.LeaveHistory ON;
    INSERT dbo.LeaveHistory (LeaveId, EmpId, LeaveStartDate, LeaveEndDate, noOfDays, LeaveStatus, LeaveReason, ManagerComments)
    VALUES (3, 3000, '2025-12-12', '2025-12-14', NULL, N'PENDING', N'Convocation', NULL);
    SET IDENTITY_INSERT dbo.LeaveHistory OFF;
END
GO

PRINT 'Done.';
