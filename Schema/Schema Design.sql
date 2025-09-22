CREATE TABLE Tenants (
    TenantID INT PRIMARY KEY IDENTITY(1,1),
    TenantName NVARCHAR(100) NOT NULL,
    Domain NVARCHAR(100) UNIQUE NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

GO

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL, -- Encrypted password
    UserType NVARCHAR(20) NOT NULL, -- Patient, Doctor, Pharmacist, Admin
    PhoneNumber NVARCHAR(15),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Users_TenantID ON Users(TenantID);

GO
CREATE INDEX IDX_Users_Email ON Users(Email);

GO
CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Address NVARCHAR(255),
    BloodGroup NVARCHAR(5),
    InsuranceProvider NVARCHAR(100),
    InsurancePolicyNumber NVARCHAR(50),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

CREATE INDEX IDX_Patients_UserID ON Patients(UserID);

GO
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Specialization NVARCHAR(100),
    LicenseNumber NVARCHAR(50) UNIQUE,
    HospitalAffiliation NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Doctors_UserID ON Doctors(UserID);


GO
CREATE TABLE Medications (
    MedicationID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    MedicationName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Manufacturer NVARCHAR(100),
    DosageForm NVARCHAR(50), -- Tablet, Capsule, Liquid, etc.
    Strength NVARCHAR(50), -- 500mg, 10mg, etc.
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Medications_TenantID ON Medications(TenantID);

GO
CREATE TABLE Pharmacies (
    PharmacyID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    PharmacyName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    PhoneNumber NVARCHAR(15),
    Email NVARCHAR(100),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Pharmacies_TenantID ON Pharmacies(TenantID);
GO

CREATE TABLE PatientFollowUp (
    PatientFollowUpID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    FollowUp NVARCHAR(100) NOT NULL,
	Description NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
GO
CREATE TABLE Prescriptions (
    PrescriptionID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID),
	PatientFollowUpID INT NOT NULL FOREIGN KEY REFERENCES PatientFollowUp(PatientFollowUpID),
    PharmacyID INT FOREIGN KEY REFERENCES Pharmacies(PharmacyID),
    IssueDate DATETIME DEFAULT GETDATE(),
    ExpiryDate DATETIME,
    Language NVARCHAR(10) DEFAULT 'English', -- Multi-language support
    StatusID INT NOT NULL, -- Foreign key to PrescriptionStatuses table
    FollowUpDate DATETIME, -- Quick Actions: Follow-up date
    IsArchived BIT DEFAULT 0, -- Archive functionality
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Prescriptions_TenantID ON Prescriptions(TenantID);

GO
CREATE INDEX IDX_Prescriptions_PatientID ON Prescriptions(PatientID);

GO
CREATE TABLE PrescriptionItems (
    PrescriptionItemID INT PRIMARY KEY IDENTITY(1,1),
    PrescriptionID INT NOT NULL FOREIGN KEY REFERENCES Prescriptions(PrescriptionID),
    MedicationID INT NOT NULL FOREIGN KEY REFERENCES Medications(MedicationID),
    Dosage NVARCHAR(50), -- 1 tablet, 2 times a day
    Quantity INT,
    Instructions NVARCHAR(255), -- Take with food, etc.
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_PrescriptionItems_PrescriptionID ON PrescriptionItems(PrescriptionID);

GO
CREATE TABLE Diseases (
    DiseaseID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    DiseaseName NVARCHAR(100) NOT NULL, -- Name of the disease or diagnosis
    Description NVARCHAR(255), -- Optional description
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Diseases_TenantID ON Diseases(TenantID);

GO
CREATE TABLE PrescriptionDiagnoses (
    PrescriptionDiagnosisID INT PRIMARY KEY IDENTITY(1,1),
    PrescriptionID INT NOT NULL FOREIGN KEY REFERENCES Prescriptions(PrescriptionID),
    DiseaseID INT NOT NULL FOREIGN KEY REFERENCES Diseases(DiseaseID),
    Notes NVARCHAR(255), -- Additional notes about the diagnosis
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_PrescriptionDiagnoses_PrescriptionID ON PrescriptionDiagnoses(PrescriptionID);

GO
CREATE INDEX IDX_PrescriptionDiagnoses_DiseaseID ON PrescriptionDiagnoses(DiseaseID);

GO
CREATE TABLE Examinations (
    ExaminationID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID),
    ExaminationDate DATETIME DEFAULT GETDATE(), -- Date of the examination
    Findings NVARCHAR(MAX), -- Doctor's observations or findings
    Notes NVARCHAR(255), -- Additional notes
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Examinations_TenantID ON Examinations(TenantID);

GO
CREATE INDEX IDX_Examinations_PatientID ON Examinations(PatientID);

GO
CREATE INDEX IDX_Examinations_DoctorID ON Examinations(DoctorID);

GO
CREATE TABLE PrescriptionExaminations (
    PrescriptionExaminationID INT PRIMARY KEY IDENTITY(1,1),
    PrescriptionID INT NOT NULL FOREIGN KEY REFERENCES Prescriptions(PrescriptionID),
    ExaminationID INT NOT NULL FOREIGN KEY REFERENCES Examinations(ExaminationID),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_PrescriptionExaminations_PrescriptionID ON PrescriptionExaminations(PrescriptionID);

GO
CREATE INDEX IDX_PrescriptionExaminations_ExaminationID ON PrescriptionExaminations(ExaminationID);

GO
CREATE TABLE LabTests (
    LabTestID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    TestName NVARCHAR(100) NOT NULL, -- Name of the test (e.g., CBC, Lipid Profile)
    Description NVARCHAR(255), -- Optional description of the test
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_LabTests_TenantID ON LabTests(TenantID);

GO
CREATE TABLE PatientLabTests (
    PatientLabTestID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    PatientID INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientID),
    DoctorID INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorID),
    LabTestID INT NOT NULL FOREIGN KEY REFERENCES LabTests(LabTestID),
    TestDate DATETIME DEFAULT GETDATE(), -- Date the test was ordered
    Status NVARCHAR(20) DEFAULT 'Pending', -- Pending, Completed, Cancelled
    Notes NVARCHAR(255), -- Additional notes about the test
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO

CREATE INDEX IDX_PatientLabTests_TenantID ON PatientLabTests(TenantID);

GO
CREATE INDEX IDX_PatientLabTests_PatientID ON PatientLabTests(PatientID);

GO
CREATE INDEX IDX_PatientLabTests_DoctorID ON PatientLabTests(DoctorID);

GO
CREATE INDEX IDX_PatientLabTests_LabTestID ON PatientLabTests(LabTestID);

GO
CREATE TABLE LabTestResults (
    LabTestResultID INT PRIMARY KEY IDENTITY(1,1),
    PatientLabTestID INT NOT NULL FOREIGN KEY REFERENCES PatientLabTests(PatientLabTestID),
    Result NVARCHAR(MAX), -- Test results (can be JSON or structured data)
    ResultDate DATETIME DEFAULT GETDATE(), -- Date the results were recorded
    Notes NVARCHAR(255), -- Additional notes about the results
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO

CREATE INDEX IDX_LabTestResults_PatientLabTestID ON LabTestResults(PatientLabTestID);

GO
CREATE TABLE PrescriptionLabTests (
    PrescriptionLabTestID INT PRIMARY KEY IDENTITY(1,1),
    PrescriptionID INT NOT NULL FOREIGN KEY REFERENCES Prescriptions(PrescriptionID),
    PatientLabTestID INT NOT NULL FOREIGN KEY REFERENCES PatientLabTests(PatientLabTestID),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO

CREATE INDEX IDX_PrescriptionLabTests_PrescriptionID ON PrescriptionLabTests(PrescriptionID);

GO
CREATE INDEX IDX_PrescriptionLabTests_PatientLabTestID ON PrescriptionLabTests(PatientLabTestID);

GO
CREATE TABLE PrescriptionAdvice (
    AdviceID INT PRIMARY KEY IDENTITY(1,1),
    PrescriptionID INT NOT NULL FOREIGN KEY REFERENCES Prescriptions(PrescriptionID),
    AdviceText NVARCHAR(MAX) NOT NULL, -- Doctor's advice or recommendations
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_PrescriptionAdvice_PrescriptionID ON PrescriptionAdvice(PrescriptionID);

GO
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    Message NVARCHAR(255) NOT NULL,
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

GO
CREATE INDEX IDX_Notifications_TenantID ON Notifications(TenantID);

GO
CREATE INDEX IDX_Notifications_UserID ON Notifications(UserID);

GO
CREATE TABLE ScannedPrescriptions (
    ScannedPrescriptionID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    PrescriptionID INT FOREIGN KEY REFERENCES Prescriptions(PrescriptionID),
    FilePath NVARCHAR(255) NOT NULL, -- Path to the scanned file
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_ScannedPrescriptions_TenantID ON ScannedPrescriptions(TenantID);

GO

CREATE TABLE Languages (
    LanguageID INT PRIMARY KEY IDENTITY(1,1),
    LanguageCode NVARCHAR(10) NOT NULL, -- e.g., 'en', 'bn'
    LanguageName NVARCHAR(50) NOT NULL, -- e.g., 'English', 'Bangla'
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

-- Insert supported languages
--INSERT INTO Languages (LanguageCode, LanguageName)
--VALUES ('en', 'English'), ('bn', 'Bangla');

GO

CREATE TABLE MedicationTranslations (
    TranslationID INT PRIMARY KEY IDENTITY(1,1),
    MedicationID INT NOT NULL FOREIGN KEY REFERENCES Medications(MedicationID),
    LanguageID INT NOT NULL FOREIGN KEY REFERENCES Languages(LanguageID),
    TranslatedName NVARCHAR(100) NOT NULL, -- Medication name in the target language
    TranslatedDescription NVARCHAR(255), -- Medication description in the target language
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_MedicationTranslations_MedicationID ON MedicationTranslations(MedicationID);

GO
CREATE INDEX IDX_MedicationTranslations_LanguageID ON MedicationTranslations(LanguageID);

GO

CREATE TABLE LabTestTranslations (
    TranslationID INT PRIMARY KEY IDENTITY(1,1),
    LabTestID INT NOT NULL FOREIGN KEY REFERENCES LabTests(LabTestID),
    LanguageID INT NOT NULL FOREIGN KEY REFERENCES Languages(LanguageID),
    TranslatedName NVARCHAR(100) NOT NULL, -- Lab test name in the target language
    TranslatedDescription NVARCHAR(255), -- Lab test description in the target language
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_LabTestTranslations_LabTestID ON LabTestTranslations(LabTestID);

GO
CREATE INDEX IDX_LabTestTranslations_LanguageID ON LabTestTranslations(LanguageID);

GO

CREATE TABLE Symptoms (
    SymptomID INT PRIMARY KEY IDENTITY(1,1),
    TenantID INT NOT NULL FOREIGN KEY REFERENCES Tenants(TenantID),
    SymptomName NVARCHAR(100) NOT NULL, -- Name of the symptom (e.g., Fever, Cough)
    Description NVARCHAR(255), -- Optional description of the symptom
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_Symptoms_TenantID ON Symptoms(TenantID);


GO
CREATE TABLE SymptomTranslations (
    TranslationID INT PRIMARY KEY IDENTITY(1,1),
    SymptomID INT NOT NULL FOREIGN KEY REFERENCES Symptoms(SymptomID),
    LanguageID INT NOT NULL FOREIGN KEY REFERENCES Languages(LanguageID),
    TranslatedName NVARCHAR(100) NOT NULL, -- Symptom name in the target language
    TranslatedDescription NVARCHAR(255), -- Symptom description in the target language
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_SymptomTranslations_SymptomID ON SymptomTranslations(SymptomID);

GO
CREATE INDEX IDX_SymptomTranslations_LanguageID ON SymptomTranslations(LanguageID);

GO
CREATE TABLE DiseaseTranslations (
    TranslationID INT PRIMARY KEY IDENTITY(1,1),
    DiseaseID INT NOT NULL FOREIGN KEY REFERENCES Diseases(DiseaseID),
    LanguageID INT NOT NULL FOREIGN KEY REFERENCES Languages(LanguageID),
    TranslatedName NVARCHAR(100) NOT NULL, -- Disease name in the target language
    TranslatedDescription NVARCHAR(255), -- Disease description in the target language
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_DiseaseTranslations_DiseaseID ON DiseaseTranslations(DiseaseID);

GO
CREATE INDEX IDX_DiseaseTranslations_LanguageID ON DiseaseTranslations(LanguageID);

GO
CREATE TABLE ExaminationTranslations (
    TranslationID INT PRIMARY KEY IDENTITY(1,1),
    ExaminationID INT NOT NULL FOREIGN KEY REFERENCES Examinations(ExaminationID),
    LanguageID INT NOT NULL FOREIGN KEY REFERENCES Languages(LanguageID),
    TranslatedFindings NVARCHAR(MAX), -- Findings in the target language
    TranslatedNotes NVARCHAR(255), -- Notes in the target language
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO
CREATE INDEX IDX_ExaminationTranslations_ExaminationID ON ExaminationTranslations(ExaminationID);

GO
CREATE INDEX IDX_ExaminationTranslations_LanguageID ON ExaminationTranslations(LanguageID);

GO

CREATE TABLE AdviceTranslations (
    TranslationID INT PRIMARY KEY IDENTITY(1,1),
    AdviceID INT NOT NULL FOREIGN KEY REFERENCES PrescriptionAdvice(AdviceID),
    LanguageID INT NOT NULL FOREIGN KEY REFERENCES Languages(LanguageID),
    TranslatedAdvice NVARCHAR(MAX) NOT NULL, -- Advice in the target language
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    IsDeleted BIT DEFAULT 0
);

GO

CREATE INDEX IDX_AdviceTranslations_AdviceID ON AdviceTranslations(AdviceID);

GO
CREATE INDEX IDX_AdviceTranslations_LanguageID ON AdviceTranslations(LanguageID);

GO