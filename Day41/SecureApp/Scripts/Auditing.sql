-- Example SQL Server Audit (adjust paths/targets)
USE master;
GO
IF NOT EXISTS (SELECT * FROM sys.server_audits WHERE name = 'SecureAudit')
BEGIN
    CREATE SERVER AUDIT SecureAudit TO FILE (FILEPATH = 'C:\\SQLAudit\\');
END
GO
ALTER SERVER AUDIT SecureAudit WITH (STATE = ON);
GO
USE SecureDb;
GO
IF NOT EXISTS (SELECT * FROM sys.database_audit_specifications WHERE name = 'SecureDbAuditSpec')
BEGIN
    CREATE DATABASE AUDIT SPECIFICATION SecureDbAuditSpec
    FOR SERVER AUDIT SecureAudit
        ADD (SELECT ON SCHEMA::dbo BY app_readwrite),
        ADD (UPDATE ON SCHEMA::dbo BY app_readwrite),
        ADD (FAILED_DATABASE_AUTHENTICATION_GROUP)
    WITH (STATE = ON);
END
GO


