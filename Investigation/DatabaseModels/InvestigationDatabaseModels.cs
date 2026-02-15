using System;

namespace Investigation.DatabaseModels
{
    /// <summary>
    /// Database model for Investigation_DeleteById stored procedure
    /// Parameters: @InvestigationID INT
    /// </summary>
    public class InvestigationDeleteModel
    {
        public int InvestigationID { get; set; }
    }

    /// <summary>
    /// Database model for Investigation_Insert stored procedure
    /// Parameters: @Name NVARCHAR(500), @Description NVARCHAR(MAX) = NULL,
    /// @Code NVARCHAR(50) = NULL, @IsActive BIT = 1, @InvestigationID INT OUTPUT
    /// </summary>
    public class InvestigationInsertModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int InvestigationID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for Investigation_Update stored procedure
    /// Parameters: @InvestigationID INT, @Name NVARCHAR(500), @Description NVARCHAR(MAX) = NULL,
    /// @Code NVARCHAR(50) = NULL, @IsActive BIT = 1, @UpdatedId INT OUTPUT
    /// </summary>
    public class InvestigationUpdateModel
    {
        public int InvestigationID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

