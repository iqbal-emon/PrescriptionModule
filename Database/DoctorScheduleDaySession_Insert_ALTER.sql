-- =============================================
-- Updated Stored Procedure: DoctorScheduleDaySession_Insert
-- Includes all fields from entity and DTO
-- Note: TenantID is in DTO but not in current table structure
-- =============================================

ALTER PROCEDURE [dbo].[DoctorScheduleDaySession_Insert]
    @DoctorScheduleID INT,
    @ScheduleDayofWeek NVARCHAR(50) = NULL,
    @StartTime NVARCHAR(20) = NULL,
    @EndTime NVARCHAR(20) = NULL,
    @NoOfPatients INT = NULL,
    @IsActive BIT = 1,
    @TenantID INT = NULL,  -- Added for DTO compatibility (not in current table structure)
    @DoctorScheduleDaySessionID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
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
        -- Note: TenantID is not in the current table structure
        -- If table is updated to include TenantID, uncomment the line below:
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
        -- Note: TenantID is not in the current table structure
        -- If table is updated to include TenantID, uncomment the line below:
        -- ,@TenantID
    );
    
    SET @DoctorScheduleDaySessionID = SCOPE_IDENTITY();
END
GO

