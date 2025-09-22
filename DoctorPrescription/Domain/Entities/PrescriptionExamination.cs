using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class PrescriptionExamination
    {
        [Key]
        public int PrescriptionExaminationID { get; set; }

        [ForeignKey("Prescription")]
        [Required]
        public int PrescriptionID { get; set; }

        [ForeignKey("Examination")]
        [Required]
        public int ExaminationID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public virtual Prescription Prescription { get; set; }
        public virtual Examination Examination { get; set; }
    }
}
