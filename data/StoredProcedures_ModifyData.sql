USE CommerceBank_TransactionDB;
GO


/* Drop procedures then recreate them. */
DROP PROCEDURE IF EXISTS EditNotificationRule;
DROP PROCEDURE IF EXISTS DelNotificationRule;
DROP PROCEDURE IF EXISTS MarkNotificationsRead;
GO


/* Procedures that modify/delete data. */
CREATE PROCEDURE EditNotificationRule
    @id INT,
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
    UPDATE Notification_Rule
        SET
            type = @type,
            condition = @condition,
            value = @value,
            notify_text = @notify_text,
            notify_email = @notify_email,
            notify_web = @notify_web
        WHERE
            id = @id AND customer_id = @customer_id;
END
GO


CREATE PROCEDURE DelNotificationRule
    @id INT
AS
BEGIN
    DELETE FROM Notification_Rule 
        WHERE id = @id;
END
GO


CREATE PROCEDURE MarkNotificationsRead
    @customer_id NVARCHAR(450)
AS
BEGIN
    UPDATE Notification
        SET read_by_user = 1
        FROM Notification AS N
            INNER JOIN Notification_Rule AS NR
                ON N.notification_rule = NR.id
        WHERE NR.customer_id = @customer_id;
END
GO