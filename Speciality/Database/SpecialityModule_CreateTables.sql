-- =============================================
-- Speciality Module Database Tables
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Create tables for Speciality module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Speciality Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Speciality]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Speciality]
    (
        [SpecialityID] INT IDENTITY(1,1) PRIMARY KEY,
        [SpecialityName] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(500) NULL,
        [TenantID] INT NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        INDEX [IX_Speciality_TenantID] ([TenantID]),
        INDEX [IX_Speciality_IsDeleted] ([IsDeleted])
    );
    PRINT 'Table Speciality created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Speciality already exists.';
END
GO

PRINT 'Speciality module table creation script completed.';
GO

