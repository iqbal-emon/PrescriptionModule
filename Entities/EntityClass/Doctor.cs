using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }

        [ForeignKey("Speciality")]
        public int? SpecialityID { get; set; }

        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string Specialization { get; set; }

        [MaxLength(50)] // Max length for NVARCHAR(50)
        public string LicenseNumber { get; set; }

        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string HospitalAffiliation { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public int? DoctorReferenceID { get; set; }

        [MaxLength(500)]
        public string? Expertise { get; set; }

        public int? ProfileStep { get; set; }

        [MaxLength(50)]
        public string? BmdcRegNo { get; set; }

        public DateTime? BmdcRegExpiryDate { get; set; }

        [MaxLength(50)]
        public string? IdentityNumber { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        public int? DoctorTitle { get; set; }

    }
}
