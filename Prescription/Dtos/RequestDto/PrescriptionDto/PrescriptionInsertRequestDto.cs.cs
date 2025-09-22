using Entities.EntityClass;
using Entities.EntityClass.PatientEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionDto
{
    public class PrescriptionInsertRequestDto
    {
  
        public int? TenantId { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set; }

        public int PatientFollowUpId { get; set; }
        public int? PharmacyId { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }

        public string Language { get; set; } 
        public int StatusId { get; set; }

        public DateTime? FollowUpDate { get; set; }
        public bool isHeader { get; set; }
        public bool isPreHand { get; set; }

        public bool IsArchived { get; set; } = false;
        public int? AppointmentRefId { get; set; }


    }
}
