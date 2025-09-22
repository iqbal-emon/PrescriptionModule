using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.Patients
{
    public class PatientsUpdateRequestDto
    {
        public int PatientID { get; set; }
        public int? UserID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string PatientAge { get; set; }
        public string? Address { get; set; }
        public string? BloodGroup { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsurancePolicyNumber { get; set; }
        public int? PatientReferenceID { get; set; }
    }
}
