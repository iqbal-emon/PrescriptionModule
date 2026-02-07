using System;

namespace Appointment.Dtos.ResponseDto.AppointmentDto
{
    public class SessionListDto
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}

