Exercise 2: E-commerce Platform Search Function

This exercise demonstrates linear search and binary search for an e-commerce product catalog.

Steps covered:
1. Understand asymptotic notation: linear search is O(n), binary search is O(log n) on sorted input.
2. Setup: `Product` class with `productId`, `productName`, and `category`.
3. Implementation: `SearchDemo.java` provides `linearSearch` and `binarySearch` methods and a `main` demonstrating both.
4. Analysis: README notes time complexity and the cost of sorting before binary search.

Compile and run (from workspace or project root):

```bash
javac -d out "Exercise 2 - E-commerce Platform Search Function"/*.java
java -cp out SearchDemo
```

Expected output example:
- Linear search: index=6 item=Product{...} time=... ns
- Binary search: index=... item=Product{...} time=... ns
- Complexity notes

Notes:
- Binary search requires sorted data (we sort a copy in the demo).
- For large catalogs, prefer indexed/search engines (Elasticsearch) or in-memory hash maps for exact matches (O(1)).
