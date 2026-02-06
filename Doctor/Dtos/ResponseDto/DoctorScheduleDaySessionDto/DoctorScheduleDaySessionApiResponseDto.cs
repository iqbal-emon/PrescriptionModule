using System;

namespace Doctor.Dtos.ResponseDto.DoctorScheduleDaySessionDto
{
    public class DoctorScheduleDaySessionApiResponseDto
    {
        public int DoctorScheduleDaySessionID { get; set; }
        public int DoctorScheduleID { get; set; }
        public string? ScheduleDayofWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? NoOfPatients { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

