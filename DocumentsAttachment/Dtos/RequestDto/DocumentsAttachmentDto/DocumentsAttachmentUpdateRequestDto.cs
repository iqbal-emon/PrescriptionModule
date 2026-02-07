using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsAttachment.Dtos.RequestDto.DocumentsAttachmentDto
{
    public class DocumentsAttachmentUpdateRequestDto
    {
        public int DocumentsAttachmentID { get; set; }
        public string? FileName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? Path { get; set; }
        public string? EntityType { get; set; }
        public int? EntityId { get; set; }
        public string? AttachmentType { get; set; }
        public int? RelatedEntityid { get; set; }
        
        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

