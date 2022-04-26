USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      Recipient Table Foreign Keys
--
-- Copyright © 2022, mocella, All Rights Reserved
-------------------------------------------------------------------------------
-- Modification History (most recent first)
-------------------------------------------------------------------------------
--       
-------------------------------------------------------------------------------
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;

IF NOT EXISTS (   SELECT *
                  FROM   sys.foreign_keys
                  WHERE  object_id = OBJECT_ID(N'dbo.FK_Recipient_Message')
                         AND parent_object_id = OBJECT_ID(N'dbo.Recipient'))
    ALTER TABLE dbo.Recipient WITH CHECK
    ADD CONSTRAINT FK_Recipient_Message
        FOREIGN KEY ( MessageId )
        REFERENCES dbo.[Message] ( MessageId );

IF NOT EXISTS (   SELECT *
                  FROM   sys.foreign_keys
                  WHERE  object_id = OBJECT_ID(N'dbo.FK_Recipient_Identity')
                         AND parent_object_id = OBJECT_ID(N'dbo.Recipient'))
    ALTER TABLE dbo.Recipient WITH CHECK
    ADD CONSTRAINT FK_Recipient_Identity
        FOREIGN KEY ( IdentityId )
        REFERENCES dbo.[Identity] ( IdentityId );

IF NOT EXISTS (   SELECT *
                  FROM   sys.foreign_keys
                  WHERE  object_id = OBJECT_ID(N'dbo.FK_Recipient_RecipientType')
                         AND parent_object_id = OBJECT_ID(N'dbo.Recipient'))
    ALTER TABLE dbo.Recipient WITH CHECK
    ADD CONSTRAINT FK_Recipient_RecipientType
        FOREIGN KEY ( RecipientTypeId )
        REFERENCES dbo.RecipientType ( RecipientTypeId );