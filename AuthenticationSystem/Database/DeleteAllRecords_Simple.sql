USE [PrescriptoAuth]
GO

-- =============================================
-- Simple Script to Delete All Records from All Tables
-- Gets table names dynamically and deletes from them
-- =============================================

PRINT 'Starting deletion process...'
GO

-- Step 1: Disable all foreign key constraints
PRINT 'Step 1: Disabling all foreign key constraints...'
EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO

PRINT 'All foreign key constraints disabled.'
GO

-- Step 2: Delete from all tables
PRINT 'Step 2: Deleting records from all tables...'
EXEC sp_msforeachtable 'DELETE FROM ?'
GO

PRINT 'All records deleted.'
GO

-- Step 3: Re-enable all foreign key constraints
PRINT 'Step 3: Re-enabling all foreign key constraints...'
EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
GO

PRINT 'All foreign key constraints re-enabled.'
GO

-- Step 4: Reset identity columns
PRINT 'Step 4: Resetting identity columns...'
EXEC sp_msforeachtable 'IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT(''?'', RESEED, 0)'
GO

PRINT 'All identity columns reset.'
GO

PRINT '========================================'
PRINT 'All records deleted successfully!'
PRINT '========================================'
GO

