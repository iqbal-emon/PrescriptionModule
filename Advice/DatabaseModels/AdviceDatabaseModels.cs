using System;

namespace Advice.DatabaseModels
{
    /// <summary>
    /// Database model for CommonAdvices_DeleteById stored procedure
    /// Parameters: @CommonAdviceID INT
    /// </summary>
    public class CommonAdviceDeleteModel
    {
        public int CommonAdviceID { get; set; }
    }

    /// <summary>
    /// Database model for CommonAdvices_Insert stored procedure
    /// Parameters: @Advice NVARCHAR(MAX), @Type NVARCHAR(50) = NULL, @IsActive BIT = 1,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted bit = 0,
    /// @Description nvarchar(500) = null, @CommonAdviceID INT OUTPUT
    /// </summary>
    public class CommonAdviceInsertModel
    {
        public string Advice { get; set; }
        public string? Type { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Description { get; set; }
        public int CommonAdviceID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for CommonAdvices_Update stored procedure
    /// Parameters: @CommonAdviceID INT, @Advice NVARCHAR(MAX), @Type NVARCHAR(50) = NULL,
    /// @IsActive BIT = 1, @UpdatedId INT OUTPUT
    /// </summary>
    public class CommonAdviceUpdateModel
    {
        public int CommonAdviceID { get; set; }
        public string Advice { get; set; }
        public string? Type { get; set; }
        public bool IsActive { get; set; } = true;
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

