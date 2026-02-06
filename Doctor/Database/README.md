# Doctor Module Database Scripts

This directory contains all database scripts for the Doctor module.

## Files

### 1. `DoctorModule_CreateTables.sql`
Creates all database tables for the 6 new Doctor module entities:
- `DoctorSpecialization`
- `DoctorScheduleDaySession`
- `DoctorScheduledDayOff`
- `DoctorFeesSetup`
- `MasterDoctor`
- `CampaignDoctor`

**Usage:**
```sql
-- Update the database name in the script
USE [YourDatabaseName]
GO

-- Run the script
```

### 2. `DoctorModule_StoredProcedures.sql`
Creates all stored procedures (37 total) for CRUD operations:

#### For 6 New Entities (30 procedures):
Each entity has 5 procedures:
- `{Entity}_GetAll` - Get all records
- `{Entity}_GetById` - Get single record by ID
- `{Entity}_Insert` - Insert new record
- `{Entity}_Update` - Update existing record
- `{Entity}_DeleteById` - Soft delete record

#### GetByDoctorId Procedures (7 procedures):
- `DoctorChamber_GetByDoctorId`
- `DoctorDegree_GetByDoctorId`
- `DoctorSpecialization_GetByDoctorId`
- `DoctorSchedule_GetByDoctorId`
- `CampaignDoctor_GetByDoctorId`
- `MasterDoctor_GetByDoctorId`
- `DoctorFeesSetup_GetByDoctorId`

**Usage:**
```sql
-- Update the database name in the script
USE [YourDatabaseName]
GO

-- Run the script
```

## Installation Order

1. **First:** Run `DoctorModule_CreateTables.sql` to create the tables
2. **Second:** Run `DoctorModule_StoredProcedures.sql` to create the stored procedures

## Important Notes

1. **Database Name:** Update `USE [Prescripto]` to your actual database name in both scripts.

2. **Foreign Key Dependencies:** The scripts assume these tables already exist:
   - `Doctor` table
   - `DoctorSchedule` table
   - If you have `Speciality` or `Specialization` master tables, you may want to add foreign keys

3. **Soft Delete:** All delete operations are soft deletes (setting `IsDeleted = 1`) rather than hard deletes.

4. **Indexes:** Basic indexes are created on foreign key columns for performance.

5. **Default Values:**
   - `CreatedAt` defaults to `GETDATE()`
   - `IsDeleted` defaults to `0` (false)
   - `IsActive` defaults to `1` (true) where applicable

## Verification

After running the scripts, verify:

```sql
-- Check tables exist
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME IN (
    'DoctorSpecialization',
    'DoctorScheduleDaySession',
    'DoctorScheduledDayOff',
    'DoctorFeesSetup',
    'MasterDoctor',
    'CampaignDoctor'
);

-- Check stored procedures exist
SELECT ROUTINE_NAME 
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_TYPE = 'PROCEDURE'
AND ROUTINE_NAME LIKE 'Doctor%' 
   OR ROUTINE_NAME LIKE 'MasterDoctor%'
   OR ROUTINE_NAME LIKE 'CampaignDoctor%'
ORDER BY ROUTINE_NAME;
```

## Troubleshooting

1. **Foreign Key Errors:** Ensure `Doctor` and `DoctorSchedule` tables exist before running the table creation script.

2. **Permission Errors:** Ensure the database user has `CREATE TABLE` and `CREATE PROCEDURE` permissions.

3. **Syntax Errors:** Ensure you're using SQL Server (the scripts are written for SQL Server syntax).

## Next Steps

After creating tables and stored procedures:
1. Test the API endpoints
2. Build and verify compilation
3. Run integration tests

