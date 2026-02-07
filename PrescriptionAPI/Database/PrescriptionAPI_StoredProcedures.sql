-- =============================================
-- Prescription API Module Stored Procedures
-- Database: Prescripto (or your database name)
-- Created: 2025-02-07
-- Description: All stored procedures for Prescription API module entities
-- 
-- IMPORTANT: Run PrescriptionAPI_CreateTables.sql first to create the required tables
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- 1. CommonAdvices Stored Procedures
-- =============================================

-- CommonAdvices_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonAdvices_GetAll]
GO

CREATE PROCEDURE [dbo].[CommonAdvices_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CommonAdviceID],
        [Advice],
        [Type],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[CommonAdvices]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- CommonAdvices_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonAdvices_GetById]
GO

CREATE PROCEDURE [dbo].[CommonAdvices_GetById]
    @CommonAdviceID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CommonAdviceID],
        [Advice],
        [Type],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[CommonAdvices]
    WHERE [CommonAdviceID] = @CommonAdviceID
        AND [IsDeleted] = 0;
END
GO

-- CommonAdvices_GetByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices_GetByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonAdvices_GetByName]
GO

CREATE PROCEDURE [dbo].[CommonAdvices_GetByName]
    @AdviceName NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CommonAdviceID],
        [Advice],
        [Type],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[CommonAdvices]
    WHERE ([Advice] LIKE '%' + @AdviceName + '%' OR @AdviceName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- GetBookMarksAdviceByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBookMarksAdviceByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBookMarksAdviceByDoctorId]
GO

CREATE PROCEDURE [dbo].[GetBookMarksAdviceByDoctorId]
    @DoctorReferenceID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- This procedure should join with a bookmarks table if it exists
    -- For now, returning all active advice (adjust based on your bookmark implementation)
    SELECT 
        ca.[CommonAdviceID],
        ca.[Advice],
        ca.[Type],
        ca.[CreatedAt],
        ca.[UpdatedAt],
        ca.[IsDeleted],
        ca.[IsActive]
    FROM [dbo].[CommonAdvices] ca
    WHERE ca.[IsDeleted] = 0
        AND ca.[IsActive] = 1
    ORDER BY ca.[CreatedAt] DESC;
END
GO

-- CommonAdvices_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonAdvices_Insert]
GO

CREATE PROCEDURE [dbo].[CommonAdvices_Insert]
    @Advice NVARCHAR(MAX),
    @Type NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @CommonAdviceID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[CommonAdvices]
    (
        [Advice],
        [Type],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @Advice,
        @Type,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @CommonAdviceID = SCOPE_IDENTITY();
END
GO

-- CommonAdvices_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonAdvices_Update]
GO

CREATE PROCEDURE [dbo].[CommonAdvices_Update]
    @CommonAdviceID INT,
    @Advice NVARCHAR(MAX),
    @Type NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CommonAdvices]
    SET 
        [Advice] = @Advice,
        [Type] = @Type,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [CommonAdviceID] = @CommonAdviceID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @CommonAdviceID;
END
GO

-- CommonAdvices_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonAdvices_DeleteById]
GO

CREATE PROCEDURE [dbo].[CommonAdvices_DeleteById]
    @CommonAdviceID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CommonAdvices]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [CommonAdviceID] = @CommonAdviceID;
END
GO

-- =============================================
-- 2. Diagnosis Stored Procedures
-- =============================================

-- Diagnosis_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_GetAll]
GO

CREATE PROCEDURE [dbo].[Diagnosis_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DiagonosisID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Diagonosis]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- Diagnosis_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_GetById]
GO

CREATE PROCEDURE [dbo].[Diagnosis_GetById]
    @DiagnosisId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DiagonosisID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Diagonosis]
    WHERE [DiagonosisID] = @DiagnosisId
        AND [IsDeleted] = 0;
END
GO

-- Diagonosis_GetDiagnosesByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagonosis_GetDiagnosesByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagonosis_GetDiagnosesByName]
GO

CREATE PROCEDURE [dbo].[Diagonosis_GetDiagnosesByName]
    @DiagnosisName NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DiagonosisID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Diagonosis]
    WHERE ([Name] LIKE '%' + @DiagnosisName + '%' OR @DiagnosisName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- GetBookMarksDiagnosisByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBookMarksDiagnosisByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBookMarksDiagnosisByDoctorId]
GO

CREATE PROCEDURE [dbo].[GetBookMarksDiagnosisByDoctorId]
    @DoctorReferenceID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        d.[DiagonosisID],
        d.[Name],
        d.[Description],
        d.[Code],
        d.[CreatedAt],
        d.[UpdatedAt],
        d.[IsDeleted],
        d.[IsActive]
    FROM [dbo].[Diagonosis] d
    WHERE d.[IsDeleted] = 0
        AND d.[IsActive] = 1
    ORDER BY d.[CreatedAt] DESC;
END
GO

-- Diagnosis_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_Insert]
GO

CREATE PROCEDURE [dbo].[Diagnosis_Insert]
    @Name NVARCHAR(500),
    @Description NVARCHAR(MAX) = NULL,
    @Code NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @DiagonosisID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Diagonosis]
    (
        [Name],
        [Description],
        [Code],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @Name,
        @Description,
        @Code,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @DiagonosisID = SCOPE_IDENTITY();
END
GO

-- Diagnosis_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_Update]
GO

CREATE PROCEDURE [dbo].[Diagnosis_Update]
    @DiagonosisID INT,
    @Name NVARCHAR(500),
    @Description NVARCHAR(MAX) = NULL,
    @Code NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Diagonosis]
    SET 
        [Name] = @Name,
        [Description] = @Description,
        [Code] = @Code,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [DiagonosisID] = @DiagonosisID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @DiagonosisID;
END
GO

-- Diagnosis_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_DeleteById]
GO

CREATE PROCEDURE [dbo].[Diagnosis_DeleteById]
    @DiagonosisID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Diagonosis]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [DiagonosisID] = @DiagonosisID;
END
GO

-- =============================================
-- 3. Symptom Stored Procedures
-- =============================================

-- Symptom_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_GetAll]
GO

CREATE PROCEDURE [dbo].[Symptom_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SymptomID],
        [TenantID],
        [SymptomName],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Symptoms]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- Symptom_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_GetById]
GO

CREATE PROCEDURE [dbo].[Symptom_GetById]
    @SymptomId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SymptomID],
        [TenantID],
        [SymptomName],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Symptoms]
    WHERE [SymptomID] = @SymptomId
        AND [IsDeleted] = 0;
END
GO

-- Symptom_GetSymptomsByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_GetSymptomsByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_GetSymptomsByName]
GO

CREATE PROCEDURE [dbo].[Symptom_GetSymptomsByName]
    @SymptomName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [SymptomID],
        [TenantID],
        [SymptomName],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Symptoms]
    WHERE ([SymptomName] LIKE '%' + @SymptomName + '%' OR @SymptomName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- GetBookMarksSymtomByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBookMarksSymtomByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBookMarksSymtomByDoctorId]
GO

CREATE PROCEDURE [dbo].[GetBookMarksSymtomByDoctorId]
    @DoctorReferenceID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        s.[SymptomID],
        s.[TenantID],
        s.[SymptomName],
        s.[Description],
        s.[CreatedAt],
        s.[UpdatedAt],
        s.[IsDeleted]
    FROM [dbo].[Symptoms] s
    WHERE s.[IsDeleted] = 0
    ORDER BY s.[CreatedAt] DESC;
END
GO

-- Symptom_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_Insert]
GO

CREATE PROCEDURE [dbo].[Symptom_Insert]
    @TenantID INT,
    @SymptomName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @SymptomID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Symptoms]
    (
        [TenantID],
        [SymptomName],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @TenantID,
        @SymptomName,
        @Description,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @SymptomID = SCOPE_IDENTITY();
END
GO

-- Symptom_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_Update]
GO

CREATE PROCEDURE [dbo].[Symptom_Update]
    @SymptomID INT,
    @TenantID INT,
    @SymptomName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Symptoms]
    SET 
        [TenantID] = @TenantID,
        [SymptomName] = @SymptomName,
        [Description] = @Description,
        [UpdatedAt] = GETDATE()
    WHERE [SymptomID] = @SymptomID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @SymptomID;
END
GO

-- Symptom_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_DeleteById]
GO

CREATE PROCEDURE [dbo].[Symptom_DeleteById]
    @SymptomID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Symptoms]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [SymptomID] = @SymptomID;
END
GO

-- =============================================
-- 4. CommonHistory Stored Procedures
-- =============================================

-- CommonHistory_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonHistory_GetAll]
GO

CREATE PROCEDURE [dbo].[CommonHistory_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CommonHistoryId],
        [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[CommonHistory]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- CommonHistory_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonHistory_GetById]
GO

CREATE PROCEDURE [dbo].[CommonHistory_GetById]
    @CommonHistoryID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CommonHistoryId],
        [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[CommonHistory]
    WHERE [CommonHistoryId] = @CommonHistoryID
        AND [IsDeleted] = 0;
END
GO

-- CommonHistory_GetByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory_GetByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonHistory_GetByName]
GO

CREATE PROCEDURE [dbo].[CommonHistory_GetByName]
    @CommonHistoryName NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CommonHistoryId],
        [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[CommonHistory]
    WHERE ([Name] LIKE '%' + @CommonHistoryName + '%' OR @CommonHistoryName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- GetBookMarksHistoryByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBookMarksHistoryByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBookMarksHistoryByDoctorId]
GO

CREATE PROCEDURE [dbo].[GetBookMarksHistoryByDoctorId]
    @DoctorReferenceID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ch.[CommonHistoryId],
        ch.[Name],
        ch.[Description],
        ch.[CreatedAt],
        ch.[UpdatedAt],
        ch.[IsDeleted],
        ch.[IsActive]
    FROM [dbo].[CommonHistory] ch
    WHERE ch.[IsDeleted] = 0
        AND ch.[IsActive] = 1
    ORDER BY ch.[CreatedAt] DESC;
END
GO

-- CommonHistory_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonHistory_Insert]
GO

CREATE PROCEDURE [dbo].[CommonHistory_Insert]
    @Name NVARCHAR(500),
    @Description NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @CommonHistoryId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[CommonHistory]
    (
        [Name],
        [Description],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @Name,
        @Description,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @CommonHistoryId = SCOPE_IDENTITY();
END
GO

-- CommonHistory_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonHistory_Update]
GO

CREATE PROCEDURE [dbo].[CommonHistory_Update]
    @CommonHistoryId INT,
    @Name NVARCHAR(500),
    @Description NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CommonHistory]
    SET 
        [Name] = @Name,
        [Description] = @Description,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [CommonHistoryId] = @CommonHistoryId
        AND [IsDeleted] = 0;
    SET @UpdatedId = @CommonHistoryId;
END
GO

-- CommonHistory_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CommonHistory_DeleteById]
GO

CREATE PROCEDURE [dbo].[CommonHistory_DeleteById]
    @CommonHistoryId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CommonHistory]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [CommonHistoryId] = @CommonHistoryId;
END
GO

-- =============================================
-- 5. Investigation Stored Procedures
-- =============================================

-- Investigations_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigations_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigations_GetAll]
GO

CREATE PROCEDURE [dbo].[Investigations_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [InvestigationID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Investigation]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- Investigations_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigations_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigations_GetById]
GO

CREATE PROCEDURE [dbo].[Investigations_GetById]
    @InvestigationId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [InvestigationID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Investigation]
    WHERE [InvestigationID] = @InvestigationId
        AND [IsDeleted] = 0;
END
GO

-- Investigation_GetInvestigationsByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigation_GetInvestigationsByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigation_GetInvestigationsByName]
GO

CREATE PROCEDURE [dbo].[Investigation_GetInvestigationsByName]
    @InvestigationName NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [InvestigationID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Investigation]
    WHERE ([Name] LIKE '%' + @InvestigationName + '%' OR @InvestigationName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- Investigations_BookMarks
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigations_BookMarks]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigations_BookMarks]
GO

CREATE PROCEDURE [dbo].[Investigations_BookMarks]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [InvestigationID],
        [Name],
        [Description],
        [Code],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Investigation]
    WHERE [IsDeleted] = 0
        AND [IsActive] = 1
    ORDER BY [CreatedAt] DESC;
END
GO

-- Investigation_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigation_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigation_Insert]
GO

CREATE PROCEDURE [dbo].[Investigation_Insert]
    @Name NVARCHAR(500),
    @Description NVARCHAR(MAX) = NULL,
    @Code NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @InvestigationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Investigation]
    (
        [Name],
        [Description],
        [Code],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @Name,
        @Description,
        @Code,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @InvestigationID = SCOPE_IDENTITY();
END
GO

-- Investigation_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigation_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigation_Update]
GO

CREATE PROCEDURE [dbo].[Investigation_Update]
    @InvestigationID INT,
    @Name NVARCHAR(500),
    @Description NVARCHAR(MAX) = NULL,
    @Code NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Investigation]
    SET 
        [Name] = @Name,
        [Description] = @Description,
        [Code] = @Code,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [InvestigationID] = @InvestigationID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @InvestigationID;
END
GO

-- Investigation_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigation_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Investigation_DeleteById]
GO

CREATE PROCEDURE [dbo].[Investigation_DeleteById]
    @InvestigationID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Investigation]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [InvestigationID] = @InvestigationID;
END
GO

-- =============================================
-- 6. Medication Stored Procedures
-- =============================================

-- Medication_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_GetAll]
GO

CREATE PROCEDURE [dbo].[Medication_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [MedicationId],
        [TenantId],
        [MedicationBrandId],
        [GenericName],
        [DAR],
        [MedicationName],
        [Description],
        [Manufacturer],
        [DosageForm],
        [Strength],
        [Indication],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Medications]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- Medication_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_GetById]
GO

CREATE PROCEDURE [dbo].[Medication_GetById]
    @MedicationId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [MedicationId],
        [TenantId],
        [MedicationBrandId],
        [GenericName],
        [DAR],
        [MedicationName],
        [Description],
        [Manufacturer],
        [DosageForm],
        [Strength],
        [Indication],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Medications]
    WHERE [MedicationId] = @MedicationId
        AND [IsDeleted] = 0;
END
GO

-- Medication_GetByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_GetByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_GetByName]
GO

CREATE PROCEDURE [dbo].[Medication_GetByName]
    @MedicationName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [MedicationId],
        [TenantId],
        [MedicationBrandId],
        [GenericName],
        [DAR],
        [MedicationName],
        [Description],
        [Manufacturer],
        [DosageForm],
        [Strength],
        [Indication],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted],
        [IsActive]
    FROM [dbo].[Medications]
    WHERE ([MedicationName] LIKE '%' + @MedicationName + '%' OR @MedicationName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- GetBookMarksMedicationByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBookMarksMedicationByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[GetBookMarksMedicationByDoctorId]
GO

CREATE PROCEDURE [dbo].[GetBookMarksMedicationByDoctorId]
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        m.[MedicationId],
        m.[TenantId],
        m.[MedicationBrandId],
        m.[GenericName],
        m.[DAR],
        m.[MedicationName],
        m.[Description],
        m.[Manufacturer],
        m.[DosageForm],
        m.[Strength],
        m.[Indication],
        m.[CreatedAt],
        m.[UpdatedAt],
        m.[IsDeleted],
        m.[IsActive]
    FROM [dbo].[Medications] m
    WHERE m.[IsDeleted] = 0
        AND m.[IsActive] = 1
    ORDER BY m.[CreatedAt] DESC;
END
GO

-- Medication_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_Insert]
GO

CREATE PROCEDURE [dbo].[Medication_Insert]
    @TenantId INT,
    @MedicationBrandId INT,
    @GenericName NVARCHAR(255),
    @DAR NVARCHAR(50),
    @MedicationName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @Manufacturer NVARCHAR(100) = NULL,
    @DosageForm NVARCHAR(50) = NULL,
    @Strength NVARCHAR(50) = NULL,
    @Indication NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @MedicationId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Medications]
    (
        [TenantID],
        [MedicationBrandId],
        [GenericName],
        [DAR],
        [MedicationName],
        [Description],
        [Manufacturer],
        [DosageForm],
        [Strength],
        [Indication],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @TenantId,
        @MedicationBrandId,
        @GenericName,
        @DAR,
        @MedicationName,
        @Description,
        @Manufacturer,
        @DosageForm,
        @Strength,
        @Indication,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @MedicationId = SCOPE_IDENTITY();
END
GO

-- Medication_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_Update]
GO

CREATE PROCEDURE [dbo].[Medication_Update]
    @MedicationId INT,
    @TenantId INT,
    @MedicationBrandId INT,
    @GenericName NVARCHAR(255),
    @DAR NVARCHAR(50),
    @MedicationName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @Manufacturer NVARCHAR(100) = NULL,
    @DosageForm NVARCHAR(50) = NULL,
    @Strength NVARCHAR(50) = NULL,
    @Indication NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Medications]
    SET 
        [TenantID] = @TenantId,
        [MedicationBrandId] = @MedicationBrandId,
        [GenericName] = @GenericName,
        [DAR] = @DAR,
        [MedicationName] = @MedicationName,
        [Description] = @Description,
        [Manufacturer] = @Manufacturer,
        [DosageForm] = @DosageForm,
        [Strength] = @Strength,
        [Indication] = @Indication,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [MedicationId] = @MedicationId
        AND [IsDeleted] = 0;
    SET @UpdatedId = @MedicationId;
END
GO

-- Medication_DeledeById (Note: Typo in repository, using exact name)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_DeledeById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_DeledeById]
GO

CREATE PROCEDURE [dbo].[Medication_DeledeById]
    @MedicationId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Medications]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [MedicationId] = @MedicationId;
END
GO

-- =============================================
-- 7. FollowUp Stored Procedures
-- =============================================

-- FollowUp_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_GetAll]
GO

CREATE PROCEDURE [dbo].[FollowUp_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [PatientFollowUpID] AS [FollowUpId],
        [TenantID] AS [TenantId],
        [FollowUp] AS [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[PatientFollowUp]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- FollowUp_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_GetById]
GO

CREATE PROCEDURE [dbo].[FollowUp_GetById]
    @FollowUpId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [PatientFollowUpID] AS [FollowUpId],
        [TenantID] AS [TenantId],
        [FollowUp] AS [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[PatientFollowUp]
    WHERE [PatientFollowUpID] = @FollowUpId
        AND [IsDeleted] = 0;
END
GO

-- FollowUp_GetFollowUpsByName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_GetFollowUpsByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_GetFollowUpsByName]
GO

CREATE PROCEDURE [dbo].[FollowUp_GetFollowUpsByName]
    @FollowUpName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [PatientFollowUpID] AS [FollowUpId],
        [TenantID] AS [TenantId],
        [FollowUp] AS [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[PatientFollowUp]
    WHERE ([FollowUp] LIKE '%' + @FollowUpName + '%' OR @FollowUpName IS NULL)
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- FollowUp_BookMarks
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_BookMarks]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_BookMarks]
GO

CREATE PROCEDURE [dbo].[FollowUp_BookMarks]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [PatientFollowUpID] AS [FollowUpId],
        [TenantID] AS [TenantId],
        [FollowUp] AS [Name],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[PatientFollowUp]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- FollowUp_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_Insert]
GO

CREATE PROCEDURE [dbo].[FollowUp_Insert]
    @TenantId INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @FollowUpId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[PatientFollowUp]
    (
        [TenantID],
        [FollowUp],
        [Description],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @TenantId,
        @Name,
        @Description,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @FollowUpId = SCOPE_IDENTITY();
END
GO

-- FollowUp_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_Update]
GO

CREATE PROCEDURE [dbo].[FollowUp_Update]
    @FollowUpId INT,
    @TenantId INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[PatientFollowUp]
    SET 
        [TenantID] = @TenantId,
        [FollowUp] = @Name,
        [Description] = @Description,
        [UpdatedAt] = GETDATE()
    WHERE [PatientFollowUpID] = @FollowUpId
        AND [IsDeleted] = 0;
    SET @UpdatedId = @FollowUpId;
END
GO

-- FollowUp_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_DeleteById]
GO

CREATE PROCEDURE [dbo].[FollowUp_DeleteById]
    @FollowUpId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[PatientFollowUp]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [PatientFollowUpID] = @FollowUpId;
END
GO

-- FollowUp_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_DeleteById]
GO

CREATE PROCEDURE [dbo].[FollowUp_DeleteById]
    @FollowUpId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[PatientFollowUp]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [FollowUpId] = @FollowUpId;
END
GO

-- =============================================
-- END OF STORED PROCEDURES
-- =============================================
-- Total Procedures Created: 49
-- 1. CommonAdvices: 7 procedures
-- 2. Diagnosis: 7 procedures
-- 3. Symptom: 7 procedures
-- 4. CommonHistory: 7 procedures
-- 5. Investigation: 7 procedures
-- 6. Medication: 7 procedures
-- 7. FollowUp: 7 procedures
-- =============================================

PRINT 'All Prescription API module stored procedures creation script completed.';
GO

