using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Dtos.RequestDto.EmailTemplateDto
{
    public class EmailTemplateInsertRequestDto
    {
        [MaxLength(100, ErrorMessage = "Header cannot exceed 100 characters.")]
        public string? Header { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        public string? Body { get; set; }

        [MaxLength(100, ErrorMessage = "Footer cannot exceed 100 characters.")]
        public string? Footer { get; set; }

        [Required(ErrorMessage = "EmailTemplateType is required.")]
        public int EmailTemplateType { get; set; }

        public string? AttachmentHtmlCode { get; set; }
    }
}
