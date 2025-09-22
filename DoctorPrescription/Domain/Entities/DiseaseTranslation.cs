using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPrescription.Domain.Entities
{
    public class DiseaseTranslation
    {
        [Key]
        public int TranslationID { get; set; }

        [ForeignKey("Disease")]
        [Required]
        public int DiseaseID { get; set; }

        [ForeignKey("Language")]
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

        // Navigation properties
        public virtual Disease Disease { get; set; }
        public virtual Language Language { get; set; }
    }
}
