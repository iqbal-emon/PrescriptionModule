using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.ResponseDto.MedicationDto
{
    public record MedicationApiResponseDto
    {
        public int MedicationId { get; set; }
        public int TenantId { get; set; }
        public string MedicationName { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string DosageForm { get; set; } // Tablet, Capsule, Liquid, etc.
        public string Strength { get; set; } // 500mg, 10mg, etc.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int MedicationBrandId { get; set; }
        public string GenericName { get; set; }
        public string Indication { get; set; }
        public string DAR { get; set; }
         public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
