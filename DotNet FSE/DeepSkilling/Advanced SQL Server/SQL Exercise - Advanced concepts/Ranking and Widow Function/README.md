# Assignment 3 — SQL Ranking & Window Functions

## Overview
This assignment demonstrates SQL **Window Functions**: `ROW_NUMBER()`, `RANK()`, `DENSE_RANK()`, used with `OVER()` and `PARTITION BY` to find the **top 3 most expensive products per category** — without collapsing rows like `GROUP BY` would.

---

## File

```
Assignment3_SQL_WindowFunctions/
├── window_functions.sql   ← All SQL: table creation, data, all queries
└── README.md
```

---

## What is a Window Function?

A **window function** performs a calculation across a **set of rows related to the current row** (the "window"), without collapsing them into a single output row. Unlike `GROUP BY`, each source row still appears in the result.

```sql
FUNCTION_NAME() OVER (
    PARTITION BY column   -- defines the "window" / groups
    ORDER BY column DESC  -- defines the order within the window
)
```

---

## Sample Data — Products Table

| ProductID | ProductName | Category | Price |
|---|---|---|---|
| 1 | Laptop Pro 15 | Electronics | 1500.00 |
| 5 | Monitor 27" | Electronics | 450.00 |
| 3 | Mechanical Keyboard | Electronics | 120.00 |
| 7 | Standing Desk | Furniture | 600.00 |
| 6 | Desk Chair Ergo | Furniture | 350.00 |
| 13 | Design Patterns | Books | 55.00 |
| 14 | The Pragmatic Programmer | Books | 50.00 |
| 15 | SICP | Books | 50.00 |
| ... | ... | ... | ... |

Note: "The Pragmatic Programmer" and "SICP" both cost $50 — a deliberate **tie** to show ranking differences.

---

## The Three Ranking Functions

### ROW_NUMBER()
Assigns a **unique** sequential number — ties get different numbers.

```sql
ROW_NUMBER() OVER (PARTITION BY Category ORDER BY Price DESC)
```

| ProductName | Price | RowNum |
|---|---|---|
| Design Patterns | 55.00 | 1 |
| The Pragmatic Prog | 50.00 | **2** |
| SICP | 50.00 | **3** ← unique even though same price |
| Python Cookbook | 45.00 | 4 |

---

### RANK()
Ties get the **same rank**; the next rank **skips** (leaves a gap).

```sql
RANK() OVER (PARTITION BY Category ORDER BY Price DESC)
```

| ProductName | Price | RankNum |
|---|---|---|
| Design Patterns | 55.00 | 1 |
| The Pragmatic Prog | 50.00 | **2** |
| SICP | 50.00 | **2** ← same rank |
| Python Cookbook | 45.00 | **4** ← skips 3 |

---

### DENSE_RANK()
Ties get the **same rank**; the next rank **does NOT skip**.

```sql
DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC)
```

| ProductName | Price | DenseRank |
|---|---|---|
| Design Patterns | 55.00 | 1 |
| The Pragmatic Prog | 50.00 | **2** |
| SICP | 50.00 | **2** ← same rank |
| Python Cookbook | 45.00 | **3** ← no gap |

---

## Quick Comparison Table

| Scenario | ROW_NUMBER | RANK | DENSE_RANK |
|---|---|---|---|
| Ties allowed? | No (always unique) | Yes | Yes |
| Gap after tie? | N/A | **Yes** | **No** |
| Use when… | Need unique IDs | Sports leaderboard | Exam percentile grades |

---

## Top 3 per Category — Final Query (CTE pattern)

```sql
WITH RankedProducts AS (
    SELECT
        Category, ProductName, Price,
        DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS DenseRank
    FROM Products
)
SELECT * FROM RankedProducts WHERE DenseRank <= 3
ORDER BY Category, DenseRank;
```

### Expected Output

| Category | ProductName | Price | DenseRank |
|---|---|---|---|
| Books | Design Patterns | 55.00 | 1 |
| Books | The Pragmatic Prog | 50.00 | 2 |
| Books | SICP | 50.00 | 2 |
| Books | Python Cookbook | 45.00 | 3 |
| Electronics | Laptop Pro 15 | 1500.00 | 1 |
| Electronics | Monitor 27" | 450.00 | 2 |
| Electronics | Mechanical Keyboard | 120.00 | 3 |
| Furniture | Standing Desk | 600.00 | 1 |
| Furniture | Desk Chair Ergo | 350.00 | 2 |
| Furniture | Bookshelf Oak | 200.00 | 3 |

> Using `DENSE_RANK` with `<= 3` means both tied $50 books appear — neither is unfairly excluded.

---

## How to Run

Paste the contents of `window_functions.sql` into any SQL client:

- **SQL Server**: SQL Server Management Studio (SSMS) or Azure Data Studio
- **PostgreSQL**: pgAdmin or `psql`
- **MySQL 8+**: MySQL Workbench (window functions supported from v8.0)
- **SQLite**: DB Browser for SQLite (v3.25+)
- **Online**: https://sqliteonline.com or https://sqlfiddle.com

Run each query block separately (separated by the `--` comment headers) to see each step's output.
