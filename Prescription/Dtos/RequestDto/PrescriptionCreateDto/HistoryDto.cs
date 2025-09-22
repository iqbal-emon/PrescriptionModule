using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class HistoryDto
    {
        public string? Name { get; set; }
        public string? PastHistory { get; set; }
        public string? PresentHistory { get; set; }
        public int Id { get; set; }
    }
}
