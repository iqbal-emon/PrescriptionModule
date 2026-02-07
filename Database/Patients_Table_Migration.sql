-- =============================================
-- Patients Table Migration - Add PatientProfile Fields
-- =============================================
-- This script adds fields from PatientProfile to Patients table
-- Execute this script to update the database schema

USE [YourDatabaseName] -- Replace with your actual database name
GO

-- Add new columns to Patients table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'FullName')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [FullName] NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'IsSelf')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [IsSelf] BIT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'PatientName')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [PatientName] NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'Age')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [Age] INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'City')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [City] NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'ZipCode')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [ZipCode] NVARCHAR(20) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'Country')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [Country] NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'MobileNo')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [MobileNo] NVARCHAR(20) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'PatientMobileNo')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [PatientMobileNo] NVARCHAR(20) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'Email')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [Email] NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'PatientEmail')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [PatientEmail] NVARCHAR(255) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [CreatedBy] NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'CreatorCode')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [CreatorCode] NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'CreatorRole')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [CreatorRole] NVARCHAR(50) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'CreatorEntityId')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [CreatorEntityId] INT NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND name = 'IsFirstTime')
BEGIN
    ALTER TABLE [dbo].[Patients] ADD [IsFirstTime] BIT NULL;
END
GO

PRINT 'Patients table migration completed successfully.';
GO

