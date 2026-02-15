using System;

namespace Diseases.DatabaseModels
{
    /// <summary>
    /// Database model for Disease_DeleteById stored procedure
    /// Parameters: @DiseaseID INT
    /// </summary>
    public class DiseaseDeleteModel
    {
        public int DiseaseID { get; set; }
    }

    /// <summary>
    /// Database model for Disease_Insert stored procedure
    /// Parameters: @DiseaseID INT, @TenantId INT, @DiseaseName NVARCHAR(100),
    /// @Description NVARCHAR(255) = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL
    /// </summary>
    public class DiseaseInsertModel
    {
        public int DiseaseID { get; set; }
        public int TenantId { get; set; }
        public string DiseaseName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for Disease_Update stored procedure
    /// Parameters: @DiseaseID INT, @TenantId INT = NULL, @DiseaseName NVARCHAR(100) = NULL,
    /// @Description NVARCHAR(255) = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @CreatedAt DATETIME = NULL
    /// </summary>
    public class DiseaseUpdateModel
    {
        public int DiseaseID { get; set; }
        public int? TenantId { get; set; }
        public string? DiseaseName { get; set; }
        public string? Description { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

