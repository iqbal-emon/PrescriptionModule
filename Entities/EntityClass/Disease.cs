using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [MaxLength(100)]
        [Required]
        public string DiseaseName { get; set; } // Name of the disease or diagnosis

        [MaxLength(255)]
        public string Description { get; set; } // Optional description

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
