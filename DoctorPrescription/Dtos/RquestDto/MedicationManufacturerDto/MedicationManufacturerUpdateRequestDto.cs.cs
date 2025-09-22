using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.RequestDto.MedicationManufacturerDto
{
    public class MedicationManufacturerUpdateRequestDto
    {
        public int ManufacturerId { get; set; }

        public string? Name { get; set; } // Manufacturer name

    }
}
