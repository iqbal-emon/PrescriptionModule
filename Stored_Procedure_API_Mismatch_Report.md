# Stored Procedure and API Mismatch Analysis Report

## Date: 2026-02-07

This report documents all mismatches found between stored procedures and their API usage, including parameter name mismatches, missing stored procedures, and incorrect API implementations.

---

## Critical Issues Found

### 1. ❌ Appointment_GetAll - Parameter Name Mismatch (FIXED)

**Location:** `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentQueryRepository.cs`

**Issue:**
- Line 86: `parameters.Add("@sessionId ", sessionId);` - **Trailing space and lowercase**
- Line 87: `parameters.Add("@scheduleId ", scheduleId);` - **Trailing space and lowercase**

**Stored Procedure Expects:**
- `@SessionId` (capital S, no space)
- `@ScheduleId` (capital S, no space)

**Impact:** 
- SessionId and ScheduleId filters were not being applied correctly
- Stored procedure would receive NULL for these parameters even when values were provided

**Resolution:** ✅ FIXED
- Changed to `@SessionId` and `@ScheduleId` (correct casing, no spaces)

---

### 2. ⚠️ Appointment_GetAll - Incomplete Parameter Passing

**Location:** `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentQueryRepository.cs`

**Issue:**
- Line 32-66: First `GetAll(int doctorId)` method calls `Appointment_GetAll` with only `doctorId` parameter
- Stored procedure expects: `@DoctorId`, `@SessionId`, `@ScheduleId`, `@SearchText`, `@PageNumber`, `@PageSize`, `@TotalCount OUTPUT`

**Current Code:**
```csharp
var result = await _dataAccess.LoadDataUsingProcedure<AppointmentApiResponseDto, dynamic>(
    "Appointment_GetAll",
    new { doctorId=doctorId }  // Only passing doctorId
);
```

**Problem:**
- Missing pagination parameters (@PageNumber, @PageSize, @TotalCount)
- Missing filter parameters (@SessionId, @ScheduleId, @SearchText)
- This method will not work correctly with the stored procedure

**Recommendation:**
- Remove the first `GetAll(int doctorId)` method or update it to use the paginated version
- The paginated version (line 67-128) correctly passes all parameters

---

### 3. ❌ Appointment_Update - Missing Stored Procedure

**Location:** `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentCommandRepository.cs`

**Issue:**
- Line 117-120: Code calls `Appointment_Update` stored procedure
- **Stored procedure does NOT exist in database scripts**

**Current Code:**
```csharp
var result = await _dataAccess.SaveDataUsingProcedureReturnIdWithIntDataType<Entities.EntityClass.Appointment>(
    "Appointment_Update",
    entity
);
```

**Impact:**
- Update operation will fail with "Stored procedure not found" error
- Appointment updates are not functional

**Required Stored Procedure:**
```sql
CREATE PROCEDURE [dbo].[Appointment_Update]
(
    @Id INT,
    @SessionId INT = NULL,
    @ScheduleId INT = NULL,
    @PatientId INT = NULL,
    @AppointmentDate DATETIME = NULL,
    @DoctorProfileId INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[Appointment]
    SET 
        SessionId = ISNULL(@SessionId, SessionId),
        ScheduleId = ISNULL(@ScheduleId, ScheduleId),
        PatientId = ISNULL(@PatientId, PatientId),
        AppointmentDate = ISNULL(@AppointmentDate, AppointmentDate),
        DoctorProfileId = ISNULL(@DoctorProfileId, DoctorProfileId),
        UpdatedAt = GETUTCDATE()
    WHERE Id = @Id AND IsDeleted = 0;
    
    IF @@ROWCOUNT > 0
        SELECT @Id AS UpdatedAppointmentId;
    ELSE
        SELECT NULL AS UpdatedAppointmentId;
END
```

---

### 4. ⚠️ Appointment_DeleteById - Missing Stored Procedure

**Location:** `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentCommandRepository.cs`

**Issue:**
- Line 30-33: Code calls `Appointment_DeleteById` stored procedure
- **Stored procedure does NOT exist in database scripts**

**Current Code:**
```csharp
var result = await _dataAccess.LoadSingleDataUsingProcedure<Entities.EntityClass.Appointment, dynamic>(
    "Appointment_DeleteById",
    new { AppointmentId = id }
);
```

**Impact:**
- Delete operation will fail with "Stored procedure not found" error
- Appointment deletions are not functional

**Required Stored Procedure:**
```sql
CREATE PROCEDURE [dbo].[Appointment_DeleteById]
    @AppointmentId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE [dbo].[Appointment]
    SET 
        IsDeleted = 1,
        UpdatedAt = GETUTCDATE()
    WHERE Id = @AppointmentId AND IsDeleted = 0;
    
    IF @@ROWCOUNT > 0
        SELECT @AppointmentId AS DeletedAppointmentId;
    ELSE
        SELECT NULL AS DeletedAppointmentId;
END
```

---

## Parameter Name Case Sensitivity Issues

### General Note on Dapper Parameter Mapping

Dapper performs **case-insensitive** parameter matching by default, so:
- `doctorId` will match `@DoctorId` ✅
- `appointmentId` will match `@AppointmentId` ✅

However, **trailing spaces** will cause issues:
- `@sessionId ` (with space) will NOT match `@SessionId` ❌

**Best Practice:** Always use exact parameter names as defined in stored procedures to avoid confusion and potential issues.

---

## Stored Procedure vs API Purpose Verification

### ✅ Appointment_Insert - CORRECT

**Stored Procedure Purpose:**
- Creates/updates Patient record based on phone number
- Creates Appointment record
- Returns SerialNo

**API Purpose:**
- Insert new appointment with patient information
- Handles patient creation if not exists

**Status:** ✅ Purpose matches correctly

---

### ✅ Appointment_GetAll - CORRECT (after parameter fix)

**Stored Procedure Purpose:**
- Returns paginated list of appointments
- Filters by DoctorId, SessionId, ScheduleId, SearchText
- Returns TotalCount via OUTPUT parameter

**API Purpose:**
- Get all appointments for a doctor with pagination and filtering

**Status:** ✅ Purpose matches correctly (after parameter name fix)

---

### ✅ Appointment_GetById - CORRECT

**Stored Procedure Purpose:**
- Returns single appointment with patient details

**API Purpose:**
- Get appointment by ID with full details

**Status:** ✅ Purpose matches correctly

---

## Recommendations

### Immediate Actions Required:

1. **Create Missing Stored Procedures:**
   - `Appointment_Update` - For updating appointments
   - `Appointment_DeleteById` - For soft-deleting appointments

2. **Fix or Remove Duplicate GetAll Method:**
   - Remove the first `GetAll(int doctorId)` method (line 32-66)
   - Or update it to properly call the paginated version

3. **Verify All Parameter Names:**
   - Review all stored procedure calls for trailing spaces
   - Ensure parameter names match exactly (case-insensitive is OK, but exact is better)

### Code Quality Improvements:

1. **Add Parameter Validation:**
   - Validate required parameters before calling stored procedures
   - Handle NULL values appropriately

2. **Error Handling:**
   - Add specific error messages for missing stored procedures
   - Log parameter mismatches for debugging

3. **Testing:**
   - Test all CRUD operations for Appointment module
   - Verify pagination works correctly
   - Test filtering by SessionId and ScheduleId

---

## Files Modified

1. ✅ `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentQueryRepository.cs`
   - Fixed parameter names: `@sessionId ` → `@SessionId`, `@scheduleId ` → `@ScheduleId`

## Files Requiring Attention

1. ⚠️ `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentCommandRepository.cs`
   - Remove or fix `Update` method (calls non-existent stored procedure)
   - Remove or fix `Delete` method (calls non-existent stored procedure)

2. ⚠️ `PrescriptionModule/Appointment/Insfracture/RepositoriesImplement/Appointment/AppointmentQueryRepository.cs`
   - Remove duplicate `GetAll(int doctorId)` method or update it

3. ⚠️ Database Scripts
   - Add `Appointment_Update` stored procedure
   - Add `Appointment_DeleteById` stored procedure

---

## Summary

### Issues Found: 4
- ✅ Fixed: 1 (Parameter name mismatch)
- ⚠️ Requires Action: 3 (Missing stored procedures, duplicate method)

### Impact:
- **High:** Update and Delete operations completely non-functional
- **Medium:** First GetAll method may not work correctly
- **Low:** Parameter name issue (now fixed) was causing filter failures

---

## Next Steps

1. Create the missing stored procedures in the database
2. Update the repository code to handle the new stored procedures correctly
3. Remove or fix the duplicate GetAll method
4. Test all Appointment CRUD operations
5. Review other modules for similar issues

