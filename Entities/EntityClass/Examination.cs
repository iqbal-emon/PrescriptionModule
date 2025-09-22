using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Examination
    {
        [Key]
        public int ExaminationID { get; set; }

        [ForeignKey("Tenant")]
        [Required]
        public int TenantID { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public int PatientID { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public int DoctorID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExaminationDate { get; set; } = DateTime.Now;

        [MaxLength(4000)] // Maximum length for NVARCHAR(MAX)
        public string Findings { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; }

        public string BloodPressure { get;set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
