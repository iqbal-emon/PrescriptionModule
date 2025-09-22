using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionInvestigation.Dtos.RequestDto.PrescriptionInvestigationDto
{
    public class PrescriptionInvestigationUpdateRequestDto
    {
        public int PrescriptionInvestigationId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? InvestigationId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
