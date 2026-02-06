using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorSpecializationDto
{
    public class DoctorSpecializationUpdateRequestDto
    {
        public int? DoctorSpecializationID { get; set; }

        public int? DoctorID { get; set; }

        public int? SpecialityID { get; set; }

        public int? SpecializationID { get; set; }

        [MaxLength(500, ErrorMessage = "Service details cannot exceed 500 characters.")]
        public string? ServiceDetails { get; set; }

        [MaxLength(200, ErrorMessage = "Document name cannot exceed 200 characters.")]
        public string? DocumentName { get; set; }
    }
}

