using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorScheduleDaySessionDto
{
    public class DoctorScheduleDaySessionInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor Schedule ID is required.")]
        public int DoctorScheduleID { get; set; }

        [MaxLength(50, ErrorMessage = "Schedule day of week cannot exceed 50 characters.")]
        public string? ScheduleDayofWeek { get; set; }

        [MaxLength(20, ErrorMessage = "Start time cannot exceed 20 characters.")]
        public string? StartTime { get; set; }

        [MaxLength(20, ErrorMessage = "End time cannot exceed 20 characters.")]
        public string? EndTime { get; set; }

        public int? NoOfPatients { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

