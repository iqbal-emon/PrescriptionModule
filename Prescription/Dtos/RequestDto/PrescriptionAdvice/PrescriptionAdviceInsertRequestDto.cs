using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionAdvice
{
    public class PrescriptionAdviceInsertRequestDto
    {
        [Required(ErrorMessage = "PrescriptionId is required.")]
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "CommonId is required.")]
          public int CommonAdviceId { get; set; }
        public string? Description {  get; set; }
    }
}
