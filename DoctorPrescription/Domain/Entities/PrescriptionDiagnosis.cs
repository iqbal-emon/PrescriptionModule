using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class PrescriptionDiagnosis
    {
        [Key]
        public int PrescriptionDiagnosisId { get; set; }

        [Required]
        public int PrescriptionId { get; set; } // Foreign Key to Prescriptions

        [Required]
        public int DiseaseId { get; set; } // Foreign Key to Diseases

        [MaxLength(255)]
        public string Notes { get; set; } // Additional notes about the diagnosis

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        // Navigation Properties (Optional)
        [ForeignKey("PrescriptionId")]
        public virtual Prescription Prescription { get; set; }

        [ForeignKey("DiseaseId")]
        public virtual Disease Disease { get; set; }
    }
}
