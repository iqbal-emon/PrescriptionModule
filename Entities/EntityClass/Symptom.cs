using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Symptom
    {
        [Key]
        public int SymptomID { get; set; }

        [ForeignKey("Tenant")]
        [Required]
        public int TenantID { get; set; }

        [Required]
        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string SymptomName { get; set; }

        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
