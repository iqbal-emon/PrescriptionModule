using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorSpecialization_DeleteById stored procedure
    /// Parameters: @DoctorSpecializationID INT
    /// </summary>
    public class DoctorSpecializationDeleteModel
    {
        public int DoctorSpecializationID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorSpecialization_Insert stored procedure
    /// Parameters: @DoctorID INT, @SpecialityID INT = NULL, @SpecializationID INT = NULL,
    /// @ServiceDetails NVARCHAR(500) = NULL, @DocumentName NVARCHAR(200) = NULL,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL,
    /// @DoctorSpecializationID INT OUTPUT
    /// </summary>
    public class DoctorSpecializationInsertModel
    {
        public int DoctorID { get; set; }
        public int? SpecialityID { get; set; }
        public int? SpecializationID { get; set; }
        public string? ServiceDetails { get; set; }
        public string? DocumentName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public int DoctorSpecializationID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for DoctorSpecialization_Update stored procedure
    /// Parameters: @DoctorSpecializationID INT, @DoctorID INT, @SpecialityID INT = NULL,
    /// @SpecializationID INT = NULL, @ServiceDetails NVARCHAR(500) = NULL,
    /// @DocumentName NVARCHAR(200) = NULL, @UpdatedId INT OUTPUT
    /// </summary>
    public class DoctorSpecializationUpdateModel
    {
        public int DoctorSpecializationID { get; set; }
        public int DoctorID { get; set; }
        public int? SpecialityID { get; set; }
        public int? SpecializationID { get; set; }
        public string? ServiceDetails { get; set; }
        public string? DocumentName { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

