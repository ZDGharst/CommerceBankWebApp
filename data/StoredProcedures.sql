/* Drop procedures then recreate them. */

DROP PROCEDURE IF EXISTS ReturnTransactions;
DROP PROCEDURE IF EXISTS ReturnNotificationRules;
DROP PROCEDURE IF EXISTS LoginNotification;

/* Procedures that return data. */
CREATE PROCEDURE ReturnTransactions @account_id INT
AS
SELECT id, account_id, timestamp, description, IIF(type = 'CR', 'Credit', 'Debit') as type, amount, balance_after FROM Financial_Transaction WHERE account_id = @account_id ORDER BY id DESC;

CREATE PROCEDURE ReturnNotificationRules @UserName NVARCHAR(256)
AS
SELECT NR.id, NR.customer_id, NR.type, NR.condition, NR.value, NR.notify_web, NR.notify_email, NR.notify_text FROM Notification_Rule AS NR
    INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
    WHERE USR.UserName = @UserName
    ORDER BY type;

/* Procedures that create data. */
CREATE PROCEDURE LoginNotification @customer_id VARCHAR(450)
AS
BEGIN
	DECLARE @rule_id INT;
    
    SELECT @rule_id = id
    FROM Notification_Rule
    WHERE customer_id = @customer_id AND type = "login";

    IF @rule_id IS NOT NULL
    BEGIN
        INSERT INTO Notification (notification_rule, message)
        VALUES (
            @rule_id,
            "A new login on your customer account occured on " + CONVERT(VARCHAR, GETDATE(), 1) + " at " + CONVERT(VARCHAR, GETDATE(), 8) + "."
        );
    END
END
