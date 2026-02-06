using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorSpecializationDto
{
    public class DoctorSpecializationInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        public int? SpecialityID { get; set; }

        public int? SpecializationID { get; set; }

        [MaxLength(500, ErrorMessage = "Service details cannot exceed 500 characters.")]
        public string? ServiceDetails { get; set; }

        [MaxLength(200, ErrorMessage = "Document name cannot exceed 200 characters.")]
        public string? DocumentName { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

