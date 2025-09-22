using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionDiagonosis.Dtos.RequestDto.PrescriptionDiagonosisDto
{
    public class PrescriptionDiagonsisUpdateRequestDto
    {
        public int PrescriptionDiagnosisId { get; set; }

        public int? PrescriptionId { get; set; } // Foreign Key to Prescriptions

        public int? DiseaseId { get; set; } // Foreign Key to Diseases

        public string? Notes { get; set; } // Additional notes about the diagnosis



    }
}
