using System;
using System.ComponentModel.DataAnnotations;

namespace Prescription.Dtos.RequestDto.DoctorDegreeDto
{
    public class DoctorDegreeInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int? DoctorID { get; set; }

        [Required(ErrorMessage = "Degree ID is required.")]
        public int DegreeID { get; set; }

        public int PassingYear { get; set; }

        public string InstituteName { get; set; } = string.Empty;

        public int? InstituteID { get; set; }

        public string Country { get; set; } = string.Empty;

        public int? CountryID { get; set; }
        public string City { get; set; } = string.Empty;

        public int? CityID { get; set; }

        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }

  
    }
}
