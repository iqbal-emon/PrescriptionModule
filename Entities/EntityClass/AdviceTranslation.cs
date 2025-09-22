using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.EntityClass.PrescriptionEntity;

namespace Entities.EntityClass
{
    public class AdviceTranslation 
    {
        [Key]
        public int TranslationID { get; set; }

        [ForeignKey("PrescriptionAdvice")]
        [Required]
        public int AdviceID { get; set; }

        [ForeignKey("Language")]
        [Required]
        public int LanguageID { get; set; }

        [Required]
        public string TranslatedAdvice { get; set; } // NVARCHAR(MAX) can be mapped as string

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        // Navigation properties
     
    }
}
