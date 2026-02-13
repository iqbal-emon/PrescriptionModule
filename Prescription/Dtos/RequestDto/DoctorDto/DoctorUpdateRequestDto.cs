using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.DoctorDto
{
    public class DoctorUpdateRequestDto
    {
        public int DoctorID { get; set; }
        public int? UserID { get; set; }
        public int? SpecialityID { get; set; }
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public string? HospitalAffiliation { get; set; }
        public int? DoctorReferenceID { get; set; }
    }
}
