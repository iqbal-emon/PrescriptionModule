using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannedPrescription.Dtos.ResponseDto.ScannedPrescriptionDto
{
    public class ScannedPrescriptionApiResponseDto
    {
        public int ScannedPrescriptionId { get; set; }
        public int TenantId { get; set; }
        public int? PrescriptionId { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
