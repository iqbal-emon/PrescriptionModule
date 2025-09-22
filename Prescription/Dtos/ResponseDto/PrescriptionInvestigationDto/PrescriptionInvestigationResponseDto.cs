using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionInvestigationDto
{
    public class PrescriptionInvestigationResponseDto
    {
        public string Name { get; set; }
        public string? Notes { get; set; }
        public int Id { get; set; }
    }
}
