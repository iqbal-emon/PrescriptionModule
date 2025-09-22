using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.MedicineEntity
{
    public class ManufacturerTranslation
    {
        [Key]
        public int TranslationID { get; set; }

        [Required]
        public int ManufacturerID { get; set; }

        [Required]
        public int LanguageID { get; set; }  // Links to a Languages table

        [Required]
        [MaxLength(255)]
        public string TranslatedName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
