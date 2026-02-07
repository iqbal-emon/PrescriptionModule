-- =============================================
-- Documents Attachment Module Stored Procedures
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: All stored procedures for Documents Attachment module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- DocumentsAttachment_GetAll
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_GetAll]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DocumentsAttachmentID],
        [FileName],
        [OriginalFileName],
        [Path],
        [EntityType],
        [EntityId],
        [AttachmentType],
        [RelatedEntityid],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DocumentsAttachment]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- DocumentsAttachment_GetById
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_GetById]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_GetById]
    @DocumentsAttachmentID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DocumentsAttachmentID],
        [FileName],
        [OriginalFileName],
        [Path],
        [EntityType],
        [EntityId],
        [AttachmentType],
        [RelatedEntityid],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DocumentsAttachment]
    WHERE [DocumentsAttachmentID] = @DocumentsAttachmentID
        AND [IsDeleted] = 0;
END
GO

-- =============================================
-- DocumentsAttachment_GetByEntityIdAndType
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_GetByEntityIdAndType]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_GetByEntityIdAndType]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_GetByEntityIdAndType]
    @EntityId INT,
    @EntityType NVARCHAR(50) = NULL,
    @AttachmentType NVARCHAR(50) = NULL,
    @RelatedEntityid INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DocumentsAttachmentID],
        [FileName],
        [OriginalFileName],
        [Path],
        [EntityType],
        [EntityId],
        [AttachmentType],
        [RelatedEntityid],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DocumentsAttachment]
    WHERE [EntityId] = @EntityId
        AND (@EntityType IS NULL OR [EntityType] = @EntityType)
        AND (@AttachmentType IS NULL OR [AttachmentType] = @AttachmentType)
        AND (@RelatedEntityid IS NULL OR [RelatedEntityid] = @RelatedEntityid)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- DocumentsAttachment_GetDocumentInfo
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_GetDocumentInfo]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_GetDocumentInfo]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_GetDocumentInfo]
    @EntityId INT,
    @EntityType NVARCHAR(50) = NULL,
    @AttachmentType NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1
        [DocumentsAttachmentID],
        [FileName],
        [OriginalFileName],
        [Path],
        [EntityType],
        [EntityId],
        [AttachmentType],
        [RelatedEntityid],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DocumentsAttachment]
    WHERE [EntityId] = @EntityId
        AND (@EntityType IS NULL OR [EntityType] = @EntityType)
        AND (@AttachmentType IS NULL OR [AttachmentType] = @AttachmentType)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- DocumentsAttachment_GetPaginated
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_GetPaginated]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_GetPaginated]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_GetPaginated]
    @Sorting NVARCHAR(100) = '',
    @SkipCount INT = 0,
    @MaxResultCount INT = 10
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'
    SELECT 
        [DocumentsAttachmentID],
        [FileName],
        [OriginalFileName],
        [Path],
        [EntityType],
        [EntityId],
        [AttachmentType],
        [RelatedEntityid],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DocumentsAttachment]
    WHERE [IsDeleted] = 0';
    
    IF @Sorting IS NOT NULL AND @Sorting != ''
    BEGIN
        SET @SQL = @SQL + N' ORDER BY ' + @Sorting;
    END
    ELSE
    BEGIN
        SET @SQL = @SQL + N' ORDER BY [CreatedAt] DESC';
    END
    
    SET @SQL = @SQL + N' OFFSET ' + CAST(@SkipCount AS NVARCHAR(10)) + N' ROWS FETCH NEXT ' + CAST(@MaxResultCount AS NVARCHAR(10)) + N' ROWS ONLY';
    
    EXEC sp_executesql @SQL;
END
GO

-- =============================================
-- DocumentsAttachment_Insert
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_Insert]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_Insert]
    @FileName NVARCHAR(500) = NULL,
    @OriginalFileName NVARCHAR(500) = NULL,
    @Path NVARCHAR(1000) = NULL,
    @EntityType NVARCHAR(50) = NULL,
    @EntityId INT = NULL,
    @AttachmentType NVARCHAR(50) = NULL,
    @RelatedEntityid INT = NULL,
    @TenantID INT,
    @DocumentsAttachmentID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[DocumentsAttachment]
    (
        [FileName],
        [OriginalFileName],
        [Path],
        [EntityType],
        [EntityId],
        [AttachmentType],
        [RelatedEntityid],
        [TenantID],
        [CreatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @FileName,
        @OriginalFileName,
        @Path,
        @EntityType,
        @EntityId,
        @AttachmentType,
        @RelatedEntityid,
        @TenantID,
        GETDATE(),
        0
    );
    SET @DocumentsAttachmentID = SCOPE_IDENTITY();
END
GO

-- =============================================
-- DocumentsAttachment_Update
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_Update]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_Update]
    @DocumentsAttachmentID INT,
    @FileName NVARCHAR(500) = NULL,
    @OriginalFileName NVARCHAR(500) = NULL,
    @Path NVARCHAR(1000) = NULL,
    @EntityType NVARCHAR(50) = NULL,
    @EntityId INT = NULL,
    @AttachmentType NVARCHAR(50) = NULL,
    @RelatedEntityid INT = NULL,
    @TenantID INT,
    @UpdatedDocumentsAttachmentID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DocumentsAttachment]
    SET 
        [FileName] = @FileName,
        [OriginalFileName] = @OriginalFileName,
        [Path] = @Path,
        [EntityType] = @EntityType,
        [EntityId] = @EntityId,
        [AttachmentType] = @AttachmentType,
        [RelatedEntityid] = @RelatedEntityid,
        [TenantID] = @TenantID,
        [UpdatedAt] = GETDATE()
    WHERE [DocumentsAttachmentID] = @DocumentsAttachmentID
        AND [IsDeleted] = 0;
    
    SET @UpdatedDocumentsAttachmentID = @DocumentsAttachmentID;
END
GO

-- =============================================
-- DocumentsAttachment_DeleteById
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DocumentsAttachment_DeleteById]
GO

CREATE PROCEDURE [dbo].[DocumentsAttachment_DeleteById]
    @DocumentsAttachmentID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DocumentsAttachment]
    SET 
        [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [DocumentsAttachmentID] = @DocumentsAttachmentID;
END
GO

PRINT 'Documents Attachment module stored procedures created successfully.';
GO

