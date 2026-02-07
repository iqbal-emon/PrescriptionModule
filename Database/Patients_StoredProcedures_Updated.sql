-- =============================================
-- Updated Patients Stored Procedures
-- Includes all merged fields from PatientProfile
-- =============================================
-- Execute this after running Patients_Table_Migration.sql

USE [YourDatabaseName] -- Replace with your actual database name
GO

-- =============================================
-- Patients_Insert - Updated with new fields
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_Insert]
GO

CREATE PROCEDURE [dbo].[Patients_Insert]
    @PatientID INT,
    @UserID INT,
	@PatientReferenceID INT = NULL,
    @DateOfBirth DATETIME = NULL,
    @Gender NVARCHAR(10) = NULL,
    @Address NVARCHAR(255) = NULL,
    @BloodGroup NVARCHAR(5) = NULL,
    @InsuranceProvider NVARCHAR(100) = NULL,
    @InsurancePolicyNumber NVARCHAR(50) = NULL,
    @CreatedAt DATETIME = NULL,
    @UpdatedAt DATETIME = NULL,
    @IsDeleted BIT = NULL,
	@PatientAge NVARCHAR(10) = NULL,
	@PatientCode NVARCHAR(100) = NULL,
    @FullName NVARCHAR(255) = NULL,
    @IsSelf BIT = NULL,
    @PatientName NVARCHAR(255) = NULL,
    @Age INT = NULL,
    @City NVARCHAR(100) = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @Country NVARCHAR(100) = NULL,
    @MobileNo NVARCHAR(20) = NULL,
    @PatientMobileNo NVARCHAR(20) = NULL,
    @Email NVARCHAR(255) = NULL,
    @PatientEmail NVARCHAR(255) = NULL,
    @CreatedBy NVARCHAR(100) = NULL,
    @CreatorCode NVARCHAR(100) = NULL,
    @CreatorRole NVARCHAR(50) = NULL,
    @CreatorEntityId INT = NULL,
    @IsFirstTime BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Patients (
        UserID,
		PatientReferenceID,
        DateOfBirth,
        Gender,
        Address,
        BloodGroup,
        InsuranceProvider,
        InsurancePolicyNumber,
        CreatedAt,
        UpdatedAt,
        IsDeleted,
		PatientAge,
		PatientCode,
        FullName,
        IsSelf,
        PatientName,
        Age,
        City,
        ZipCode,
        Country,
        MobileNo,
        PatientMobileNo,
        Email,
        PatientEmail,
        CreatedBy,
        CreatorCode,
        CreatorRole,
        CreatorEntityId,
        IsFirstTime
    )
    VALUES (
        @UserID,
		@PatientReferenceID,
        @DateOfBirth,
        ISNULL(@Gender, ''),
        ISNULL(@Address, ''),
        ISNULL(@BloodGroup, ''),
        ISNULL(@InsuranceProvider, ''),
        ISNULL(@InsurancePolicyNumber, ''),
        GETUTCDATE(),
        GETUTCDATE(),
        0,
		ISNULL(@PatientAge, ''),
		ISNULL(@PatientCode, ''),
        ISNULL(@FullName, ''),
        @IsSelf,
        ISNULL(@PatientName, ''),
        @Age,
        ISNULL(@City, ''),
        ISNULL(@ZipCode, ''),
        ISNULL(@Country, ''),
        ISNULL(@MobileNo, ''),
        ISNULL(@PatientMobileNo, ''),
        ISNULL(@Email, ''),
        ISNULL(@PatientEmail, ''),
        ISNULL(@CreatedBy, ''),
        ISNULL(@CreatorCode, ''),
        ISNULL(@CreatorRole, ''),
        @CreatorEntityId,
        @IsFirstTime
    );

    -- Return the newly inserted ID
    SELECT SCOPE_IDENTITY() AS PatientID;
END;
GO

-- =============================================
-- Patients_Update - Updated with new fields
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_Update]
GO

CREATE PROCEDURE [dbo].[Patients_Update] 
    @PatientID INT,
	@PatientReferenceID INT = NULL,
    @UserID INT = NULL,
    @DateOfBirth DATETIME = NULL,
    @Gender NVARCHAR(10) = NULL,
    @Address NVARCHAR(255) = NULL,
    @BloodGroup NVARCHAR(5) = NULL,
    @InsuranceProvider NVARCHAR(100) = NULL,
    @InsurancePolicyNumber NVARCHAR(50) = NULL,
    @UpdatedAt DATETIME = NULL,
    @IsDeleted BIT = NULL,
    @CreatedAt DATETIME = NULL,
    @PatientAge NVARCHAR(10) = NULL,
    @PatientCode NVARCHAR(100) = NULL,
    @FullName NVARCHAR(255) = NULL,
    @IsSelf BIT = NULL,
    @PatientName NVARCHAR(255) = NULL,
    @Age INT = NULL,
    @City NVARCHAR(100) = NULL,
    @ZipCode NVARCHAR(20) = NULL,
    @Country NVARCHAR(100) = NULL,
    @MobileNo NVARCHAR(20) = NULL,
    @PatientMobileNo NVARCHAR(20) = NULL,
    @Email NVARCHAR(255) = NULL,
    @PatientEmail NVARCHAR(255) = NULL,
    @CreatedBy NVARCHAR(100) = NULL,
    @CreatorCode NVARCHAR(100) = NULL,
    @CreatorRole NVARCHAR(50) = NULL,
    @CreatorEntityId INT = NULL,
    @IsFirstTime BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Update only if PatientID exists and is not deleted
    UPDATE Patients
    SET 
        UserID = CASE WHEN @UserID > 0 THEN @UserID ELSE UserID END,
		PatientReferenceID = CASE WHEN @PatientReferenceID > 0 THEN @PatientReferenceID ELSE PatientReferenceID END,
        DateOfBirth = ISNULL(@DateOfBirth, DateOfBirth),
        Gender = ISNULL(@Gender, Gender),
        Address = ISNULL(@Address, Address),
        BloodGroup = ISNULL(@BloodGroup, BloodGroup),
        InsuranceProvider = ISNULL(@InsuranceProvider, InsuranceProvider),
        InsurancePolicyNumber = ISNULL(@InsurancePolicyNumber, InsurancePolicyNumber),
        UpdatedAt = GETUTCDATE(),
		PatientAge = ISNULL(@PatientAge, PatientAge),
        PatientCode = ISNULL(@PatientCode, PatientCode),
        FullName = ISNULL(@FullName, FullName),
        IsSelf = ISNULL(@IsSelf, IsSelf),
        PatientName = ISNULL(@PatientName, PatientName),
        Age = ISNULL(@Age, Age),
        City = ISNULL(@City, City),
        ZipCode = ISNULL(@ZipCode, ZipCode),
        Country = ISNULL(@Country, Country),
        MobileNo = ISNULL(@MobileNo, MobileNo),
        PatientMobileNo = ISNULL(@PatientMobileNo, PatientMobileNo),
        Email = ISNULL(@Email, Email),
        PatientEmail = ISNULL(@PatientEmail, PatientEmail),
        CreatedBy = ISNULL(@CreatedBy, CreatedBy),
        CreatorCode = ISNULL(@CreatorCode, CreatorCode),
        CreatorRole = ISNULL(@CreatorRole, CreatorRole),
        CreatorEntityId = ISNULL(@CreatorEntityId, CreatorEntityId),
        IsFirstTime = ISNULL(@IsFirstTime, IsFirstTime)
    WHERE 
        PatientID = @PatientID 
        AND IsDeleted = 0;

    -- Check if the update was successful
    IF @@ROWCOUNT > 0
    BEGIN
        SELECT @PatientID AS UpdatedPatientID;
    END
    ELSE
    BEGIN
        SELECT NULL AS UpdatedPatientID;  -- Return NULL if no update happened
    END
END;
GO

PRINT 'Patients stored procedures updated successfully.';
GO

