-- =============================================
-- Update Stored Procedure: Doctor_GetById
-- Description: Updates Doctor_GetById to return all profile fields including User data
-- Date: 2025-02
-- =============================================

USE [SoowGood_System]  -- Change to your database name
GO

-- Drop existing procedure if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetById]
GO

-- Create updated procedure
CREATE PROCEDURE [dbo].[Doctor_GetById]
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DoctorID,
        UserID,
        SpecialityID,
        ISNULL(Specialization, '') AS Specialization,
        ISNULL(LicenseNumber, '') AS LicenseNumber,
        ISNULL(HospitalAffiliation, '') AS HospitalAffiliation,
        CreatedAt,
        UpdatedAt,
        IsDeleted,
        DoctorReferenceID,
        ISNULL(Expertise, '') AS Expertise,
        ProfileStep,
        -- New fields from Doctors table
        BmdcRegNo,
        BmdcRegExpiryDate,
        IdentityNumber,
        City,
        Country,
        Address,
        DoctorTitle
    FROM Doctors
    WHERE DoctorID = @DoctorId 
        AND (IsDeleted = 0 OR IsDeleted IS NULL);
END;
GO

PRINT 'Stored procedure Doctor_GetById updated successfully.';
GO

