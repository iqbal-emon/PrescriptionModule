-- =============================================
-- Speciality Module Stored Procedures
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: All stored procedures for Speciality module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Speciality_GetAll
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Speciality_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Speciality_GetAll]
GO

CREATE PROCEDURE [dbo].[Speciality_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SpecialityID],
        [SpecialityName],
        [Description],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Speciality]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- Speciality_GetById
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Speciality_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Speciality_GetById]
GO

CREATE PROCEDURE [dbo].[Speciality_GetById]
    @SpecialityID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SpecialityID],
        [SpecialityName],
        [Description],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Speciality]
    WHERE [SpecialityID] = @SpecialityID
        AND [IsDeleted] = 0;
END
GO

-- =============================================
-- Speciality_Insert
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Speciality_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Speciality_Insert]
GO

CREATE PROCEDURE [dbo].[Speciality_Insert]
    @SpecialityName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @TenantID INT,
    @SpecialityID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Speciality]
    (
        [SpecialityName],
        [Description],
        [TenantID],
        [CreatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @SpecialityName,
        @Description,
        @TenantID,
        GETDATE(),
        0
    );
    SET @SpecialityID = SCOPE_IDENTITY();
END
GO

-- =============================================
-- Speciality_Update
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Speciality_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Speciality_Update]
GO

CREATE PROCEDURE [dbo].[Speciality_Update]
    @SpecialityID INT,
    @SpecialityName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @TenantID INT,
    @UpdatedSpecialityID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Speciality]
    SET 
        [SpecialityName] = @SpecialityName,
        [Description] = @Description,
        [TenantID] = @TenantID,
        [UpdatedAt] = GETDATE()
    WHERE [SpecialityID] = @SpecialityID
        AND [IsDeleted] = 0;
    
    SET @UpdatedSpecialityID = @SpecialityID;
END
GO

-- =============================================
-- Speciality_DeleteById
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Speciality_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Speciality_DeleteById]
GO

CREATE PROCEDURE [dbo].[Speciality_DeleteById]
    @SpecialityID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Speciality]
    SET 
        [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [SpecialityID] = @SpecialityID;
END
GO

PRINT 'Speciality module stored procedures created successfully.';
GO

