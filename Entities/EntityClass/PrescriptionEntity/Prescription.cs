using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class Prescription
    {
        [Key]
        [Column("PrescriptionID")]
        public int PrescriptionId { get; set; }

        [Required]
        [Column("TenantID")]
        public int TenantId { get; set; }

        [Required]
        [Column("PatientID")]
        public int PatientId { get; set; }

        [Required]
        [Column("DoctorID")]
        public int DoctorId { get; set; }
        
        [Required]
        [Column("PatientFollowUpID")]
        public int PatientFollowUpId { get; set; }

        [Column("PharmacyID")]
        public int? PharmacyId { get; set; }

        public DateTime? IssueDate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(10)]
        public string? Language { get; set; } = "English";

        [Required]
        [Column("StatusID")]
        public int StatusId { get; set; } // Foreign Key to PrescriptionStatuses

        public DateTime? FollowUpDate { get; set; }

        public bool? IsArchived { get; set; } = false;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("IsHeader")]
        public bool IsHeader { get; set; }
        
        [Column("isPreHand")]
        public bool isPreHand { get; set; }
        
        public bool? IsDeleted { get; set; } = false;

        [Column("AppointmentRefId")]
        public int? AppointmentRefId { get; set; }
        
        [MaxLength(100)]
        public string? PrescriptionCode { get; set; }
    }
}
