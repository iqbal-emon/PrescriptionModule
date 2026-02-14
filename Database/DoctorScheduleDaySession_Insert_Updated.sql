-- Updated Stored Procedure: DoctorScheduleDaySession_Insert
-- Includes all fields from the entity and DTO

ALTER PROCEDURE [dbo].[DoctorScheduleDaySession_Insert]
    @DoctorScheduleID INT,
    @ScheduleDayofWeek NVARCHAR(50) = NULL,
    @StartTime NVARCHAR(20) = NULL,
    @EndTime NVARCHAR(20) = NULL,
    @NoOfPatients INT = NULL,
    @IsActive BIT = 1,
    @TenantID INT = NULL,  -- Added if table has TenantID column
    @DoctorScheduleDaySessionID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if TenantID column exists in the table
    -- If the table doesn't have TenantID, remove @TenantID parameter and TenantID from INSERT
    
    INSERT INTO [dbo].[DoctorScheduleDaySession]
    (
        [DoctorScheduleID],
        [ScheduleDayofWeek],
        [StartTime],
        [EndTime],
        [NoOfPatients],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
        -- Add TenantID here if the table has this column:
        -- ,[TenantID]
    )
    VALUES
    (
        @DoctorScheduleID,
        @ScheduleDayofWeek,
        @StartTime,
        @EndTime,
        @NoOfPatients,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
        -- Add @TenantID here if the table has TenantID column:
        -- ,@TenantID
    );
    
    SET @DoctorScheduleDaySessionID = SCOPE_IDENTITY();
END
GO

