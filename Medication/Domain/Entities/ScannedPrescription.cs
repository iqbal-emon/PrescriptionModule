using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class ScannedPrescription
    {
        [Key]
        public int ScannedPrescriptionID { get; set; }

        [ForeignKey("Tenant")]
        [Required]
        public int TenantID { get; set; }

        [ForeignKey("Prescription")]
        public int? PrescriptionID { get; set; } // Nullable because PrescriptionID can be null

        [Required]
        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public virtual Prescription Prescription { get; set; }
    }
}
