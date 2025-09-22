using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Dtos.RequestDto.PrescriptionPdfDto
{
    public class PrescriptionPdfRequestDto
    {
        public string HtmlContent { get; set; }
        public string PdfFilePath { get; set; }
        public string WkhtmltopdfPath { get; set; }
        public string FileName { get; set; }
    }
}
