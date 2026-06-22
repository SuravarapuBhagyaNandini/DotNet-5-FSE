-- ============================================================
-- Assignment 3: SQL Ranking & Window Functions
-- Goal: ROW_NUMBER(), RANK(), DENSE_RANK(), OVER(), PARTITION BY
-- ============================================================

-- ─── STEP 1: Create and populate the Products table ──────────

CREATE TABLE Products (
    ProductID   INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category    VARCHAR(50),
    Price       DECIMAL(10, 2)
);

INSERT INTO Products (ProductID, ProductName, Category, Price) VALUES
(1,  'Laptop Pro 15',     'Electronics',  1500.00),
(2,  'Wireless Mouse',    'Electronics',   25.00),
(3,  'Mechanical Keyboard','Electronics',  120.00),
(4,  'USB-C Hub',         'Electronics',   75.00),
(5,  'Monitor 27"',       'Electronics',  450.00),
(6,  'Desk Chair Ergo',   'Furniture',    350.00),
(7,  'Standing Desk',     'Furniture',    600.00),
(8,  'Bookshelf Oak',     'Furniture',    200.00),
(9,  'Filing Cabinet',    'Furniture',    150.00),
(10, 'Lamp LED',          'Furniture',     80.00),
(11, 'Python Cookbook',   'Books',         45.00),
(12, 'Clean Code',        'Books',         40.00),
(13, 'Design Patterns',   'Books',         55.00),
(14, 'The Pragmatic Prog','Books',         50.00),
(15, 'SICP',              'Books',         50.00);  -- Same price as row 14 → ties!


-- ─── STEP 2: ROW_NUMBER() ────────────────────────────────────
-- Assigns a UNIQUE sequential number within each Category,
-- ordered by Price DESC (most expensive = 1).
-- Ties get DIFFERENT numbers — the order between ties is arbitrary.

SELECT
    Category,
    ProductName,
    Price,
    ROW_NUMBER() OVER (
        PARTITION BY Category       -- restart numbering for each category
        ORDER BY Price DESC         -- rank most expensive first
    ) AS RowNum
FROM Products
ORDER BY Category, RowNum;

/*
Expected Output (ROW_NUMBER):
Category      | ProductName         | Price   | RowNum
--------------+---------------------+---------+-------
Books         | Design Patterns     |  55.00  |  1
Books         | The Pragmatic Prog  |  50.00  |  2    ← tie: one gets 2, other gets 3
Books         | SICP                |  50.00  |  3    ← tie: arbitrary but unique
Books         | Python Cookbook     |  45.00  |  4
Books         | Clean Code          |  40.00  |  5
Electronics   | Laptop Pro 15       | 1500.00 |  1
Electronics   | Monitor 27"         |  450.00 |  2
...
*/


-- ─── STEP 3: RANK() ──────────────────────────────────────────
-- Assigns a rank within each partition. TIES get the SAME rank,
-- but the NEXT rank SKIPS the duplicated numbers.
-- (e.g., two items at rank 2 → next item is rank 4, NOT 3)

SELECT
    Category,
    ProductName,
    Price,
    RANK() OVER (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS PriceRank
FROM Products
ORDER BY Category, PriceRank;

/*
Expected Output (RANK):
Category | ProductName         | Price  | PriceRank
---------+---------------------+--------+----------
Books    | Design Patterns     | 55.00  |  1
Books    | The Pragmatic Prog  | 50.00  |  2    ← both tied rows get rank 2
Books    | SICP                | 50.00  |  2    ←
Books    | Python Cookbook     | 45.00  |  4    ← skips 3 (gap after tie)
Books    | Clean Code          | 40.00  |  5
*/


-- ─── STEP 4: DENSE_RANK() ────────────────────────────────────
-- Like RANK(), ties get the SAME rank, but the NEXT rank does
-- NOT skip — it's always consecutive. No gaps.

SELECT
    Category,
    ProductName,
    Price,
    DENSE_RANK() OVER (
        PARTITION BY Category
        ORDER BY Price DESC
    ) AS DenseRank
FROM Products
ORDER BY Category, DenseRank;

/*
Expected Output (DENSE_RANK):
Category | ProductName         | Price  | DenseRank
---------+---------------------+--------+----------
Books    | Design Patterns     | 55.00  |  1
Books    | The Pragmatic Prog  | 50.00  |  2    ← both tied rows get rank 2
Books    | SICP                | 50.00  |  2    ←
Books    | Python Cookbook     | 45.00  |  3    ← NO gap — consecutive after tie
Books    | Clean Code          | 40.00  |  4
*/


-- ─── STEP 5: Side-by-side comparison of all three ─────────────
-- Seeing ROW_NUMBER, RANK, and DENSE_RANK together makes the
-- difference in tie-handling immediately visible.

SELECT
    Category,
    ProductName,
    Price,
    ROW_NUMBER()  OVER (PARTITION BY Category ORDER BY Price DESC) AS RowNum,
    RANK()        OVER (PARTITION BY Category ORDER BY Price DESC) AS RankNum,
    DENSE_RANK()  OVER (PARTITION BY Category ORDER BY Price DESC) AS DenseRankNum
FROM Products
ORDER BY Category, Price DESC;

/*
Expected Output (All Three — Books category):
Category | ProductName         | Price  | RowNum | RankNum | DenseRankNum
---------+---------------------+--------+--------+---------+-------------
Books    | Design Patterns     | 55.00  |   1    |    1    |      1
Books    | The Pragmatic Prog  | 50.00  |   2    |    2    |      2      ← tie
Books    | SICP                | 50.00  |   3    |    2    |      2      ← tie
Books    | Python Cookbook     | 45.00  |   4    |    4    |      3      ← gap vs no gap
Books    | Clean Code          | 40.00  |   5    |    5    |      4

KEY DIFFERENCES:
- ROW_NUMBER: always unique (2, 3 for ties)
- RANK:       same for ties, skips next (2, 2, then 4)
- DENSE_RANK: same for ties, no skip   (2, 2, then 3)
*/


-- ─── STEP 6: Filter — Top 3 per Category using CTE ───────────
-- Wrap the ranking in a CTE (Common Table Expression) and filter
-- to retrieve only the top 3 most expensive products per category.

WITH RankedProducts AS (
    SELECT
        Category,
        ProductName,
        Price,
        DENSE_RANK() OVER (
            PARTITION BY Category
            ORDER BY Price DESC
        ) AS DenseRank
    FROM Products
)
SELECT
    Category,
    ProductName,
    Price,
    DenseRank
FROM RankedProducts
WHERE DenseRank <= 3
ORDER BY Category, DenseRank;

/*
Expected Output (Top 3 per Category):
Category      | ProductName          | Price   | DenseRank
--------------+----------------------+---------+----------
Books         | Design Patterns      |  55.00  |  1
Books         | The Pragmatic Prog   |  50.00  |  2
Books         | SICP                 |  50.00  |  2   ← both rank 2, both included
Books         | Python Cookbook      |  45.00  |  3
Electronics   | Laptop Pro 15        | 1500.00 |  1
Electronics   | Monitor 27"          |  450.00 |  2
Electronics   | Mechanical Keyboard  |  120.00 |  3
Furniture     | Standing Desk        |  600.00 |  1
Furniture     | Desk Chair Ergo      |  350.00 |  2
Furniture     | Bookshelf Oak        |  200.00 |  3
*/
