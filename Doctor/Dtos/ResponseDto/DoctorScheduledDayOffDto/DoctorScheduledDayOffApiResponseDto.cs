using System;

namespace Doctor.Dtos.ResponseDto.DoctorScheduledDayOffDto
{
    public class DoctorScheduledDayOffApiResponseDto
    {
        public int DoctorScheduledDayOffID { get; set; }
        public int DoctorScheduleID { get; set; }
        public string? OffDay { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

