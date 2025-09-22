using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionAdvice.Dtos.ResponseDto.PrescriptionAdvice
{
    public class PrescriptionAdviceApiResponseDto
    {
        public int PrescriptionAdviceId { get; set; }
        public int PrescriptionId { get; set; }
        public int CommonAdviceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
