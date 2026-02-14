using System;
using System.ComponentModel.DataAnnotations;

namespace Doctor.Dtos.RequestDto.DigitalSignatureDto
{
    public class DigitalSignatureInsertRequestDto
    {
        [Required(ErrorMessage = "Doctor ID is required.")]
        public int DoctorID { get; set; }

        [MaxLength(500)]
        public string? FileName { get; set; }

        [MaxLength(500)]
        public string? OriginalFileName { get; set; }

        [MaxLength(1000)]
        public string? FilePath { get; set; }

        public long? FileSize { get; set; }

        [MaxLength(100)]
        public string? MimeType { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }

        public int? CreatedBy { get; set; }
    }
}

