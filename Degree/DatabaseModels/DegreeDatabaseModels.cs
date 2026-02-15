using System;

namespace Degree.DatabaseModels
{
    /// <summary>
    /// Database model for Degree_DeleteById stored procedure
    /// Parameters: @DegreeID INT
    /// </summary>
    public class DegreeDeleteModel
    {
        public int DegreeID { get; set; }
    }

    /// <summary>
    /// Database model for Degree_Insert stored procedure
    /// Parameters: @DegreeID INT, @TenantId INT, @DegreeName NVARCHAR(100) = NULL,
    /// @Duration NVARCHAR(50) = NULL, @DurationType NVARCHAR(50) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class DegreeInsertModel
    {
        public int DegreeID { get; set; }
        public int TenantId { get; set; }
        public string? DegreeName { get; set; }
        public int? Duration { get; set; } // SP uses NVARCHAR(50), not INT
        public string? DurationType { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for Degree_Update stored procedure
    /// Parameters: @DegreeID INT, @TenantId INT = NULL, @DegreeName NVARCHAR(100) = NULL,
    /// @Duration INT = NULL, @DurationType NVARCHAR(50) = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class DegreeUpdateModel
    {
        public int DegreeID { get; set; }
        public int? TenantId { get; set; }
        public string? DegreeName { get; set; }
        public int? Duration { get; set; }
        public string? DurationType { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

