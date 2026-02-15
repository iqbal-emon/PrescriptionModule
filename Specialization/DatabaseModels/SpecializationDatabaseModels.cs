namespace Specialization.DatabaseModels
{
    /// <summary>
    /// Database model for Specialization_DeleteById stored procedure
    /// Parameters: @SpecializationID INT
    /// </summary>
    public class SpecializationDeleteModel
    {
        public int SpecializationID { get; set; }
    }

    /// <summary>
    /// Database model for Specialization_Insert stored procedure
    /// Parameters: @SpecializationName NVARCHAR(200), @Description NVARCHAR(500) = NULL,
    /// @SpecialityID INT = NULL, @TenantID INT, @SpecializationID INT OUTPUT
    /// </summary>
    public class SpecializationInsertModel
    {
        public string SpecializationName { get; set; }
        public string? Description { get; set; }
        public int? SpecialityID { get; set; }
        public int TenantID { get; set; }
        public int SpecializationID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for Specialization_Update stored procedure
    /// Parameters: @SpecializationID INT, @SpecializationName NVARCHAR(200) = NULL,
    /// @Description NVARCHAR(500) = NULL, @SpecialityID INT = NULL, @UpdatedSpecializationID INT OUTPUT
    /// </summary>
    public class SpecializationUpdateModel
    {
        public int SpecializationID { get; set; }
        public string? SpecializationName { get; set; }
        public string? Description { get; set; }
        public int? SpecialityID { get; set; }
        public int UpdatedSpecializationID { get; set; } // OUTPUT parameter
    }
}

