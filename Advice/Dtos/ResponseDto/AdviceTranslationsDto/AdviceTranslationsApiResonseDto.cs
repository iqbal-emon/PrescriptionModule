using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advice.Dtos.ResponseDto.AdviceTranslationsDto
{
    public class AdviceTranslationsApiResonseDto
    {
        public int TranslationId { get; set; }
        public int AdviceId { get; set; }
        public int LanguageId { get; set; }
        public string TranslatedAdvice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
