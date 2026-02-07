using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatienFolowUp.Dtos.ResponseDto.Patients
{
    public record PatientsApiResponseDto
    {
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public int PatientReferenceID { get; set; }
        public string PatientAge { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PatientCode { get; set; }
        public string? FullName { get; set; }
        public bool? IsSelf { get; set; }
        public string? PatientName { get; set; }
        public int? Age { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? MobileNo { get; set; }
        public string? PatientMobileNo { get; set; }
        public string? Email { get; set; }
        public string? PatientEmail { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatorCode { get; set; }
        public string? CreatorRole { get; set; }
        public int? CreatorEntityId { get; set; }
        public bool? IsFirstTime { get; set; }
    }
}
