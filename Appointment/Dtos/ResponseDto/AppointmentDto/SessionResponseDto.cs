using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Dtos.ResponseDto.AppointmentDto
{
    public class SessionResponseDto
    {
        public long? DoctorScheduleId { get; set; }
        public string? DoctorScheduleName { get; set; }
        public string? ScheduleDayofWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? NoOfPatients { get; set; }
        public bool? IsActive { get; set; }
    }
}
