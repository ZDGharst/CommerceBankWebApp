/* This file contains the SQL to create the database from scratch. */

DROP DATABASE IF EXISTS CommerceBank;
CREATE DATABASE CommerceBank;

USE CommerceBank;

CREATE TABLE Customer (
    id                  INT PRIMARY KEY IDENTITY(1, 1),
    username            VARCHAR(32) NOT NULL,
    password            VARCHAR(32) NOT NULL,
    email               VARCHAR(100) NOT NULL,
    first_name          VARCHAR(50) NOT NULL,
    last_name           VARCHAR(50) NOT NULL,
    date_of_birth       DATE NOT NULL,
    address             VARCHAR(100) NOT NULL,
    city                VARCHAR(50) NOT NULL,
    state               VARCHAR(2) NOT NULL,
    zip                 INT NOT NULL,
    last_login          DATETIME NOT NULL
);

CREATE TABLE Account (
    id                  INT PRIMARY KEY IDENTITY(211111110, 1),
    customer_id         INT NOT NULL,
    account_type        VARCHAR(8) NOT NULL,
    balance             MONEY NOT NULL,
    nickname            VARCHAR(32),
    interest_rate       DECIMAL(6,6) NOT NULL
);

CREATE TABLE Financial_Transaction (
    id                  INT PRIMARY KEY IDENTITY(1, 1),
    account_id          INT NOT NULL,
    timestamp           DATETIME NOT NULL,
    type                VARCHAR(2) NOT NULL,
    amount              MONEY NOT NULL,
    balance_after       MONEY NOT NULL,
    description         VARCHAR(100)
);

CREATE TABLE Notification (
    id                  INT PRIMARY KEY IDENTITY(1, 1),
    transaction_id      INT,
    notification_rule   INT NOT NULL,
    message             VARCHAR(150)
);

CREATE TABLE Notification_Rule (
    id                  INT PRIMARY KEY IDENTITY(1, 1),
    customer_id         INT NOT NULL,
    type                VARCHAR(32) NOT NULL,
    condition           VARCHAR(32),
    value				DECIMAL
);

ALTER TABLE Account
	ADD FOREIGN KEY (customer_id) REFERENCES Customer(id);
	
ALTER TABLE Financial_Transaction
	ADD FOREIGN KEY (account_id) REFERENCES Account(id);
	
ALTER TABLE Notification
	ADD FOREIGN KEY (transaction_id) REFERENCES Financial_Transaction(id),
	    FOREIGN KEY (notification_rule) REFERENCES Notification_Rule(id);
	
ALTER TABLE Notification_Rule
	ADD FOREIGN KEY (customer_id) REFERENCES Customer(id);
	
INSERT INTO Customer
	(username, password, email, first_name, last_name, date_of_birth, address, city, state, zip, last_login)
	VALUES (
		'JPrice', '123456', 'jprice@example.com', 'Joe', 'Price', GETDATE(), '1234 Main St.', 'Kansas City', 'MO', 64117, GETDATE()
	);
	
INSERT INTO Account
	(customer_id, account_type, balance, nickname, interest_rate)
	VALUES (
		1, 'Checking', $5000, 'General Checking', 0.001
	);
