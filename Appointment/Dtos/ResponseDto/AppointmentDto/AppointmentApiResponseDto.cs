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
        public string? ScheduleName { get; set; }
        public string? SessionName { get; set; }
        public string? AppointmentDate { get; set; }
         public int? SerialNo { get; set; }
        public bool IsDeleted { get; set; }
        public string? DoctorScheduleName { get; set; }
        public string? ScheduleDayofWeek { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? NoOfPatients { get; set; }
        public bool? IsActive { get; set; }

        public string? ScheduleTypeName { get; set; }
        public string? ConsultancyTypeName { get; set; }
        public long? DoctorChamberId { get; set; }
        //public DoctorChamber? DoctorChamber { get; set; }
        public string? Chamber { get; set; }
        public string? Status { get; set; }
        public DateTime? OffDayFrom { get; set; }
        public string? DayTextFrom { get; set; }
        public DateTime? OffDayTo { get; set; }
        public string? DayTextTo { get; set; }
        public string? Remarks { get; set; }
        public bool? ResponseSuccess { get; set; }
        public string? ResponseMessage { get; set; }


    }
}
