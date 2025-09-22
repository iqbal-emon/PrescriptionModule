using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.DoctorDto
{
    public class DoctorInsertRequestDto
    {
        [Required(ErrorMessage = "User ID is required.")]
        public int? UserID { get; set; }

        [MaxLength(100, ErrorMessage = "Specialization cannot exceed 100 characters.")]
        public string? Specialization { get; set; }

        [MaxLength(50, ErrorMessage = "License number cannot exceed 50 characters.")]
        public string? LicenseNumber { get; set; }

        public int? DoctorReferenceID { get; set; }

        [MaxLength(100, ErrorMessage = "Hospital affiliation cannot exceed 100 characters.")]
        public string? HospitalAffiliation { get; set; }
    }
}
