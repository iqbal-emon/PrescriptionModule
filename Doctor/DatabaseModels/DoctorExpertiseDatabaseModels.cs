using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorExpertise_DeleteById stored procedure
    /// Parameters: @DoctorExpertiseID INT
    /// </summary>
    public class DoctorExpertiseDeleteModel
    {
        public int DoctorExpertiseID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorExpertise_Insert stored procedure
    /// Parameters: @DoctorExpertiseID INT, @TenantID INT, @DoctorID INT, @ExpertiseID INT,
    /// @ExperienceYears INT = NULL, @Certification NVARCHAR(255) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// Note: Uses SELECT SCOPE_IDENTITY() AS DoctorExpertiseID
    /// </summary>
    public class DoctorExpertiseInsertModel
    {
        public int DoctorExpertiseID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public int ExpertiseID { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Certification { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for DoctorExpertise_Update stored procedure
    /// Parameters: @DoctorExpertiseID INT, @TenantID INT = NULL, @DoctorID INT = NULL,
    /// @ExpertiseID INT = NULL, @ExperienceYears INT = NULL, @Certification NVARCHAR(255) = NULL,
    /// @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL
    /// Note: Uses SELECT @DoctorExpertiseID AS UpdatedDoctorExpertiseID
    /// </summary>
    public class DoctorExpertiseUpdateModel
    {
        public int DoctorExpertiseID { get; set; }
        public int? TenantID { get; set; }
        public int? DoctorID { get; set; }
        public int? ExpertiseID { get; set; }
        public int? ExperienceYears { get; set; }
        public string? Certification { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

