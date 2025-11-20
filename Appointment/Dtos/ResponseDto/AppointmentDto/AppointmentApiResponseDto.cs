using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Dtos.ResponseDto.AppointmentDto
{
    public class AppointmentApiResponseDto
    {
        public int? AppointmentId { get; set; }
        public int? PatientID { get; set; }
        public string? PatientName { get; set; }
        public string? BloodGroup { get; set; }
        public string? Gender { get; set; }
        public int? PatientAge { get; set; }
        public int? SessionId { get; set; }
        public int? ScheduleId { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
    }
}
