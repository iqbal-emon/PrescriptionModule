-- =============================================
-- Documents Attachment Module Database Tables
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Create tables for Documents Attachment module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- DocumentsAttachment Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentsAttachment]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DocumentsAttachment]
    (
        [DocumentsAttachmentID] INT IDENTITY(1,1) PRIMARY KEY,
        [FileName] NVARCHAR(500) NULL,
        [OriginalFileName] NVARCHAR(500) NULL,
        [Path] NVARCHAR(1000) NULL,
        [EntityType] NVARCHAR(50) NULL,  -- Doctor, Patient, etc.
        [EntityId] INT NULL,
        [AttachmentType] NVARCHAR(50) NULL,  -- ProfilePicture, Document, etc.
        [RelatedEntityid] INT NULL,
        [TenantID] INT NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        INDEX [IX_DocumentsAttachment_EntityId] ([EntityId]),
        INDEX [IX_DocumentsAttachment_EntityType] ([EntityType]),
        INDEX [IX_DocumentsAttachment_AttachmentType] ([AttachmentType]),
        INDEX [IX_DocumentsAttachment_TenantID] ([TenantID]),
        INDEX [IX_DocumentsAttachment_IsDeleted] ([IsDeleted])
    );
    PRINT 'Table DocumentsAttachment created successfully.';
END
ELSE
BEGIN
    PRINT 'Table DocumentsAttachment already exists.';
END
GO

PRINT 'Documents Attachment module table creation script completed.';
GO

