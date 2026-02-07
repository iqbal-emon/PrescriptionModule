using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.RquestDto.MedicatonDto
{
    public class MedicationUpdateRequestDto
    {
        public int MedicationId { get; set; }
        public int? TenantId { get; set; }
        public string? MedicationName { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? DosageForm { get; set; } // Tablet, Capsule, Liquid, etc.
        public string? Strength { get; set; } // 500mg, 10mg, etc.

        public int MedicationBrandId { get; set; }
        public string? GenericName { get; set; }
        public string? DAR { get; set; }

    }
}
