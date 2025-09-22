using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PatientEntity
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [ForeignKey("User")]
        [Required]
        public int UserID { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10)] // Max length for NVARCHAR(10)
        public string Gender { get; set; }

        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string Address { get; set; }

        [MaxLength(5)] // Max length for NVARCHAR(5)
        public string BloodGroup { get; set; }

        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string InsuranceProvider { get; set; }

        [MaxLength(50)] // Max length for NVARCHAR(50)
        public string InsurancePolicyNumber { get; set; }

        public string PatientAge { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
        public int PatientReferenceID { get; set; }
        // Navigation property
        //public virtual User User { get; set; }
    }
}
