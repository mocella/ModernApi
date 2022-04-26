USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      Message Table
--
-- Copyright © 2022, mocella, All Rights Reserved
-------------------------------------------------------------------------------
-- Modification History (most recent first)
-------------------------------------------------------------------------------
--       
-------------------------------------------------------------------------------
IF (SELECT OBJECT_ID(N'dbo.Message', N'U')) IS NULL
BEGIN
    CREATE TABLE dbo.[Message] (
        MessageId     bigint            NOT NULL
            IDENTITY( 1, 1 )
            CONSTRAINT PK_Message_Id
                PRIMARY KEY CLUSTERED,
        SenderId      bigint            NOT NULL,
        [Subject]     nvarchar(500)     NULL,
        Body          nvarchar(max)     NULL,                  
        Created       datetimeoffset(7) NOT NULL CONSTRAINT DF_Message_Created DEFAULT (sysdatetime()),
        Modified      datetimeoffset(7) NOT NULL CONSTRAINT DF_Message_Modified DEFAULT (sysdatetime())
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