using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTemplate.Dtos.ResponseDto.EmailTemplateDto
{
    public record EmailTemplateApiResponseDto
    {
        public Guid Id { get; set; } // EmailTemplate Identifier
        public string Header { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public int EmailTemplateType { get; set; }
        public string AttachmentHtmlCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
