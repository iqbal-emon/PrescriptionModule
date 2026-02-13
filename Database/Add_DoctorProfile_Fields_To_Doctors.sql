-- =============================================
-- Migration Script: Add Doctor Profile Fields to Doctors Table
-- Description: Adds BMDC, IdentityNumber, City, Country, Address columns
-- Date: 2025-02
-- =============================================

USE [SoowGood_System]  -- Change to your database name
GO

-- =============================================
-- Add BmdcRegNo column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'BmdcRegNo')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [BmdcRegNo] NVARCHAR(50) NULL;
    PRINT 'Column BmdcRegNo added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column BmdcRegNo already exists in Doctors table.';
END
GO

-- =============================================
-- Add BmdcRegExpiryDate column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'BmdcRegExpiryDate')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [BmdcRegExpiryDate] DATETIME NULL;
    PRINT 'Column BmdcRegExpiryDate added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column BmdcRegExpiryDate already exists in Doctors table.';
END
GO

-- =============================================
-- Add IdentityNumber column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'IdentityNumber')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [IdentityNumber] NVARCHAR(50) NULL;
    PRINT 'Column IdentityNumber added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column IdentityNumber already exists in Doctors table.';
END
GO

-- =============================================
-- Add City column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'City')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [City] NVARCHAR(100) NULL;
    PRINT 'Column City added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column City already exists in Doctors table.';
END
GO

-- =============================================
-- Add Country column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'Country')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [Country] NVARCHAR(100) NULL;
    PRINT 'Column Country added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column Country already exists in Doctors table.';
END
GO

-- =============================================
-- Add Address column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'Address')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [Address] NVARCHAR(255) NULL;
    PRINT 'Column Address added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column Address already exists in Doctors table.';
END
GO

-- =============================================
-- Add DoctorTitle column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'DoctorTitle')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [DoctorTitle] INT NULL;
    PRINT 'Column DoctorTitle added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column DoctorTitle already exists in Doctors table.';
END
GO

PRINT 'Migration completed successfully.';
GO

