using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionDiagonosisDto
{
    public class PrescriptionDiagonosisInsertRequestDto
    {
        [Required(ErrorMessage = "PrescriptionId is required.")]
        public int PrescriptionId { get; set; } // Foreign Key to Prescriptions

        [Required(ErrorMessage = "DiagnosisID is required.")]
        public int DiagnosisId { get; set; } // Foreign Key to Diseases

        [MaxLength(255, ErrorMessage = "Notes cannot exceed 255 characters.")]
        public string Notes { get; set; } // Additional notes about the diagnosis
        public string? PastDiagnosis { get; set; }
        public string? PresentDiagnosis { get; set; }



    }
}
