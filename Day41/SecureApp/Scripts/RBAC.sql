USE SecureDb;
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'app_readwrite')
    CREATE ROLE app_readwrite;
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO app_readwrite;
GO
-- Example app user mapping
-- CREATE USER secureapp FROM LOGIN secureapp_login;
-- EXEC sp_addrolemember 'app_readwrite', 'secureapp';


