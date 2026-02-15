namespace Symptoms.DatabaseModels
{
    /// <summary>
    /// Database model for Symptom_DeleteById stored procedure
    /// Parameters: @SymptomID INT
    /// </summary>
    public class SymptomDeleteModel
    {
        public int SymptomID { get; set; }
    }

    /// <summary>
    /// Database model for Symptom_Insert stored procedure
    /// Parameters: @TenantID INT, @SymptomName NVARCHAR(100), @Description NVARCHAR(255) = NULL,
    /// @SymptomID INT OUTPUT
    /// </summary>
    public class SymptomInsertModel
    {
        public int TenantID { get; set; }
        public string SymptomName { get; set; }
        public string? Description { get; set; }
        public int SymptomID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for Symptom_Update stored procedure
    /// Parameters: @SymptomID INT, @TenantID INT, @SymptomName NVARCHAR(100), @Description NVARCHAR(255) = NULL,
    /// @UpdatedId INT OUTPUT
    /// </summary>
    public class SymptomUpdateModel
    {
        public int SymptomID { get; set; }
        public int TenantID { get; set; }
        public string SymptomName { get; set; }
        public string? Description { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

