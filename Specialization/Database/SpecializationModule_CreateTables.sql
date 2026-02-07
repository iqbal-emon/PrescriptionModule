-- =============================================
-- Specialization Module Database Tables
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Create tables for Specialization module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Specialization Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialization]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Specialization]
    (
        [SpecializationID] INT IDENTITY(1,1) PRIMARY KEY,
        [SpecialityID] INT NULL,
        [SpecializationName] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(500) NULL,
        [TenantID] INT NOT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        CONSTRAINT [FK_Specialization_Speciality] FOREIGN KEY ([SpecialityID]) 
            REFERENCES [dbo].[Speciality]([SpecialityID]),
        INDEX [IX_Specialization_SpecialityID] ([SpecialityID]),
        INDEX [IX_Specialization_TenantID] ([TenantID]),
        INDEX [IX_Specialization_IsDeleted] ([IsDeleted])
    );
    PRINT 'Table Specialization created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Specialization already exists.';
END
GO

PRINT 'Specialization module table creation script completed.';
GO

