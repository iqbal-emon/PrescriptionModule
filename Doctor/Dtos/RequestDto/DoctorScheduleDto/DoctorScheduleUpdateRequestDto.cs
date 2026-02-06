using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorScheduleDto
{
    public class DoctorScheduleUpdateRequestDto
    {
        public int? DoctorScheduleID { get; set; }

        public int? DoctorID { get; set; }

        public int? ScheduleID { get; set; }

        public int? TenantID { get; set; }
    }
}

