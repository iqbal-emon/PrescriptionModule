using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.ExaminationsDto
{
    public class ExaminationsUpdateRequestDto
    {

        public int ExaminationID { get; set; }
        public int? TenantID { get; set; }
        public int? PatientID { get; set; }
        public int? DoctorID { get; set; }
        public DateTime? ExaminationDate { get; set; } 
        public string? Findings { get; set; }
        public string? Notes { get; set; }

    }
}
