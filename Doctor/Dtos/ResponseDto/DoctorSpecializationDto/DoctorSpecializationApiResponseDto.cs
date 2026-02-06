using System;

namespace Doctor.Dtos.ResponseDto.DoctorSpecializationDto
{
    public class DoctorSpecializationApiResponseDto
    {
        public int DoctorSpecializationID { get; set; }
        public int DoctorID { get; set; }
        public int? SpecialityID { get; set; }
        public int? SpecializationID { get; set; }
        public string? ServiceDetails { get; set; }
        public string? DocumentName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

