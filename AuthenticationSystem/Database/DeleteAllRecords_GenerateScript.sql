USE [PrescriptoAuth]
GO

-- =============================================
-- Script to Generate DELETE Statements for All Tables
-- This script generates the DELETE SQL statements
-- that you can review before executing
-- =============================================

PRINT '========================================'
PRINT 'Generating DELETE Script for All Tables'
PRINT '========================================'
GO

-- =============================================
-- Generate DELETE statements (for review)
-- =============================================

PRINT '-- ============================================='
PRINT '-- Generated DELETE Script'
PRINT '-- Review this script before executing!'
PRINT '-- ============================================='
PRINT ''
PRINT 'USE [PrescriptoAuth]'
PRINT 'GO'
PRINT ''
PRINT '-- Disable all foreign key constraints'
PRINT 'EXEC sp_msforeachtable ''ALTER TABLE ? NOCHECK CONSTRAINT ALL'''
PRINT 'GO'
PRINT ''
PRINT '-- Delete from all tables'
PRINT ''

-- Generate DELETE statements for each table
SELECT 
    'DELETE FROM ' + QUOTENAME(s.name) + '.' + QUOTENAME(t.name) + ';' AS DeleteStatement
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE t.type = 'U'  -- User tables only
    AND t.is_ms_shipped = 0  -- Exclude system tables
ORDER BY t.name

PRINT ''
PRINT '-- Re-enable all foreign key constraints'
PRINT 'EXEC sp_msforeachtable ''ALTER TABLE ? CHECK CONSTRAINT ALL'''
PRINT 'GO'
PRINT ''
PRINT '-- Reset identity columns'
PRINT 'EXEC sp_msforeachtable ''IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1 DBCC CHECKIDENT(''?'', RESEED, 0)'''
PRINT 'GO'
PRINT ''
PRINT 'PRINT ''All records deleted successfully!'''
PRINT 'GO'

PRINT ''
PRINT '========================================'
PRINT 'Script generation complete!'
PRINT 'Copy the generated statements above and execute them.'
PRINT '========================================'
GO

