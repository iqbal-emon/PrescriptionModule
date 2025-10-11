using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Dtos.ResponseDto.PatientsDto
{
    public class PatientDataDto
    {
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public int PatientReferenceID { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public string?   PatientAge { get; set; }   // Keep it int for better search

        // User Info
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? ReferenceUserId { get; set; }

        // For pagination
        public int TotalCount { get; set; }
        public  string PatientCode { get; set; }
    }
}
