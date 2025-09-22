using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionTemplateDto
{
    public class PrescriptionTemplateUpdateRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int PrescriptionID { get; set; }
        public int DoctorID { get; set; } 
    }
}
