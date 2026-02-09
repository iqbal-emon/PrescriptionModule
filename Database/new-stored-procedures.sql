USE [SoowGood_System]
GO

-- =============================================
-- Stored Procedure: Prescription_GetByAppointmentId
-- Description: Get prescription by appointment ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Prescription_GetByAppointmentId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Prescription_GetByAppointmentId]
GO
CREATE PROCEDURE [dbo].[Prescription_GetByAppointmentId]
    @AppointmentId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        PrescriptionID,
        TenantID,
        PatientID,
        DoctorID,
        PatientFollowUpID,
        PharmacyID,
        IssueDate,
        ExpiryDate,
        Language,
        StatusID,
        FollowUpDate,
        IsArchived,
        CreatedAt,
        UpdatedAt,
        IsDeleted,
        IsHeader,
        AppointmentRefId,
        isPreHand,
        PrescriptionCode
    FROM [dbo].[Prescriptions]
    WHERE AppointmentRefId = @AppointmentId AND IsDeleted = 0;
END;
GO

-- =============================================
-- Stored Procedure: PrescriptionTemplate_GetById
-- Description: Get prescription template by ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrescriptionTemplate_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[PrescriptionTemplate_GetById]
GO
CREATE PROCEDURE [dbo].[PrescriptionTemplate_GetById]
    @PrescriptionTemplateId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pt.PrescriptionTemplateID,
        pt.Name,
        pt.PrescriptionID,
        pt.DoctorID,
        pt.IsActive,
        pt.IsDeleted,
        pt.CreatedAt,
        pt.UpdatedAt,
        p.PrescriptionID AS Prescription_PrescriptionID,
        p.TenantID,
        p.PatientID,
        p.DoctorID AS Prescription_DoctorID,
        p.PatientFollowUpID,
        p.PharmacyID,
        p.IssueDate,
        p.ExpiryDate,
        p.Language,
        p.StatusID,
        p.FollowUpDate,
        p.IsArchived,
        p.CreatedAt AS Prescription_CreatedAt,
        p.UpdatedAt AS Prescription_UpdatedAt,
        p.IsDeleted AS Prescription_IsDeleted,
        p.IsHeader,
        p.AppointmentRefId,
        p.isPreHand,
        p.PrescriptionCode
    FROM [dbo].[PrescriptionTemplate] pt
    INNER JOIN [dbo].[Prescriptions] p ON pt.PrescriptionID = p.PrescriptionID
    WHERE pt.PrescriptionTemplateID = @PrescriptionTemplateId 
        AND pt.IsDeleted = 0 
        AND p.IsDeleted = 0;
END;
GO

-- =============================================
-- Stored Procedure: PrescriptionTemplate_GetAllByDoctorId
-- Description: Get all prescription templates by doctor ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrescriptionTemplate_GetAllByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[PrescriptionTemplate_GetAllByDoctorId]
GO
CREATE PROCEDURE [dbo].[PrescriptionTemplate_GetAllByDoctorId]
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pt.PrescriptionTemplateID,
        pt.Name,
        pt.PrescriptionID,
        pt.DoctorID,
        pt.IsActive,
        pt.IsDeleted,
        pt.CreatedAt,
        pt.UpdatedAt
    FROM [dbo].[PrescriptionTemplate] pt
    WHERE pt.DoctorID = @DoctorId 
        AND pt.IsDeleted = 0
        AND pt.IsActive = 1
    ORDER BY pt.CreatedAt DESC;
END;
GO

-- =============================================
-- Stored Procedure: PrescriptionPdf_GetByPatientDoctorId
-- Description: Get prescription PDFs by patient and doctor ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrescriptionPdf_GetByPatientDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[PrescriptionPdf_GetByPatientDoctorId]
GO
CREATE PROCEDURE [dbo].[PrescriptionPdf_GetByPatientDoctorId]
    @PatientId INT,
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pp.PrescriptionPdfID,
        pp.TenantID,
        pp.PrescriptionID,
        pp.DoctorID,
        pp.PatientID,
        pp.FilePath,
        pp.Description,
        pp.CreatedAt,
        pp.UpdatedAt,
        pp.IsDeleted,
        pp.IsActive,
        pp.AppointmentRefId
    FROM [dbo].[PrescriptionPdf] pp
    WHERE pp.PatientID = @PatientId 
        AND pp.DoctorID = @DoctorId
        AND pp.IsDeleted = 0
        AND pp.IsActive = 1
    ORDER BY pp.CreatedAt DESC;
END;
GO

-- =============================================
-- Stored Procedure: PrescriptionPdf_GetPrehandByDoctorId
-- Description: Get prehand prescription PDFs by doctor ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrescriptionPdf_GetPrehandByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[PrescriptionPdf_GetPrehandByDoctorId]
GO
CREATE PROCEDURE [dbo].[PrescriptionPdf_GetPrehandByDoctorId]
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        pp.PrescriptionPdfID,
        pp.TenantID,
        pp.PrescriptionID,
        pp.DoctorID,
        pp.PatientID,
        pp.FilePath,
        pp.Description,
        pp.CreatedAt,
        pp.UpdatedAt,
        pp.IsDeleted,
        pp.IsActive,
        pp.AppointmentRefId
    FROM [dbo].[PrescriptionPdf] pp
    INNER JOIN [dbo].[Prescriptions] p ON pp.PrescriptionID = p.PrescriptionID
    WHERE p.DoctorID = @DoctorId
        AND p.isPreHand = 1
        AND pp.IsDeleted = 0
        AND pp.IsActive = 1
    ORDER BY pp.CreatedAt DESC;
END;
GO

-- =============================================
-- Stored Procedure: PrescriptionPdf_GetByAppointmentId
-- Description: Get prescription PDF by appointment ID
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrescriptionPdf_GetByAppointmentId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[PrescriptionPdf_GetByAppointmentId]
GO
CREATE PROCEDURE [dbo].[PrescriptionPdf_GetByAppointmentId]
    @AppointmentId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        pp.PrescriptionPdfID,
        pp.TenantID,
        pp.PrescriptionID,
        pp.DoctorID,
        pp.PatientID,
        pp.FilePath,
        pp.Description,
        pp.CreatedAt,
        pp.UpdatedAt,
        pp.IsDeleted,
        pp.IsActive,
        pp.AppointmentRefId
    FROM [dbo].[PrescriptionPdf] pp
    WHERE pp.AppointmentRefId = @AppointmentId 
        AND pp.IsDeleted = 0
        AND pp.IsActive = 1
    ORDER BY pp.CreatedAt DESC;
END;
GO

-- =============================================
-- Stored Procedure: Diagnosis_GetBookmarks
-- Description: Get bookmarked diagnoses
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_GetBookmarks]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_GetBookmarks]
GO
CREATE PROCEDURE [dbo].[Diagnosis_GetBookmarks]
    @DoctorId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        d.DiagonosisID,
        d.Name,
        d.Description,
        d.Code,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        d.IsActive
    FROM [dbo].[Diagonosis] d
    WHERE d.IsDeleted = 0 
        AND d.IsActive = 1
        AND (@DoctorId IS NULL OR EXISTS (
            SELECT 1 FROM [dbo].[PrescriptionDiagnoses] pd
            INNER JOIN [dbo].[Prescriptions] p ON pd.PrescriptionID = p.PrescriptionID
            WHERE pd.DiagnosisID = d.DiagonosisID 
                AND p.DoctorID = @DoctorId
                AND pd.IsDeleted = 0
        ))
    ORDER BY d.Name;
END;
GO

-- =============================================
-- Stored Procedure: Diagnosis_GetByName
-- Description: Get diagnoses by name (search)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Diagnosis_GetByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Diagnosis_GetByName]
GO
CREATE PROCEDURE [dbo].[Diagnosis_GetByName]
    @Name NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DiagonosisID,
        Name,
        Description,
        Code,
        CreatedAt,
        UpdatedAt,
        IsDeleted,
        IsActive
    FROM [dbo].[Diagonosis]
    WHERE IsDeleted = 0 
        AND IsActive = 1
        AND (@Name IS NULL OR Name LIKE '%' + @Name + '%')
    ORDER BY Name;
END;
GO

-- =============================================
-- Stored Procedure: FollowUp_GetAllByName
-- Description: Get all followups by name (search)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FollowUp_GetAllByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[FollowUp_GetAllByName]
GO
CREATE PROCEDURE [dbo].[FollowUp_GetAllByName]
    @Name NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        FollowUpID,
        TenantID,
        Name,
        Description,
        CreatedAt,
        UpdatedAt,
        IsDeleted
    FROM [dbo].[FollowUp]
    WHERE IsDeleted = 0
        AND (@Name IS NULL OR Name LIKE '%' + @Name + '%')
    ORDER BY Name;
END;
GO

-- =============================================
-- Stored Procedure: Medication_GetByName
-- Description: Get medications by name (search)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_GetByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_GetByName]
GO
CREATE PROCEDURE [dbo].[Medication_GetByName]
    @Name NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        MedicationID,
        TenantID,
        MedicationName,
        Description,
        Manufacturer,
        DosageForm,
        Strength,
        CreatedAt,
        UpdatedAt,
        IsDeleted,
        MedicationBrandId,
        GenericName,
        DAR,
        IsActive,
        Barcode,
        QRCode,
        CategoryID,
        indication
    FROM [dbo].[Medications]
    WHERE IsDeleted = 0
        AND (@Name IS NULL OR MedicationName LIKE '%' + @Name + '%' OR GenericName LIKE '%' + @Name + '%')
    ORDER BY MedicationName;
END;
GO

-- =============================================
-- Stored Procedure: Symptom_GetBookmarks
-- Description: Get bookmarked symptoms (chief complaints)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_GetBookmarks]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_GetBookmarks]
GO
CREATE PROCEDURE [dbo].[Symptom_GetBookmarks]
    @DoctorId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        s.SymptomID,
        s.TenantID,
        s.SymptomName,
        s.Description,
        s.CreatedAt,
        s.UpdatedAt,
        s.IsDeleted
    FROM [dbo].[Symptoms] s
    WHERE s.IsDeleted = 0
        AND (@DoctorId IS NULL OR EXISTS (
            SELECT 1 FROM [dbo].[PrescriptionSymtom] ps
            INNER JOIN [dbo].[Prescriptions] p ON ps.PrescriptionID = p.PrescriptionID
            WHERE ps.SymtomID = s.SymptomID 
                AND p.DoctorID = @DoctorId
                AND ps.IsDeleted = 0
        ))
    ORDER BY s.SymptomName;
END;
GO

-- =============================================
-- Stored Procedure: Symptom_GetByName
-- Description: Get symptoms (chief complaints) by name (search)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Symptom_GetByName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Symptom_GetByName]
GO
CREATE PROCEDURE [dbo].[Symptom_GetByName]
    @Name NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        SymptomID,
        TenantID,
        SymptomName,
        Description,
        CreatedAt,
        UpdatedAt,
        IsDeleted
    FROM [dbo].[Symptoms]
    WHERE IsDeleted = 0
        AND (@Name IS NULL OR SymptomName LIKE '%' + @Name + '%')
    ORDER BY SymptomName;
END;
GO

-- =============================================
-- Stored Procedure: ScannedPrescription_Insert
-- Description: Insert scanned prescription
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScannedPrescription_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[ScannedPrescription_Insert]
GO
CREATE PROCEDURE [dbo].[ScannedPrescription_Insert]
    @TenantID INT,
    @PrescriptionID INT = NULL,
    @FilePath NVARCHAR(255),
    @CreatedAt DATETIME = NULL,
    @UpdatedAt DATETIME = NULL,
    @IsDeleted BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [dbo].[ScannedPrescriptions](
        TenantID,
        PrescriptionID,
        FilePath,
        CreatedAt,
        UpdatedAt,
        IsDeleted
    )
    VALUES (
        @TenantID,
        @PrescriptionID,
        @FilePath,
        ISNULL(@CreatedAt, GETUTCDATE()),
        ISNULL(@UpdatedAt, GETUTCDATE()),
        @IsDeleted
    );

    SELECT SCOPE_IDENTITY() AS ScannedPrescriptionID;
END;
GO

-- =============================================
-- Stored Procedure: Medication_GetDivisionUsage
-- Description: Get medication division usage statistics
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Medication_GetDivisionUsage]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Medication_GetDivisionUsage]
GO
CREATE PROCEDURE [dbo].[Medication_GetDivisionUsage]
    @TenantId INT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        m.MedicationID,
        m.MedicationName,
        m.GenericName,
        COUNT(pi.PrescriptionItemID) AS UsageCount,
        SUM(pi.Quantity) AS TotalQuantity
    FROM [dbo].[Medications] m
    LEFT JOIN [dbo].[PrescriptionItems] pi ON m.MedicationID = pi.MedicationID
    LEFT JOIN [dbo].[Prescriptions] p ON pi.PrescriptionID = p.PrescriptionID
    WHERE m.IsDeleted = 0
        AND (@TenantId IS NULL OR m.TenantID = @TenantId)
        AND (@StartDate IS NULL OR p.CreatedAt >= @StartDate)
        AND (@EndDate IS NULL OR p.CreatedAt <= @EndDate)
        AND (p.IsDeleted = 0 OR p.IsDeleted IS NULL)
    GROUP BY m.MedicationID, m.MedicationName, m.GenericName
    ORDER BY UsageCount DESC;
END;
GO

-- =============================================
-- Stored Procedure: Doctor_GetDetailsByAdmin
-- Description: Get doctor details by admin (comprehensive)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetDetailsByAdmin]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetDetailsByAdmin]
GO
CREATE PROCEDURE [dbo].[Doctor_GetDetailsByAdmin]
    @DoctorId INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Main doctor info
    SELECT 
        d.DoctorID,
        d.UserID,
        d.Specialization,
        d.LicenseNumber,
        d.HospitalAffiliation,
        d.CreatedAt,
        d.UpdatedAt,
        d.IsDeleted,
        d.DoctorReferenceID,
        d.Expertise,
        d.ProfileStep,
        u.UserID AS User_UserID,
        u.TenantID,
        u.FirstName,
        u.LastName,
        u.FullName,
        u.UserName,
        u.Email,
        u.PhoneNumber,
        u.ContactNo,
        u.UserType,
        u.RoleId,
        u.IsActive AS User_IsActive
    FROM [dbo].[Doctors] d
    LEFT JOIN [dbo].[Users] u ON d.UserID = u.UserID
    WHERE d.DoctorID = @DoctorId 
        AND d.IsDeleted = 0
        AND (u.IsDeleted = 0 OR u.IsDeleted IS NULL);

    -- Doctor Degrees
    SELECT 
        dd.DoctorDegreeID,
        dd.TenantID,
        dd.DoctorID,
        dd.DegreeID,
        dd.PassingYear,
        dd.InstituteName,
        dd.InstituteID,
        dd.Country,
        dd.CountryID,
        dd.City,
        dd.CityID,
        dd.ZipCode,
        dd.ZipCodeID,
        dd.IsDeleted,
        dd.CreatedAt,
        dd.UpdatedAt,
        deg.DegreeID AS Degree_DegreeID,
        deg.DegreeName,
        deg.Duration,
        deg.DurationType
    FROM [dbo].[DoctorDegree] dd
    LEFT JOIN [dbo].[Degree] deg ON dd.DegreeID = deg.DegreeID
    WHERE dd.DoctorID = @DoctorId 
        AND dd.IsDeleted = 0;

    -- Doctor Specializations
    SELECT 
        ds.DoctorSpecializationID,
        ds.DoctorID,
        ds.SpecialityID,
        ds.SpecializationID,
        ds.ServiceDetails,
        ds.DocumentName,
        ds.CreatedAt,
        ds.UpdatedAt,
        ds.IsDeleted,
        sp.SpecialityID AS Speciality_SpecialityID,
        sp.SpecialityName,
        sp.Description AS SpecialityDescription,
        sz.SpecializationID AS Specialization_SpecializationID,
        sz.SpecializationName,
        sz.Description AS SpecializationDescription
    FROM [dbo].[DoctorSpecialization] ds
    LEFT JOIN [dbo].[Speciality] sp ON ds.SpecialityID = sp.SpecialityID
    LEFT JOIN [dbo].[Specialization] sz ON ds.SpecializationID = sz.SpecializationID
    WHERE ds.DoctorID = @DoctorId 
        AND ds.IsDeleted = 0;

    -- Doctor Chambers
    SELECT 
        dc.ChamberID,
        dc.TenantID,
        dc.DoctorID,
        dc.ChamberName,
        dc.Address,
        dc.Country,
        dc.CountryID,
        dc.City,
        dc.CityID,
        dc.ZipCode,
        dc.ZipCodeID,
        dc.IsVisibleOnPrescription,
        dc.CreatedAt,
        dc.UpdatedAt,
        dc.IsDeleted,
        dc.ChamberReferenceId,
        dc.DistrictId,
        dc.DivisionId
    FROM [dbo].[DoctorChambers] dc
    WHERE dc.DoctorID = @DoctorId 
        AND dc.IsDeleted = 0;
END;
GO

