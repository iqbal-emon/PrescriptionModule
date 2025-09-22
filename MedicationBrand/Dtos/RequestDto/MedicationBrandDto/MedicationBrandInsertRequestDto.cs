using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationBrand.Dtos.RequestDto.MedicationBrandDto
{
    public class MedicationBrandInsertRequestDto
    {
        [Required(ErrorMessage = "ManufacturerID is required.")]
        public int ManufacturerID { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        [MaxLength(255, ErrorMessage = "Brand name cannot exceed 255 characters.")]
        public string BrandName { get; set; }

    }
}
