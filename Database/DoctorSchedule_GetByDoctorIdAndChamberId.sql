/****** Object:  StoredProcedure [dbo].[DoctorSchedule_GetByDoctorIdAndChamberId]    Script Date: 2/13/2026 5:43:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		System Generated
-- Create date: 2026-02-13
-- Description:	Get DoctorSchedule records by DoctorID and ChamberID
-- Note: Currently, DoctorSchedule table doesn't have ChamberID column.
--       This procedure returns all schedules for the doctor.
--       Chamber filtering should be done in application layer or 
--       update this procedure when ChamberID column is added to DoctorSchedule table.
-- =============================================
CREATE PROCEDURE [dbo].[DoctorSchedule_GetByDoctorIdAndChamberId]
    @DoctorID INT,
    @ChamberID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get schedules for a specific doctor
    -- TODO: Add ChamberID filter when DoctorSchedule table is updated with ChamberID column
    -- For now, returning all schedules for the doctor (chamber filtering in application layer)
    SELECT 
        [DoctorScheduleID],
        [DoctorID],
        [ScheduleID],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorSchedule]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
    
    -- Future implementation when ChamberID is added:
    -- WHERE [DoctorID] = @DoctorID
    --     AND [ChamberID] = @ChamberID  -- or [DoctorChamberID] = @ChamberID
    --     AND [IsDeleted] = 0
END
GO

