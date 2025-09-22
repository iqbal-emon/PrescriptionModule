using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionExaminationDto
{
    public class PrescriptionExaminationInsertRequestDto
    {
        [Required(ErrorMessage = "PrescriptionId is required.")]
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "ExaminationId is required.")]
        public int ExaminationId { get; set; }

        public string? Description { get; set; }
    }
}
