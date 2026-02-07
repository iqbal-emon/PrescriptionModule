using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsAttachment.Dtos.RequestDto.DocumentsAttachmentDto
{
    public class DocumentsAttachmentInsertRequestDto
    {
        [MaxLength(500)]
        public string? FileName { get; set; }

        [MaxLength(500)]
        public string? OriginalFileName { get; set; }

        [MaxLength(1000)]
        public string? Path { get; set; }

        [MaxLength(50)]
        public string? EntityType { get; set; }

        public int? EntityId { get; set; }

        [MaxLength(50)]
        public string? AttachmentType { get; set; }

        public int? RelatedEntityid { get; set; }

        [Required(ErrorMessage = "Tenant ID is required.")]
        public int TenantID { get; set; }
    }
}

