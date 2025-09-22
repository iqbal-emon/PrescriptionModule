using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.MedicineEntity
{
    public class MedicationManufacturer : BaseCommonEntityField
    {
        [Key]
        public int ManufacturerID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
