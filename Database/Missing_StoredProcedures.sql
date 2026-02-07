-- =============================================
-- Missing Stored Procedures for Patients Module
-- =============================================

-- Patients_GetByPhoneAndCode
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetByPhoneAndCode]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetByPhoneAndCode]
GO

CREATE PROCEDURE [dbo].[Patients_GetByPhoneAndCode]
    @PatientCode NVARCHAR(100),
    @PhoneNo NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        p.PatientID,
        p.UserID,
        p.PatientReferenceID,
        p.DateOfBirth,
        ISNULL(p.Gender, '') AS Gender,
        ISNULL(p.Address, '') AS Address,
        ISNULL(p.BloodGroup, '') AS BloodGroup,
        ISNULL(p.InsuranceProvider, '') AS InsuranceProvider,
        ISNULL(p.InsurancePolicyNumber, '') AS InsurancePolicyNumber,
        CONVERT(NVARCHAR(10), ISNULL(p.PatientAge, 0)) AS PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        ISNULL(p.PatientCode, '') AS PatientCode,
        ISNULL(p.FullName, '') AS FullName,
        ISNULL(p.IsSelf, 0) AS IsSelf,
        ISNULL(p.PatientName, '') AS PatientName,
        ISNULL(p.Age, 0) AS Age,
        ISNULL(p.City, '') AS City,
        ISNULL(p.ZipCode, '') AS ZipCode,
        ISNULL(p.Country, '') AS Country,
        ISNULL(p.MobileNo, '') AS MobileNo,
        ISNULL(p.PatientMobileNo, '') AS PatientMobileNo,
        ISNULL(p.Email, '') AS Email,
        ISNULL(p.PatientEmail, '') AS PatientEmail,
        ISNULL(p.CreatedBy, '') AS CreatedBy,
        ISNULL(p.CreatorCode, '') AS CreatorCode,
        ISNULL(p.CreatorRole, '') AS CreatorRole,
        ISNULL(p.CreatorEntityId, 0) AS CreatorEntityId,
        ISNULL(p.IsFirstTime, 0) AS IsFirstTime,
        u.PhoneNumber
    FROM Patients p
    LEFT JOIN Users u ON u.UserID = p.UserID
    WHERE (p.PatientCode = @PatientCode OR u.PhoneNumber = @PhoneNo)
        AND p.IsDeleted = 0;
END;
GO

-- Patients_GetAllSimple
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetAllSimple]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetAllSimple]
GO

CREATE PROCEDURE [dbo].[Patients_GetAllSimple]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.PatientID,
        p.UserID,
        p.PatientReferenceID,
        p.DateOfBirth,
        ISNULL(p.Gender, '') AS Gender,
        ISNULL(p.Address, '') AS Address,
        ISNULL(p.BloodGroup, '') AS BloodGroup,
        ISNULL(p.InsuranceProvider, '') AS InsuranceProvider,
        ISNULL(p.InsurancePolicyNumber, '') AS InsurancePolicyNumber,
        CONVERT(NVARCHAR(10), ISNULL(p.PatientAge, 0)) AS PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        ISNULL(p.PatientCode, '') AS PatientCode,
        ISNULL(p.FullName, '') AS FullName,
        ISNULL(p.IsSelf, 0) AS IsSelf,
        ISNULL(p.PatientName, '') AS PatientName,
        ISNULL(p.Age, 0) AS Age,
        ISNULL(p.City, '') AS City,
        ISNULL(p.ZipCode, '') AS ZipCode,
        ISNULL(p.Country, '') AS Country,
        ISNULL(p.MobileNo, '') AS MobileNo,
        ISNULL(p.PatientMobileNo, '') AS PatientMobileNo,
        ISNULL(p.Email, '') AS Email,
        ISNULL(p.PatientEmail, '') AS PatientEmail,
        ISNULL(p.CreatedBy, '') AS CreatedBy,
        ISNULL(p.CreatorCode, '') AS CreatorCode,
        ISNULL(p.CreatorRole, '') AS CreatorRole,
        ISNULL(p.CreatorEntityId, 0) AS CreatorEntityId,
        ISNULL(p.IsFirstTime, 0) AS IsFirstTime,
        u.PhoneNumber,
        u.FirstName,
        u.LastName,
        u.Email AS UserEmail
    FROM Patients p
    LEFT JOIN Users u ON u.UserID = p.UserID
    WHERE p.IsDeleted = 0
    ORDER BY p.CreatedAt DESC;
END;
GO

-- Patients_GetByUserProfileId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetByUserProfileId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetByUserProfileId]
GO

CREATE PROCEDURE [dbo].[Patients_GetByUserProfileId]
    @ProfileId INT,
    @Role NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- This procedure retrieves patients based on profile ID and role
    -- Adjust the query based on your actual schema relationships
    SELECT 
        p.PatientID,
        p.UserID,
        p.PatientReferenceID,
        p.DateOfBirth,
        ISNULL(p.Gender, '') AS Gender,
        ISNULL(p.Address, '') AS Address,
        ISNULL(p.BloodGroup, '') AS BloodGroup,
        ISNULL(p.InsuranceProvider, '') AS InsuranceProvider,
        ISNULL(p.InsurancePolicyNumber, '') AS InsurancePolicyNumber,
        CONVERT(NVARCHAR(10), ISNULL(p.PatientAge, 0)) AS PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        ISNULL(p.PatientCode, '') AS PatientCode,
        ISNULL(p.FullName, '') AS FullName,
        ISNULL(p.IsSelf, 0) AS IsSelf,
        ISNULL(p.PatientName, '') AS PatientName,
        ISNULL(p.Age, 0) AS Age,
        ISNULL(p.City, '') AS City,
        ISNULL(p.ZipCode, '') AS ZipCode,
        ISNULL(p.Country, '') AS Country,
        ISNULL(p.MobileNo, '') AS MobileNo,
        ISNULL(p.PatientMobileNo, '') AS PatientMobileNo,
        ISNULL(p.Email, '') AS Email,
        ISNULL(p.PatientEmail, '') AS PatientEmail,
        ISNULL(p.CreatedBy, '') AS CreatedBy,
        ISNULL(p.CreatorCode, '') AS CreatorCode,
        ISNULL(p.CreatorRole, '') AS CreatorRole,
        ISNULL(p.CreatorEntityId, 0) AS CreatorEntityId,
        ISNULL(p.IsFirstTime, 0) AS IsFirstTime,
        u.PhoneNumber,
        u.FirstName,
        u.LastName,
        u.Email AS UserEmail
    FROM Patients p
    LEFT JOIN Users u ON u.UserID = p.UserID
    WHERE p.CreatorEntityId = @ProfileId
        AND ISNULL(p.CreatorRole, '') = @Role
        AND p.IsDeleted = 0
    ORDER BY p.CreatedAt DESC;
END;
GO

-- Patients_GetBySearchUserProfileId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetBySearchUserProfileId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetBySearchUserProfileId]
GO

CREATE PROCEDURE [dbo].[Patients_GetBySearchUserProfileId]
    @ProfileId INT,
    @Role NVARCHAR(50),
    @Name NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.PatientID,
        p.UserID,
        p.PatientReferenceID,
        p.DateOfBirth,
        ISNULL(p.Gender, '') AS Gender,
        ISNULL(p.Address, '') AS Address,
        ISNULL(p.BloodGroup, '') AS BloodGroup,
        ISNULL(p.InsuranceProvider, '') AS InsuranceProvider,
        ISNULL(p.InsurancePolicyNumber, '') AS InsurancePolicyNumber,
        CONVERT(NVARCHAR(10), ISNULL(p.PatientAge, 0)) AS PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        ISNULL(p.PatientCode, '') AS PatientCode,
        ISNULL(p.FullName, '') AS FullName,
        ISNULL(p.IsSelf, 0) AS IsSelf,
        ISNULL(p.PatientName, '') AS PatientName,
        ISNULL(p.Age, 0) AS Age,
        ISNULL(p.City, '') AS City,
        ISNULL(p.ZipCode, '') AS ZipCode,
        ISNULL(p.Country, '') AS Country,
        ISNULL(p.MobileNo, '') AS MobileNo,
        ISNULL(p.PatientMobileNo, '') AS PatientMobileNo,
        ISNULL(p.Email, '') AS Email,
        ISNULL(p.PatientEmail, '') AS PatientEmail,
        ISNULL(p.CreatedBy, '') AS CreatedBy,
        ISNULL(p.CreatorCode, '') AS CreatorCode,
        ISNULL(p.CreatorRole, '') AS CreatorRole,
        ISNULL(p.CreatorEntityId, 0) AS CreatorEntityId,
        ISNULL(p.IsFirstTime, 0) AS IsFirstTime,
        u.PhoneNumber,
        u.FirstName,
        u.LastName,
        u.Email AS UserEmail
    FROM Patients p
    LEFT JOIN Users u ON u.UserID = p.UserID
    WHERE p.CreatorEntityId = @ProfileId
        AND ISNULL(p.CreatorRole, '') = @Role
        AND (
            @Name = '' OR 
            ISNULL(p.FullName, '') LIKE '%' + @Name + '%' OR
            ISNULL(p.PatientName, '') LIKE '%' + @Name + '%' OR
            ISNULL(u.FirstName, '') + ' ' + ISNULL(u.LastName, '') LIKE '%' + @Name + '%'
        )
        AND p.IsDeleted = 0
    ORDER BY p.CreatedAt DESC;
END;
GO

-- Patients_GetFiltered
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetFiltered]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetFiltered]
GO

CREATE PROCEDURE [dbo].[Patients_GetFiltered]
    @SearchTerm NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.PatientID,
        p.UserID,
        p.PatientReferenceID,
        p.DateOfBirth,
        ISNULL(p.Gender, '') AS Gender,
        ISNULL(p.Address, '') AS Address,
        ISNULL(p.BloodGroup, '') AS BloodGroup,
        ISNULL(p.InsuranceProvider, '') AS InsuranceProvider,
        ISNULL(p.InsurancePolicyNumber, '') AS InsurancePolicyNumber,
        CONVERT(NVARCHAR(10), ISNULL(p.PatientAge, 0)) AS PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        ISNULL(p.PatientCode, '') AS PatientCode,
        ISNULL(p.FullName, '') AS FullName,
        ISNULL(p.IsSelf, 0) AS IsSelf,
        ISNULL(p.PatientName, '') AS PatientName,
        ISNULL(p.Age, 0) AS Age,
        ISNULL(p.City, '') AS City,
        ISNULL(p.ZipCode, '') AS ZipCode,
        ISNULL(p.Country, '') AS Country,
        ISNULL(p.MobileNo, '') AS MobileNo,
        ISNULL(p.PatientMobileNo, '') AS PatientMobileNo,
        ISNULL(p.Email, '') AS Email,
        ISNULL(p.PatientEmail, '') AS PatientEmail,
        ISNULL(p.CreatedBy, '') AS CreatedBy,
        ISNULL(p.CreatorCode, '') AS CreatorCode,
        ISNULL(p.CreatorRole, '') AS CreatorRole,
        ISNULL(p.CreatorEntityId, 0) AS CreatorEntityId,
        ISNULL(p.IsFirstTime, 0) AS IsFirstTime,
        u.PhoneNumber,
        u.FirstName,
        u.LastName,
        u.Email AS UserEmail
    FROM Patients p
    LEFT JOIN Users u ON u.UserID = p.UserID
    WHERE p.IsDeleted = 0
        AND (
            @SearchTerm = '' OR
            ISNULL(p.PatientCode, '') LIKE '%' + @SearchTerm + '%' OR
            ISNULL(p.FullName, '') LIKE '%' + @SearchTerm + '%' OR
            ISNULL(p.PatientName, '') LIKE '%' + @SearchTerm + '%' OR
            ISNULL(u.PhoneNumber, '') LIKE '%' + @SearchTerm + '%' OR
            ISNULL(u.FirstName, '') + ' ' + ISNULL(u.LastName, '') LIKE '%' + @SearchTerm + '%' OR
            ISNULL(u.Email, '') LIKE '%' + @SearchTerm + '%'
        )
    ORDER BY p.CreatedAt DESC;
END;
GO

-- =============================================
-- Missing Stored Procedures for Doctor Module
-- =============================================

-- Doctor_GetByUserName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetByUserName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetByUserName]
GO

CREATE PROCEDURE [dbo].[Doctor_GetByUserName]
    @UserName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        d.DoctorID,
        d.UserID,
        d.DoctorReferenceID,
        ISNULL(d.Specialization, '') AS Specialization,
        ISNULL(d.LicenseNumber, '') AS LicenseNumber,
        ISNULL(d.HospitalAffiliation, '') AS HospitalAffiliation,
        ISNULL(d.Expertise, '') AS Expertise,
        ISNULL(d.ProfileStep, 0) AS ProfileStep,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        u.UserName,
        u.FirstName,
        u.LastName,
        u.Email,
        u.PhoneNumber,
        u.IsActive
    FROM Doctors d
    INNER JOIN Users u ON u.UserID = d.UserID
    WHERE u.UserName = @UserName
        AND d.IsDeleted = 0;
END;
GO

-- Doctor_GetByEmail
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetByEmail]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetByEmail]
GO

CREATE PROCEDURE [dbo].[Doctor_GetByEmail]
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        d.DoctorID,
        d.UserID,
        d.DoctorReferenceID,
        ISNULL(d.Specialization, '') AS Specialization,
        ISNULL(d.LicenseNumber, '') AS LicenseNumber,
        ISNULL(d.HospitalAffiliation, '') AS HospitalAffiliation,
        ISNULL(d.Expertise, '') AS Expertise,
        ISNULL(d.ProfileStep, 0) AS ProfileStep,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        u.UserName,
        u.FirstName,
        u.LastName,
        u.Email,
        u.PhoneNumber,
        u.IsActive
    FROM Doctors d
    INNER JOIN Users u ON u.UserID = d.UserID
    WHERE u.Email = @Email
        AND d.IsDeleted = 0;
END;
GO

-- Doctor_GetByOnlineStatus
-- Note: This assumes IsOnline is stored in Users table or a related table
-- If IsOnline is in a different table, adjust the query accordingly
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetByOnlineStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetByOnlineStatus]
GO

CREATE PROCEDURE [dbo].[Doctor_GetByOnlineStatus]
    @IsOnline BIT
AS
BEGIN
    SET NOCOUNT ON;

    -- If IsOnline is in Users table, use this:
    SELECT 
        d.DoctorID,
        d.UserID,
        d.DoctorReferenceID,
        ISNULL(d.Specialization, '') AS Specialization,
        ISNULL(d.LicenseNumber, '') AS LicenseNumber,
        ISNULL(d.HospitalAffiliation, '') AS HospitalAffiliation,
        ISNULL(d.Expertise, '') AS Expertise,
        ISNULL(d.ProfileStep, 0) AS ProfileStep,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        u.UserName,
        u.FirstName,
        u.LastName,
        u.Email,
        u.PhoneNumber,
        u.IsActive
    FROM Doctors d
    INNER JOIN Users u ON u.UserID = d.UserID
    WHERE u.IsActive = @IsOnline  -- Adjust this based on where IsOnline is stored
        AND d.IsDeleted = 0;
    
    -- If IsOnline is a separate field in Doctors table, you would need to:
    -- 1. Add IsOnline column to Doctors table
    -- 2. Change the WHERE clause to: WHERE d.IsOnline = @IsOnline AND d.IsDeleted = 0
END;
GO

-- Doctor_GetByActiveStatus
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetByActiveStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetByActiveStatus]
GO

CREATE PROCEDURE [dbo].[Doctor_GetByActiveStatus]
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.DoctorID,
        d.UserID,
        d.DoctorReferenceID,
        ISNULL(d.Specialization, '') AS Specialization,
        ISNULL(d.LicenseNumber, '') AS LicenseNumber,
        ISNULL(d.HospitalAffiliation, '') AS HospitalAffiliation,
        ISNULL(d.Expertise, '') AS Expertise,
        ISNULL(d.ProfileStep, 0) AS ProfileStep,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        u.UserName,
        u.FirstName,
        u.LastName,
        u.Email,
        u.PhoneNumber,
        u.IsActive
    FROM Doctors d
    INNER JOIN Users u ON u.UserID = d.UserID
    WHERE u.IsActive = @IsActive
        AND d.IsDeleted = 0;
END;
GO

-- Doctor_UpdateActiveStatus
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_UpdateActiveStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_UpdateActiveStatus]
GO

CREATE PROCEDURE [dbo].[Doctor_UpdateActiveStatus]
    @DoctorID INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Update IsActive in Users table
    UPDATE u
    SET u.IsActive = @IsActive,
        u.UpdatedAt = GETDATE()
    FROM Users u
    INNER JOIN Doctors d ON d.UserID = u.UserID
    WHERE d.DoctorID = @DoctorID
        AND d.IsDeleted = 0;
    
    -- Return updated doctor record
    SELECT 
        d.DoctorID,
        d.UserID,
        d.DoctorReferenceID,
        ISNULL(d.Specialization, '') AS Specialization,
        ISNULL(d.LicenseNumber, '') AS LicenseNumber,
        ISNULL(d.HospitalAffiliation, '') AS HospitalAffiliation,
        ISNULL(d.Expertise, '') AS Expertise,
        ISNULL(d.ProfileStep, 0) AS ProfileStep,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        u.IsActive
    FROM Doctors d
    INNER JOIN Users u ON u.UserID = d.UserID
    WHERE d.DoctorID = @DoctorID;
END;
GO

-- Doctor_UpdateOnlineStatus
-- Note: This assumes IsOnline is stored in Users table
-- If IsOnline is in Doctors table, adjust accordingly
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_UpdateOnlineStatus]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_UpdateOnlineStatus]
GO

CREATE PROCEDURE [dbo].[Doctor_UpdateOnlineStatus]
    @DoctorID INT,
    @IsOnline BIT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- If IsOnline is in Users table, use this:
    -- Note: You may need to add IsOnline column to Users table if it doesn't exist
    -- For now, this is a placeholder - adjust based on your schema
    
    -- Update IsActive as a proxy for IsOnline (if IsOnline column doesn't exist)
    -- OR update IsOnline if the column exists in Users table
    UPDATE u
    SET u.UpdatedAt = GETDATE()
    -- Add: u.IsOnline = @IsOnline  if the column exists
    FROM Users u
    INNER JOIN Doctors d ON d.UserID = u.UserID
    WHERE d.DoctorID = @DoctorID
        AND d.IsDeleted = 0;
    
    -- Return updated doctor record
    SELECT 
        d.DoctorID,
        d.UserID,
        d.DoctorReferenceID,
        ISNULL(d.Specialization, '') AS Specialization,
        ISNULL(d.LicenseNumber, '') AS LicenseNumber,
        ISNULL(d.HospitalAffiliation, '') AS HospitalAffiliation,
        ISNULL(d.Expertise, '') AS Expertise,
        ISNULL(d.ProfileStep, 0) AS ProfileStep,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted
    FROM Doctors d
    WHERE d.DoctorID = @DoctorID;
    
    -- IMPORTANT: If IsOnline column exists in Users or Doctors table,
    -- uncomment and adjust the UPDATE statement above accordingly
END;
GO

-- =============================================
-- END OF MISSING STORED PROCEDURES
-- =============================================

