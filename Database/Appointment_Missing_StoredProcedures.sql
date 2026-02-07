USE [SoowGood_System]
GO

/****** Object:  StoredProcedure [dbo].[Appointment_Update]    Script Date: 2/7/2026 3:08:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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
    
    -- Update only if appointment exists and is not deleted
    UPDATE [dbo].[Appointment]
    SET 
        SessionId = CASE WHEN @SessionId IS NOT NULL THEN @SessionId ELSE SessionId END,
        ScheduleId = CASE WHEN @ScheduleId IS NOT NULL THEN @ScheduleId ELSE ScheduleId END,
        PatientId = CASE WHEN @PatientId IS NOT NULL THEN @PatientId ELSE PatientId END,
        AppointmentDate = CASE WHEN @AppointmentDate IS NOT NULL THEN @AppointmentDate ELSE AppointmentDate END,
        DoctorProfileId = CASE WHEN @DoctorProfileId IS NOT NULL THEN @DoctorProfileId ELSE DoctorProfileId END,
        UpdatedAt = GETUTCDATE()
    WHERE Id = @Id AND IsDeleted = 0;
    
    -- Check if the update was successful
    IF @@ROWCOUNT > 0
    BEGIN
        SELECT @Id AS UpdatedAppointmentId;
    END
    ELSE
    BEGIN
        SELECT NULL AS UpdatedAppointmentId;  -- Return NULL if no update happened
    END
END
GO

/****** Object:  StoredProcedure [dbo].[Appointment_DeleteById]    Script Date: 2/7/2026 3:08:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Appointment_DeleteById]
    @AppointmentId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Soft delete: Update only if appointment exists and is not already deleted
    UPDATE [dbo].[Appointment]
    SET 
        IsDeleted = 1,
        UpdatedAt = GETUTCDATE()
    WHERE Id = @AppointmentId AND IsDeleted = 0;
    
    -- Check if the update was successful
    IF @@ROWCOUNT > 0
    BEGIN
        SELECT @AppointmentId AS DeletedAppointmentId;
    END
    ELSE
    BEGIN
        SELECT NULL AS DeletedAppointmentId;  -- Return NULL if no update happened
    END
END
GO

