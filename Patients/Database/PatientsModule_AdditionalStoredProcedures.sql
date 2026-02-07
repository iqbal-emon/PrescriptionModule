-- =============================================
-- Patients Module - Additional Stored Procedures
-- Database: Prescripto (or your database name)
-- Created: 2025-02
-- Description: Additional stored procedures for Patients module
-- =============================================

USE [Prescripto]  -- Change to your database name
GO

-- =============================================
-- Patients_GetByAgentMaster
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetByAgentMaster]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetByAgentMaster]
GO

CREATE PROCEDURE [dbo].[Patients_GetByAgentMaster]
    @AgentMasterID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- This assumes there's a relationship between Patient and AgentMaster
    -- Adjust the query based on your actual schema
    SELECT DISTINCT
        p.[PatientID],
        p.[UserID],
        p.[DateOfBirth],
        p.[Gender],
        p.[Address],
        p.[BloodGroup],
        p.[InsuranceProvider],
        p.[InsurancePolicyNumber],
        p.[PatientAge],
        p.[CreatedAt],
        p.[UpdatedAt],
        p.[IsDeleted],
        p.[PatientReferenceID],
        p.[PatientCode]
    FROM [dbo].[Patient] p
    INNER JOIN [dbo].[MasterDoctor] md ON p.[PatientID] = md.[DoctorID]  -- Adjust join based on actual schema
    WHERE md.[AgentMasterID] = @AgentMasterID
        AND p.[IsDeleted] = 0
    ORDER BY p.[CreatedAt] DESC;
    
    -- Alternative implementation if Patient has direct AgentMasterID relationship:
    -- WHERE p.[AgentMasterID] = @AgentMasterID AND p.[IsDeleted] = 0
END
GO

-- =============================================
-- Patients_GetByAgentSupervisor
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients_GetByAgentSupervisor]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[Patients_GetByAgentSupervisor]
GO

CREATE PROCEDURE [dbo].[Patients_GetByAgentSupervisor]
    @AgentSupervisorID INT
AS
BEGIN
    SET NOCOUNT ON;
    -- This assumes there's a relationship between Patient and AgentSupervisor
    -- Adjust the query based on your actual schema
    SELECT DISTINCT
        p.[PatientID],
        p.[UserID],
        p.[DateOfBirth],
        p.[Gender],
        p.[Address],
        p.[BloodGroup],
        p.[InsuranceProvider],
        p.[InsurancePolicyNumber],
        p.[PatientAge],
        p.[CreatedAt],
        p.[UpdatedAt],
        p.[IsDeleted],
        p.[PatientReferenceID],
        p.[PatientCode]
    FROM [dbo].[Patient] p
    -- Adjust join based on actual schema - this is a placeholder
    -- You may need to join through an Agent or MasterDoctor table
    WHERE p.[IsDeleted] = 0
    ORDER BY p.[CreatedAt] DESC;
    
    -- Alternative implementation if Patient has direct AgentSupervisorID relationship:
    -- WHERE p.[AgentSupervisorID] = @AgentSupervisorID AND p.[IsDeleted] = 0
END
GO

PRINT 'Patients module additional stored procedures created successfully.';
GO

