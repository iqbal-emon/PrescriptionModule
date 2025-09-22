using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.PrescriptionEntity
{
    public class PrescriptionPdf : BaseCommonEntityField
    {
        [Key]
        public int PrescriptionPdfID { get; set; }

        [ForeignKey("Tenant")]
        [Required]
        public int TenantID { get; set; }

        public int? PrescriptionID { get; set; } // Nullable because PrescriptionID can be null

        public int? DoctorID { get; set; } // Nullable because PrescriptionID can be null

        public int? PatientID { get; set; } // Nullable because PrescriptionID can be null

        [Required]
        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string FilePath { get; set; }

        public int? AppointmentRefId { get; set; }
    }
}
