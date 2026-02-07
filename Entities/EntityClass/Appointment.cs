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

        public int? SessionId { get; set; }

        public int? ScheduleId { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public int? PatientId { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public int? SerialNo { get; set; }

        public int? DoctorProfileId { get; set; }
    }
}
