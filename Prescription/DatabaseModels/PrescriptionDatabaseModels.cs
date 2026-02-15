using System;

namespace Prescription.DatabaseModels
{
    /// <summary>
    /// Database model for Prescription_DeledeById stored procedure
    /// Parameters: @PrescriptionID INT
    /// </summary>
    public class PrescriptionDeleteModel
    {
        public int PrescriptionID { get; set; }
    }

    /// <summary>
    /// Database model for Prescription_Insert stored procedure
    /// Parameters: @PrescriptionId INT, @TenantId INT, @PatientId INT, @DoctorId INT,
    /// @IsHeader BIT = 0, @IsPreHand BIT = 0, @PatientFollowUpId INT = NULL, @PharmacyId INT = NULL,
    /// @IssueDate DATETIME, @ExpiryDate DATETIME = NULL, @Language NVARCHAR(50) = NULL,
    /// @StatusId INT, @FollowUpDate DATETIME = NULL, @IsArchived BIT = 0,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = 0,
    /// @AppointmentRefId INT = NULL, @PrescriptionCode NVARCHAR(100) = NULL
    /// </summary>
    public class PrescriptionInsertModel
    {
        public int PrescriptionId { get; set; }
        public int TenantId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public bool IsHeader { get; set; } = false;
        public bool IsPreHand { get; set; } = false;
        public int? PatientFollowUpId { get; set; }
        public int? PharmacyId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Language { get; set; }
        public int StatusId { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int? AppointmentRefId { get; set; }
        public string? PrescriptionCode { get; set; }
    }

    /// <summary>
    /// Database model for Prescription_Update stored procedure
    /// Parameters: @PrescriptionId INT, @TenantId INT = NULL, @PatientId INT = NULL, @DoctorId INT = NULL,
    /// @PatientFollowUpId INT = NULL, @PharmacyId INT = NULL, @IssueDate DATETIME = NULL,
    /// @ExpiryDate DATETIME = NULL, @Language NVARCHAR(10) = NULL, @StatusId INT = NULL,
    /// @FollowUpDate DATETIME = NULL, @IsArchived BIT = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL, @IsHeader BIT = 0, @AppointmentRefId INT = NULL
    /// </summary>
    public class PrescriptionUpdateModel
    {
        public int PrescriptionId { get; set; }
        public int? TenantId { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientFollowUpId { get; set; }
        public int? PharmacyId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Language { get; set; }
        public int? StatusId { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public bool? IsArchived { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsHeader { get; set; } = false;
        public int? AppointmentRefId { get; set; }
    }
}

