using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationManufacturer.Dtos.ReponseDto.MedicationManufacturerDto
{
    public class MedicationManufacturerApiReponseDto
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; } // Manufacturer name
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
