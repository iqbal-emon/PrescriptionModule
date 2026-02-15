using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DigitalSignature_DeleteById stored procedure
    /// Parameters: @DigitalSignatureID INT
    /// </summary>
    public class DigitalSignatureDeleteModel
    {
        public int DigitalSignatureID { get; set; }
    }

    /// <summary>
    /// Database model for DigitalSignature_Insert stored procedure
    /// Parameters: @DigitalSignatureID INT, @DoctorID INT, @FileName NVARCHAR(500) = NULL,
    /// @OriginalFileName NVARCHAR(500) = NULL, @FilePath NVARCHAR(1000) = NULL,
    /// @FileSize BIGINT = NULL, @MimeType NVARCHAR(100) = NULL, @IsActive BIT = 1,
    /// @TenantID INT, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @CreatedBy INT = NULL, @UpdatedBy INT = NULL, @IsDeleted BIT = 0
    /// </summary>
    public class DigitalSignatureInsertModel
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
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    /// <summary>
    /// Database model for DigitalSignature_Update stored procedure
    /// Parameters: @DigitalSignatureID INT, @DoctorID INT = NULL, @FileName NVARCHAR(500) = NULL,
    /// @OriginalFileName NVARCHAR(500) = NULL, @FilePath NVARCHAR(1000) = NULL,
    /// @FileSize BIGINT = NULL, @MimeType NVARCHAR(100) = NULL, @IsActive BIT = NULL,
    /// @TenantID INT = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @CreatedBy INT = NULL, @UpdatedBy INT = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class DigitalSignatureUpdateModel
    {
        public int DigitalSignatureID { get; set; }
        public int? DoctorID { get; set; }
        public string? FileName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? FilePath { get; set; }
        public long? FileSize { get; set; }
        public string? MimeType { get; set; }
        public bool? IsActive { get; set; }
        public int? TenantID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

