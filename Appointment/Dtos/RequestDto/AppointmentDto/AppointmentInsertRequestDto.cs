using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Dtos.RequestDto.AppointmentDto
{
    public class AppointmentInsertRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string PatientName { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]

        public int DoctorProfileId  { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public string BloodGroup { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
    }
}
