using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionPdf.Dtos.ResponseDto.PrescriptionPdfDto
{
    public class PrescriptionPdfPatientResponseDto
    {
        public int PrescriptionPdfID { get; set; }
        public int TenantID { get; set; }
        public int? PrescriptionID { get; set; }
        public int? DoctorID { get; set; }
        public int? PatientID { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int? AppointmentRefId { get; set; }

        // These are the additional fields from the JOIN with the Users table
        public int DoctorUserId { get; set; } // Doctor's UserId
        public string DoctorName { get; set; } // Doctor's FirstName (from Users table)
        public DateTime? FollowUpDate { get; set; }

        public int PatientUserId { get; set; } // Patient's UserId
        public string PatientName { get; set; } // Patient's FirstName (from Users table)
        public string PatientCode { get; set; }
    }
}
