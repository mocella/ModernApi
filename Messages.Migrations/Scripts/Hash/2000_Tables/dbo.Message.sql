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
-- 2022-04-28   Tony Mocella    Added MessageGuid to schema
-------------------------------------------------------------------------------
IF (SELECT OBJECT_ID(N'dbo.Message', N'U')) IS NULL
BEGIN
    CREATE TABLE dbo.[Message] (
        MessageId     bigint            NOT NULL
            IDENTITY( 1, 1 )
            CONSTRAINT PK_Message_Id
                PRIMARY KEY CLUSTERED,
        MessageGuid   uniqueidentifier  NOT NULL CONSTRAINT DF_Message_MessageGuid DEFAULT (newid()),
        SenderId      bigint            NOT NULL,
        [Subject]     nvarchar(500)     NULL,
        Body          nvarchar(max)     NULL,                  
        Created       datetimeoffset(7) NOT NULL CONSTRAINT DF_Message_Created     DEFAULT (sysdatetime()),
        Modified      datetimeoffset(7) NOT NULL CONSTRAINT DF_Message_Modified    DEFAULT (sysdatetime())
    )
END
GO
-------------------------------------------------------------------------------
-- Here be alters
-------------------------------------------------------------------------------
IF NOT EXISTS ( SELECT  *
            FROM    INFORMATION_SCHEMA.COLUMNS
            WHERE   TABLE_NAME = 'Message'
                    AND TABLE_SCHEMA = 'dbo'
                    AND COLUMN_NAME = 'MessageGuid' )
    BEGIN
        ALTER TABLE dbo.Message
            ADD MessageGuid   uniqueidentifier  NOT NULL CONSTRAINT DF_Message_MessageGuid DEFAULT (newid());
    END;
GO
-------------------------------------------------------------------------------
-- Here be indexes
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------
-- Here be triggers
-------------------------------------------------------------------------------