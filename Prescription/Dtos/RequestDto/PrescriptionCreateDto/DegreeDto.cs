using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class DegreeDto
    {
        public string? DegreeName { get; set; }
        public string? InstituteName { get; set; }
        public string? InstituteCity { get; set; }
        public string? InstituteCountry { get; set; }
    }
}
