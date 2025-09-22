using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionTemplateDto
{
    public class PrescriptionTemplateApiResponseDto
    {
        public int PrescriptionTemplateID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PrescriptionID { get; set; }
        public int DoctorID { get; set; }
    }
}
