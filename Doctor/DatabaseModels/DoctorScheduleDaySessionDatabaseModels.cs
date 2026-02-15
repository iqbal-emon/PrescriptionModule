using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorScheduleDaySession_DeleteById stored procedure
    /// Parameters: @DoctorScheduleDaySessionID INT
    /// </summary>
    public class DoctorScheduleDaySessionDeleteModel
    {
        public int DoctorScheduleDaySessionID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorScheduleDaySession_Insert stored procedure
    /// Parameters: @DoctorScheduleID INT, @ScheduleDayofWeek NVARCHAR(50) = NULL,
    /// @StartTime NVARCHAR(20) = NULL, @EndTime NVARCHAR(20) = NULL, @NoOfPatients INT = NULL,
    /// @IsActive BIT = 1, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = 0, @TenantID INT = NULL, @DoctorScheduleDaySessionID INT OUTPUT
    /// </summary>
    public class DoctorScheduleDaySessionInsertModel
    {
        public int DoctorScheduleID { get; set; }
        public string? ScheduleDayofWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? NoOfPatients { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? TenantID { get; set; }
        public int DoctorScheduleDaySessionID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for DoctorScheduleDaySession_Update stored procedure
    /// Parameters: @DoctorScheduleDaySessionID INT, @DoctorScheduleID INT = NULL,
    /// @ScheduleDayofWeek NVARCHAR(50) = NULL, @StartTime NVARCHAR(20) = NULL,
    /// @EndTime NVARCHAR(20) = NULL, @NoOfPatients INT = NULL, @IsActive BIT = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @TenantID INT = NULL, @UpdatedId INT OUTPUT
    /// </summary>
    public class DoctorScheduleDaySessionUpdateModel
    {
        public int DoctorScheduleDaySessionID { get; set; }
        public int? DoctorScheduleID { get; set; }
        public string? ScheduleDayofWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? NoOfPatients { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public int? TenantID { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

