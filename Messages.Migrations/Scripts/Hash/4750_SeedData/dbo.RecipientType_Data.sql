USE [$DbName$]
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

-------------------------------------------------------------------------------
-- Author       Tony Mocella    
-- Created      2022-04-26
-- Purpose      RecipientType Seed Data
--
-- Copyright © 2022, mocella, All Rights Reserved
-------------------------------------------------------------------------------
-- Modification History (most recent first)
-------------------------------------------------------------------------------
--       
-------------------------------------------------------------------------------
SET IDENTITY_INSERT dbo.RecipientType ON;

WITH src AS (
    SELECT * FROM (VALUES
        (1, 'TO'),
        (2, 'CC'),
        (3, 'BCC')
    ) AS s (RecipientTypeId, [Name])
)
MERGE dbo.RecipientType AS dst
    USING src
    ON src.RecipientTypeId = dst.RecipientTypeId
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (RecipientTypeId, [Name])
        VALUES
        (
            src.RecipientTypeId,
            src.[Name]
        )
;

SET IDENTITY_INSERT dbo.RecipientType OFF