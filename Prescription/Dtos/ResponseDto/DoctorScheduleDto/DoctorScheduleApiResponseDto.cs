using System;

namespace Prescription.Dtos.ResponseDto.DoctorScheduleDto
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
    }
}
