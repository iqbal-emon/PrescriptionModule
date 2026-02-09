using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.ResponseDto.MedicationDto
{
    public class MedicationDivisionUsageDto
    {
        public int MedicationID { get; set; }
        public string MedicationName { get; set; }
        public string GenericName { get; set; }
        public int UsageCount { get; set; }
        public decimal? TotalQuantity { get; set; }
    }
}

