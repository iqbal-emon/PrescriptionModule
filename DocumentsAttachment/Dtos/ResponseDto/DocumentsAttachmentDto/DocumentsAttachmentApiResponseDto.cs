using System;

namespace DocumentsAttachment.Dtos.ResponseDto.DocumentsAttachmentDto
{
    public class DocumentsAttachmentApiResponseDto
    {
        public int DocumentsAttachmentID { get; set; }
        public int TenantID { get; set; }
        public string? FileName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? Path { get; set; }
        public string? EntityType { get; set; }
        public int? EntityId { get; set; }
        public string? AttachmentType { get; set; }
        public int? RelatedEntityid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

