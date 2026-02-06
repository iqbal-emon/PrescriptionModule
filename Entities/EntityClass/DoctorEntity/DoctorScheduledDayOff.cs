using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorScheduledDayOff
    {
        [Key]
        public int DoctorScheduledDayOffID { get; set; }

        [ForeignKey("DoctorSchedule")]
        [Required]
        public int DoctorScheduleID { get; set; }

        [MaxLength(50)]
        public string? OffDay { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}

