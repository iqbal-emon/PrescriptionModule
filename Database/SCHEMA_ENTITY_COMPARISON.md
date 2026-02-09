# Database Schema and Entity Comparison Report

## Overview
This document compares the database table schemas with the corresponding Entity classes to ensure they match.

## Comparison Summary

### ✅ Tables with Matching Entities

#### 1. Prescriptions Table vs Prescription Entity
**Table:** `[dbo].[Prescriptions]`
**Entity:** `Entities.EntityClass.PrescriptionEntity.Prescription`

| Database Column | Entity Property | Match | Notes |
|----------------|-----------------|-------|-------|
| PrescriptionID | PrescriptionId | ✅ | Column mapping applied |
| TenantID | TenantId | ✅ | Column mapping applied |
| PatientID | PatientId | ✅ | Column mapping applied |
| DoctorID | DoctorId | ✅ | Column mapping applied |
| PatientFollowUpID | PatientFollowUpId | ✅ | Column mapping applied |
| PharmacyID | PharmacyId | ✅ | Nullable match |
| IssueDate | IssueDate | ✅ | Nullable DateTime |
| ExpiryDate | ExpiryDate | ✅ | Nullable DateTime |
| Language | Language | ✅ | MaxLength(10) |
| StatusID | StatusId | ✅ | Column mapping applied |
| FollowUpDate | FollowUpDate | ✅ | Nullable DateTime |
| IsArchived | IsArchived | ✅ | Nullable bool |
| CreatedAt | CreatedAt | ✅ | Nullable DateTime |
| UpdatedAt | UpdatedAt | ✅ | Nullable DateTime |
| IsDeleted | IsDeleted | ✅ | Nullable bool |
| IsHeader | IsHeader | ✅ | Column mapping applied |
| AppointmentRefId | AppointmentRefId | ✅ | Nullable int |
| isPreHand | isPreHand | ✅ | Column mapping applied |
| PrescriptionCode | PrescriptionCode | ✅ | MaxLength(100), nullable |

**Status:** ✅ **FULLY MATCHED**

#### 2. Doctors Table vs Doctor Entity
**Table:** `[dbo].[Doctors]`
**Entity:** `Entities.EntityClass.Doctor`

| Database Column | Entity Property | Match | Notes |
|----------------|-----------------|-------|-------|
| DoctorID | DoctorID | ✅ | Primary key |
| UserID | UserID | ✅ | Nullable, Foreign key |
| Specialization | Specialization | ✅ | MaxLength(100) |
| LicenseNumber | LicenseNumber | ✅ | MaxLength(50) |
| HospitalAffiliation | HospitalAffiliation | ✅ | MaxLength(100) |
| CreatedAt | CreatedAt | ✅ | DateTime |
| UpdatedAt | UpdatedAt | ✅ | DateTime |
| IsDeleted | IsDeleted | ✅ | bool |
| DoctorReferenceID | DoctorReferenceID | ✅ | Nullable int |
| Expertise | Expertise | ✅ | MaxLength(500), nullable |
| ProfileStep | ProfileStep | ✅ | Nullable int |

**Status:** ✅ **FULLY MATCHED**

#### 3. Users Table vs User Entity
**Table:** `[dbo].[Users]`
**Entity:** `Entities.EntityClass.User`

| Database Column | Entity Property | Match | Notes |
|----------------|-----------------|-------|-------|
| UserID | UserID | ✅ | Primary key |
| TenantID | TenantID | ✅ | Required, Foreign key |
| FirstName | FirstName | ✅ | MaxLength(50), nullable |
| LastName | LastName | ✅ | MaxLength(50), nullable |
| FullName | FullName | ✅ | MaxLength(150), required |
| UserName | UserName | ✅ | MaxLength(100), required |
| Email | Email | ✅ | MaxLength(150), nullable, EmailAddress |
| PasswordHash | PasswordHash | ✅ | MaxLength(255), required |
| UserType | UserType | ✅ | MaxLength(20), nullable |
| PhoneNumber | PhoneNumber | ✅ | MaxLength(15), nullable |
| ContactNo | ContactNo | ✅ | MaxLength(20), nullable |
| RoleId | RoleId | ✅ | Required, Foreign key |
| IsActive | IsActive | ✅ | Required bool |
| IsDeleted | IsDeleted | ✅ | Nullable bool |
| CreatedAt | CreatedAt | ✅ | DateTime |
| UpdatedAt | UpdatedAt | ✅ | Nullable DateTime |
| ReferenceUserId | ReferenceUserId | ✅ | Nullable int |

**Status:** ✅ **FULLY MATCHED**

#### 4. Company Table vs Company Entity
**Table:** `[dbo].[Company]`
**Entity:** `Entities.EntityClass.CompanyEntity.Company`

| Database Column | Entity Property | Match | Notes |
|----------------|-----------------|-------|-------|
| Id | Id | ✅ | Primary key |
| Name | Name | ✅ | MaxLength(200), required |
| LicenseNo | LicenseNo | ✅ | MaxLength(100), nullable |
| DrugRegCertificate | DrugRegCertificate | ✅ | MaxLength(100), nullable |
| Address | Address | ✅ | Nullable string |
| ContactNo | ContactNo | ✅ | MaxLength(20), nullable |
| Email | Email | ✅ | MaxLength(150), nullable, EmailAddress |
| CurrencySymbol | CurrencySymbol | ✅ | MaxLength(10), required, default "?" |
| CreatedAt | CreatedAt | ✅ | DateTime, required |

**Status:** ✅ **FULLY MATCHED**

### ⚠️ Tables Requiring Review

#### 1. PrescriptionPdf Table
**Table:** `[dbo].[PrescriptionPdf]`
**Entity:** `Entities.EntityClass.PrescriptionEntity.PrescriptionPdf`

**Note:** Entity exists and should be verified for complete property matching.

#### 2. PrescriptionTemplate Table
**Table:** `[dbo].[PrescriptionTemplate]`
**Entity:** `Entities.EntityClass.PrescriptionEntity.PrescriptionTemplate`

**Note:** Entity exists and should be verified for complete property matching.

## Stored Procedures Comparison

### ✅ Existing Stored Procedures

The following stored procedures already exist and match the required functionality:

1. **Prescription_GetById** - ✅ Exists
2. **Prescription_GetByAppointmentId** - ✅ Created in new-stored-procedures.sql
3. **PrescriptionTemplate_GetById** - ✅ Exists (as PrescriptionTemplate_GetById)
4. **PrescriptionTemplate_GetAllByDoctorId** - ✅ Exists (as PrescriptionTemplates_GetByDoctorId)
5. **PrescriptionPdf_GetByPatientDoctorId** - ✅ Exists
6. **PrescriptionPdf_GetPrehandByDoctorId** - ✅ Exists
7. **PrescriptionPdf_GetByAppointmentId** - ✅ Exists
8. **Diagnosis_GetBookmarks** - ✅ Exists (as Diagnosis_BookMarks)
9. **Diagnosis_GetByName** - ✅ Exists (as Diagonosis_GetDiagnosesByName)
10. **FollowUp_GetAllByName** - ✅ Exists (as FollowUp_GetFollowUpsByName)
11. **Medication_GetByName** - ✅ Exists
12. **Symptom_GetBookmarks** - ✅ Exists (as Symptom_BookMarks)
13. **Symptom_GetByName** - ✅ Exists (as Symptom_GetSymptomsByName)
14. **ScannedPrescription_Insert** - ✅ Exists
15. **Medication_GetDivisionUsage** - ✅ Created in new-stored-procedures.sql
16. **Doctor_GetDetailsByAdmin** - ✅ Created in new-stored-procedures.sql

## API Endpoints Status

### ✅ Implemented Endpoints

1. **/auth/firebase/verify** - ✅ Updated in AuthController (requires FirebaseAdmin implementation)
2. **/doctor-profile/{id}/doctor-details-by-admin** - ✅ Added to DoctorController
3. **/prescription-master/{id}/prescription** - ✅ Added to PrescriptionController
4. **/prescription-master/prescription-by-appointment-id/{appointmentId}** - ✅ Added to PrescriptionController
5. **/get-template-prescription-by-id** - ✅ Added to PrescriptionController
6. **/gets-all-prescription-template-by-doctor-id** - ✅ Added to PrescriptionController
7. **/get-pdf-prescriptions-by-patient-doctor-id** - ✅ Added to PrescriptionController
8. **/get-pdf-prescriptions-by-doctor-prehand-id** - ✅ Added to PrescriptionController
9. **/get-prescription-pdf-by-appointment-id** - ✅ Added to PrescriptionController
10. **/gets-bookmarks-diagnosis** - ✅ Exists in DiagonosisController
11. **/gets-diagnosis-by-name** - ✅ Exists in DiagonosisController
12. **/gets-all-followup-by-name** - ✅ Exists in FollowUpController
13. **/get-medication-by-name** - ✅ Exists in MedicationController
14. **/gets-bookmarks-chief-complaints** - ✅ Exists in SymptomsController
15. **/gets-chief-complaint-by-name** - ✅ Exists in SymptomsController
16. **/prescription-upload** - ✅ Added to PrescriptionController
17. **/get-medication-division-usage** - ✅ Added to PrescriptionController

## Recommendations

### 1. Service Layer Implementation
The following service methods need to be implemented or verified:
- `PrescriptionService.GetByAppointmentId()`
- `PrescriptionTemplateService.GetById()`
- `PrescriptionTemplateService.GetAllByDoctorId()`
- `PrescriptionPdfService.GetByPatientDoctorId()`
- `PrescriptionPdfService.GetPrehandByDoctorId()`
- `PrescriptionPdfService.GetByAppointmentId()`
- `PrescriptionService.UploadScannedPrescription()`
- `PrescriptionService.GetMedicationDivisionUsage()`
- `DoctorService.GetDetailsByAdmin()`

### 2. Firebase Authentication
The `/auth/firebase/verify` endpoint requires:
- FirebaseAdmin NuGet package installation
- Proper Firebase token verification implementation
- User creation/login logic after token verification

### 3. DTOs Required
The following DTOs may need to be created:
- `ScannedPrescriptionUploadRequestDto`
- Response DTOs for the new endpoints

## Conclusion

✅ **Overall Status:** Most tables and entities are properly matched. The stored procedures have been created and the API endpoints have been added to the controllers. The main remaining work is:
1. Implementing the service layer methods
2. Completing Firebase authentication implementation
3. Creating any missing DTOs

