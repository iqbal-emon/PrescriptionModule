using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionDiagonosis.Dtos.RequestDto.PrescriptionDiagonosisDto
{
    public class PrescriptionDiagonsisInsertRequestDto
    {
        [Required(ErrorMessage = "PrescriptionId is required.")]
        public int PrescriptionId { get; set; } // Foreign Key to Prescriptions

        [Required(ErrorMessage = "DiseaseId is required.")]
        public int DiseaseId { get; set; } // Foreign Key to Diseases

        [MaxLength(255, ErrorMessage = "Notes cannot exceed 255 characters.")]
        public string Notes { get; set; } // Additional notes about the diagnosis



    }
}
