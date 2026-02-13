using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor.Dtos.ResponseDto.DoctorDto
{
    public class DoctorApiResponseDto
    {
        public int DoctorID { get; set; }
        public int UserID { get; set; }
        public int? SpecialityID { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string HospitalAffiliation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}
