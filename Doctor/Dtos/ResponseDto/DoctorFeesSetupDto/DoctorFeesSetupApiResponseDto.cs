using System;

namespace Doctor.Dtos.ResponseDto.DoctorFeesSetupDto
{
    public class DoctorFeesSetupApiResponseDto
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
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

