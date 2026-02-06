using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorFeesSetupDto
{
    public class DoctorFeesSetupUpdateRequestDto
    {
        public int? DoctorFeesSetupID { get; set; }

        public int? DoctorScheduleID { get; set; }

        [MaxLength(50, ErrorMessage = "Appointment type cannot exceed 50 characters.")]
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

        public bool? IsActive { get; set; }
    }
}

