using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class DoctorDto
    {

        public string? DoctorName { get; set; }
        public int? DoctorProfileId { get; set; }
        public string? DoctorCode { get; set; }
        public string? signature { get; set; }
        public string? specializationName { get; set; }
        public string? bmdc { get; set; }
        public List<DegreeDto>? Degree { get; set; }
        public string? AreaOfExperties { get; set; }
        public List<ChamberDto>? Chamber { get; set; }
        public List<ScheduleDto>? Schedule { get; set; }    
    }
}
