namespace FollowUp.DatabaseModels
{
    /// <summary>
    /// Database model for FollowUp_DeleteById stored procedure
    /// Parameters: @FollowUpId INT
    /// </summary>
    public class FollowUpDeleteModel
    {
        public int FollowUpId { get; set; }
    }

    /// <summary>
    /// Database model for FollowUp_Insert stored procedure
    /// Parameters: @TenantId INT, @Name NVARCHAR(100), @Description NVARCHAR(255) = NULL,
    /// @FollowUpId INT OUTPUT
    /// </summary>
    public class FollowUpInsertModel
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int FollowUpId { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for FollowUp_Update stored procedure
    /// Parameters: @FollowUpId INT, @TenantId INT, @Name NVARCHAR(100), @Description NVARCHAR(255) = NULL,
    /// @UpdatedId INT OUTPUT
    /// </summary>
    public class FollowUpUpdateModel
    {
        public int FollowUpId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

