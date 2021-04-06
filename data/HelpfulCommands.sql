/* This command keeps this file from ever being ran by accident. */
exit;
GO

/* Start the SQL Server through terminal. */
/opt/mssql-tools/bin/sqlcmd -U SA

/* Run a .sql file into SQL Server. */
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -i /home/fontaine/Projects/commerce/data/DatabaseDefinition.sql

/* Back up the entire database to file. File must exist, and SQL Server must have write permissions. */
BACKUP DATABASE CommerceBank_TransactionDB TO DISK = '/home/fontaine/backup.bak';

/* Only back up the differences since the last full database backup. */
BACKUP DATABASE CommerceBank_TransactionDB TO DISK = '/path/to/file/backup.bak' WITH DIFFERENTIAL;

/* Get the columns and their data types of a table. */
SELECT C.NAME AS COLUMN_NAME, TYPE_NAME(C.USER_TYPE_ID) AS DATA_TYPE, C.IS_NULLABLE, C.MAX_LENGTH, C.PRECISION, C.SCALE FROM SYS.COLUMNS C JOIN SYS.TYPES T ON C.USER_TYPE_ID=T.USER_TYPE_ID WHERE C.OBJECT_ID=OBJECT_ID('table_name');

/* Scaffold CRUD tables. Edit only the parameters in brackets and remove the brackets after. */
dotnet aspnet-codegenerator controller -name [Customer_InfoController] -m [Customer_Info] -dc [DbaContext] --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

/* See all the tables in a database. */
SELECT name FROM SYSOBJECTS WHERE xtype = 'U';
