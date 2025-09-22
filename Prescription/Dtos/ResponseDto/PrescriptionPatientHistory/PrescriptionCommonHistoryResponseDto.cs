using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionPatientHistory
{
    public class PrescriptionCommonHistoryResponseDto
    {
        public string? Name { get; set; }
        public string? Notes { get; set; }
        public string? PastHistory { get; set; }
        public string? PresentHistory { get; set; }
        public int Id{ get; set; }
    }
}
