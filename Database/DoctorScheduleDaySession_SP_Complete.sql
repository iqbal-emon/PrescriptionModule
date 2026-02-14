-- =============================================
-- Complete Updated Stored Procedures
-- DoctorScheduleDaySession_Insert and Update
-- All entity fields included as parameters
-- =============================================

-- =============================================
-- INSERT Stored Procedure
-- =============================================
ALTER PROCEDURE [dbo].[DoctorScheduleDaySession_Insert]
    @DoctorScheduleID INT,
    @ScheduleDayofWeek NVARCHAR(50) = NULL,
    @StartTime NVARCHAR(20) = NULL,
    @EndTime NVARCHAR(20) = NULL,
    @NoOfPatients INT = NULL,
    @IsActive BIT = 1,
    @CreatedAt DATETIME = NULL,  -- Optional: if provided, use it; otherwise use GETDATE()
    @UpdatedAt DATETIME = NULL,  -- Optional: if provided, use it; otherwise use GETDATE()
    @IsDeleted BIT = 0,  -- Optional: if provided, use it; otherwise use 0 (false)
    @TenantID INT = NULL,  -- Added for DTO compatibility (currently not in table structure)
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
        -- Note: TenantID parameter is accepted but not inserted since table doesn't have this column
        -- If table is updated to include TenantID column, uncomment below:
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
        ISNULL(@CreatedAt, GETDATE()),  -- Use provided value or GETDATE()
        ISNULL(@UpdatedAt, GETDATE()),  -- Use provided value or GETDATE()
        ISNULL(@IsDeleted, 0)  -- Use provided value or 0 (false)
        -- Note: TenantID parameter is accepted but not inserted since table doesn't have this column
        -- If table is updated to include TenantID column, uncomment below:
        -- ,@TenantID
    );
    SET @DoctorScheduleDaySessionID = SCOPE_IDENTITY();
END
GO

-- =============================================
-- UPDATE Stored Procedure
-- =============================================
ALTER PROCEDURE [dbo].[DoctorScheduleDaySession_Update]
    @DoctorScheduleDaySessionID INT,
    @DoctorScheduleID INT = NULL,  -- Optional: only update if provided
    @ScheduleDayofWeek NVARCHAR(50) = NULL,
    @StartTime NVARCHAR(20) = NULL,
    @EndTime NVARCHAR(20) = NULL,
    @NoOfPatients INT = NULL,
    @IsActive BIT = NULL,  -- Optional: only update if provided
    @CreatedAt DATETIME = NULL,  -- Optional: typically not updated, but included for completeness
    @UpdatedAt DATETIME = NULL,  -- Optional: if provided, use it; otherwise use GETDATE()
    @IsDeleted BIT = NULL,  -- Optional: if provided, use it for soft delete
    @TenantID INT = NULL,  -- Added for DTO compatibility (currently not in table structure)
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorScheduleDaySession]
    SET 
        [DoctorScheduleID] = ISNULL(@DoctorScheduleID, [DoctorScheduleID]),  -- Only update if provided
        [ScheduleDayofWeek] = ISNULL(@ScheduleDayofWeek, [ScheduleDayofWeek]),
        [StartTime] = ISNULL(@StartTime, [StartTime]),
        [EndTime] = ISNULL(@EndTime, [EndTime]),
        [NoOfPatients] = ISNULL(@NoOfPatients, [NoOfPatients]),
        [IsActive] = ISNULL(@IsActive, [IsActive]),  -- Only update if provided
        [CreatedAt] = ISNULL(@CreatedAt, [CreatedAt]),  -- Only update if provided (typically not updated)
        [UpdatedAt] = ISNULL(@UpdatedAt, GETDATE()),  -- Use provided value or GETDATE()
        [IsDeleted] = ISNULL(@IsDeleted, [IsDeleted])  -- Only update if provided (for soft delete)
        -- Note: TenantID parameter is accepted but not updated since table doesn't have this column
        -- If table is updated to include TenantID column, uncomment below:
        -- ,[TenantID] = ISNULL(@TenantID, [TenantID])
    WHERE [DoctorScheduleDaySessionID] = @DoctorScheduleDaySessionID
        AND ([IsDeleted] = 0 OR @IsDeleted IS NOT NULL);  -- Allow update even if deleted if explicitly setting IsDeleted
    SET @UpdatedId = @DoctorScheduleDaySessionID;
END
GO

