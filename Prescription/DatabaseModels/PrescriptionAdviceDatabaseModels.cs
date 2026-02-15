using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionAdvice_DeleteById stored procedure
    /// Parameters: @AdviceId INT
    /// </summary>
    public class PrescriptionAdviceDeleteModel
    {
        public int AdviceId { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionAdvice_Insert stored procedure
    /// Parameters: @PrescriptionAdviceId INT, @PrescriptionId INT, @CommonAdviceId INT,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @Description NVARCHAR(255) = NULL, @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionAdviceInsertModel
    {
        public int PrescriptionAdviceId { get; set; }
        public int PrescriptionId { get; set; }
        public int CommonAdviceId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionAdvice_Update stored procedure
    /// Parameters: @PrescriptionAdviceId INT, @PrescriptionId INT = NULL, @CommonAdviceId INT = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL,
    /// @Description NVARCHAR(255) = NULL, @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionAdviceUpdateModel
    {
        public int PrescriptionAdviceId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? CommonAdviceId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}

