using System;

namespace Diagonosis.DatabaseModels
{
    /// <summary>
    /// Database model for Diagonosis_DeleteById stored procedure
    /// Parameters: @DiagnosisId INT (Note: SP uses @DiagnosisId, not @DiagonosisID)
    /// </summary>
    public class DiagonosisDeleteModel
    {
        public int DiagnosisId { get; set; }
    }

    /// <summary>
    /// Database model for Diagonosis_Insert stored procedure
    /// Parameters: @Name NVARCHAR(500), @Description NVARCHAR(MAX) = NULL, @Code NVARCHAR(50) = NULL,
    /// @IsActive BIT = 1, @DiagonosisID INT OUTPUT
    /// </summary>
    public class DiagonosisInsertModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int DiagonosisID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for Diagnosis_Update stored procedure (Note: SP name is Diagnosis_Update, not Diagonosis_Update)
    /// Parameters: @DiagonosisID INT, @Name NVARCHAR(500), @Description NVARCHAR(MAX) = NULL,
    /// @Code NVARCHAR(50) = NULL, @IsActive BIT = 1, @UpdatedId INT OUTPUT
    /// </summary>
    public class DiagonosisUpdateModel
    {
        public int DiagonosisID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

