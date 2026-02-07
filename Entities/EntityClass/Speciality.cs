using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.EntityClass
{
    public class Speciality
    {
        [Key]
        public int SpecialityID { get; set; }

        [Required]
        [MaxLength(200)]
        public string SpecialityName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public int TenantID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

