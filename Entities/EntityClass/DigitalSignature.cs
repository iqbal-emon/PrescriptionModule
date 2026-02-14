using System;

namespace Entities.EntityClass.DoctorEntity
{
    public class DigitalSignature
    {
        public int DigitalSignatureID { get; set; }
        public int DoctorID { get; set; }
        public string? FileName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? FilePath { get; set; }
        public long? FileSize { get; set; }
        public string? MimeType { get; set; }
        public bool IsActive { get; set; } = true;
        public int TenantID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

