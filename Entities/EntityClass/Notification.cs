using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.EntityClass
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [MaxLength(1000)]
        public string? Message { get; set; }

        [MaxLength(100)]
        public string? TransactionType { get; set; }

        public int? CreatorEntityId { get; set; }

        [MaxLength(200)]
        public string? CreatorName { get; set; }

        [MaxLength(100)]
        public string? CreatorRole { get; set; }

        [MaxLength(200)]
        public string? CreateForName { get; set; }

        public int? NotifyToEntityId { get; set; }

        [MaxLength(200)]
        public string? NotifyToName { get; set; }

        [MaxLength(100)]
        public string? NotifyToRole { get; set; }

        [MaxLength(100)]
        public string? NoticeFromEntity { get; set; }

        public int? NoticeFromEntityId { get; set; }

        [Required]
        public int TenantID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}

