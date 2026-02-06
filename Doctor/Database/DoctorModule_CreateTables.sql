-- =============================================
-- Doctor Module Database Tables
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Create tables for all Doctor module entities
-- =============================================

USE [Prescripto]  -- Change to your database name
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
            REFERENCES [dbo].[DoctorSchedule]([ScheduleID]),
        INDEX [IX_DoctorScheduleDaySession_DoctorScheduleID] ([DoctorScheduleID])
    );
    PRINT 'Table DoctorScheduleDaySession created successfully.';
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
            REFERENCES [dbo].[DoctorSchedule]([ScheduleID]),
        INDEX [IX_DoctorScheduledDayOff_DoctorScheduleID] ([DoctorScheduleID])
    );
    PRINT 'Table DoctorScheduledDayOff created successfully.';
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
            REFERENCES [dbo].[DoctorSchedule]([ScheduleID]),
        INDEX [IX_DoctorFeesSetup_DoctorScheduleID] ([DoctorScheduleID])
    );
    PRINT 'Table DoctorFeesSetup created successfully.';
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
-- Total Tables Created: 6
-- 1. DoctorSpecialization
-- 2. DoctorScheduleDaySession
-- 3. DoctorScheduledDayOff
-- 4. DoctorFeesSetup
-- 5. MasterDoctor
-- 6. CampaignDoctor
-- =============================================

PRINT 'All Doctor module tables creation script completed.';
GO

