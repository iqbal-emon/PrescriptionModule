-- =============================================
-- Doctor Module - ALTER TABLE Script
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Add new columns to Doctor table
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Add Expertise column to Doctor table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctor]') AND name = 'Expertise')
BEGIN
    ALTER TABLE [dbo].[Doctor]
    ADD [Expertise] NVARCHAR(500) NULL;
    PRINT 'Column Expertise added to Doctor table successfully.';
END
ELSE
BEGIN
    PRINT 'Column Expertise already exists in Doctor table.';
END
GO

-- =============================================
-- Add ProfileStep column to Doctor table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctor]') AND name = 'ProfileStep')
BEGIN
    ALTER TABLE [dbo].[Doctor]
    ADD [ProfileStep] INT NULL;
    PRINT 'Column ProfileStep added to Doctor table successfully.';
END
ELSE
BEGIN
    PRINT 'Column ProfileStep already exists in Doctor table.';
END
GO

PRINT 'Doctor table ALTER script completed.';
GO

