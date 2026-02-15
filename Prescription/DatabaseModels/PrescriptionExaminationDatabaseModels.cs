using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionExamination_DeleteById stored procedure
    /// Parameters: @PrescriptionExaminationID INT
    /// </summary>
    public class PrescriptionExaminationDeleteModel
    {
        public int PrescriptionExaminationID { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionExamination_Insert stored procedure
    /// Parameters: @PrescriptionExaminationID INT, @PrescriptionID INT, @ExaminationID INT,
    /// @Description NVARCHAR(255) = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL
    /// </summary>
    public class PrescriptionExaminationInsertModel
    {
        public int PrescriptionExaminationID { get; set; }
        public int PrescriptionID { get; set; }
        public int ExaminationID { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionExamination_Update stored procedure
    /// Parameters: @PrescriptionExaminationID INT, @PrescriptionID INT = NULL, @ExaminationID INT = NULL,
    /// @Description NVARCHAR(255) = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @CreatedAt DATETIME = NULL
    /// </summary>
    public class PrescriptionExaminationUpdateModel
    {
        public int PrescriptionExaminationID { get; set; }
        public int? PrescriptionID { get; set; }
        public int? ExaminationID { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

