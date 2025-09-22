using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class DiagnosisDto
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public string? PastDiagnosis { get; set; }
        public string? PresentDiagnosis { get; set; }
    }
}
