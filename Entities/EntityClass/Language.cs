using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.EntityClass
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }

        [Required]
        [MaxLength(10)] // Max length for NVARCHAR(10)
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(50)] // Max length for NVARCHAR(50)
        public string LanguageName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
