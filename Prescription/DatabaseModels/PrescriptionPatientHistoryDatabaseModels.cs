using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionPatientHistory_DeleteById stored procedure
    /// Parameters: @PrescriptionPatientHistoryID INT
    /// </summary>
    public class PrescriptionPatientHistoryDeleteModel
    {
        public int PrescriptionPatientHistoryID { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionPatientHistory_Insert stored procedure
    /// Parameters: @PrescriptionPatientHistoryID INT, @CommonHistoryID INT, @PrescriptionID INT,
    /// @Description NVARCHAR(250) = NULL, @PresentHistory NVARCHAR(250) = NULL,
    /// @PastHistory NVARCHAR(250) = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL, @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionPatientHistoryInsertModel
    {
        public int PrescriptionPatientHistoryID { get; set; }
        public int CommonHistoryID { get; set; }
        public int PrescriptionID { get; set; }
        public string? Description { get; set; }
        public string? PresentHistory { get; set; }
        public string? PastHistory { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionPatientHistory_Update stored procedure
    /// Parameters: @PrescriptionPatientHistoryID INT, @CommonHistoryID INT = NULL, @PrescriptionID INT = NULL,
    /// @Description NVARCHAR(250) = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @CreatedAt DATETIME = NULL, @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionPatientHistoryUpdateModel
    {
        public int PrescriptionPatientHistoryID { get; set; }
        public int? CommonHistoryID { get; set; }
        public int? PrescriptionID { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
    }
}

