-- =============================================
-- Doctor Module Database Tables
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Create tables for all Doctor module entities
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- 0. Prerequisite Tables (if they don't exist)
-- =============================================

-- Create Doctor table if it doesn't exist
-- Note: If Doctors (plural) table exists, you may need to create a synonym or update foreign key references
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor]') AND type in (N'U'))
BEGIN
    -- Check if Doctors (plural) table exists
    IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctors]') AND type in (N'U'))
    BEGIN
        PRINT 'WARNING: Doctors (plural) table exists but Doctor (singular) does not.';
        PRINT 'Creating Doctor table. If you want to use Doctors table instead, please update foreign key references.';
    END
    
    -- Create Doctor table
    CREATE TABLE [dbo].[Doctor]
    (
        [DoctorID] INT IDENTITY(1,1) PRIMARY KEY,
        [UserID] INT NULL,
        [Specialization] NVARCHAR(100) NULL,
        [LicenseNumber] NVARCHAR(50) NULL,
        [HospitalAffiliation] NVARCHAR(100) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0
    );
    PRINT 'Table Doctor created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Doctor already exists.';
END
GO

-- Create DoctorSchedule table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSchedule]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DoctorSchedule]
    (
        [DoctorScheduleID] INT IDENTITY(1,1) PRIMARY KEY,
        [DoctorID] INT NOT NULL,
        [ScheduleID] INT NULL,
        [TenantID] INT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        CONSTRAINT [FK_DoctorSchedule_Doctor] FOREIGN KEY ([DoctorID]) 
            REFERENCES [dbo].[Doctor]([DoctorID]),
        INDEX [IX_DoctorSchedule_DoctorID] ([DoctorID]),
        INDEX [IX_DoctorSchedule_ScheduleID] ([ScheduleID]),
        INDEX [IX_DoctorSchedule_TenantID] ([TenantID])
    );
    PRINT 'Table DoctorSchedule created successfully.';
END
ELSE
BEGIN
    PRINT 'Table DoctorSchedule already exists.';
    -- Check if the table has the correct primary key column
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSchedule]') AND name = 'DoctorScheduleID' AND is_identity = 1)
    BEGIN
        PRINT 'WARNING: DoctorSchedule table exists but may not have DoctorScheduleID as primary key.';
        PRINT 'Please verify the table structure matches the expected schema.';
    END
END
GO

-- =============================================
-- 1. DoctorSpecialization Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[DoctorSpecialization]
    (
        [DoctorSpecializationID] INT IDENTITY(1,1) PRIMARY KEY,
        [DoctorID] INT NOT NULL,
        [SpecialityID] INT NULL,
        [SpecializationID] INT NULL,
        [ServiceDetails] NVARCHAR(500) NULL,
        [DocumentName] NVARCHAR(200) NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        CONSTRAINT [FK_DoctorSpecialization_Doctor] FOREIGN KEY ([DoctorID]) 
            REFERENCES [dbo].[Doctor]([DoctorID]),
        INDEX [IX_DoctorSpecialization_DoctorID] ([DoctorID]),
        INDEX [IX_DoctorSpecialization_SpecialityID] ([SpecialityID]),
        INDEX [IX_DoctorSpecialization_SpecializationID] ([SpecializationID])
    );
    PRINT 'Table DoctorSpecialization created successfully.';
END
ELSE
BEGIN
    PRINT 'Table DoctorSpecialization already exists.';
END
GO

-- =============================================
-- 2. DoctorScheduleDaySession Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduleDaySession]') AND type in (N'U'))
BEGIN
    -- Check if DoctorSchedule table has the correct primary key
    DECLARE @DoctorSchedulePKColumn NVARCHAR(128);
    SELECT @DoctorSchedulePKColumn = c.name
    FROM sys.indexes i
    INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'[dbo].[DoctorSchedule]')
      AND i.is_primary_key = 1
      AND ic.key_ordinal = 1;
    
    IF @DoctorSchedulePKColumn IS NULL OR @DoctorSchedulePKColumn != 'DoctorScheduleID'
    BEGIN
        PRINT 'ERROR: DoctorSchedule table exists but does not have DoctorScheduleID as primary key.';
        PRINT 'Current primary key column: ' + ISNULL(@DoctorSchedulePKColumn, 'NOT FOUND');
        PRINT 'Cannot create DoctorScheduleDaySession table with foreign key constraint.';
        PRINT 'Please fix the DoctorSchedule table structure first.';
    END
    ELSE
    BEGIN
        CREATE TABLE [dbo].[DoctorScheduleDaySession]
        (
            [DoctorScheduleDaySessionID] INT IDENTITY(1,1) PRIMARY KEY,
            [DoctorScheduleID] INT NOT NULL,
            [ScheduleDayofWeek] NVARCHAR(50) NULL,
            [StartTime] NVARCHAR(20) NULL,
            [EndTime] NVARCHAR(20) NULL,
            [NoOfPatients] INT NULL,
            [IsActive] BIT NOT NULL DEFAULT 1,
            [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
            [UpdatedAt] DATETIME NULL,
            [IsDeleted] BIT NOT NULL DEFAULT 0,
            
            CONSTRAINT [FK_DoctorScheduleDaySession_DoctorSchedule] FOREIGN KEY ([DoctorScheduleID]) 
                REFERENCES [dbo].[DoctorSchedule]([DoctorScheduleID]),
            INDEX [IX_DoctorScheduleDaySession_DoctorScheduleID] ([DoctorScheduleID])
        );
        PRINT 'Table DoctorScheduleDaySession created successfully.';
    END
END
ELSE
BEGIN
    PRINT 'Table DoctorScheduleDaySession already exists.';
END
GO

-- =============================================
-- 3. DoctorScheduledDayOff Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduledDayOff]') AND type in (N'U'))
BEGIN
    -- Check if DoctorSchedule table has the correct primary key
    DECLARE @DoctorSchedulePKColumn2 NVARCHAR(128);
    SELECT @DoctorSchedulePKColumn2 = c.name
    FROM sys.indexes i
    INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'[dbo].[DoctorSchedule]')
      AND i.is_primary_key = 1
      AND ic.key_ordinal = 1;
    
    IF @DoctorSchedulePKColumn2 IS NULL OR @DoctorSchedulePKColumn2 != 'DoctorScheduleID'
    BEGIN
        PRINT 'ERROR: DoctorSchedule table exists but does not have DoctorScheduleID as primary key.';
        PRINT 'Current primary key column: ' + ISNULL(@DoctorSchedulePKColumn2, 'NOT FOUND');
        PRINT 'Cannot create DoctorScheduledDayOff table with foreign key constraint.';
        PRINT 'Please fix the DoctorSchedule table structure first.';
    END
    ELSE
    BEGIN
        CREATE TABLE [dbo].[DoctorScheduledDayOff]
        (
            [DoctorScheduledDayOffID] INT IDENTITY(1,1) PRIMARY KEY,
            [DoctorScheduleID] INT NOT NULL,
            [OffDay] NVARCHAR(50) NULL,
            [IsActive] BIT NOT NULL DEFAULT 1,
            [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
            [UpdatedAt] DATETIME NULL,
            [IsDeleted] BIT NOT NULL DEFAULT 0,
            
            CONSTRAINT [FK_DoctorScheduledDayOff_DoctorSchedule] FOREIGN KEY ([DoctorScheduleID]) 
                REFERENCES [dbo].[DoctorSchedule]([DoctorScheduleID]),
            INDEX [IX_DoctorScheduledDayOff_DoctorScheduleID] ([DoctorScheduleID])
        );
        PRINT 'Table DoctorScheduledDayOff created successfully.';
    END
END
ELSE
BEGIN
    PRINT 'Table DoctorScheduledDayOff already exists.';
END
GO

-- =============================================
-- 4. DoctorFeesSetup Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup]') AND type in (N'U'))
BEGIN
    -- Check if DoctorSchedule table has the correct primary key
    DECLARE @DoctorSchedulePKColumn3 NVARCHAR(128);
    SELECT @DoctorSchedulePKColumn3 = c.name
    FROM sys.indexes i
    INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
    INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
    WHERE i.object_id = OBJECT_ID(N'[dbo].[DoctorSchedule]')
      AND i.is_primary_key = 1
      AND ic.key_ordinal = 1;
    
    IF @DoctorSchedulePKColumn3 IS NULL OR @DoctorSchedulePKColumn3 != 'DoctorScheduleID'
    BEGIN
        PRINT 'ERROR: DoctorSchedule table exists but does not have DoctorScheduleID as primary key.';
        PRINT 'Current primary key column: ' + ISNULL(@DoctorSchedulePKColumn3, 'NOT FOUND');
        PRINT 'Cannot create DoctorFeesSetup table with foreign key constraint.';
        PRINT 'Please fix the DoctorSchedule table structure first.';
    END
    ELSE
    BEGIN
        CREATE TABLE [dbo].[DoctorFeesSetup]
        (
            [DoctorFeesSetupID] INT IDENTITY(1,1) PRIMARY KEY,
            [DoctorScheduleID] INT NOT NULL,
            [AppointmentType] NVARCHAR(50) NULL,
            [CurrentFee] DECIMAL(18, 2) NULL,
            [PreviousFee] DECIMAL(18, 2) NULL,
            [FeeAppliedFrom] DATETIME NULL,
            [FollowUpPeriod] INT NULL,
            [ReportShowPeriod] INT NULL,
            [Discount] DECIMAL(18, 2) NULL,
            [DiscountAppliedFrom] DATETIME NULL,
            [DiscountPeriod] INT NULL,
            [TotalFee] DECIMAL(18, 2) NULL,
            [IsActive] BIT NOT NULL DEFAULT 1,
            [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
            [UpdatedAt] DATETIME NULL,
            [IsDeleted] BIT NOT NULL DEFAULT 0,
            
            CONSTRAINT [FK_DoctorFeesSetup_DoctorSchedule] FOREIGN KEY ([DoctorScheduleID]) 
                REFERENCES [dbo].[DoctorSchedule]([DoctorScheduleID]),
            INDEX [IX_DoctorFeesSetup_DoctorScheduleID] ([DoctorScheduleID])
        );
        PRINT 'Table DoctorFeesSetup created successfully.';
    END
END
ELSE
BEGIN
    PRINT 'Table DoctorFeesSetup already exists.';
END
GO

-- =============================================
-- 5. MasterDoctor Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[MasterDoctor]
    (
        [MasterDoctorID] INT IDENTITY(1,1) PRIMARY KEY,
        [DoctorID] INT NOT NULL,
        [AgentMasterID] INT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        CONSTRAINT [FK_MasterDoctor_Doctor] FOREIGN KEY ([DoctorID]) 
            REFERENCES [dbo].[Doctor]([DoctorID]),
        INDEX [IX_MasterDoctor_DoctorID] ([DoctorID]),
        INDEX [IX_MasterDoctor_AgentMasterID] ([AgentMasterID])
    );
    PRINT 'Table MasterDoctor created successfully.';
END
ELSE
BEGIN
    PRINT 'Table MasterDoctor already exists.';
END
GO

-- =============================================
-- 6. CampaignDoctor Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CampaignDoctor]
    (
        [CampaignDoctorID] INT IDENTITY(1,1) PRIMARY KEY,
        [DoctorID] INT NOT NULL,
        [CampaignID] INT NULL,
        [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME NULL,
        [IsDeleted] BIT NOT NULL DEFAULT 0,
        
        CONSTRAINT [FK_CampaignDoctor_Doctor] FOREIGN KEY ([DoctorID]) 
            REFERENCES [dbo].[Doctor]([DoctorID]),
        INDEX [IX_CampaignDoctor_DoctorID] ([DoctorID]),
        INDEX [IX_CampaignDoctor_CampaignID] ([CampaignID])
    );
    PRINT 'Table CampaignDoctor created successfully.';
END
ELSE
BEGIN
    PRINT 'Table CampaignDoctor already exists.';
END
GO

-- =============================================
-- END OF TABLE CREATION
-- =============================================
-- Total Tables Created: 8 (including prerequisites)
-- Prerequisites:
-- 0a. Doctor (if not exists)
-- 0b. DoctorSchedule (if not exists)
-- Main Tables:
-- 1. DoctorSpecialization
-- 2. DoctorScheduleDaySession
-- 3. DoctorScheduledDayOff
-- 4. DoctorFeesSetup
-- 5. MasterDoctor
-- 6. CampaignDoctor
-- =============================================

PRINT 'All Doctor module tables creation script completed.';
GO

