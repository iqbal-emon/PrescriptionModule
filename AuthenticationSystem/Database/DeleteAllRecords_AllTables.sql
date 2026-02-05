USE [PrescriptoAuth]
GO

-- =============================================
-- Script to Delete ALL Records from ALL Tables in Database
-- WARNING: This will delete ALL data from ALL tables in the database!
-- Execute with EXTREME CAUTION. This is a destructive operation.
-- =============================================

PRINT '========================================'
PRINT 'WARNING: This will delete ALL records from ALL tables!'
PRINT '========================================'
PRINT 'Starting deletion process...'
GO

-- =============================================
-- Step 1: Disable ALL foreign key constraints
-- =============================================
PRINT 'Step 1: Disabling all foreign key constraints...'
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO

PRINT 'All foreign key constraints disabled.'
GO

-- =============================================
-- Step 2: Delete from ALL tables
-- =============================================
PRINT 'Step 2: Deleting records from all tables...'
EXEC sp_msforeachtable 'DELETE FROM ?'
GO

PRINT 'All records deleted from all tables.'
GO

-- =============================================
-- Step 3: Re-enable ALL foreign key constraints
-- =============================================
PRINT 'Step 3: Re-enabling all foreign key constraints...'
EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
GO

PRINT 'All foreign key constraints re-enabled.'
GO

-- =============================================
-- Step 4: Reset Identity Columns (Optional)
-- =============================================
PRINT 'Step 4: Resetting identity columns...'
EXEC sp_msforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT(''?'', RESEED, 0)'
GO

PRINT 'All identity columns reset.'
GO

PRINT '========================================'
PRINT 'All records deleted successfully from all tables!'
PRINT 'All identity columns have been reset.'
PRINT '========================================'
GO

