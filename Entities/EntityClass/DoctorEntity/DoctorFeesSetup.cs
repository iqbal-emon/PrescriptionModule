using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.DoctorEntity
{
    public class DoctorFeesSetup
    {
        [Key]
        public int DoctorFeesSetupID { get; set; }

        [ForeignKey("DoctorSchedule")]
        [Required]
        public int DoctorScheduleID { get; set; }

        [MaxLength(50)]
        public string? AppointmentType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? CurrentFee { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PreviousFee { get; set; }

        public DateTime? FeeAppliedFrom { get; set; }

        public int? FollowUpPeriod { get; set; }

        public int? ReportShowPeriod { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }

        public DateTime? DiscountAppliedFrom { get; set; }

        public int? DiscountPeriod { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalFee { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}

