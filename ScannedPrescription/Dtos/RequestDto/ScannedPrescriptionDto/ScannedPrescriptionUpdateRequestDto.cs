using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannedPrescription.Dtos.RequestDto.ScannedPrescription
{
    public class ScannedPrescriptionUpdateRequestDto
    {
        public int ScannedPrescriptionId { get; set; }
        public int? TenantId { get; set; }
        public int? PrescriptionId { get; set; }
        public string? FilePath { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
