using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.ResponseDto.MedicationDto
{
    public class MedicationMostUsedDto
    {
        public int? MedicationId { get; set; }
        public string? MedicationName { get; set; }
        public string? Manufacturer { get; set; }
        public string? GenericName { get; set; }
        public int? UsageCount { get; set; }
        public int? TotalCount { get; set; } // for pagination


    }
}
