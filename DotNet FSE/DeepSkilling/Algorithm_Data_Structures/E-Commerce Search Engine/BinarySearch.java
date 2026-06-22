public class BinarySearch {
    // Assumes array sorted by productName (case-insensitive)
    public static int findByName(Product[] products, String name) {
        int lo = 0, hi = products.length - 1;
        while (lo <= hi) {
            int mid = lo + (hi - lo) / 2;
            int cmp = products[mid].getProductName().compareToIgnoreCase(name);
            if (cmp == 0) return mid;
            if (cmp < 0) lo = mid + 1; else hi = mid - 1;
        }
        return -1;
    }
}
