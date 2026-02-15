using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorSchedule_DeleteById stored procedure
    /// Parameters: @DoctorScheduleID INT
    /// </summary>
    public class DoctorScheduleDeleteModel
    {
        public int DoctorScheduleID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorSchedule_Insert stored procedure
    /// Parameters: @DoctorScheduleID INT, @TenantID INT, @DoctorID INT, @ScheduleID INT,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = 0
    /// Note: Uses SELECT SCOPE_IDENTITY() AS DoctorScheduleID
    /// </summary>
    public class DoctorScheduleInsertModel
    {
        public int DoctorScheduleID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public int ScheduleID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    /// <summary>
    /// Database model for DoctorSchedule_Update stored procedure
    /// Parameters: @DoctorScheduleID INT, @TenantID INT = NULL, @DoctorID INT = NULL,
    /// @ScheduleID INT = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @CreatedAt DATETIME = NULL
    /// Note: Uses SELECT @DoctorScheduleID AS UpdatedDoctorScheduleID
    /// </summary>
    public class DoctorScheduleUpdateModel
    {
        public int DoctorScheduleID { get; set; }
        public int? TenantID { get; set; }
        public int? DoctorID { get; set; }
        public int? ScheduleID { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

