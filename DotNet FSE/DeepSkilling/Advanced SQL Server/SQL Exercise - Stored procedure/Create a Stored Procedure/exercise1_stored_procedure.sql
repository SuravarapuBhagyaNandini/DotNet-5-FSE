-- ============================================================
--  Exercise 1: Create a Stored Procedure
--  Goal: Retrieve employee details by department &
--        insert a new employee record.
-- ============================================================


-- ── PART A ───────────────────────────────────────────────────
-- Stored Procedure: sp_GetEmployeesByDepartment
-- Retrieves all employee details for a given DepartmentID.
-- ─────────────────────────────────────────────────────────────

IF OBJECT_ID('sp_GetEmployeesByDepartment', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetEmployeesByDepartment;
GO

CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT          -- Parameter: the department to filter on
AS
BEGIN
    SET NOCOUNT ON;

    -- Step 2: Select employee details joined with department name
    SELECT
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        d.DepartmentName,
        e.Salary,
        e.JoinDate
    FROM Employees   e
    JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE e.DepartmentID = @DepartmentID
    ORDER BY e.LastName, e.FirstName;
END;
GO


-- ── PART B ───────────────────────────────────────────────────
-- Stored Procedure: sp_InsertEmployee
-- Inserts a new employee record into the Employees table.
-- ─────────────────────────────────────────────────────────────

IF OBJECT_ID('sp_InsertEmployee', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertEmployee;
GO

CREATE PROCEDURE sp_InsertEmployee
    @FirstName    VARCHAR(50),
    @LastName     VARCHAR(50),
    @DepartmentID INT,
    @Salary       DECIMAL(10,2),
    @JoinDate     DATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate)
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);

    -- Return the newly inserted employee's ID
    SELECT SCOPE_IDENTITY() AS NewEmployeeID;
END;
GO


-- ============================================================
--  TEST / EXECUTION EXAMPLES
-- ============================================================

-- Test sp_GetEmployeesByDepartment  →  returns HR employees
EXEC sp_GetEmployeesByDepartment @DepartmentID = 1;

-- Test sp_GetEmployeesByDepartment  →  returns IT employees
EXEC sp_GetEmployeesByDepartment @DepartmentID = 3;

-- Test sp_InsertEmployee  →  adds a new Finance employee
EXEC sp_InsertEmployee
    @FirstName    = 'Alice',
    @LastName     = 'Brown',
    @DepartmentID = 2,
    @Salary       = 6500.00,
    @JoinDate     = '2023-06-01';

-- Verify the new row was inserted
SELECT * FROM Employees WHERE LastName = 'Brown';
