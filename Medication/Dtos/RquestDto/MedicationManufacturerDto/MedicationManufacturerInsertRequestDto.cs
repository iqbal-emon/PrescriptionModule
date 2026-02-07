using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.RequestDto.MedicationManufacturerDto
{
    public class MedicationManufacturerInsertRequestDto
    {
     

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } // Manufacturer name

    }
}
