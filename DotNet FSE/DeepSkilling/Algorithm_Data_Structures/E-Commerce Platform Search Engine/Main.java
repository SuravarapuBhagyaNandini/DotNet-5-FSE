import java.util.Arrays;

public class Main {
    public static void main(String[] args) {
        Product[] products = new Product[] {
            new Product(1, "Wireless Mouse", "Electronics"),
            new Product(2, "USB-C Cable", "Electronics"),
            new Product(3, "Water Bottle", "Home"),
            new Product(4, "Notebook", "Office"),
            new Product(5, "Desk Lamp", "Home")
        };

        // Linear search (unsorted array)
        System.out.println("Linear search: find 'Notebook'");
        int idx = LinearSearch.findByName(products, "Notebook");
        System.out.println(idx >= 0 ? "Found: " + products[idx] : "Not found");

        // Prepare sorted array for binary search
        Product[] sorted = Arrays.copyOf(products, products.length);
        Arrays.sort(sorted);

        System.out.println("Binary search: find 'Desk Lamp'");
        int idx2 = BinarySearch.findByName(sorted, "Desk Lamp");
        System.out.println(idx2 >= 0 ? "Found: " + sorted[idx2] : "Not found");
    }
}
