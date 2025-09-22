using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.RequestDto.MedicationBrandDto
{
    public class MedicationBrandUpdateRequestDto
    {
        public int MedicationBrandId { get; set; }
        public int? ManufacturerId { get; set; }
        public string? BrandName { get; set; }
    }
}
