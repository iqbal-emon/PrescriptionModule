using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionItem_DeleteById stored procedure
    /// Parameters: @PrescriptionItemId INT
    /// </summary>
    public class PrescriptionItemDeleteModel
    {
        public int PrescriptionItemId { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionItem_Insert stored procedure
    /// Parameters: @PrescriptionItemId INT, @PrescriptionId INT, @MedicationId INT,
    /// @Dosage NVARCHAR(50) = NULL, @Quantity INT, @Duration NVARCHAR(50) = NULL,
    /// @Timing NVARCHAR(50) = NULL, @Instructions NVARCHAR(255) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @MealTime NVARCHAR(50) = NULL
    /// </summary>
    public class PrescriptionItemInsertModel
    {
        public int PrescriptionItemId { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicationId { get; set; }
        public string? Dosage { get; set; }
        public int Quantity { get; set; }
        public string? Duration { get; set; }
        public string? Timing { get; set; }
        public string? Instructions { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public string? MealTime { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionItem_Update stored procedure
    /// Parameters: @PrescriptionItemId INT, @PrescriptionId INT = NULL, @MedicationId INT = NULL,
    /// @Dosage NVARCHAR(50) = NULL, @Duration NVARCHAR(50) = NULL, @Timing NVARCHAR(50) = NULL,
    /// @MealTime NVARCHAR(50) = NULL, @Quantity INT = NULL, @Instructions NVARCHAR(255) = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class PrescriptionItemUpdateModel
    {
        public int PrescriptionItemId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? MedicationId { get; set; }
        public string? Dosage { get; set; }
        public string? Duration { get; set; }
        public string? Timing { get; set; }
        public string? MealTime { get; set; }
        public int? Quantity { get; set; }
        public string? Instructions { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

