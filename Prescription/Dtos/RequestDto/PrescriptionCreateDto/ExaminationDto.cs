using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class ExaminationDto
    {
        public string? Systolic { get; set; }
        public string? Diastolic { get; set; }
        public string? Pulse { get; set; }
        public string? Temperature { get; set; }
        public string? Weight { get; set; }
        public string? HeightFeet { get; set; }
        public string? HeightInches { get; set; }
    }
}
