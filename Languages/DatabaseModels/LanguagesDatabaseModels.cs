using System;

namespace Languages.DatabaseModels
{
    /// <summary>
    /// Database model for Languages_DeleteById stored procedure
    /// Parameters: @LanguageID INT
    /// </summary>
    public class LanguagesDeleteModel
    {
        public int LanguageID { get; set; }
    }

    /// <summary>
    /// Database model for Languages_Insert stored procedure
    /// Parameters: @LanguageID INT, @LanguageCode NVARCHAR(10), @LanguageName NVARCHAR(50),
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class LanguagesInsertModel
    {
        public int LanguageID { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for Languages_Update stored procedure
    /// Parameters: @LanguageID INT, @LanguageCode NVARCHAR(10) = NULL, @LanguageName NVARCHAR(50) = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class LanguagesUpdateModel
    {
        public int LanguageID { get; set; }
        public string? LanguageCode { get; set; }
        public string? LanguageName { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

