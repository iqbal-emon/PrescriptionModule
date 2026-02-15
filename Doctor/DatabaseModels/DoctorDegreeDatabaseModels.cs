using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorDegree_DeleteById stored procedure
    /// Parameters: @DoctorDegreeID INT
    /// </summary>
    public class DoctorDegreeDeleteModel
    {
        public int DoctorDegreeID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorDegree_Insert stored procedure
    /// Parameters: @DoctorDegreeID INT, @TenantID INT, @DoctorID INT, @DegreeID INT,
    /// @PassingYear INT, @InstituteName NVARCHAR(255) = NULL, @InstituteID INT = NULL,
    /// @Country NVARCHAR(100) = NULL, @CountryID INT = NULL, @City NVARCHAR(100) = NULL,
    /// @CityID INT = NULL, @ZipCode NVARCHAR(20) = NULL, @ZipCodeID INT = NULL,
    /// @IsDeleted BIT = 0, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL
    /// Note: Uses SELECT SCOPE_IDENTITY() AS DoctorDegreeID
    /// </summary>
    public class DoctorDegreeInsertModel
    {
        public int DoctorDegreeID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public int DegreeID { get; set; }
        public int PassingYear { get; set; }
        public string? InstituteName { get; set; }
        public int? InstituteID { get; set; }
        public string? Country { get; set; }
        public int? CountryID { get; set; }
        public string? City { get; set; }
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Database model for DoctorDegree_Update stored procedure
    /// Parameters: @DoctorDegreeID INT, @TenantID INT = NULL, @DoctorID INT = NULL,
    /// @DegreeID INT = NULL, @PassingYear INT = NULL, @InstituteName NVARCHAR(255) = NULL,
    /// @InstituteID INT = NULL, @Country NVARCHAR(100) = NULL, @CountryID INT = NULL,
    /// @City NVARCHAR(100) = NULL, @CityID INT = NULL, @ZipCode NVARCHAR(20) = NULL,
    /// @ZipCodeID INT = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL,
    /// @UpdatedAt DATETIME = NULL
    /// Note: Uses SELECT @DoctorDegreeID AS UpdatedDoctorDegreeID
    /// </summary>
    public class DoctorDegreeUpdateModel
    {
        public int DoctorDegreeID { get; set; }
        public int? TenantID { get; set; }
        public int? DoctorID { get; set; }
        public int? DegreeID { get; set; }
        public int? PassingYear { get; set; }
        public string? InstituteName { get; set; }
        public int? InstituteID { get; set; }
        public string? Country { get; set; }
        public int? CountryID { get; set; }
        public string? City { get; set; }
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

