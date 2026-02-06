using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorScheduledDayOffDto
{
    public class DoctorScheduledDayOffUpdateRequestDto
    {
        public int? DoctorScheduledDayOffID { get; set; }

        public int? DoctorScheduleID { get; set; }

        [MaxLength(50, ErrorMessage = "Off day cannot exceed 50 characters.")]
        public string? OffDay { get; set; }

        public bool? IsActive { get; set; }
    }
}

