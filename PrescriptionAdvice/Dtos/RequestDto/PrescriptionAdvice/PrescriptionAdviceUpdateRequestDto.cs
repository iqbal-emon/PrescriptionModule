using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionAdvice.Dtos.RequestDto.PrescriptionAdvice
{
    public class PrescriptionAdviceUpdateRequestDto
    {
        public int PrescriptionAdviceId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? CommonAdviceId { get; set; }
    }
}
