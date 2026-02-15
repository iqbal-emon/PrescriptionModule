using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorFeesSetup_DeleteById stored procedure
    /// Parameters: @DoctorFeesSetupID INT
    /// </summary>
    public class DoctorFeesSetupDeleteModel
    {
        public int DoctorFeesSetupID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorFeesSetup_Insert stored procedure
    /// Parameters: @DoctorScheduleID INT, @AppointmentType NVARCHAR(50) = NULL,
    /// @CurrentFee DECIMAL(18, 2) = NULL, @PreviousFee DECIMAL(18, 2) = NULL,
    /// @FeeAppliedFrom DATETIME = NULL, @FollowUpPeriod INT = NULL, @ReportShowPeriod INT = NULL,
    /// @Discount DECIMAL(18, 2) = NULL, @DiscountAppliedFrom DATETIME = NULL,
    /// @DiscountPeriod INT = NULL, @TotalFee DECIMAL(18, 2) = NULL, @IsActive BIT = 1,
    /// @DoctorFeesSetupID INT OUTPUT
    /// </summary>
    public class DoctorFeesSetupInsertModel
    {
        public int DoctorScheduleID { get; set; }
        public string? AppointmentType { get; set; }
        public decimal? CurrentFee { get; set; }
        public decimal? PreviousFee { get; set; }
        public DateTime? FeeAppliedFrom { get; set; }
        public int? FollowUpPeriod { get; set; }
        public int? ReportShowPeriod { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? DiscountAppliedFrom { get; set; }
        public int? DiscountPeriod { get; set; }
        public decimal? TotalFee { get; set; }
        public bool IsActive { get; set; } = true;
        public int DoctorFeesSetupID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for DoctorFeesSetup_Update stored procedure
    /// Parameters: @DoctorFeesSetupID INT, @DoctorScheduleID INT, @AppointmentType NVARCHAR(50) = NULL,
    /// @CurrentFee DECIMAL(18, 2) = NULL, @PreviousFee DECIMAL(18, 2) = NULL,
    /// @FeeAppliedFrom DATETIME = NULL, @FollowUpPeriod INT = NULL, @ReportShowPeriod INT = NULL,
    /// @Discount DECIMAL(18, 2) = NULL, @DiscountAppliedFrom DATETIME = NULL,
    /// @DiscountPeriod INT = NULL, @TotalFee DECIMAL(18, 2) = NULL, @IsActive BIT = 1,
    /// @UpdatedId INT OUTPUT
    /// </summary>
    public class DoctorFeesSetupUpdateModel
    {
        public int DoctorFeesSetupID { get; set; }
        public int DoctorScheduleID { get; set; }
        public string? AppointmentType { get; set; }
        public decimal? CurrentFee { get; set; }
        public decimal? PreviousFee { get; set; }
        public DateTime? FeeAppliedFrom { get; set; }
        public int? FollowUpPeriod { get; set; }
        public int? ReportShowPeriod { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? DiscountAppliedFrom { get; set; }
        public int? DiscountPeriod { get; set; }
        public decimal? TotalFee { get; set; }
        public bool IsActive { get; set; } = true;
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

