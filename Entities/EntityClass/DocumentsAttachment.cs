using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.EntityClass
{
    public class DocumentsAttachment
    {
        [Key]
        public int DocumentsAttachmentID { get; set; }

        [MaxLength(500)]
        public string? FileName { get; set; }

        [MaxLength(500)]
        public string? OriginalFileName { get; set; }

        [MaxLength(1000)]
        public string? Path { get; set; }

        [MaxLength(50)]
        public string? EntityType { get; set; } // Doctor, Patient, etc.

        public int? EntityId { get; set; }

        [MaxLength(50)]
        public string? AttachmentType { get; set; } // ProfilePicture, Document, etc.

        public int? RelatedEntityid { get; set; }

        [Required]
        public int TenantID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

