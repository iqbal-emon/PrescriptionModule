using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patients.Dtos.ResponseDto.PatientsDto
{
    public class FollowUpPatientDto
    {
        public string PatientName { get; set; }  // FirstName + LastName (e.g., "Anika Paul")
        public string PatientId { get; set; }  // PatientReferenceID (e.g., "PRB-4590")
        public string AppointmentTime { get; set; }  // e.g., "09:30 AM"
        public string ContactNumber { get; set; }  // PhoneNumber (e.g., "434343434")
        public string LastVisit { get; set; }  // Last
        public string Age { get; set; }
        public string Gender { get; set; }
    }
}
