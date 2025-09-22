using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionItem
{
    public class PrescriptionItemUpdateRequestDto
    {
          public int PrescriptionItemId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? MedicationId { get; set; }
        public string? Duration { get; set; }
        public string? Dosage { get; set; } // Example: 1 tablet, 2 times a day
        public string? MealTime { get; set; }
        public int? Quantity { get; set; }
        public string? Instructions { get; set; } // Example: Take with food

    }
}
