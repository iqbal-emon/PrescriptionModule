using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionDiagonosis.Dtos.ResponseDto.PrescriptionDiagonosisDto
{
    public class PrescriptionDiagonsisApiResponseDto
    {
        public int PrescriptionDiagnosisId { get; set; }
        public int PrescriptionId { get; set; } // Foreign Key to Prescriptions
        public int DiseaseId { get; set; } // Foreign Key to Diseases
        public string Notes { get; set; } // Additional notes about the diagnosis
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
