USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      Recipient Table
--
-- Copyright © 2022, mocella, All Rights Reserved
-------------------------------------------------------------------------------
-- Modification History (most recent first)
-------------------------------------------------------------------------------
--       
-------------------------------------------------------------------------------
IF (SELECT OBJECT_ID(N'dbo.Recipient', N'U')) IS NULL
BEGIN
    CREATE TABLE dbo.[Recipient] (
        RecipientId     bigint              NOT NULL
            IDENTITY(1, 1 )
            CONSTRAINT PK_Recipient_Id
                PRIMARY KEY CLUSTERED,
        MessageId       bigint              NOT NULL,
        IdentityId      bigint              NOT NULL,
        RecipientTypeId tinyint             NOT NULL,
        Created         datetimeoffset(7)   NOT NULL CONSTRAINT DF_Recipient_Created DEFAULT (sysdatetime()),
        Modified        datetimeoffset(7)   NOT NULL CONSTRAINT DF_Recipient_Modified DEFAULT (sysdatetime())
    )
END

-------------------------------------------------------------------------------
-- Here be alters
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------
-- Here be indexes
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------
-- Here be triggers
-------------------------------------------------------------------------------