using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.RquestDto.MedicatonDto
{
    public class MedicationInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "Medication name is required.")]
        [MaxLength(100, ErrorMessage = "Medication name cannot exceed 100 characters.")]
        public string MedicationName { get; set; }

        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

        [MaxLength(100, ErrorMessage = "Manufacturer cannot exceed 100 characters.")]
        public string Manufacturer { get; set; }

        [MaxLength(50, ErrorMessage = "Dosage form cannot exceed 50 characters.")]
        public string? DosageForm { get; set; } // Tablet, Capsule, Liquid, etc.

        [MaxLength(50, ErrorMessage = "Strength cannot exceed 50 characters.")]
        public string? Strength { get; set; } // 500mg, 10mg, etc.

        [Required(ErrorMessage = "MedicationBrandId is required.")]
        public int MedicationBrandId { get; set; }

        [MaxLength(255, ErrorMessage = "GenericName cannot exceed 255 characters.")]
        public string? GenericName { get; set; }

        [Required(ErrorMessage = "DAR is required.")]
        [MaxLength(50, ErrorMessage = "DAR cannot exceed 50 characters.")]
        public string DAR { get; set; }


    }

}
