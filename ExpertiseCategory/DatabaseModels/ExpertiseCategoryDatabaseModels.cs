using System;

namespace ExpertiseCategory.DatabaseModels
{
    /// <summary>
    /// Database model for ExpertiseCategory_DeleteById stored procedure
    /// Parameters: @ExpertiseID INT
    /// </summary>
    public class ExpertiseCategoryDeleteModel
    {
        public int ExpertiseID { get; set; }
    }

    /// <summary>
    /// Database model for ExpertiseCategory_Insert stored procedure
    /// Parameters: @ExpertiseID INT OUTPUT, @TenantID INT, @ExpertiseName NVARCHAR(255),
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class ExpertiseCategoryInsertModel
    {
        public int TenantID { get; set; }
        public string ExpertiseName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public int ExpertiseID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for ExpertiseCategory_Update stored procedure
    /// Parameters: @ExpertiseID INT, @TenantID INT = NULL, @ExpertiseName NVARCHAR(255) = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class ExpertiseCategoryUpdateModel
    {
        public int ExpertiseID { get; set; }
        public int? TenantID { get; set; }
        public string? ExpertiseName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

