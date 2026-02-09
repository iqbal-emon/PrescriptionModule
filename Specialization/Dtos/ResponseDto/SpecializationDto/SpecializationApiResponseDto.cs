using System;

namespace Specialization.Dtos.ResponseDto.SpecializationDto
{
    public class SpecializationApiResponseDto
    {
        public int SpecializationID { get; set; }
        public int? SpecialityID { get; set; }
        public string SpecializationName { get; set; }
        public string? Description { get; set; }
        public int TenantID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

