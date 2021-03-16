/* This command keeps this file from ever being ran by accident. */
exit;
GO

/* Start the SQL Server through terminal. */
/opt/mssql-tools/bin/sqlcmd -U SA

/* Run a .sql file into SQL Server. */
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -i /home/fontaine/Projects/commerce/data/DatabaseDefinition.sql

/* Back up the entire database to file. File must exist, and SQL Server must have write permissions. */
BACKUP DATABASE CommerceBank TO DISK = '/path/to/file/backup.bak';

/* Only back up the differences since the last full database backup. */
BACKUP DATABASE CommerceBank TO DISK = '/path/to/file/backup.bak' WITH DIFFERENTIAL;
