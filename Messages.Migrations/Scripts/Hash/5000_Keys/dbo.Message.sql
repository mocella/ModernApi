USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      Message Table Foreign Keys
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
                  WHERE  object_id = OBJECT_ID(N'dbo.FK_Message_Identity')
                         AND parent_object_id = OBJECT_ID(N'dbo.[Message]'))
    ALTER TABLE dbo.[Message] WITH CHECK
    ADD CONSTRAINT FK_Message_Identity
        FOREIGN KEY ( SenderId )
        REFERENCES dbo.[Identity] ( IdentityId );