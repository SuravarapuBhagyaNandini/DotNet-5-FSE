import java.util.Arrays;

public class SearchDemo {
    // Linear search by product name
    public static int linearSearch(Product[] arr, String target) {
        for (int i = 0; i < arr.length; i++) {
            if (arr[i].getProductName().equalsIgnoreCase(target)) return i;
        }
        return -1;
    }

    // Binary search assumes array is sorted by productName (case-insensitive)
    public static int binarySearch(Product[] arr, String target) {
        int low = 0, high = arr.length - 1;
        while (low <= high) {
            int mid = (low + high) >>> 1;
            int cmp = arr[mid].getProductName().compareToIgnoreCase(target);
            if (cmp == 0) return mid;
            if (cmp < 0) low = mid + 1;
            else high = mid - 1;
        }
        return -1;
    }

    public static void main(String[] args) {
        Product[] products = new Product[] {
            new Product(1, "Apple iPhone 14", "Mobiles"),
            new Product(2, "Samsung Galaxy S22", "Mobiles"),
            new Product(3, "Sony WH-1000XM4", "Audio"),
            new Product(4, "Dell XPS 13", "Laptops"),
            new Product(5, "HP Envy 15", "Laptops"),
            new Product(6, "Logitech MX Master 3", "Accessories"),
            new Product(7, "Apple AirPods Pro", "Audio"),
            new Product(8, "Amazon Echo Dot", "Smart Home"),
            new Product(9, "Google Nest Hub", "Smart Home"),
            new Product(10, "Canon EOS R5", "Cameras")
        };

        String target = "Apple AirPods Pro";

        // Linear search on unsorted array
        long t0 = System.nanoTime();
        int idxLinear = linearSearch(products, target);
        long t1 = System.nanoTime();
        System.out.println("Linear search: index=" + idxLinear + " item=" + (idxLinear>=0?products[idxLinear]:"not found") + " time=" + (t1-t0) + " ns");

        // For binary search we need sorted copy
        Product[] sorted = Arrays.copyOf(products, products.length);
        Arrays.sort(sorted);

        long t2 = System.nanoTime();
        int idxBinary = binarySearch(sorted, target);
        long t3 = System.nanoTime();
        System.out.println("Binary search: index=" + idxBinary + " item=" + (idxBinary>=0?sorted[idxBinary]:"not found") + " time=" + (t3-t2) + " ns");

        // Show complexity notes
        System.out.println();
        System.out.println("Complexity: Linear search O(n), Binary search O(log n) on sorted data.");
        System.out.println("Note: Binary search requires sorted data; sorting costs O(n log n) unless maintained.");
    }
}
