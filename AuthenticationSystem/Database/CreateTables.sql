USE [PrescriptoAuth]
GO

-- =============================================
-- Company Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
    DROP TABLE [dbo].[Company]
GO

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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- =============================================
-- CompanyBranch Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CompanyBranch]') AND type in (N'U'))
    DROP TABLE [dbo].[CompanyBranch]
GO

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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_CompanyBranch_CompanyId] ON [dbo].[CompanyBranch]
(
    [CompanyId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

-- =============================================
-- Permissions Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permissions]') AND type in (N'U'))
    DROP TABLE [dbo].[Permissions]
GO

CREATE TABLE [dbo].[Permissions](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [DisplayName] [nvarchar](150) NOT NULL,
    [PermissionValue] [nvarchar](500) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- =============================================
-- Roles Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
    DROP TABLE [dbo].[Roles]
GO

CREATE TABLE [dbo].[Roles](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [IsDefault] [bit] NOT NULL DEFAULT (0),
    [IsActive] [bit] NOT NULL DEFAULT (1),
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

-- =============================================
-- RolePermission Table
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission]') AND type in (N'U'))
    DROP TABLE [dbo].[RolePermission]
GO

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
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_RolePermission_RoleId] ON [dbo].[RolePermission]
(
    [RoleId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_RolePermission_PermissionId] ON [dbo].[RolePermission]
(
    [PermissionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

-- =============================================
-- Users Table (Merged Schema)
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
    DROP TABLE [dbo].[Users]
GO

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
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Users_TenantID] ON [dbo].[Users]
(
    [TenantID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
    [RoleId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
    [Email] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Users_UserType] ON [dbo].[Users]
(
    [UserType] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

PRINT 'All tables created successfully!'
GO

