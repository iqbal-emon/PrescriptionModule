-- =============================================
-- Specialization Module Stored Procedures
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: All stored procedures for Specialization module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Specialization_GetAll
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_GetAll]
GO

CREATE PROCEDURE [dbo].[Specialization_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SpecializationID],
        [SpecialityID],
        [SpecializationName],
        [Description],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Specialization]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- Specialization_GetById
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_GetById]
GO

CREATE PROCEDURE [dbo].[Specialization_GetById]
    @SpecializationID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SpecializationID],
        [SpecialityID],
        [SpecializationName],
        [Description],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Specialization]
    WHERE [SpecializationID] = @SpecializationID
        AND [IsDeleted] = 0;
END
GO

-- =============================================
-- Specialization_GetBySpecialityId
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_GetBySpecialityId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_GetBySpecialityId]
GO

CREATE PROCEDURE [dbo].[Specialization_GetBySpecialityId]
    @SpecialityID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SpecializationID],
        [SpecialityID],
        [SpecializationName],
        [Description],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Specialization]
    WHERE [SpecialityID] = @SpecialityID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- Specialization_Insert
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_Insert]
GO

CREATE PROCEDURE [dbo].[Specialization_Insert]
    @SpecialityID INT = NULL,
    @SpecializationName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @TenantID INT,
    @SpecializationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Specialization]
    (
        [SpecialityID],
        [SpecializationName],
        [Description],
        [TenantID],
        [CreatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @SpecialityID,
        @SpecializationName,
        @Description,
        @TenantID,
        GETDATE(),
        0
    );
    SET @SpecializationID = SCOPE_IDENTITY();
END
GO

-- =============================================
-- Specialization_Update
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_Update]
GO

CREATE PROCEDURE [dbo].[Specialization_Update]
    @SpecializationID INT,
    @SpecialityID INT = NULL,
    @SpecializationName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @TenantID INT,
    @UpdatedSpecializationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Specialization]
    SET 
        [SpecialityID] = @SpecialityID,
        [SpecializationName] = @SpecializationName,
        [Description] = @Description,
        [TenantID] = @TenantID,
        [UpdatedAt] = GETDATE()
    WHERE [SpecializationID] = @SpecializationID
        AND [IsDeleted] = 0;
    
    SET @UpdatedSpecializationID = @SpecializationID;
END
GO

-- =============================================
-- Specialization_DeleteById
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_DeleteById]
GO

CREATE PROCEDURE [dbo].[Specialization_DeleteById]
    @SpecializationID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Specialization]
    SET 
        [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [SpecializationID] = @SpecializationID;
END
GO

PRINT 'Specialization module stored procedures created successfully.';
GO

