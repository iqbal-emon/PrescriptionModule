# Stored Procedure Update Summary

## ✅ **Updated Stored Procedures**

### 1. **DoctorScheduleDaySession_Insert**
**File**: `PrescriptionModule/Database/store-procedures.txt` (Line ~3734)

#### Changes Made:
- ✅ Added `@TenantID INT = NULL` parameter
- ✅ All entity fields are now included as parameters:
  - `@DoctorScheduleID` (Required)
  - `@ScheduleDayofWeek` (Optional)
  - `@StartTime` (Optional)
  - `@EndTime` (Optional)
  - `@NoOfPatients` (Optional)
  - `@IsActive` (Default: 1)
  - `@TenantID` (Optional - for DTO compatibility)
  - `@DoctorScheduleDaySessionID` (OUTPUT)

#### Note:
- `TenantID` is accepted as a parameter (for DTO compatibility) but **not inserted** into the table since the `DoctorScheduleDaySession` table doesn't have this column
- If the table is updated in the future to include `TenantID`, uncomment the marked lines in the INSERT statement

### 2. **DoctorScheduleDaySession_Update**
**File**: `PrescriptionModule/Database/store-procedures.txt` (Line ~3785)

#### Changes Made:
- ✅ Added `@TenantID INT = NULL` parameter
- ✅ All updatable fields are included as parameters

#### Note:
- `TenantID` is accepted as a parameter but **not updated** since the table doesn't have this column
- If the table is updated in the future to include `TenantID`, uncomment the marked line in the UPDATE statement

---

## 📋 **Field Mapping**

### Entity Fields → SP Parameters:

| Entity Field | SP Parameter | Required | Notes |
|-------------|-------------|----------|-------|
| `DoctorScheduleID` | `@DoctorScheduleID` | ✅ Yes | Foreign key |
| `ScheduleDayofWeek` | `@ScheduleDayofWeek` | ❌ No | MaxLength(50) |
| `StartTime` | `@StartTime` | ❌ No | MaxLength(20) |
| `EndTime` | `@EndTime` | ❌ No | MaxLength(20) |
| `NoOfPatients` | `@NoOfPatients` | ❌ No | Nullable int |
| `IsActive` | `@IsActive` | ❌ No | Default: 1 (true) |
| `TenantID` | `@TenantID` | ❌ No | **Not in table, but in DTO** |
| `CreatedAt` | Auto-set | N/A | GETDATE() in SP |
| `UpdatedAt` | Auto-set | N/A | GETDATE() in SP |
| `IsDeleted` | Auto-set | N/A | Default: 0 (false) |
| `DoctorScheduleDaySessionID` | `@DoctorScheduleDaySessionID` | OUTPUT | Identity column |

---

## 🔧 **Implementation Notes**

### Why TenantID is in SP but not in INSERT/UPDATE:
1. The DTO (`DoctorScheduleDaySessionInsertRequestDto`) requires `TenantID`
2. The repository mapping will try to pass `TenantID` to the SP
3. If the SP doesn't accept it, the call will fail
4. By accepting it as a parameter (even if unused), we maintain compatibility
5. When the table is updated to include `TenantID`, simply uncomment the marked lines

### Current Table Structure:
```sql
CREATE TABLE [dbo].[DoctorScheduleDaySession](
    [DoctorScheduleDaySessionID] [int] IDENTITY(1,1) NOT NULL,
    [DoctorScheduleID] [int] NOT NULL,
    [ScheduleDayofWeek] [nvarchar](50) NULL,
    [StartTime] [nvarchar](20) NULL,
    [EndTime] [nvarchar](20) NULL,
    [NoOfPatients] [int] NULL,
    [IsActive] [bit] NOT NULL,
    [CreatedAt] [datetime] NOT NULL,
    [UpdatedAt] [datetime] NULL,
    [IsDeleted] [bit] NOT NULL
    -- Note: No TenantID column currently
)
```

---

## ✅ **All Fields Now Included**

The stored procedures now accept all fields from:
- ✅ Entity class (`DoctorScheduleDaySession`)
- ✅ DTO class (`DoctorScheduleDaySessionInsertRequestDto`)
- ✅ All parameters match the expected structure

---

## 🚀 **Ready to Use**

The stored procedures are now fully compatible with:
- The entity mapping
- The DTO structure
- The service layer calls
- Future table updates (just uncomment TenantID lines)

