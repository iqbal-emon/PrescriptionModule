using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.MedicineEntity
{
    public class MedicationBrand : BaseCommonEntityField
    {
        [Key]
        public int MedicationBrandID { get; set; }

        [Required]
        public int ManufacturerID { get; set; }

        [Required]
        [MaxLength(255)]
        public string BrandName { get; set; }
    }
}
