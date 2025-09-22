using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrescriptionPdf.Dtos.RequestDto.PrescriptionPdfDto
{
    public class FileUploadRequestViewModel
    {
        public IFormFile File { get; set; }
    }
}
