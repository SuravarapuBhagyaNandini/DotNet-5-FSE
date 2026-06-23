# Exercise 1 – Create a Stored Procedure

##  Steps Followed

### Step 1 – Define the Stored Procedure with a DepartmentID Parameter

```sql
CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    ...
END;
```

### Step 2 – SQL Query to Select Employee Details by DepartmentID

```sql
SELECT
    e.EmployeeID, e.FirstName, e.LastName,
    d.DepartmentName, e.Salary, e.JoinDate
FROM Employees   e
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE e.DepartmentID = @DepartmentID
ORDER BY e.LastName, e.FirstName;
```

### Step 3 – sp_InsertEmployee Stored Procedure

```sql
CREATE PROCEDURE sp_InsertEmployee
    @FirstName    VARCHAR(50),
    @LastName     VARCHAR(50),
    @DepartmentID INT,
    @Salary       DECIMAL(10,2),
    @JoinDate     DATE
AS
BEGIN
    INSERT INTO Employees (FirstName, LastName, DepartmentID, Salary, JoinDate)
    VALUES (@FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);

    SELECT SCOPE_IDENTITY() AS NewEmployeeID;
END;
```

---

## 🚀 How to Run

1. Open **SQL Server Management Studio (SSMS)** or **Azure Data Studio**.
2. Run `schema_and_data.sql` first to create tables and insert sample data.
3. Run `exercise1_stored_procedure.sql` to create and test the procedures.

---

##  Expected Output

### EXEC sp_GetEmployeesByDepartment @DepartmentID = 1  (HR)

| EmployeeID | FirstName | LastName | DepartmentName | Salary  | JoinDate   |
|------------|-----------|----------|----------------|---------|------------|
| 1          | John      | Doe      | HR             | 5000.00 | 2020-01-15 |

### EXEC sp_GetEmployeesByDepartment @DepartmentID = 3  (IT)

| EmployeeID | FirstName | LastName | DepartmentName | Salary  | JoinDate   |
|------------|-----------|----------|----------------|---------|------------|
| 3          | Michael   | Johnson  | IT             | 7000.00 | 2018-07-30 |

### EXEC sp_InsertEmployee (Insert Alice Brown)

| NewEmployeeID |
|---------------|
| 5             |

### SELECT * FROM Employees WHERE LastName = 'Brown'

| EmployeeID | FirstName | LastName | DepartmentID | Salary  | JoinDate   |
|------------|-----------|----------|--------------|---------|------------|
| 5          | Alice     | Brown    | 2            | 6500.00 | 2023-06-01 |

---

## 📚 Key Concepts

| Concept              | Description                                                  |
|----------------------|--------------------------------------------------------------|
| Stored Procedure     | Pre-compiled SQL block stored in the database                |
| `@Parameter`         | Input variable passed when executing the procedure           |
| `SCOPE_IDENTITY()`   | Returns the last auto-generated identity value               |
| `SET NOCOUNT ON`     | Suppresses "rows affected" messages for cleaner output       |
| `JOIN`               | Combines Employees and Departments on matching DepartmentID  |
