USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      RecipientType Table
--
-- Copyright © 2022, mocella, All Rights Reserved
-------------------------------------------------------------------------------
-- Modification History (most recent first)
-------------------------------------------------------------------------------
--       
-------------------------------------------------------------------------------
IF (SELECT OBJECT_ID(N'dbo.RecipientType', N'U')) IS NULL
BEGIN
    CREATE TABLE dbo.RecipientType (
        RecipientTypeId tinyint             NOT NULL
            IDENTITY( 1, 1 )
            CONSTRAINT PK_RecipientType_Id
                PRIMARY KEY CLUSTERED,
        [Name]          nvarchar(3)         NOT NULL,
        Created         datetimeoffset(7)   NOT NULL CONSTRAINT DF_RecipientType_Created DEFAULT (sysdatetime()),
        Modified        datetimeoffset(7)   NOT NULL CONSTRAINT DF_RecipientType_Modified DEFAULT (sysdatetime())
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