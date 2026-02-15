using System;

namespace Examinations.DatabaseModels
{
    /// <summary>
    /// Database model for Examinations_DeleteById stored procedure
    /// Parameters: @ExaminationID INT
    /// </summary>
    public class ExaminationsDeleteModel
    {
        public int ExaminationID { get; set; }
    }

    /// <summary>
    /// Database model for Examinations_Insert stored procedure
    /// Parameters: @ExaminationID INT, @TenantID INT, @PatientID INT, @DoctorID INT,
    /// @ExaminationDate DATETIME = NULL, @Findings NVARCHAR(4000) = NULL, @Notes NVARCHAR(255) = NULL,
    /// @BloodPressure NVARCHAR(255) = NULL, @Pulse NVARCHAR(255) = NULL, @Temperature NVARCHAR(255) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class ExaminationsInsertModel
    {
        public int ExaminationID { get; set; }
        public int TenantID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public string? Findings { get; set; }
        public string? Notes { get; set; }
        public string? BloodPressure { get; set; }
        public string? Pulse { get; set; }
        public string? Temperature { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for Examinations_Update stored procedure
    /// Parameters: @ExaminationId INT, @TenantId INT = NULL, @PatientId INT = NULL, @DoctorId INT = NULL,
    /// @ExaminationDate DATETIME = NULL, @Findings NVARCHAR(4000) = NULL, @Notes NVARCHAR(255) = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// Note: SP uses camelCase (ExaminationId, TenantId, PatientId, DoctorId) unlike Insert/Delete which use all caps
    /// </summary>
    public class ExaminationsUpdateModel
    {
        public int ExaminationId { get; set; }
        public int? TenantId { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? ExaminationDate { get; set; }
        public string? Findings { get; set; }
        public string? Notes { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

