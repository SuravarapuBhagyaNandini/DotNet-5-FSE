# Exercise 5 – Return Data from a Stored Procedure

##  Steps Followed

### Step 1 – Define the Stored Procedure with a DepartmentID Parameter

```sql
CREATE PROCEDURE sp_GetEmployeeCountByDepartment
    @DepartmentID INT
AS
BEGIN
    ...
END;
```

### Step 2 – SQL Query to COUNT Employees in the Specified Department

```sql
SELECT
    d.DepartmentName,
    COUNT(e.EmployeeID) AS TotalEmployees
FROM Departments d
LEFT JOIN Employees e ON e.DepartmentID = d.DepartmentID
WHERE d.DepartmentID = @DepartmentID
GROUP BY d.DepartmentName;
```

A `LEFT JOIN` is used so that departments with zero employees still appear
in the result with a count of `0`.

### Step 3 – Execute / Save the Stored Procedure

```sql
-- Part A: result-set approach
EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 1;

-- Part B: OUTPUT parameter approach
DECLARE @Count INT;
EXEC sp_GetEmployeeCountByDepartment_Output
    @DepartmentID  = 3,
    @EmployeeCount = @Count OUTPUT;
SELECT @Count AS ITEmployeeCount;

-- Part C: all departments at once
EXEC sp_GetAllDepartmentCounts;
```

---

##  How to Run

1. Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**.
2. Run `schema_and_data.sql` first to create tables and insert sample data.
3. Run `exercise5_return_data.sql` to create and test all procedures.

---

## Expected Output

### EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 1  (HR)

| DepartmentName | TotalEmployees |
|----------------|----------------|
| HR             | 1              |

### EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 3  (IT)

| DepartmentName | TotalEmployees |
|----------------|----------------|
| IT             | 1              |

### EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 2  (Finance)

| DepartmentName | TotalEmployees |
|----------------|----------------|
| Finance        | 1              |

### OUTPUT Parameter – ITEmployeeCount

| ITEmployeeCount |
|-----------------|
| 1               |

### EXEC sp_GetAllDepartmentCounts

| DepartmentID | DepartmentName | TotalEmployees |
|--------------|----------------|----------------|
| 1            | HR             | 1              |
| 2            | Finance        | 1              |
| 3            | IT             | 1              |
| 4            | Marketing      | 1              |

---

## Key Concepts

| Concept            | Description                                                        |
|--------------------|--------------------------------------------------------------------|
| `COUNT()`          | Aggregate function — counts non-NULL values in a column            |
| `LEFT JOIN`        | Includes departments even when no employees are assigned           |
| `GROUP BY`         | Groups rows by department so COUNT works per department            |
| `OUTPUT` parameter | Lets the procedure write a value back to the caller's variable     |
| `DECLARE`          | Declares a local variable to capture the OUTPUT parameter value    |
| Result-set return  | Simplest approach — returns a table the caller can read directly   |

---

## 🔄 Difference Between Return Approaches

| Approach         | How data comes back          | Best used when …                          |
|------------------|------------------------------|-------------------------------------------|
| SELECT (result set) | As a table of rows        | Caller needs multiple columns / rows      |
| OUTPUT parameter | As a scalar variable         | Caller needs a single numeric value       |
| All-departments  | One result set, all rows     | Dashboard / reporting queries             |
