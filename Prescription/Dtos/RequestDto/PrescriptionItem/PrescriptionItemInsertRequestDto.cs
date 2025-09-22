using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionItem
{
    public class PrescriptionItemInsertRequestDto
    {
        [Required(ErrorMessage = "PrescriptionId is required.")]
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "MedicationId is required.")]
        public int MedicationId { get; set; }

        public string? Dosage { get; set; } // Example: 1 tablet, 2 times a day

        public int Quantity { get; set; }
        public string? Duration { get; set; }
        public string? MealTime { get; set; }

        public string? Instructions { get; set; } // Example: Take with food

        public string? Timing { get; set; } // Example: Take with food
        

    }
}
