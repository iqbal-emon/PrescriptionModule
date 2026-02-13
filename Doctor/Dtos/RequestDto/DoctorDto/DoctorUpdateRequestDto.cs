using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor.Dtos.RequestDto.DoctorDto
{
    public class DoctorUpdateRequestDto
    {
        public int DoctorID { get; set; }
        public int? UserID { get; set; }
        public int? SpecialityID { get; set; }
        public string? Specialization { get; set; }
        public int? DoctorReferenceID { get; set; }
        public string? LicenseNumber { get; set; }
        public string? HospitalAffiliation { get; set; }
        
        // User table fields
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ContactNo { get; set; }
        
        // Doctor profile fields
        public string? BmdcRegNo { get; set; }
        public string? BmdcRegExpiryDate { get; set; }
        public string? IdentityNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? DoctorTitle { get; set; }
        public int? ProfileStep { get; set; }
        public string? Expertise { get; set; }
    }
}
