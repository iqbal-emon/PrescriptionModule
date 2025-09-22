using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class ExaminationTranslation
    {
        [Key]
        public int TranslationID { get; set; }

        [ForeignKey("Examination")]
        [Required]
        public int ExaminationID { get; set; }

        [ForeignKey("Language")]
        [Required]
        public int LanguageID { get; set; }

        public string TranslatedFindings { get; set; } // NVARCHAR(MAX) can be mapped as string

        [MaxLength(255)] // Max length for NVARCHAR(255)
        public string TranslatedNotes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public virtual Examination Examination { get; set; }
        public virtual Language Language { get; set; }
    }
}
