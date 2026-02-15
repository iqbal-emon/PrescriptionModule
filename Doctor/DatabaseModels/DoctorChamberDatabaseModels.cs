using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for DoctorChamber_DeleteById stored procedure
    /// Parameters: @ChamberID INT
    /// </summary>
    public class DoctorChamberDeleteModel
    {
        public int ChamberID { get; set; }
    }

    /// <summary>
    /// Database model for DoctorChamber_Insert stored procedure
    /// Parameters: @ChamberID INT, @TenantID INT, @DoctorID INT, @ChamberName NVARCHAR(100) = NULL,
    /// @Address NVARCHAR(255) = NULL, @Country NVARCHAR(100) = NULL, @CountryID INT = NULL,
    /// @City NVARCHAR(100) = NULL, @CityID INT = NULL, @ZipCode NVARCHAR(20) = NULL,
    /// @ZipCodeID INT = NULL, @IsVisibleOnPrescription BIT = 1, @ChamberReferenceId INT = NULL,
    /// @DistrictId INT = NULL, @DivisionId INT = NULL, @CreatedAt DATETIME = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = 0
    /// Note: Uses SELECT SCOPE_IDENTITY() AS ChamberID
    /// </summary>
    public class DoctorChamberInsertModel
    {
        public int ChamberID { get; set; }
        public int TenantID { get; set; }
        public int DoctorID { get; set; }
        public string? ChamberName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public int? CountryID { get; set; }
        public string? City { get; set; }
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool IsVisibleOnPrescription { get; set; } = true;
        public int? ChamberReferenceId { get; set; }
        public int? DistrictId { get; set; }
        public int? DivisionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    /// <summary>
    /// Database model for DoctorChamber_Update stored procedure
    /// Parameters: @ChamberID INT, @TenantID INT = NULL, @DoctorID INT = NULL,
    /// @ChamberName NVARCHAR(100) = NULL, @Address NVARCHAR(255) = NULL,
    /// @Country NVARCHAR(100) = NULL, @CountryID INT = NULL, @City NVARCHAR(100) = NULL,
    /// @CityID INT = NULL, @ZipCode NVARCHAR(20) = NULL, @ZipCodeID INT = NULL,
    /// @IsVisibleOnPrescription BIT = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// Note: Uses SELECT @ChamberID AS UpdatedChamberID
    /// </summary>
    public class DoctorChamberUpdateModel
    {
        public int ChamberID { get; set; }
        public int? TenantID { get; set; }
        public int? DoctorID { get; set; }
        public string? ChamberName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public int? CountryID { get; set; }
        public string? City { get; set; }
        public int? CityID { get; set; }
        public string? ZipCode { get; set; }
        public int? ZipCodeID { get; set; }
        public bool? IsVisibleOnPrescription { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

