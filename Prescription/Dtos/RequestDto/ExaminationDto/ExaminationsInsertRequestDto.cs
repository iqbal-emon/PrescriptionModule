using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.ExaminationsDto
{
    public class ExaminationsInsertRequestDto
    {
        [Required(ErrorMessage = "TenantId is required.")]
        public int? TenantId { get; set; }

        [Required(ErrorMessage = "PatientId is required.")]
        public int? PatientId { get; set; }

        [Required(ErrorMessage = "DoctorId is required.")]
        public int? DoctorId { get; set; }

        [Required(ErrorMessage = "ExaminationDate is required.")]
        public DateTime ExaminationDate { get; set; } = DateTime.UtcNow;

        [MaxLength(4000, ErrorMessage = "Findings cannot exceed 4000 characters.")]
        public string Findings { get; set; }

        [MaxLength(255, ErrorMessage = "Notes cannot exceed 255 characters.")]
        public string Notes { get; set; }

        public string? BloodPressure { get; set; }
        public string? Pulse { get; set; }
        public string? Temperature { get; set; }

    }

}
