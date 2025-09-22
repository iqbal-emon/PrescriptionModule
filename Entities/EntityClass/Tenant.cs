using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Tenant
    {
        [Key]
        public int TenantID { get; set; }

        [Required]
        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string TenantName { get; set; }

        [Required]
        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string Domain { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
