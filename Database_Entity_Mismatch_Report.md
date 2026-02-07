# Database-Entity Mismatch Analysis and Resolution Report

## Summary
This document outlines all mismatches found between database table scripts and entity classes, along with the resolutions applied.

## Date: 2026-02-07

---

## 1. Appointment Entity vs Appointment Table

### Issues Found:
1. **Entity had fields not in database table:**
   - `PatientName` - NOT in table
   - `Gender` - NOT in table
   - `Age` - NOT in table
   - `PhoneNumber` - NOT in table
   - `BloodGroup` - NOT in table

2. **Entity missing fields from database table:**
   - `PatientId` - EXISTS in table but missing in entity

3. **Field nullability mismatches:**
   - `SessionId` - Entity: Required, Table: NULL
   - `ScheduleId` - Entity: Required, Table: NULL
   - `AppointmentDate` - Entity: Required, Table: NULL
   - `DoctorProfileId` - Entity: Required, Table: NULL
   - `SerialNo` - Entity: `[NotMapped]`, Table: Actual column

### Resolution:
- ✅ Removed fields that don't exist in database (`PatientName`, `Gender`, `Age`, `PhoneNumber`, `BloodGroup`)
- ✅ Added missing `PatientId` field
- ✅ Made all fields nullable to match database schema
- ✅ Removed `[NotMapped]` from `SerialNo` as it exists in the table
- ✅ Updated repository to accept `AppointmentInsertRequestDto` directly (since stored procedure handles patient creation)
- ✅ Updated service layer to pass DTO directly to repository

### Files Modified:
- `PrescriptionModule/Entities/EntityClass/Appointment.cs`
- `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentCommandRepository.cs`
- `PrescriptionModule/Appointment/Domain/Repositories/Appointment/IAppointmentCommandRepository.cs`
- `PrescriptionModule/Appointment/Application/Services/AppointmentService.cs`

---

## 2. Patient Entity vs Patients Table

### Issues Found:
1. **Type mismatch:**
   - `PatientAge` - Entity: `double?`, Table: `decimal(5, 2)`

2. **Nullability mismatches:**
   - `Gender` - Entity: `string` (not nullable), Table: `nvarchar(10) NULL`
   - `Address` - Entity: `string` (not nullable), Table: `nvarchar(255) NULL`
   - `BloodGroup` - Entity: `string` (not nullable), Table: `nvarchar(5) NULL`
   - `InsuranceProvider` - Entity: `string` (not nullable), Table: `nvarchar(100) NULL`
   - `InsurancePolicyNumber` - Entity: `string` (not nullable), Table: `nvarchar(50) NULL`
   - `PatientReferenceID` - Entity: `int` (not nullable), Table: `int NULL`
   - `PatientCode` - Entity: `string` (not nullable), Table: `nvarchar(100) NULL`
   - `CreatedAt` - Entity: `DateTime` (not nullable), Table: `datetime NULL`
   - `UpdatedAt` - Entity: `DateTime` (not nullable), Table: `datetime NULL`
   - `IsDeleted` - Entity: `bool` (not nullable), Table: `bit NULL`

### Resolution:
- ✅ Changed `PatientAge` from `double?` to `decimal?` with `[Column(TypeName = "decimal(5, 2)")]` attribute
- ✅ Made all nullable fields nullable in entity to match database
- ✅ Added `[MaxLength(100)]` attribute to `PatientCode`

### Files Modified:
- `PrescriptionModule/Entities/EntityClass/PatientEntity/Patient.cs`

---

## 3. Prescription Entity vs Prescriptions Table

### Issues Found:
1. **Column name casing mismatches:**
   - Entity: `PrescriptionId`, Table: `PrescriptionID`
   - Entity: `TenantId`, Table: `TenantID`
   - Entity: `PatientId`, Table: `PatientID`
   - Entity: `DoctorId`, Table: `DoctorID`
   - Entity: `PatientFollowUpId`, Table: `PatientFollowUpID`
   - Entity: `PharmacyId`, Table: `PharmacyID`
   - Entity: `StatusId`, Table: `StatusID`
   - Entity: `isHeader`, Table: `IsHeader`

2. **Nullability mismatches:**
   - `IssueDate` - Entity: `DateTime` (not nullable), Table: `datetime NULL`
   - `Language` - Entity: `string` (required), Table: `nvarchar(10) NULL`
   - `IsArchived` - Entity: `bool` (not nullable), Table: `bit NULL`
   - `CreatedAt` - Entity: `DateTime` (not nullable), Table: `datetime NULL`
   - `UpdatedAt` - Entity: `DateTime` (not nullable), Table: `datetime NULL`
   - `IsDeleted` - Entity: `bool` (not nullable), Table: `bit NULL`
   - `PrescriptionCode` - Entity: `string` (not nullable), Table: `nvarchar(100) NULL`

### Resolution:
- ✅ Added `[Column]` attributes to map entity properties to correct database column names
- ✅ Made nullable fields nullable in entity to match database
- ✅ Added `[MaxLength(100)]` attribute to `PrescriptionCode`
- ✅ Fixed `isHeader` to `IsHeader` with proper column mapping

### Files Modified:
- `PrescriptionModule/Entities/EntityClass/PrescriptionEntity/Prescription.cs`

---

## 4. Company Entity vs Company Table

### Status: ✅ No Issues Found
- All fields match correctly
- No `UpdatedAt` field in either (as expected)

---

## 5. CompanyBranch Entity vs CompanyBranch Table

### Status: ✅ No Issues Found
- All fields match correctly
- No `UpdatedAt` field in either (as expected)

---

## 6. User Entity vs Users Table

### Status: ✅ No Issues Found
- All fields match correctly
- Field names, types, and nullability are consistent

---

## Stored Procedures Review

### Appointment_Insert
- ✅ Uses DTO fields correctly (`PatientName`, `Gender`, `Age`, `PhoneNumber`, `BloodGroup`)
- ✅ Creates/updates Patient record and then creates Appointment with `PatientId`
- ✅ Returns `SerialNo`

### Appointment_GetAll
- ✅ Returns correct fields from Appointment table
- ✅ Joins with Patients and Users tables for patient information

### Appointment_GetById
- ✅ Returns correct fields from Appointment table
- ✅ Joins with Patients and Users tables

### Note on Appointment_Update
- ⚠️ Stored procedure `Appointment_Update` was not found in the database scripts
- The Update method in repository may need to be reviewed/implemented

---

## Recommendations

1. **Review Update Operations:**
   - Verify if `Appointment_Update` stored procedure exists or needs to be created
   - Review Update methods for all entities to ensure they work with the corrected entity structures

2. **Testing:**
   - Test all CRUD operations for Appointment, Patient, and Prescription entities
   - Verify that stored procedures work correctly with the updated entity structures
   - Test nullable field handling

3. **Code Review:**
   - Review all places where Appointment entity was used with old fields (`PatientName`, `Gender`, etc.)
   - Ensure DTOs are used correctly in service layer
   - Check for any remaining hardcoded field references

4. **Documentation:**
   - Update API documentation if any field changes affect API contracts
   - Document the change in Appointment entity structure (patient info now in Patient table, not Appointment table)

---

## Files Modified Summary

### Entity Classes:
1. `PrescriptionModule/Entities/EntityClass/Appointment.cs`
2. `PrescriptionModule/Entities/EntityClass/PatientEntity/Patient.cs`
3. `PrescriptionModule/Entities/EntityClass/PrescriptionEntity/Prescription.cs`

### Repository Layer:
4. `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentCommandRepository.cs`
5. `PrescriptionModule/Appointment/Domain/Repositories/Appointment/IAppointmentCommandRepository.cs`

### Service Layer:
6. `PrescriptionModule/Appointment/Application/Services/AppointmentService.cs`

---

## Conclusion

All identified mismatches have been resolved. The entity classes now accurately reflect the database table structures. The Appointment entity has been restructured to match the database schema, and the repository/service layers have been updated to handle the new structure correctly.

