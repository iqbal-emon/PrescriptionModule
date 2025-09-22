using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionExaminationDto
{
    public class PrescriptionExaminationApiResponseDto
    {
        public int PrescriptionExaminationId { get; set; }
        public int PrescriptionId { get; set; }
        public int ExaminationId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
