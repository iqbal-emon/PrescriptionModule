using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DoctorChamberDto
{
    public class DoctorChamberInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Chamber Name is required.")]
        public string ChamberName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;

        public string? Country { get; set; }

        public int? CountryID { get; set; }

        public string? City { get; set; }
        public bool IsVisibleOnPrescription { get; set; }
        public int? CityID { get; set; }

        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
        public int? ChamberReferenceId { get; set; }
        public int? DistrictId { get; set; }
        public int? DivisionId { get; set; }

        public bool IsDeleted { get; set; }
    }
}

