using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorDegreeDto
{
    public class DoctorDegreeInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Degree ID is required.")]
        public int DegreeID { get; set; }

        [Required(ErrorMessage = "Passing Year is required.")]
        public int PassingYear { get; set; }

        [Required(ErrorMessage = "Institute Name is required.")]
        public string InstituteName { get; set; } = string.Empty;

        public int? InstituteID { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; } = string.Empty;

        public int? CountryID { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; } = string.Empty;

        public int? CityID { get; set; }

        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

