using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string PatientName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public int SessionId { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        [Required]
        public string BloodGroup { get; set; }  
        [Required]

        public DateTime AppointmentDate { get;set; }
        [NotMapped]
        public int? SerialNo { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
   

        public DateTime UpdatedAt { get; set; }
    }
}
