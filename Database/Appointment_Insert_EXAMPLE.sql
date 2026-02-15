-- =============================================
-- Example: Execute Appointment_Insert Stored Procedure
-- =============================================
-- This example uses the actual values from the UI
-- =============================================

USE [SoowGood_System_Dev_2]
GO

-- Example 1: Execute with values from the image
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'Md Zakir Hossain Zakir',
    @PhoneNumber = N'+8801777606656',
    @Gender = N'Male',
    @BloodGroup = N'O+',
    @Age = 52,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 12:10:00'
GO

-- =============================================
-- Example 2: Execute with different patient (new patient)
-- =============================================
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'John Doe',
    @PhoneNumber = N'01512345678',
    @Gender = N'Male',
    @BloodGroup = N'A+',
    @Age = 35,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 14:00:00'
GO

-- =============================================
-- Example 3: Execute with existing patient (by phone number)
-- =============================================
-- If patient with phone '+8801777606656' already exists,
-- it will use existing patient instead of creating new one
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'Md Zakir Hossain Zakir',
    @PhoneNumber = N'+8801777606656',  -- Same phone number
    @Gender = N'Male',
    @BloodGroup = N'O+',
    @Age = 52,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 15:00:00'
GO

-- =============================================
-- Example 4: Test with minimal required data
-- =============================================
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'Test Patient',
    @PhoneNumber = N'01987654321',
    @Gender = N'Female',
    @BloodGroup = N'',  -- Empty blood group (will be NULL)
    @Age = 25,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 16:00:00'
GO

-- =============================================
-- Example 5: Check what the stored procedure returns
-- =============================================
-- The stored procedure returns SerialNo on success
-- Example result: SerialNo = 1, 2, 3, etc.

DECLARE @ResultSerialNo INT;

EXEC @ResultSerialNo = [dbo].[Appointment_Insert]
    @PatientName = N'Md Zakir Hossain Zakir',
    @PhoneNumber = N'+8801777606656',
    @Gender = N'Male',
    @BloodGroup = N'O+',
    @Age = 52,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 12:10:00';

SELECT @ResultSerialNo AS ReturnedSerialNo;
GO

-- =============================================
-- Example 6: Test error handling (invalid data)
-- =============================================
-- This should return an error
EXEC [dbo].[Appointment_Insert]
    @PatientName = N'',  -- Empty name (should fail validation)
    @PhoneNumber = N'01572772606',
    @Gender = N'Male',
    @BloodGroup = N'O+',
    @Age = 52,
    @SessionId = 11,
    @ScheduleId = 11,
    @DoctorProfileId = 1,
    @AppointmentDate = '2026-02-16 12:10:00'
GO

-- =============================================
-- Verify the appointment was created
-- =============================================
SELECT TOP 10
    A.Id AS AppointmentId,
    A.SerialNo,
    A.AppointmentDate,
    A.SessionId,
    A.ScheduleId,
    A.DoctorProfileId,
    P.PatientID,
    U.FirstName + ' ' + U.LastName AS PatientName,
    U.PhoneNumber,
    P.PatientAge AS Age,
    P.Gender,
    P.BloodGroup
FROM [dbo].[Appointment] A
LEFT JOIN [dbo].[Patients] P ON A.PatientId = P.PatientID
LEFT JOIN [dbo].[Users] U ON P.UserID = U.UserID
WHERE A.IsDeleted = 0
  AND U.PhoneNumber = '+8801777606656'  -- Filter by phone number from example
ORDER BY A.CreatedAt DESC;
GO

-- =============================================
-- Check patient was created/found
-- =============================================
SELECT 
    P.PatientID,
    P.PatientCode,
    P.PatientAge,
    P.Gender,
    P.BloodGroup,
    U.FirstName,
    U.LastName,
    U.PhoneNumber,
    U.Email
FROM [dbo].[Patients] P
INNER JOIN [dbo].[Users] U ON P.UserID = U.UserID
WHERE U.PhoneNumber = '+8801777606656'
  AND P.IsDeleted = 0
  AND U.IsDeleted = 0;
GO

