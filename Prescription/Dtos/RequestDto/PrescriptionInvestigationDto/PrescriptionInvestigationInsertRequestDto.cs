using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionInvestigationDto
{
    public class PrescriptionInvestigationInsertRequestDto
    {

        public int? PrescriptionId { get; set; } // Nullable because PrescriptionId can be null

        [Required(ErrorMessage = "InvestigationId is required.")]
        public int InvestigationId { get; set; }
        public string? Description { get; set; }

    }
}
