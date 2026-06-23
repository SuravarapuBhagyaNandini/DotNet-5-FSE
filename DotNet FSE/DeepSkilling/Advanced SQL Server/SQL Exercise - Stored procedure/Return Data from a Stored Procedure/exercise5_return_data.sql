-- ============================================================
--  Exercise 5: Return Data from a Stored Procedure
--  Goal: Return the total number of employees in a department.
-- ============================================================


-- ── PART A ───────────────────────────────────────────────────
-- Stored Procedure: sp_GetEmployeeCountByDepartment
-- Returns the total count of employees for a given DepartmentID
-- using a SELECT (result set approach).
-- ─────────────────────────────────────────────────────────────

IF OBJECT_ID('sp_GetEmployeeCountByDepartment', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetEmployeeCountByDepartment;
GO

CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT          -- Step 1: Parameter for DepartmentID
AS
BEGIN
    SET NOCOUNT ON;

    -- Step 2: COUNT employees in the specified department
    SELECT
        d.DepartmentName,
        COUNT(e.EmployeeID) AS TotalEmployees
    FROM Departments d
    LEFT JOIN Employees e ON e.DepartmentID = d.DepartmentID
    WHERE d.DepartmentID = @DepartmentID
    GROUP BY d.DepartmentName;
END;
GO


-- ── PART B ───────────────────────────────────────────────────
-- Stored Procedure: sp_GetEmployeeCountByDepartment_Output
-- Returns the count via an OUTPUT parameter (alternative approach).
-- ─────────────────────────────────────────────────────────────

IF OBJECT_ID('sp_GetEmployeeCountByDepartment_Output', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetEmployeeCountByDepartment_Output;
GO

CREATE PROCEDURE sp_GetEmployeeCountByDepartment_Output
    @DepartmentID   INT,
    @EmployeeCount  INT OUTPUT   -- OUTPUT parameter to return the count
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @EmployeeCount = COUNT(EmployeeID)
    FROM Employees
    WHERE DepartmentID = @DepartmentID;
END;
GO


-- ── PART C ───────────────────────────────────────────────────
-- Stored Procedure: sp_GetAllDepartmentCounts
-- Returns employee counts for ALL departments in one call.
-- ─────────────────────────────────────────────────────────────

IF OBJECT_ID('sp_GetAllDepartmentCounts', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllDepartmentCounts;
GO

CREATE PROCEDURE sp_GetAllDepartmentCounts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.DepartmentID,
        d.DepartmentName,
        COUNT(e.EmployeeID) AS TotalEmployees
    FROM Departments d
    LEFT JOIN Employees e ON e.DepartmentID = d.DepartmentID
    GROUP BY d.DepartmentID, d.DepartmentName
    ORDER BY d.DepartmentID;
END;
GO


-- ============================================================
--  TEST / EXECUTION EXAMPLES
-- ============================================================

-- Step 3: Execute sp_GetEmployeeCountByDepartment
-- Test: Count employees in HR (DepartmentID = 1)
EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 1;

-- Test: Count employees in IT (DepartmentID = 3)
EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 3;

-- Test: Count employees in Finance (DepartmentID = 2)
EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 2;

-- Test OUTPUT parameter version
DECLARE @Count INT;
EXEC sp_GetEmployeeCountByDepartment_Output
    @DepartmentID  = 3,
    @EmployeeCount = @Count OUTPUT;
SELECT @Count AS ITEmployeeCount;

-- Test: All department counts in one call
EXEC sp_GetAllDepartmentCounts;
