using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Dtos.ResponseDto.AppointmentDto
{
    public class AppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public int SessionId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? SerialNo { get; set; }
        public int? DoctorProfileId { get; set; }

        // Patient Info
        public int PatientID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string? PatientAge { get; set; }
        public string PatientCode { get; set; }

        // User (Patient) Info
        public string PatientName { get; set; }    // Mapped from U.FirstName
        public string LastName { get; set; }
        public string PatientEmail { get; set; }  // Mapped from U.Email
        public string PhoneNumber { get; set; }
    }
}
