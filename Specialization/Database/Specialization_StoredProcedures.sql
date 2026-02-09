USE [SoowGood_System]
GO

-- =============================================
-- Specialization Stored Procedures
-- Created to support SpecializationController
-- =============================================

-- Check if Specialization table exists, if not create it
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Specialization](
        [SpecializationID] [int] IDENTITY(1,1) NOT NULL,
        [SpecialityID] [int] NULL,
        [SpecializationName] [nvarchar](200) NOT NULL,
        [Description] [nvarchar](500) NULL,
        [TenantID] [int] NOT NULL,
        [CreatedAt] [datetime] NOT NULL,
        [UpdatedAt] [datetime] NULL,
        [IsDeleted] [bit] NOT NULL DEFAULT 0,
        PRIMARY KEY CLUSTERED ([SpecializationID] ASC)
    )
END
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
-- Specialization_GetFiltered
-- Get specializations that are used by doctors
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_GetFiltered]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_GetFiltered]
GO

CREATE PROCEDURE [dbo].[Specialization_GetFiltered]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT
        s.[SpecializationID],
        s.[SpecialityID],
        s.[SpecializationName],
        s.[Description],
        s.[TenantID],
        s.[CreatedAt],
        s.[UpdatedAt],
        s.[IsDeleted]
    FROM [dbo].[Specialization] s
    INNER JOIN [dbo].[DoctorSpecialization] ds ON s.[SpecializationID] = ds.[SpecializationID]
    WHERE s.[IsDeleted] = 0
        AND ds.[IsDeleted] = 0
    ORDER BY s.[CreatedAt] DESC;
END
GO

-- =============================================
-- Specialization_Insert
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Specialization_Insert]
GO

CREATE PROCEDURE [dbo].[Specialization_Insert]
    @SpecializationName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @SpecialityID INT = NULL,
    @TenantID INT,
    @SpecializationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Specialization]
    (
        [SpecializationName],
        [Description],
        [SpecialityID],
        [TenantID],
        [CreatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @SpecializationName,
        @Description,
        @SpecialityID,
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
    @SpecializationName NVARCHAR(200) = NULL,
    @Description NVARCHAR(500) = NULL,
    @SpecialityID INT = NULL,
    @UpdatedSpecializationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Specialization]
    SET 
        [SpecializationName] = ISNULL(@SpecializationName, [SpecializationName]),
        [Description] = ISNULL(@Description, [Description]),
        [SpecialityID] = @SpecialityID,
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

