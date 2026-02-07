using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class PrescriptionAdvice
    {
        [Key]
        public int AdviceID { get; set; }

        [ForeignKey("Prescription")]
        [Required]
        public int PrescriptionID { get; set; }

        [Required]
        [MaxLength(4000)] // Max length for NVARCHAR(MAX)
        public string AdviceText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        // Navigation property
        public virtual Prescription Prescription { get; set; }
    }
}
