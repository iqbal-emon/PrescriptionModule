USE [SoowGood_System_Dev_2]
GO
/****** Object:  StoredProcedure [dbo].[Doctor_Insert]    Script Date: 2/13/2026 5:56:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Doctor_Insert]
    @UserID INT = NULL,
    @SpecialityID INT = NULL,
    @Specialization NVARCHAR(100) = NULL,
    @LicenseNumber NVARCHAR(50) = NULL,
    @DoctorReferenceID INT = NULL,
    @HospitalAffiliation NVARCHAR(100) = NULL,
    @Expertise NVARCHAR(500) = NULL,
    @ProfileStep INT = NULL,
    @BmdcRegNo NVARCHAR(50) = NULL,
    @BmdcRegExpiryDate DATETIME = NULL,
    @IdentityNumber NVARCHAR(50) = NULL,
    @City NVARCHAR(100) = NULL,
    @Country NVARCHAR(100) = NULL,
    @Address NVARCHAR(255) = NULL,
    @DoctorTitle INT = NULL,
    @CreatedAt DATETIME = NULL,
    @UpdatedAt DATETIME = NULL,
    @IsDeleted BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Doctors(
        UserID,
        SpecialityID,
        DoctorReferenceID,
        Specialization,
        LicenseNumber,
        HospitalAffiliation,
        Expertise,
        ProfileStep,
        BmdcRegNo,
        BmdcRegExpiryDate,
        IdentityNumber,
        City,
        Country,
        Address,
        DoctorTitle,
        CreatedAt,
        UpdatedAt,
        IsDeleted
    )
    VALUES (
        @UserID,
        @SpecialityID,
        @DoctorReferenceID,
        ISNULL(@Specialization, ''),
        ISNULL(@LicenseNumber, ''),
        ISNULL(@HospitalAffiliation, ''),
        @Expertise,
        ISNULL(@ProfileStep, 0),
        @BmdcRegNo,
        @BmdcRegExpiryDate,
        @IdentityNumber,
        @City,
        @Country,
        @Address,
        @DoctorTitle,
        ISNULL(@CreatedAt, GETUTCDATE()),
        ISNULL(@UpdatedAt, GETUTCDATE()),
        ISNULL(@IsDeleted, 0)
    );

    -- Return the newly inserted ID
    SELECT SCOPE_IDENTITY() AS DoctorID;
END;
GO

