using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionItems.Dtos.RequestDto.PrescriptionItem
{
    public class PrescriptionItemInsertRequestDto
    {
        [Required(ErrorMessage = "PrescriptionId is required.")]
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "MedicationId is required.")]
        public int MedicationId { get; set; }

        [Required(ErrorMessage = "Dosage is required.")]
        [MaxLength(50, ErrorMessage = "Dosage cannot exceed 50 characters.")]
        public string Dosage { get; set; } // Example: 1 tablet, 2 times a day

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [MaxLength(255, ErrorMessage = "Instructions cannot exceed 255 characters.")]
        public string Instructions { get; set; } // Example: Take with food

    }
}
