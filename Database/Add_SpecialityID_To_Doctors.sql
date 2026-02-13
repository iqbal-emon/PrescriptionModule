-- =============================================
-- Migration Script: Add SpecialityID to Doctors Table
-- Description: Adds SpecialityID column and foreign key constraint
-- Date: 2025-02
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Add SpecialityID column to Doctors table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND name = 'SpecialityID')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD [SpecialityID] INT NULL;
    PRINT 'Column SpecialityID added to Doctors table successfully.';
END
ELSE
BEGIN
    PRINT 'Column SpecialityID already exists in Doctors table.';
END
GO

-- =============================================
-- Add Foreign Key Constraint for SpecialityID
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Doctors_Speciality')
BEGIN
    ALTER TABLE [dbo].[Doctors]
    ADD CONSTRAINT [FK_Doctors_Speciality] FOREIGN KEY([SpecialityID])
    REFERENCES [dbo].[Speciality] ([SpecialityID]);
    PRINT 'Foreign key constraint FK_Doctors_Speciality added successfully.';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_Doctors_Speciality already exists.';
END
GO

PRINT 'Migration completed successfully.';
GO

