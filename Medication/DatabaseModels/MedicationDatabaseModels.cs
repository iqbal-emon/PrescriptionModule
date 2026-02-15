using System;

namespace Medication.DatabaseModels
{
    /// <summary>
    /// Database model for Medication_DeledeById stored procedure
    /// Parameters: @MedicationId INT
    /// </summary>
    public class MedicationDeleteModel
    {
        public int MedicationId { get; set; }
    }

    /// <summary>
    /// Database model for Medication_Insert stored procedure
    /// Parameters: @TenantId INT, @MedicationBrandId INT, @GenericName NVARCHAR(255), @DAR NVARCHAR(50),
    /// @MedicationName NVARCHAR(100), @Description NVARCHAR(255) = NULL, @Manufacturer NVARCHAR(100) = NULL,
    /// @DosageForm NVARCHAR(50) = NULL, @Strength NVARCHAR(50) = NULL, @Indication NVARCHAR(MAX) = NULL,
    /// @IsActive BIT = 1, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted bit = 0,
    /// @MedicationId INT OUTPUT
    /// </summary>
    public class MedicationInsertModel
    {
        public int TenantId { get; set; }
        public int MedicationBrandId { get; set; }
        public string GenericName { get; set; }
        public string DAR { get; set; }
        public string MedicationName { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? DosageForm { get; set; }
        public string? Strength { get; set; }
        public string? Indication { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    /// <summary>
    /// Database model for Medication_Update stored procedure
    /// Parameters: @MedicationId INT, @TenantId INT, @MedicationBrandId INT, @GenericName NVARCHAR(255),
    /// @DAR NVARCHAR(50), @MedicationName NVARCHAR(100), @Description NVARCHAR(255) = NULL,
    /// @Manufacturer NVARCHAR(100) = NULL, @DosageForm NVARCHAR(50) = NULL, @Strength NVARCHAR(50) = NULL,
    /// @Indication NVARCHAR(MAX) = NULL, @IsActive BIT = 1, @UpdatedId INT OUTPUT
    /// </summary>
    public class MedicationUpdateModel
    {
        public int MedicationId { get; set; }
        public int TenantId { get; set; }
        public int MedicationBrandId { get; set; }
        public string GenericName { get; set; }
        public string DAR { get; set; }
        public string MedicationName { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public string? DosageForm { get; set; }
        public string? Strength { get; set; }
        public string? Indication { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

