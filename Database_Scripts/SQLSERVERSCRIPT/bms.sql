-- Make sure the DB exists and broker toggle may roll back in-flight txns (expected messages)
USE [master];
GO
IF DB_ID(N'bank') IS NULL
    CREATE DATABASE [bank];
GO
ALTER DATABASE [bank] SET COMPATIBILITY_LEVEL = 160;
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
    EXEC(N'USE [bank]; EXEC sp_fulltext_database @action = ''enable'';');
END
ALTER DATABASE [bank] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;
ALTER DATABASE [bank] SET QUERY_STORE = ON;
GO

USE [bank];
GO

-- Clean drops: avoid the IF ... IS NOT NULL pattern that caused your syntax error
DROP TABLE IF EXISTS dbo.[Trans];
DROP TABLE IF EXISTS dbo.[Account];
DROP TABLE IF EXISTS dbo.[Users];
GO

-- Recreate tables (same columns as your script)
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE dbo.[Account](
  [AccountNo]   int NOT NULL PRIMARY KEY,
  [FirstName]   varchar(30) NULL,
  [LastName]    varchar(30) NULL,
  [City]        varchar(30) NULL,
  [State]       varchar(30) NULL,
  [Amount]      numeric(9,2) NULL,
  [AccountType] varchar(10) NULL,
  [CheqFacil]   varchar(10) NULL
);
GO

CREATE TABLE dbo.[Trans](
  [TranId]     int IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [AccountNo]  int NULL,
  [TranAmount] numeric(9,2) NULL,
  [TranType]   varchar(5) NULL,
  [TranDate]   date NULL
);
GO

CREATE TABLE dbo.[Users](
  [Id]       int IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Username] varchar(30) NULL,
  [Passcode] varchar(30) NULL
);
GO

-- Constraints (optional but recommended)
ALTER TABLE dbo.[Trans]  WITH CHECK
  ADD CONSTRAINT FK_Trans_Account FOREIGN KEY (AccountNo) REFERENCES dbo.[Account](AccountNo);
ALTER TABLE dbo.[Trans]
  ADD CONSTRAINT CK_Trans_TranType CHECK (TranType IN ('C','D'));
GO

-- Data
INSERT dbo.[Account] (AccountNo, FirstName, LastName, City, State, Amount, AccountType, CheqFacil) VALUES
(1,N'Venkata',N'Gayathri',N'Hyderabad',N'TS',CAST(108222.00 AS numeric(9,2)),N'Savings',N'Yes'),
(2,N'Raj',N'Kishore',N'Hyd',N'TS',CAST(99222.00 AS numeric(9,2)),N'Savings',N'Yes'),
(3,N'Ranjan',N'Kishore',N'Hyd',N'TS',CAST(99222.00 AS numeric(9,2)),N'Savings',N'Yes'),
(4,N'Ram',N'Kishan',N'Hyderabad',N'TS',CAST(90222.00 AS numeric(9,2)),N'Savings',N'Yes'),
(5,N'Gayathri',N'Venkata',N'Chennai',N'TN',CAST(88222.00 AS numeric(9,2)),N'Savings',N'Yes'),
(6,N'Abhishek',N'Prabhakar',N'Delhi',N'UP',CAST(99234.00 AS numeric(9,2)),N'Savings',N'Yes'),
(7,N'Ajay',N'Kumar',N'Chennai',N'TN',CAST(90233.00 AS numeric(9,2)),N'Savings',N'Yes'),
(8,N'Shri',N'Hari',N'Chennai',N'TN',CAST(90233.00 AS numeric(9,2)),N'Savings',N'Yes');
GO

SET IDENTITY_INSERT dbo.[Trans] ON;
INSERT dbo.[Trans] (TranId, AccountNo, TranAmount, TranType, TranDate) VALUES
(1,1,CAST(2000.00  AS numeric(9,2)),N'C',CAST(N'2025-03-05' AS date)),
(2,1,CAST(2000.00  AS numeric(9,2)),N'C',CAST(N'2025-03-05' AS date)),
(3,1,CAST(2500.00  AS numeric(9,2)),N'C',CAST(N'2025-03-05' AS date)),
(4,1,CAST(2500.00  AS numeric(9,2)),N'D',CAST(N'2025-03-05' AS date)),
(5,1,CAST(5000.00  AS numeric(9,2)),N'C',CAST(N'2025-03-05' AS date)),
(6,1,CAST(10000.00 AS numeric(9,2)),N'D',CAST(N'2025-03-05' AS date)),
(7,1,CAST(20000.00 AS numeric(9,2)),N'C',CAST(N'2025-03-05' AS date)),
(8,1,CAST(800.00   AS numeric(9,2)),N'C',CAST(N'2025-08-02' AS date)),
(9,1,CAST(800.00   AS numeric(9,2)),N'D',CAST(N'2025-08-02' AS date)),
(10,7,CAST(10000.00 AS numeric(9,2)),N'C',CAST(N'2025-08-02' AS date)),
(11,7,CAST(8000.00  AS numeric(9,2)),N'D',CAST(N'2025-08-02' AS date)),
(12,8,CAST(9000.00  AS numeric(9,2)),N'C',CAST(N'2025-08-02' AS date)),
(13,8,CAST(7000.00  AS numeric(9,2)),N'D',CAST(N'2025-08-02' AS date)),
(14,8,CAST(8000.00  AS numeric(9,2)),N'C',CAST(N'2025-08-02' AS date)),
(15,8,CAST(8000.00  AS numeric(9,2)),N'D',CAST(N'2025-08-02' AS date));
SET IDENTITY_INSERT dbo.[Trans] OFF;
GO

SET IDENTITY_INSERT dbo.[Users] ON;
INSERT dbo.[Users] ([Id],[Username],[Passcode]) VALUES
(1,N'Gayathri',N'Krishna'),
(2,N'Isha',N'Ghosh'),
(3,N'Laksha',N'Katara');
SET IDENTITY_INSERT dbo.[Users] OFF;
GO

-- Default on TranDate (matches 'date' type)
ALTER TABLE dbo.[Trans] ADD DEFAULT (CAST(GETDATE() AS date)) FOR [TranDate];
GO

-- Unique username (same as your script)
SET ANSI_PADDING ON;
ALTER TABLE dbo.[Users] ADD CONSTRAINT UQ_Users_Username UNIQUE NONCLUSTERED ([Username]);
GO

-- Helpful index for typical queries
CREATE INDEX IX_Trans_AccountNo_TranDate ON dbo.[Trans](AccountNo, TranDate);
GO
