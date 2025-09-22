using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }  // Primary Key

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } // e.g., "Admin", "Student", "Professor"

        [StringLength(255)]
        public string Description { get; set; } // Optional description of the role

        public bool IsActive { get; set; } = true; // Soft delete functionality

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }

}
