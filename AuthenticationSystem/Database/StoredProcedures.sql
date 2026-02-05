USE [PrescriptoAuth]
GO

-- =============================================
-- Company Stored Procedures
-- =============================================

-- Company_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Company_GetAll]
GO

CREATE PROCEDURE [dbo].[Company_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [Name],
        [LicenseNo],
        [DrugRegCertificate],
        [Address],
        [ContactNo],
        [Email],
        [CurrencySymbol],
        [CreatedAt]
    FROM [dbo].[Company]
    ORDER BY [CreatedAt] DESC;
END
GO

-- Company_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Company_GetById]
GO

CREATE PROCEDURE [dbo].[Company_GetById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [Name],
        [LicenseNo],
        [DrugRegCertificate],
        [Address],
        [ContactNo],
        [Email],
        [CurrencySymbol],
        [CreatedAt]
    FROM [dbo].[Company]
    WHERE [Id] = @Id;
END
GO

-- Company_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Company_Insert]
GO

CREATE PROCEDURE [dbo].[Company_Insert]
    @Name NVARCHAR(200),
    @LicenseNo NVARCHAR(100) = NULL,
    @DrugRegCertificate NVARCHAR(100) = NULL,
    @Address NVARCHAR(MAX) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    @Email NVARCHAR(150) = NULL,
    @CurrencySymbol NVARCHAR(10) = '?',
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Company]
    (
        [Name],
        [LicenseNo],
        [DrugRegCertificate],
        [Address],
        [ContactNo],
        [Email],
        [CurrencySymbol],
        [CreatedAt]
    )
    VALUES
    (
        @Name,
        @LicenseNo,
        @DrugRegCertificate,
        @Address,
        @ContactNo,
        @Email,
        @CurrencySymbol,
        GETDATE()
    );
    SET @Id = SCOPE_IDENTITY();
END
GO

-- Company_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Company_Update]
GO

CREATE PROCEDURE [dbo].[Company_Update]
    @Id INT,
    @Name NVARCHAR(200),
    @LicenseNo NVARCHAR(100) = NULL,
    @DrugRegCertificate NVARCHAR(100) = NULL,
    @Address NVARCHAR(MAX) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    @Email NVARCHAR(150) = NULL,
    @CurrencySymbol NVARCHAR(10) = '?',
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Company]
    SET 
        [Name] = @Name,
        [LicenseNo] = @LicenseNo,
        [DrugRegCertificate] = @DrugRegCertificate,
        [Address] = @Address,
        [ContactNo] = @ContactNo,
        [Email] = @Email,
        [CurrencySymbol] = @CurrencySymbol
    WHERE [Id] = @Id;
    SET @UpdatedId = @Id;
END
GO

-- Company_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Company_DeleteById]
GO

CREATE PROCEDURE [dbo].[Company_DeleteById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Company]
    WHERE [Id] = @Id;
END
GO

-- =============================================
-- CompanyBranch Stored Procedures
-- =============================================

-- CompanyBranch_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CompanyBranch_GetAll]
GO

CREATE PROCEDURE [dbo].[CompanyBranch_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [CompanyId],
        [Name],
        [Address],
        [ContactNo],
        [IsActive],
        [CreatedAt]
    FROM [dbo].[CompanyBranch]
    ORDER BY [CreatedAt] DESC;
END
GO

-- CompanyBranch_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CompanyBranch_GetById]
GO

CREATE PROCEDURE [dbo].[CompanyBranch_GetById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [CompanyId],
        [Name],
        [Address],
        [ContactNo],
        [IsActive],
        [CreatedAt]
    FROM [dbo].[CompanyBranch]
    WHERE [Id] = @Id;
END
GO

-- CompanyBranch_GetByCompanyId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch_GetByCompanyId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CompanyBranch_GetByCompanyId]
GO

CREATE PROCEDURE [dbo].[CompanyBranch_GetByCompanyId]
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [CompanyId],
        [Name],
        [Address],
        [ContactNo],
        [IsActive],
        [CreatedAt]
    FROM [dbo].[CompanyBranch]
    WHERE [CompanyId] = @CompanyId
    ORDER BY [CreatedAt] DESC;
END
GO

-- CompanyBranch_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CompanyBranch_Insert]
GO

CREATE PROCEDURE [dbo].[CompanyBranch_Insert]
    @CompanyId INT,
    @Name NVARCHAR(150),
    @Address NVARCHAR(MAX) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    @IsActive BIT = 1,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[CompanyBranch]
    (
        [CompanyId],
        [Name],
        [Address],
        [ContactNo],
        [IsActive],
        [CreatedAt]
    )
    VALUES
    (
        @CompanyId,
        @Name,
        @Address,
        @ContactNo,
        @IsActive,
        GETDATE()
    );
    SET @Id = SCOPE_IDENTITY();
END
GO

-- CompanyBranch_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CompanyBranch_Update]
GO

CREATE PROCEDURE [dbo].[CompanyBranch_Update]
    @Id INT,
    @CompanyId INT,
    @Name NVARCHAR(150),
    @Address NVARCHAR(MAX) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CompanyBranch]
    SET 
        [CompanyId] = @CompanyId,
        [Name] = @Name,
        [Address] = @Address,
        [ContactNo] = @ContactNo,
        [IsActive] = @IsActive
    WHERE [Id] = @Id;
    SET @UpdatedId = @Id;
END
GO

-- CompanyBranch_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CompanyBranch_DeleteById]
GO

CREATE PROCEDURE [dbo].[CompanyBranch_DeleteById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[CompanyBranch]
    WHERE [Id] = @Id;
END
GO

-- =============================================
-- Permission Stored Procedures
-- =============================================

-- Permission_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Permission_GetAll]
GO

CREATE PROCEDURE [dbo].[Permission_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [DisplayName],
        [PermissionValue]
    FROM [dbo].[Permissions]
    ORDER BY [DisplayName];
END
GO

-- Permission_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Permission_GetById]
GO

CREATE PROCEDURE [dbo].[Permission_GetById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [DisplayName],
        [PermissionValue]
    FROM [dbo].[Permissions]
    WHERE [Id] = @Id;
END
GO

-- Permission_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Permission_Insert]
GO

CREATE PROCEDURE [dbo].[Permission_Insert]
    @DisplayName NVARCHAR(150),
    @PermissionValue NVARCHAR(500),
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Permissions]
    (
        [DisplayName],
        [PermissionValue]
    )
    VALUES
    (
        @DisplayName,
        @PermissionValue
    );
    SET @Id = SCOPE_IDENTITY();
END
GO

-- Permission_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Permission_Update]
GO

CREATE PROCEDURE [dbo].[Permission_Update]
    @Id INT,
    @DisplayName NVARCHAR(150),
    @PermissionValue NVARCHAR(500),
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Permissions]
    SET 
        [DisplayName] = @DisplayName,
        [PermissionValue] = @PermissionValue
    WHERE [Id] = @Id;
    SET @UpdatedId = @Id;
END
GO

-- Permission_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Permission_DeleteById]
GO

CREATE PROCEDURE [dbo].[Permission_DeleteById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Permissions]
    WHERE [Id] = @Id;
END
GO

-- =============================================
-- Role Stored Procedures
-- =============================================

-- Role_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Role_GetAll]
GO

CREATE PROCEDURE [dbo].[Role_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [Name],
        [IsDefault],
        [IsActive]
    FROM [dbo].[Roles]
    ORDER BY [Name];
END
GO

-- Role_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Role_GetById]
GO

CREATE PROCEDURE [dbo].[Role_GetById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [Name],
        [IsDefault],
        [IsActive]
    FROM [dbo].[Roles]
    WHERE [Id] = @Id;
END
GO

-- Role_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Role_Insert]
GO

CREATE PROCEDURE [dbo].[Role_Insert]
    @Name NVARCHAR(100),
    @IsDefault BIT = 0,
    @IsActive BIT = 1,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Roles]
    (
        [Name],
        [IsDefault],
        [IsActive]
    )
    VALUES
    (
        @Name,
        @IsDefault,
        @IsActive
    );
    SET @Id = SCOPE_IDENTITY();
END
GO

-- Role_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Role_Update]
GO

CREATE PROCEDURE [dbo].[Role_Update]
    @Id INT,
    @Name NVARCHAR(100),
    @IsDefault BIT = 0,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Roles]
    SET 
        [Name] = @Name,
        [IsDefault] = @IsDefault,
        [IsActive] = @IsActive
    WHERE [Id] = @Id;
    SET @UpdatedId = @Id;
END
GO

-- Role_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Role_DeleteById]
GO

CREATE PROCEDURE [dbo].[Role_DeleteById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Roles]
    WHERE [Id] = @Id;
END
GO

-- =============================================
-- RolePermission Stored Procedures
-- =============================================

-- RolePermission_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_GetAll]
GO

CREATE PROCEDURE [dbo].[RolePermission_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [RoleId],
        [PermissionId]
    FROM [dbo].[RolePermission]
    ORDER BY [RoleId], [PermissionId];
END
GO

-- RolePermission_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_GetById]
GO

CREATE PROCEDURE [dbo].[RolePermission_GetById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [RoleId],
        [PermissionId]
    FROM [dbo].[RolePermission]
    WHERE [Id] = @Id;
END
GO

-- RolePermission_GetByRoleId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_GetByRoleId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_GetByRoleId]
GO

CREATE PROCEDURE [dbo].[RolePermission_GetByRoleId]
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [RoleId],
        [PermissionId]
    FROM [dbo].[RolePermission]
    WHERE [RoleId] = @RoleId;
END
GO

-- RolePermission_GetByPermissionId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_GetByPermissionId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_GetByPermissionId]
GO

CREATE PROCEDURE [dbo].[RolePermission_GetByPermissionId]
    @PermissionId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [RoleId],
        [PermissionId]
    FROM [dbo].[RolePermission]
    WHERE [PermissionId] = @PermissionId;
END
GO

-- RolePermission_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_Insert]
GO

CREATE PROCEDURE [dbo].[RolePermission_Insert]
    @RoleId INT,
    @PermissionId INT,
    @Id INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[RolePermission]
    (
        [RoleId],
        [PermissionId]
    )
    VALUES
    (
        @RoleId,
        @PermissionId
    );
    SET @Id = SCOPE_IDENTITY();
END
GO

-- RolePermission_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_Update]
GO

CREATE PROCEDURE [dbo].[RolePermission_Update]
    @Id INT,
    @RoleId INT,
    @PermissionId INT,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[RolePermission]
    SET 
        [RoleId] = @RoleId,
        [PermissionId] = @PermissionId
    WHERE [Id] = @Id;
    SET @UpdatedId = @Id;
END
GO

-- RolePermission_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[RolePermission_DeleteById]
GO

CREATE PROCEDURE [dbo].[RolePermission_DeleteById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[RolePermission]
    WHERE [Id] = @Id;
END
GO

-- =============================================
-- User Stored Procedures
-- =============================================

-- User_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetAll]
GO

CREATE PROCEDURE [dbo].[User_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [IsDeleted] = 0 OR [IsDeleted] IS NULL
    ORDER BY [CreatedAt] DESC;
END
GO

-- User_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetById]
GO

CREATE PROCEDURE [dbo].[User_GetById]
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [UserID] = @UserID;
END
GO

-- User_GetByUserName
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetByUserName]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetByUserName]
GO

CREATE PROCEDURE [dbo].[User_GetByUserName]
    @UserName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [UserName] = @UserName
        AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL);
END
GO

-- User_GetByEmail
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetByEmail]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetByEmail]
GO

CREATE PROCEDURE [dbo].[User_GetByEmail]
    @Email NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [Email] = @Email
        AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL);
END
GO

-- User_GetByRoleId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetByRoleId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetByRoleId]
GO

CREATE PROCEDURE [dbo].[User_GetByRoleId]
    @RoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [RoleId] = @RoleId
        AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL)
    ORDER BY [CreatedAt] DESC;
END
GO

-- User_GetByTenantId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetByTenantId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetByTenantId]
GO

CREATE PROCEDURE [dbo].[User_GetByTenantId]
    @TenantID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [TenantID] = @TenantID
        AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL)
    ORDER BY [CreatedAt] DESC;
END
GO

-- User_GetByUserType
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_GetByUserType]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_GetByUserType]
GO

CREATE PROCEDURE [dbo].[User_GetByUserType]
    @UserType NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [UserID],
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [UpdatedAt],
        [ReferenceUserId]
    FROM [dbo].[Users]
    WHERE [UserType] = @UserType
        AND ([IsDeleted] = 0 OR [IsDeleted] IS NULL)
    ORDER BY [CreatedAt] DESC;
END
GO

-- User_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_Insert]
GO

CREATE PROCEDURE [dbo].[User_Insert]
    @TenantID INT,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @FullName NVARCHAR(150),
    @UserName NVARCHAR(100),
    @Email NVARCHAR(150) = NULL,
    @PasswordHash NVARCHAR(255),
    @UserType NVARCHAR(20) = NULL,
    @PhoneNumber NVARCHAR(15) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    @RoleId INT,
    @IsActive BIT = 1,
    @ReferenceUserId INT = NULL,
    @UserID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Users]
    (
        [TenantID],
        [FirstName],
        [LastName],
        [FullName],
        [UserName],
        [Email],
        [PasswordHash],
        [UserType],
        [PhoneNumber],
        [ContactNo],
        [RoleId],
        [IsActive],
        [IsDeleted],
        [CreatedAt],
        [ReferenceUserId]
    )
    VALUES
    (
        @TenantID,
        @FirstName,
        @LastName,
        @FullName,
        @UserName,
        @Email,
        @PasswordHash,
        @UserType,
        @PhoneNumber,
        @ContactNo,
        @RoleId,
        @IsActive,
        0,
        GETDATE(),
        @ReferenceUserId
    );
    SET @UserID = SCOPE_IDENTITY();
END
GO

-- User_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_Update]
GO

CREATE PROCEDURE [dbo].[User_Update]
    @UserID INT,
    @TenantID INT,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @FullName NVARCHAR(150),
    @UserName NVARCHAR(100),
    @Email NVARCHAR(150) = NULL,
    @PasswordHash NVARCHAR(255) = NULL,
    @UserType NVARCHAR(20) = NULL,
    @PhoneNumber NVARCHAR(15) = NULL,
    @ContactNo NVARCHAR(20) = NULL,
    @RoleId INT,
    @IsActive BIT = 1,
    @IsDeleted BIT = NULL,
    @ReferenceUserId INT = NULL,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Users]
    SET 
        [TenantID] = @TenantID,
        [FirstName] = @FirstName,
        [LastName] = @LastName,
        [FullName] = @FullName,
        [UserName] = @UserName,
        [Email] = @Email,
        [PasswordHash] = ISNULL(@PasswordHash, [PasswordHash]),
        [UserType] = @UserType,
        [PhoneNumber] = @PhoneNumber,
        [ContactNo] = @ContactNo,
        [RoleId] = @RoleId,
        [IsActive] = @IsActive,
        [IsDeleted] = ISNULL(@IsDeleted, [IsDeleted]),
        [UpdatedAt] = GETDATE(),
        [ReferenceUserId] = @ReferenceUserId
    WHERE [UserID] = @UserID;
    SET @UpdatedId = @UserID;
END
GO

-- User_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[User_DeleteById]
GO

CREATE PROCEDURE [dbo].[User_DeleteById]
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Soft delete by setting IsDeleted = 1
    UPDATE [dbo].[Users]
    SET 
        [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [UserID] = @UserID;
END
GO

PRINT 'All stored procedures created successfully!'
GO

