using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionExaminationDto
{
    public class PrescriptionExaminationUpdateRequestDto
    {
        public int PrescriptionExaminationId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? ExaminationId { get; set; }
        public string? Description { get; set; }
    }
}
