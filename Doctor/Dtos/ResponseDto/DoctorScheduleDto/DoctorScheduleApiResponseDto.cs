using System;
using System.Collections.Generic;
using Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto;

namespace Doctor.Dtos.ResponseDto.DoctorScheduleDto
{
    public class DoctorScheduleApiResponseDto
    {
        public int DoctorScheduleID { get; set; }
        public int DoctorID { get; set; }
        public int ScheduleID { get; set; }
        public int TenantID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<DoctorScheduleDaySessionApiResponseDto> DoctorScheduleDaySession { get; set; } = new List<DoctorScheduleDaySessionApiResponseDto>();
    }
}

