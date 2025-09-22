using System;

namespace Degree.Dtos.ResponseDto.DegreeDto
{
    public class DegreeApiResponseDto
    {
        public int DegreeID { get; set; }
        public int TenantID { get; set; }
        public string DegreeName { get; set; }
        public int Duration { get; set; }
        public string? DurationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
