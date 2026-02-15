-- =============================================
-- QUICK TEST: Execute Appointment_Insert
-- =============================================
-- Copy and paste this into SQL Server Management Studio
-- =============================================

USE [SoowGood_System_Dev_2]
GO

-- Execute with values from your UI
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

-- Expected Result:
-- Returns: SerialNo (e.g., 1, 2, 3, etc.)
-- 
-- If error occurs, you'll see:
-- ErrorNumber | ErrorMessage | ErrorLine | etc.

