using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionSymtom_DeleteById stored procedure
    /// Parameters: @PrescriptionSymtomID INT
    /// </summary>
    public class PrescriptionSymtomDeleteModel
    {
        public int PrescriptionSymtomID { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionSymtom_Insert stored procedure
    /// Parameters: @PrescriptionSymtomID INT, @PrescriptionID INT, @SymtomID INT,
    /// @Days NVARCHAR(50) = NULL, @Description NVARCHAR(50) = NULL, @PastSymtom NVARCHAR(50) = NULL,
    /// @PresentSymtom NVARCHAR(150) = NULL, @Duration NVARCHAR(150) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionSymtomInsertModel
    {
        public int PrescriptionSymtomID { get; set; }
        public int PrescriptionID { get; set; }
        public int SymtomID { get; set; }
        public string? Days { get; set; }
        public string? Description { get; set; }
        public string? PastSymtom { get; set; }
        public string? PresentSymtom { get; set; }
        public string? Duration { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionSymtom_Update stored procedure
    /// Parameters: @PrescriptionSymtomID INT, @PrescriptionID INT = NULL, @SymtomID INT = NULL,
    /// @Description NVARCHAR(50) = NULL, @Days NVARCHAR(50) = NULL, @Duration NVARCHAR(50) = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @IsActive BIT = NULL,
    /// @CreatedAt DATETIME = NULL
    /// </summary>
    public class PrescriptionSymtomUpdateModel
    {
        public int PrescriptionSymtomID { get; set; }
        public int? PrescriptionID { get; set; }
        public int? SymtomID { get; set; }
        public string? Description { get; set; }
        public string? Days { get; set; }
        public string? Duration { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

