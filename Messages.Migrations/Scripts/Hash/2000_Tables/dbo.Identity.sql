USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      Identity Table
--
-- Copyright © 2022, mocella, All Rights Reserved
-------------------------------------------------------------------------------
-- Modification History (most recent first)
-------------------------------------------------------------------------------
--       
-------------------------------------------------------------------------------
IF (SELECT OBJECT_ID(N'dbo.Identity', N'U')) IS NULL
BEGIN
    CREATE TABLE dbo.[Identity] (
        IdentityId      bigint              NOT NULL
            IDENTITY( 1, 1 )
            CONSTRAINT PK_Identity_Id
                PRIMARY KEY CLUSTERED,
        EmailAddress    nvarchar(255)       NOT NULL,
        Created         datetimeoffset(7)   NOT NULL CONSTRAINT DF_Identity_Created DEFAULT (sysdatetime()),
        Modified        datetimeoffset(7)   NOT NULL CONSTRAINT DF_Identity_Modified DEFAULT (sysdatetime())
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