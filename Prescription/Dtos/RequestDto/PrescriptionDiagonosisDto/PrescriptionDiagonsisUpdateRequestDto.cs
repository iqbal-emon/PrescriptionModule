using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionDiagonosisDto
{
    public class PrescriptionDiagonosisUpdateRequestDto
    {
        public int PrescriptionDiagnosisId { get; set; }

        public int? PrescriptionId { get; set; } // Foreign Key to Prescriptions

        public int? DiseaseId { get; set; } // Foreign Key to Diseases

        public string? Notes { get; set; } // Additional notes about the diagnosis



    }
}
