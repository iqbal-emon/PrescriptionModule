# Database Migration Summary

This document lists all database schemas and stored procedures that need to be created for the newly implemented modules.

## 📋 Table of Contents
1. [New Tables](#new-tables)
2. [Table Alterations](#table-alterations)
3. [Stored Procedures](#stored-procedures)
4. [Execution Order](#execution-order)

---

## 🆕 New Tables

### 1. Speciality Table
**File**: `Speciality/Database/SpecialityModule_CreateTables.sql`
- **Table Name**: `Speciality`
- **Columns**: SpecialityID, SpecialityName, Description, TenantID, CreatedAt, UpdatedAt, IsDeleted

### 2. Specialization Table
**File**: `Specialization/Database/SpecializationModule_CreateTables.sql`
- **Table Name**: `Specialization`
- **Columns**: SpecializationID, SpecialityID (FK), SpecializationName, Description, TenantID, CreatedAt, UpdatedAt, IsDeleted

### 3. DocumentsAttachment Table
**File**: `DocumentsAttachment/Database/DocumentsAttachmentModule_CreateTables.sql`
- **Table Name**: `DocumentsAttachment`
- **Columns**: DocumentsAttachmentID, FileName, OriginalFileName, Path, EntityType, EntityId, AttachmentType, RelatedEntityid, TenantID, CreatedAt, UpdatedAt, IsDeleted

---

## 🔧 Table Alterations

### Doctor Table
**File**: `Doctor/Database/DoctorModule_AlterTable.sql`
- **New Columns**:
  - `Expertise` (NVARCHAR(500), NULL)
  - `ProfileStep` (INT, NULL)

---

## 📦 Stored Procedures

### Speciality Module
**File**: `Speciality/Database/SpecialityModule_StoredProcedures.sql`
- `Speciality_GetAll`
- `Speciality_GetById`
- `Speciality_Insert`
- `Speciality_Update`
- `Speciality_DeleteById`

### Specialization Module
**File**: `Specialization/Database/SpecializationModule_StoredProcedures.sql`
- `Specialization_GetAll`
- `Specialization_GetById`
- `Specialization_GetBySpecialityId`
- `Specialization_Insert`
- `Specialization_Update`
- `Specialization_DeleteById`

### DocumentsAttachment Module
**File**: `DocumentsAttachment/Database/DocumentsAttachmentModule_StoredProcedures.sql`
- `DocumentsAttachment_GetAll`
- `DocumentsAttachment_GetById`
- `DocumentsAttachment_GetByEntityIdAndType`
- `DocumentsAttachment_GetDocumentInfo`
- `DocumentsAttachment_GetPaginated`
- `DocumentsAttachment_Insert`
- `DocumentsAttachment_Update`
- `DocumentsAttachment_DeleteById`

### Doctor Module (Additional)
**File**: `Doctor/Database/DoctorModule_StoredProcedures.sql` (append to existing)
- `Doctor_UpdateExpertise`
- `Doctor_UpdateProfileStep`
- `Doctor_GetByCreatorId`

### Patients Module (Additional)
**File**: `Patients/Database/PatientsModule_AdditionalStoredProcedures.sql`
- `Patients_GetByAgentMaster`
- `Patients_GetByAgentSupervisor`

---

## ⚠️ Important Notes

### 1. Database Name
All scripts use `[Prescripto]` as the database name. **Please update this** to match your actual database name before execution.

### 2. Agent Relationships
The `Patients_GetByAgentMaster` and `Patients_GetByAgentSupervisor` stored procedures contain placeholder logic. You may need to adjust the JOIN conditions based on your actual database schema:
- If `Patient` table has direct `AgentMasterID` and `AgentSupervisorID` columns, use those
- If the relationship is through `MasterDoctor` or another table, adjust the JOIN accordingly

### 3. Doctor_GetByCreatorId
This procedure assumes a relationship through `MasterDoctor` table. Adjust if your schema differs.

### 4. Notification Table
The Notification table may already exist in your schema. If not, ensure it matches the entity definition in `Entities/EntityClass/Notification.cs`.

---

## 📝 Execution Order

1. **Create New Tables** (in order):
   ```
   1. SpecialityModule_CreateTables.sql
   2. SpecializationModule_CreateTables.sql (depends on Speciality)
   3. DocumentsAttachmentModule_CreateTables.sql
   ```

2. **Alter Existing Tables**:
   ```
   4. DoctorModule_AlterTable.sql
   ```

3. **Create Stored Procedures** (can be run in any order):
   ```
   5. SpecialityModule_StoredProcedures.sql
   6. SpecializationModule_StoredProcedures.sql
   7. DocumentsAttachmentModule_StoredProcedures.sql
   8. DoctorModule_StoredProcedures.sql (append new procedures)
   9. PatientsModule_AdditionalStoredProcedures.sql
   ```

---

## ✅ Verification Checklist

After executing all scripts, verify:

- [ ] Speciality table exists with all columns
- [ ] Specialization table exists with foreign key to Speciality
- [ ] DocumentsAttachment table exists with all columns
- [ ] Doctor table has Expertise and ProfileStep columns
- [ ] All stored procedures exist and can be executed
- [ ] Foreign key constraints are properly set up
- [ ] Indexes are created for performance

---

## 🔍 Testing Queries

After migration, test with these queries:

```sql
-- Test Speciality
SELECT * FROM [dbo].[Speciality] WHERE [IsDeleted] = 0;

-- Test Specialization
SELECT * FROM [dbo].[Specialization] WHERE [IsDeleted] = 0;

-- Test DocumentsAttachment
SELECT * FROM [dbo].[DocumentsAttachment] WHERE [IsDeleted] = 0;

-- Test Doctor new columns
SELECT [DoctorID], [Expertise], [ProfileStep] FROM [dbo].[Doctor] WHERE [IsDeleted] = 0;

-- Test stored procedures
EXEC [dbo].[Speciality_GetAll];
EXEC [dbo].[Specialization_GetBySpecialityId] @SpecialityID = 1;
EXEC [dbo].[Doctor_UpdateExpertise] @DoctorID = 1, @Expertise = 'Test Expertise';
```

---

**Last Updated**: 2025-02
**Status**: Ready for execution

