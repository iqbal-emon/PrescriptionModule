using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class PatientDto
    {
        public string? PatientName { get; set; }
        public string? PatientAge { get; set; }
        public string? patientBloodGroup { get; set; }
        public int? PatientProfileId { get; set; }
        public string? PatientCode { get; set; }
        public string? patientPhoneNo { get; set; }
        public string? patientGender { get; set; }
    }
}
