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
        public string? Gender { get; set; }

        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string? Address { get; set; }

        [MaxLength(5)] // Max length for NVARCHAR(5)
        public string? BloodGroup { get; set; }

        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string? InsuranceProvider { get; set; }

        [MaxLength(50)] // Max length for NVARCHAR(50)
        public string? InsurancePolicyNumber { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PatientAge { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public bool? IsDeleted { get; set; } = false;
        
        public int? PatientReferenceID { get; set; }
        
        [MaxLength(100)]
        public string? PatientCode { get; set; }

        [MaxLength(255)]
        public string? FullName { get; set; }

        public bool? IsSelf { get; set; }

        [MaxLength(255)]
        public string? PatientName { get; set; }

        public int? Age { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? ZipCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? MobileNo { get; set; }

        [MaxLength(20)]
        public string? PatientMobileNo { get; set; }

        [MaxLength(255)]
        public string? Email { get; set; }

        [MaxLength(255)]
        public string? PatientEmail { get; set; }

        [MaxLength(100)]
        public string? CreatedBy { get; set; }

        [MaxLength(100)]
        public string? CreatorCode { get; set; }

        [MaxLength(50)]
        public string? CreatorRole { get; set; }

        public int? CreatorEntityId { get; set; }

        public bool? IsFirstTime { get; set; }
        // Navigation property
        //public virtual User User { get; set; }
    }
}
