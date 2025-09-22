using System;

namespace Prescription.Dtos.RequestDto.ExpertiseCategoryDto
{
    public class ExpertiseCategoryApiResponseDto
    {
        public int ExpertiseID { get; set; }
        public string? ExpertiseName { get; set; }
        public int TenantID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
