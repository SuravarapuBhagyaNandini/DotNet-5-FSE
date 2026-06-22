public class LinearSearch {
    // Returns index of first match or -1
    public static int findByName(Product[] products, String name) {
        for (int i = 0; i < products.length; i++) {
            if (products[i].getProductName().equalsIgnoreCase(name)) return i;
        }
        return -1;
    }
}
