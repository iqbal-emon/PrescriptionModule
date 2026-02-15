using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionInvestigation_DeleteById stored procedure
    /// Parameters: @PrescriptionInvestigationID INT
    /// </summary>
    public class PrescriptionInvestigationDeleteModel
    {
        public int PrescriptionInvestigationID { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionInvestigation_Insert stored procedure
    /// Parameters: @PrescriptionInvestigationID INT, @InvestigationID INT, @PrescriptionID INT,
    /// @Description NVARCHAR(255) = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL, @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionInvestigationInsertModel
    {
        public int PrescriptionInvestigationID { get; set; }
        public int InvestigationID { get; set; }
        public int PrescriptionID { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionInvestigation_Update stored procedure
    /// Parameters: @PrescriptionInvestigationId INT, @InvestigationID INT, @PrescriptionID INT = NULL,
    /// @Description NVARCHAR(255) = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @IsActive BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class PrescriptionInvestigationUpdateModel
    {
        public int PrescriptionInvestigationId { get; set; }
        public int InvestigationID { get; set; }
        public int? PrescriptionID { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

