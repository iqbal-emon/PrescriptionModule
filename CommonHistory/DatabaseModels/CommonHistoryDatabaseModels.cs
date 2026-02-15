namespace CommonHistory.DatabaseModels
{
    /// <summary>
    /// Database model for CommonHistory_DeleteById stored procedure
    /// Parameters: @CommonHistoryId INT
    /// </summary>
    public class CommonHistoryDeleteModel
    {
        public int CommonHistoryId { get; set; }
    }

    /// <summary>
    /// Database model for CommonHistory_Insert stored procedure
    /// Parameters: @Name NVARCHAR(500), @Description NVARCHAR(MAX) = NULL, @IsActive BIT = 1,
    /// @CommonHistoryId INT OUTPUT
    /// </summary>
    public class CommonHistoryInsertModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int CommonHistoryId { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for CommonHistory_Update stored procedure
    /// Parameters: @CommonHistoryId INT, @Name NVARCHAR(500), @Description NVARCHAR(MAX) = NULL,
    /// @IsActive BIT = 1, @UpdatedId INT OUTPUT
    /// </summary>
    public class CommonHistoryUpdateModel
    {
        public int CommonHistoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

