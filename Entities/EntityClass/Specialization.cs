using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.EntityClass
{
    public class Specialization
    {
        [Key]
        public int SpecializationID { get; set; }

        [ForeignKey("Speciality")]
        public int? SpecialityID { get; set; }

        [Required]
        [MaxLength(200)]
        public string SpecializationName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int TenantID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation Property
        public Speciality? Speciality { get; set; }
    }
}

