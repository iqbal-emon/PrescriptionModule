using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.MedicineEntity
{
    public class MedicationBrandTranslation : BaseCommonEntityField
    {
        [Key]
        public int TranslationID { get; set; }

        [Required]
        public int MedicationBrandID { get; set; }

        [Required]
        public int LanguageID { get; set; }  // Links to a Language table

        [Required]
        [MaxLength(255)]
        public string TranslatedBrandName { get; set; }
    }
}
