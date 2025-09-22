using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionDiagonsisDto
{
    public class PrescriptionDiagonsisResponseDto
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; } // Foreign Key to Prescriptions
        public int DiagnosisId { get; set; } // Foreign Key to Diseases
        public string Notes { get; set; } // Additional notes about the diagnosis
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string DiagnosisName { get; set; } // Additional notes about the diagnosis
        public string? PastDiagnosis { get; set; }
        public string? PresentDiagnosis { get; set; }


    }
}
