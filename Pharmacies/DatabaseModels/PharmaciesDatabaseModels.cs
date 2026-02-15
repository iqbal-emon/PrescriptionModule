using System;

namespace Pharmacies.DatabaseModels
{
    /// <summary>
    /// Database model for Pharmacy_DeleteById stored procedure
    /// Parameters: @PharmacyID INT
    /// </summary>
    public class PharmacyDeleteModel
    {
        public int PharmacyID { get; set; }
    }

    /// <summary>
    /// Database model for Pharmacy_Insert stored procedure
    /// Parameters: @PharmacyID INT, @TenantId INT, @PharmacyName NVARCHAR(100),
    /// @Address NVARCHAR(255) = NULL, @PhoneNumber NVARCHAR(15) = NULL, @Email NVARCHAR(100) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class PharmacyInsertModel
    {
        public int PharmacyID { get; set; }
        public int TenantId { get; set; }
        public string PharmacyName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for Pharmacy_Update stored procedure
    /// Parameters: @PharmacyID INT, @TenantId INT = NULL, @PharmacyName NVARCHAR(100) = NULL,
    /// @Address NVARCHAR(255) = NULL, @PhoneNumber NVARCHAR(15) = NULL, @Email NVARCHAR(100) = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class PharmacyUpdateModel
    {
        public int PharmacyID { get; set; }
        public int? TenantId { get; set; }
        public string? PharmacyName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

