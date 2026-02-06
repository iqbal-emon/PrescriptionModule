using System;

namespace Doctor.Dtos.ResponseDto.DoctorExpertiseDto
{
    public class DoctorExpertiseApiResponseDto
    {
        public int DoctorExpertiseID { get; set; }
        public int DoctorID { get; set; }
        public int ExpertiseID { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Certification { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

