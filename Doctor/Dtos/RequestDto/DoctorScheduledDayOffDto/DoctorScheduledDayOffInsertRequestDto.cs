using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorScheduledDayOffDto
{
    public class DoctorScheduledDayOffInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor Schedule ID is required.")]
        public int DoctorScheduleID { get; set; }

        [MaxLength(50, ErrorMessage = "Off day cannot exceed 50 characters.")]
        public string? OffDay { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

