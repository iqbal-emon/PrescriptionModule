using System;

namespace Prescription.Dtos.ResponseDto.DoctorDegreeDto
{
    public class DoctorDegreeApiResponseDto
    {
        public int DoctorDegreeID { get; set; }
        public int DoctorID { get; set; }
        public int DegreeID { get; set; }
        public int PassingYear { get; set; }
        public string InstituteName { get; set; }
        public int? InstituteID { get; set; }
        public string Country { get; set; }
        public int? CountryID { get; set; }
        public string City { get; set; }
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
