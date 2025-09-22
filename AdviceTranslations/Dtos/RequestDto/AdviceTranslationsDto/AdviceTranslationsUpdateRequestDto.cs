using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviceTranslations.Dtos.RequestDto.AdviceTranslationsDto
{
    public class AdviceTranslationsUpdateRequestDto
    {
        public int TranslationId { get; set; }
        public int? AdviceId { get; set; }
        public int? LanguageId { get; set; }
        public string? TranslatedAdvice { get; set; }
    }
}
