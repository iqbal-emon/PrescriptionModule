using System;

namespace Doctor.DatabaseModels
{
    /// <summary>
    /// Database model for Doctor_DeleteById stored procedure
    /// Parameters: @DoctorID INT
    /// </summary>
    public class DoctorDeleteModel
    {
        public int DoctorID { get; set; }
    }

    /// <summary>
    /// Database model for Doctor_Insert stored procedure
    /// Parameters: @UserID INT = NULL, @DoctorID INT = NULL, @SpecialityID INT = NULL,
    /// @Specialization NVARCHAR(100) = NULL, @LicenseNumber NVARCHAR(50) = NULL,
    /// @DoctorReferenceID INT = NULL, @HospitalAffiliation NVARCHAR(100) = NULL,
    /// @Expertise NVARCHAR(500) = NULL, @ProfileStep INT = NULL, @BmdcRegNo NVARCHAR(50) = NULL,
    /// @BmdcRegExpiryDate DATETIME = NULL, @IdentityNumber NVARCHAR(50) = NULL,
    /// @City NVARCHAR(100) = NULL, @Country NVARCHAR(100) = NULL, @Address NVARCHAR(255) = NULL,
    /// @DoctorTitle INT = NULL, @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL,
    /// @IsDeleted BIT = NULL
    /// Note: Uses SELECT SCOPE_IDENTITY() AS DoctorID
    /// </summary>
    public class DoctorInsertModel
    {
        public int? UserID { get; set; }
        public int? DoctorID { get; set; }
        public int? SpecialityID { get; set; }
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public int? DoctorReferenceID { get; set; }
        public string? HospitalAffiliation { get; set; }
        public string? Expertise { get; set; }
        public int? ProfileStep { get; set; }
        public string? BmdcRegNo { get; set; }
        public DateTime? BmdcRegExpiryDate { get; set; }
        public string? IdentityNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? DoctorTitle { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }

    /// <summary>
    /// Database model for Doctor_Update stored procedure
    /// Parameters: @DoctorID INT, @UserID INT = NULL, @SpecialityID INT = NULL,
    /// @Specialization NVARCHAR(100) = NULL, @LicenseNumber NVARCHAR(50) = NULL,
    /// @DoctorReferenceID INT = NULL, @HospitalAffiliation NVARCHAR(100) = NULL,
    /// @Expertise NVARCHAR(500) = NULL, @ProfileStep INT = NULL,
    /// @FullName NVARCHAR(150) = NULL, @Email NVARCHAR(150) = NULL, @MobileNo NVARCHAR(15) = NULL,
    /// @PhoneNumber NVARCHAR(15) = NULL, @ContactNo NVARCHAR(20) = NULL,
    /// @BmdcRegNo NVARCHAR(50) = NULL, @BmdcRegExpiryDate DATETIME = NULL,
    /// @IdentityNumber NVARCHAR(50) = NULL, @City NVARCHAR(100) = NULL,
    /// @Country NVARCHAR(100) = NULL, @Address NVARCHAR(255) = NULL, @DoctorTitle INT = NULL,
    /// @UpdatedAt DATETIME = NULL, @IsDeleted BIT = NULL, @CreatedAt DATETIME = NULL
    /// Note: Uses SELECT @DoctorID AS UpdatedDoctorID
    /// </summary>
    public class DoctorUpdateModel
    {
        public int DoctorID { get; set; }
        public int? UserID { get; set; }
        public int? SpecialityID { get; set; }
        public string? Specialization { get; set; }
        public string? LicenseNumber { get; set; }
        public int? DoctorReferenceID { get; set; }
        public string? HospitalAffiliation { get; set; }
        public string? Expertise { get; set; }
        public int? ProfileStep { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ContactNo { get; set; }
        public string? BmdcRegNo { get; set; }
        public DateTime? BmdcRegExpiryDate { get; set; }
        public string? IdentityNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public int? DoctorTitle { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

