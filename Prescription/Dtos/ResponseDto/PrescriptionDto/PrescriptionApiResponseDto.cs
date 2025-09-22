using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionDto
{
    public class PrescriptionApiResponseDto
    {
        public int PrescriptionId { get; set; }
        public int TenantId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int PatientFollowUpId { get; set; }
        public int? PharmacyId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Language { get; set; }
        public int StatusId { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public bool isHeader { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? AppointmentRefId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
