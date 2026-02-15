# Appointment_Insert FullName Fix

## Problem

SQL Error 515: "Cannot insert the value NULL into column 'FullName', table 'SoowGood_System_Dev_2.dbo.Users'; column does not allow nulls."

The `Users` table requires the following NOT NULL columns:
- `FullName` (NVARCHAR(150) NOT NULL)
- `UserName` (NVARCHAR(100) NOT NULL, UNIQUE)
- `RoleId` (INT NOT NULL)
- `PasswordHash` (NVARCHAR(255) NOT NULL)

The stored procedure was only inserting `FirstName` and `LastName`, but not `FullName`, `UserName`, or `RoleId`.

## Solution

Updated `Appointment_Insert` stored procedure to:

1. **Added `FullName` column**: Set to `@PatientName` (trimmed)
2. **Added `UserName` column**: Generated as `'Patient_' + cleaned phone number` with uniqueness check
3. **Added `RoleId` column**: 
   - Added optional parameter `@PatientRoleId INT = NULL`
   - If not provided, queries for a Patient role (name contains 'Patient' or IsDefault = 1)
   - Falls back to first active role if no Patient role found
   - Validates that a valid role exists before inserting

## Changes Made

### 1. Stored Procedure Signature
```sql
ALTER PROCEDURE [dbo].[Appointment_Insert]
(
    @PatientName NVARCHAR(100),
    @PhoneNumber NVARCHAR(20),
    @Gender NVARCHAR(50),
    @BloodGroup NVARCHAR(10),
    @Age INT,
    @SessionId INT,
    @ScheduleId INT,
    @DoctorProfileId INT,
    @AppointmentDate DATETIME,
    @PatientRoleId INT = NULL  -- NEW: Optional RoleId parameter
)
```

### 2. Added Variables
```sql
DECLARE 
    @NewUserID INT = NULL,
    @NewPatientID INT = NULL,
    @NewAppointmentID INT,
    @PatientCode VARCHAR(10),
    @SerialNo INT,
    @PatientRoleIdValue INT = NULL,  -- NEW: Stores resolved RoleId
    @UserName NVARCHAR(100);         -- NEW: Generated username
```

### 3. Role ID Resolution Logic
```sql
-- Get Patient Role ID (if not provided, query for default Patient role)
IF @PatientRoleId IS NULL OR @PatientRoleId <= 0
BEGIN
    -- Try to find a role with name containing 'Patient' or use default role
    SELECT TOP 1 @PatientRoleIdValue = Id
    FROM [dbo].[Role]
    WHERE (Name LIKE '%Patient%' OR Name LIKE '%patient%' OR IsDefault = 1)
      AND IsActive = 1
    ORDER BY IsDefault DESC, Id ASC;
    
    -- If still no role found, use first active role
    IF @PatientRoleIdValue IS NULL
    BEGIN
        SELECT TOP 1 @PatientRoleIdValue = Id
        FROM [dbo].[Role]
        WHERE IsActive = 1
        ORDER BY Id ASC;
    END
END
ELSE
BEGIN
    SET @PatientRoleIdValue = @PatientRoleId;
END

-- Validate RoleId was found
IF @PatientRoleIdValue IS NULL OR @PatientRoleIdValue <= 0
BEGIN
    RAISERROR('No valid Patient role found. Please ensure at least one active role exists in the Role table.', 16, 1);
    RETURN;
END
```

### 4. UserName Generation
```sql
-- Generate unique UserName from phone number (remove special characters)
DECLARE @CleanPhone NVARCHAR(20) = LTRIM(RTRIM(@PhoneNumber));
SET @CleanPhone = REPLACE(@CleanPhone, '+', '');
SET @CleanPhone = REPLACE(@CleanPhone, '-', '');
SET @CleanPhone = REPLACE(@CleanPhone, ' ', '');
SET @CleanPhone = REPLACE(@CleanPhone, '(', '');
SET @CleanPhone = REPLACE(@CleanPhone, ')', '');
SET @UserName = 'Patient_' + @CleanPhone;

-- Ensure UserName is unique (append number if needed)
DECLARE @UserNameCounter INT = 0;
DECLARE @FinalUserName NVARCHAR(100) = @UserName;

WHILE EXISTS (SELECT 1 FROM [dbo].[Users] WHERE UserName = @FinalUserName)
BEGIN
    SET @UserNameCounter = @UserNameCounter + 1;
    SET @FinalUserName = @UserName + '_' + CAST(@UserNameCounter AS NVARCHAR(10));
END
```

### 5. Updated INSERT Statement
```sql
INSERT INTO [dbo].[Users]
(
    TenantID, FirstName, LastName, FullName, UserName, Email, PasswordHash, UserType,
    PhoneNumber, RoleId, CreatedAt, UpdatedAt, IsActive, IsDeleted, ReferenceUserId
)
VALUES
(
    1,
    LTRIM(RTRIM(@PatientName)),
    LTRIM(RTRIM(@PatientName)),
    LTRIM(RTRIM(@PatientName)),  -- FullName (required)
    @FinalUserName,                -- UserName (required, unique)
    'Patient@gmail.com',
    'Patient',                      -- PasswordHash (required)
    'Patient',
    LTRIM(RTRIM(@PhoneNumber)),
    @PatientRoleIdValue,           -- RoleId (required)
    GETUTCDATE(),
    GETUTCDATE(),
    1,
    0,
    NULL
);
```

## Backward Compatibility

✅ **Fully backward compatible** - The `@PatientRoleId` parameter is optional with a default value of `NULL`, so existing code will continue to work without changes.

The stored procedure will automatically:
1. Query for a Patient role if `@PatientRoleId` is not provided
2. Use the provided `@PatientRoleId` if it's passed

## Testing

To test the fix:

1. **Test with default role lookup** (no `@PatientRoleId` provided):
```sql
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'Test Patient',
    @PhoneNumber = N'+8801234567890',
    @Gender = N'Male',
    @BloodGroup = N'O+',
    @Age = 30,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 12:00:00'
```

2. **Test with explicit RoleId**:
```sql
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'Test Patient',
    @PhoneNumber = N'+8801234567891',
    @Gender = N'Male',
    @BloodGroup = N'O+',
    @Age = 30,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 12:00:00',
    @PatientRoleId = 2  -- Explicit role ID
```

## Expected Results

- ✅ User created with `FullName`, `UserName`, and `RoleId`
- ✅ `UserName` is unique (appends number if duplicate)
- ✅ Patient record created successfully
- ✅ Appointment record created successfully
- ✅ Returns `SerialNo` for the appointment

## Notes

- The `UserName` format is `Patient_` + cleaned phone number (e.g., `Patient_8801234567890`)
- If a duplicate `UserName` exists, it appends `_1`, `_2`, etc.
- The stored procedure requires at least one active role in the `Role` table
- If no Patient-specific role is found, it uses the default role or first active role

