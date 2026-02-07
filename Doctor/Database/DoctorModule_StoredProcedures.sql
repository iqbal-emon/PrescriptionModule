-- =============================================
-- Doctor Module Stored Procedures
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: All stored procedures for Doctor module entities
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- 1. DoctorSpecialization Stored Procedures
-- =============================================

-- DoctorSpecialization_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSpecialization_GetAll]
GO

CREATE PROCEDURE [dbo].[DoctorSpecialization_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorSpecializationID],
        [DoctorID],
        [SpecialityID],
        [SpecializationID],
        [ServiceDetails],
        [DocumentName],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorSpecialization]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorSpecialization_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSpecialization_GetById]
GO

CREATE PROCEDURE [dbo].[DoctorSpecialization_GetById]
    @DoctorSpecializationID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorSpecializationID],
        [DoctorID],
        [SpecialityID],
        [SpecializationID],
        [ServiceDetails],
        [DocumentName],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorSpecialization]
    WHERE [DoctorSpecializationID] = @DoctorSpecializationID
        AND [IsDeleted] = 0;
END
GO

-- DoctorSpecialization_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSpecialization_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[DoctorSpecialization_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorSpecializationID],
        [DoctorID],
        [SpecialityID],
        [SpecializationID],
        [ServiceDetails],
        [DocumentName],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorSpecialization]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorSpecialization_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSpecialization_Insert]
GO

CREATE PROCEDURE [dbo].[DoctorSpecialization_Insert]
    @DoctorID INT,
    @SpecialityID INT = NULL,
    @SpecializationID INT = NULL,
    @ServiceDetails NVARCHAR(500) = NULL,
    @DocumentName NVARCHAR(200) = NULL,
    @DoctorSpecializationID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[DoctorSpecialization]
    (
        [DoctorID],
        [SpecialityID],
        [SpecializationID],
        [ServiceDetails],
        [DocumentName],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorID,
        @SpecialityID,
        @SpecializationID,
        @ServiceDetails,
        @DocumentName,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @DoctorSpecializationID = SCOPE_IDENTITY();
END
GO

-- DoctorSpecialization_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSpecialization_Update]
GO

CREATE PROCEDURE [dbo].[DoctorSpecialization_Update]
    @DoctorSpecializationID INT,
    @DoctorID INT,
    @SpecialityID INT = NULL,
    @SpecializationID INT = NULL,
    @ServiceDetails NVARCHAR(500) = NULL,
    @DocumentName NVARCHAR(200) = NULL,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorSpecialization]
    SET 
        [DoctorID] = @DoctorID,
        [SpecialityID] = @SpecialityID,
        [SpecializationID] = @SpecializationID,
        [ServiceDetails] = @ServiceDetails,
        [DocumentName] = @DocumentName,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorSpecializationID] = @DoctorSpecializationID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @DoctorSpecializationID;
END
GO

-- DoctorSpecialization_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSpecialization_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSpecialization_DeleteById]
GO

CREATE PROCEDURE [dbo].[DoctorSpecialization_DeleteById]
    @DoctorSpecializationID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorSpecialization]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorSpecializationID] = @DoctorSpecializationID;
END
GO

-- =============================================
-- 2. DoctorScheduleDaySession Stored Procedures
-- =============================================

-- DoctorScheduleDaySession_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduleDaySession_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduleDaySession_GetAll]
GO

CREATE PROCEDURE [dbo].[DoctorScheduleDaySession_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorScheduleDaySessionID],
        [DoctorScheduleID],
        [ScheduleDayofWeek],
        [StartTime],
        [EndTime],
        [NoOfPatients],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorScheduleDaySession]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorScheduleDaySession_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduleDaySession_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduleDaySession_GetById]
GO

CREATE PROCEDURE [dbo].[DoctorScheduleDaySession_GetById]
    @DoctorScheduleDaySessionID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorScheduleDaySessionID],
        [DoctorScheduleID],
        [ScheduleDayofWeek],
        [StartTime],
        [EndTime],
        [NoOfPatients],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorScheduleDaySession]
    WHERE [DoctorScheduleDaySessionID] = @DoctorScheduleDaySessionID
        AND [IsDeleted] = 0;
END
GO

-- DoctorScheduleDaySession_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduleDaySession_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduleDaySession_Insert]
GO

CREATE PROCEDURE [dbo].[DoctorScheduleDaySession_Insert]
    @DoctorScheduleID INT,
    @ScheduleDayofWeek NVARCHAR(50) = NULL,
    @StartTime NVARCHAR(20) = NULL,
    @EndTime NVARCHAR(20) = NULL,
    @NoOfPatients INT = NULL,
    @IsActive BIT = 1,
    @DoctorScheduleDaySessionID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[DoctorScheduleDaySession]
    (
        [DoctorScheduleID],
        [ScheduleDayofWeek],
        [StartTime],
        [EndTime],
        [NoOfPatients],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorScheduleID,
        @ScheduleDayofWeek,
        @StartTime,
        @EndTime,
        @NoOfPatients,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @DoctorScheduleDaySessionID = SCOPE_IDENTITY();
END
GO

-- DoctorScheduleDaySession_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduleDaySession_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduleDaySession_Update]
GO

CREATE PROCEDURE [dbo].[DoctorScheduleDaySession_Update]
    @DoctorScheduleDaySessionID INT,
    @DoctorScheduleID INT,
    @ScheduleDayofWeek NVARCHAR(50) = NULL,
    @StartTime NVARCHAR(20) = NULL,
    @EndTime NVARCHAR(20) = NULL,
    @NoOfPatients INT = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorScheduleDaySession]
    SET 
        [DoctorScheduleID] = @DoctorScheduleID,
        [ScheduleDayofWeek] = @ScheduleDayofWeek,
        [StartTime] = @StartTime,
        [EndTime] = @EndTime,
        [NoOfPatients] = @NoOfPatients,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorScheduleDaySessionID] = @DoctorScheduleDaySessionID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @DoctorScheduleDaySessionID;
END
GO

-- DoctorScheduleDaySession_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduleDaySession_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduleDaySession_DeleteById]
GO

CREATE PROCEDURE [dbo].[DoctorScheduleDaySession_DeleteById]
    @DoctorScheduleDaySessionID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorScheduleDaySession]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorScheduleDaySessionID] = @DoctorScheduleDaySessionID;
END
GO

-- =============================================
-- 3. DoctorScheduledDayOff Stored Procedures
-- =============================================

-- DoctorScheduledDayOff_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduledDayOff_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduledDayOff_GetAll]
GO

CREATE PROCEDURE [dbo].[DoctorScheduledDayOff_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorScheduledDayOffID],
        [DoctorScheduleID],
        [OffDay],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorScheduledDayOff]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorScheduledDayOff_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduledDayOff_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduledDayOff_GetById]
GO

CREATE PROCEDURE [dbo].[DoctorScheduledDayOff_GetById]
    @DoctorScheduledDayOffID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorScheduledDayOffID],
        [DoctorScheduleID],
        [OffDay],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorScheduledDayOff]
    WHERE [DoctorScheduledDayOffID] = @DoctorScheduledDayOffID
        AND [IsDeleted] = 0;
END
GO

-- DoctorScheduledDayOff_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduledDayOff_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduledDayOff_Insert]
GO

CREATE PROCEDURE [dbo].[DoctorScheduledDayOff_Insert]
    @DoctorScheduleID INT,
    @OffDay NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @DoctorScheduledDayOffID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[DoctorScheduledDayOff]
    (
        [DoctorScheduleID],
        [OffDay],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorScheduleID,
        @OffDay,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @DoctorScheduledDayOffID = SCOPE_IDENTITY();
END
GO

-- DoctorScheduledDayOff_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduledDayOff_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduledDayOff_Update]
GO

CREATE PROCEDURE [dbo].[DoctorScheduledDayOff_Update]
    @DoctorScheduledDayOffID INT,
    @DoctorScheduleID INT,
    @OffDay NVARCHAR(50) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorScheduledDayOff]
    SET 
        [DoctorScheduleID] = @DoctorScheduleID,
        [OffDay] = @OffDay,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorScheduledDayOffID] = @DoctorScheduledDayOffID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @DoctorScheduledDayOffID;
END
GO

-- DoctorScheduledDayOff_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorScheduledDayOff_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorScheduledDayOff_DeleteById]
GO

CREATE PROCEDURE [dbo].[DoctorScheduledDayOff_DeleteById]
    @DoctorScheduledDayOffID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorScheduledDayOff]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorScheduledDayOffID] = @DoctorScheduledDayOffID;
END
GO

-- =============================================
-- 4. DoctorFeesSetup Stored Procedures
-- =============================================

-- DoctorFeesSetup_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorFeesSetup_GetAll]
GO

CREATE PROCEDURE [dbo].[DoctorFeesSetup_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorFeesSetupID],
        [DoctorScheduleID],
        [AppointmentType],
        [CurrentFee],
        [PreviousFee],
        [FeeAppliedFrom],
        [FollowUpPeriod],
        [ReportShowPeriod],
        [Discount],
        [DiscountAppliedFrom],
        [DiscountPeriod],
        [TotalFee],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorFeesSetup]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorFeesSetup_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorFeesSetup_GetById]
GO

CREATE PROCEDURE [dbo].[DoctorFeesSetup_GetById]
    @DoctorFeesSetupID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorFeesSetupID],
        [DoctorScheduleID],
        [AppointmentType],
        [CurrentFee],
        [PreviousFee],
        [FeeAppliedFrom],
        [FollowUpPeriod],
        [ReportShowPeriod],
        [Discount],
        [DiscountAppliedFrom],
        [DiscountPeriod],
        [TotalFee],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorFeesSetup]
    WHERE [DoctorFeesSetupID] = @DoctorFeesSetupID
        AND [IsDeleted] = 0;
END
GO

-- DoctorFeesSetup_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorFeesSetup_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[DoctorFeesSetup_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        fs.[DoctorFeesSetupID],
        fs.[DoctorScheduleID],
        fs.[AppointmentType],
        fs.[CurrentFee],
        fs.[PreviousFee],
        fs.[FeeAppliedFrom],
        fs.[FollowUpPeriod],
        fs.[ReportShowPeriod],
        fs.[Discount],
        fs.[DiscountAppliedFrom],
        fs.[DiscountPeriod],
        fs.[TotalFee],
        fs.[IsActive],
        fs.[CreatedAt],
        fs.[UpdatedAt],
        fs.[IsDeleted]
    FROM [dbo].[DoctorFeesSetup] fs
    INNER JOIN [dbo].[DoctorSchedule] ds ON fs.[DoctorScheduleID] = ds.[DoctorScheduleID]
    WHERE ds.[DoctorID] = @DoctorID
        AND fs.[IsDeleted] = 0
    ORDER BY fs.[CreatedAt] DESC;
END
GO

-- DoctorFeesSetup_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorFeesSetup_Insert]
GO

CREATE PROCEDURE [dbo].[DoctorFeesSetup_Insert]
    @DoctorScheduleID INT,
    @AppointmentType NVARCHAR(50) = NULL,
    @CurrentFee DECIMAL(18, 2) = NULL,
    @PreviousFee DECIMAL(18, 2) = NULL,
    @FeeAppliedFrom DATETIME = NULL,
    @FollowUpPeriod INT = NULL,
    @ReportShowPeriod INT = NULL,
    @Discount DECIMAL(18, 2) = NULL,
    @DiscountAppliedFrom DATETIME = NULL,
    @DiscountPeriod INT = NULL,
    @TotalFee DECIMAL(18, 2) = NULL,
    @IsActive BIT = 1,
    @DoctorFeesSetupID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[DoctorFeesSetup]
    (
        [DoctorScheduleID],
        [AppointmentType],
        [CurrentFee],
        [PreviousFee],
        [FeeAppliedFrom],
        [FollowUpPeriod],
        [ReportShowPeriod],
        [Discount],
        [DiscountAppliedFrom],
        [DiscountPeriod],
        [TotalFee],
        [IsActive],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorScheduleID,
        @AppointmentType,
        @CurrentFee,
        @PreviousFee,
        @FeeAppliedFrom,
        @FollowUpPeriod,
        @ReportShowPeriod,
        @Discount,
        @DiscountAppliedFrom,
        @DiscountPeriod,
        @TotalFee,
        @IsActive,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @DoctorFeesSetupID = SCOPE_IDENTITY();
END
GO

-- DoctorFeesSetup_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorFeesSetup_Update]
GO

CREATE PROCEDURE [dbo].[DoctorFeesSetup_Update]
    @DoctorFeesSetupID INT,
    @DoctorScheduleID INT,
    @AppointmentType NVARCHAR(50) = NULL,
    @CurrentFee DECIMAL(18, 2) = NULL,
    @PreviousFee DECIMAL(18, 2) = NULL,
    @FeeAppliedFrom DATETIME = NULL,
    @FollowUpPeriod INT = NULL,
    @ReportShowPeriod INT = NULL,
    @Discount DECIMAL(18, 2) = NULL,
    @DiscountAppliedFrom DATETIME = NULL,
    @DiscountPeriod INT = NULL,
    @TotalFee DECIMAL(18, 2) = NULL,
    @IsActive BIT = 1,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorFeesSetup]
    SET 
        [DoctorScheduleID] = @DoctorScheduleID,
        [AppointmentType] = @AppointmentType,
        [CurrentFee] = @CurrentFee,
        [PreviousFee] = @PreviousFee,
        [FeeAppliedFrom] = @FeeAppliedFrom,
        [FollowUpPeriod] = @FollowUpPeriod,
        [ReportShowPeriod] = @ReportShowPeriod,
        [Discount] = @Discount,
        [DiscountAppliedFrom] = @DiscountAppliedFrom,
        [DiscountPeriod] = @DiscountPeriod,
        [TotalFee] = @TotalFee,
        [IsActive] = @IsActive,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorFeesSetupID] = @DoctorFeesSetupID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @DoctorFeesSetupID;
END
GO

-- DoctorFeesSetup_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorFeesSetup_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorFeesSetup_DeleteById]
GO

CREATE PROCEDURE [dbo].[DoctorFeesSetup_DeleteById]
    @DoctorFeesSetupID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[DoctorFeesSetup]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorFeesSetupID] = @DoctorFeesSetupID;
END
GO

-- =============================================
-- 5. MasterDoctor Stored Procedures
-- =============================================

-- MasterDoctor_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[MasterDoctor_GetAll]
GO

CREATE PROCEDURE [dbo].[MasterDoctor_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [MasterDoctorID],
        [DoctorID],
        [AgentMasterID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[MasterDoctor]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- MasterDoctor_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[MasterDoctor_GetById]
GO

CREATE PROCEDURE [dbo].[MasterDoctor_GetById]
    @MasterDoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [MasterDoctorID],
        [DoctorID],
        [AgentMasterID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[MasterDoctor]
    WHERE [MasterDoctorID] = @MasterDoctorID
        AND [IsDeleted] = 0;
END
GO

-- MasterDoctor_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[MasterDoctor_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[MasterDoctor_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [MasterDoctorID],
        [DoctorID],
        [AgentMasterID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[MasterDoctor]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- MasterDoctor_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[MasterDoctor_Insert]
GO

CREATE PROCEDURE [dbo].[MasterDoctor_Insert]
    @DoctorID INT,
    @AgentMasterID INT = NULL,
    @MasterDoctorID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[MasterDoctor]
    (
        [DoctorID],
        [AgentMasterID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorID,
        @AgentMasterID,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @MasterDoctorID = SCOPE_IDENTITY();
END
GO

-- MasterDoctor_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[MasterDoctor_Update]
GO

CREATE PROCEDURE [dbo].[MasterDoctor_Update]
    @MasterDoctorID INT,
    @DoctorID INT,
    @AgentMasterID INT = NULL,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[MasterDoctor]
    SET 
        [DoctorID] = @DoctorID,
        [AgentMasterID] = @AgentMasterID,
        [UpdatedAt] = GETDATE()
    WHERE [MasterDoctorID] = @MasterDoctorID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @MasterDoctorID;
END
GO

-- MasterDoctor_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MasterDoctor_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[MasterDoctor_DeleteById]
GO

CREATE PROCEDURE [dbo].[MasterDoctor_DeleteById]
    @MasterDoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[MasterDoctor]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [MasterDoctorID] = @MasterDoctorID;
END
GO

-- =============================================
-- 6. CampaignDoctor Stored Procedures
-- =============================================

-- CampaignDoctor_GetAll
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor_GetAll]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CampaignDoctor_GetAll]
GO

CREATE PROCEDURE [dbo].[CampaignDoctor_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CampaignDoctorID],
        [DoctorID],
        [CampaignID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[CampaignDoctor]
    WHERE [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- CampaignDoctor_GetById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor_GetById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CampaignDoctor_GetById]
GO

CREATE PROCEDURE [dbo].[CampaignDoctor_GetById]
    @CampaignDoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CampaignDoctorID],
        [DoctorID],
        [CampaignID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[CampaignDoctor]
    WHERE [CampaignDoctorID] = @CampaignDoctorID
        AND [IsDeleted] = 0;
END
GO

-- CampaignDoctor_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CampaignDoctor_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[CampaignDoctor_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [CampaignDoctorID],
        [DoctorID],
        [CampaignID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[CampaignDoctor]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- CampaignDoctor_Insert
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor_Insert]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CampaignDoctor_Insert]
GO

CREATE PROCEDURE [dbo].[CampaignDoctor_Insert]
    @DoctorID INT,
    @CampaignID INT = NULL,
    @CampaignDoctorID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[CampaignDoctor]
    (
        [DoctorID],
        [CampaignID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    )
    VALUES
    (
        @DoctorID,
        @CampaignID,
        GETDATE(),
        GETDATE(),
        0
    );
    SET @CampaignDoctorID = SCOPE_IDENTITY();
END
GO

-- CampaignDoctor_Update
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor_Update]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CampaignDoctor_Update]
GO

CREATE PROCEDURE [dbo].[CampaignDoctor_Update]
    @CampaignDoctorID INT,
    @DoctorID INT,
    @CampaignID INT = NULL,
    @UpdatedId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CampaignDoctor]
    SET 
        [DoctorID] = @DoctorID,
        [CampaignID] = @CampaignID,
        [UpdatedAt] = GETDATE()
    WHERE [CampaignDoctorID] = @CampaignDoctorID
        AND [IsDeleted] = 0;
    SET @UpdatedId = @CampaignDoctorID;
END
GO

-- CampaignDoctor_DeleteById
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CampaignDoctor_DeleteById]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[CampaignDoctor_DeleteById]
GO

CREATE PROCEDURE [dbo].[CampaignDoctor_DeleteById]
    @CampaignDoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[CampaignDoctor]
    SET [IsDeleted] = 1,
        [UpdatedAt] = GETDATE()
    WHERE [CampaignDoctorID] = @CampaignDoctorID;
END
GO

-- =============================================
-- 7. GetByDoctorId Procedures for Existing Entities
-- =============================================

-- DoctorChamber_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorChamber_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorChamber_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[DoctorChamber_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [ChamberID],
        [TenantID],
        [DoctorID],
        [ChamberName],
        [Address],
        [Country],
        [CountryID],
        [City],
        [CityID],
        [ZipCode],
        [ZipCodeID],
        [IsVisibleOnPrescription],
        [ChamberReferenceId],
        [DistrictId],
        [DivisionId],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorChamber]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorDegree_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorDegree_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorDegree_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[DoctorDegree_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorDegreeID],
        [DoctorID],
        [DegreeID],
        [InstituteName],
        [PassingYear],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorDegree]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- DoctorSchedule_GetByDoctorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DoctorSchedule_GetByDoctorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[DoctorSchedule_GetByDoctorId]
GO

CREATE PROCEDURE [dbo].[DoctorSchedule_GetByDoctorId]
    @DoctorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [DoctorScheduleID],
        [DoctorID],
        [ScheduleID],
        [TenantID],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[DoctorSchedule]
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0
    ORDER BY [CreatedAt] DESC;
END
GO

-- =============================================
-- Doctor Additional Stored Procedures
-- =============================================

-- Doctor_UpdateExpertise
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_UpdateExpertise]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_UpdateExpertise]
GO

CREATE PROCEDURE [dbo].[Doctor_UpdateExpertise]
    @DoctorID INT,
    @Expertise NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Doctor]
    SET 
        [Expertise] = @Expertise,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0;
    
    SELECT 
        [DoctorID],
        [UserID],
        [Specialization],
        [LicenseNumber],
        [HospitalAffiliation],
        [Expertise],
        [ProfileStep],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Doctor]
    WHERE [DoctorID] = @DoctorID;
END
GO

-- Doctor_UpdateProfileStep
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_UpdateProfileStep]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_UpdateProfileStep]
GO

CREATE PROCEDURE [dbo].[Doctor_UpdateProfileStep]
    @DoctorID INT,
    @ProfileStep INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Doctor]
    SET 
        [ProfileStep] = @ProfileStep,
        [UpdatedAt] = GETDATE()
    WHERE [DoctorID] = @DoctorID
        AND [IsDeleted] = 0;
    
    SELECT 
        [DoctorID],
        [UserID],
        [Specialization],
        [LicenseNumber],
        [HospitalAffiliation],
        [Expertise],
        [ProfileStep],
        [CreatedAt],
        [UpdatedAt],
        [IsDeleted]
    FROM [dbo].[Doctor]
    WHERE [DoctorID] = @DoctorID;
END
GO

-- Doctor_GetByCreatorId
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Doctor_GetByCreatorId]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Doctor_GetByCreatorId]
GO

CREATE PROCEDURE [dbo].[Doctor_GetByCreatorId]
    @CreatorID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- This assumes there's a relationship between Doctor and Creator
    -- Adjust the query based on your actual schema (e.g., through MasterDoctor, Appointment, etc.)
    SELECT DISTINCT
        d.[DoctorID],
        d.[UserID],
        d.[Specialization],
        d.[LicenseNumber],
        d.[HospitalAffiliation],
        d.[Expertise],
        d.[ProfileStep],
        d.[CreatedAt],
        d.[UpdatedAt],
        d.[IsDeleted]
    FROM [dbo].[Doctor] d
    INNER JOIN [dbo].[MasterDoctor] md ON d.[DoctorID] = md.[DoctorID]
    WHERE md.[AgentMasterID] = @CreatorID
        AND d.[IsDeleted] = 0
    ORDER BY d.[CreatedAt] DESC;
END
GO

-- =============================================
-- END OF STORED PROCEDURES
-- =============================================
-- Total Procedures Created: 40
-- - 6 entities × 5 procedures (GetAll, GetById, Insert, Update, DeleteById) = 30
-- - 7 GetByDoctorId procedures = 7
-- - 3 Additional procedures (UpdateExpertise, UpdateProfileStep, GetByCreatorId) = 3
-- =============================================

