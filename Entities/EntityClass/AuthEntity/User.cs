using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.EntityClass;

namespace Entities.EntityClass.AuthEntity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [MaxLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(20)]
        public string? ContactNo { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public Role? Role { get; set; }
    }
}

