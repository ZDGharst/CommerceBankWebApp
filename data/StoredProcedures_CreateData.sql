USE CommerceBank_TransactionDB;
GO


/* Drop procedures then recreate them. */
DROP PROCEDURE IF EXISTS LoginNotification;
DROP PROCEDURE IF EXISTS AddNotificationRule;
DROP PROCEDURE IF EXISTS AddFinancialTransaction;
GO

/* Procedures that create data. */
CREATE PROCEDURE LoginNotification
    @UserName NVARCHAR(256)
AS
BEGIN
	DECLARE @rule_id INT;
	DECLARE @read_bit BIT;
	DECLARE @message VARCHAR(300);
    
    SELECT @rule_id = NR.id, @read_bit = NR.notify_web, @message = NR.message
        FROM Notification_Rule AS NR
        INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
        WHERE USR.UserName = @UserName AND type = "Login";

    IF @rule_id IS NOT NULL
    BEGIN
        INSERT INTO Notification (notification_rule, read_by_user, message)
        VALUES (
            @rule_id,
            ~@read_bit,
            IIF(
            	@message = "NA" OR @message IS NULL,
            	"A new login on your customer account occured on " + FORMAT(GETDATE(), 'MM/dd/yy') + " at " + FORMAT(GETDATE(), 'hh:mm:ss tt') + ".",
            	@message
            )
        );
    END
END
GO


CREATE PROCEDURE AddNotificationRule
    @customer_id NVARCHAR(450),
    @type VARCHAR(32),
    @condition VARCHAR(32),
    @value DECIMAL(18, 2),
    @notify_text BIT,
    @notify_email BIT,
    @notify_web BIT,
    @message VARCHAR(300)
AS
BEGIN
    IF (@type = 'Login' OR @type = 'Balance' OR @type = 'Withdrawal' OR @type = 'Deposit') AND
        (@condition = 'Above' OR @condition = 'Below' OR @condition = 'NA') 
        BEGIN
            INSERT INTO Notification_Rule (customer_id, type, condition, value, notify_text, notify_email, notify_web, message)
                VALUES (@customer_id, @type, @condition, @value, @notify_text, @notify_email, @notify_web, @message);
        END
END
GO


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
GO
