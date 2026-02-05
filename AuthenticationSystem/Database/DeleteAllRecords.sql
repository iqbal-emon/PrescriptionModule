USE [PrescriptoAuth]
GO

-- =============================================
-- Script to Delete All Records from Tables
-- WARNING: This will delete ALL data from the specified tables!
-- Execute with caution. This is a destructive operation.
-- =============================================

PRINT 'Starting deletion of all records...'
GO

-- =============================================
-- Method 1: Delete from Specific Tables (Recommended)
-- This method deletes only from the new AuthenticationSystem tables
-- =============================================

-- Step 1: Disable foreign key constraints for our tables
PRINT 'Disabling foreign key constraints...'
GO

-- Disable FK constraints for specific tables
ALTER TABLE [dbo].[CompanyBranch] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[RolePermission] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Users] NOCHECK CONSTRAINT ALL
GO

PRINT 'Foreign key constraints disabled.'
GO

-- Step 2: Delete records from tables (in safe order)
PRINT 'Deleting records from tables...'
GO

-- Delete from RolePermission (has FKs to Roles and Permissions)
PRINT 'Deleting records from RolePermission...'
DELETE FROM [dbo].[RolePermission]
PRINT 'RolePermission records deleted.'
GO

-- Delete from Users (has FKs to Roles, Tenants, and self-reference)
PRINT 'Deleting records from Users...'
-- First, set ReferenceUserId to NULL to break self-referencing
UPDATE [dbo].[Users] SET [ReferenceUserId] = NULL WHERE [ReferenceUserId] IS NOT NULL
DELETE FROM [dbo].[Users]
PRINT 'Users records deleted.'
GO

-- Delete from CompanyBranch (has FK to Company)
PRINT 'Deleting records from CompanyBranch...'
DELETE FROM [dbo].[CompanyBranch]
PRINT 'CompanyBranch records deleted.'
GO

-- Delete from Permissions
PRINT 'Deleting records from Permissions...'
DELETE FROM [dbo].[Permissions]
PRINT 'Permissions records deleted.'
GO

-- Delete from Roles
PRINT 'Deleting records from Roles...'
DELETE FROM [dbo].[Roles]
PRINT 'Roles records deleted.'
GO

-- Delete from Company
PRINT 'Deleting records from Company...'
DELETE FROM [dbo].[Company]
PRINT 'Company records deleted.'
GO

-- Step 3: Re-enable foreign key constraints
PRINT 'Re-enabling foreign key constraints...'
GO

ALTER TABLE [dbo].[CompanyBranch] CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT ALL
GO

PRINT 'Foreign key constraints re-enabled.'
GO

-- =============================================
-- Reset Identity Columns (Optional - Uncomment if needed)
-- =============================================

-- Reset identity seed to start from 1 again
-- Uncomment the following lines if you want to reset identity columns

/*
PRINT 'Resetting identity columns...'
DBCC CHECKIDENT ('[dbo].[Company]', RESEED, 0)
DBCC CHECKIDENT ('[dbo].[CompanyBranch]', RESEED, 0)
DBCC CHECKIDENT ('[dbo].[Permissions]', RESEED, 0)
DBCC CHECKIDENT ('[dbo].[Roles]', RESEED, 0)
DBCC CHECKIDENT ('[dbo].[RolePermission]', RESEED, 0)
DBCC CHECKIDENT ('[dbo].[Users]', RESEED, 0)
PRINT 'Identity columns reset.'
GO
*/

PRINT 'All records deleted successfully!'
PRINT 'Note: Identity columns are NOT reset. Uncomment the DBCC CHECKIDENT section if you want to reset them.'
GO

-- =============================================
-- Method 2: Delete from ALL Tables in Database (Use with EXTREME CAUTION!)
-- Uncomment the section below ONLY if you want to delete from ALL tables
-- =============================================

/*
USE [PrescriptoAuth]
GO

PRINT 'WARNING: This will delete ALL records from ALL tables in the database!'
PRINT 'Starting deletion process...'
GO

-- Disable all foreign key constraints
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO

PRINT 'All foreign key constraints disabled.'
GO

-- Delete from all tables
EXEC sp_msforeachtable 'DELETE FROM ?'
GO

PRINT 'All records deleted from all tables.'
GO

-- Re-enable all foreign key constraints
EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
GO

PRINT 'All foreign key constraints re-enabled.'
GO

PRINT 'All records deleted successfully from all tables!'
GO
*/

