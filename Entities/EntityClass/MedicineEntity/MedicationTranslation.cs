using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass.MedicineEntity
{
    public class MedicationTranslation
    {
        [Key]
        public int TranslationID { get; set; }

        [Required]
        public int MedicationID { get; set; }

        [Required]
        public int LanguageID { get; set; }

        [Required]
        [MaxLength(100)] // Max length for NVARCHAR(100)
        public string TranslatedName { get; set; }

        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string TranslatedDescription { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

    }
}
