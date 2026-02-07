using System;

namespace Speciality.Dtos.ResponseDto.SpecialityDto
{
    public class SpecialityApiResponseDto
    {
        public int SpecialityID { get; set; }
        public int TenantID { get; set; }
        public string SpecialityName { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

