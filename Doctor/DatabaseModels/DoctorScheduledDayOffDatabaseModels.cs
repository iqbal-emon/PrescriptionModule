namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorScheduledDayOff_DeleteById stored procedure
    /// Parameters: @DoctorScheduledDayOffID INT
    /// </summary>
    public class DoctorScheduledDayOffDeleteModel
    {
        public int DoctorScheduledDayOffID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorScheduledDayOff_Insert stored procedure
    /// Parameters: @DoctorScheduleID INT, @OffDay NVARCHAR(50) = NULL, @IsActive BIT = 1,
    /// @DoctorScheduledDayOffID INT OUTPUT
    /// </summary>
    public class DoctorScheduledDayOffInsertModel
    {
        public int DoctorScheduleID { get; set; }
        public string? OffDay { get; set; }
        public bool IsActive { get; set; } = true;
        public int DoctorScheduledDayOffID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for DoctorScheduledDayOff_Update stored procedure
    /// Parameters: @DoctorScheduledDayOffID INT, @DoctorScheduleID INT, @OffDay NVARCHAR(50) = NULL,
    /// @IsActive BIT = 1, @UpdatedId INT OUTPUT
    /// </summary>
    public class DoctorScheduledDayOffUpdateModel
    {
        public int DoctorScheduledDayOffID { get; set; }
        public int DoctorScheduleID { get; set; }
        public string? OffDay { get; set; }
        public bool IsActive { get; set; } = true;
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

