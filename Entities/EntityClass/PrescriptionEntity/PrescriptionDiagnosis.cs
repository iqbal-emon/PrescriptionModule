using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionDiagnosis
    {
        [Key]
        public int PrescriptionDiagnosisId { get; set; }

        [Required]
        public int PrescriptionId { get; set; } // Foreign Key to Prescriptions

        [Required]
        public int DiagnosisId { get; set; } // Foreign Key to Diseases

        [MaxLength(255)]
        public string Notes { get; set; } // Additional notes about the diagnosis

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
        public string? PastDiagnosis { get; set; }
        public string? PresentDiagnosis { get; set; }

    }
}
