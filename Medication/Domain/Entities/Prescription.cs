using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int PatientFollowUpId { get; set; }

        public int? PharmacyId { get; set; }

        public DateTime IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string Language { get; set; } = "English";

        [Required]
        public int StatusId { get; set; } // Foreign Key to PrescriptionStatuses

        public DateTime? FollowUpDate { get; set; }

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}
