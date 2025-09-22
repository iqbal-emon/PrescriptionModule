using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionPdf.Dtos.RequestDto.PrescriptionPdfDto
{
    public class PrescriptionPdfInsertRequestDto
    {
        [Required(ErrorMessage = "TenantID is required.")]
        public int TenantID { get; set; }

        [Required(ErrorMessage = "PrescriptionID is required.")]
        public int PrescriptionID { get; set; }

        [Required(ErrorMessage = "DoctorID is required.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "PatientID is required.")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "FilePath is required.")]
        public string FilePath { get; set; }
        
        public string? Description { get; set; }
        public int? AppointmentRefId { get; set; }

    }

}
