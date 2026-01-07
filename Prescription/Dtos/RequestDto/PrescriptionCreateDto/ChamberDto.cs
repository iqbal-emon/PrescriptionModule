using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionCreateDto
{
    public class ChamberDto
    {
        public int chamberId { get; set; }
        public string? ChamberName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public int? districtId { get; set; }
        public int? divisionId { get; set; }
    }
}
