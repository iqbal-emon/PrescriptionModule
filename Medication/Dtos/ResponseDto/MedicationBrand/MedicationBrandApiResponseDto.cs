using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medication.Dtos.ResponseDto.MedicationBrand
{
    public class MedicationBrandApiResponseDto
    {
        public int MedicationBrandId { get; set; }
        public int ManufacturerId { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
