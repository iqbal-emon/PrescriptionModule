USE [SoowGood_System_Dev_2]
GO
/****** Object:  StoredProcedure [dbo].[Appointment_Insert]    Script Date: 2/15/2026 12:38:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
    @PatientRoleId INT = NULL  -- Optional: RoleId for Patient user. If NULL, will query for default Patient role
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @NewUserID INT = NULL,
        @NewPatientID INT = NULL,
        @NewAppointmentID INT,
        @PatientCode VARCHAR(10),
        @SerialNo INT,
        @PatientRoleIdValue INT = NULL,
        @UserName NVARCHAR(100);

    BEGIN TRY
        BEGIN TRANSACTION;

        ----------------------------------------------------------------------
        -- VALIDATE INPUT PARAMETERS
        ----------------------------------------------------------------------
        IF @PatientName IS NULL OR LEN(LTRIM(RTRIM(@PatientName))) = 0
        BEGIN
            RAISERROR('PatientName is required and cannot be empty', 16, 1);
            RETURN;
        END

        IF @PhoneNumber IS NULL OR LEN(LTRIM(RTRIM(@PhoneNumber))) = 0
        BEGIN
            RAISERROR('PhoneNumber is required and cannot be empty', 16, 1);
            RETURN;
        END

        IF @Gender IS NULL OR LEN(LTRIM(RTRIM(@Gender))) = 0
        BEGIN
            RAISERROR('Gender is required and cannot be empty', 16, 1);
            RETURN;
        END

        IF @Age IS NULL OR @Age < 0 OR @Age > 120
        BEGIN
            RAISERROR('Age must be between 0 and 120', 16, 1);
            RETURN;
        END

        IF @SessionId IS NULL OR @SessionId <= 0
        BEGIN
            RAISERROR('SessionId is required and must be greater than 0', 16, 1);
            RETURN;
        END

        IF @ScheduleId IS NULL OR @ScheduleId <= 0
        BEGIN
            RAISERROR('ScheduleId is required and must be greater than 0', 16, 1);
            RETURN;
        END

        IF @DoctorProfileId IS NULL OR @DoctorProfileId <= 0
        BEGIN
            RAISERROR('DoctorProfileId is required and must be greater than 0', 16, 1);
            RETURN;
        END

        IF @AppointmentDate IS NULL
        BEGIN
            RAISERROR('AppointmentDate is required', 16, 1);
            RETURN;
        END

        ----------------------------------------------------------------------
        -- 1. CHECK EXISTING PATIENT BY PHONE NUMBER
        ----------------------------------------------------------------------
        SELECT TOP 1
            @NewPatientID = p.PatientID,
            @NewUserID = u.UserID
        FROM [dbo].[Patients] p
        INNER JOIN [dbo].[Users] u ON p.UserID = u.UserID
        WHERE u.PhoneNumber = @PhoneNumber
          AND u.IsDeleted = 0
          AND p.IsDeleted = 0;


        ----------------------------------------------------------------------
        -- 2. IF NEW → INSERT USER & PATIENT
        ----------------------------------------------------------------------
        IF @NewPatientID IS NULL
        BEGIN
            -- Generate Patient Code
            SET @PatientCode = 'PA' + UPPER(SUBSTRING(REPLACE(NEWID(), '-', ''), 1, 6));

            -- Get Patient Role ID (if not provided, query for default Patient role or use first active role)
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

            -------------------------
            -- Insert into Users
            -------------------------
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

            SET @NewUserID = SCOPE_IDENTITY();

            -- Validate User was created
            IF @NewUserID IS NULL OR @NewUserID <= 0
            BEGIN
                RAISERROR('Failed to create User. SCOPE_IDENTITY() returned NULL or invalid value.', 16, 1);
                RETURN;
            END

            -------------------------
            -- Insert into Patients
            -------------------------
            INSERT INTO [dbo].[Patients]
            (
                UserID, DateOfBirth, Gender, Address, BloodGroup,
                InsuranceProvider, InsurancePolicyNumber, CreatedAt, UpdatedAt,
                IsDeleted, PatientReferenceID, PatientAge, PatientCode
            )
            VALUES
            (
                @NewUserID,
                NULL,
                LTRIM(RTRIM(@Gender)),
                NULL,
                CASE WHEN @BloodGroup IS NULL OR LEN(LTRIM(RTRIM(@BloodGroup))) = 0 THEN NULL ELSE LTRIM(RTRIM(@BloodGroup)) END,
                NULL,
                NULL,
                GETUTCDATE(),
                GETUTCDATE(),
                0,
                NULL,
                @Age,
                @PatientCode
            );

            SET @NewPatientID = SCOPE_IDENTITY();

            -- Validate Patient was created
            IF @NewPatientID IS NULL OR @NewPatientID <= 0
            BEGIN
                RAISERROR('Failed to create Patient. SCOPE_IDENTITY() returned NULL or invalid value.', 16, 1);
                RETURN;
            END
        END

        -- Final validation: Ensure we have a valid PatientID
        IF @NewPatientID IS NULL OR @NewPatientID <= 0
        BEGIN
            RAISERROR('PatientID is NULL or invalid. Cannot create appointment without a valid patient.', 16, 1);
            RETURN;
        END

        ----------------------------------------------------------------------
        -- 3. GET NEXT SERIAL NUMBER
        ----------------------------------------------------------------------
        SELECT @SerialNo = ISNULL(MAX(SerialNo), 0) + 1
        FROM [dbo].[Appointment]
        WHERE SessionId = @SessionId
          AND ScheduleId = @ScheduleId
          AND CAST(AppointmentDate AS DATE) = CAST(@AppointmentDate AS DATE)
          AND IsDeleted = 0;

        -- Ensure SerialNo is valid
        IF @SerialNo IS NULL OR @SerialNo <= 0
        BEGIN
            SET @SerialNo = 1;
        END

        ----------------------------------------------------------------------
        -- 4. INSERT APPOINTMENT
        ----------------------------------------------------------------------
        INSERT INTO [dbo].[Appointment]
        (
            SessionId,
            ScheduleId,
            IsDeleted,
            CreatedAt,
            UpdatedAt,
            PatientId,
            AppointmentDate,
            SerialNo,
            DoctorProfileId
        )
        VALUES
        (
            @SessionId,
            @ScheduleId,
            0,
            GETUTCDATE(),
            GETUTCDATE(),
            @NewPatientID,
            @AppointmentDate,
            @SerialNo,
            @DoctorProfileId
        );

        SET @NewAppointmentID = SCOPE_IDENTITY();

        -- Validate Appointment was created
        IF @NewAppointmentID IS NULL OR @NewAppointmentID <= 0
        BEGIN
            RAISERROR('Failed to create Appointment. SCOPE_IDENTITY() returned NULL or invalid value.', 16, 1);
            RETURN;
        END

        ----------------------------------------------------------------------
        -- 5. COMMIT + RETURN SERIAL NUMBER
        ----------------------------------------------------------------------
        COMMIT TRANSACTION;

        SELECT @SerialNo AS SerialNo;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 
            ROLLBACK TRANSACTION;

        DECLARE @ErrorNumber INT = ERROR_NUMBER();
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        DECLARE @ErrorLine INT = ERROR_LINE();
        DECLARE @ErrorProcedure NVARCHAR(128) = ERROR_PROCEDURE();

        -- Return detailed error information
        SELECT 
            @ErrorNumber AS ErrorNumber, 
            @ErrorMessage AS ErrorMessage,
            @ErrorSeverity AS ErrorSeverity,
            @ErrorState AS ErrorState,
            @ErrorLine AS ErrorLine,
            @ErrorProcedure AS ErrorProcedure,
            -- Additional debug info
            @NewUserID AS NewUserID,
            @NewPatientID AS NewPatientID,
            @NewAppointmentID AS NewAppointmentID,
            @SerialNo AS SerialNo;

        -- Optionally re-throw the error (comment out if you want to return error info instead)
        -- RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

