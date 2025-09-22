using System;

namespace DoctorChamber.Dtos.ResponseDtoDoctorChamberDto
{
    public class DoctorChamberApiResponseDto
    {
        public int ChamberID { get; set; }
        public int DoctorID { get; set; }
        public string ChamberName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int? CountryID { get; set; }
        public string City { get; set; }
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool IsVisibleOnPrescription { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
