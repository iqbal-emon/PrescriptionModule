using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        [MaxLength(100)]
        public string MedicationName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Manufacturer { get; set; }

        [MaxLength(50)]
        public string DosageForm { get; set; } // Tablet, Capsule, Liquid, etc.

        [MaxLength(50)]
        public string Strength { get; set; } // 500mg, 10mg, etc.

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
