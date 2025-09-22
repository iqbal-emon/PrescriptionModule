using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.PrescriptionSymtomDto
{
    public class PrescriptionSymtomResponseDto
    {
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Days { get; set; }
        public string? Notes { get; set; }
        public int Id { get; set; }
    }
}
