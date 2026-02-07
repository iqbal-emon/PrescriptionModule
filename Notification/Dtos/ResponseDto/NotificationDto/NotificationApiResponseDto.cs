using System;

namespace Notification.Dtos.ResponseDto.NotificationDto
{
    public class NotificationApiResponseDto
    {
        public int NotificationID { get; set; }
        public int TenantID { get; set; }
        public string? Message { get; set; }
        public string? TransactionType { get; set; }
        public int? CreatorEntityId { get; set; }
        public string? CreatorName { get; set; }
        public string? CreatorRole { get; set; }
        public string? CreateForName { get; set; }
        public int? NotifyToEntityId { get; set; }
        public string? NotifyToName { get; set; }
        public string? NotifyToRole { get; set; }
        public string? NoticeFromEntity { get; set; }
        public int? NoticeFromEntityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

