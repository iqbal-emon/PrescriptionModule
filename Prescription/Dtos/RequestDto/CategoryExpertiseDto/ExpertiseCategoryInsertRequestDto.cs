using System;
using System.ComponentModel.DataAnnotations;

namespace Prescription.Dtos.RequestDto.ExpertiseCategoryDto
{
    public class ExpertiseCategoryInsertRequestDto
    {
        public string? ExpertiseName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int? TenantID { get; set; }

    }
}
