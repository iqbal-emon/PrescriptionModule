using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannedPrescription.Dtos.RequestDto.ScannedPrescription
{
    public class ScannedPrescriptionInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int TenantId { get; set; }

        public int? PrescriptionId { get; set; } // Nullable because PrescriptionId can be null

        [Required(ErrorMessage = "FilePath is required.")]
        [MaxLength(255, ErrorMessage = "FilePath cannot exceed 255 characters.")]
        public string FilePath { get; set; }

    }
}
