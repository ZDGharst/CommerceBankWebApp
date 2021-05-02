USE CommerceBank_TransactionDB;
GO


/* Drop procedures then recreate them. */
DROP PROCEDURE IF EXISTS ReturnAccounts;
DROP PROCEDURE IF EXISTS ReturnTransactions;
DROP PROCEDURE IF EXISTS ReturnNotificationRules;
DROP PROCEDURE IF EXISTS ReturnUnreadNotifications;
DROP PROCEDURE IF EXISTS ReturnTriggeredNotifications;
GO


/* Procedures that return data. */
CREATE PROCEDURE ReturnAccounts
    @CustomerId VARCHAR(450)
AS
BEGIN
    SELECT id, account_type, balance, nickname, interest_rate
        FROM Account
        INNER JOIN Customer_Account ON Account.id = Customer_Account.account_id
        WHERE Customer_Account.customer_id = @CustomerId
        ORDER BY id ASC;
END
GO


CREATE PROCEDURE ReturnTransactions
    @account_id INT
AS
BEGIN
    SELECT id, account_id, timestamp, description, IIF(type = 'CR', 'Credit', 'Debit') as type, amount, balance_after
        FROM Financial_Transaction
        WHERE account_id = @account_id
        ORDER BY id DESC;
END
GO


CREATE PROCEDURE ReturnNotificationRules
    @UserName NVARCHAR(256)
AS
BEGIN
    SELECT NR.id, NR.customer_id, NR.type, NR.condition, NR.value, NR.notify_web, NR.notify_email, NR.notify_text,
            NR.message
        FROM Notification_Rule AS NR
        INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
        WHERE USR.UserName = @UserName
        ORDER BY type, condition;
END
GO


CREATE PROCEDURE ReturnUnreadNotifications
    @UserName NVARCHAR(256)
AS
BEGIN
    SELECT N.id, N.transaction_id, N.notification_rule, N.message FROM Notification as N
        INNER JOIN Notification_Rule AS NR ON N.notification_rule = NR.id
        INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
        WHERE USR.UserName = @UserName AND read_by_user = 0
        ORDER BY N.id DESC;
END
GO


CREATE PROCEDURE ReturnTriggeredNotifications
    @UserName NVARCHAR(256),
    @TimeFrame DATETIME
AS
BEGIN
    SELECT NR.id, NR.type, NR.condition, NR.value, COUNT(notification_rule) as Count
        FROM Notification_Rule AS NR
        LEFT JOIN (SELECT notification_rule FROM Notification WHERE Notification.timestamp > @TimeFrame) b ON NR.id = notification_rule
        INNER JOIN AspNetUsers AS USR ON NR.customer_id = USR.Id 
        WHERE USR.UserName = @UserName
        GROUP BY NR.id, NR.type, NR.condition, NR.value, notification_rule
        ORDER BY Count DESC
END
GO
