USE [PrescriptoAuth]
GO

-- =============================================
-- Dynamic Script to Delete All Records from All Tables
-- This script:
-- 1. Gets all table names from the database
-- 2. Generates DELETE statements dynamically
-- 3. Handles foreign key constraints
-- WARNING: This will delete ALL data from ALL tables!
-- =============================================

PRINT '========================================'
PRINT 'Dynamic Delete All Records Script'
PRINT '========================================'
PRINT 'Starting process...'
GO

-- =============================================
-- Step 1: Get all table names and disable foreign keys
-- =============================================
PRINT 'Step 1: Getting all table names and disabling foreign key constraints...'
GO

-- Create a temporary table to store table names
IF OBJECT_ID('tempdb..#TablesToDelete') IS NOT NULL
    DROP TABLE #TablesToDelete
GO

CREATE TABLE #TablesToDelete (
    TableName NVARCHAR(255),
    SchemaName NVARCHAR(255),
    FullTableName NVARCHAR(512)
)
GO

-- Get all user tables (exclude system tables)
INSERT INTO #TablesToDelete (TableName, SchemaName, FullTableName)
SELECT 
    t.name AS TableName,
    s.name AS SchemaName,
    QUOTENAME(s.name) + '.' + QUOTENAME(t.name) AS FullTableName
FROM sys.tables t
INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE t.type = 'U'  -- User tables only
    AND t.is_ms_shipped = 0  -- Exclude system tables
ORDER BY t.name
GO

-- Display tables that will be deleted
PRINT 'Tables found:'
SELECT FullTableName AS 'Table to Delete From' FROM #TablesToDelete
GO

-- Disable all foreign key constraints
PRINT 'Disabling all foreign key constraints...'
DECLARE @sql NVARCHAR(MAX) = ''
SELECT @sql = @sql + 'ALTER TABLE ' + FullTableName + ' NOCHECK CONSTRAINT ALL;' + CHAR(13)
FROM #TablesToDelete
EXEC sp_executesql @sql
GO

PRINT 'All foreign key constraints disabled.'
GO

-- =============================================
-- Step 2: Generate and execute DELETE statements
-- =============================================
PRINT 'Step 2: Generating DELETE statements and deleting records...'
GO

DECLARE @DeleteSQL NVARCHAR(MAX) = ''
DECLARE @TableName NVARCHAR(512)
DECLARE @RowCount INT = 0

-- Cursor to iterate through tables
DECLARE table_cursor CURSOR FOR
SELECT FullTableName FROM #TablesToDelete

OPEN table_cursor
FETCH NEXT FROM table_cursor INTO @TableName

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Build DELETE statement
    SET @DeleteSQL = 'DELETE FROM ' + @TableName
    
    -- Execute DELETE
    BEGIN TRY
        EXEC sp_executesql @DeleteSQL
        SET @RowCount = @@ROWCOUNT
        PRINT 'Deleted from ' + @TableName + ' - Rows affected: ' + CAST(@RowCount AS NVARCHAR(10))
    END TRY
    BEGIN CATCH
        PRINT 'Error deleting from ' + @TableName + ': ' + ERROR_MESSAGE()
    END CATCH
    
    FETCH NEXT FROM table_cursor INTO @TableName
END

CLOSE table_cursor
DEALLOCATE table_cursor
GO

PRINT 'All DELETE statements executed.'
GO

-- =============================================
-- Step 3: Re-enable foreign key constraints
-- =============================================
PRINT 'Step 3: Re-enabling foreign key constraints...'
GO

DECLARE @EnableSQL NVARCHAR(MAX) = ''
SELECT @EnableSQL = @EnableSQL + 'ALTER TABLE ' + FullTableName + ' CHECK CONSTRAINT ALL;' + CHAR(13)
FROM #TablesToDelete
EXEC sp_executesql @EnableSQL
GO

PRINT 'All foreign key constraints re-enabled.'
GO

-- =============================================
-- Step 4: Reset Identity Columns (Optional)
-- =============================================
PRINT 'Step 4: Resetting identity columns...'
GO

DECLARE @ResetSQL NVARCHAR(MAX) = ''
DECLARE @ResetTableName NVARCHAR(512)

-- Get tables with identity columns
DECLARE reset_cursor CURSOR FOR
SELECT FullTableName 
FROM #TablesToDelete t
WHERE EXISTS (
    SELECT 1 
    FROM sys.columns c 
    INNER JOIN sys.tables tab ON c.object_id = tab.object_id
    INNER JOIN sys.schemas s ON tab.schema_id = s.schema_id
    WHERE c.is_identity = 1 
        AND tab.name = t.TableName
        AND s.name = t.SchemaName
)

OPEN reset_cursor
FETCH NEXT FROM reset_cursor INTO @ResetTableName

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @ResetSQL = 'DBCC CHECKIDENT(''' + @ResetTableName + ''', RESEED, 0)'
    BEGIN TRY
        EXEC sp_executesql @ResetSQL
        PRINT 'Reset identity for ' + @ResetTableName
    END TRY
    BEGIN CATCH
        PRINT 'Error resetting identity for ' + @ResetTableName + ': ' + ERROR_MESSAGE()
    END CATCH
    
    FETCH NEXT FROM reset_cursor INTO @ResetTableName
END

CLOSE reset_cursor
DEALLOCATE reset_cursor
GO

-- Clean up temporary table
DROP TABLE #TablesToDelete
GO

PRINT '========================================'
PRINT 'All records deleted successfully!'
PRINT 'All identity columns have been reset.'
PRINT '========================================'
GO

