using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorScheduleDto
{
    public class DoctorScheduleInputRequestDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Doctor Profile ID is required.")]
        public int DoctorProfileId { get; set; }

        public int? DoctorChamberId { get; set; }

        [MaxLength(150, ErrorMessage = "Schedule name cannot exceed 150 characters.")]
        public string? ScheduleName { get; set; }

        public int? ScheduleType { get; set; }

        public int? ConsultancyType { get; set; }

        public bool IsActive { get; set; } = true;

        public string? OffDayFrom { get; set; }

        public string? OffDayTo { get; set; }

        public List<DoctorScheduleDaySessionInputDto> DoctorScheduleDaySession { get; set; } = new List<DoctorScheduleDaySessionInputDto>();

        public List<object> DoctorFeesSetup { get; set; } = new List<object>();

        public int TenantID { get; set; } = 0; // Default, should be set from context
    }

    public class DoctorScheduleDaySessionInputDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Schedule day of week is required.")]
        [MaxLength(50, ErrorMessage = "Schedule day of week cannot exceed 50 characters.")]
        public string ScheduleDayofWeek { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [MaxLength(20, ErrorMessage = "Start time cannot exceed 20 characters.")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [MaxLength(20, ErrorMessage = "End time cannot exceed 20 characters.")]
        public string EndTime { get; set; }

        public int? NoOfPatients { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

