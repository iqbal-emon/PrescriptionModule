using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for PrescriptionFollowUp_DeleteById stored procedure
    /// Parameters: @PrescriptionFollowUpID INT
    /// </summary>
    public class PrescriptionFollowUpDeleteModel
    {
        public int PrescriptionFollowUpID { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionFollowUp_Insert stored procedure
    /// Parameters: @PrescriptionFollowUpID INT, @PrescriptionID INT, @FollowUpID INT,
    /// @Description NVARCHAR(255) = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL, @IsActive BIT = NULL
    /// </summary>
    public class PrescriptionFollowUpInsertModel
    {
        public int PrescriptionFollowUpID { get; set; }
        public int PrescriptionID { get; set; }
        public int FollowUpID { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Database model for PrescriptionFollowUp_Update stored procedure
    /// Parameters: @PrescriptionFollowUpId INT, @followUpId INT, @PrescriptionId INT = NULL,
    /// @Description NVARCHAR(255) = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @IsActive BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class PrescriptionFollowUpUpdateModel
    {
        public int PrescriptionFollowUpId { get; set; }
        public int followUpId { get; set; }
        public int? PrescriptionId { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

