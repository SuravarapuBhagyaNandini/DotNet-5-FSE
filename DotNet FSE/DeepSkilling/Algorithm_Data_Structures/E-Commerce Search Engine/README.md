**Exercise: E-commerce Platform Search Function**

Overview
- Implement linear and binary search over product collections and compare complexities.

Files
- `Product.java` : product model (implements Comparable by name).
- `LinearSearch.java` : linear search implementation.
- `BinarySearch.java` : binary search implementation (array must be sorted by name).
- `Main.java` : runner demonstrating both searches.

Build & Run
```
javac *.java
java Main
```

Expected Output
```
Linear search: find 'Notebook'
Found: Product{id=4, name='Notebook', category='Office'}
Binary search: find 'Desk Lamp'
Found: Product{id=5, name='Desk Lamp', category='Home'}
```

Analysis
- Big O notation: describes asymptotic upper bounds on runtime in terms of input size `n`.
- Linear search: O(n) time — checks items sequentially. Best-case O(1), worst-case O(n), average O(n).
- Binary search: O(log n) time — requires sorted input, repeatedly halves search space. Best-case O(1), worst-case O(log n), average O(log n).
- Choice for platform: if data is frequently searched and mostly read-only, maintain sorted arrays or indexed structures (binary search or B-trees) for O(log n) lookups; if data is small or unsorted with frequent writes, linear search may be acceptable but scales poorly.
