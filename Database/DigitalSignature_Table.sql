-- ============================================
-- Digital Signature Table Creation Script
-- ============================================
-- This table stores digital signatures for doctors
-- Created: 2025-02
-- Follows the same pattern as DoctorDegree table
-- ============================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DigitalSignature] (
        [DigitalSignatureID] INT IDENTITY(1,1) PRIMARY KEY,
        [DoctorID] INT NOT NULL,
        [FileName] NVARCHAR(500) NULL,
        [OriginalFileName] NVARCHAR(500) NULL,
        [FilePath] NVARCHAR(1000) NULL,
        [FileSize] BIGINT NULL,
        [MimeType] NVARCHAR(100) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [TenantID] INT NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME NULL,
        [CreatedBy] INT NULL,
        [UpdatedBy] INT NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        -- Foreign Key Constraint
        CONSTRAINT [FK_DigitalSignature_Doctor] FOREIGN KEY ([DoctorID]) 
            REFERENCES [dbo].[Doctor] ([DoctorID]) ON DELETE CASCADE
    );
    
    -- Indexes
    CREATE INDEX [IX_DigitalSignature_DoctorID] ON [dbo].[DigitalSignature] ([DoctorID]);
    CREATE INDEX [IX_DigitalSignature_TenantID] ON [dbo].[DigitalSignature] ([TenantID]);
    CREATE INDEX [IX_DigitalSignature_IsActive] ON [dbo].[DigitalSignature] ([IsActive]);
    CREATE INDEX [IX_DigitalSignature_IsDeleted] ON [dbo].[DigitalSignature] ([IsDeleted]);
    
    PRINT 'DigitalSignature table created successfully.';
END
ELSE
BEGIN
    PRINT 'DigitalSignature table already exists.';
END
GO

-- ============================================
-- Stored Procedures for Digital Signature
-- Following the same pattern as DoctorDegree_* procedures
-- ============================================

-- Get All Digital Signatures
/****** Object:  StoredProcedure [dbo].[DigitalSignature_GetAll]    Script Date: 2025-02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature_GetAll]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DigitalSignature_GetAll];
GO

CREATE PROCEDURE [dbo].[DigitalSignature_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [DigitalSignatureID],
        [DoctorID],
        [FileName],
        [OriginalFileName],
        [FilePath],
        [FileSize],
        [MimeType],
        [IsActive],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [CreatedBy],
        [UpdatedBy],
        [IsDeleted]
    FROM [dbo].[DigitalSignature]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- Get Digital Signature by ID
/****** Object:  StoredProcedure [dbo].[DigitalSignature_GetById]    Script Date: 2025-02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature_GetById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DigitalSignature_GetById];
GO

CREATE PROCEDURE [dbo].[DigitalSignature_GetById]
    @DigitalSignatureID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        [DigitalSignatureID],
        [DoctorID],
        [FileName],
        [OriginalFileName],
        [FilePath],
        [FileSize],
        [MimeType],
        [IsActive],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [CreatedBy],
        [UpdatedBy],
        [IsDeleted]
    FROM [dbo].[DigitalSignature]
    WHERE [DigitalSignatureID] = @DigitalSignatureID 
        AND [IsDeleted] = 0;
END
GO

-- Get Digital Signature by Doctor ID
/****** Object:  StoredProcedure [dbo].[DigitalSignature_GetByDoctorId]    Script Date: 2025-02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature_GetByDoctorId]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DigitalSignature_GetByDoctorId];
GO

CREATE PROCEDURE [dbo].[DigitalSignature_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 1
        [DigitalSignatureID],
        [DoctorID],
        [FileName],
        [OriginalFileName],
        [FilePath],
        [FileSize],
        [MimeType],
        [IsActive],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [CreatedBy],
        [UpdatedBy],
        [IsDeleted]
    FROM [dbo].[DigitalSignature]
    WHERE [DoctorID] = @DoctorID 
        AND [IsDeleted] = 0 
        AND [IsActive] = 1
    ORDER BY [CreatedAt] DESC;
END
GO

-- Insert Digital Signature
/****** Object:  StoredProcedure [dbo].[DigitalSignature_Insert]    Script Date: 2025-02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature_Insert]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DigitalSignature_Insert];
GO

CREATE PROCEDURE [dbo].[DigitalSignature_Insert]
    @DigitalSignatureID INT,
    @DoctorID INT,
    @FileName NVARCHAR(500) = NULL,
    @OriginalFileName NVARCHAR(500) = NULL,
    @FilePath NVARCHAR(1000) = NULL,
    @FileSize BIGINT = NULL,
    @MimeType NVARCHAR(100) = NULL,
    @IsActive BIT = 1,
    @TenantID INT,
    @CreatedAt DATETIME = NULL,
    @UpdatedAt DATETIME = NULL,
    @CreatedBy INT = NULL,
    @UpdatedBy INT = NULL,
    @IsDeleted BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Deactivate existing signatures for this doctor
        UPDATE [dbo].[DigitalSignature]
        SET [IsActive] = 0, [UpdatedAt] = GETUTCDATE(), [UpdatedBy] = @CreatedBy
        WHERE [DoctorID] = @DoctorID AND [IsDeleted] = 0;
        
        -- Insert new signature
        INSERT INTO [dbo].[DigitalSignature] (
            [DoctorID], 
            [FileName], 
            [OriginalFileName], 
            [FilePath], 
            [FileSize], 
            [MimeType], 
            [IsActive],
            [TenantID], 
            [CreatedAt], 
            [UpdatedAt],
            [CreatedBy], 
            [UpdatedBy],
            [IsDeleted]
        )
        VALUES (
            @DoctorID, 
            @FileName, 
            @OriginalFileName, 
            @FilePath,
            @FileSize, 
            @MimeType, 
            @IsActive,
            @TenantID, 
            GETUTCDATE(), 
            GETUTCDATE(),
            @CreatedBy, 
            @UpdatedBy,
            @IsDeleted
        );
        
        -- Return the newly inserted ID
        SELECT SCOPE_IDENTITY() AS DigitalSignatureID;
    END TRY
    BEGIN CATCH
        SELECT 0 AS DigitalSignatureID;
    END CATCH
END
GO

-- Update Digital Signature
/****** Object:  StoredProcedure [dbo].[DigitalSignature_Update]    Script Date: 2025-02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature_Update]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DigitalSignature_Update];
GO

CREATE PROCEDURE [dbo].[DigitalSignature_Update]
    @DigitalSignatureID INT,
    @DoctorID INT = NULL,
    @FileName NVARCHAR(500) = NULL,
    @OriginalFileName NVARCHAR(500) = NULL,
    @FilePath NVARCHAR(1000) = NULL,
    @FileSize BIGINT = NULL,
    @MimeType NVARCHAR(100) = NULL,
    @IsActive BIT = NULL,
    @TenantID INT = NULL,
    @CreatedAt DATETIME = NULL,
    @UpdatedAt DATETIME = NULL,
    @CreatedBy INT = NULL,
    @UpdatedBy INT = NULL,
    @IsDeleted BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE [dbo].[DigitalSignature]
        SET 
            [DoctorID] = CASE WHEN @DoctorID > 0 THEN @DoctorID ELSE [DoctorID] END,
            [FileName] = ISNULL(@FileName, [FileName]),
            [OriginalFileName] = ISNULL(@OriginalFileName, [OriginalFileName]),
            [FilePath] = ISNULL(@FilePath, [FilePath]),
            [FileSize] = ISNULL(@FileSize, [FileSize]),
            [MimeType] = ISNULL(@MimeType, [MimeType]),
            [IsActive] = ISNULL(@IsActive, [IsActive]),
            [TenantID] = CASE WHEN @TenantID > 0 THEN @TenantID ELSE [TenantID] END,
            [UpdatedAt] = GETUTCDATE(),
            [UpdatedBy] = ISNULL(@UpdatedBy, [UpdatedBy]),
            [IsDeleted] = ISNULL(@IsDeleted, [IsDeleted])
        WHERE [DigitalSignatureID] = @DigitalSignatureID AND [IsDeleted] = 0;
        
        SELECT @DigitalSignatureID AS DigitalSignatureID;
    END TRY
    BEGIN CATCH
        SELECT 0 AS DigitalSignatureID;
    END CATCH
END
GO

-- Delete Digital Signature (Soft Delete)
/****** Object:  StoredProcedure [dbo].[DigitalSignature_DeleteById]    Script Date: 2025-02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DigitalSignature_DeleteById]') AND type in (N'P'))
    DROP PROCEDURE [dbo].[DigitalSignature_DeleteById];
GO

CREATE PROCEDURE [dbo].[DigitalSignature_DeleteById]
    @DigitalSignatureID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE [dbo].[DigitalSignature]
        SET 
            [IsDeleted] = 1,
            [IsActive] = 0,
            [UpdatedAt] = GETUTCDATE()
        WHERE [DigitalSignatureID] = @DigitalSignatureID;
        
        SELECT @DigitalSignatureID AS DigitalSignatureID;
    END TRY
    BEGIN CATCH
        SELECT 0 AS DigitalSignatureID;
    END CATCH
END
GO

PRINT 'Digital Signature stored procedures created successfully.';
GO

