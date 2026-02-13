-- =============================================
-- Update Stored Procedure: Doctor_Update
-- Description: Updates Doctor_Update to handle all profile fields and User table updates
-- Date: 2025-02
-- =============================================

USE [SoowGood_System]  -- Change to your database name
GO

-- Drop existing procedure if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_Update]
GO

-- Create updated procedure
CREATE PROCEDURE [dbo].[Doctor_Update] 
    @DoctorID INT,
    @UserID INT = NULL,
    @SpecialityID INT = NULL,
    @Specialization NVARCHAR(100) = NULL,
    @LicenseNumber NVARCHAR(50) = NULL,
    @DoctorReferenceID INT = NULL,
    @HospitalAffiliation NVARCHAR(100) = NULL,
    @Expertise NVARCHAR(500) = NULL,
    @ProfileStep INT = NULL,
    
    -- User table fields
    @FullName NVARCHAR(150) = NULL,
    @Email NVARCHAR(150) = NULL,
    @MobileNo NVARCHAR(15) = NULL,
    @PhoneNumber NVARCHAR(15) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    
    -- Doctor profile fields
    @BmdcRegNo NVARCHAR(50) = NULL,
    @BmdcRegExpiryDate DATETIME = NULL,
    @IdentityNumber NVARCHAR(50) = NULL,
    @City NVARCHAR(100) = NULL,
    @Country NVARCHAR(100) = NULL,
    @Address NVARCHAR(255) = NULL,
    @DoctorTitle INT = NULL,
    
    @UpdatedAt DATETIME = NULL,
    @IsDeleted BIT = NULL,
    @CreatedAt DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Update Users table if UserID is provided and user fields are provided
        IF @UserID IS NOT NULL AND @UserID > 0
        BEGIN
            UPDATE [dbo].[Users]
            SET 
                [FullName] = ISNULL(@FullName, [FullName]),
                [Email] = ISNULL(@Email, [Email]),
                [PhoneNumber] = ISNULL(@MobileNo, ISNULL(@PhoneNumber, [PhoneNumber])),
                [ContactNo] = ISNULL(@ContactNo, [ContactNo]),
                [UpdatedAt] = GETUTCDATE()
            WHERE [UserID] = @UserID;
        END
        
        -- Update Doctors table
        UPDATE [dbo].[Doctors]
        SET 
            UserID = CASE WHEN @UserID > 0 THEN @UserID ELSE UserID END,
            SpecialityID = CASE WHEN @SpecialityID > 0 THEN @SpecialityID ELSE SpecialityID END,
            DoctorReferenceID = CASE WHEN @DoctorReferenceID > 0 THEN @DoctorReferenceID ELSE DoctorReferenceID END,
            Specialization = ISNULL(@Specialization, Specialization),
            LicenseNumber = ISNULL(@LicenseNumber, LicenseNumber),
            HospitalAffiliation = ISNULL(@HospitalAffiliation, HospitalAffiliation),
            Expertise = ISNULL(@Expertise, Expertise),
            ProfileStep = ISNULL(@ProfileStep, ProfileStep),
            BmdcRegNo = ISNULL(@BmdcRegNo, BmdcRegNo),
            BmdcRegExpiryDate = ISNULL(@BmdcRegExpiryDate, BmdcRegExpiryDate),
            IdentityNumber = ISNULL(@IdentityNumber, IdentityNumber),
            City = ISNULL(@City, City),
            Country = ISNULL(@Country, Country),
            Address = ISNULL(@Address, Address),
            DoctorTitle = ISNULL(@DoctorTitle, DoctorTitle),
            UpdatedAt = GETUTCDATE()
        WHERE 
            DoctorID = @DoctorID 
            AND (IsDeleted = 0 OR IsDeleted IS NULL);
        
        -- Check if the update was successful
        IF @@ROWCOUNT > 0
        BEGIN
            COMMIT TRANSACTION;
            SELECT @DoctorID AS UpdatedDoctorID;
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT NULL AS UpdatedDoctorID;  -- Return NULL if no update happened
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

PRINT 'Stored procedure Doctor_Update updated successfully.';
GO

