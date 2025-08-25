-- Transparent Data Encryption (TDE) - adjust database name
USE master;
GO
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = '##MS_DatabaseMasterKey##')
BEGIN
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'ReplaceWithStrongPasswordAndBackup';
END
GO
IF NOT EXISTS (SELECT * FROM sys.certificates WHERE name = 'TDECert')
BEGIN
    CREATE CERTIFICATE TDECert WITH SUBJECT = 'TDE Cert';
END
GO
USE SecureDb;
GO
IF NOT EXISTS (SELECT * FROM sys.dm_database_encryption_keys)
BEGIN
    CREATE DATABASE ENCRYPTION KEY WITH ALGORITHM = AES_256 ENCRYPTION BY SERVER CERTIFICATE TDECert;
END
GO
ALTER DATABASE SecureDb SET ENCRYPTION ON;
GO


