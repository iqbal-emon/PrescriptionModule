using System;
using System.ComponentModel.DataAnnotations;

namespace Prescription.Dtos.RequestDto.DoctorExpertiseDto
{
    public class DoctorExpertiseInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Expertise ID is required.")]
        public int ExpertiseID { get; set; }

        public int? ExperienceYears { get; set; }

        public string? Certification { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }

    }
}
