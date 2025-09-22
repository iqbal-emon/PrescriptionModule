using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionInvestigationDto
{
    public class PrescriptionInvestigationApiResponseDto
    {
        public int PrescriptionInvestigationId { get; set; }
        public int PrescriptionId { get; set; }
        public int InvestigationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
