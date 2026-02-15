-- =============================================
-- Quick Test Script for Appointment_Insert
-- =============================================
-- Use this to quickly test the stored procedure
-- =============================================

USE [SoowGood_System_Dev_2]
GO

-- Clear any previous test data (optional - be careful!)
-- DELETE FROM [dbo].[Appointment] WHERE PatientId IN (
--     SELECT PatientID FROM [dbo].[Patients] P
--     INNER JOIN [dbo].[Users] U ON P.UserID = U.UserID
--     WHERE U.PhoneNumber = '+8801777606656'
-- );

-- =============================================
-- TEST: Execute with exact values from UI
-- =============================================
PRINT '========================================';
PRINT 'Testing Appointment_Insert';
PRINT '========================================';
PRINT '';

DECLARE @SerialNo INT;
DECLARE @ErrorNumber INT;
DECLARE @ErrorMessage NVARCHAR(4000);

BEGIN TRY
    -- Execute stored procedure
    EXEC [dbo].[Appointment_Insert]
        @PatientName = N'Md Zakir Hossain Zakir',
        @PhoneNumber = N'+8801777606656',
        @Gender = N'Male',
        @BloodGroup = N'O+',
        @Age = 52,
        @SessionId = 11,
        @ScheduleId = 11,
        @DoctorProfileId = 1,
        @AppointmentDate = '2026-02-16 12:10:00';
    
    PRINT '✅ Stored procedure executed successfully';
    PRINT 'Check the result set above for SerialNo';
    
END TRY
BEGIN CATCH
    SET @ErrorNumber = ERROR_NUMBER();
    SET @ErrorMessage = ERROR_MESSAGE();
    
    PRINT '❌ Error occurred:';
    PRINT '   Error Number: ' + CAST(@ErrorNumber AS VARCHAR(10));
    PRINT '   Error Message: ' + @ErrorMessage;
    PRINT '   Error Line: ' + CAST(ERROR_LINE() AS VARCHAR(10));
END CATCH

PRINT '';
PRINT '========================================';
PRINT 'Verification: Check created appointment';
PRINT '========================================';

SELECT TOP 5
    A.Id AS AppointmentId,
    A.SerialNo,
    A.AppointmentDate,
    A.SessionId,
    A.ScheduleId,
    A.DoctorProfileId,
    U.FirstName + ' ' + U.LastName AS PatientName,
    U.PhoneNumber,
    P.PatientAge AS Age,
    P.Gender,
    P.BloodGroup,
    A.CreatedAt
FROM [dbo].[Appointment] A
LEFT JOIN [dbo].[Patients] P ON A.PatientId = P.PatientID
LEFT JOIN [dbo].[Users] U ON P.UserID = U.UserID
WHERE A.IsDeleted = 0
ORDER BY A.CreatedAt DESC;

GO

