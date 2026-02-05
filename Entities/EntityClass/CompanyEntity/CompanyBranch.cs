using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.EntityClass.CompanyEntity
{
    public class CompanyBranch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string? Address { get; set; }

        [MaxLength(20)]
        public string? ContactNo { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public Company? Company { get; set; }
    }
}

