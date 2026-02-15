using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionDiagonosis_DeleteById stored procedure
    /// Parameters: @DiagnosisId INT
    /// </summary>
    public class PrescriptionDiagonsisDeleteModel
    {
        public int DiagnosisId { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionDiagnoses_Insert stored procedure
    /// Parameters: @PrescriptionDiagnosisId INT, @PrescriptionId INT, @DiagnosisID INT,
    /// @Notes NVARCHAR(255) = NULL, @PastDiagnosis NVARCHAR(255) = NULL, @PresentDiagnosis NVARCHAR(255) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class PrescriptionDiagonsisInsertModel
    {
        public int PrescriptionDiagnosisId { get; set; }
        public int PrescriptionId { get; set; }
        public int DiagnosisID { get; set; }
        public string? Notes { get; set; }
        public string? PastDiagnosis { get; set; }
        public string? PresentDiagnosis { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionDiagnoses_Update stored procedure
    /// Parameters: @PrescriptionDiagnosisId INT, @PrescriptionId INT = NULL, @DiagnosisId INT = NULL,
    /// @Notes NVARCHAR(255) = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @CreatedAt DATETIME = NULL
    /// </summary>
    public class PrescriptionDiagonsisUpdateModel
    {
        public int PrescriptionDiagnosisId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? DiagnosisId { get; set; }
        public string? Notes { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

