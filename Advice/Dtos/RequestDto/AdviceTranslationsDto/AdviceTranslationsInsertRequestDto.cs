using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Dtos.RequestDto.AdviceTranslationsDto
{
    public class AdviceTranslationsInsertRequestDto
    {
        [Required(ErrorMessage = "AdviceId is required.")]
        public int AdviceId { get; set; }

        [Required(ErrorMessage = "LanguageId is required.")]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = "TranslatedAdvice is required.")]
        [MaxLength(1000, ErrorMessage = "TranslatedAdvice cannot exceed 1000 characters.")]
        public string TranslatedAdvice { get; set; }



    }
}
