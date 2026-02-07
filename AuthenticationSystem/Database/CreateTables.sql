-- =============================================
-- Authentication System Database Setup
-- =============================================
-- This script creates the PrescriptoAuth database and all required tables
-- It handles existing databases and tables gracefully
-- =============================================

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PrescriptoAuth')
BEGIN
    CREATE DATABASE [PrescriptoAuth];
    PRINT 'Database PrescriptoAuth created successfully.';
END
ELSE
BEGIN
    PRINT 'Database PrescriptoAuth already exists.';
END
GO

USE [PrescriptoAuth]
GO

-- =============================================
-- Drop Foreign Keys and Tables in Correct Order
-- =============================================

-- Drop Users table first (has self-reference and multiple foreign keys)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    -- Drop foreign keys first
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Users_Roles')
        ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles];
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Users_Tenant')
        ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Tenant];
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Users_ReferenceUser')
        ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_ReferenceUser];
    
    DROP TABLE [dbo].[Users];
    PRINT 'Table Users dropped.';
END
GO

-- Drop RolePermission table (depends on Roles and Permissions)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission]') AND type in (N'U'))
BEGIN
    -- Drop foreign keys first
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_RolePermission_Roles')
        ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Roles];
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_RolePermission_Permissions')
        ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Permissions];
    
    DROP TABLE [dbo].[RolePermission];
    PRINT 'Table RolePermission dropped.';
END
GO

-- Drop CompanyBranch table (depends on Company)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch]') AND type in (N'U'))
BEGIN
    -- Drop foreign keys first
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CompanyBranch_Company')
        ALTER TABLE [dbo].[CompanyBranch] DROP CONSTRAINT [FK_CompanyBranch_Company];
    
    DROP TABLE [dbo].[CompanyBranch];
    PRINT 'Table CompanyBranch dropped.';
END
GO

-- Drop independent tables
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[Company];
    PRINT 'Table Company dropped.';
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permissions]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[Permissions];
    PRINT 'Table Permissions dropped.';
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[Roles];
    PRINT 'Table Roles dropped.';
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenants]') AND type in (N'U'))
BEGIN
    DROP TABLE [dbo].[Tenants];
    PRINT 'Table Tenants dropped.';
END
GO

-- =============================================
-- Create Tables in Correct Order
-- =============================================

-- =============================================
-- Company Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Company](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Name] [nvarchar](200) NOT NULL,
        [LicenseNo] [nvarchar](100) NULL,
        [DrugRegCertificate] [nvarchar](100) NULL,
        [Address] [nvarchar](max) NULL,
        [ContactNo] [nvarchar](20) NULL,
        [Email] [nvarchar](150) NULL,
        [CurrencySymbol] [nvarchar](10) NOT NULL DEFAULT ('?'),
        [CreatedAt] [datetime] NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
    PRINT 'Table Company created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Company already exists.';
END
GO

-- =============================================
-- CompanyBranch Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CompanyBranch](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [CompanyId] [int] NOT NULL,
        [Name] [nvarchar](150) NOT NULL,
        [Address] [nvarchar](max) NULL,
        [ContactNo] [nvarchar](20) NULL,
        [IsActive] [bit] NOT NULL DEFAULT (1),
        [CreatedAt] [datetime] NOT NULL DEFAULT (GETDATE()),
        CONSTRAINT [PK_CompanyBranch] PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        CONSTRAINT [FK_CompanyBranch_Company] FOREIGN KEY([CompanyId])
        REFERENCES [dbo].[Company] ([Id])
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
    PRINT 'Table CompanyBranch created successfully.';
END
ELSE
BEGIN
    PRINT 'Table CompanyBranch already exists.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_CompanyBranch_CompanyId' AND object_id = OBJECT_ID('[dbo].[CompanyBranch]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_CompanyBranch_CompanyId] ON [dbo].[CompanyBranch]
    (
        [CompanyId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

-- =============================================
-- Permissions Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permissions]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Permissions](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [DisplayName] [nvarchar](150) NOT NULL,
        [PermissionValue] [nvarchar](500) NOT NULL,
        CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY];
    PRINT 'Table Permissions created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Permissions already exists.';
END
GO

-- =============================================
-- Roles Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Roles](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Name] [nvarchar](100) NOT NULL,
        [IsDefault] [bit] NOT NULL DEFAULT (0),
        [IsActive] [bit] NOT NULL DEFAULT (1),
        CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY];
    PRINT 'Table Roles created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Roles already exists.';
END
GO

-- =============================================
-- RolePermission Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[RolePermission](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [RoleId] [int] NOT NULL,
        [PermissionId] [int] NOT NULL,
        CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED ([Id] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        CONSTRAINT [FK_RolePermission_Roles] FOREIGN KEY([RoleId])
        REFERENCES [dbo].[Roles] ([Id])
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
        CONSTRAINT [FK_RolePermission_Permissions] FOREIGN KEY([PermissionId])
        REFERENCES [dbo].[Permissions] ([Id])
        ON DELETE NO ACTION
        ON UPDATE NO ACTION
    ) ON [PRIMARY];
    PRINT 'Table RolePermission created successfully.';
END
ELSE
BEGIN
    PRINT 'Table RolePermission already exists.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RolePermission_RoleId' AND object_id = OBJECT_ID('[dbo].[RolePermission]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_RolePermission_RoleId] ON [dbo].[RolePermission]
    (
        [RoleId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RolePermission_PermissionId' AND object_id = OBJECT_ID('[dbo].[RolePermission]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_RolePermission_PermissionId] ON [dbo].[RolePermission]
    (
        [PermissionId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

-- =============================================
-- Tenants Table (Required for Users foreign key)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tenants]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Tenants](
        [TenantID] [int] IDENTITY(1,1) NOT NULL,
        [TenantName] [nvarchar](100) NOT NULL,
        [Domain] [nvarchar](100) NOT NULL,
        [CreatedAt] [datetime] NOT NULL DEFAULT (GETDATE()),
        [UpdatedAt] [datetime] NULL,
        [IsActive] [bit] NOT NULL DEFAULT (1),
        CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED ([TenantID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        CONSTRAINT [UQ_Tenants_Domain] UNIQUE NONCLUSTERED ([Domain] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY];
    PRINT 'Table Tenants created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Tenants already exists.';
END
GO

-- =============================================
-- Users Table (Merged Schema)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Users](
        [UserID] [int] IDENTITY(1,1) NOT NULL,
        [TenantID] [int] NOT NULL,
        [FirstName] [nvarchar](50) NULL,
        [LastName] [nvarchar](50) NULL,
        [FullName] [nvarchar](150) NOT NULL,
        [UserName] [nvarchar](100) NOT NULL,
        [Email] [nvarchar](150) NULL,
        [PasswordHash] [nvarchar](255) NOT NULL,
        [UserType] [nvarchar](20) NULL,
        [PhoneNumber] [nvarchar](15) NULL,
        [ContactNo] [nvarchar](20) NULL,
        [RoleId] [int] NOT NULL,
        [IsActive] [bit] NOT NULL DEFAULT (1),
        [IsDeleted] [bit] NULL DEFAULT (0),
        [CreatedAt] [datetime] NOT NULL DEFAULT (GETDATE()),
        [UpdatedAt] [datetime] NULL,
        [ReferenceUserId] [int] NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
        CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
        REFERENCES [dbo].[Roles] ([Id])
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
        CONSTRAINT [FK_Users_Tenant] FOREIGN KEY([TenantID])
        REFERENCES [dbo].[Tenants] ([TenantID])
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
        CONSTRAINT [FK_Users_ReferenceUser] FOREIGN KEY([ReferenceUserId])
        REFERENCES [dbo].[Users] ([UserID])
        ON DELETE NO ACTION
        ON UPDATE NO ACTION,
        CONSTRAINT [UQ_Users_UserName] UNIQUE NONCLUSTERED ([UserName] ASC)
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY];
    PRINT 'Table Users created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Users already exists.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_TenantID' AND object_id = OBJECT_ID('[dbo].[Users]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users_TenantID] ON [dbo].[Users]
    (
        [TenantID] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_RoleId' AND object_id = OBJECT_ID('[dbo].[Users]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
    (
        [RoleId] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_Email' AND object_id = OBJECT_ID('[dbo].[Users]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
    (
        [Email] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_UserType' AND object_id = OBJECT_ID('[dbo].[Users]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users_UserType] ON [dbo].[Users]
    (
        [UserType] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY];
END
GO

PRINT 'All tables created successfully!'
GO
