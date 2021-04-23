/* Drop procedures then recreate them. */
DROP PROCEDURE IF EXISTS ReturnAccounts;
DROP PROCEDURE IF EXISTS ReturnTransactions;
DROP PROCEDURE IF EXISTS ReturnNotificationRules;
DROP PROCEDURE IF EXISTS LoginNotification;
DROP PROCEDURE IF EXISTS AddNotificationRule;

/* Procedures that return data. */
CREATE PROCEDURE ReturnAccounts @CustomerId VARCHAR(450)
AS
SELECT id, account_type, balance, nickname, interest_rate FROM Account
	INNER JOIN Customer_Account ON Account.id = Customer_Account.account_id
	WHERE Customer_Account.customer_id = @CustomerId
	ORDER BY id ASC;

CREATE PROCEDURE ReturnTransactions @account_id INT
AS
SELECT id, account_id, timestamp, description, IIF(type = 'CR', 'Credit', 'Debit') as type, amount, balance_after
FROM Financial_Transaction WHERE account_id = @account_id ORDER BY id DESC;

CREATE PROCEDURE ReturnNotificationRules @UserName NVARCHAR(256)
AS
SELECT NR.id, NR.customer_id, NR.type, NR.condition, NR.value, NR.notify_web, NR.notify_email, NR.notify_text, NR.message FROM Notification_Rule AS NR
    INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
    WHERE USR.UserName = @UserName
    ORDER BY type;

/* Procedures that create data. */
CREATE PROCEDURE LoginNotification @UserName NVARCHAR(256)
AS
BEGIN
	DECLARE @rule_id INT;
	DECLARE @read_bit BIT;
    
    SELECT @rule_id = NR.id, @read_bit = NR.notify_web
        FROM Notification_Rule AS NR
        INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
        WHERE USR.UserName = @UserName AND type = "Login";

    IF @rule_id IS NOT NULL
    BEGIN
        INSERT INTO Notification (notification_rule, read_by_user, message)
        VALUES (
            @rule_id,
            ~@read_bit,
            "A new login on your customer account occured on " + CONVERT(VARCHAR, GETDATE(), 1) + " at " + CONVERT(VARCHAR, GETDATE(), 8) + "."
        );
    END
END

CREATE PROCEDURE AddNotificationRule
    @customer_id NVARCHAR(450),
    @type VARCHAR(32),
    @condition VARCHAR(32),
    @value DECIMAL(18, 0),
    @notify_text BIT,
    @notify_email BIT,
    @notify_web BIT,
    @message VARCHAR(300)
AS
BEGIN
    IF (@type = 'Login' OR @type = 'Balance' OR @type = 'Withdrawal' OR @type = 'Deposit') AND
        (@condition = 'Above' OR @condition = 'Over' OR @condition = 'NA') AND
        (@value >= 0.00) BEGIN
        IF (@message IS NULL) 
            INSERT INTO Notification_Rule (customer_id, type, condition, value, notify_text,
                notify_email, notify_web, message)
                VALUES (@customer_id, @type, @condition, @value, @notify_text, @notify_email,
                @notify_web, 'Your notification rule "' + @type + ' ' + @condition + ' ' +
                CONVERT(VARCHAR, @value) + '" has been triggered!');
        ELSE 
            INSERT INTO Notification_Rule (customer_id, type, condition, value, notify_text,
                notify_email, notify_web, message)
                VALUES (@customer_id, @type, @condition, @value, @notify_text, @notify_email,
                @notify_web, @message);
    END
END

CREATE PROCEDURE AddFinancialTransaction
    @account_id INT,
    @type VARCHAR(2),
    @amount MONEY,
    @description VARCHAR(100)
AS
BEGIN
    DECLARE @new_balance MONEY;

    IF (@type = 'CR')
        BEGIN
        UPDATE Account
            SET balance = balance + @amount, @new_balance = balance + @amount
            WHERE id = @account_id;
        END
    ELSE
        BEGIN
        UPDATE Account
            SET balance = balance - @amount, @new_balance = balance - @amount
            WHERE id = @account_id;
        END

    INSERT INTO Financial_Transaction
        (account_id, timestamp, type, amount, balance_after, description)
    VALUES (
        @account_id,
        GETDATE(),
        @type,
        @amount,
        @new_balance,
        @description
    );
END
