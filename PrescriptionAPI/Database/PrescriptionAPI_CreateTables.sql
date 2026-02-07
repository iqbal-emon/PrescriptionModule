-- =============================================
-- Prescription API Module Database Tables
-- Database: Prescripto (or your database name)
-- Created: 2025-02-07
-- Description: Create tables for Prescription API module entities
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- 1. CommonAdvices Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonAdvices]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CommonAdvices]
    (
        [CommonAdviceID] INT IDENTITY(1,1) PRIMARY KEY,
        [Advice] NVARCHAR(MAX) NOT NULL,
        [Type] NVARCHAR(50) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        [IsActive] BIT NOT NULL DEFAULT 1,
        
        INDEX [IX_CommonAdvices_IsDeleted] ([IsDeleted]),
        INDEX [IX_CommonAdvices_IsActive] ([IsActive])
    );
    PRINT 'Table CommonAdvices created successfully.';
END
ELSE
BEGIN
    PRINT 'Table CommonAdvices already exists.';
END
GO

-- =============================================
-- 2. CommonHistory Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CommonHistory]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CommonHistory]
    (
        [CommonHistoryId] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(500) NOT NULL,
        [Description] NVARCHAR(MAX) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        [IsActive] BIT NOT NULL DEFAULT 1,
        
        INDEX [IX_CommonHistory_IsDeleted] ([IsDeleted]),
        INDEX [IX_CommonHistory_IsActive] ([IsActive])
    );
    PRINT 'Table CommonHistory created successfully.';
END
ELSE
BEGIN
    PRINT 'Table CommonHistory already exists.';
END
GO

-- =============================================
-- 3. Diagonosis Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagonosis]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Diagonosis]
    (
        [DiagonosisID] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(500) NOT NULL,
        [Description] NVARCHAR(MAX) NULL,
        [Code] NVARCHAR(50) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        [IsActive] BIT NOT NULL DEFAULT 1,
        
        INDEX [IX_Diagonosis_IsDeleted] ([IsDeleted]),
        INDEX [IX_Diagonosis_IsActive] ([IsActive]),
        INDEX [IX_Diagonosis_Code] ([Code])
    );
    PRINT 'Table Diagonosis created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Diagonosis already exists.';
END
GO

-- =============================================
-- 4. Investigation Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Investigation]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Investigation]
    (
        [InvestigationID] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(500) NOT NULL,
        [Description] NVARCHAR(MAX) NULL,
        [Code] NVARCHAR(50) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        [IsActive] BIT NOT NULL DEFAULT 1,
        
        INDEX [IX_Investigation_IsDeleted] ([IsDeleted]),
        INDEX [IX_Investigation_IsActive] ([IsActive]),
        INDEX [IX_Investigation_Code] ([Code])
    );
    PRINT 'Table Investigation created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Investigation already exists.';
END
GO

-- =============================================
-- END OF TABLE CREATION
-- =============================================
-- Total Tables Created: 4
-- 1. CommonAdvices
-- 2. CommonHistory
-- 3. Diagonosis
-- 4. Investigation
-- =============================================

PRINT 'All Prescription API module tables creation script completed.';
GO

