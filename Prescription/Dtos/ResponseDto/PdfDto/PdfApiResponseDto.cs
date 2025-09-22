using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionPdf.Dtos.ResponseDto.PdfDto
{
    public record PdfApiResponseDto
    {
        public int PrescriptionPdfId { get; set; }
        public int TenantId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
