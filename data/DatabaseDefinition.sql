/* This file contains the SQL to create the database from scratch. Anyone who uses this file should
 * end up on the same schema as the database in production, but without the content. Before running
 * this, get the password from the environment variables for the user created below. 
 * 
 * TODO: -Add the hashing algorithm to the password column once the log in form is ready. 
 *       -Add additional users other than the SA in order to encapsulate data. */
USE CommerceBank_TransactionDB;

/* Create the web viewer user and grant it permissions for stored procedures. Insert password in quotes.
 * TODO: Replace grants for select/update/insert with stored procedures once they are created. */
-- CREATE LOGIN CommerceWebAppCustomer WITH PASSWORD = "";
-- CREATE USER CommerceWebAppCustomer FOR LOGIN CommerceWebAppCustomer;
-- GRANT SELECT, UPDATE, INSERT TO CommerceWebAppCustomer;

/* Drop tables in order of foreign key dependencies. */
DROP TABLE IF EXISTS Notification;
DROP TABLE IF EXISTS Notification_Rule;
DROP TABLE IF EXISTS Financial_Transaction;
DROP TABLE IF EXISTS Customer_Account;
DROP TABLE IF EXISTS Account;
DROP TABLE IF EXISTS Customer_Info;

/* Create all initial tables. */
CREATE TABLE [dbo].[Customer_Info] (
    [id]            NVARCHAR (450) NOT NULL,
    [first_name]    VARCHAR (32)   NOT NULL,
    [last_name]     VARCHAR (32)   NOT NULL,
    [date_of_birth] DATE           NOT NULL,
    [address]       VARCHAR (100)  NOT NULL,
    [city]          VARCHAR (50)   NOT NULL,
    [state]         VARCHAR (2)    NOT NULL,
    [zip]           INT            NOT NULL,

    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

CREATE TABLE [dbo].[Account] (
    [id]            INT            IDENTITY(211111110, 1),
    [account_type]  VARCHAR (8)    NOT NULL,
    [balance]       MONEY          NOT NULL,
    [nickname]      VARCHAR (32)   NULL,
    [interest_rate] DECIMAL (6, 6) NULL,

    PRIMARY KEY CLUSTERED ([id] ASC)
);

/* Can't cluster Customer_Account because its primary key would exceed 900 bytes */
CREATE TABLE [dbo].[Customer_Account] (
    [customer_id]   NVARCHAR (450) NOT NULL,
    [account_id]    INT            NOT NULL,

    FOREIGN KEY ([customer_id]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    FOREIGN KEY ([account_id]) REFERENCES [dbo].[Account] ([Id])
);

CREATE TABLE [dbo].[Financial_Transaction] (
    [id]            INT           IDENTITY(1, 1),
    [account_id]    INT           NOT NULL,
    [timestamp]     DATETIME      NOT NULL,
    [type]          VARCHAR (2)   NOT NULL,
    [amount]        MONEY         NOT NULL,
    [balance_after] MONEY         NOT NULL,
    [description]   VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([account_id]) REFERENCES [dbo].[Account] ([id])
);

CREATE TABLE [dbo].[Notification_Rule] (
    [id]          INT            IDENTITY(1, 1),
    [customer_id] NVARCHAR (450) NOT NULL,
    [type]        VARCHAR (32)   NOT NULL,
    [condition]   VARCHAR (32)   NULL,
    [value]       DECIMAL (18)   NULL,

    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([customer_id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

CREATE TABLE [dbo].[Notification] (
    [id]                INT           IDENTITY(1, 1),
    [transaction_id]    INT           NULL,
    [notification_rule] INT           NOT NULL,
    [message]           VARCHAR (150) NULL,

    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([transaction_id]) REFERENCES [dbo].[Financial_Transaction] ([id]),
    FOREIGN KEY ([notification_rule]) REFERENCES [dbo].[Notification_Rule] ([id])
);

/* Insert initial customer and account. */ 
INSERT INTO Customer_Info VALUES (
    "b5e057b8-06ae-458d-a7f6-6228d707e5c3",
    "John",
    "Smith",
    DATEADD(year, -21, CAST(GETDATE() AS date)),
    "5000 Holmes St",
    "Kansas City",
    "MO",
    64110
);

INSERT INTO Account (account_type, balance, nickname, interest_rate)
    VALUES (
        'Checking', $5000, 'General Checking', 0.001
    );

INSERT INTO Customer_Account VALUES (
    "b5e057b8-06ae-458d-a7f6-6228d707e5c3",
    211111110
);

/* Import initial transactions for the initial customer. */
BULK INSERT Financial_Transaction
    FROM '/home/fontaine/Projects/commerce/data/RawData.csv'
    WITH (
         FIRSTROW = 2,
         DATAFILETYPE='char',
         FIELDTERMINATOR =',',
         ROWTERMINATOR = '\n',
         TABLOCK
    );
