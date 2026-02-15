using System;

namespace AdviceTranslations.DatabaseModels
{
    /// <summary>
    /// Database model for AdviceTranslations_DeleteById stored procedure
    /// Parameters: @TranslationId INT
    /// </summary>
    public class AdviceTranslationsDeleteModel
    {
        public int TranslationId { get; set; }
    }

    /// <summary>
    /// Database model for AdviceTranslations_Insert stored procedure
    /// Parameters: @TranslationId INT, @AdviceId INT, @LanguageId INT, @TranslatedAdvice NVARCHAR(MAX), 
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL
    /// </summary>
    public class AdviceTranslationsInsertModel
    {
        public int TranslationId { get; set; }
        public int AdviceId { get; set; }
        public int LanguageId { get; set; }
        public string TranslatedAdvice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for AdviceTranslations_Update stored procedure
    /// Parameters: @TranslationId INT, @AdviceId INT = NULL, @LanguageId INT = NULL, 
    /// @TranslatedAdvice NVARCHAR(MAX) = NULL, @UpdatedAt DATETIME = NULL, 
    /// @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// </summary>
    public class AdviceTranslationsUpdateModel
    {
        public int TranslationId { get; set; }
        public int? AdviceId { get; set; }
        public int? LanguageId { get; set; }
        public string? TranslatedAdvice { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    /// <summary>
    /// Database model for AdviceTranslations_GetById stored procedure
    /// Parameters: @AdviceTranslationsId INT
    /// </summary>
    public class AdviceTranslationsGetByIdModel
    {
        public int AdviceTranslationsId { get; set; }
    }
}

