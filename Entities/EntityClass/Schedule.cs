using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }

        [ForeignKey("Tenant")]
        [Required]
        public int TenantID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        [MaxLength(20)]
        public string Day { get; set; } 

        [MaxLength(5)]
        public string Time { get; set; } 
    }
}
