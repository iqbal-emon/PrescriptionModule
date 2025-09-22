using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDTCreator.Dtos.RequestDto
{
    public class PdfGenerationRequestDto
    {
        public string HtmlContent { get; set; }
        public string PdfFilePath { get; set; }
        public string WkhtmltopdfPath { get; set; }
        public string FileName { get; set; }
    }
}
