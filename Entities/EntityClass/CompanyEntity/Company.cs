using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.EntityClass.CompanyEntity
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? LicenseNo { get; set; }

        [MaxLength(100)]
        public string? DrugRegCertificate { get; set; }

        public string? Address { get; set; }

        [MaxLength(20)]
        public string? ContactNo { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string CurrencySymbol { get; set; } = "?";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

