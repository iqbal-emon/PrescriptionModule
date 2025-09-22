using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [ForeignKey("Tenant")]
        [Required]
        public int TenantID { get; set; }

        [Required]
        [MaxLength(50)] // Max length for NVARCHAR(50)
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)] // Max length for NVARCHAR(50)
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)] // Max length for NVARCHAR(100)
        [EmailAddress] // Ensures proper email format
        public string Email { get; set; }

        [Required]
        [MaxLength(256)] // Max length for NVARCHAR(256)
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)] // Max length for NVARCHAR(20)
        public string UserType { get; set; }

        [MaxLength(15)] // Max length for NVARCHAR(15)
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public int? ReferenceUserId { get; set; }

    }
}
