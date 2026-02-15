namespace User.DatabaseModels
{
    /// <summary>
    /// Database model for User_DeleteById stored procedure
    /// Parameters: @UserID INT
    /// </summary>
    public class UserDeleteModel
    {
        public int UserID { get; set; }
    }

    /// <summary>
    /// Database model for User_Insert stored procedure
    /// Parameters: @TenantID INT, @FirstName NVARCHAR(50) = NULL, @LastName NVARCHAR(50) = NULL,
    /// @FullName NVARCHAR(150), @UserName NVARCHAR(100), @Email NVARCHAR(150) = NULL,
    /// @PasswordHash NVARCHAR(255), @UserType NVARCHAR(20) = NULL, @PhoneNumber NVARCHAR(15) = NULL,
    /// @ContactNo NVARCHAR(20) = NULL, @RoleId INT, @IsActive BIT = 1, @IsDeleted BIT = 0,
    /// @CreatedAt DATETIME = NULL, @UpdatedAt DATETIME = NULL, @ReferenceUserId INT = NULL,
    /// @UserID INT OUTPUT
    /// </summary>
    public class UserInsertModel
    {
        public int TenantID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public string? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ContactNo { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ReferenceUserId { get; set; }
        public int UserID { get; set; } // OUTPUT parameter
    }

    /// <summary>
    /// Database model for User_Update stored procedure
    /// Parameters: @UserID INT, @TenantID INT, @FirstName NVARCHAR(50) = NULL, @LastName NVARCHAR(50) = NULL,
    /// @FullName NVARCHAR(150), @UserName NVARCHAR(100), @Email NVARCHAR(150) = NULL,
    /// @PasswordHash NVARCHAR(255) = NULL, @UserType NVARCHAR(20) = NULL, @PhoneNumber NVARCHAR(15) = NULL,
    /// @ContactNo NVARCHAR(20) = NULL, @RoleId INT, @IsActive BIT = 1, @IsDeleted BIT = NULL,
    /// @ReferenceUserId INT = NULL, @UpdatedId INT OUTPUT
    /// </summary>
    public class UserUpdateModel
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ContactNo { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool? IsDeleted { get; set; }
        public int? ReferenceUserId { get; set; }
        public int UpdatedId { get; set; } // OUTPUT parameter
    }
}

