# Prescription API Module Database Scripts

This folder contains database scripts for the Prescription API module.

## Files

1. **PrescriptionAPI_CreateTables.sql** - Creates all required database tables
2. **PrescriptionAPI_StoredProcedures.sql** - Creates all stored procedures

## Installation Order

**IMPORTANT:** Run the scripts in this order:

1. First, run `PrescriptionAPI_CreateTables.sql` to create the tables
2. Then, run `PrescriptionAPI_StoredProcedures.sql` to create the stored procedures

## Tables Created

1. **CommonAdvices** - Stores common medical advice
2. **CommonHistory** - Stores common patient history templates
3. **Diagonosis** - Stores diagnosis/disease information
4. **Investigation** - Stores investigation/test information

## Stored Procedures Created

### CommonAdvices (7 procedures)
- `CommonAdvices_GetAll`
- `CommonAdvices_GetById`
- `CommonAdvices_GetByName`
- `GetBookMarksAdviceByDoctorId`
- `CommonAdvices_Insert`
- `CommonAdvices_Update`
- `CommonAdvices_DeleteById`

### Diagnosis (7 procedures)
- `Diagnosis_GetAll`
- `Diagnosis_GetById`
- `Diagonosis_GetDiagnosesByName`
- `GetBookMarksDiagnosisByDoctorId`
- `Diagnosis_Insert`
- `Diagnosis_Update`
- `Diagnosis_DeleteById`

### Symptom (7 procedures)
- `Symptom_GetAll`
- `Symptom_GetById`
- `Symptom_GetSymptomsByName`
- `GetBookMarksSymtomByDoctorId`
- `Symptom_Insert`
- `Symptom_Update`
- `Symptom_DeleteById`

### CommonHistory (7 procedures)
- `CommonHistory_GetAll`
- `CommonHistory_GetById`
- `CommonHistory_GetByName`
- `GetBookMarksHistoryByDoctorId`
- `CommonHistory_Insert`
- `CommonHistory_Update`
- `CommonHistory_DeleteById`

### Investigation (7 procedures)
- `Investigations_GetAll`
- `Investigations_GetById`
- `Investigation_GetInvestigationsByName`
- `Investigations_BookMarks`
- `Investigation_Insert`
- `Investigation_Update`
- `Investigation_DeleteById`

### Medication (7 procedures)
- `Medication_GetAll`
- `Medication_GetById`
- `Medication_GetByName`
- `GetBookMarksMedicationByDoctorId`
- `Medication_Insert`
- `Medication_Update`
- `Medication_DeledeById` (Note: Typo in repository, using exact name)

### FollowUp (7 procedures)
- `FollowUp_GetAll`
- `FollowUp_GetById`
- `FollowUp_GetFollowUpsByName`
- `FollowUp_BookMarks`
- `FollowUp_Insert`
- `FollowUp_Update`
- `FollowUp_DeleteById`

## Notes

- All tables use soft delete (IsDeleted flag) instead of physical deletion
- All tables include CreatedAt, UpdatedAt, IsDeleted, and IsActive columns
- Stored procedures follow the naming convention: `{Entity}_{Operation}`
- The `Symptoms` and `Medications` tables already exist in the main schema
- The `PatientFollowUp` table already exists in the main schema

## Dependencies

- Requires `Prescripto` database (or update USE statement)
- Some tables may reference `Tenants` table (from main schema)

