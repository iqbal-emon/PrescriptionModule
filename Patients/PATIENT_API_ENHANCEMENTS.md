# Patient API Enhancements

## Summary
This document describes the enhancements made to the Patient module in PrescriptionModule to align with the functionality available in the Mycompany project and the API_LIST.md requirements.

## New Endpoints Added

### 1. Get Patient by Phone and Code
- **Endpoint**: `GET /api/2025-02/get-patient-by-phone-and-code?pCode={code}&pPhone={phone}`
- **Method**: `GetPatientByPhoneAndCode`
- **Description**: Retrieves a patient by patient code and phone number
- **Authorization**: Requires `PermissionConstants.PatientsGetId`

### 2. Get Patient by User Name
- **Endpoint**: `GET /api/2025-02/get-patient-by-user-name?userName={userName}`
- **Method**: `GetPatientByUserName`
- **Description**: Retrieves a patient by username (looks up user first, then patient)
- **Authorization**: Requires `PermissionConstants.PatientsGetId`

### 3. Get Patient by User ID (Direct)
- **Endpoint**: `GET /api/2025-02/get-patient-by-user-id-direct?userId={userId}`
- **Method**: `GetPatientByUserIdDirect`
- **Description**: Retrieves a patient directly by user ID
- **Note**: This is different from the existing `get-patient-by-user-id` endpoint which uses `GetByRoleAndReferenceId`
- **Authorization**: Requires `PermissionConstants.PatientsGetId`

### 4. Get All Patients List
- **Endpoint**: `GET /api/2025-02/get-all-patients-list`
- **Method**: `GetAllPatientsList`
- **Description**: Retrieves all patients without pagination
- **Authorization**: Requires `PermissionConstants.PatientsGetAll`

### 5. Get Patient List by User Profile ID
- **Endpoint**: `GET /api/2025-02/get-patient-list-by-user-profile-id?profileId={id}&role={role}`
- **Method**: `GetPatientListByUserProfileId`
- **Description**: Retrieves patients filtered by user profile ID and role
- **Authorization**: Requires `PermissionConstants.PatientsGetAll`

### 6. Get Patient List by Search User Profile ID
- **Endpoint**: `GET /api/2025-02/get-patient-list-by-search-user-profile-id?profileId={id}&role={role}&name={name}`
- **Method**: `GetPatientListBySearchUserProfileId`
- **Description**: Searches patients by user profile ID, role, and name
- **Authorization**: Requires `PermissionConstants.PatientsGetAll`

### 7. Get Patient List Filter
- **Endpoint**: `GET /api/2025-02/get-patient-list-filter?searchTerm={term}`
- **Method**: `GetPatientListFilter`
- **Description**: Retrieves filtered list of patients by search term
- **Authorization**: Requires `PermissionConstants.PatientsGetAll`

## Service Methods Added

The following methods were added to `PatientsService`:

1. `GetByPhoneAndCode(string pCode, string pPhone)`
2. `GetByUserName(string userName)`
3. `GetByUserId(int userId)`
4. `GetAllPatients()`
5. `GetPatientListByUserProfileId(int profileId, string role)`
6. `GetPatientListBySearchUserProfileId(int profileId, string role, string name)`
7. `GetPatientListFilter(string searchTerm = "")`

## Repository Methods Added

The following methods were added to `IPatientsQueryRepository` and `PatientsQueryRepository`:

1. `GetByPhoneAndCode(string pCode, string pPhone)`
2. `GetByUserName(string userName)`
3. `GetByUserId(int userId)`
4. `GetAllPatients()`
5. `GetPatientListByUserProfileId(int profileId, string role)`
6. `GetPatientListBySearchUserProfileId(int profileId, string role, string name)`
7. `GetPatientListFilter(string searchTerm = "")`

## Required Stored Procedures

The following stored procedures need to be created in the database:

### 1. Patients_GetByPhoneAndCode
```sql
CREATE PROCEDURE [dbo].[Patients_GetByPhoneAndCode]
    @PatientCode NVARCHAR(50),
    @PhoneNo NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.PatientID,
        p.UserID,
        p.DateOfBirth,
        p.Gender,
        p.Address,
        p.BloodGroup,
        p.InsuranceProvider,
        p.InsurancePolicyNumber,
        p.PatientReferenceID,
        p.PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        u.PhoneNumber
    FROM [dbo].[Patients] p
    LEFT JOIN [dbo].[Users] u ON p.UserID = u.UserID
    WHERE p.PatientCode = @PatientCode 
        AND (u.PhoneNumber = @PhoneNo OR u.ContactNo = @PhoneNo)
        AND (p.IsDeleted = 0 OR p.IsDeleted IS NULL);
END
GO
```

### 2. Patients_GetByUserId
```sql
CREATE PROCEDURE [dbo].[Patients_GetByUserId]
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.PatientID,
        p.UserID,
        p.DateOfBirth,
        p.Gender,
        p.Address,
        p.BloodGroup,
        p.InsuranceProvider,
        p.InsurancePolicyNumber,
        p.PatientReferenceID,
        p.PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        u.PhoneNumber
    FROM [dbo].[Patients] p
    LEFT JOIN [dbo].[Users] u ON p.UserID = u.UserID
    WHERE p.UserID = @UserID
        AND (p.IsDeleted = 0 OR p.IsDeleted IS NULL);
END
GO
```

### 3. Patients_GetAllSimple
```sql
CREATE PROCEDURE [dbo].[Patients_GetAllSimple]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.PatientID,
        p.UserID,
        p.DateOfBirth,
        p.Gender,
        p.Address,
        p.BloodGroup,
        p.InsuranceProvider,
        p.InsurancePolicyNumber,
        p.PatientReferenceID,
        p.PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        u.PhoneNumber
    FROM [dbo].[Patients] p
    LEFT JOIN [dbo].[Users] u ON p.UserID = u.UserID
    WHERE (p.IsDeleted = 0 OR p.IsDeleted IS NULL)
    ORDER BY p.CreatedAt DESC;
END
GO
```

### 4. Patients_GetByUserProfileId
```sql
CREATE PROCEDURE [dbo].[Patients_GetByUserProfileId]
    @ProfileId INT,
    @Role NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    -- This procedure should filter patients based on creator profile ID and role
    -- Adjust the logic based on your actual database schema
    SELECT 
        p.PatientID,
        p.UserID,
        p.DateOfBirth,
        p.Gender,
        p.Address,
        p.BloodGroup,
        p.InsuranceProvider,
        p.InsurancePolicyNumber,
        p.PatientReferenceID,
        p.PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        u.PhoneNumber
    FROM [dbo].[Patients] p
    LEFT JOIN [dbo].[Users] u ON p.UserID = u.UserID
    WHERE (p.IsDeleted = 0 OR p.IsDeleted IS NULL)
    -- Add your filtering logic here based on ProfileId and Role
    ORDER BY p.CreatedAt DESC;
END
GO
```

### 5. Patients_GetBySearchUserProfileId
```sql
CREATE PROCEDURE [dbo].[Patients_GetBySearchUserProfileId]
    @ProfileId INT,
    @Role NVARCHAR(50),
    @Name NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    -- This procedure should search patients by profile ID, role, and name
    -- Adjust the logic based on your actual database schema
    SELECT 
        p.PatientID,
        p.UserID,
        p.DateOfBirth,
        p.Gender,
        p.Address,
        p.BloodGroup,
        p.InsuranceProvider,
        p.InsurancePolicyNumber,
        p.PatientReferenceID,
        p.PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        u.PhoneNumber
    FROM [dbo].[Patients] p
    LEFT JOIN [dbo].[Users] u ON p.UserID = u.UserID
    WHERE (p.IsDeleted = 0 OR p.IsDeleted IS NULL)
        AND (@Name IS NULL OR @Name = '' OR u.FullName LIKE '%' + @Name + '%')
    -- Add your filtering logic here based on ProfileId and Role
    ORDER BY p.CreatedAt DESC;
END
GO
```

### 6. Patients_GetFiltered
```sql
CREATE PROCEDURE [dbo].[Patients_GetFiltered]
    @SearchTerm NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        p.PatientID,
        p.UserID,
        p.DateOfBirth,
        p.Gender,
        p.Address,
        p.BloodGroup,
        p.InsuranceProvider,
        p.InsurancePolicyNumber,
        p.PatientReferenceID,
        p.PatientAge,
        p.CreatedAt,
        p.UpdatedAt,
        p.IsDeleted,
        u.PhoneNumber
    FROM [dbo].[Patients] p
    LEFT JOIN [dbo].[Users] u ON p.UserID = u.UserID
    WHERE (p.IsDeleted = 0 OR p.IsDeleted IS NULL)
        AND (@SearchTerm IS NULL OR @SearchTerm = '' 
            OR u.FullName LIKE '%' + @SearchTerm + '%'
            OR u.PhoneNumber LIKE '%' + @SearchTerm + '%'
            OR p.PatientCode LIKE '%' + @SearchTerm + '%')
    ORDER BY p.CreatedAt DESC;
END
GO
```

## Notes

1. **Route Differences**: The PrescriptionModule uses `/api/2025-02/` as the base route, while the API_LIST.md shows `/api/app/patient-profile/`. This is expected as different projects may use different API versioning strategies.

2. **Agent Functionality**: Some endpoints from the Mycompany project involve Agent-related functionality (e.g., `patient-list-by-agent-master`, `patient-list-by-agent-super-visor`). These were not implemented as the PrescriptionModule may not have the same Agent entity structure. If needed, these can be added later.

3. **User Lookup**: The `GetByUserName` method first looks up the user by username, then retrieves the patient. This requires the `User_GetByUserName` stored procedure to exist.

4. **Database Schema**: The stored procedures assume a certain database schema. Please adjust the table names, column names, and relationships based on your actual database structure.

5. **Missing Endpoints**: The following endpoints from API_LIST.md were not implemented as they may require additional entities or different business logic:
   - `patient-list-by-admin` (may require admin-specific filtering)
   - `patient-list-by-agent-master/{masterId}` (requires Agent entity)
   - `patient-list-by-agent-super-visor/{supervisorId}` (requires Agent entity)
   - `patient-list-filter-by-admin/{userId}?role={role}` (may require admin-specific filtering)
   - `doctor-list-by-creator-id-filter/{profileId}` (seems to be for doctors, not patients)
   - `doctor-list-filter` (seems to be for doctors, not patients)

## Testing

After implementing the stored procedures, test each endpoint to ensure:
1. Proper authentication and authorization
2. Correct data retrieval
3. Error handling for invalid inputs
4. Proper response formatting

## Future Enhancements

If Agent functionality is needed, consider:
1. Adding Agent entity and related tables
2. Implementing agent-related endpoints
3. Adding stored procedures for agent-based filtering

